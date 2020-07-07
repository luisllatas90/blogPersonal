using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Dundas.Olap.Data;
using Dundas.Olap.Manager;
using Dundas.Olap.WebUIControls;
using System.IO;

public partial class Matriculados : System.Web.UI.Page    
{

    #region Properties

    /// <summary>
    /// Gets or sets the type of the active axis builder.
    /// </summary>
    /// <value>The type of the active axis builder.</value>
    private DataAxisType ActiveAxisBuilderType
    {
        get
        {
            return (DataAxisType)Enum.Parse(typeof(DataAxisType), this.HiddenFieldAxisType.Value);
        }
        set
        {
            this.HiddenFieldAxisType.Value = value.ToString();
        }
    }

    /// <summary>
    /// Gets or sets the type of the active axis builder.
    /// </summary>
    /// <value>The type of the active axis builder.</value>
    private ChartGridViewState ActiveChartGridView
    {
        get
        {
            return (ChartGridViewState)Enum.Parse(typeof(ChartGridViewState), this.HiddenFieldChartGridState.Value);
        }
        set
        {
            this.HiddenFieldChartGridState.Value = value.ToString();   
        }
    }

    #endregion //Properties

    #region Methods

    /// <summary>
    /// Updates the axis builders visibility.
    /// </summary>
    private void UpdateAxisBuildersVisibility()
    {
        DataAxisType axisType = this.ActiveAxisBuilderType;

        this.AxisBuilderCategorical.Visible = (axisType == DataAxisType.Categorical);
        this.PanelTabCategories.CssClass = (axisType == DataAxisType.Categorical) ? "ppbtn_selected" : "ppbtn_normal";
        this.PanelTabCategories.Attributes["onclick"] = (axisType == DataAxisType.Categorical) ? "" : 
        this.OlapManager1.GetCallbackEventReference(this.OlapManager1, "axistab/Categorical", String.Empty);

        this.AxisBuilderSeries.Visible = (axisType == DataAxisType.Series);
        this.PanelTabSeries.CssClass = (axisType == DataAxisType.Series) ? "ppbtn_selected" : "ppbtn_normal";
        this.PanelTabSeries.Attributes["onclick"] = (axisType == DataAxisType.Series) ? "" : 
        this.OlapManager1.GetCallbackEventReference(this.OlapManager1, "axistab/Series", String.Empty);

        
        this.AxisBuilderSlicer.Visible = (axisType == DataAxisType.Slicer);
        this.PanelTabSlicer.CssClass = (axisType == DataAxisType.Slicer) ? "ppbtn_selected" : "ppbtn_normal";
        this.PanelTabSlicer.Attributes["onclick"] = (axisType == DataAxisType.Slicer) ? "" : 
        this.OlapManager1.GetCallbackEventReference(this.OlapManager1, "axistab/Slicer", String.Empty);
        
        this.ButtonAddDimension.Attributes["onclick"] = this.OlapManager1.GetCallbackEventReference(this.OlapManager1, "add_dimension", String.Empty);
    }

    /// <summary>
    /// Updates the chart and grid visibility.
    /// </summary>
    private void UpdateChartGridVisibility()
    {
        
        this.OlapChart1.Visible = 
        this.OlapGrid1.Visible = false;

        this.PanelTabChart.CssClass = 
        this.PanelTabGrid.CssClass = 
        this.PanelTabBoth.CssClass = "ppbtn_normal";

        switch (this.ActiveChartGridView)
        {
            case ChartGridViewState.Chart:
                this.OlapChart1.Visible = true;
                this.OlapChart1.Width = 900;
                this.PanelTabChart.CssClass = "ppbtn_selected";
                this.PanelTabChart.Attributes["onclick"] = "";
                this.PanelTabGrid.Attributes["onclick"] = this.OlapManager1.GetCallbackEventReference(this.OlapManager1, "viewtab/Grid", String.Empty);
                this.PanelTabBoth.Attributes["onclick"] = this.OlapManager1.GetCallbackEventReference(this.OlapManager1, "viewtab/Both", String.Empty);
                break;
            
            case ChartGridViewState.Grid:
                this.OlapGrid1.Visible = true;
                this.OlapGrid1.Width = System.Web.UI.WebControls.Unit.Percentage(100);
                this.PanelTabGrid.CssClass = "ppbtn_selected";
                this.PanelTabChart.Attributes["onclick"] = this.OlapManager1.GetCallbackEventReference(this.OlapManager1, "viewtab/Chart", String.Empty);
                this.PanelTabGrid.Attributes["onclick"] = "";
                this.PanelTabBoth.Attributes["onclick"] = this.OlapManager1.GetCallbackEventReference(this.OlapManager1, "viewtab/Both", String.Empty);
                break;
            
            case ChartGridViewState.Both:
                this.OlapChart1.Visible = true;
                this.OlapGrid1.Visible = true;
                this.OlapChart1.Width = 900;
                this.OlapGrid1.Width = System.Web.UI.WebControls.Unit.Percentage(100);
                this.PanelTabBoth.CssClass = "ppbtn_selected";
                this.PanelTabChart.Attributes["onclick"] = this.OlapManager1.GetCallbackEventReference(this.OlapManager1, "viewtab/Chart", String.Empty);
                this.PanelTabGrid.Attributes["onclick"] = this.OlapManager1.GetCallbackEventReference(this.OlapManager1, "viewtab/Grid", String.Empty);
                this.PanelTabBoth.Attributes["onclick"] = "";
                break;
        }
    }

    #endregion //Methods

    #region Page Overriden Methods

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        // Put user code to initialize the page here
        if (!this.IsPostBack)
        {
           this.AdomdNetDataProvider1.ConnectionString = "Data Source=.;Provider=msolap;Initial Catalog=SIG_USAT;";
           this.OlapManager1.LoadReports(this.Server.MapPath("MatriculadosCargar.xml"));
           this.OlapChart1.PreRender += new EventHandler(OlapChart1_PreRender);
           this.OlapChart1.Legends[0].Alignment = System.Drawing.StringAlignment.Center;
           this.OlapChart1.Legends[0].Docking = LegendDocking.Bottom;
        }
        this.UpdateAxisBuildersVisibility();
        this.UpdateChartGridVisibility();
        this.OlapChart1.Series[0].ShowLabelAsValue = true;
    }

    void OlapChart1_PreRender(object sender, EventArgs e)
    {
        this.OlapChart1.Titles[0].Font = new System.Drawing.Font("Trebuchet MS", 14);
        this.OlapChart1.Titles[0].Color = System.Drawing.Color.FromArgb(180, 26, 59, 105);
        for (int i = 0; i <= OlapChart1.Series.Count - 1; i++)
        { this.OlapChart1.Series[i].ShowLabelAsValue = true; }
    }
    
    #endregion //Page Overriden Methods

    #region Control Events Handlers



    /// <summary>
    /// Handles the Command event of the OlapManager1 control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Web.UI.WebControls.CommandEventArgs"/> instance containing the event data.</param>
    protected void OlapManager1_Command(object sender, CommandEventArgs e)
    {
        if (e.CommandName.StartsWith("axistab/"))
        {
            this.ActiveAxisBuilderType = (DataAxisType)Enum.Parse(typeof(DataAxisType), e.CommandName.Replace("axistab/", ""));
            
            this.UpdateAxisBuildersVisibility();
            // Update axes panel
            this.OlapManager1.UpdateClientControl(this.PanelAxes);
        }
        if (e.CommandName.StartsWith("viewtab/"))
        {
            this.ActiveChartGridView = (ChartGridViewState)Enum.Parse(typeof(ChartGridViewState), e.CommandName.Replace("viewtab/", ""));

            this.UpdateChartGridVisibility();
            
            // update toolbar buttons;
            this.OlapToolbar1.Invalidate();
            // Update chart grid view panel
            this.OlapManager1.UpdateClientControl(this.PanelChartGrid);
        }
        else if (e.CommandName == "add_dimension")
        {
            this.OlapManager1.ExecuteAddDimensionDialog(-1, this.ActiveAxisBuilderType);
        }
    }


    /// <summary>
    /// Handles the Init event of the OlapChart1 control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void OlapChart1_Init(object sender, EventArgs e)
    {
        OlapToolbarCommand separator = new OlapToolbarSeparator();
        //separator.ItemPriority = 1;

        OlapToolbarCommand exportExcelCommand = 
                new OlapToolbarCommand("Export2Excel", "../../images/excel.gif", "Exportar a Excel", String.Empty);
        //exportExcelCommand.ItemPriority = 1;
        exportExcelCommand.RequiresPostback = true;
        /*
        OlapToolbarCommand columnChartCommand   = 
                new OlapToolbarCommand("ColumnChart", "../../images/ColumnChartType.gif", "Grafico de Columnas", String.Empty);
        //columnChartCommand.ItemPriority = 1;

        OlapToolbarCommand pieChartCommand      = 
                new OlapToolbarCommand("PieChart", "../../images/PieChartType.gif", "Grafico de Pie", String.Empty);
        //pieChartCommand.ItemPriority = 1;

        OlapToolbarCommand doughnutChartCommand = 
                new OlapToolbarCommand("DoughnutChart", "../../images/DoughnutChartType.gif", "Grafico de Dona", String.Empty);
        //doughnutChartCommand.ItemPriority = 1;
        */

        this.OlapChart1.ToolbarSettings.Commands.Add(exportExcelCommand);
        this.OlapGrid1.ToolbarSettings.Commands.Add(exportExcelCommand);
        //this.OlapChart1.ToolbarSettings.Commands.Add(columnChartCommand);
        //this.OlapChart1.ToolbarSettings.Commands.Add(pieChartCommand);
        //this.OlapChart1.ToolbarSettings.Commands.Add(doughnutChartCommand);
        
        this.OlapChart1.ToolbarSettings.Commands.Add(separator);

    }

    /// <summary>
    /// Handles the Click event of the OlapToolbar1 control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="Dundas.Olap.WebUIControls.OlapToolbar.ToolbarClickEventArgs"/> instance containing the event data.</param>
    protected void OlapToolbar1_Click(object sender, OlapToolbar.ToolbarClickEventArgs e)
    {
        if (e.CommandName == "Export2Excel")
        {
            // ensure that chart is populated.
            this.OlapChart1.OlapDataBind();

            this.Response.Clear();
            this.Response.AppendHeader("content-disposition", "attachment; filename=IngresantesXSemestreXEscuela.xls");
            this.Response.ContentType = "application/vnd.ms-excel";

            if (OlapChart1.Visible == true)
            {
                
                this.Response.Write(this.GetXmlSpreadsheetData(this.OlapChart1));
                Response.End();
            }
            else if (OlapGrid1.Visible == true)
            {
                using(MemoryStream stream = new MemoryStream())
                {
                    OlapGrid1.Export(stream);
                    stream.Flush();
                    byte[] result = stream.GetBuffer();
                    this.Response.Write( System.Text.ASCIIEncoding.ASCII.GetChars(result),0,(int)stream.Length);
                    Response.End();
                }
            }

        }
        else if (e.CommandName == "ColumnChart")
        {
            this.OlapChart1.Series[0].ChartType = "Column";
            this.OlapChart1.Invalidate();
        }
        else if (e.CommandName == "PieChart")
        {
            this.OlapChart1.Series[0].ChartType = "Pie";
            this.OlapChart1.Invalidate();
        }
        else if (e.CommandName == "DoughnutChart")
        {
            this.OlapChart1.Series[0].ChartType = "Doughnut";
            this.OlapChart1.Invalidate();
        }
    }

    #endregion //Control Events Handlers

    #region Export To Excel Helper Methods

    /// <summary>
    /// Returns stream with XML Spreadsheet data format.
    /// </summary>
    /// <param name="dataObject">
    /// Data object to add the format to.
    /// </param>
    private String GetXmlSpreadsheetData( OlapChart chart)
    {
        // Create a string builder
        System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();

        // Write XML file header
        stringBuilder.Append("<?xml version=\"1.0\"?>" + Environment.NewLine);
        stringBuilder.Append("<Workbook xmlns=\"urn:schemas-microsoft-com:office:spreadsheet\"" + Environment.NewLine);
        stringBuilder.Append(" xmlns:o=\"urn:schemas-microsoft-com:office:office\"" + Environment.NewLine);
        stringBuilder.Append(" xmlns:x=\"urn:schemas-microsoft-com:office:excel\"" + Environment.NewLine);
        stringBuilder.Append(" xmlns:ss=\"urn:schemas-microsoft-com:office:spreadsheet\"" + Environment.NewLine);
        stringBuilder.Append(" xmlns:html=\"http://www.w3.org/TR/REC-html40\">" + Environment.NewLine);

        stringBuilder.Append(" <Worksheet ss:Name=\"Sheet1\">" + Environment.NewLine);
        stringBuilder.Append("  <Table>" + Environment.NewLine);

        if (chart.Series.Count > 0 &&
            chart.Series[0].Points.Count > 0)
        {
            // Find max row index of custom labels
            int maxRowIndex = 0;
            foreach (CustomLabel customLabel in chart.ChartAreas[0].AxisX.CustomLabels)
            {
                if (customLabel.RowIndex > maxRowIndex)
                {
                    maxRowIndex = customLabel.RowIndex;
                }
            }

            // Create headers for columns using series names
            stringBuilder.Append("   <Row ss:Index=\"1\">" + Environment.NewLine);
            int index = maxRowIndex + 2;
            for (int seriesIndex = 0; seriesIndex < chart.Series.Count; seriesIndex++)
            {
                string cellString = string.Format(
                    "    <Cell ss:Index=\"{0}\"><Data ss:Type=\"String\">{1}</Data></Cell>",
                    index.ToString(System.Globalization.CultureInfo.InvariantCulture),
                    chart.Series[seriesIndex].Name);
                stringBuilder.Append(cellString);
                stringBuilder.Append(Environment.NewLine);
                index++;
            }
            stringBuilder.Append("   </Row>" + Environment.NewLine);

            int ssRowIndex = 2;
            
            // Iterate through all data points
            for (int pointIndex = 0; pointIndex < chart.Series[0].Points.Count; pointIndex++)
            {


                stringBuilder.Append("   <Row ss:Index=\"" + ssRowIndex + "\" >" + Environment.NewLine);

                // Add row labels
                for (int rowIndex = maxRowIndex; rowIndex >= 0; rowIndex--)
                {
                    CustomLabel customLabelWithSameIndex = null;
                    foreach (CustomLabel customLabel in chart.ChartAreas[0].AxisX.CustomLabels)
                    {
                        if (customLabel.RowIndex == rowIndex &&
                            customLabel.Tag is Tuple)
                        {
                            double position = Math.Round(customLabel.From - 0.5);
                            if (position == pointIndex)
                            {
                                customLabelWithSameIndex = customLabel;
                                break;
                            }
                        }
                    }

                    // Check if custom label with same index was found on this row level
                    if (customLabelWithSameIndex != null)
                    {
                        string name = string.Empty;
                        int mergeDown = 0;
                        int mergeAcross = 0;

                        // Try to find number of merged across cells
                        // This numbers depend on the number of 'empty' labels to the right of the current
                        foreach (CustomLabel label in chart.ChartAreas[0].AxisX.CustomLabels)
                        {
                            if (label.RowIndex < rowIndex &&
                                label.Tag is Tuple)
                            {
                                if (label.From >= customLabelWithSameIndex.From &&
                                    label.To <= customLabelWithSameIndex.To)
                                {
                                    mergeAcross = rowIndex - label.RowIndex - 1;
                                }
                            }
                        }

                        // Get tuple last member caption
                        Tuple tuple = (Tuple)customLabelWithSameIndex.Tag;
                        name = tuple.Members[tuple.Members.Count - 1].Caption;
                        mergeDown = (int)Math.Round(customLabelWithSameIndex.To - customLabelWithSameIndex.From - 1.0);

                        // Add cell
                        string cellString = string.Format(
                            "    <Cell ss:Index=\"{0}\" ss:MergeAcross=\"{1}\" ss:MergeDown=\"{2}\"><Data ss:Type=\"String\">{3}</Data></Cell>",
                            maxRowIndex - rowIndex + 1,
                            mergeAcross.ToString(System.Globalization.CultureInfo.InvariantCulture),
                            mergeDown.ToString(System.Globalization.CultureInfo.InvariantCulture),
                            name);
                        stringBuilder.Append(cellString);
                        stringBuilder.Append(Environment.NewLine);

   
                    }
                    
                }

                // Iterate through all series
                for (int seriesIndex = 0; seriesIndex < chart.Series.Count; seriesIndex++)
                {
                    if (!chart.Series[seriesIndex].Points[pointIndex].Empty)
                    {
                        string cellString = string.Format(
                            "    <Cell ss:Index=\"{0}\"><Data ss:Type=\"Number\">{1}</Data></Cell>",
                            maxRowIndex + seriesIndex + 2,
                            chart.Series[seriesIndex].Points[pointIndex].YValues[0].ToString(System.Globalization.CultureInfo.InvariantCulture));
                        stringBuilder.Append(cellString);
                        stringBuilder.Append(Environment.NewLine);
                    }
                }

                stringBuilder.Append("   </Row>" + Environment.NewLine);
                
                ssRowIndex++;
            }
        }

        // Close all XML tags
        stringBuilder.Append("  </Table>" + Environment.NewLine);
        stringBuilder.Append(" </Worksheet>" + Environment.NewLine);
        stringBuilder.Append("</Workbook>" + Environment.NewLine);


        return stringBuilder.ToString();
    }


    #endregion //Export To Excel Helper Methods

    #region ChartGridViewState enumeration
    
    private enum ChartGridViewState
    {
        Chart,
        Grid,
        Both
    }
    
    #endregion //ChartGridViewState enumeration
}
