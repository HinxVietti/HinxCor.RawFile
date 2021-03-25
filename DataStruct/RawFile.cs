using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using HinxCor.Exceptions;
using HinxCor.RawFiles.Interface;

namespace HinxCor.RawFiles.Data { 

public partial class RawFile
{
    public static Encoding defaultCoding = Encoding.UTF8;


    public RawFile(string fileName)
    {
        this.m_file = new FileInfo(fileName);
    }

    internal RawFile(Stream stream)
    {
        this.m_stream = stream;
    }

    public string NextString(int bytecount)
    {
        //int size = length /** 2*/; //1char = 2byte;
        var arr = Next(bytecount);
        return defaultCoding.GetString(arr);
    }


}


partial class RawFile
{



    private void prepare()
    {
        if (!prepared)
        {
            if (m_file.Exists)
                m_stream = m_file.OpenRead();
        }
    }

    private void freeFile()
    {
        if (prepared)
        {
            m_stream.Dispose();
            m_stream = null;
        }
    }
}

//methods
partial class RawFile
{

    private bool TryNext(int size, out byte[] buff)
    {
        buff = Next(size);
        return buff != null;
    }

    public byte[] Next(int size)
    {
        if (!prepared)
            prepare();
        if (InUse)
        {
            byte[] buffer = new byte[size];
            m_stream.Read(buffer, 0, size);
            return buffer;
        }
        if (prepared)
            throw new FileCantReadException();
        throw new StreamCantOpenException();
    }

    public byte NextByte()
    {
        if (TryNext(1, out var arr))
            return arr[0];
        throw new Exception("Cant read byte value");
    }

    public bool NextBoolean()
    {
        if (TryNext(1, out var arr))
            return BitConverter.ToBoolean(arr, 0);
        throw new Exception("Cant read bool value");
    }

    public char NextChar()
    {
        if (TryNext(2, out var arr))
            return BitConverter.ToChar(arr, 0);
        throw new Exception("Cant read char value");
    }

    public short NextShort()
    {
        if (TryNext(2, out var arr))
            return BitConverter.ToInt16(arr, 0);
        throw new Exception("Cant read short value");
    }

    public ushort NextUShort()
    {
        if (TryNext(2, out var arr))
            return BitConverter.ToUInt16(arr, 0);
        throw new Exception("Cant read ushort value");
    }

    public int NextInt()
    {
        if (TryNext(4, out var arr))
            return BitConverter.ToInt32(arr, 0);
        throw new Exception("Cant read int32 value");
    }


    public uint NextUint()
    {
        if (TryNext(4, out var arr))
            return BitConverter.ToUInt32(arr, 0);
        throw new Exception("Cant read uint32 value");
    }

    public long NextLong()
    {
        if (TryNext(8, out var arr))
            return BitConverter.ToInt64(arr, 0);
        throw new Exception("Cant read int64 value");
    }

    public ulong NextUlong()
    {
        if (TryNext(8, out var arr))
            return BitConverter.ToUInt64(arr, 0);
        throw new Exception("Cant read uint64 value");
    }

    public float NextSingle()
    {
        if (TryNext(4, out var arr))
            return BitConverter.ToSingle(arr, 0);
        throw new Exception("Cant read single value");
    }

    public double NextDouble()
    {
        if (TryNext(8, out var arr))
            return BitConverter.ToDouble(arr, 0);
        throw new Exception("Cant read double value");
    }

    public void Dispose()
    {
        m_stream.Dispose();
    }
}

//properties
partial class RawFile : IRawFile
{
    public bool InUse => prepared && m_stream.CanRead;


    public string FileName => m_file?.FullName;

    public uint Size => (uint)LSize;

    public ulong LSize => (ulong)m_file.Length;

    private bool prepared => m_stream != null;

    public long Position { get => m_stream.Position; set => m_stream.Position = value; }

    private FileInfo m_file;

    private Stream m_stream;
}


}