using System.IO;
using WildArmsRandomizer.Management;

namespace WildArmsRandomizer.Features.Options
{
    internal class AlwaysRunOption : IAlwaysRunOption
    {
        private IRandomizerAgent Agent { get; }

        public AlwaysRunOption(IRandomizerAgent agent)
        {
            Agent = agent;
        }

        //Rewrites run away code to always evaluate to true.
        public void ApplyAlwaysRunOption()
        {
            using (var fs = File.Open(Agent.GeneralConfiguration.TempFile, FileMode.Open, FileAccess.ReadWrite))
            {
                fs.Seek(0xC5ECC, SeekOrigin.Begin);
                fs.WriteByte(02);
                fs.WriteByte(00);
                fs.WriteByte(0x01);
                fs.WriteByte(0x04);
            }
        }
    }
}
