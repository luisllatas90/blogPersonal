<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Interesado.aspx.vb" Inherits="DataJson_crm_Interesado" %>

<form id="mainForm" runat="server">
    <asp:GridView ID="grwInteresados" runat="server" AutoGenerateColumns="false">
        <Columns>
            <asp:BoundField HeaderText="Nro" />
            <asp:BoundField DataField="numerodoc_int" HeaderText="NRO DOC." />
            <asp:BoundField DataField="apepaterno_int" HeaderText="APE. PATERNO" />
            <asp:BoundField DataField="apematerno_int" HeaderText="APE. MATERNO" />
            <asp:BoundField DataField="nombres_int" HeaderText="NOMBRES" />
            <asp:BoundField DataField="situacion" HeaderText="SITUACIÓN" />
            <asp:BoundField DataField="nombre_Cpf" HeaderText="CARRERA" />
            <asp:BoundField DataField="porcentaje" HeaderText="INTERÉS" />
            <asp:BoundField DataField="descripcion_ecom" HeaderText="ESTADO ÚLT. COM." />
            <asp:BoundField DataField="fecha_com" HeaderText="FECHA ÚLT. COM." DataFormatString="{0:dd/MM/yyyy}" SortExpression="fecha_com"  />
            <asp:BoundField DataField="detalle_acu" HeaderText="ÚLT. ACUERDO" />
            <asp:BoundField DataField="requisitos_faltantes" HeaderText="REQUISITOS FALTANTES" />
            <asp:TemplateField HeaderText ="FECHA REG. INTERESADO" >
                <ItemTemplate><%#DateTime.Parse(Eval("fecha_reg").ToString()).ToString("d")%></ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</form>
