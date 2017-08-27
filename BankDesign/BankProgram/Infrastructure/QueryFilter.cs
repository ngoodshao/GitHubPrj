using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace BankProgram.Infrastructure
{
    public class QueryFilter
    {
        //{"rules":[{"field":"UserName","op":"equal","value":"Leishaohua0","type":"string"},{"field":"UserID","op":"equal","value":"Lei0","type":"string"}],"op":"and"}
        public List<Rules> rules { get; set; }
        public string op { get; set; }

        public string GetWhere()
        {
            string sWhere = " ";
            foreach (Rules rule in rules)
            {
                if (!rule.field.Equals("undefined"))
                {
                    sWhere += rule.Operator;
                }
            }
            return sWhere;
        }
    }

    public class Rules
    {
        public string field { get; set; }
        public string op
        {
            get;
            set;
        }
        public string value { get; set; }
        public string type { get; set; }

        public string Operator
        {
            get
            {
                string oper = "";
                switch (op)
                {
                    //case "and":
                    //    oper = "and {0}";
                    //    break;
                    //case "or":
                    //    oper = "or {0}";
                    //    break;
                    case "equal":
                        oper = field + " = '" + value + "'";
                        break;
                    case "notequal":
                        oper = field + " !=";
                        break;
                    case "startwith":
                        oper = field + " like '" + value + "%'";
                        break;
                    case "endwith":
                        oper = field + " like '%" + value + "'";
                        break;
                    case "like":
                        oper = field + " like '%" + value + "%'";
                        break;
                    case "greater":
                        oper = field + " > '" + value + "'";
                        break;
                    case "greaterorequal":
                        oper = field + " >= '" + value + "'";
                        break;
                    case "less":
                        oper = field + " < '" + value + "'";
                        break;
                    case "lessorequal":
                        oper = field + " <= '" + value + "'";
                        break;
                    case "in":
                        oper = field + " in (" + value + ")";
                        break;
                    case "notin":
                        oper = field + " not in (" + value + ")";
                        break;
                    default:
                        oper = field + "  = '" + value + "'";
                        break;
                }
                return " and " + oper;
            }
        }
    }
}