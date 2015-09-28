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
