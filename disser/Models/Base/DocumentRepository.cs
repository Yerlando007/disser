using Spire.Doc;
using Spire.Doc.Documents;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace disser.Models.Base
{
    /*public interface IDocumentRepository
    {
        List<string> GetWordsList(string path);
        string GetHelloTest();
    }*/

    public class DocumentRepository
    {
        private static readonly Dictionary<string, List<string>> _wordLibrary = new Dictionary<string, List<string>>();
        private static string _pathToTXTFiles = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot//TXT");
        public List<string> _stopWords()
        {
            var vvodnieWords = File.ReadAllText(Path.Combine(_pathToTXTFiles, "вводныеслова.yerlan")).Replace("\r\n   ", "").Split(',');
            var fkPlace = File.ReadAllText(Path.Combine(_pathToTXTFiles, "местоимения.yerlan")).Replace("\r\n   ", "").Split(',');
            var narech = File.ReadAllText(Path.Combine(_pathToTXTFiles, "наречия.yerlan")).Replace("\r\n   ", "").Split(',');
            var predlog = File.ReadAllText(Path.Combine(_pathToTXTFiles, "предлоги.yerlan")).Replace("\r\n   ", "").Split(',');
            var ussr = File.ReadAllText(Path.Combine(_pathToTXTFiles, "союзы.yerlan")).Replace("\r\n   ", "").Split(',');
            var stopWord = vvodnieWords.Concat(fkPlace).Concat(narech).Concat(predlog).Concat(ussr).ToList();
            return stopWord;
        }
        public string GetHelloTest() => "Привет!";

        public DocumentRepository() { }

        public Dictionary<string, int> GetRepeatingWords(string pathToFile, int repeatsCount) => GetRepeatingWords(GetWordsListStatic(pathToFile), repeatsCount);
        public Dictionary<string, int> GetRepeatingWords(List<string> words, int repeatsCount)
        {
            Dictionary<string, int> repeatingWords = _getFilteredDictionary(words);

            //Заполнение словаря из слов, которые повторились <repeatsCount> и более раз
            Dictionary<string, int> resultWords = new Dictionary<string, int>();
            for (int i = 0; i < repeatingWords.Count; i++)
                if (repeatingWords.Values.ElementAt(i) >= repeatsCount)
                    resultWords.Add(repeatingWords.Keys.ElementAt(i), repeatingWords.Values.ElementAt(i));

            return resultWords;
        }

        public static List<string> GetWordsListStatic(string pathToFile)
        {
            List<string> slova = new List<string>();

            if (Path.GetExtension(pathToFile) == ".docx")
            {
                //Load Document
                Document document = new Document();
                document.LoadFromFile(pathToFile);

                //Initialzie StringBuilder Instance
                StringBuilder sb = new StringBuilder();

                //Extract Text from Word and Save to StringBuilder Instance
                foreach (Section section in document.Sections)
                    foreach (Paragraph paragraph in section.Paragraphs)
                        sb.AppendLine(paragraph.Text);

                slova = sb.ToString().Split(' ').ToList();
            }
            else if (Path.GetExtension(pathToFile) == ".txt")
                slova = File.ReadAllText(pathToFile).Split(' ').ToList();

            return slova;
        }


        //PRIVATE ФУНКЦИИ
        private Dictionary<string, int> _getFilteredDictionary(List<string> words)
        {
            Dictionary<string, int> repeatingWords = new Dictionary<string, int>();
            var stopWords = _stopWords();
            //Фильтр слов и добавление в словарь
            foreach (string slovo in words)
            {
                string tword = slovo.Trim().ToLower();
                string word = string.Empty;
                for (int i = 0; i < tword.Length; i++)
                    if (char.IsLetter(tword[i]))
                        word += tword[i].ToString();

                if (stopWords.Contains(word))
                    continue;

                if (repeatingWords.ContainsKey(word))
                {
                    repeatingWords[word]++;
                }
                else
                {
                    repeatingWords.Add(word, 1);
                }
            }

            return repeatingWords;
        }
    }
}
