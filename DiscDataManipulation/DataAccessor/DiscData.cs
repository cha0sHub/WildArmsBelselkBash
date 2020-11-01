using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DiscDataManipulation.DataAccessor
{
    /// <summary>
    /// Class used for data mining with the Analyzer function, and for getting indexes that are in variable offsets.
    /// Don't pay too much mind to this class, it isn't actually used in the randomizer
    /// </summary>
    public class DiscData
    {
        public byte[] DataBytes { get; private set; }

        public void LoadData(string discImagePath)
        {
            DataBytes = File.ReadAllBytes(discImagePath);
        }

        public void LoadPartialData(string discImagePath, long startIndex, int dataSize)
        {
            DataBytes = new byte[dataSize];
            using (var fs = File.OpenRead(discImagePath))
            {
                fs.Seek(startIndex, SeekOrigin.Begin);
                fs.Read(DataBytes, 0, dataSize);
            }
        }

        public void LoadData(byte[] rawData)
        {
            DataBytes = rawData;
        }

        public void ClearData()
        {
            DataBytes = null;
        }
        public List<int> FindTwoByteValue(byte a, byte b)
        {
            List<int> indices = new List<int>();
            var i = 0;
            for (i = 1; i < DataBytes.Length; i++)
            {
                if (DataBytes[i - 1] == b && DataBytes[i] == a)
                {
                    indices.Add(i - 1);
                }
            }
            return indices;
        }
        public List<int> FindOneByteValue(byte a)
        {
            List<int> indices = new List<int>();
            for (int i = 1; i < DataBytes.Length; i++)
            {
                if (DataBytes[i] == a)
                {
                    indices.Add(i);
                }
            }
            return indices;
        }
        public List<int> SearchVicinityForOneByte(int index, int range, byte a)
        {
            List<int> indices = new List<int>();
            //search backward
            for (int i = index; i > (Math.Max(0, index - range)); i--)
            {
                if (i == DataBytes.Length)
                {
                    continue;
                }
                if (DataBytes[i] == a)
                {
                    indices.Add(i);
                }
            }
            //search forward
            for (int i = index; i < (Math.Min(DataBytes.Length, index + range)); i++)
            {
                if (i == DataBytes.Length)
                {
                    continue;
                }
                if (DataBytes[i] == a)
                {
                    indices.Add(i);
                }
            }
            return indices;
        }
        public List<int> SearchVicinityForTwoBytes(int index, int range, byte a, byte b)
        {
            List<int> indices = new List<int>();

            //search backward
            for (int i = index; i > (Math.Max(0, index - range)); i--)
            {
                if (i == DataBytes.Length)
                {
                    continue;
                }
                if (DataBytes[i] == b && DataBytes[i + 1] == a)
                {
                    indices.Add(i);
                }
            }
            //search forward
            for (int i = index; i < (Math.Min(DataBytes.Length, index + range)); i++)
            {
                if (i == DataBytes.Length)
                {
                    continue;
                }
                if (DataBytes[i - 1] == b && DataBytes[i] == a)
                {
                    indices.Add(i);
                }
            }
            return indices;
        }

        public List<int> FindByteSequence(List<byte> list)
        {
            List<int> indices = new List<int>();
            byte firstByte = list[0];
            for (int i = 1; i < DataBytes.Length; i++)
            {
                if (DataBytes[i] == firstByte)
                {
                    bool sequenceFound = true;
                    for (int j = 1; j < list.Count; j++)
                    {
                        if (i + j >= DataBytes.Length)
                        {
                            sequenceFound = false;
                            break;
                        }
                        if (DataBytes[i + j] != list[j])
                        {
                            sequenceFound = false;
                            break;
                        }

                    }
                    if (sequenceFound)
                        indices.Add(i);
                }
            }
            return indices;
        }

        public void PrintFromIndex(int index, int range)
        {
            using (StreamWriter sw = File.CreateText("output.txt"))
            {
                var l = $"Printing Data Back from index: {index}";
                sw.WriteLine(l);
                Console.WriteLine(l);
                bool even = true;
                for (int i = index; i > (Math.Max(0, index - range)); i--)
                {
                    if (i < 0)
                        continue;
                    l = $"[{i}]({even}): {BitConverter.ToInt16(DataBytes, i)}";
                    sw.WriteLine(l);
                    Console.WriteLine(l);
                    even = !even;
                }
                l = $"Printing Data Back for index: {index}";
                sw.WriteLine(l);
                Console.WriteLine(l);
                even = true;
                for (int i = index; i < (Math.Min(DataBytes.Length, index + range)); i++)
                {
                    if (i >= DataBytes.Length)
                        continue;
                    l = $"[{i}]({even}): {BitConverter.ToInt16(DataBytes, i)}";
                    sw.WriteLine(l);
                    Console.WriteLine(l);
                    even = !even;
                }
            }
        }
        public void ExportDataToCsv(int startIndex, int blockSize, int numOfBlocks, string itemType)
        {
            var dataExtracted = new byte[numOfBlocks][];
            using (StreamWriter sw = File.CreateText(itemType + "By2.csv"))
            {
                for (int i = 0; i < numOfBlocks; i++)
                {
                    string line = "";
                    for (int j = 0; j < blockSize; j += 2)
                    {
                        line += BitConverter.ToUInt16(DataBytes, startIndex + (i * blockSize) + j) + ",";
                    }
                    sw.WriteLine(line);
                }
            }
            using (StreamWriter sw = File.CreateText(itemType + "By1.csv"))
            {
                for (int i = 0; i < numOfBlocks; i++)
                {
                    string line = "";
                    for (int j = 0; j < blockSize; j++)
                    {
                        line += DataBytes[startIndex + (i * blockSize) + j] + ",";
                    }
                    sw.WriteLine(line);
                }
            }
        }

        public byte[][] ExtractBytesToFile(int startIndex, int blockSize, int numOfBlocks, string itemType, string fileName)
        {
            if (!Directory.Exists(itemType))
            {
                Directory.CreateDirectory(itemType);
            }
            var dataExtracted = new byte[numOfBlocks][];
            for (int i = 0; i < numOfBlocks; i++)
            {

                using (BinaryWriter bw = new BinaryWriter(File.Open(itemType + "/" + fileName + ".bin", FileMode.Create)))
                {
                    for (int j = 0; j < blockSize; j++)
                    {
                        bw.Write(DataBytes[startIndex + (i * blockSize) + j]);
                    }
                }
            }
            return dataExtracted;
        }


        public byte[][] ExtractByteDataBySizeToFile(int startIndex, int blockSize, int numOfBlocks, string itemType)
        {
            if (!Directory.Exists(itemType))
            {
                Directory.CreateDirectory(itemType);
            }
            var dataExtracted = new byte[numOfBlocks][];
            for (int i = 0; i < numOfBlocks; i++)
            {

                using (StreamWriter sw = File.CreateText(itemType + "/" + i + ".txt"))
                {

                    sw.WriteLine("Writing out data as two byte values:");
                    sw.WriteLine("========================================");
                    for (int j = 0; j < blockSize; j += 2)
                    {
                        var bytes = BitConverter.ToString(DataBytes, startIndex + (i * blockSize) + j, 2);
                        var line = $"[{j}]({bytes}): {BitConverter.ToUInt16(DataBytes, startIndex + (i * blockSize) + j)}";
                        sw.WriteLine(line);
                    }

                    sw.WriteLine("Writing out data as two byte values, staggered by one byte in case of a flag causing misalignment:");
                    sw.WriteLine("========================================");
                    for (int j = 1; j < blockSize; j += 2)
                    {
                        var bytes = BitConverter.ToString(DataBytes, startIndex + (i * blockSize) + j, 2);
                        var line = $"[{j}]({bytes}): {BitConverter.ToUInt16(DataBytes, startIndex + (i * blockSize) + j)}";
                        sw.WriteLine(line);
                    }/**/

                    sw.WriteLine("Writing out data as one byte values:");
                    sw.WriteLine("========================================");
                    dataExtracted[i] = new byte[blockSize];
                    for (int j = 0; j < blockSize; j++)
                    {

                        dataExtracted[i][j] = DataBytes[startIndex + (i * blockSize) + j];
                        var bytes = BitConverter.ToString(DataBytes, startIndex + (i * blockSize) + j, 1);
                        var line = $"[{j}]({bytes}): {DataBytes[startIndex + (i * blockSize) + j]}";
                        sw.WriteLine(line);
                    }
                }
            }
            return dataExtracted;
        }
        public byte[][] ExtractByteDataBySize(int startIndex, int blockSize, int numOfBlocks, string itemType)
        {
            var dataExtracted = new byte[numOfBlocks][];
            List<int> indexes1 = new List<int>();
            List<int> indexes2 = new List<int>();
            List<int> indexes3 = new List<int>();
            for (int i = 0; i < numOfBlocks; i++)
            {
                dataExtracted[i] = new byte[blockSize];
                for (int j = 0; j < blockSize; j++)
                {
                    dataExtracted[i][j] = DataBytes[startIndex + (i * blockSize) + j];
                    if (dataExtracted[i][j] == 209 && i == 239)
                    {
                        indexes3.Add(j);
                    }
                    if (dataExtracted[i][j] == 210 && i == 240)
                    {
                        indexes1.Add(j);
                    }
                }
            }
            var result = indexes1.Intersect(indexes3);
            foreach (int index in result)
            {
                Console.WriteLine(index);
            }
            return dataExtracted;
        }
        public string ExtractString(int startIndex, int endIndex)
        {
            var list = ExtractStrings(startIndex, endIndex, '\0');
            if (list.Count == 0)
                return string.Empty;
            return ExtractStrings(startIndex, endIndex, '\0')[0];
        }
        public List<string> ExtractStrings(int startIndex, int endIndex, char delimiter)
        {
            var extractedStrings = new List<string>();
            string inProgress = string.Empty;
            for (int i = startIndex; i <= endIndex; i++)
            {
                var character = Convert.ToChar(DataBytes[i]);
                if (character == delimiter)
                {
                    if (!string.IsNullOrEmpty(inProgress))
                    {
                        extractedStrings.Add(inProgress);
                        inProgress = string.Empty;
                    }
                }
                else
                {
                    inProgress += character;
                }
            }
            if (!string.IsNullOrEmpty(inProgress))
            {
                extractedStrings.Add(inProgress);
            }
            return extractedStrings;
        }
        public void WriteStringsToFile(string filename, List<string> lines)
        {
            using (var sw = File.CreateText(filename))
            {
                foreach (var line in lines)
                {
                    sw.WriteLine(line);
                }
            }
        }
        public List<string> ExtractAttackNames()
        {
            /*
                        function getAttackName( id )
	            local str = "";
	
	            if( id == 0 ) then
		            return "NONE"
	            end
	
	            if( id == 255 ) then
		            return "Physical attack"
	            end
	
	            local base_addr			=	0x0001BB48;
	            local name_addr_lo 	= memory.readword(base_addr + id * 4);
	            local name_addr_hi 	= bit.band(memory.readword(base_addr + id * 4 + 2), 0x000000FF);
	            local name_addr 			= bit.lshift(name_addr_hi, 16) + name_addr_lo + 1;
	            local cur_char 			= memory.readbyte(name_addr);
	
	            while cur_char ~= 0 do
		            str = str .. string.char(cur_char);
		            name_addr = name_addr + 1;
		            cur_char = memory.readbyte(name_addr);
	            end
	
	            return str
            end
            */
            var attackNames = new List<string>();
            using (StreamWriter sw = File.CreateText("enemyAttacks2.txt"))
            {

                for (int i = 0; i < 214; i++)
                {
                    try
                    {
                        int startIndex = BitConverter.ToUInt16(DataBytes, 0x72F28 + (i * 4));
                        int endIndex = BitConverter.ToUInt16(DataBytes, 0x72F28 + (i * 4) + 2) & 0x000000FF;
                        endIndex = endIndex << 16;
                        var nameAddr = endIndex + startIndex + 1 + 357344;
                        string attack = ExtractString(nameAddr, nameAddr + 255);
                        attackNames.Add(attack);
                        sw.WriteLine($"{i} - {attack}");
                    }
                    catch (Exception) { }
                }
            }
            return attackNames;
        }
    }

}
