<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmRestablecerClave.aspx.vb" Inherits="administrativo_frmRestablecerClave" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Restablecer contraseña</title>    
    <link href="../scripts/css/bootstrap.css" rel="stylesheet" type="text/css" />
    <script src="../scripts/js/jquery-1.12.3.min.js" type="text/javascript"></script>    
    <script src="../scripts/js/bootstrap.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        function ShowMessage(message, messagetype) {
            var cssclass;
            switch (messagetype) {
                case '0':
                    cssclass = 'alert-success'
                    break;
                case "1":
                    cssclass = 'alert-danger'
                    break;
                case '3':
                    cssclass = 'alert-warning'
                    break;
                default:
                    cssclass = 'alert-info'
            }            
            if (cssclass == 'alert-success') {
                $('#alert_container').append('<div id="alert_div" style="margin: 0 0.5%; -webkit-box-shadow: 3px 4px 6px #999;" class="alert alert-success"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><span>' + message + '</span></div>');
            } else if (cssclass == 'alert-danger')  {
                $('#alert_container').append('<div id="alert_div" style="margin: 0 0.5%; -webkit-box-shadow: 3px 4px 6px #999;" class="alert alert-danger"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><span>' + message + '</span></div>');
            } else if (cssclass == 'alert-warning') {
                $('#alert_container').append('<div id="alert_div" style="margin: 0 0.5%; -webkit-box-shadow: 3px 4px 6px #999;" class="alert alert-danger"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><span>' + message + '</span></div>');            
            } else {
                $('#alert_container').append('<div id="alert_div" style="margin: 0 0.5%; -webkit-box-shadow: 3px 4px 6px #999;" class="alert alert-info"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><span>' + message + '</span></div>');
            }            
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">    
    <div class="messagealert" id="alert_container"></div><br />
    <div class="container-fluid">       
       <div class="panel panel-default" id="pnlLista" runat="server">
            <div class="panel-heading">
                <b>Restablecer contraseña</b></div>
            <div class="panel-body">
                <div class="form-group">
                    <div class="col-md-4">
                        <asp:Label ID="Label1" runat="server" Text="Label">Cod. Univ./Apellidos y Nombres/DNI:</asp:Label>
                    </div>                    
                    <div class="col-md-5">
                        <asp:TextBox ID="txtBuscar" runat="server" class="form-control" ></asp:TextBox> 
                    </div>
                    <div class="col-md-3">
                        <asp:Button ID="btnBuscar" runat="server" Text="Buscar" class="btn btn-info" />
                    </div>                                            
                </div>                                    
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-12">    
                        <div id="aviso" runat="server">
                            <h4 class="text-danger"><small class="text-danger">Tener en cuenta que la contraseña reestablecida ser&aacute;: 123456. </small>No olvidar informar al usuario.</h4>                        
                        </div>                    
                        <div class="form-group">
                            <div class="table-responsive">                                                        
                                <asp:GridView ID="gvDatos" runat="server" Width="99%" DataKeyNames="codigo_alu"
                                    CssClass="table table-bordered bs-table" AutoGenerateColumns="False" ShowHeader="true">
                                    <Columns>
                                        <asp:BoundField DataField="codigo_alu" HeaderText="ID" />
                                        <asp:BoundField DataField="nombre_Cpf" HeaderText="CARRERA PROFESIONAL" />
                                        <asp:BoundField DataField="codigoUniver_Alu" HeaderText="COD. UNIV." />
                                        <asp:BoundField DataField="alumno" HeaderText="ESTUDIANTE" />
                                        <asp:BoundField DataField="tipoDocIdent_Alu" HeaderText="TIPO DOC." />
                                        <asp:BoundField DataField="nroDocIdent_Alu" HeaderText="Nº. DOC." />
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="OPERACION">
                                            <ItemTemplate>
                                                <asp:Button ID="btnEdit" runat="server" Text="Restablecer" OnClick="btnRestablece_Click"
                                                    CssClass="btn btn-success btn-sm" CommandName="Edit" OnClientClick="return confirm('¿Desea restablecer su contraseña?');"
                                                    CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" />
                                            </ItemTemplate>
                                            <HeaderStyle></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        No se encontraron registros!
                                    </EmptyDataTemplate>
                                    <HeaderStyle BackColor="#D9534F" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Center"
                                        Font-Size="12px" />
                                    <RowStyle Font-Size="11px" />
                                    <EditRowStyle BackColor="#ffffcc" />
                                    <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                                </asp:GridView>
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
