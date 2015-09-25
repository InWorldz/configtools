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
