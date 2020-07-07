<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmVerCargosAbonos.aspx.vb" Inherits="administrativo_pec_frmVerCargosAbonos" Theme="Acero" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <link href="../../../private/estilo.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <p class="usatTitulo">Cargos y Abonos</p>
        <p><asp:Button ID="cmdCancelar" runat="server" Text="Cerrar" 
            SkinID="BotonSalir" ValidationGroup="Salir" 
            onclientclick="self.parent.tb_remove();" UseSubmitBehavior="False" /></p>
        <asp:GridView ID="gvResultado" runat="server" AutoGenerateColumns="False" 
            SkinID="skinGridViewLineas">
            <Columns>
                <asp:BoundField DataField="Servicio" HeaderText="Servicio" ReadOnly="True" 
                    SortExpression="Servicio" />
                <asp:BoundField DataField="codigo_Deu" HeaderText="Codigo Deuda" 
                    ReadOnly="True" SortExpression="codigo_Deu" >
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Documento" HeaderText="Documento" ReadOnly="True" 
                    SortExpression="Documento" >
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="Fecha_Operacion" HeaderText="Fecha Operacion" 
                    ReadOnly="True" SortExpression="Fecha_Operacion" >
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Cargos" HeaderText="Cargos" ReadOnly="True" 
                    SortExpression="Cargos" >
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="Abonos" HeaderText="Abonos" ReadOnly="True" 
                    SortExpression="Abonos" >
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="Saldo" HeaderText="Saldo" ReadOnly="True" 
                    SortExpression="Saldo" >
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="Trans" HeaderText="Transf." ReadOnly="True" 
                    SortExpression="Trans" >
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="observacion_Deu" HeaderText="Observacion" 
                    ReadOnly="True" SortExpression="observacion_Deu" >
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="Genera Mora" SortExpression="GeneraMora">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" 
                            Text='<%# iif(eval("generamora")=1,"Sí","No") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:BoundField DataField="FechaVenc" HeaderText="Fecha Venc." ReadOnly="True" 
                    SortExpression="FechaVenc" >
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="descripcion_Cco" HeaderText="Centro de Costos" 
                    ReadOnly="True" SortExpression="descripcion_Cco" Visible="False" >
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="Orden" HeaderText="Orden" ReadOnly="True" 
                    SortExpression="Orden" Visible="False" />
            </Columns>
        </asp:GridView>
    </form>
</body>
</html>
