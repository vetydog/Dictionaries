using System;
using System.Collections.Generic;

namespace Dictionaries
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Dictionaries dictionaries = new Dictionaries();
            MyDictionary? currentDictionary = null;
            dictionaries.LoadFromFile();

            while (true)
            {
                WaitBeforeRefresh();
                ClearScreen();
                Console.WriteLine("===== MAIN MENU =====");
                Console.WriteLine("1. Create dictionary");
                Console.WriteLine("2. Open dictionary");
                Console.WriteLine("3. Exit");
                Console.Write("Choose: ");

                int choice = ReadNumber();

                switch (choice)
                {
                    case 1:
                        currentDictionary = new MyDictionary();
                        currentDictionary.Create(dictionaries);

                        if (currentDictionary.Name != null)
                            DictionaryMenu(currentDictionary);

                        break;

                    case 2:
                        if (dictionaries.ListDictionary.Count == 0)
                        {
                            Console.WriteLine("No dictionaries.");
                            Pause();
                            break;
                        }

                        Console.WriteLine("Available dictionaries:");

                        for (int i = 0; i < dictionaries.ListDictionary.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}. {dictionaries.ListDictionary[i].Name}");
                        }

                        Console.Write("Choose: ");
                        int index = ReadNumber() - 1;

                        if (index >= 0 && index < dictionaries.ListDictionary.Count)
                        {
                            currentDictionary = dictionaries.ListDictionary[index];
                            currentDictionary.ImportDictionary();
                            DictionaryMenu(currentDictionary);
                        }

                        break;

                    case 3:
                        return;
                }
            }
        }

        static void DictionaryMenu(MyDictionary dic)
        {
            while (true)
            {
                WaitBeforeRefresh();
                ClearScreen();
                Console.WriteLine($"===== {dic.Name} =====");
                Console.WriteLine("1. Add word");
                Console.WriteLine("2. Add translation");
                Console.WriteLine("3. Delete word");
                Console.WriteLine("4. Delete translation");
                Console.WriteLine("5. Change word");
                Console.WriteLine("6. Change translation");
                Console.WriteLine("7. Search translation");
                Console.WriteLine("8. Back");

                int choice = ReadNumber();

                switch (choice)
                {
                    case 1:
                        {
                            Console.Write("Word: ");
                            string word = ReadText();

                            Console.Write("Number of translations: ");

                            int n;

                            while (!int.TryParse(Console.ReadLine(), out n) || n <= 0)
                            {
                                Console.Write("Enter a positive integer: ");
                            }


                            List<string> list = new List<string>();

                            for (int i = 0; i < n; i++)
                            {
                                Console.Write($"Translation {i + 1}: ");
                                list.Add(ReadText());
                            }

                            dic.AddWordAndTranslate(word, list);
                            break;
                        }

                    case 2:
                        {
                            Console.Write("Word: ");
                            string word = ReadText();

                            Console.Write("Translation: ");
                            string tr = ReadText();

                            dic.AddTranslate(word, tr);
                            break;
                        }

                    case 3:
                        {
                            Console.Write("Word: ");
                            dic.DeleteWord(ReadText());
                            break;
                        }

                    case 4:
                        {
                            Console.Write("Word: ");
                            string word = ReadText();

                            Console.Write("Translation: ");
                            string tr = ReadText();

                            dic.DeleteTranslate(word, tr);
                            break;
                        }

                    case 5:
                        {
                            Console.Write("Old word: ");
                            string oldWord = ReadText();

                            Console.Write("New word: ");
                            string newWord = ReadText();

                            dic.ChangeWord(oldWord, newWord);
                            break;
                        }

                    case 6:
                        {
                            Console.Write("Word: ");
                            string word = ReadText();

                            Console.Write("Old translation: ");
                            string oldTr = ReadText();

                            Console.Write("New translation: ");
                            string newTr = ReadText();

                            dic.ChangeTranslate(word, oldTr, newTr);
                            break;
                        }

                    case 7:
                        {
                            Console.Write("Word: ");
                            dic.SearchTranslation(ReadText());
                            Console.WriteLine();
                            Pause();
                            break;
                        }

                    case 8:
                        return;
                }

                Console.WriteLine("\nPress any key...");
                Pause();
            }
        }

        static string ReadText()
        {
            return Console.ReadLine()?.Trim() ?? string.Empty;
        }

        static int ReadNumber()
        {
            return int.TryParse(Console.ReadLine(), out int value) ? value : -1;
        }

        static void ClearScreen()
        {
            if (!Console.IsOutputRedirected)
                Console.Clear();
        }

        static void Pause()
        {
            if (!Console.IsInputRedirected)
                Console.ReadKey();
        }

        static void WaitBeforeRefresh()
        {
            if (!Console.IsInputRedirected)
                Thread.Sleep(2000);
        }
    }
}