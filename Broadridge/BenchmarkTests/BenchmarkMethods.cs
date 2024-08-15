using BenchmarkDotNet.Attributes;
using Broadridge.Logic;
using System.Collections.Concurrent;
using System.Runtime.Caching;

namespace BenchmarkTests
{
    public class BenchmarkMethods
    {
        private readonly MemoryCache wordCache = MemoryCache.Default;

        [Benchmark]
        public List<KeyValuePair<string, int>> WithCachingBenchmark()
        {
            var FrequecyCalculatorService = new FrequecyCalculator();
            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string fileName = Path.Combine(docPath, "TestBenchmark.txt");
            string text = File.ReadAllText(fileName);
            var words = SplitWords(text);

            return FrequecyCalculatorService.WordFrequencyCalculator(words);

        }


        [Benchmark]
        public List<KeyValuePair<string, int>> NoCachingBenchmark()
        {
            var FrequecyCalculatorService = new FrequecyCalculator();
            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string fileName = Path.Combine(docPath, "TestBenchmark.txt");
            string text = File.ReadAllText(fileName);
            var words = SplitWords(text);

            return WordFrequencyCalculatorNoCaching(words);

        }
        public List<KeyValuePair<string, int>> WordFrequencyCalculatorNoCaching(string[] words)
        {

            var wordsByFrequency = new ConcurrentDictionary<string, int>(StringComparer.OrdinalIgnoreCase);

            Parallel.ForEach(words, async word =>
            {
                if (wordCache.Contains(word))
                {
                    wordsByFrequency.AddOrUpdate(word, 1, (key, oldValue) => oldValue + 1);
                }

            });


            return wordsByFrequency.OrderBy(x => x.Key).ThenBy(x => x.Value).ToList();
        }

        public string[] SplitWords(string text)
        {
            var words = text.Split(new[] { ' ', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries); //what is new line in windows-1252??
            return words;
        }
    }
}
