﻿<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmExportaPostulantes.aspx.vb" Inherits="administrativo_frmExportaPostulantes" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div id ="diverrores" runat="server">    
    </div>
    
    <asp:GridView ID="grwListaPersonas" runat="server"
        AutoGenerateColumns="False" 
            DataKeyNames="codigo_alu,codigo_pso,EstadoPostulacion,imprimiocartacat_Dal,Categorizacion,otroalu,categorizado_Dal" CellPadding="3" 
        SkinID="skinGridViewLineas" EmptyDataText="No se encontraron registros.">
        
        <Columns>
            <asp:BoundField HeaderText="Nro" />
            <asp:BoundField DataField="TipoDoc" HeaderText="Tipo Doc.">
            </asp:BoundField>
            <asp:BoundField DataField="Nrodoc" HeaderText="Nro. Doc.">
            </asp:BoundField>
            <asp:BoundField HeaderText="Participante" DataField="participante" />            
            <asp:BoundField DataField="codUniversitario" HeaderText="Cód. Univ." >
            </asp:BoundField>
            <asp:BoundField DataField="carrera" HeaderText="Escuela" />
            <asp:BoundField DataField="nombre_Min" HeaderText="Modalidad Ingreso" />
            <asp:BoundField DataField="CentroCosto" HeaderText="Centro Costo" />
            <asp:BoundField DataField="CicloIngreso" HeaderText="Ciclo Ingreso" />
            <asp:BoundField DataField="fechaRegistro_Dal" DataFormatString={0:g}  HeaderText="Fecha Registro" />
            <asp:BoundField DataField="usuario_per" HeaderText="Usuario Registro" />
            <asp:BoundField DataField="EstadoPostulacion" HeaderText="Estado" />                                                                          
            <asp:BoundField DataField="notaIngreso_Dal" HeaderText="notaIngreso_Dal" Visible="false"/>
            <asp:BoundField DataField="observacion_Dal" HeaderText="observacion_Dal" Visible="false"/>                                        
            <asp:BoundField DataField="imprimiocartacat_Dal" HeaderText="Imprimió Carta" />                                 
            <asp:BoundField DataField="foto_alu" HeaderText="Foto" />       
        </Columns>
        <PagerStyle BackColor="Silver" HorizontalAlign="Center" />
    </asp:GridView>
    
    </form>
</body>
</html>
