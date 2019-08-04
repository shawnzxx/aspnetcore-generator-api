using api.Controllers;
using Faker;
using System;
using System.Linq;
using Xunit;

namespace tests
{
    public class RangeTests
    {
        [Fact]
        public void CountShouldControlNumberOfResults()
        {
            //Arrange
            var range = new Range { Count = 3, Sort = true };

            //Act
            var generated = range.Of(Name.FullName);

            //Assert
            Assert.Equal(3, generated.Count());
        }

        [Fact]
        public void SortShouldOrderResults()
        {
            var range = new Range { Count = 3, Sort = true };
            var values = new[] { "a", "c", "b" };
            var counter = 0;
            var generated = range.Of(() => values[counter++]);

            Assert.Equal(new[] { "a", "b", "c" }, generated.ToArray());
        }
    }
}
