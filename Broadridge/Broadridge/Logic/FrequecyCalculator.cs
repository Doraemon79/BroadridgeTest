using Broadridge.Logic.Interfaces;
using System.Collections.Concurrent;
using System.Runtime.Caching;

namespace Broadridge.Logic
{
    public class FrequecyCalculator : IFrequencyCalculator
    {
        private readonly MemoryCache wordCache = MemoryCache.Default;
        public List<KeyValuePair<string, int>> WordFrequencyCalculator(string[] words)
        {

            var wordsByFrequency = new ConcurrentDictionary<string, int>(StringComparer.OrdinalIgnoreCase);

            Parallel.ForEach(words, async word =>
            {

                await Cachemaker(wordCache, word);
                if (wordCache.Contains(word))
                {
                    wordsByFrequency.AddOrUpdate(word, 1, (key, oldValue) => oldValue + 1);
                }
                else
                {
                    wordsByFrequency.TryAdd(word, 1);
                }
            });
            return wordsByFrequency.OrderByDescending(x => x.Value).ThenBy(x => x.Key).ToList();
        }


        private async Task<bool> Cachemaker(MemoryCache cache, string word)
        {
            if (wordCache.Contains(word))
            {
                return true;
            }
            else
            {
                CacheItemPolicy policy = new CacheItemPolicy { SlidingExpiration = TimeSpan.FromMinutes(10) };
                wordCache.Add(word, 1, policy);
                return false;
            }
        }
    }
}
