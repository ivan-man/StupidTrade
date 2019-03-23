using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Enums
{
    [Flags]
    public enum LockState
    {
        Free,
        Write,
        Read,
        ReadAndWritePending,
        WriteReserved,
    }
}
