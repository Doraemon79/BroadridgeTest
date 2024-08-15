namespace Broadridge.Logic.Interfaces
{
    public interface IHelper
    {
        string[] SplitWords(string text);
        void FlushOutput(List<KeyValuePair<string, int>> wordsbyFrequecy);
    }
}
