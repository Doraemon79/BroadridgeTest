using BenchmarkDotNet.Attributes;
using Broadridge.Logic;
using System.Collections.Concurrent;
using System.Reflection;
using System.Runtime.Caching;

namespace BenchmarkTests
{
    public class BenchmarkMethods
    {
        private readonly MemoryCache wordCache = MemoryCache.Default;

        [Benchmark]
        public List<KeyValuePair<string, int>> UsedBenchmark()
        {
            string filePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "TestBenchmark.txt");
            var words = SplitWords(filePath);
            var FrequecyCalculatorService = new FrequencyCalculator();
            return FrequecyCalculatorService.WordFrequencyCalculator(words);
        }

        [Benchmark]
        public List<KeyValuePair<string, int>> LocalDictionariesBenchmark()
        {
            string filePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "TestBenchmark.txt");
            var words = SplitWords(filePath);
            var FrequecyCalculatorService = new FrequencyCalculator();
            return FrequecyCalculatorService.WordFrequencyCalculator(words);
        }

        [Benchmark]
        public List<KeyValuePair<string, int>> WithNoCachingBenchmark()
        {
            string filePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "TestBenchmark.txt");
            var words = SplitWords(filePath);
            var filteredWords = words.Where(word => !string.IsNullOrWhiteSpace(word));

            return WordFrequencyCalculatorWithCaching(words);
        }

        [Benchmark]
        public List<KeyValuePair<string, int>> WithTryAdd()
        {
            string filePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "TestBenchmark.txt");
            var words = SplitWords(filePath);

            return WordFrequencyCalculatorWithTryAdd(words);

        }

        [Benchmark]
        public List<KeyValuePair<string, int>> WithCachingBenchmark()
        {
            var FrequecyCalculatorService = new FrequencyCalculator();
            string filePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "TestBenchmark.txt");
            var words = SplitWords(filePath);

            return WordFrequencyCalculator(words);

        }


        public List<KeyValuePair<string, int>> LocalDictionaries(string[] words)
        {

            var wordsByFrequency = new ConcurrentDictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            var localDictionaries = new ConcurrentBag<Dictionary<string, int>>();

            // Filter out empty or whitespace-only strings before processing
            var filteredWords = words.Where(word => !string.IsNullOrWhiteSpace(word));

            Parallel.ForEach(filteredWords, () => new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase), (word, state, localDict) =>
            {
                if (localDict.ContainsKey(word))
                {
                    localDict[word]++;
                }
                else
                {
                    localDict[word] = 1;
                }
                return localDict;
            },
            localDict =>
            {
                localDictionaries.Add(localDict);
            });

            // Join results from local dictionaries
            var finalDictionary = new ConcurrentDictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            foreach (var localDict in localDictionaries)
            {
                foreach (var kvp in localDict)
                {
                    finalDictionary.AddOrUpdate(kvp.Key, kvp.Value, (key, oldValue) => oldValue + kvp.Value);
                }
            }

            return finalDictionary.OrderBy(x => x.Key).ThenBy(x => x.Value).ToList();


        }

        public List<KeyValuePair<string, int>> WordFrequencyCalculator(string[] words)
        {

            var wordsByFrequency = new ConcurrentDictionary<string, int>(StringComparer.OrdinalIgnoreCase);

            //some articles advise to use tryget to verify the key exists before adding but 

            Parallel.ForEach(words, word =>
            {
                wordsByFrequency.AddOrUpdate(word, 1, (key, oldValue) => oldValue + 1);

            });

            return [.. wordsByFrequency.OrderBy(x => x.Key).ThenBy(x => x.Value)];
        }





        public List<KeyValuePair<string, int>> WordFrequencyCalculatorWithCaching(string[] words)
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



        public List<KeyValuePair<string, int>> WordFrequencyCalculatorWithTryAdd(string[] words)
        {

            var wordsByFrequency = new ConcurrentDictionary<string, int>(StringComparer.OrdinalIgnoreCase);

            //some articles advise to use tryget to verify the key exists before adding but 
            Parallel.ForEach(words, async word =>
            {
                if (!wordsByFrequency.TryAdd(word, 1))
                {

                    wordsByFrequency.TryUpdate(word, wordsByFrequency[word] + 1, wordsByFrequency[word]);
                }
            });

            return [.. wordsByFrequency.OrderBy(x => x.Key).ThenBy(x => x.Value)];
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

        public string[] SplitWords(string text)
        {
            var words = text.Split(new[] { ' ', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries); //what is new line in windows-1252??
            return words;
        }
    }
}
