using Broadridge.Logic;
using System.Collections.Concurrent;

namespace BroadRidgeTests
{
    public class FrequencyCalculatorTests
    {
        [Fact]
        public void FrequencyCalculator_returnsTrue()
        {
            // Arrange
            var FrequecyCalculatorService = new FrequecyCalculator();
            var words = new string[] { "a", "b", "a", "b", "c", "a", "b", "a" };
            var response = new ConcurrentDictionary<string, int>();

            response.TryAdd("c", 1);
            response.TryAdd("b", 3);
            response.TryAdd("a", 4);
            var expected = response.OrderByDescending(x => x.Value).ThenBy(x => x.Key).ToList();

            // Act
            var result = FrequecyCalculatorService.WordFrequencyCalculator(words);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void FrequencyCalculator_NotEqual_returnsFalse()
        {
            // Arrange
            var FrequecyCalculatorService = new FrequecyCalculator();
            var words = new string[] { "a", "b", "b", "c", "a", "b", "a" };
            var response = new ConcurrentDictionary<string, int>();
            response.TryAdd("a", 2);
            response.TryAdd("b", 3);
            response.TryAdd("c", 1);

            // Act
            var result = FrequecyCalculatorService.WordFrequencyCalculator(words);

            // Assert
            Assert.NotEqual(response, result);
        }

        [Fact]
        public void FrequencyCalculator_Givenempty_returnsTrue()
        {
            // Arrange
            var FrequecyCalculatorService = new FrequecyCalculator();
            var words = new string[] { };
            var response = new ConcurrentDictionary<string, int>();


            // Act
            var result = FrequecyCalculatorService.WordFrequencyCalculator(words);

            // Assert
            Assert.Equal(response, result);
        }

    }
}
