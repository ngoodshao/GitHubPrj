using LSH.EF.CodeFirst.DLL;
using LSH.EF.CodeFirst.DLL.DBSet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankProgram.Models
{
    public class SQLDBHelperClient
    {
        public static M_User_DB Create()
        {
            return SQLDBHelper.CreateDBClass<M_User_DB>();
        }
    }
}