using System;
using System.IO;

namespace Utilities
{
    //Port of Abyssonym's Python utility to remove and re-insert sector data
    public static class Uniso
    {
        private const int FullHeaderLength = 0x18;
        private const int HeaderLength = 0x10;
        private const int SubHeaderLength = 0x8;
        private const int DataLength = 0x800;
        private const int ErrorDetectLength = 0x4;
        private const int ErrorCorrectLength = 0x114;
        private const int SectorLength = 0x930;
        public static void RemoveSectorMetadata(string sourceFile, string outputFile)
        {
            // playstation discs are CD-ROM XA mode 2 (form 1 for data)
            // form 2 sectors also exist on many discs, so this is a huge hack,
            //but I am treating every sector as form 1 for now.
            Console.WriteLine("REMOVING SECTOR METADATA");
            using (var source = File.OpenRead(sourceFile))
            {
                using (var output = File.Create(outputFile))
                {
                    var headerBuffer = new byte[FullHeaderLength];
                    var errorDetectBuffer = new byte[ErrorDetectLength];
                    var dataBuffer = new byte[DataLength];
                    while (true)
                    {
                        if (source.Length - source.Position < FullHeaderLength + DataLength)
                            break;
                        source.Read(headerBuffer, 0, FullHeaderLength);
                        source.Read(dataBuffer, 0, DataLength);
                        output.Write(dataBuffer, 0, DataLength);
                        var checkData = source.Read(errorDetectBuffer, 0, ErrorDetectLength);
                        source.Seek(0x114, SeekOrigin.Current);
                    }
                }
            }
        }

        public static void InjectLogicalSectors(string sourceFile, string originalFile, string outputFile, bool debug = false)
        {
            if (File.Exists(outputFile))
                File.Delete(outputFile);
            File.Copy(originalFile, outputFile);
            Console.WriteLine("REINJECTING LOGICAL SECTORS TO ORIGINAL ISO");
            long minPointer = -1;
            long maxPointer = -1;
            var numChangedSectors = 0;
            using (var source = File.OpenRead(sourceFile))
            {
                using (var original = File.OpenRead(originalFile))
                {
                    using (var output = File.OpenWrite(outputFile))
                    {
                        var sourceDataBuffer = new byte[DataLength];
                        var destDataBuffer = new byte[DataLength];
                        var headerBuffer = new byte[HeaderLength];
                        var subHeaderBuffer = new byte[SubHeaderLength];
                        var errorDetectBuffer = new byte[ErrorDetectLength];
                        var errorCorrectBuffer = new byte[ErrorCorrectLength];

                        while (true)
                        {
                            //var pointerSource = source.Position;
                            var pointerDest = original.Position;
                            if (source.Length - source.Position == 0 && original.Length - original.Position == 0)
                                break;

                            source.Read(sourceDataBuffer, 0, DataLength);
                            original.Read(headerBuffer, 0, HeaderLength);
                            original.Read(subHeaderBuffer, 0, SubHeaderLength);
                            original.Read(destDataBuffer, 0, DataLength);
                            original.Read(errorDetectBuffer, 0, ErrorDetectLength);
                            original.Read(errorCorrectBuffer, 0, ErrorCorrectLength);
                            if (CompareByteArray(sourceDataBuffer, destDataBuffer))
                            {
                                continue;
                            }
                            if (subHeaderBuffer[2] == 0x20)
                            {
                                Console.WriteLine("WARNING: ");
                                Console.WriteLine("A form sector was modified. This software does not");
                                Console.WriteLine("accurately read from and write to form 2 sectors, which ");
                                Console.WriteLine("are typically used for audio and video data.");
                            }
                            //cha0s TODO: debug check for edc_cc thing
                            if (minPointer == -1)
                                minPointer = pointerDest;
                            maxPointer = Math.Max(pointerDest + DataLength, maxPointer);
                            numChangedSectors++;
                            // gotta do this dumb file seeking thing for windows
                            output.Seek(pointerDest, SeekOrigin.Begin);
                            output.Write(headerBuffer, 0, HeaderLength);
                            output.Write(subHeaderBuffer, 0, SubHeaderLength);
                            output.Seek(pointerDest + FullHeaderLength, SeekOrigin.Begin);
                            output.Write(sourceDataBuffer, 0, DataLength);
                            output.Seek(pointerDest + FullHeaderLength + DataLength, SeekOrigin.Begin);
                            byte[] edc, ecc;
                            var sectorData = new byte[DataLength + FullHeaderLength];
                            var index = 0;
                            for (var i = 0; i < headerBuffer.Length; i++)
                            {
                                sectorData[index] = headerBuffer[i];
                                index++;
                            }
                            for (var i = 0; i < subHeaderBuffer.Length; i++)
                            {
                                sectorData[index] = subHeaderBuffer[i];
                                index++;
                            }
                            for (var i = 0; i < sourceDataBuffer.Length; i++)
                            {
                                sectorData[index] = sourceDataBuffer[i];
                                index++;
                            }
                            CdRomEcc.GetEdcEcc(sectorData, out edc, out ecc);
                            output.Write(edc, 0, edc.Length);
                            output.Write(ecc, 0, ecc.Length);
                            output.Seek(pointerDest + FullHeaderLength + DataLength + edc.Length + ecc.Length, SeekOrigin.Begin);
                            original.Seek(pointerDest + FullHeaderLength + DataLength + edc.Length + ecc.Length, SeekOrigin.Begin);
                        }
                    }
                }
            }
            if (minPointer != -1 && maxPointer != -1)
            {
                Console.WriteLine($"{numChangedSectors} SECTORS CHANGED IN RANGE {minPointer} - {maxPointer}");
            }
            else
            {
                Console.WriteLine("NO CHANGES MADE TO ISO");
            }
        }
        private static bool CompareByteArray(byte[] a, byte[] b)
        {
            if (a.Length != b.Length)
                return false;
            for (var i = 0; i < a.Length; i++)
                if (a[i] != b[i])
                    return false;
            return true;
        }
    }
}
