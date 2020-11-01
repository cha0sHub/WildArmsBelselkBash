using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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

namespace WildArmsModel.EventParsing
{
    public class EventParser : IEventParser
    {
        private IAttackCollection AttackCollection { get; }
        private IArmCollection ArmCollection { get; }
        private IFastDrawCollection FastDrawCollection { get; }
        private ISpellCollection SpellCollection { get; }
        private ISummonCollection SummonCollection { get; }
        private IEnemyCollection EnemyCollection { get; }
        private IItemCollection ItemCollection { get; }
        private IAreaCollection AreaCollection { get; }

        public EventParser(IAttackCollection attackCollection,
                            IArmCollection armCollection,
                            IFastDrawCollection fastDrawCollection,
                            ISpellCollection spellCollection,
                            ISummonCollection summonCollection,
                            IEnemyCollection enemyCollection,
                            IItemCollection itemCollection,
                            IAreaCollection areaCollection)
        {
            AttackCollection = attackCollection;
            ArmCollection = armCollection;
            FastDrawCollection = fastDrawCollection;
            SpellCollection = spellCollection;
            SummonCollection = summonCollection;
            EnemyCollection = enemyCollection;
            ItemCollection = itemCollection;
            AreaCollection = areaCollection;
        }

        public void PrintEvents(AreaObject area, string outputFile)
        {
            var uniqueUnknowns = new HashSet<byte>();
            var lines = new List<string>();
            ItemCollection.SetItemNames();
            foreach (var ev in area.Events)
            {
                lines.Add("");
                lines.Add("========================");
                lines.Add("========================");
                lines.Add("");
                lines.Add($"Event ID: {ev.Id}");
                lines.Add("--------------------");
                lines.Add($"Index Type Indicators: {ev.TypeIndicator1} {ev.TypeIndicator2} {ev.TypeIndicator3}");
                lines.Add($"Item ID: {ev.ItemId}");
                var item = ItemCollection.Items.FirstOrDefault(i => i.Id == ev.ItemId);
                if (item != null)
                {
                    lines.Add($"Item: {item.Name}");
                }
                if (ev.IsGella)
                {
                    lines.Add($"Gella: {ev.Gella}");
                }

                lines.Add("");
                lines.Add("Raw Index Values (Bytes)");
                lines.Add("------------------------");
                var line = "";
                for (var i = 0; i < 18; i++)
                {
                    line += ev.ReadByte(i) + " ";
                }
                lines.Add(line);

                lines.Add("");
                lines.Add("Raw Index Values (Short)");
                lines.Add("------------------------");
                line = "";
                for (var i = 0; i < 18; i += 2)
                {
                    line += ev.ReadUnsignedShort(i) + " ";
                }
                lines.Add(line);

                lines.Add("");
                lines.Add("Raw Index Values (Short) Offset 1");
                lines.Add("------------------------");
                line = "";
                for (var i = 1; i < 18; i += 2)
                {
                    line += ev.ReadUnsignedShort(i) + " ";
                }
                lines.Add(line);

                var eventPosition = area.EventPositions.FirstOrDefault(p => p.Id == ev.Id);
                if (eventPosition != null)
                {
                    lines.Add("");
                    lines.Add($"Event Position: {eventPosition.Offset}");
                }

                var eventDetails = area.EventDetails.FirstOrDefault(d => d.Id == ev.Id);

                if (eventDetails == null)
                    continue;

                lines.Add("");
                lines.Add($"Event Details (Size: {eventDetails.Size}):");
                lines.Add("---------------");

                var currentPosition = 0;
                while (currentPosition < eventDetails.Size)
                {
                    var actionId = eventDetails.ReadByte(currentPosition);
                    currentPosition++;
                    ActionTemplate actionTemplate = null;
                    if (TemplateDictionary.Templates.ContainsKey(actionId))
                    {
                        actionTemplate = TemplateDictionary.Templates[actionId];
                    }
                    else
                    {
                        actionTemplate = new ActionTemplate(actionId, "Unknown Action", 0);
                        uniqueUnknowns.Add(actionId);
                    }
                    lines.Add("");
                    lines.Add($"Action: {actionTemplate.Id} - {actionTemplate.Action}");
                    lines.Add($"Argument Size: {actionTemplate.ArgumentByteSize}");
                    if (actionTemplate.StringStart)
                    {
                        var actionString = "";
                        while (true)
                        {
                            var characterByte = eventDetails.ReadByte(currentPosition);
                            if (characterByte == 0)
                                break;
                            actionString += (char)characterByte;
                            currentPosition++;
                        }
                        currentPosition++;
                        lines.Add($"Action String: {actionString}");
                    }
                    else
                    {
                        lines.Add("");
                        lines.Add("Raw Action Values (Bytes)");
                        lines.Add("-------------------------");
                        line = "";
                        for (var i = currentPosition; i < currentPosition + actionTemplate.ArgumentByteSize; i++)
                        {
                            line += eventDetails.ReadByte(i) + " ";
                        }
                        lines.Add(line);

                        lines.Add("");
                        lines.Add("Raw Action Values (Short)");
                        lines.Add("-------------------------");
                        line = "";
                        for (var i = currentPosition; i < currentPosition + actionTemplate.ArgumentByteSize; i += 2)
                        {
                            line += eventDetails.ReadUnsignedShort(i) + " ";
                        }
                        lines.Add(line);

                        lines.Add("");
                        lines.Add("Raw Action Values (Short) Offset 1");
                        lines.Add("-------------------------");
                        line = "";
                        for (var i = currentPosition + 1; i < currentPosition + actionTemplate.ArgumentByteSize; i += 2)
                        {
                            line += eventDetails.ReadUnsignedShort(i) + " ";
                        }
                        lines.Add(line);

                        currentPosition += actionTemplate.ArgumentByteSize;
                    }
                }
                var eventDetailsAsString = "";
                for (var i = 0; i < eventDetails.Size; i++)
                {
                    eventDetailsAsString += (char)eventDetails.ReadByte(i);
                }
                lines.Add("");
                lines.Add("Details as String:");
                lines.Add("------------------");
                lines.Add(eventDetailsAsString);
            }

            using (var streamWriter = File.CreateText(outputFile))
            {
                foreach (var line in lines)
                {
                    streamWriter.WriteLine(line);
                }
            }
            var unknowns = uniqueUnknowns.ToList();
            unknowns.Sort();
            foreach (var unknown in unknowns)
            {
                Console.WriteLine(string.Format("{0:X2}",unknown));
            }
        }
    }
}
