using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;
using OfficeOpenXml;

namespace Apps.Util
{
    public static class Epplus
    {
        public static DataTable ToDataTable(string path)
        {
            var app = new OfficeOpenXml.ExcelPackage();
            app.Load(File.OpenRead(path));
            var worksheet = app.Workbook.Worksheets.First();
            DataTable datatable = new DataTable();
            bool hasHeader = true;
            foreach (var firstRowCell in worksheet.Cells[1, 1, 1, worksheet.Dimension.End.Column])
            {
                datatable.Columns.Add(hasHeader ? firstRowCell.Text.Trim() : string.Format("Column {0}", firstRowCell.Start.Column));
            }
            var startRow = hasHeader ? 2 : 1;
            for (var rowNum = startRow; rowNum <= worksheet.Dimension.End.Row; rowNum++)
            {
                var wsRow = worksheet.Cells[rowNum, 1, rowNum, worksheet.Dimension.End.Column];
                var row = datatable.NewRow();
                foreach (var cell in wsRow)
                {
                    row[cell.Start.Column - 1] = cell.Text;
                }
                datatable.Rows.Add(row);
            }
            app.Dispose();
            return datatable;
        }
    }
}
