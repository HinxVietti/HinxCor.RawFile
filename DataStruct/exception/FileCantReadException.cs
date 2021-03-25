using System;
using System.Collections.Generic;

namespace HinxCor.Exceptions
{
    public class FileCantReadException : Exception
    {
        public override string Message => "file cant read.";
    }
}
