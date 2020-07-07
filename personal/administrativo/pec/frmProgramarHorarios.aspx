<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmProgramarHorarios.aspx.vb" Inherits="administrativo_pec_frmProgramarHorarios" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register assembly="BusyBoxDotNet" namespace="BusyBoxDotNet" tagprefix="busyboxdotnet" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title> 
    <script src="jquery/jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function() {
            $('.tabs .tab-links a').on('click', function(e) {
                var currentAttrValue = $(this).attr('href');
                // Show/Hide Tabs
                $('.tabs ' + currentAttrValue).show().siblings().hide();
                // Change/remove current tab to active
                $(this).parent('li').addClass('active').siblings().removeClass('active');
                e.preventDefault();
                
            });
        });               
    </script>
<script type="text/javascript">
    function CheckVarias( valor) {

        if (form1.chkVar.checked == true ) {
           
        }else {
        
        }
    }

   
    
</script>
    <style type="text/css" >
      body
        { font-family:Trebuchet MS;
          font-size:11px;
          cursor:hand;
          background-color:#F0F0F0;	
        }
        .fuente
        {font-family:Trebuchet MS;
          font-size:11px;
            }
        /*----- Tabs -----*/
        .tabs {
            width:100%;
            display:inline-block;
        }

        /*----- Tab Links -----*/
        /* Clearfix */
        .tab-links:after {
            display:block;
            clear:both;
            content:'';
        }

        .tab-links li {
            margin:0px 5px;
            float:left;
            list-style:none;
        }

        .tab-links a {
            padding:5px 10px;
            display:inline-block;
            border-radius:3px 3px 0px 0px;
            background:#F0F0F0;
            font-size:12px;
            font-weight:400;
            color:#4c4c4c;
            transition:all linear 0.15s;
        }

        .tab-links a:hover {
            background:#a7cce5;
            text-decoration:none;
        }

        li.active a, li.active a:hover {
            background:#fff;
            color:blue;
        }

        /*----- Content of Tabs -----*/
        .tab-content {
            padding:15px;
            border-radius:3px;
            box-shadow:-1px 1px 1px rgba(0,0,0,0.15);
            background:#fff;
        }

        .tab {
            display:none;
        }

        .tab.active {
            display:block;
           
        }
         .celda1
        {           
            background:white;
            padding:5px;
            border:1px solid #808080;           
            color:#2F4F4F;
            font-weight:bold;            
        }
        
          .btn
       {
            border:1px solid #5D7B9D; 
            background:#F7F6F3 ; 
            font-family:Tahoma; 
            font-size:8pt; 
            font-weight:bold;  padding:5px; 
       }
        .style1
        {
            width: 113px;
        }
        .style2
        {
            width: 120px;
        }
        .sinTop { border-top:0px; }
       .sinleft{ border-left:0px; }
       .sinRight{ border-right:0px; }
       .sinBottom{ border-bottom:0px; }

    </style>
  
</head>
<body>
<form id="form1" runat="server">
<busyboxdotnet:BusyBox ID="BusyBox1" runat="server" ShowBusyBox="OnLeavingPage" Image="Clock" Text="Su solicitud esta siendo procesada..." Title="Por favor espere" />
<div class="tabs">
 <ul class="tab-links">
    <li id="li1Asp" runat="server" ><a href="#tab1">Horario PreDefinido</a></li>
    <li id="li2Asp" runat="server" ><a href="#tab2">Horario Personalizado</a></li>        
 </ul>

 <div class="tab-content">
    <div id="tab1" runat="server" class="tab">
    <br /><br />
    <table style="width: 60%;" border="0" cellpadding="2" cellspacing="0">
    <tr>
    <td class="style1">Día</td>
    <td class="style2 style1">Mes</td>
    <td>Año</td>
    </tr>
    <tr>
    <td class="style1"><asp:DropDownList ID="ddlSelDia" runat="server" AutoPostBack="True" CssClass="fuente">
                <asp:ListItem Value="LU">Lunes</asp:ListItem>
                <asp:ListItem Value="MA">Martes</asp:ListItem>
                <asp:ListItem Value="MI">Miércoles</asp:ListItem>
                <asp:ListItem Value="JU">Jueves</asp:ListItem>
                <asp:ListItem Value="VI">Viernes</asp:ListItem>
                <asp:ListItem Value="SA">Sábado</asp:ListItem>
            </asp:DropDownList>
        </td><td class="style2"><asp:DropDownList ID="ddlSelMes" runat="server"   CssClass="fuente"
                AutoPostBack="True">
            <asp:ListItem Value="01">Enero</asp:ListItem>
            <asp:ListItem Value="02">Febrero</asp:ListItem>
            <asp:ListItem Value="03">Marzo</asp:ListItem>
            <asp:ListItem Value="04">Abril</asp:ListItem>
            <asp:ListItem Value="05">Mayo</asp:ListItem>
            <asp:ListItem Value="06">Junio</asp:ListItem>
            <asp:ListItem Value="07">Julio</asp:ListItem>
            <asp:ListItem Value="08">Agosto</asp:ListItem>
            <asp:ListItem Value="09">Setiembre</asp:ListItem>
            <asp:ListItem Value="10">Octubre</asp:ListItem>
            <asp:ListItem Value="11">Noviembre</asp:ListItem>
            <asp:ListItem Value="12">Diciembre</asp:ListItem>
            
        </asp:DropDownList>
        </td>
        <td><asp:DropDownList ID="ddlSelAño" runat="server" AutoPostBack="True"  CssClass="fuente">
        </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td class="style1" >
            &nbsp;</td>
      
        <td class="style2">
                        &nbsp;</td>
            <td></td>
      
    </tr>
    <tr>
        <td class="style1" >
            Fecha</td>
      
        <td class="style2">
            Horario</td>
            <td>&nbsp;</td>
      
    </tr>
    <tr>
        <td class="style1">
            <asp:DropDownList ID="ddlFecha" runat="server"  CssClass="fuente">
            </asp:DropDownList>
        </td>
      
        <td class="style2">
            <asp:DropDownList ID="ddlHorario" runat="server"  CssClass="fuente">
              
            </asp:DropDownList>
        </td>
      <td style="text-align: right">
          &nbsp;</td>
      
    </tr>
    <tr>
        <td class="style1">
            &nbsp;</td>
      
        <td class="style2">
            &nbsp;</td>
      <td style="text-align: right">
<asp:Button ID="btnRegistrarPre" runat="server" Text="Registrar" CssClass="btn" />
        </td>
      
    </tr>
    </table>
    </div>
    <div id="tab2" runat="server" class="tab">    
        <table style="width: 100%;">
    <tr>
    <td></td>
    <td>&nbsp;</td>
    </tr>
    
    <tr>
    <td>Fecha Inicio</td>
    <td>
        <asp:DropDownList ID="ddlInicioDia" runat="server" CssClass="fuente">
        </asp:DropDownList>
        <asp:DropDownList ID="ddlInicioMes" runat="server" CssClass="fuente">
            <asp:ListItem Value="01">Enero</asp:ListItem>
            <asp:ListItem Value="02">Febrero</asp:ListItem>
            <asp:ListItem Value="03">Marzo</asp:ListItem>
            <asp:ListItem Value="04">Abril</asp:ListItem>
            <asp:ListItem Value="05">Mayo</asp:ListItem>
            <asp:ListItem Value="06">Junio</asp:ListItem>
            <asp:ListItem Value="07">Julio</asp:ListItem>
            <asp:ListItem Value="08">Agosto</asp:ListItem>
            <asp:ListItem Value="09">Setiembre</asp:ListItem>
            <asp:ListItem Value="10">Octubre</asp:ListItem>
            <asp:ListItem Value="11">Noviembre</asp:ListItem>
            <asp:ListItem Value="12">Diciembre</asp:ListItem>
        </asp:DropDownList>
        <asp:DropDownList ID="ddlInicioAño" runat="server" CssClass="fuente">
        </asp:DropDownList>
        </td>
    </tr>
    
    <tr>
    <td>Hora Inicio</td>
    <td>
        <asp:DropDownList ID="ddlInicioHora" runat="server" CssClass="fuente">
        </asp:DropDownList>
        <asp:DropDownList ID="ddlInicioMinuto" runat="server" CssClass="fuente">
        </asp:DropDownList>
        </td>
    </tr>
    
    <tr>
    <td>Hora Fin</td>
    <td>
        <asp:DropDownList ID="ddlFinHora" runat="server" CssClass="fuente">
        </asp:DropDownList>
        <asp:DropDownList ID="ddlFinMinuto" runat="server" CssClass="fuente">
        </asp:DropDownList>
        </td>
    </tr>
    
    <tr>
    <td>Fecha Fin</td>
    <td>
        <asp:CheckBox ID="chkVarias" runat="server" Text="Crear varias sesiones" 
             CssClass="fuente"  AutoPostBack=true/>
        
       
             
        <br />
        <asp:DropDownList ID="ddlFinDia" runat="server" CssClass="fuente" >
        </asp:DropDownList>
        <asp:DropDownList ID="ddlFinMes" runat="server" CssClass="fuente">
            <asp:ListItem Value="01">Enero</asp:ListItem>
            <asp:ListItem Value="02">Febrero</asp:ListItem>
            <asp:ListItem Value="03">Marzo</asp:ListItem>
            <asp:ListItem Value="04">Abril</asp:ListItem>
            <asp:ListItem Value="05">Mayo</asp:ListItem>
            <asp:ListItem Value="06">Junio</asp:ListItem>
            <asp:ListItem Value="07">Julio</asp:ListItem>
            <asp:ListItem Value="08">Agosto</asp:ListItem>
            <asp:ListItem Value="09">Setiembre</asp:ListItem>
            <asp:ListItem Value="10">Octubre</asp:ListItem>
            <asp:ListItem Value="11">Noviembre</asp:ListItem>
            <asp:ListItem Value="12">Diciembre</asp:ListItem>
        </asp:DropDownList>
        <asp:DropDownList ID="ddlFinAño" runat="server" CssClass="fuente">
        </asp:DropDownList>
        </td>
    </tr>
    
    <tr>
    <td>Días de Sesión</td>
    <td>
            <asp:DropDownList ID="ddlDiaSelPer" runat="server" CssClass="fuente">
                <asp:ListItem Value="LU">Lunes</asp:ListItem>
                <asp:ListItem Value="MA">Martes</asp:ListItem>
                <asp:ListItem Value="MI">Miércoles</asp:ListItem>
                <asp:ListItem Value="JU">Jueves</asp:ListItem>
                <asp:ListItem Value="VI">Viernes</asp:ListItem>
                <asp:ListItem Value="SA">Sábado</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    
    <tr>
    <td>
        &nbsp;</td>
    <td>
<asp:Button ID="btnRegistrarPers" runat="server" Text="Registrar" CssClass="btn" />
        </td>
    </tr>
    
</table>

    </div> 
    
 </div>
   
 <asp:Label ID="lblMsj" runat="server" 
        style="color: #003366; font-weight: 700; font-style: italic"></asp:Label>
</div>

</form>        
</body>
</html>
