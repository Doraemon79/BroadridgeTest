using Broadridge.Logic.Interfaces;

namespace Broadridge.Logic
{
    /// <summary>
    /// class to handle input and opuput
    /// </summary>
    public class Helper : IHelper
    {
        public string[] SplitWords(string text)
        {
            var words = text.Split(new[] { ' ', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries); //what is new line in windows-1252??
            return words;
        }

        public void FlushOutput(List<KeyValuePair<string, int>> wordsbyFrequecy)
        {
            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string fileName = Path.Combine(docPath, "Broadridge.txt");
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                foreach (var word in wordsbyFrequecy)
                {
                    writer.WriteLine($"{word.Key},{word.Value}\n");
                }
            }
        }
    }
}
