<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SolicitarActivos.aspx.vb" Inherits="administrativo_m_SolicitarActivos_SolicitarActivos" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>Solicitar Activos</title>
    
    <script src="../../../scripts/js/jquery-1.12.3.min.js" type="text/javascript"></script>    
    <link href="../../../scripts/css/bootstrap.css" rel="Stylesheet" type="text/css"/>    
    <script src="../../../scripts/js/bootstrap.min.js" type="text/javascript"></script>    
    <script src="../../../assets/js/bootstrap-datepicker.js" type="text/javascript"></script>
    
</head>
<body>
 
 <div class="container-fluid">
    <h4>Nueva Solicitud</h4>
    
    <form id="form1" runat="server">
        
        <div class="form-group row">
            <label class="col-sm-2">Artículo</label>
            <div class="col-sm-8">
              <input type="text" class="form-control" id="txtBusquedaArticulo">
            </div>
            <div class="col-sm-2">
                <button type="submit">Buscar</button>
            </div>
        </div> 
    </form>
    
    <asp:GridView ID="gvActivoFijo" runat="server" Width="99%" AutoGenerateColumns="false" ShowHeader="true" 
        DataKeyNames="codigo_af,motivo_af" CssClass="table table-bordered">
        <Columns>
            <asp:BoundField DataField="codigo_af" HeaderText="Descripción"/>
            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Cantidad"> 
                <ItemTemplate>
                    <asp:Button ID="btnDarBaja" runat="server" Text="Cantidad" OnClick="btnDarBaja_Click" CommandName="DarBaja" 
                        CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" CssClass="btn btn-danger btn-sm" 
                        OnClientClick="return confirm('¿Desea solicitar este artículo?');" />
                </ItemTemplate>
            </asp:TemplateField>           
            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Agregar">
                <ItemTemplate>
                    <asp:Button ID="btnDarBaja" runat="server" Text="Agregar" OnClick="btnDarBaja_Click" CommandName="DarBaja" 
                        CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" CssClass="btn btn-danger btn-sm" 
                        OnClientClick="return confirm('¿Desea solicitar este artículo?');" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <EmptyDataTemplate>
            No se encontraron Datos!
        </EmptyDataTemplate>
        <HeaderStyle BackColor="#D9534F" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Center" Font-Size="12px"/>
        <RowStyle Font-Size="11px"/>
        <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered"/>
    </asp:GridView>
 </div>
 
</body>
</html>
