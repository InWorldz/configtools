using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace IWConfigLib
{
    public class ConfigGenerator
    {
        private Dictionary<string, string> _extraValues = new Dictionary<string, string>();
        private string _valueFilePath;
        private List<ConfigTemplate> _templates = new List<ConfigTemplate>();

        public ConfigGenerator(string templateDir, string valueFile)
        {
            _valueFilePath = valueFile;
            this.LoadTemplates(templateDir);
        }

        private void LoadTemplates(string templateDir)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(templateDir);
            FileInfo[] files = dirInfo.GetFiles("*.tmpl");

            foreach (FileInfo fileInfo in files)
            {
                _templates.Add(new ConfigTemplate(fileInfo.FullName));
            }
        }

        public void RunTemplates(string outputDir)
        {
            ConfigValuesFile values = this.SetupValues();

            foreach (ConfigTemplate template in _templates)
            {
                string filledTemplate = template.FillTemplate(values);
                string fullOutPath = Path.Combine(outputDir, Path.GetFileName(template.FileName.Replace(".tmpl", "")));

                File.WriteAllText(fullOutPath, filledTemplate, Encoding.ASCII);
            }
        }

        public void AddValue(string key, string value)
        {
            _extraValues.Add(key, value);
        }

        private ConfigValuesFile SetupValues()
        {
            ConfigValuesFile cv = new ConfigValuesFile(_valueFilePath);
            
            //also add values pushed to us
            foreach (KeyValuePair<string, string> val in _extraValues)
            {
                cv.Add(val.Key, val.Value);
            }

            //validate that there arent any {REQUIRE} values undefined
            cv.Validate();

            return cv;
        }

        public bool ParseCommandLine(string[] cmdLine)
        {
            foreach (string cmd in cmdLine)
            {
                if (cmd.Contains('='))
                {
                    KeyValuePair<string, string> kvp;
                    if (Util.ParseConfigLine(cmd, out kvp))
                    {
                        AddValue(kvp.Key, kvp.Value);
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
