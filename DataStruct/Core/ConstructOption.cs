using System;
using System.Collections.Generic;
using HinxCor.RawFiles.Data;

namespace HinxCor.RawFiles
{
    //
    public class ConstructOption
    {
        public int priority;
        public int handleMethod;
    }


    public class ConstructorInfo : ConstructOption
    {
        public static Type[][] GetConstructTypes(Interface.IRawFile reader)
        {
            int codepage = reader.NextInt();
            var coding = System.Text.Encoding.GetEncoding(codepage);
            if (coding != null)
                RawFile.defaultCoding = coding;
            int count = reader.NextInt();
            var ts = new Type[count][];
            for (int i = 0; i < ts.Length; i++)
            {
                ts[i] = new Type[reader.NextInt()];
                if (ts[i].Length > 0)
                    for (int k = 0; k < ts[i].Length; k++)
                    {
                        int nl = reader.NextByte();
                        string typeName = reader.NextString(nl);
                        ts[i][k] = Type.GetType(typeName);
                    }
            }
            return ts;
        }

        public static void WriteConstructTypes(Interface.IRawFileWriter writer, object obj)
        {
            if (ReferenceEquals(obj, null))
                return;

            var tss = GetConstructTypes(obj);
            writer.Next(RawFileWriter.defaultCoding.CodePage);
            writer.NextInt(tss.Length);
            for (int i = 0; i < tss.Length; i++)
            {
                var ts = tss[i];
                writer.NextInt(ts.Length);
                for (int k = 0; k < ts.Length; k++)
                {
                    var t = ts[k];
                    string name = t.FullName;
                    writer.NextByte((byte)RawFileWriter.defaultCoding.GetByteCount(name));
                    writer.Next(name);
                }
            }
        }

        public static Type[][] GetConstructTypes(object obj)
        {
            if (obj == null)
                return null;
            Type[][] constructers = null;

            var type = obj.GetType();
            var css = type.GetConstructors();

            constructers = new Type[css.Length][];
            for (int i = 0; i < css.Length; i++)
            {
                var ps = css[i].GetParameters();
                // var ts = css[i].GetGenericArguments();
                var ts = new Type[ps.Length];
                for (int k = 0; k < ps.Length; k++)
                    ts[k] = ps[k].ParameterType;
                constructers[i] = ts;
            }
            return constructers;
        }
    }
}