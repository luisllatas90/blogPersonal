<%@ Page Language="VB" AutoEventWireup="false" CodeFile="registrogeneral.aspx.vb" Inherits="academico_formacionacademicadocente_registrogeneral" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>Registro de Formación Académica Docente</title>
    <link href="../../scripts/css/bootstrap.css" rel="stylesheet" type="text/css" />
    <script src="../../scripts/js/jquery-1.12.3.min.js" type="text/javascript"></script>
    <script src="../../scripts/js/bootstrap.min.js" type="text/javascript"></script>        
    <script type="text/javascript">
        function ShowMessage(message, messagetype) {
            var cssclass;
            switch (messagetype) {
                case 'Success':
                    cssclass = 'alert-success'
                    break;
                case 'Errors':
                    cssclass = 'alert-danger'
                    break;
                case '1':
                    cssclass = 'alert-danger'
                    break;
                case 'Warning':
                    cssclass = 'alert-warning'
                    break;
                default:
                    cssclass = 'alert-info'
            }

            if (cssclass != 'alert-danger') {
                $('#alert_container').append('<div id="alert_div" style="margin: 0 0.5%; -webkit-box-shadow: 3px 4px 6px #999;" class="alert alert-success"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><span>' + message + '</span></div>');
            } else {
                $('#alert_container').append('<div id="alert_div" style="margin: 0 0.5%; -webkit-box-shadow: 3px 4px 6px #999;" class="alert alert-danger"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><span>' + message + '</span></div>');
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server" class="form form-horizontal">
    
<div class="container-fluid">
    <div class="panel panel-default">
        <div class="panel-heading"><b>Formación Académica Docente</b></div>
            <div class="panel-body">
    
      <div class="container-fluid">
       <!-- <div class="panel panel-default">-->
        <!-- <div class="panel-heading">Búsqueda de Docentes</div>-->
        <!-- <div class="panel-body">-->
         <div class="form-group">
         
         <div class="row col-md-12">
            <div class="col-md-5">Departamento</div>
            <div class="col-md-2">Estado</div>
            <div class="col-md-5">Docente</div>
         </div>
         <div class="row col-md-12">
            <div class="col-md-5"><asp:DropDownList ID="ddlDpto" runat="server" AutoPostBack="True"></asp:DropDownList> </div>
            <div class="col-md-2"><asp:DropDownList ID="ddlEstado" runat="server" AutoPostBack="True"> <asp:ListItem Selected="True" Value="1">ACTIVO</asp:ListItem><asp:ListItem Value="0">INACTIVO</asp:ListItem></asp:DropDownList></div>                                
            <div class="col-md-5"><asp:DropDownList ID="ddlDocente" runat="server" AutoPostBack="True"></asp:DropDownList> </div>   
         </div> 
         </div> 
         <!-- </div>-->
         <!-- </div>-->
     </div> 
  
     <div class="container-fluid" runat="server" >
        <div class="panel panel-default">
         <div class="panel-heading">Registrar Grado Académico</div>
    <div class="panel-body">
     
     <div class="row col-md-12">
     <div class="form-group">
            <div class="col-md-2">Nivel Programa</div>
            <div class="col-md-10"><asp:DropDownList ID="ddlNivel" runat="server" AutoPostBack="True">
            <asp:ListItem Value="2">Pre Grado</asp:ListItem>
            <asp:ListItem Value="9">Maestría</asp:ListItem>
            <asp:ListItem Value="11">Doctorado</asp:ListItem>
        </asp:DropDownList></div>
     </div>   
     </div>
     
        <div class="row col-md-12">
        <div class="form-group">
                <div class="col-md-2">Programa</div>
                <div class="col-md-10"> <asp:DropDownList ID="ddlPrograma" runat="server" AutoPostBack="True">
        </asp:DropDownList></div>
        </div> </div>  
        
        <div class="row col-md-12">
        <div class="form-group">
                <div class="col-md-2">Especificar Programa</div>
                <div class="col-md-10"> <asp:TextBox ID="txtProgramaExtranjero" 
            runat="server" Enabled="False"  Columns="90"></asp:TextBox></div>
        </div> </div>  
        
        <div class="row col-md-12">
        <div class="form-group">
                <div class="col-md-2">Universidad</div>
                <div class="col-md-10"><asp:DropDownList ID="ddlUniversidad" runat="server" 
            AutoPostBack="True">
        </asp:DropDownList></div>
        </div> </div>  
        
        <div class="row col-md-12">
        <div class="form-group">
            <div class="col-md-2">Especificar Univ.</div>
            <div class="col-md-8"><asp:TextBox ID="txtUniversidadExtranjero" 
            runat="server" Enabled="False" Columns="90"></asp:TextBox></div>
            <div class="col-md-2"><asp:Button ID="btnAgregar" runat="server" Text="Agregar" CssClass="btn btn-success"/></div>
        </div> </div>  
                     
                 <div class="row col-md-12">
        <div class="form-group">
        <div class="col-md-12">
             <asp:GridView ID="gvGrados" runat="server" AutoGenerateColumns="False" DataKeyNames="codigo_gra" CssClass="table table-bordered bs-table datatable">
       
        
        
            <Columns>
                <asp:BoundField DataField="codigo_gra" HeaderText="codigo_gra" 
                    Visible="False" />
                <asp:BoundField DataField="personal" HeaderText="Docente" />
                <asp:BoundField DataField="descripcion_test" HeaderText="Nivel" />
                <asp:BoundField DataField="nombre_pro" HeaderText="Programa" />
                <asp:BoundField DataField="nombreExtPro_gra" 
                    HeaderText="Programa - Extranjero" />
                <asp:BoundField DataField="nombre_uni" HeaderText="Universidad" />
                <asp:BoundField DataField="nombreExtUni_gra" 
                    HeaderText="Universidad - Extranjero" />
                <asp:CommandField ShowDeleteButton="True" ButtonType="Button" ControlStyle-CssClass="btn btn-info" />
                
            </Columns>
         <EmptyDataTemplate>No se encontró información.</EmptyDataTemplate>
         <HeaderStyle BackColor="#D9534F" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Center"
                            Font-Size="11px" />
                        <RowStyle Font-Size="11px" />
                        <EditRowStyle BackColor="#ffffcc" />
                        <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                                                            
                                    <SelectedRowStyle BackColor="#FFFFD2" />
        </asp:GridView></div>
        </div>
        </div>           
     </div>
        </div>
     </div>
    
    

        </div>
    </div>
</div>

</form>
</body>
</html>
