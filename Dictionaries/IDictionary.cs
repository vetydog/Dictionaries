using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Dictionaries
{
    internal interface IDictionary
    {
        void Create(Dictionaries dic);
        void AddWordAndTranslate(string key, List<string> value);
        void AddTranslate(string key, string translate);
        void DeleteWord(string key);
        void DeleteTranslate(string key, string translate);
        void ChangeWord (string oldKey, string newKey);
        void ChangeTranslate(string key,string oldValue, string newValue);

        void SearchTranslation(string key);


    }
}
