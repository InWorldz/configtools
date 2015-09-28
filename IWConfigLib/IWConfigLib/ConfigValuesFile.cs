/*
 * Copyright (c) 2015, InWorldz Halcyon Developers
 * All rights reserved.
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace IWConfigLib
{
    public class ConfigValuesFile : IEnumerable<KeyValuePair<string, string>>
    {
        private const string REQUIRE_TOKEN = "{REQUIRE}";

        private Dictionary<string, string> _kvps = new Dictionary<string,string>();


        public ConfigValuesFile(string filePath)
        {
            using (StreamReader reader = File.OpenText(filePath))
            {
                string line = reader.ReadLine();
                while (line != null)
                {
                    this.ParseLine(line);
                    line = reader.ReadLine();
                }
            }
        }

        private void ParseLine(string line)
        {
            KeyValuePair<string, string> output;
            if (Util.ParseConfigLine(line, out output))
            {
                _kvps.Add(output.Key, output.Value);
            }
        }

        public void Add(string key, string value)
        {
            if (!_kvps.ContainsKey(key))
            {
                _kvps.Add(key, value);
            }
            else
            {
                if (_kvps[key] == REQUIRE_TOKEN)
                {
                    _kvps.Remove(key);
                    _kvps.Add(key, value);
                }
                else
                {
                    throw new ArgumentException(String.Format("Key '{0}' already defined", key));
                }
            }
        }

        public void Validate()
        {
            foreach (KeyValuePair<string, string> kvp in _kvps)
            {
                if (kvp.Value == REQUIRE_TOKEN)
                {
                    throw new ValueMissingException(String.Format("The required configuration value for {0} is missing", kvp.Key));
                }
            }
        }


        #region IEnumerable<KeyValuePair<string,string>> Members

        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
        {
            return _kvps.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _kvps.GetEnumerator();
        }

        #endregion
    }
}
