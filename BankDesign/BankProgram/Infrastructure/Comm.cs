using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace BankProgram.Infrastructure
{
    public class CommClass
    {
        public static DataTable CreateTB(string columnnames)
        {
            DataTable dt = new DataTable();
            //fMachineNO:System.String,PCT:System.Double
            string[] colNames = columnnames.Split(',');
            foreach (string s in colNames)
            {
                string[] str = s.Split(':');
                Type objType = Type.GetType(str[1], true);
                dt.Columns.Add(str[0], objType);
            }
            return dt;
        }
    }
}