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
using NDesk.Options;
using System.Collections.Generic;

namespace ConfigGenRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            string templateDir = null;
            string outputDir = null;
            string valuesFile = null;
            bool help = false;

            var p = new OptionSet() {
                { "o|outputdir",    "The output directory for generated files", v => outputDir = v },
                { "t|templatedir",  "The directory to look into for generator templates", v => templateDir = v },
                { "v|valuesfile",   "(optional) File to read template fill values from", v => valuesFile = v },
                { "h|?|help",       "Prints this help message", v => help = v != null },
            };

            List<string> extra = p.Parse(args);

            if (help)
            {
                p.WriteOptionDescriptions(Console.Out);
                return;
            }

            if (outputDir == null)
            {
                outputDir = Properties.Settings.Default.OutputDir;
            }

            IWConfigLib.ConfigGenerator cg 
                = new IWConfigLib.ConfigGenerator(templateDir, valuesFile);

            if (!cg.ParseCommandLine(args))
            {
                return;
            }

            try
            {
                cg.RunTemplates(outputDir);
            }
            catch (IWConfigLib.ValueMissingException e)
            {
                Console.Error.WriteLine("Configuration failed: " + e.Message);
            }
        }
        
    }
}
