using System;
using System.Collections.Generic;
using System.Text;

namespace WildArmsRandomizer.Configuration
{
    public class GeneralConfiguration
    {
        public string InputFile { get; } = "wild_arms.bin";
        public string Seed { get; set; } = DateTime.Now.Ticks.ToString();
        public string TempFile { get; } = "wa1.bin";
        public string OutputFile { get; } 
    }
}
