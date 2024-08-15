using Broadridge.Logic.Interfaces;
using System.Collections.Concurrent;

namespace Broadridge.Logic
{
    /// <summary>
    /// class to handle the frequency of words
    /// </summary>
    public class FrequecyCalculator : IFrequencyCalculator
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

            //by using tryAdd I reduce the updates to the dictionary
            Parallel.ForEach(words, word =>
            {
                if (!wordsByFrequency.TryAdd(word, 1))
                {

                    wordsByFrequency.TryUpdate(word, wordsByFrequency[word] + 1, wordsByFrequency[word]);
                }
            });

            return [.. wordsByFrequency.OrderBy(x => x.Key).ThenBy(x => x.Value)];
        }
    }
}
