using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace IWConfigLib
{
    public class ConfigTemplate
    {
        private string _fileName;
        private string _templateText;

        public string FileName
        {
            get
            {
                return _fileName;
            }
        }

        public ConfigTemplate(string fileName)
        {
            _fileName = fileName;
            using (StreamReader reader = File.OpenText(fileName))
            {
                _templateText = reader.ReadToEnd();
            }
        }

        public string FillTemplate(ConfigValuesFile values)
        {
            string result = _templateText;
            foreach (KeyValuePair<string, string> kvp in values)
            {
                result = result.Replace("%" + kvp.Key + "%", kvp.Value);
            }

            return result;
        }
    }
}
