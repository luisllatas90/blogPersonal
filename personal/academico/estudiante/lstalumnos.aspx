<%@ Page Language="VB" AutoEventWireup="false" CodeFile="lstalumnos.aspx.vb" Inherits="academico_estudiante_lstalumnos" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../scripts/css/bootstrap.css" rel="stylesheet" type="text/css" />
    <script src="../../scripts/js/jquery-1.12.3.min.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <!--<div class="container">-->                    
            <h3>B&uacute;squeda de estudiantes de Pregrado</h3>
            <div class="row">
                <div class="col-sm-3">
                    Buscar por DNI/Apellidos y Nombres:
                </div>
                <div class="col-sm-5">                    
                    <asp:TextBox ID="txtBuscar" runat="server" Width="100%" ></asp:TextBox>                    
                </div>
                <div class="col-sm-4">
                    <asp:Button ID="btnBuscar" runat="server" Text="Buscar" class="btn btn-primary" Width="100px" Height="30px" />                                   
                </div>
            </div>
            <div class="row">
                <div class="col-sm-11">
                    <asp:GridView ID="gvEstudiante" runat="server" Width="100%" 
                        AutoGenerateColumns="False"
                        CssClass="table table-bordered bs-table" >
                        <Columns>
                            <asp:BoundField DataField="codigo_Alu" HeaderText="ID" />
                            <asp:BoundField DataField="codigoUniver_Alu" HeaderText="Código" />
                            <asp:BoundField DataField="alumno" HeaderText="Apellidos y Nombres" />
                            <asp:BoundField DataField="codigoUniver_Alu" HeaderText="Escuela Profesional" />
                            <asp:BoundField DataField="estadoactual" HeaderText="Estado Actual" />
                            <asp:BoundField DataField="estadodeuda" HeaderText="Tiene Deuda" />
                            <asp:CommandField ShowSelectButton="True" />
                        </Columns>
                    </asp:GridView>   
                </div>
                
            </div>
        <!--</div>-->
    </form>
</body>
</html>
