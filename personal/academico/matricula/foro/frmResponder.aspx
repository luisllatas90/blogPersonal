<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmResponder.aspx.vb" Inherits="academico_matricula_foro_frmResponder" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Responder Solicitud</title>    
        <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv='X-UA-Compatible' content='IE=7' />
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />
    <link href="../../../scripts/css/bootstrap.css" rel="Stylesheet" type="text/css" />
    <link rel='stylesheet' href='../../../assets/css/material.css'/>
    <link href="../../../assets/css/bootstrap-datepicker3.css" rel="Stylesheet" type="text/css" />
    <link href="../../../assets/fontawesome-5.2/css/all.min.css" rel="stylesheet" type="text/css" />

    <script src="../../../scripts/js/jquery-1.12.3.min.js" type="text/javascript"></script>

    

    <script src="../../../scripts/js/bootstrap.min.js" type="text/javascript"></script>

    <script src="../../../assets/js/bootstrap-datepicker.js" type="text/javascript"></script>

    <script src="../../../assets/fontawesome-5.2/js/all.min.js" type="text/javascript"></script>

    <script type="text/javascript" src='../../../assets/js/jquery.accordion.js'></script>
    <script type="text/javascript" src='../../../assets/js/materialize.js'></script>    
    <style type="text/css">
     body
        { font-family:Trebuchet MS;
          font-size:11px;
          cursor:hand;
          
        }
     .col1
     {
         width:15%;
         }
     .col2
     {
         width:85%;
         }
    </style>
</head>
<body>
    <form id="form1" runat="server"  class="form form-horizontal">
       <div class="container-fluid">
        <div class="messagealert" id="alert_container"></div>
         <asp:Panel CssClass="panel panel-primary" id="panel1"  runat="server" style="padding:0px;">
            <div class="panel panel-heading" >
            <h4>Responder Solicitud </h4>
            </div>
            <div class="panel panel-body"  style="padding:3px;"> 
               <div class="row">
                    <div class="col-md-6">
                        <div class="form-group">
                            <label class="col-md-4" for="lblIncidencia">
                                N. Solicitud</label>
                            <div class="col-md-8">                            
                                <asp:Label ID="lblIncidencia" runat="server" Text="" CssClass="form-control"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-group">
                            <label class="col-md-4" for="lblEscuela">
                                Carrera Profesional</label>
                            <div class="col-md-8">                            
                                <asp:Label ID="lblEscuela" runat="server" Text="" CssClass="form-control"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
               <div class="row">
              <div class="col-md-6">
                        <div class="form-group">
                            <label class="col-md-4" for="lblCodUniv">
                              Cod. Univ.</label>
                            <div class="col-md-8">                            
                              <asp:Label ID="lblCodUniv" runat="server" Text=""  CssClass="form-control"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                     <div class="form-group">
                            <label class="col-md-4" for="lblAlumno">
                               Alumno</label>
                            <div class="col-md-8">                            
                              <asp:Label ID="lblAlumno" runat="server" Text="" CssClass="form-control"></asp:Label>
                            </div>
                        </div>
                    
                    </div>
                    
             </div>
               <div class="row">
              <div class="col-md-6">
                        <div class="form-group">
                            <label class="col-md-4" for="lblAsunto">
                                Asunto</label>
                            <div class="col-md-8">                            
                              <asp:Label ID="lblAsunto" runat="server" Text="" CssClass="form-control"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                     <div class="form-group">
                            <label class="col-md-4" for="lblMensaje">
                              Mensaje </label>
                            <div class="col-md-8">                            
                              <asp:Label ID="lblMensaje" runat="server" Text="" CssClass="form-control"></asp:Label>
                            </div>
                        </div>
                    
                    </div>
                    
             </div>
               <div class="row">
              <div class="col-md-6">
                        <div class="form-group">
                            <label class="col-md-4" for="txtResponder">
                                Responder:</label>
                            <div class="col-md-8">                            
                              <asp:TextBox ID="txtResponder" runat="server" Width="100%" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                     <div class="form-group">
                            <label class="col-md-4" for="FileUpload1">
                              Adjunto: </label>
                            <div class="col-md-8">                            
                              <asp:FileUpload ID="FileUpload1" runat="server" Width="100%" />
                            </div>
                        </div>
                    
                    </div>
                    
             </div>
            </div>
            <div class="panel panel-footer">
            
             
                    <div class="btn-group" role="group" aria-label="Basic example">
                           <asp:Button ID="btnResponder" CssClass="btn btn-success" runat="server" Text="Responder" /> 
                            <input type="button" value="Cerrar" class="btn btn-default" />
                    </div>
            </div>
          </asp:Panel>  
        </div>    

    <asp:HiddenField ID="HdInstancia" runat="server" />
    <asp:HiddenField ID="HdIncidente" runat="server" />
    </form>
</body>
</html>
