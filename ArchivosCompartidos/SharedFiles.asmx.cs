﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Usat.SharedFilesModels.FilesEntity.Shared;
using Usat.SharedFilesModels.FilesEntity.Entity;
using Usat.SharedFiles.BussionesLogic.SahredFilelogicService;
using System.Web.Services.Protocols;
using System.Web.Script.Services;
using System.Web.Services.Description;
using System.Security.Principal;
using System.Collections.Specialized;
using System.IO;
using System.Xml;
using System.Text;
using System.Net;
 
namespace SharedFilesService
{
    /// <summary>
    /// Descripción breve de SharedFiles
    /// </summary>
    [WebService(Namespace = "http://usat.edu.pe", Description = "Servicio Web de Almacenamiento de Archivos en la Nube(Cloud)")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class SharedFiles : System.Web.Services.WebService
    {

        //[SoapDocumentMethod("UploadFileInq")]
        //[ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
        [WebMethod(Description = "Permite almacenar archivos")]
        /*  public ResultMessage UploadFile(string Nombre, string Usuario, string Equipo, string Ip)//, string Fecha, string Extencion,string TransaccionId, int TablaId, string NroOperacion, string Archivo)
          {*/
        // HttpContext request = HttpContext.Current;
        public ResultMessage UploadFile(ParameterFileType ParameterFile)
        {
            ResultMessage result = new ResultMessage { };
            try
            {
                //ParameterFileType param = new ParameterFileType();
                IIdentity winid = HttpContext.Current.User.Identity;
                WindowsIdentity wi = (WindowsIdentity)winid;

                NameValueCollection varServer = HttpContext.Current.Request.ServerVariables;

                ParameterFile.Usuario = varServer["HTTP_QVUSER"];// wi.Name;
                SharedFileMananger shared = new SharedFileMananger();
                  result = shared.CreateFiles(ParameterFile);
                //return new ResultMessage { Status ="",  Sta   tusBody =  new ErrorMessage { Code ="o", Message ="oksks"} };// result;
                return result;
            }
            catch (Exception ex) 
            {

                result = new ResultMessage
                {
                    Status = "ERROR",
                    StatusBody = new ErrorMessage
                    {
                         Code ="501",
                         Message = "Message Exception:.." + ex.Message
                    }
                };
            }
            return result;
        }
        
        //[SoapDocumentMethod("DownloadFileInq")]
        //[ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
        [WebMethod(Description = "Permite descargar archivos", EnableSession = false)]     
        public string DownloadFile(String IdArchivo,string param, string param1, string param2)
        {            
            //IIdentity winid = HttpContext.Current.User.Identity;
            //WindowsIdentity wi = (WindowsIdentity)winid;

            // string EmployeeLogin = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            //string data = HttpContext.Current.Request .ServerVariables["AUTH_USER"].ToString();            
            
            XmlDocument xmlSoapRequest = new XmlDocument();
            NameValueCollection varServer = HttpContext.Current.Request.ServerVariables;
            string[] keys = varServer.AllKeys;
           
            string fff = "";
            for (int i = 0; i < varServer.Count; i++)
            {
                fff = fff + varServer[i]; ;
            }
            SharedFileMananger shared = new SharedFileMananger();
            string path = Server.MapPath("files");

            //string remoteUri = "https://intranet.usat.edu.pe/campusvirtual/librerianet/reglamentos/";
            //param = "USAT\\CSenmache";
            string fileName = shared.PermiteDescargarArchivo(IdArchivo, param, param1);   //varServer["HTTP_QVUSER"]
            //string fileName = "REGLAMENTO_BIBLIOTECA.pdf", myStringWebResource = null;
            WebClient myWebClient = new WebClient();
            //myStringWebResource = remoteUri + fileName;
            //myWebClient.DownloadFile(myStringWebResource, param2);
            if (fileName.Contains("Error") == true)
            {
                return fileName;
            }
            else
            {
                //myWebClient.DownloadFile(fileName, param2);
                return shared.GetArchivo(IdArchivo, param, param1);
            }
            
            //return "Descarga completa - " + fileName;
            //return shared.GetArchivo(IdArchivo, varServer["HTTP_QVUSER"], path, param1);// wi.Name);// +"::: Descargando " + fff + ":::::::: " + gg["HTTP_QVUSER:"] + "  ----  " + gg["HTTP_QVUSER"] + "***** " + gg["HTTP_QVUSER:"] + "===" + gg["HTTP_QVUSER"] + keys[0];
            //return shared.GetArchivo(IdArchivo, "USAT\\esaavedra", path, param1);// wi.Name);// +"::: Descargando " + fff + ":::::::: " + gg["HTTP_QVUSER:"] + "  ----  " + gg["HTTP_QVUSER"] + "***** " + gg["HTTP_QVUSER:"] + "===" + gg["HTTP_QVUSER"] + keys[0];
        }

        [WebMethod(Description = "Descargar Boleta de pago de planillas")]   
        public ResultMessage GenerateTicket(string param1,string param2,string param3,string param4,string param5,int param6, string param7,string param8) {
      // public ResultMessage GenerateTicket(string CodigoPer,string CodigoPlla,string TipoPlla,string Mes,string Periodo,int TablaId, string Fecha) {
         //   string CodigoPer,string CodigoPlla,string TipoPlla,string Mes,string Periodo,int TablaId, string Fecha
              //param8:valida la autenticacion activa del usuario
        PlanillasManager shared = new PlanillasManager(param1, param2, param3, param4, param5, param6, param7);
         //   PlanillasManager shared = new PlanillasManager(CodigoPer, CodigoPlla, TipoPlla, Mes, Periodo, TablaId, Fecha);
           // shared.GenerateTikect();
        ResultMessage result = new ResultMessage { };
        try
        {
            IIdentity winid = HttpContext.Current.User.Identity;
         string Logo=   HttpContext.Current.Server.MapPath("~/Images/logo.png");
         string BarTitle = HttpContext.Current.Server.MapPath("~/Images/bar.png");
         string signature = HttpContext.Current.Server.MapPath("~/Images/signature.txt");
            NameValueCollection varServer = HttpContext.Current.Request.ServerVariables;
          //  byte[] Files = File.ReadAllBytes(firma);
           // string text = System.IO.File.ReadAllText(@"C:\Users\Public\TestFolder\WriteText.txt");
           //string Base64String = Convert.ToBase64String(Files, 0, Files.Length);
            // ParameterFile.Usuario = varServer["HTTP_QVUSER"];// wi.Name;

            WindowsIdentity wi = (WindowsIdentity)winid;

                //         result=   shared.GenerateTikect(varServer["HTTP_QVUSER"]);

                result = shared.GenerateTikect(param8, Logo, BarTitle, signature);


            }
        catch (Exception ex)
        {

            result = new ResultMessage
            {
                Status = "ERROR",
                StatusBody = new ErrorMessage
                {
                    Code = "501",
                    Message = "Message Exception:.." + ex.Message
                }
            };
        }
            
          //  param8 = wi.Name;
        return result; // new ResultMessage { Status = "", StatusBody = new ErrorMessage { Code = "o", Message = "oksks" } };
        }

        [WebMethod(Description = "Descargar Boleta de pago de planillas")]
        public ResultMessage GenerateCTS(string Semestre, string Periodo, string ImpTc, string CodigoPer, string Fecha,int TablaId)
        {
            PlanillasManager shared = new PlanillasManager(Semestre, Periodo, ImpTc, CodigoPer, Fecha, TablaId);           
            IIdentity winid = HttpContext.Current.User.Identity;
            WindowsIdentity wi = (WindowsIdentity)winid;
            return shared.GenerateCTS(wi.Name);
         // return   new ResultMessage { Status = "", StatusBody = new ErrorMessage { Code = "o", Message = "oksks" } };
        }
        [WebMethod(Description = "Valida si existe la fotografia del Alumno")]
        public bool ExistsStudentPicture(string image)
        {
            WebRequest webRequest = WebRequest.Create("https://intranet.usat.edu.pe/campusvirtual/librerianet/egresado/fotos/"+image);
            webRequest.Method = "HEAD";
            webRequest.Timeout = 1000;

            try
            {
                var response = webRequest.GetResponse();
                /* response is `200 OK` */
                response.Close();
            }
            catch
            {
                /* Any other response */
                return false;
            }

            return true;
        }

        [WebMethod(Description = "Descargar Boleta de pago de planillas")]
        public string Demo() {
            string path= Server.MapPath("files");
            return "";
        }
        
    }

}
