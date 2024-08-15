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
        /// I tried with cache and tryGet() but with a concurrent dictionary it is faster
        ///  with just a simple addOrUpdate
        ///  tried using a cache with async but the results were unreliable, needs more time to study
        /// </summary>
        /// <param name="words"></param>
        /// <returns></returns>
        public List<KeyValuePair<string, int>> WordFrequencyCalculator(string[] words)
        {

            var wordsByFrequency = new ConcurrentDictionary<string, int>(StringComparer.OrdinalIgnoreCase);


            Parallel.ForEach(words, async word =>
            {
                wordsByFrequency.AddOrUpdate(word, 1, (key, oldValue) => oldValue + 1);

            });

            return wordsByFrequency.OrderBy(x => x.Key).ThenBy(x => x.Value).ToList();
        }
    }
}
