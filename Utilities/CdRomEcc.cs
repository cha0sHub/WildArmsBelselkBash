using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public static class CdRomEcc
    {
        private const int L2Raw = 0x800;
        private const int L2P = 43 * 2 * 2;
        private const int L2Q = 26 * 2 * 2;

        public static long Crc32(byte[] data)
        {
            long result = 0;
            foreach (var value in data)
            {
                result = CdRomEccTables.EdcCrcTable[(result ^ value) & 0xFF] ^ (result >> 8);
            }
            return result;
        }

        public static byte[] EncodeL2P(byte[] data)
        {
            var baseIndex = 0;
            var pIndex = 4 + L2Raw + 4 + 8;
            var targetSize = pIndex + L2P;
            var newData = new byte[data.Length + (targetSize - data.Length)];
            for (var i = 0; i < data.Length; i++)
            {
                newData[i] = data[i];
            }
            for (var i = 0; i < 43; i++)
            {
                byte a = 0;
                byte b = 0;
                var index = baseIndex;
                for (var j = 19; j < 43; j++)
                {
                    a ^= (byte)CdRomEccTables.L2Sq[j, newData[index]];
                    b ^= (byte)CdRomEccTables.L2Sq[j, newData[index + 1]];
                    index += (2 * 43);
                }
                newData[pIndex] = (byte)(a >> 8);
                newData[pIndex + (43 * 2)] = (byte)(a & 0xFF);
                newData[pIndex + 1] = (byte)(b >> 8);
                newData[pIndex + (43 * 2) + 1] = (byte)(b & 0xFF);
                baseIndex += 2;
                pIndex += 2;
            }
            return newData;
        }

        public static byte[] EncodeL2Q(byte[] data)
        {
            var baseIndex = 0;
            var qIndex = 4 + L2Raw + 4 + 8 + L2P;
            var modIndex = qIndex;
            var targetSize = qIndex + L2Q;
            var newData = new byte[data.Length + (targetSize - data.Length)];
            for (var i = 0; i < data.Length; i++)
            {
                newData[i] = data[i];
            }
            var counter = 0;
            for (var i = 0; i < 26; i++)
            {
                byte a = 0;
                byte b = 0;
                var index = baseIndex;
                for (var j = 0; j < 43; j++)
                {
                    a ^= (byte)CdRomEccTables.L2Sq[j, newData[index]];
                    b ^= (byte)CdRomEccTables.L2Sq[j, newData[index + 1]];
                    index += (2 * 44);
                    index = index % modIndex;
                }
                newData[qIndex] = (byte)(a >> 8);
                newData[qIndex + (26 * 2)] = (byte)(a & 0xFF);
                newData[qIndex + 1] = (byte)(b >> 8);
                newData[qIndex + (26 * 2) + 1] = (byte)(b & 0xFF);
                baseIndex += (2 * 43);
                qIndex += 2;
                counter += 1;
            }
            return newData;
        }
        public static void GetEdcEcc(byte[] data, out byte[] edc, out byte[] ecc)
        {
            var bytesToHash = new byte[0x808];
            var index = 0;
            for (int i = 0x10; i < 0x818; i++)
            {
                bytesToHash[index] = data[i];
                index++;
            }
            var edcX = Crc32(bytesToHash);
            var newData = new byte[0x930];
            for (int i = 0; i < data.Length; i++)
            {
                newData[i] = data[i];
            }
            for (var i = 0; i < 4; i++)
            {
                newData[0x818 + i] = (byte)(edcX & 0xFF);
            }
            var dataToEncode = new byte[0x820];
            dataToEncode[0] = 0x0;
            dataToEncode[1] = 0x0;
            dataToEncode[2] = 0x0;
            dataToEncode[3] = 0x0;
            index = 4;
            for (var i = 0x10; i < 0x81C; i++)
            {
                dataToEncode[index] = newData[i];
                index++;
            }
            var temp = EncodeL2P(dataToEncode);
            temp = EncodeL2Q(temp);
            index = 0x81C;
            for (var i = temp.Length - 0x114; i < temp.Length; i++)
            {
                newData[index] = temp[i];
                index++;
            }
            edc = new byte[0x4];
            index = 0;
            for (var i = 0x818; i < 0x81C; i++)
            {
                edc[index] = newData[i];
                index++;
            }
            ecc = new byte[0x114];
            index = 0;
            for (var i = 0x81C; i < newData.Length; i++)
            {
                ecc[index] = newData[i];
                index++;
            }
        }
    }
}
