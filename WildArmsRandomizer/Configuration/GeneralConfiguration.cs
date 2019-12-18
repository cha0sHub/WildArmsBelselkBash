using System;
using System.Collections.Generic;
using System.Text;

namespace WildArmsRandomizer.Configuration
{
    public class GeneralConfiguration
    {
        public string InputFile { get; set; } = "wild_arms.bin";
        public string Seed { get; set; }
        public string TempFile { get; set; } = "wa1.bin";
        public string OutputFile { get; set; } 
    }
}
