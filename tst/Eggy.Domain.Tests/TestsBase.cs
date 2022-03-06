using System.Diagnostics.CodeAnalysis;
using FluentAssertions;

namespace Eggy.Domain.Tests;

public abstract class TestsBase
{
    [ExcludeFromCodeCoverage]
    static TestsBase() =>
        AssertionOptions.AssertEquivalencyUsing(options => options.WithStrictOrdering());
}