using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace Dictionaries
{
    internal class Dictionaries
    {
        private List<MyDictionary> listDic;

       public List<MyDictionary> ListDictionary
        {
            get { return listDic; }
        }
       public Dictionaries() {
            listDic = new List<MyDictionary>();
        }

        public List<MyDictionary> ExportToList()
        {
            return listDic;
        }

        public void Add(MyDictionary dictionary) { 
                listDic.Add(dictionary);
        }

    public void LoadFromFile()
    {
        string[] files = Directory.GetFiles(Environment.CurrentDirectory, "*.txt");

        foreach (string file in files)
        {
            string name = Path.GetFileNameWithoutExtension(file);

            MyDictionary dic = new MyDictionary(name);

            listDic.Add(dic);
        }
}

    }
}
