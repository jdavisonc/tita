using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infocorp.TITA.WITLogic
{
    public enum ItemType : int
    {
        ISSUE,
        TASK,
        WORKPACKAGE
    }

    public enum CommandType : int
    {
        ADD,
        MODIFY,
        DELETE
    }
}
