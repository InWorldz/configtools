using System;
using System.Linq;
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
