using System.Collections.Generic;
using System.IO;
using DiscDataManipulation.Model;
using WildArmsModel.Model.Events;
using WildArmsModel.Model.ShoppingLists;

namespace WildArmsModel.Model.Areas
{
    public class AreaObject : DiscMappedObject
    {
        public const int ObjectSize = 0x91000;
        public const int DataLength = 0x7530;

        public IReadOnlyList<EventObject> Events => EventData.MappedObjectReadOnly;
        public IReadOnlyList<ShoppingListObject> ShoppingLists => ShoppingListData.MappedObjectReadOnly;

        public ShoppingListCollection ShoppingListData { get; }
        public EventCollection EventData { get; }

        public AreaObject(byte[] rawData, long discOffset) : base(rawData, discOffset)
        {
            ShoppingListData = new ShoppingListCollection();
            EventData = new EventCollection();
            ParseData();
        }

        public void PrintEvents()
        {
            var lines = new List<string>();
            foreach (var ev in EventData.MappedObjectReadOnly)
            {
                var line = "";
                for (int i = 0; i < 18; i++)
                {
                    line += ev.ReadByte(i) + ",";
                }
                lines.Add(line);
            }
            using (var sw = File.CreateText("eventdata/" + this.ToString() + ".txt"))
            {
                foreach (var line in lines)
                {
                    sw.WriteLine(line);
                }
            }
        }
        public void WriteBin()
        {
            using (var sw = File.Create("areadata/" + this.ToString() + ".bin"))
            {
                var rawData = new byte[ObjectSize];
                for (var i = 0; i < ObjectSize; i++)
                {
                    rawData[i] = ReadByte(i);
                }
                sw.Write(rawData, 0, ObjectSize);
            }
        }

        private void ParseData()
        {
            ExtractEvents();
            ExtractShopLists();
        }

        private void ExtractEvents()
        {
            //sequence of bytes indicates the start of the collection of events.
            var eventDataStart = FindByteSequence(new List<byte>() { 20, 128, 255, 255, 255, 255, 255, 255 });
            if (eventDataStart.Count != 1)
                return;

            //extracts all events from area, which are all 18 bytes long. Complex events are stored afterwards, with the event entries here
            //likely being an index for them. Complex events not currently handled.
            int id = 0;
            int count = 0;
            while (true)
            {
                int parentOffset = eventDataStart[0] + 2 + (18 * count);
                byte[] eventData = new byte[18];
                for (int i = 0; i < 18; i++)
                {
                    eventData[i] = ReadByte(parentOffset + i);
                }

                id = eventData[EventOffsets.Id];
                if (id == 0)
                {
                    count++;
                    continue;
                }
                if (id == count - 1)
                {
                    count++;
                    continue;
                }
                if (id != count)
                    break;
                var nEvent = new EventObject(this, parentOffset);
                EventData.AddMappedObject(nEvent);
                count++;
            }
        }

        private void ExtractShopLists()
        {
            //probably an easier way to identify shopping lists, but this weird method seems to work fine.
            var startIndices = FindTwoByteValue(0x03, 0x16);
            var endIndices = FindTwoByteValue(0x09, 0xFF);
            var endIndices2 = FindTwoByteValue(0x00, 0xFF);
            int id = 0;
            long offset = 0;
            foreach (var index in startIndices)
            {
                if (offset == 0)
                    offset = index;
                int nextIndex = index + 2;
                int count = 0;
                List<byte> shopItems = new List<byte>();
                while (true)
                {
                    shopItems.Add(ReadByte(nextIndex));
                    count++;
                    nextIndex++;
                    if (endIndices.Contains(nextIndex) || endIndices2.Contains(nextIndex))
                        break;
                    if (nextIndex - index > 20)
                    {
                        shopItems.Clear();
                        break;
                    }
                }
                if (shopItems.Count > 0)
                {
                    var shoppingList = new ShoppingListObject(this, index + 2, id, count);
                    ShoppingListData.AddMappedObject(shoppingList);
                }
                id++;
            }
        }
    }
}
