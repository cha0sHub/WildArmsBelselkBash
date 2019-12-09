using System.IO;
using WildArmsRandomizer.Management;

namespace WildArmsRandomizer.Features.Options
{
    internal class CeciliaSpellsOption : ICeciliaSpellsOption
    {

        private IRandomizerAgent Agent { get; }

        public CeciliaSpellsOption(IRandomizerAgent agent)
        {
            Agent = agent;
        }

        public void ChangeCeciliaStartingSpells()
        {
            using (var fs = File.Open(Agent.GeneralConfiguration.TempFile, FileMode.Open, FileAccess.ReadWrite))
            {
                fs.Seek(1951587, SeekOrigin.Begin);
                fs.WriteByte(0x2F); // Teleport
                fs.WriteByte(0x36); // Randomizer
            }
        }

    }
}
