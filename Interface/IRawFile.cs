
using System;
using System.Collections.Generic;

namespace HinxCor.RawFiles.Interface
{


    public interface IRawFile : IDisposable
    {
        bool InUse { get; }
        string FileName { get; }
        long Position { get; set; }
        uint Size { get; }
        ulong LSize { get; }



        bool NextBoolean();
        byte NextByte();
        char NextChar();
        short NextShort();
        ushort NextUShort();
        int NextInt();
        uint NextUint();
        long NextLong();
        ulong NextUlong();
        float NextSingle();
        double NextDouble();

        byte[] Next(int size);

        string NextString(int bytecount);

    }

    /*
        L   T
        1    bool
        1    byte
        2    char
        2    short
        2    ushort
        4    int
        4    uint
        8    long
        8    ulong
        4    single
        8    double
     */

    /*
     byte 1
     short 2
     int 4
     long 8
     ushort = short = 2 
     single 4
     dougle 8
     char 2
     bool 1
         */
}