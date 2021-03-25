using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HinxCor.RawFiles.Data
{

    public partial class RawFileWriter : Interface.IRawFileWriter
    {
        public static Encoding defaultCoding = System.Text.Encoding.UTF8;

        public void Flush()
        {
            m_stream.Flush();
        }

    }


    partial class RawFileWriter
    {
        Stream m_stream;

        public void Dispose()
        {
            m_stream.Dispose();
        }

        private bool prepared => m_stream != null;

        internal static RawFileWriter CreateStream(Stream stream)
        {
            return new RawFileWriter
            {
                m_stream = stream
            };
        }

        public static RawFileWriter CreateFile(string fileName)
        {
            return new RawFileWriter()
            {
                m_stream = new FileStream(fileName, FileMode.OpenOrCreate)
            };
        }
    }

    partial class RawFileWriter
    {
        public bool InUse => prepared;

        public string FileName { get; set; }

        public long Position { get => m_stream.Position; set => m_stream.Position = value; }

        public uint Size => (uint)LSize;

        public ulong LSize => (ulong)m_stream.Length;



        public int Next(bool value) => NextBoolean(value);

        public int Next(byte value) => NextByte(value);

        public int Next(char value) => NextChar(value);

        public int Next(short value) => NextShort(value);

        public int Next(ushort value) => NextUShort(value);

        public int Next(int value) => NextInt(value);

        public int Next(uint value) => NextUint(value);

        public int Next(long value) => NextLong(value);

        public int Next(ulong value) => NextUlong(value);

        public int Next(float value) => NextSingle(value);

        public int Next(double value) => NextDouble(value);

        public int Next(byte[] arr)
        {
            m_stream.Write(arr, 0, arr.Length);
            return (int)LSize;
        }

        public int NextBoolean(bool value) => Next(BitConverter.GetBytes(value));

        public int NextByte(byte value)
        {
            m_stream.WriteByte(value);
            return (int)LSize;
        }

        public int NextString(string value)
        {
            var dat = defaultCoding.GetBytes(value);
            return Next(dat);
        }

        public int Next(string value) => NextString(value);

        public int NextChar(char value) => Next(BitConverter.GetBytes(value));

        public int NextDouble(double value) => Next(BitConverter.GetBytes(value));

        public int NextInt(int value) => Next(BitConverter.GetBytes(value));

        public int NextLong(long value) => Next(BitConverter.GetBytes(value));

        public int NextShort(short value) => Next(BitConverter.GetBytes(value));

        public int NextSingle(float value) => Next(BitConverter.GetBytes(value));

        public int NextUint(uint value) => Next(BitConverter.GetBytes(value));

        public int NextUlong(ulong value) => Next(BitConverter.GetBytes(value));

        public int NextUShort(ushort value) => Next(BitConverter.GetBytes(value));

    }
}