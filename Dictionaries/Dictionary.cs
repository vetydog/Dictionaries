using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Xml.Linq;
using static System.Console;

namespace Dictionaries
{
    internal class MyDictionary : IDictionary
    {
        private string? _name;

        public string? Name
        {
            get { return _name; }
        }

        private Dictionary<string, List<string>> words;


        public MyDictionary()
        {
            words = new Dictionary<string, List<string>>();
        }

        public MyDictionary(string name)
        {
            _name = name;
            words = new Dictionary<string, List<string>>();
        }

        public Dictionary<string, string> ImportLanguages()
        {
            string json = File.ReadAllText("languages.json");

            Dictionary<string, string> languages = JsonSerializer.Deserialize<Dictionary<string, string>>(json);

            return languages;
        }

        public void Create(Dictionaries dic)
        {
            Dictionary<string, string> languages = ImportLanguages();
            WriteLine("Enter the first language for dictionary:"); 
            string lan1 = ReadLine();
            if (!languages.ContainsKey(lan1)) {
                WriteLine("The language is not exist");
                return;
            }
            WriteLine("Enter the second language for dictionary:"); 
            string lan2 = ReadLine();
            if (!languages.ContainsKey(lan2))
            {
                WriteLine("The language is not exist");
                return;
            }
            string name = $"{lan1}-{lan2}";
            foreach (var dictionary in dic.ListDictionary)
            {
                if (dictionary.Name == name)
                {
                    WriteLine("The dictionary already exists");
                    return;
                }
            }

            _name = $"{lan1}-{lan2}";
            dic.Add(this);

            WriteLine("Dictionary created!");

            File.Create(_name+ ".txt").Close();
        }
        public void AddWordAndTranslate( string key, List<string> value)
        {
            if (words.ContainsKey(key))
            {
                WriteLine("Word already exists");
                return;
            }

            words.Add(key, value);
            ExportDictionary();
            WriteLine("Word is added");
        }

        public void AddTranslate( string key, string translate)
        {
            if (!words.ContainsKey(key))
            {
                WriteLine("This word is not in the dictionary");
                return;
            }
                if (words.ContainsKey(key))
            {
                if (words[key].Contains(translate))
                {
                    WriteLine("Translate already exists");
                    return;
                }

                words[key].Add(translate);
            }
            else
            {
                words.Add(key, new List<string> { translate });
            }
            
            ExportDictionary();
            WriteLine("Translation is added");
        }

        public void DeleteWord(string key)
        {
            if (!words.ContainsKey(key))
            {
                Console.WriteLine("The word does not exist");
                return;
            }

            words.Remove(key);
            ExportDictionary();

            Console.WriteLine("Word deleted");
        }

        public void DeleteTranslate(string key, string translate)
        {
            if (!words.ContainsKey(key))
            {
                Console.WriteLine("The word does not exist");
                return;
            }

            if (!words[key].Contains(translate))
            {
                Console.WriteLine("Translation does not exist");
                return;
            }

            if (words[key].Count == 1)
            {
                Console.WriteLine("This is the last translation. You cannot delete it.");
                return;
            }

            words[key].Remove(translate);
            ExportDictionary();

            Console.WriteLine("Translation deleted");
        }

        public void ChangeWord( string oldKey, string newKey)
        {
            if (!words.ContainsKey(oldKey))
            {
                WriteLine("The word does not exist");
                return;
            }

            words[newKey] = words[oldKey];
            words.Remove(oldKey);

            WriteLine("Word changed");

            ExportDictionary();
        }

        public void ChangeTranslate( string key, string oldValue, string newValue)
        {
            if (!words.ContainsKey(key))
            {
                WriteLine("The word does not exist");
                return;
            }

            if (!words[key].Contains(oldValue))
            {
                WriteLine("Translation does not exist");
                return;
            }

            if (words[key].Contains(newValue))
            {
                WriteLine("Translate already exists");
                return;
            }

            words[key].Remove(oldValue);
            words[key].Add(newValue);

            WriteLine("Translation changed");

            ExportDictionary();
        }

        public void SearchTranslation( string key)
        {
            if (!words.ContainsKey(key))
            {
                WriteLine("The word is not exists");
                return;
            }
            var t = from i in words[key] select i;
            foreach (var t2 in t) {
                Write(t2 + " ");
            }
            return;
        }


        private void ExportDictionary()
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(_name + ".txt", false, Encoding.UTF8))
                {
                    foreach (var item in words.OrderBy(x => x.Key))
                    {
                        sw.Write(item.Key + ": ");

                        for (int i = 0; i < item.Value.Count; i++)
                        {
                            sw.Write(item.Value[i]);

                            if (i < item.Value.Count - 1)
                            {
                                sw.Write(", ");
                            }
                        }

                        sw.WriteLine();
                    }
                }
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("File access denied");
            }
            catch (IOException)
            {
                Console.WriteLine("Error while working with the file");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void ImportDictionary()
        {
            words.Clear();

            foreach (string line in File.ReadAllLines(_name + ".txt"))
            {
                string[] parts = line.Split(':');

                string word = parts[0].Trim();

                List<string> translates = new List<string>();

                foreach (string t in parts[1].Split(','))
                {
                    if (t.Trim() != "")
                        translates.Add(t.Trim());
                }

                words.Add(word, translates);
            }
        }


















    }
}
