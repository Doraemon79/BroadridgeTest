using Broadridge.Logic.Interfaces;
using System.Runtime.Caching;

namespace Broadridge.Logic
{
    public class FrequecyCalculator : IFrequencyCalculator
    {
        private readonly MemoryCache wordCache = MemoryCache.Default;
        public List<KeyValuePair<string, int>> WordFrequencyCalculator(string[] words)
        {



            return default;
        }


        private async Task<bool> Cachemaker(MemoryCache cache, string word)
        {
            return default;
        }
    }

}
