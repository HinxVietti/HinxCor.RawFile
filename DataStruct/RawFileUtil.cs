using System;
using System.Collections.Generic;
using System.IO;
using HinxCor.RawFiles.Data;
using HinxCor.RawFiles.Interface;

namespace HinxCor.RawFiles
{


    public class RawFileUtil
    {
        public static IRawFile GetReaderFromStream(Stream stream)
        {
            return new RawFile(stream);
        }

        public static IRawFile GetReaderFromFile(string fileName)
        {
            return new RawFile(fileName);
        }


        public static IRawFileWriter GenWriterTo(string fileName)
        {
            return RawFileWriter.CreateFile(fileName);
        }


        public static IRawFileWriter GenWriterTo(Stream stream)
        {
            return RawFileWriter.CreateStream(stream);
        }



        public static T Contruct<T>(RawFile reader)
        {
            T target = default(T);

            var type = typeof(T);
            var constructer = type.GetConstructor(new Type[0]);



            return target;
        }

        public static object Contruct(Type type, RawFile reader)
        {
            object target = null;



            return target;
        }

        public static void Contruct(string typeName, RawFile reader)
        {
            var t = Type.GetType(typeName);


        }

    }

}