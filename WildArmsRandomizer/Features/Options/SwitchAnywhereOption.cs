using System.IO;
using WildArmsRandomizer.Management;

namespace WildArmsRandomizer.Features.Options
{
    internal class SwitchAnywhereOption : ISwitchAnywhereOption
    {
        private IRandomizerAgent Agent { get; set; }

        public SwitchAnywhereOption(IRandomizerAgent agent)
        {
            Agent = agent;
        }

        public void ApplySwitchAnywhereOption()
        {
            using (var fs = File.Open(Agent.GeneralConfiguration.TempFile, FileMode.Open, FileAccess.ReadWrite))
            {
                fs.Seek(724596, SeekOrigin.Begin);
                fs.WriteByte(0x01);
                fs.WriteByte(0x00);
                fs.WriteByte(0x02);
                fs.WriteByte(0x24);

                fs.Seek(724320, SeekOrigin.Begin);
                fs.WriteByte(0x01);
                fs.WriteByte(0x00);
                fs.WriteByte(0x02);
                fs.WriteByte(0x24);

                fs.Seek(725200, SeekOrigin.Begin);
                fs.WriteByte(0x01);
                fs.WriteByte(0x00);
                fs.WriteByte(0x02);
                fs.WriteByte(0x24);

                fs.Seek(725264, SeekOrigin.Begin);
                fs.WriteByte(0x01);
                fs.WriteByte(0x00);
                fs.WriteByte(0x02);
                fs.WriteByte(0x24);

                fs.Seek(760464, SeekOrigin.Begin);
                fs.WriteByte(0x01);
                fs.WriteByte(0x00);
                fs.WriteByte(0x02);
                fs.WriteByte(0x24);
            }
        }
    }
}
