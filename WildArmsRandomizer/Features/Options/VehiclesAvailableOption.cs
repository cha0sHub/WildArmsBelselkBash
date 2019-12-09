using System.IO;
using WildArmsRandomizer.Management;

namespace WildArmsRandomizer.Features.Options
{
    internal class VehiclesAvailableOption : IVehiclesAvailableOption
    {
        private IRandomizerAgent Agent { get; }

        public VehiclesAvailableOption(IRandomizerAgent agent)
        {
            Agent = agent;
        }

        public void ApplyVehiclesAvailableOption()
        {
            using (var fs = File.Open(Agent.GeneralConfiguration.TempFile, FileMode.Open, FileAccess.ReadWrite))
            {
                //Enable Vehicles
                fs.Seek(597884, SeekOrigin.Begin);
                fs.WriteByte(0x03);
                fs.WriteByte(0x00);
                fs.WriteByte(0x00);
                fs.WriteByte(0x14);

                //Set Gullwing to fly high
                fs.Seek(559984, SeekOrigin.Begin);
                fs.WriteByte(0x0A);
                fs.WriteByte(0x00);
                fs.WriteByte(0x00);
                fs.WriteByte(0x14);
            }
        }
    }
}
