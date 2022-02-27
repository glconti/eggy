using FluentAssertions;

namespace Eggy.Client.Tests;

public abstract class TestsBase
{
    static TestsBase() =>
        AssertionOptions.AssertEquivalencyUsing(options => options.WithStrictOrdering());
}