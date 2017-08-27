﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;

namespace PMS.Client.Infrastructure
{
   /// <summary>
   /// JSON序列化和反序列化辅助类
   /// </summary>
   public class JsonHelper
   {
       /// <summary>
       /// JSON序列化
       /// </summary>
       public static string JsonSerializer<T>(T t)
       {
           DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
           MemoryStream ms = new MemoryStream();
           ser.WriteObject(ms, t);
           string jsonString = Encoding.UTF8.GetString(ms.ToArray());
           ms.Close();
           //替换Json的Date字符串
           string p = @"\\/Date\((\d+)\+\d+\)\\/";
           MatchEvaluator matchEvaluator = new MatchEvaluator(ConvertJsonDateToDateString);
            Regex reg = new Regex(p);
           jsonString = reg.Replace(jsonString, matchEvaluator);
           return jsonString;
       }
    
       /// <summary>
       /// JSON反序列化
       /// </summary>
       public static T JsonDeserialize<T>(string jsonString)
       {
           //将"yyyy-MM-dd HH:mm:ss"格式的字符串转为"\/Date(1294499956278+0800)\/"格式
           string p = @"\d{4}-\d{2}-\d{2}\s\d{2}:\d{2}:\d{2}";
           MatchEvaluator matchEvaluator = new MatchEvaluator(ConvertDateStringToJsonDate);
           Regex reg = new Regex(p);
           jsonString = reg.Replace(jsonString, matchEvaluator);
           DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
           MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
           T obj = (T)ser.ReadObject(ms);
           return obj;
       }
    
       /// <summary>
       /// 将Json序列化的时间由/Date(1294499956278+0800)转为字符串
       /// </summary>
       private static string ConvertJsonDateToDateString(Match m)
       {
           string result = string.Empty;
           DateTime dt = new DateTime(1970,1,1);
           dt = dt.AddMilliseconds(long.Parse(m.Groups[1].Value));
           dt = dt.ToLocalTime();
           result = dt.ToString("yyyy-MM-dd HH:mm:ss");
           return result;
       }
    
       /// <summary>
       /// 将时间字符串转为Json时间
       /// </summary>
       private static string ConvertDateStringToJsonDate(Match m)
       {
           string result = string.Empty;
           DateTime dt = DateTime.Parse(m.Groups[0].Value);
           dt = dt.ToUniversalTime();
           TimeSpan ts = dt - DateTime.Parse("1970-01-01");
           result = string.Format("\\/Date({0}+0800)\\/",ts.TotalMilliseconds);
           return result;
       }

       /// <summary>
       /// 将datatable转换为json  
       /// </summary>
       /// <param name="dtb">Dt</param>
       /// <returns>JSON字符串</returns>
       public static string DataTable2Json(DataTable dtb)
       {
           JavaScriptSerializer jss = new JavaScriptSerializer();
           System.Collections.ArrayList dic = new System.Collections.ArrayList();
           foreach (DataRow dr in dtb.Rows)
           {
               System.Collections.Generic.Dictionary<string, object> drow = new System.Collections.Generic.Dictionary<string, object>();
               foreach (DataColumn dc in dtb.Columns)
               {
                   drow.Add(dc.ColumnName, dr[dc.ColumnName]);
               }
               dic.Add(drow);

           }
           //序列化  
           return jss.Serialize(dic);
       }

   }
}