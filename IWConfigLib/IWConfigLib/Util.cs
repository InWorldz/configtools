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

namespace IWConfigLib
{
    public class Util
    {
        static public bool ParseConfigLine(string line, out KeyValuePair<string, string> result)
        {
            if (line != String.Empty)
            {
                int eqIndex = line.IndexOf('=');
                if (eqIndex > 0)
                {
                    string key = line.Substring(0, eqIndex).Trim();
                    string val = line.Substring(eqIndex + 1);

                    result = new KeyValuePair<string, string>(key, val);
                    return true;
                }
            }

            result = new KeyValuePair<string, string>();
            return false;
        }
    }
}
