using System;
using System.Collections.Generic;
using System.Text;
using WildArmsModel.Model.Areas;
using WildArmsModel.Model.Events;
using WildArmsRandomizer.Management;

namespace WildArmsRandomizer.Features.Options
{
    internal class EventReducerOption : IEventReducerOption
    {
        private IRandomizerAgent Agent { get; }

        private IAreaCollection AreaCollection { get; }

        public EventReducerOption(IRandomizerAgent agent, IAreaCollection areaCollection)
        {
            Agent = agent;
            AreaCollection = areaCollection;
        }

        public void ReduceEvents()
        {
            RemoveWorldMapEvents();
            RemoveAdlehydeFairGuard();
            ReduceRudyIntro();
            ReduceCeciliaIntro();
            ReduceJackIntro();
            AreaCollection.WriteObjects(Agent.GeneralConfiguration.TempFile);
        }

        private void RemoveWorldMapEvents()
        {
            var filgaia = AreaCollection.GetMappedObject(0);

            var photosphereEvent = filgaia.Events[67];
            BlankOutEvent(photosphereEvent);

            var dragonShrineEvent = filgaia.Events[63];
            BlankOutEvent(dragonShrineEvent);
            dragonShrineEvent = filgaia.Events[64];
            BlankOutEvent(dragonShrineEvent);
            dragonShrineEvent = filgaia.Events[65];
            BlankOutEvent(dragonShrineEvent);
            dragonShrineEvent = filgaia.Events[66];
            BlankOutEvent(dragonShrineEvent);

            var elwPyramid1Event = filgaia.Events[30];
            elwPyramid1Event.WriteByte(58, 6); //Gate Generator
            var elwPyramid2Event = filgaia.Events[29];
            elwPyramid2Event.WriteByte(70, 6); //Malduke
            var elwPyramid4Event = filgaia.Events[32];
            elwPyramid4Event.WriteByte(43, 6); //Sweet Candy
            var elwPyramid5Event = filgaia.Events[31];
            elwPyramid5Event.WriteByte(60, 6); //De La Metalica
            var elwPyramid6Event = filgaia.Events[28];
            elwPyramid6Event.WriteByte(67, 6); //The Abysswhatr

            var kaDingelEvent = filgaia.Events[61];
            kaDingelEvent.WriteByte(0xFF, 0);
            kaDingelEvent.WriteByte(0xFF, 1);
            kaDingelEvent.WriteByte(0xFF, 2);
            kaDingelEvent.WriteByte(0xFF, 3);
            kaDingelEvent.WriteByte(0xFF, 4);
            kaDingelEvent.WriteByte(0xFF, 5);

            var ghostShipEvent = filgaia.Events[42];
            ghostShipEvent.WriteByte(0xFF, 0);
            ghostShipEvent.WriteByte(0xFF, 1);
            ghostShipEvent.WriteByte(0xFF, 2);
            ghostShipEvent.WriteByte(0xFF, 3);
            ghostShipEvent.WriteByte(0xFF, 4);
            ghostShipEvent.WriteByte(0xFF, 5);

            var pleasingGardenEvent = filgaia.Events[41];
            pleasingGardenEvent.WriteByte(0xFF, 0);
            pleasingGardenEvent.WriteByte(0xFF, 1);
            pleasingGardenEvent.WriteByte(0xFF, 2);
            pleasingGardenEvent.WriteByte(0xFF, 3);
            pleasingGardenEvent.WriteByte(0xFF, 4);
            pleasingGardenEvent.WriteByte(0xFF, 5);

            var mazeOfDeathEvent = filgaia.Events[37];
            mazeOfDeathEvent.WriteByte(0xFF, 0);
            mazeOfDeathEvent.WriteByte(0xFF, 1);
            mazeOfDeathEvent.WriteByte(0xFF, 2);
            mazeOfDeathEvent.WriteByte(0xFF, 3);
            mazeOfDeathEvent.WriteByte(0xFF, 4);
            mazeOfDeathEvent.WriteByte(0xFF, 5);

            SetEntrance(0x3CBB, 67, 0x60, 0x05, 0x8E, 0x0D); //Malduke
            SetEntrance(0x3CC9, 70, 0xE0, 0x03, 0xF8, 0x05); //The Abyss
            SetEntrance(0x3CD7, 58, 0xFE, 0x00, 0x2A, 0x0A); //Gate Generator
            SetEntrance(0x3CE5, 60, 0x60, 0x04, 0x28, 0x0D); //De Le Metalica
            SetEntrance(0x3CF3, 43, 0x80, 0x03, 0xA2, 0x03); //Sweet Candy

        }

        private void SetEntrance(int offset, byte areaId, byte b1, byte b2, byte b3, byte b4)
        {
            var filgaia = AreaCollection.GetMappedObject(0);
            filgaia.WriteByte(areaId, offset);
            filgaia.WriteByte(b1, offset + 1);
            filgaia.WriteByte(b2, offset + 2);
            filgaia.WriteByte(b3, offset + 3);
            filgaia.WriteByte(b4, offset + 4);
        }

        private void BlankOutEvent(EventObject ev)
        {
            for (var j = 0; j < 18; j++)
            {
                ev.WriteByte(0, j);
            }
        }

        private void RemoveAdlehydeFairGuard()
        {
            var adlehydes = new List<int> { 31, 32, 33, 34 };

            foreach (var adlehyde in adlehydes)
            {
                var area = AreaCollection.GetMappedObject(adlehyde);
                for (var i = 0x7C27; i < 0x7C67; i++)
                {
                    area.WriteByte(0, i);
                }
            }
        }

        private void ReduceRudyIntro()
        {
            var surfVillage = AreaCollection.GetMappedObject(2);

            for (var i = 0x6706; i < 0x6983; i++)
            {
                surfVillage.WriteByte(0, i);
            }
            for (var i = 0x6A35; i < 0x6B64; i++)
            {
                surfVillage.WriteByte(0, i);
            }

        }

        private void ReduceCeciliaIntro()
        {
            var curanAbbey = AreaCollection.GetMappedObject(3);

            for (var i = 0x7369; i < 0x77E9; i++)
            {
                curanAbbey.WriteByte(0x00, i);
            }
            for (var i = 0x81CF; i < 0x8201; i++)
            {
                curanAbbey.WriteByte(0x00, i);
            }
        }

        private void ReduceJackIntro()
        {
            var memoryTemple = AreaCollection.GetMappedObject(36);

            for (var i = 0x3DEA; i < 0x4552; i++)
            {
                memoryTemple.WriteByte(0, i);
            }
        }
    }
}
