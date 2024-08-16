using Broadridge.Logic.Interfaces;
using System.Collections.Concurrent;

namespace Broadridge.Logic
{
    /// <summary>
    /// class to handle the frequency of words
    /// </summary>
    public class FrequencyCalculator : IFrequencyCalculator
    {
        /// <summary>
        /// I tried with cache  but with a concurrent dictionary it is faster
        ///  with just a simple tryAdd
        ///  tried using a cache with async but the results were unreliable, needs more time to study
        /// </summary>
        /// <param name="words"></param>
        /// <returns></returns>
        public List<KeyValuePair<string, int>> WordFrequencyCalculator(string[] words)
        {

            var wordsByFrequency = new ConcurrentDictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            var localDictionaries = new ConcurrentBag<Dictionary<string, int>>();

            // Filter out empty or whitespace-only strings before processing
            var filteredWords = words.Where(word => !string.IsNullOrWhiteSpace(word));

            Parallel.ForEach(filteredWords, word =>
            {
                wordsByFrequency.AddOrUpdate(word, 1, (key, oldValue) => oldValue + 1);

            });



            return [.. wordsByFrequency.OrderBy(x => x.Key).ThenBy(x => x.Value)];
        }
    }
}
