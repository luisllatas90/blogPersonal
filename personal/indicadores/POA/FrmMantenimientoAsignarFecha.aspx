<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmMantenimientoAsignarFecha.aspx.vb" Inherits="indicadores_POA_FrmMantenimientoAsignarFecha" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../css/estilo.css" rel="stylesheet" type="text/css" media="screen" />
    <style type="text/css">
    .titulo_poa 
    {
        position:absolute;
        top:15px;
        left:15px;
        font-size:14px;
        font-weight:bold;
        font-family:"Helvetica Neue",Helvetica,Arial,sans-serif;
        color:#337ab7;
        background-color:White;
        padding-bottom:10px;
        padding-left:5px;
        padding-right:5px;    
        z-index:1;    
    }
    .contorno_poa
    {
        position:relative;
        top:10px;
        border:1px solid #C0C0C0;
        padding-left:4px;
        padding-top:20px;
        padding-right:4px;
    }
    .mensajeExito
    {
        background-color: #d9edf7;
        border: 1px solid #808080;  
        font-weight:bold;
        color:#31708f;
        height:18px;
        padding-top:3px;
        padding-bottom:3px;
    } 
    .mensajeEliminado
    {
        color:#8a6d3b;
        background-color:#fcf8e3;
        border: 1px solid #C5BE51;
        font-weight:bold;
        height:18px;
        padding-top:3px;
        padding-bottom:3px;
    }   
    .mensajeError
    {
        background-color: #f2dede;
        border: 1px solid #E9ABAB;
        font-weight:bold;
        color:#a94442;
        height:18px;
        padding-top:3px;
        padding-bottom:3px;
    }
    .tab_activo
    {
        width:200px;
        vertical-align:middle;
        font-weight:bold;
        color:White;
        background-color:#3871b0;
        border-color:#285e8e;
        border-style:inset;
        border-width:1px;
        border-bottom-width:0px;
        font-size:12px;
        font-family:"Helvetica Neue",Helvetica,Arial,sans-serif;
    }
        
    .tab_inactivo
    {
        width:200px;
        vertical-align:middle;
        font-weight:bold;
        color:#FFF;
        background-color:#337ab7;
        filter:alpha(opacity=65);
        border-color:#ccc;
        border-style:solid;
        border-width:1px;
        border-bottom-color:#337ab7 ;
        font-size:12px;
        font-family:"Helvetica Neue",Helvetica,Arial,sans-serif;
    }
    .celda_combinada
    {
        border-color:rgb(169,169,169);
        border-style:solid;
        border-width:1px;
    }
             .FixedHeader {
            position: absolute;
            font-weight: bold;
        }  
          
        .nombre_poa
        {
            color:#468847;
            font-weight:bold;
            padding-top:3px;
            padding-bottom:3px;
        }
        .nombre_acp
        {
            color:#aa6708;
            font-weight:bold;
            padding-top:3px;
            padding-bottom:3px;
        }
        .nombre_dap
        {
            color:#084B8A;
            opacity:0.65;
            font-weight:bold;
            padding-top:3px;
            padding-bottom:3px;   
        }
        .fecha_revisa
        {
            color:#FF4000;
            opacity:0.65;
            font-weight:bold;
            padding-top:3px;
            padding-bottom:3px;   
        }
        .fecha
        {
            color:#088A85;
            opacity:0.65;
            font-weight:bold;
            padding-top:3px;
            padding-bottom:3px;   
        }
        
        
    </style>
</head>
<body>
    <form id="form1" runat="server">
 <div class="titulo_poa">
        <asp:Label ID="Label1" runat="server"
            Text="Asignación de Fecha de Revisión"></asp:Label>
    </div>
    <div class="contorno_poa">
        <table width="100%" id="tabla" runat="server">
        <tr>
        <td width="140px" >Plan Operativo Anual</td>
        <td width="510px"><div class="nombre_poa"><asp:Label ID="lblpoa" runat="server" 
                Text="POA" Width="850px"></asp:Label></div></td>
        </tr>
        <tr>
        <td width="50px">Programa/Proyecto</td>
        <td width="140px"><div class="nombre_acp"><asp:Label ID="lblacp" runat="server" 
                Text="Programa/Proyecto" Width="850px"></asp:Label></div></td>
        </tr>
        <tr>
        <td>Detalle de Actividad</td>
        <td><div class="nombre_dap"><asp:Label ID="lbldap" runat="server" Text="DetalleAcp" 
                Width="850px"></asp:Label></div></td>
        </tr>
        <tr>
        <td>Fecha de Inicio</td>
        <td><div class="fecha"><asp:Label ID="lblfecini" runat="server" Text="dd/mm/yyyy"></asp:Label></div></td>
        </tr>
        <tr>
        <td>Fecha Fin</td>
        <td><div class="fecha"><asp:Label ID="lblfecfin" runat="server" Text="dd/mm/yyyy"></asp:Label></div></td>
        </tr>
        <tr>
        <td>Fecha de Revision</td>
        <td><div class="fecha_revisa">
        <asp:TextBox ID="txtfecrevisa" runat="server" ReadOnly="true" Width="90px" ValidationGroup="Grupo1"></asp:TextBox>
         <asp:Button ID="btnCalendario" runat="server" Text="..." Width="20px" />
          <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtfecrevisa"
             runat="server" ErrorMessage="Seleccione Fecha de Revisión" ValidationGroup="Grupo1">&nbsp;</asp:RequiredFieldValidator>
            </div>
            
        </td> 
        </tr>
        <tr>
            <td></td>
            <td>
                <div id="divCalendario" runat="server">
                    <asp:Calendar ID="Calendario" runat="server" EnableTheming="True" >
                    <SelectedDayStyle BackColor="#FF6666" />
                    <TitleStyle Font-Bold="True" Font-Names="Arial Narrow" 
                         />
                    </asp:Calendar>
                </div>
            </td>
        </tr>
        <tr>
        <td colspan="2">
        <asp:ValidationSummary runat="server" ID="Validationsummary" ValidationGroup="Grupo1" ShowSummary="true" />
         <div runat="server" id="aviso">
                        <asp:Label ID="lblmensaje" runat="server" Font-Bold="true"></asp:Label>
         </div>
        </td>
        </tr>
        <tr>
        <td align="right" colspan="3">
            <asp:Button ID="btnGuardar" runat="server" CssClass="guardar2" Text="   Guardar" ValidationGroup="Grupo1"/>
            &nbsp;
            <asp:Button ID="btnCancelar" runat="server" CssClass="regresar2" Text="  Cancelar" />
         </td>
        </tr>
        </table>
        <asp:HiddenField ID="hdcodigo_dap" runat="server" Value="0" />
</div>
</form>
</body>
</html>
