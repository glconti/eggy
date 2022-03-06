using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Nuke.Common;
using Nuke.Common.CI;
using Nuke.Common.CI.TeamCity;
using Nuke.Common.Execution;
using Nuke.Common.Git;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.Coverlet;
using Nuke.Common.Tools.DotCover;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Tools.GitVersion;
using Nuke.Common.Tools.ReportGenerator;
using Nuke.Common.Utilities.Collections;
using static Nuke.Common.EnvironmentInfo;
using static Nuke.Common.IO.FileSystemTasks;
using static Nuke.Common.IO.PathConstruction;
using static Nuke.Common.Tools.DotNet.DotNetTasks;
using static Nuke.Common.Tools.ReportGenerator.ReportGeneratorTasks;
using static Nuke.Common.Tools.DotCover.DotCoverTasks;

[CheckBuildProjectConfigurations]
[ShutdownDotNetAfterServerBuild]
class Build : NukeBuild
{
    /// Support plugins are available for:
    ///   - JetBrains ReSharper        https://nuke.build/resharper
    ///   - JetBrains Rider            https://nuke.build/rider
    ///   - Microsoft VisualStudio     https://nuke.build/visualstudio
    ///   - Microsoft VSCode           https://nuke.build/vscode

    public static int Main () => Execute<Build>(x => x.Compile);

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

    [Solution(GenerateProjects = true)] readonly Solution Solution;
    [GitRepository] readonly GitRepository GitRepository;
    [GitVersion] readonly GitVersion GitVersion;

    AbsolutePath SourceDirectory => RootDirectory / "src";
    AbsolutePath TestsDirectory => RootDirectory / "tst";
    AbsolutePath OutputDirectory => RootDirectory / "output";
    AbsolutePath ArtifactsDirectory => RootDirectory / "artifacts";
    AbsolutePath TestResultDirectory => ArtifactsDirectory / "test-results";
    AbsolutePath ReportDirectory => ArtifactsDirectory / "reports";

    Target Clean => _ => _
        .Before(Restore)
        .Executes(() =>
        {
            SourceDirectory.GlobDirectories("**/bin", "**/obj").ForEach(DeleteDirectory);
            TestsDirectory.GlobDirectories("**/bin", "**/obj").ForEach(DeleteDirectory);
            EnsureCleanDirectory(OutputDirectory);
        });

    Target Restore => _ => _
        .Executes(() =>
        {
            DotNetRestore(s => s
                .SetProjectFile(Solution));
        });

    Target Compile => _ => _
        .DependsOn(Restore)
        .Executes(() =>
        {
            DotNetBuild(s => s
                .SetProjectFile(Solution)
                .SetConfiguration(Configuration)
                .SetAssemblyVersion(GitVersion.AssemblySemVer)
                .SetFileVersion(GitVersion.AssemblySemFileVer)
                .SetInformationalVersion(GitVersion.InformationalVersion)
                //.SetOutputDirectory(OutputDirectory)
                .EnableNoRestore());
        });

    Target Test => _ => _
        .DependsOn(Compile)
        .Produces(TestResultDirectory / "*.trx")
        .Produces(TestResultDirectory / "*.xml")
        .Partition(2)
        .Executes(() =>
        {
            DotNetTest(_ => _
                    .Apply(TestSettingsBase)
                    //.Apply(TestSettings)
                    .CombineWith(TestProjects,
                        (_, v) => _
                            .Apply(TestProjectSettingsBase, v)
                    )
                ,
                completeOnFailure: true)
                ;
        });

    Target Cover => _ => _
        .DependsOn(Test)
        .Consumes(Test)
        .Executes(() =>
        {
            ReportGenerator(_ => _
                .SetReports(TestResultDirectory / "*.xml")
                .SetReportTypes(ReportTypes.HtmlInline)
                .SetTargetDirectory(ReportDirectory)
                .SetFramework("net6.0"));
        });

    Configure<DotNetTestSettings> TestSettingsBase => _ => _
        .SetConfiguration(Configuration)
        .SetNoBuild(SucceededTargets.Contains(Compile))
        //.SetDataCollector("XPlat Code Coverage")
        .ResetVerbosity()
        .SetResultsDirectory(TestResultDirectory)
        .When(InvokedTargets.Contains(Cover) || IsServerBuild, _ => _
            .EnableCollectCoverage()
            .SetCoverletOutputFormat(CoverletOutputFormat.cobertura)
            //.SetExcludeByFile("*.Generated.cs")
            //.When(TeamCity.Instance is not null, _ => _
            //    .SetCoverletOutputFormat((CoverletOutputFormat) $"\\\"{CoverletOutputFormat.cobertura},{CoverletOutputFormat.teamcity}\\\""))
            .When(IsServerBuild, _ => _
                .EnableUseSourceLink()));

    Configure<DotNetTestSettings, Project> TestProjectSettingsBase => (_, v) => _
        .SetProjectFile(v)
        //// https://github.com/Tyrrrz/GitHubActionsTestLogger
        //.When(GitHubActions.Instance is not null && v.HasPackageReference("GitHubActionsTestLogger"), _ => _
        //    .AddLoggers("GitHubActions;report-warnings=false"))
        //// https://github.com/JetBrains/TeamCity.VSTest.TestAdapter
        //.When(TeamCity.Instance is not null && v.HasPackageReference("TeamCity.VSTest.TestAdapter"), _ => _
        //    .AddLoggers("TeamCity")
        //    // https://github.com/xunit/visualstudio.xunit/pull/108
        //    .AddRunSetting("RunConfiguration.NoAutoReporters", bool.TrueString))
        .AddLoggers($"trx;LogFileName={v.Name}.trx")
        .When(InvokedTargets.Contains(Cover) || IsServerBuild, _ => _
            .SetCoverletOutput(TestResultDirectory / $"{v.Name}.xml"));

    IEnumerable<Project> TestProjects => Partition.GetCurrent(Solution.GetProjects("*.Tests"));
}
