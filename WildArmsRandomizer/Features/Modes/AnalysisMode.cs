using System;
using System.Collections.Generic;
using System.Linq;
using WildArmsModel.EventParsing;
using WildArmsModel.EventParsing.Templates;
using WildArmsModel.Model.Areas;
using WildArmsModel.Model.Arms;
using WildArmsModel.Model.Attacks;
using WildArmsModel.Model.Enemies;
using WildArmsModel.Model.Events;
using WildArmsModel.Model.FastDraws;
using WildArmsModel.Model.Items;
using WildArmsModel.Model.Spells;
using WildArmsModel.Model.Summons;
using WildArmsRandomizer.Management;

namespace WildArmsRandomizer.Features.Modes
{
    internal class AnalysisMode : IAnalysisMode
    {
        private IAttackCollection AttackCollection { get; }
        private IArmCollection ArmCollection { get; }
        private IFastDrawCollection FastDrawCollection { get; }
        private ISpellCollection SpellCollection { get; }
        private ISummonCollection SummonCollection { get; }
        private IEnemyCollection EnemyCollection { get; }
        private IItemCollection ItemCollection { get; }
        private IAreaCollection AreaCollection { get; }
        private IEventParser EventParser { get; }
        private IRandomizerAgent Agent { get; }

        public AnalysisMode(IAttackCollection attackCollection,
                            IArmCollection armCollection,
                            IFastDrawCollection fastDrawCollection,
                            ISpellCollection spellCollection,
                            ISummonCollection summonCollection,
                            IEnemyCollection enemyCollection,
                            IItemCollection itemCollection,
                            IAreaCollection areaCollection,
                            IRandomizerAgent agent,
                            IEventParser eventParser)
        {
            AttackCollection = attackCollection;
            ArmCollection = armCollection;
            FastDrawCollection = fastDrawCollection;
            SpellCollection = spellCollection;
            SummonCollection = summonCollection;
            EnemyCollection = enemyCollection;
            ItemCollection = itemCollection;
            AreaCollection = areaCollection;
            Agent = agent;
            EventParser = eventParser;
        }

        public void RunAnalysis()
        {
            var mountainPassArea = AreaCollection.Areas[38];
            var lighterEvent = mountainPassArea.Events[35];
            var guardEvent = mountainPassArea.Events[56];
            var rockEvent = mountainPassArea.Events[38];

            //mountainPassArea.WriteByte(0x00, 0x1FEB);
            //mountainPassArea.WriteByte(0x00, 0x1FF1); // 0x1FF1

            var lighterIdOccurences = new List<int>();
            var guardIdOccurences = new List<int>();

            for (var i = 0; i < AreaObject.ObjectSize; i++)
            {
                var value = mountainPassArea.ReadByte(i);
                if (value == 36)
                {
                    lighterIdOccurences.Add(i);
                }
                if (value == 57)
                {
                    guardIdOccurences.Add(i); //2256
                }
            }
            //9CC - 1FEA, 2509 - 2779, 2609-2779
            var guardEventValue = mountainPassArea.ReadUnsignedShort(2654); //4127 0x101F 1FEB (0xFCC)
            var lightEventValue = mountainPassArea.ReadUnsignedShort(2612); //3610 1DE6 (diff 517 0x205)
            var rockEventValue = mountainPassArea.ReadUnsignedShort(2618); //65035

            //EventParser.PrintEvents(mountainPassArea, "mountainPassAreaEvents.txt");

            //event types/offsets start at 2540
            /*
            var count = 0;
            for (var i = 2540; i < 2540 + 144; i += 2)
            {
                var value = mountainPassArea.ReadUnsignedShort(i);
                Console.WriteLine($"{count} - {value}");
                count++;
            }
            foreach (var eventPosition in mountainPassArea.EventPositions)
            {
                Console.WriteLine($"{eventPosition.Id} - {eventPosition.Offset}");
            }*/
            
            mountainPassArea.WriteByte(0x00, 0x1FE3);
            /*mountainPassArea.WriteByte(0x00, 0x1FE4);
            mountainPassArea.WriteByte(0x00, 0x1FE5);
            mountainPassArea.WriteByte(0x00, 0x1FE6);
            mountainPassArea.WriteByte(0x00, 0x1FE7);
            mountainPassArea.WriteByte(0x00, 0x1FE8);
            mountainPassArea.WriteByte(0x00, 0x1FE9);
            mountainPassArea.WriteByte(0x00, 0x1FEA);
            mountainPassArea.WriteByte(0x00, 0x1FEB);
            mountainPassArea.WriteByte(0x00, 0x1FEC);
            mountainPassArea.WriteByte(0x00, 0x1FED);
            mountainPassArea.WriteByte(0x00, 0x1FEE);
            mountainPassArea.WriteByte(0x00, 0x1FEF);

            var offset = 0x1FE4 + 0;
            if (false)
            {
                mountainPassArea.WriteByte(0x0D, offset);
                mountainPassArea.WriteByte(0x06, offset + 1);
                mountainPassArea.WriteByte(0x01, offset + 2);
            }*/
            
            
            var eventActionBytes = new List<List<byte>>();
            var lighterEventDetails = mountainPassArea.EventDetails.First(d => d.Id == 36);
            /*
            var position = 0;
            for (var i = 8; i < lighterEventDetails.Size; i++)
            {
                //lighterEventDetails.WriteByte(lighterEventDetails.ReadByte(i), position);
                lighterEventDetails.WriteByte(0x00, i);
                position++;
            }
            //lighterEventDetails.WriteByte(0x00, position);
            Console.WriteLine(string.Format("{0:X2}", lighterEventDetails.ReadByte(0)));*/

            
            var currentPosition = 0;
            while (currentPosition < lighterEventDetails.Size)
            {
                var actionBytes = new List<byte>();
                var actionId = lighterEventDetails.ReadByte(currentPosition);
                actionBytes.Add(actionId);
                currentPosition++;
                ActionTemplate actionTemplate = null;
                if (TemplateDictionary.Templates.ContainsKey(actionId))
                {
                    actionTemplate = TemplateDictionary.Templates[actionId];
                }
                else
                {
                    actionTemplate = new ActionTemplate(actionId, "Unknown Action", 0);
                }
                if (actionTemplate.StringStart || actionTemplate.NullTerminated)
                {
                    var actionString = "";
                    while (true)
                    {
                        var characterByte = lighterEventDetails.ReadByte(currentPosition);
                        actionBytes.Add(characterByte);
                        if (characterByte == 0 && lighterEventDetails.ReadByte(currentPosition + 1) != 0)
                            break;
                        actionString += (char)characterByte;
                        currentPosition++;
                    }
                    currentPosition++;
                }
                
                else
                {
                    for (var i = currentPosition; i < currentPosition + actionTemplate.ArgumentByteSize; i++)
                    {
                        actionBytes.Add(lighterEventDetails.ReadByte(i));
                    }
                    currentPosition += actionTemplate.ArgumentByteSize;
                }
                eventActionBytes.Add(actionBytes);
            }

            //61 - memory change to obtain lighter
            //84 - memory change to set lighter flag
            var position = 0;
            var skipTo = 61;
            var stopAt = 83; //106  

            lighterEventDetails.WriteByte(0x24, 0);
            /*
            position = WriteAction(lighterEventDetails, eventActionBytes[61], position);
            for (var i = 62; i < 66; i++)
            {
                position = WriteAction(lighterEventDetails, eventActionBytes[i], position);
            }
            position = WriteAction(lighterEventDetails, eventActionBytes[66], position);
            for (var i = 67; i < 71; i++)
            {
                position = WriteAction(lighterEventDetails, eventActionBytes[i], position);
            }
            position = WriteAction(lighterEventDetails, eventActionBytes[71], position);
            position = WriteAction(lighterEventDetails, eventActionBytes[72], position);
            for (var i = 73; i < 75; i++)
            {
                position = WriteAction(lighterEventDetails, eventActionBytes[i], position);
            }
            position = WriteAction(lighterEventDetails, eventActionBytes[76], position);
            position = WriteAction(lighterEventDetails, eventActionBytes[79], position);
            position = WriteAction(lighterEventDetails, eventActionBytes[83], position);
            position = WriteAction(lighterEventDetails, eventActionBytes[84], position);
            for (var i = position; i < lighterEventDetails.Size; i++)
            {
                lighterEventDetails.WriteByte(0x00, i);
            }
            /*
            for (var i = skipTo; i < stopAt; i++)
            {
                foreach (var b in eventActionBytes[i])
                {
                    lighterEventDetails.WriteByte(b, position);
                    position++;
                }
            }
            for (var i = stopAt; i < eventActionBytes.Count; i++)
            {
                foreach (var b in eventActionBytes[i])
                {
                    lighterEventDetails.WriteByte(0x00, position);
                    position++;
                }
            }
            lighterEventDetails.WriteByte(0x00, position);
            foreach (var b in eventActionBytes[skipTo])
            {
                Console.WriteLine(string.Format("{0:X2}", b));
            }*/

            /*
            position = 0;
            for (var i = 8; i < lighterEventDetails.Size; i++)
            {
                //lighterEventDetails.WriteByte(lighterEventDetails.ReadByte(i), position);
                lighterEventDetails.WriteByte(0x00, i);
                position++;
            }
            lighterEventDetails.WriteByte(0x00, position);
            Console.WriteLine(string.Format("{0:X2}", lighterEventDetails.ReadByte(0)));*/

            /*
            mountainPassArea.WriteByte(0x00, 2612);
            mountainPassArea.WriteByte(0x00, 2613);

            mountainPassArea.WriteByte(0x00, 2618);
            mountainPassArea.WriteByte(0x00, 2619);

            mountainPassArea.WriteByte(0x00, 2654);
            mountainPassArea.WriteByte(0x00, 2655);*/

            //events 42 - 46
            //8040 - 8045
            //starting: 4036
            //location: 8042, offset: 4006
            //lighter offset: 3610
            //guard offset: 4127
            //mountainPassArea.WriteByte(0x06, 8042);
            AreaCollection.WriteObjects(Agent.GeneralConfiguration.TempFile);
        }

        private int WriteAction(EventDetailObject eventDetails, List<byte> actionData, int position)
        {
            foreach (var b in actionData)
            {
                eventDetails.WriteByte(b, position);
                position++;
            }
            return position;
        }

        private void BlankOutEvent(EventObject ev)
        {
            for (var j = 0; j < 18; j++)
            {
                ev.WriteByte(0, j);
            }
        }
    }
}
