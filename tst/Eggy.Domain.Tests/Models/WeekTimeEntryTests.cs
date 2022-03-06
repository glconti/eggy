using Eggy.Domain.Models;
using FluentAssertions;
using Xunit;

namespace Eggy.Domain.Tests.Models;

public class WeekTimeEntryTests : TestsBase
{
    public class Generate
    {
        [Fact]
        public void ExpectCorrectId()
        {
            //Arrange
            var date = new DateOnly(2022, 02, 21);

            //Act
            var weekTimeEntry = WeekTimeEntry.Generate(date);

            //Assert
            weekTimeEntry.Id.Should().Be("2022-9");
        }

        [Fact]
        public void ExpectSevenCorrectTimeEntries()
        {
            //Arrange
            var date = new DateOnly(2022, 02, 21);

            //Act
            var weekTimeEntry =  WeekTimeEntry.Generate(date);

            //Assert
            weekTimeEntry.TimeEntries.Should().BeEquivalentTo(new[]
            {
                new DayTimeEntry {Date = new DateOnly(2022, 02, 21)},
                new DayTimeEntry {Date = new DateOnly(2022, 02, 22)},
                new DayTimeEntry {Date = new DateOnly(2022, 02, 23)},
                new DayTimeEntry {Date = new DateOnly(2022, 02, 24)},
                new DayTimeEntry {Date = new DateOnly(2022, 02, 25)},
                new DayTimeEntry {Date = new DateOnly(2022, 02, 26)},
                new DayTimeEntry {Date = new DateOnly(2022, 02, 27)}
            });
        }
    }
}