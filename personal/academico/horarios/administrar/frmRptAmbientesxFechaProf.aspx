<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmRptAmbientesxFechaProf.aspx.vb" Inherits="academico_horarios_administrar_frmRptAmbientesxFechaProf" %>
<%@ Register assembly="BusyBoxDotNet" namespace="BusyBoxDotNet" tagprefix="busyboxdotnet" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
     body
        { font-family:Trebuchet MS;
          font-size:11.5px;
          cursor:hand;
          background-color:white;	
        }
     .btn
       {
            border:1px solid #5D7B9D; 
            background:#F7F6F3 ; 
            font-family:Tahoma; 
            font-size:8pt; 
            font-weight:bold;  padding:3px; 
       }
    TBODY {
	display: table-row-group;
}
   
        #txtDesde, #txtHasta
        {
            background-color: #C9DDF5;
        }
      .txt1 { background-color:Green;}
      .txt2 { background-color:Blue;}
    </style>
     <link rel="stylesheet" href="jquery/jquery-ui.css" />
    <script src="jquery/jquery-1.9.1.js" type="text/javascript"></script>
    <script type="text/javascript" src="jquery/jquery-ui.js"></script>
    <script src="jquery/jquery.ui.datepicker-es.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function() {
            $.datepicker.setDefaults($.datepicker.regional["es"]);
            $("#txtDesde").datepicker({
                firstDay: 1
            });
            $("#txtHasta").datepicker({
                firstDay: 1
            });
        });
       
    </script>    
</head>
<body>
    <form id="form1" runat="server">
    
     <table  cellpadding="3" cellspacing="0" style="border: 1px solid #C2CFF1; width:95%" >
        <tr style="background-color: #E8EEF7; font-weight: bold; height: 30px">
            <td colspan="7"><b>Consulta de Ambientes Asignados</b></td>            
        </tr>
        </table>
        <br />
        <table>
        <tr>
        <td><b>Escuela</b></td>
        <td colspan="4"><asp:DropDownList ID="ddlCco" runat="server" AutoPostBack="True"></asp:DropDownList>
        </td>
        </tr>
        <tr>
        <td><b>Programa</b></td>
        <td colspan="4">
            <asp:DropDownList ID="ddlPrograma" runat="server">
            </asp:DropDownList>
           
        </td>
        </tr>
        <tr>
        <td><b>Estado</b></td>
        <td colspan="4"><asp:DropDownList ID="ddlEstado" runat="server">
            <asp:ListItem Value="%">TODOS</asp:ListItem>
            <asp:ListItem Value="1">PENDIENTE</asp:ListItem>
            <asp:ListItem Value="2">ASIGNADO</asp:ListItem>
            </asp:DropDownList>
        </td>
        </tr>
        <tr>
        <td><b>Desde</b></td><td><input type="text" runat="server" id="txtDesde"/></td>
            <td>&nbsp;</td>
        <td><b>Hasta</b></td><td><input type="text" runat="server" id="txtHasta"/></td>
              <td><asp:Button ID="btnBuscar" runat="server" Text="Consultar" CssClass="btn" /></td> 
              <td><asp:Button ID="btnExportar" runat="server" Text="Exportar" CssClass="btn" /></td> 
        </tr> 
        <tr>
        <td>
            <asp:Label ID="Label1" runat="server" Text="&nbsp;&nbsp;&nbsp;Pendiente"  Width="9px" Height="13px"  CssClass="txt1" ></asp:Label><br />
                        <asp:Label ID="Label2" runat="server" Text="&nbsp;&nbsp;&nbsp;Asignado"  Width="9px" Height="13px"  CssClass="txt2" ></asp:Label>
            </td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
         
        </tr>       
        </table>
        
         <br />
        <table>
        <tr>
        <td>
           <asp:GridView ID="gridAmbientes" runat="server" AutoGenerateColumns="False" 
                CaptionAlign="Top" BorderStyle="None" CellPadding="4" 
             AlternatingRowStyle-BackColor="#F7F6F4"  
                PageSize="15" DataKeyNames="codigo_test" >
               <PagerSettings PageButtonCount="25" Position="TopAndBottom" />
             <RowStyle BorderColor="#C2CFF1" />
             <Columns>
                <asp:BoundField DataField="preferencial_amb" HeaderText="Tipo">
                 <ItemStyle Font-Underline="false" ForeColor="black" />
                 </asp:BoundField>                                    
                 <asp:BoundField DataField="Ambiente" HeaderText="Ambiente">
                 <ItemStyle Font-Underline="false" ForeColor="black" Font-Bold="True" />
                 </asp:BoundField>
                 <asp:BoundField DataField="nroMatriculados_Cup" HeaderText="Mat" />
                 <asp:BoundField DataField="dia_Lho" HeaderText="Día">
                 <ItemStyle Font-Underline="false" ForeColor="black" />
                 </asp:BoundField>                 
                 <asp:BoundField DataField="fechaIni_lho" HeaderText="Fecha Inicio">
                 <ItemStyle Font-Underline="false" ForeColor="black" />
                 </asp:BoundField> 
                 <asp:BoundField DataField="horainicio" HeaderText="Hora Inicio">
                 <ItemStyle Font-Underline="false" ForeColor="black" />
                 </asp:BoundField>                  
                 <asp:BoundField DataField="horaFin_Lho" HeaderText="Hora Fin">
                 <ItemStyle Font-Underline="false" ForeColor="black" />
                 </asp:BoundField> 
                   <asp:BoundField DataField="cco" HeaderText="Centro Costos">
                 <ItemStyle Font-Underline="false" ForeColor="black" />
                 </asp:BoundField>
                 <asp:BoundField DataField="Curso" HeaderText="Curso" />
                 <asp:BoundField DataField="descripcion" HeaderText="Descripción">
                 <ItemStyle Font-Underline="false" ForeColor="black" />
                 </asp:BoundField>
                 <asp:BoundField DataField="docente" HeaderText="Docente" />
                 <asp:BoundField DataField="Personal" HeaderText="Responsable">
                 <ItemStyle Font-Underline="false" ForeColor="black" />
                 </asp:BoundField> 
                <asp:BoundField DataField="estadoHorario_lho" HeaderText="Estado">
                 <ItemStyle Font-Underline="false" ForeColor="black" />
                 </asp:BoundField>
                  <asp:BoundField DataField="codigo_Test" HeaderText="codigo_test" Visible="false">
                 <ItemStyle Font-Underline="false" ForeColor="black"/>
                 </asp:BoundField>
             </Columns>
             <EmptyDataTemplate>
                 <div style="color:#3266DB; background-color:#E8EEF7; padding:5px; font-style:italic;">
                     No se encontraron registros.
                 </div>
             </EmptyDataTemplate>
             <HeaderStyle BackColor="#e8eef7" BorderColor="#99BAE2" BorderStyle="Solid" BorderWidth="1px" ForeColor="#3366CC" />
             <AlternatingRowStyle BackColor="#F7F6F4" />
         </asp:GridView>
        </td>
        </tr>
        </table>
        <asp:GridView ID="gridAmbientesExportar" Visible="False" runat="server" AutoGenerateColumns="False" 
                CaptionAlign="Top" BorderStyle="None" CellPadding="4" 
             AlternatingRowStyle-BackColor="#F7F6F4"  
                PageSize="15" DataKeyNames="codigo_test" >
               <PagerSettings PageButtonCount="25" Position="TopAndBottom" />
             <RowStyle BorderColor="#C2CFF1" />
             <Columns>
                                              
                 <asp:BoundField DataField="Ambiente" HeaderText="Ambiente">
                 <ItemStyle Font-Underline="false" ForeColor="black" Font-Bold="True" />
                 </asp:BoundField>
                 <asp:BoundField DataField="nroMatriculados_Cup" HeaderText="Mat" />
                 <asp:BoundField DataField="dia_Lho" HeaderText="Día">
                 <ItemStyle Font-Underline="false" ForeColor="black" />
                 </asp:BoundField>                 
                 <asp:BoundField DataField="fechaIni_lho" HeaderText="Fecha Inicio">
                 <ItemStyle Font-Underline="false" ForeColor="black" />
                 </asp:BoundField> 
                 <asp:BoundField DataField="horainicio" HeaderText="Hora Inicio">
                 <ItemStyle Font-Underline="false" ForeColor="black" />
                 </asp:BoundField>                  
                 <asp:BoundField DataField="horaFin_Lho" HeaderText="Hora Fin">
                 <ItemStyle Font-Underline="false" ForeColor="black" />
                 </asp:BoundField> 
                   <asp:BoundField DataField="cco" HeaderText="Centro Costos">
                 <ItemStyle Font-Underline="false" ForeColor="black" />
                 </asp:BoundField>
                 <asp:BoundField DataField="Curso" HeaderText="Curso" />
                 <asp:BoundField DataField="descripcion" HeaderText="Descripción">
                 <ItemStyle Font-Underline="false" ForeColor="black" />
                 </asp:BoundField>
                  <asp:BoundField DataField="docente" HeaderText="Docente" />
                 <asp:BoundField DataField="Personal" HeaderText="Responsable">
                 <ItemStyle Font-Underline="false" ForeColor="black" />
                 </asp:BoundField> 
                <asp:BoundField DataField="estadoHorario_lho" HeaderText="Estado">
                 <ItemStyle Font-Underline="false" ForeColor="black" />
                 </asp:BoundField>
                  <asp:BoundField DataField="codigo_Test" HeaderText="codigo_test" Visible="false">
                 <ItemStyle Font-Underline="false" ForeColor="black"/>
                 </asp:BoundField>
             </Columns>
             <EmptyDataTemplate>
                 <div style="color:#3266DB; background-color:#E8EEF7; padding:5px; font-style:italic;">
                     No se encontraron registros.
                 </div>
             </EmptyDataTemplate>
             <HeaderStyle BackColor="#e8eef7" BorderColor="#99BAE2" BorderStyle="Solid" BorderWidth="1px" ForeColor="#3366CC" />
             <AlternatingRowStyle BackColor="#F7F6F4" />
         </asp:GridView>
    </form>
</body>
</html>
