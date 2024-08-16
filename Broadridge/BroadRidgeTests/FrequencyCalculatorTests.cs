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
            var FrequecyCalculatorService = new FrequencyCalculator();
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
            var FrequecyCalculatorService = new FrequencyCalculator();
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
            var FrequecyCalculatorService = new FrequencyCalculator();
            var words = new string[] { };
            var response = new ConcurrentDictionary<string, int>();


            // Act
            var result = FrequecyCalculatorService.WordFrequencyCalculator(words);

            // Assert
            Assert.Equal(response, result);
        }

        [Fact]
        public void FrequencyCalculator_CaseInsensitiveTest()
        {
            // Arrange
            var frequencyCalculatorService = new FrequencyCalculator();
            var words = new string[] { "Word", "word", "WORD", "wOrD" };
            var expected = new List<KeyValuePair<string, int>>
            {
                new KeyValuePair<string, int>("Word", 4)
            };

            // Act
            var result = frequencyCalculatorService.WordFrequencyCalculator(words);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void FrequencyCalculator_EmptyStringsAndWhitespaceTest()
        {
            // Arrange
            var frequencyCalculatorService = new FrequencyCalculator();
            var words = new string[] { "", "   ", "word" };
            var expected = new List<KeyValuePair<string, int>>
            {
                new KeyValuePair<string, int>("word", 1)
            };

            // Act
            var result = frequencyCalculatorService.WordFrequencyCalculator(words);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void FrequencyCalculator_LargeInputEfficiencyTest()
        {
            // Arrange
            var frequencyCalculatorService = new FrequencyCalculator();
            var words = Enumerable.Repeat("word", 100000).ToArray();
            var expected = new List<KeyValuePair<string, int>>
            {
                new KeyValuePair<string, int>("word", 100000)
            };

            // Act
            var result = frequencyCalculatorService.WordFrequencyCalculator(words);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void FrequencyCalculator_NonAlphabeticalCharsTest()
        {
            // Arrange
            var frequencyCalculatorService = new FrequencyCalculator();
            var words = new string[] { "word1", "word2", "123", "!", "word1" };
            var expected = new List<KeyValuePair<string, int>>
            {
                new KeyValuePair<string, int>("!", 1),
                new KeyValuePair<string, int>("123", 1),
                new KeyValuePair<string, int>("word1", 2),
                new KeyValuePair<string, int>("word2", 1)
            };

            // Act
            var result = frequencyCalculatorService.WordFrequencyCalculator(words);

            // Assert
            Assert.Equal(expected, result);
        }

    }
}
