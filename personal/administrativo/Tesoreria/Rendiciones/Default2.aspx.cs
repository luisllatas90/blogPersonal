using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Excel = Microsoft.Office.Interop.Excel;
public partial class Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        Excel.Application xlAppToExport = new Excel.Application();
        xlAppToExport.Workbooks.Add("");
        Excel.Worksheet xlWorkSheetToExport = default(Excel.Worksheet);
        xlWorkSheetToExport = (Excel.Worksheet)xlAppToExport.Sheets["Sheet1"];

        // ROW ID FROM WHERE THE DATA STARTS SHOWING.
        int iRowCnt = 4;

        // SHOW THE HEADER.
        xlWorkSheetToExport.Cells[1, 1] = "Employee Details";

        Excel.Range range = xlWorkSheetToExport.Cells[1, 1] as Excel.Range;
        range.EntireRow.Font.Name = "Calibri";
        range.EntireRow.Font.Bold = true;
        range.EntireRow.Font.Size = 20;
       
             // MERGE CELLS OF THE HEADER.

        // SHOW COLUMNS ON THE TOP.
        xlWorkSheetToExport.Cells[iRowCnt - 1, 1] = "Employee Name";
        xlWorkSheetToExport.Cells[iRowCnt - 1, 2] = "Mobile No.";
        xlWorkSheetToExport.Cells[iRowCnt - 1, 3] = "PresentAddress";
        xlWorkSheetToExport.Cells[iRowCnt - 1, 4] = "Email Address";

        Excel.Range range1 = xlAppToExport.ActiveCell.Worksheet.Cells[4, 1] as Excel.Range;
        //range1.AutoFormat(ExcelAutoFormat.xlRangeAutoFormatList3);

        // SAVE THE FILE IN A FOLDER.
        xlWorkSheetToExport.SaveAs("D\\EmployeeDetails.xlsx", Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, true, false, XlSaveAsAccessMode.xlNoChange, XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing);

         
        // CLEAR.
        xlAppToExport.Workbooks.Close();
        xlAppToExport.Quit();
        xlAppToExport = null;
        xlWorkSheetToExport = null;
    }
}
