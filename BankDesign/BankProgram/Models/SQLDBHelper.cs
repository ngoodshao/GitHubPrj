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
        public static M_User_DB CreateUser()
        {
            return SQLDBHelper.CreateDBClass<M_User_DB>();
        }

        public static M_CustomerInfo_DB CreateCus()
        {
            return SQLDBHelper.CreateDBClass<M_CustomerInfo_DB>();
        }
    }

}