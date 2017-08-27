using LSH.EF.CodeFirst.DLL.Model;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using PMS.Client.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;

namespace BankProgram.Models
{
    public class NPOIExcel
    {
        public static string ImportExcelFile(HttpFileCollectionBase files, DataTable dt, int iStartRow, int iStartColumn)
        {
            if(files.Count>0)
            {
                for (int i = 0; i < files.Count; i++)
                {
                    HttpPostedFileBase Postfile = (HttpPostedFileBase)files[i];
                    if (Postfile.FileName.Contains(".xlsx"))
                    {
                        return ImportExcelFileXSSF(Postfile, dt, iStartRow, iStartColumn);
                    }
                    else
                    {
                        return ImportExcelFileHSS(Postfile, dt, iStartRow, iStartColumn);
                    }
                }
            }
            return "";
        }

        /// <summary>
        /// xlsx
        /// </summary>
        /// <param name="Postfile"></param>
        /// <param name="dt"></param>
        /// <param name="iStartRow"></param>
        /// <param name="iStartColumn"></param>
        /// <returns></returns>
        private static string ImportExcelFileXSSF(HttpPostedFileBase Postfile, DataTable dt, int iStartRow, int iStartColumn)
        {
            XSSFWorkbook Xssfworkbook;

            #region//初始化信息
            try
            {
                //.xlsx应该XSSFWorkbook workbook = new XSSFWorkbook(file);
                //而xls应该用 HSSFWorkbook workbook = new HSSFWorkbook(file);
                Stream file = Postfile.InputStream;
                Xssfworkbook = new XSSFWorkbook(file);
                //HSSFWorkbook workbook = new HSSFWorkbook(file);
            }
            catch (Exception e)
            {
                throw e;
            }
            #endregion
            NPOI.SS.UserModel.ISheet sheet = Xssfworkbook.GetSheetAt(0);
            System.Collections.IEnumerator rows = sheet.GetRowEnumerator();
            for (int i = 0; i < iStartRow; i++)
            {
                rows.MoveNext();
            }
            while (rows.MoveNext())
            {
                XSSFRow row = (XSSFRow)rows.Current;
                DataRow dr = dt.NewRow();
                int j = 0;
                for (int i = iStartColumn; i < row.LastCellNum; i++)
                {
                    NPOI.SS.UserModel.ICell cell = row.GetCell(i);
                    if (cell == null)
                    {
                        dr[j] = null;
                    }
                    else
                    {
                        if (dt.Columns[j].DataType.FullName=="System.Int32")
                        {
                            dr[j] = Convert.ToInt32(cell.ToString());
                        }
                        else
                            dr[j] = cell;
                    }
                    j++;
                }
                dt.Rows.Add(dr);
            }
            return JsonHelper.DataTable2Json(dt);
        }


        /// <summary>
        /// xls
        /// </summary>
        /// <param name="Postfile"></param>
        /// <param name="dt"></param>
        /// <param name="iStartRow"></param>
        /// <param name="iStartColumn"></param>
        /// <returns></returns>
        private static string ImportExcelFileHSS(HttpPostedFileBase Postfile, DataTable dt, int iStartRow, int iStartColumn)
        {
            HSSFWorkbook hssfworkbook;

            #region//初始化信息
            try
            {
                //.xlsx应该XSSFWorkbook workbook = new XSSFWorkbook(file);
                //而xls应该用 HSSFWorkbook workbook = new HSSFWorkbook(file);
                Stream file = Postfile.InputStream;
                hssfworkbook = new HSSFWorkbook(file);
                //HSSFWorkbook workbook = new HSSFWorkbook(file);
            }
            catch (Exception e)
            {
                throw e;
            }
            #endregion
            NPOI.SS.UserModel.ISheet sheet = hssfworkbook.GetSheetAt(0);
            System.Collections.IEnumerator rows = sheet.GetRowEnumerator();
            for (int i = 0; i < iStartRow; i++)
            {
                rows.MoveNext();
            }
            while (rows.MoveNext())
            {
                HSSFRow row = (HSSFRow)rows.Current;
                DataRow dr = dt.NewRow();
                int j = 0;
                for (int i = iStartColumn; i < row.LastCellNum; i++)
                {
                    NPOI.SS.UserModel.ICell cell = row.GetCell(i);
                    if (cell == null)
                    {
                        dr[j] = null;
                    }
                    else
                    {
                        dr[j] = cell.ToString();
                    }
                    j++;
                }
                dt.Rows.Add(dr);
            }
            return JsonHelper.DataTable2Json(dt);
        }
        public static DataTable ImportExcelFileXSSF_Org(HttpFileCollectionBase files)
        {
            XSSFWorkbook hssfworkbook;

            #region//初始化信息
            try
            {
                //.xlsx应该XSSFWorkbook workbook = new XSSFWorkbook(file);
                //而xls应该用 HSSFWorkbook workbook = new HSSFWorkbook(file);
                Stream file = files[0].InputStream;
                hssfworkbook = new XSSFWorkbook(file);
                //HSSFWorkbook workbook = new HSSFWorkbook(file);
            }
            catch (Exception e)
            {
                throw e;
            }
            #endregion
            NPOI.SS.UserModel.ISheet sheet = hssfworkbook.GetSheetAt(0);
            System.Collections.IEnumerator rows = sheet.GetRowEnumerator();
            DataTable dt = new DataTable();
            rows.MoveNext();
            XSSFRow row = (XSSFRow)rows.Current;
            for (int j = 0; j < (sheet.GetRow(0).LastCellNum); j++)
            {
                //将第一列作为列表头  
                dt.Columns.Add(row.GetCell(j).ToString());
            }
            while (rows.MoveNext())
            {
                row = (XSSFRow)rows.Current;
                DataRow dr = dt.NewRow();
                for (int i = 0; i < row.LastCellNum; i++)
                {
                    NPOI.SS.UserModel.ICell cell = row.GetCell(i);
                    if (cell == null)
                    {
                        dr[i] = null;
                    }
                    else
                    {
                        dr[i] = cell.ToString();
                    }
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }

        /// <summary>
        /// Render DataTable to NPOI Excel 2003 MemoryStream
        /// NOTE:  Limitation of 65,536 rows suppored by XLS
        /// </summary>
        /// <param name="sourceTable">Source DataTable</param>
        /// <returns>MemoryStream containing NPOI Excel workbook</returns>
        public static Stream RenderDataTableToExcelHSS(DataTable sourceTable)
        {
            HSSFWorkbook workbook = new HSSFWorkbook();
            MemoryStream memoryStream = new MemoryStream();
            // By default NPOI creates "Sheet0" which is inconsistent with Excel using "Sheet1"
            HSSFSheet sheet = (HSSFSheet)workbook.CreateSheet("Sheet1");
            HSSFRow headerRow = (HSSFRow)sheet.CreateRow(0);

            // Header Row
            foreach (DataColumn column in sourceTable.Columns)
                headerRow.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);

            // Detail Rows
            int rowIndex = 1;

            // 建立儲存格樣式。 
            HSSFCellStyle style1 = (HSSFCellStyle)workbook.CreateCellStyle();//workbook.CreateCellStyle();
            style1.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Blue.Index2;
            style1.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.Red.Index;
            style1.FillPattern = NPOI.SS.UserModel.FillPattern.SolidForeground;//HSSFCellStyle.SOLID_FOREGROUND; 

            HSSFFont f = (HSSFFont)workbook.CreateFont();
            f.Color = NPOI.HSSF.Util.HSSFColor.Red.Index;
            f.FontName = "宋体";

            foreach (DataRow row in sourceTable.Rows)
            {
                HSSFRow dataRow = (HSSFRow)sheet.CreateRow(rowIndex);

                foreach (DataColumn column in sourceTable.Columns)
                {
                    //HSSFCell cell1 = dataRow.CreateCell(column.Ordinal).SetCellValue(row[column].ToString());

                    HSSFCell cell = (HSSFCell)dataRow.CreateCell(column.Ordinal);
                    cell.CellStyle = style1;
                    cell.CellStyle.SetFont(f);
                    cell.SetCellValue(row[column].ToString());

                }

                rowIndex++;
            }

            workbook.Write(memoryStream);
            memoryStream.Flush();
            memoryStream.Position = 0;

            return memoryStream;
        }


        /// <summary>
        /// Render DataTable to NPOI Excel 2003 MemoryStream
        /// NOTE:  Limitation of 65,536 rows suppored by XLS
        /// </summary>
        /// <param name="sourceTable">Source DataTable</param>
        /// <returns>MemoryStream containing NPOI Excel workbook</returns>
        public static Stream RenderDataTableToExcelHSS(List<M_User> sourceTable)
        {
            HSSFWorkbook workbook = new HSSFWorkbook();
            MemoryStream memoryStream = new MemoryStream();
            // By default NPOI creates "Sheet0" which is inconsistent with Excel using "Sheet1"
            HSSFSheet sheet = (HSSFSheet)workbook.CreateSheet("Sheet1");
            HSSFRow headerRow = (HSSFRow)sheet.CreateRow(0);

            // Header Row
            //List<MemberInfo> lstM = typeof(M_User).GetMembers().ToList();
            //int ij = 0;
            //foreach (MemberInfo m in lstM)
            //{
            //    if (m.MemberType == MemberTypes.Property)
            //    {
            //        headerRow.CreateCell(ij).SetCellValue(m.Name);
            //        ij++;
            //    }
                    
            //}
            //foreach (DataColumn column in sourceTable.Columns)
            //    headerRow.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);

            headerRow.CreateCell(0, CellType.String).SetCellValue("UserName");
            headerRow.CreateCell(1, CellType.String).SetCellValue("UserID");
            headerRow.CreateCell(2, CellType.String).SetCellValue("UserPwd");
            headerRow.CreateCell(3, CellType.String).SetCellValue("UserPermission");
            headerRow.CreateCell(4, CellType.Numeric).SetCellValue("UserState");
            headerRow.CreateCell(5, CellType.String).SetCellValue("Remark");

            // Detail Rows
            int rowIndex = 1;

            // 建立儲存格樣式。 
            HSSFCellStyle style1 = (HSSFCellStyle)workbook.CreateCellStyle();//workbook.CreateCellStyle();
            style1.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Blue.Index2;
            style1.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.Red.Index;
            style1.FillPattern = NPOI.SS.UserModel.FillPattern.SolidForeground;//HSSFCellStyle.SOLID_FOREGROUND; 

            HSSFFont f = (HSSFFont)workbook.CreateFont();
            f.Color = NPOI.HSSF.Util.HSSFColor.Red.Index;
            f.FontName = "宋体";

            foreach (M_User m in sourceTable)
            {
                HSSFRow dataRow = (HSSFRow)sheet.CreateRow(rowIndex);
                dataRow.CreateCell(0, CellType.String).SetCellValue(m.UserName);
                dataRow.CreateCell(1, CellType.String).SetCellValue(m.UserID);
                dataRow.CreateCell(2, CellType.String).SetCellValue(m.UserPwd);
                dataRow.CreateCell(3, CellType.String).SetCellValue(m.UserPermission);
                dataRow.CreateCell(4, CellType.String).SetCellValue(m.UserState);
                dataRow.CreateCell(5, CellType.String).SetCellValue(m.Remark);
                rowIndex++;
            }
            
            workbook.Write(memoryStream);
            memoryStream.Flush();
            memoryStream.Position = 0;

            return memoryStream;
        }

        /// <summary>
        /// Render DataTable to NPOI Excel 2003 MemoryStream
        /// NOTE:  Limitation of 65,536 rows suppored by XLS
        /// </summary>
        /// <param name="sourceTable">Source DataTable</param>
        /// <returns>MemoryStream containing NPOI Excel workbook</returns>
        public static Stream RenderDataTableToExcelXSSF(DataTable sourceTable)
        {
            XSSFWorkbook xssfworkbook = new XSSFWorkbook();
            MemoryStream memoryStream = new MemoryStream();
            // By default NPOI creates "Sheet0" which is inconsistent with Excel using "Sheet1"
            XSSFSheet sheet = (XSSFSheet)xssfworkbook.CreateSheet("Sheet1");
            XSSFRow headerRow = (XSSFRow)sheet.CreateRow(0);

            // Header Row
            foreach (DataColumn column in sourceTable.Columns)
                headerRow.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);

            // Detail Rows
            int rowIndex = 1;

            // 建立儲存格樣式。 
            XSSFCellStyle style1 = (XSSFCellStyle)xssfworkbook.CreateCellStyle();//workbook.CreateCellStyle();
            style1.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Blue.Index2;
            style1.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.Red.Index;
            style1.FillPattern = NPOI.SS.UserModel.FillPattern.SolidForeground;//HSSFCellStyle.SOLID_FOREGROUND; 

            XSSFFont f = (XSSFFont)xssfworkbook.CreateFont();
            f.Color = NPOI.HSSF.Util.HSSFColor.Red.Index;
            f.FontName = "宋体";

            foreach (DataRow row in sourceTable.Rows)
            {
                XSSFRow dataRow = (XSSFRow)sheet.CreateRow(rowIndex);

                foreach (DataColumn column in sourceTable.Columns)
                {
                    //HSSFCell cell1 = dataRow.CreateCell(column.Ordinal).SetCellValue(row[column].ToString());

                    XSSFCell cell = (XSSFCell)dataRow.CreateCell(column.Ordinal);
                    cell.CellStyle = style1;
                    cell.CellStyle.SetFont(f);
                    cell.SetCellValue(row[column].ToString());

                }

                rowIndex++;
            }

            xssfworkbook.Write(memoryStream);
            memoryStream.Flush();
            memoryStream.Position = 0;
            return memoryStream;
        }

        public static byte[] OutputSearchResult(List<M_User> list)
        {
            HSSFWorkbook hssfWorkbook = new HSSFWorkbook();
            ISheet sheet = hssfWorkbook.CreateSheet("维修报表");
            IRow rowHeader = sheet.CreateRow(0);
            rowHeader.CreateCell(0, CellType.String).SetCellValue("维修编号");
            rowHeader.CreateCell(1, CellType.String).SetCellValue("姓名");
            rowHeader.CreateCell(2, CellType.String).SetCellValue("手机号码");
            rowHeader.CreateCell(3, CellType.String).SetCellValue("省");
            rowHeader.CreateCell(4, CellType.String).SetCellValue("市");
            rowHeader.CreateCell(5, CellType.String).SetCellValue("区");
            rowHeader.CreateCell(6, CellType.String).SetCellValue("详细地址");
            rowHeader.CreateCell(7, CellType.String).SetCellValue("服务类型");
            rowHeader.CreateCell(8, CellType.String).SetCellValue("产品类别");
            rowHeader.CreateCell(9, CellType.String).SetCellValue("品牌");
            rowHeader.CreateCell(10, CellType.String).SetCellValue("规格");
            rowHeader.CreateCell(11, CellType.String).SetCellValue("型号");
            rowHeader.CreateCell(12, CellType.String).SetCellValue("From");
            rowHeader.CreateCell(13, CellType.String).SetCellValue("FromType");
            rowHeader.CreateCell(14, CellType.String).SetCellValue("16位机身码");
            rowHeader.CreateCell(15, CellType.String).SetCellValue("故障描述");
            rowHeader.CreateCell(16, CellType.String).SetCellValue("报修时间");
            rowHeader.CreateCell(17, CellType.String).SetCellValue("是否派单");

            for (int i = 0; i < list.Count; i++)
            {
                //IRow dataRow = sheet.CreateRow(i + 1);
                //dataRow.CreateCell(0, CellType.String).SetCellValue(list[i].UserID);
                //dataRow.CreateCell(1, CellType.String).SetCellValue(list[i].UserName);
                //dataRow.CreateCell(2, CellType.String).SetCellValue(list[i].UserPassword);
                //dataRow.CreateCell(3, CellType.String).SetCellValue(list[i].UserPermission);
                //dataRow.CreateCell(4, CellType.String).SetCellValue(list[i].UserStatus);
            }

            byte[] data = null;
            using (MemoryStream ms = new MemoryStream())
            {
                hssfWorkbook.Write(ms);
                ms.Flush();
                ms.Position = 0;
                data = ms.GetBuffer();
            }

            return data;
        }
    }
}