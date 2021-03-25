using System;
using System.Collections.Generic;

namespace HinxCor.RawFiles.Interface
{
    public interface IRawFileWriter : IDisposable
    {
        bool InUse { get; }
        string FileName { get; set; }
        long Position { get; set; }
        uint Size { get; }
        ulong LSize { get; }

        int NextBoolean(bool value);
        int NextByte(byte value);
        int NextChar(char value);
        int NextShort(short value);
        int NextUShort(ushort value);
        int NextInt(int value);
        int NextUint(uint value);
        int NextLong(long value);
        int NextUlong(ulong value);
        int NextSingle(float value);
        int NextDouble(double value);
        int NextString(string value);

        int Next(bool value);
        int Next(byte value);
        int Next(char value);
        int Next(short value);
        int Next(ushort value);
        int Next(int value);
        int Next(uint value);
        int Next(long value);
        int Next(ulong value);
        int Next(float value);
        int Next(double value);
        int Next(string value);
        int Next(byte[] arr);


        void Flush();
    }
}