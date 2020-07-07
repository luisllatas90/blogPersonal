using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Dundas.Olap.Manager;
using Dundas.Olap.Data;
using Dundas.Olap.WebUIControls;
using System.Reflection;
using System.ComponentModel;
using System.IO;
using System.Web.UI;

public partial class gerencialNet_Default: 
         System.Web.UI.Page
{
    
    override protected void OnInit(EventArgs e)
    {
        InitializeComponent();
        base.OnInit(e);
    }

    private void InitializeComponent()
    {

    }

    protected void Page_Load(object sender, EventArgs e)
    {

        OlapReport report = new OlapReport();
        string connectionString = string.Empty;
        connectionString = "Data Source=.;Provider=msolap;Initial Catalog=SIG_USAT;";

       try
        {
           if (!this.IsPostBack )
           {
               IDataProvider dataProvider;
               string assemblyPath = this.Server.MapPath("~/bin/");
               dataProvider = gerencialNet_Default.CreateAdomdNetProvider(assemblyPath);
               gerencialNet_Default.SetDataProviderConnectionString(dataProvider, connectionString);
               dataProvider.Open();

               this.OlapClient1.OlapManager.DataProvider = dataProvider;
               this.OlapClient1.OlapManager.ReloadDataSchema();

               this.OlapClient1.OlapManager.OlapReports.Clear();
               this.OlapClient1.OlapManager.OlapReports.Add(report);
               this.OlapClient1.OlapManager.SetCurrentOlapReport(report);
               this.OlapClient1.OlapManager.ExecuteClientScript(this.OlapClient1.OlapManager.GetCallbackCloseDialogScript());
           }
        }
        catch (Exception exception)
        {
            this.OlapClient1.OlapManager.ExecuteClientScript("alert('" + exception.Message.Replace('\'', '`') + "');");
        }
    }

    internal static IDataProvider CreateAdomdNetProvider(string path)
    {
        // Nombre del Proveedor de la DLL ADOMDNET
        path += "DundasWebOlapDataProviderADOMDNet.dll";

        // Chekenado si los proveedores existen
        if (!File.Exists(path))
        {
            throw (new FileNotFoundException("ADOMD for .NET Data Provider was not found.", path));
        }

        // Crear una instancia
        Assembly assembly = Assembly.LoadFrom(path);
        Assembly adomdClientAssembly = null;
        AssemblyName[] assemblyNames = assembly.GetReferencedAssemblies();
        foreach (AssemblyName assemblyName in assemblyNames)
        {
            // Check if ADOMD for .NET assembly can be loaded
            if (assemblyName.Name == "Microsoft.AnalysisServices.AdomdClient")
            {
                adomdClientAssembly = Assembly.Load(assemblyName);
                break;
            }
        }

        IDataProvider dataProvider = null;
        if (adomdClientAssembly != null)
        {
            dataProvider = assembly.CreateInstance("Dundas.Olap.Data.AdomdNet.AdomdNetDataProvider") as IDataProvider;
        }

        return dataProvider;
    }

    internal static IDataProvider CreateAdomdProvider(string path)
    {
        // Add provider DLL name to the path
        path += "DundasWebOlapDataProviderAdomd.dll";

        // Checks if provider DLL exsist
        if (!File.Exists(path))
        {
            throw (new FileNotFoundException("ADOMD Data Provider was not found.", path));
        }

        // Try to create an instance of the data provider
        Assembly assembly = Assembly.LoadFrom(path);
        IDataProvider dataProvider = assembly.CreateInstance("Dundas.Olap.Data.Adomd.AdomdDataProvider") as IDataProvider;
        return dataProvider;
    }

    internal static void SetDataProviderConnectionString(IDataProvider dataProvider,
        string connectionString)
    {
        if (dataProvider != null)
        {
            PropertyInfo pi = dataProvider.GetType().GetProperty("ConnectionString");

            if (pi != null)
            {
                pi.SetValue(dataProvider, connectionString, null);
            }

        }
    }

    internal static string GetDataProviderConnectionString(IDataProvider dataProvider)
    {
        if (dataProvider != null)
        {
            PropertyInfo pi = dataProvider.GetType().GetProperty("ConnectionString");
            if (pi != null)
            {
                return (string)pi.GetValue(dataProvider, null);
            }
        }
        return string.Empty;
    }



    
  


}
