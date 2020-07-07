<%@ Page Language="VB" AutoEventWireup="false" CodeFile="rptActualizacionFoto.aspx.vb" Inherits="rptActualizacionFoto" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta name="tipo_contenido"  content="text/html;" http-equiv="content-type" charset="utf-8">

    <link href="../../private/estilo.css" rel="stylesheet" type="text/css" />    
    
    <link rel="stylesheet" href="http://code.jquery.com/ui/1.10.1/themes/base/jquery-ui.css" />
    
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
        function txtHasta_onclick() {

        }

    </script>

	<style type="text/css">

        
       #TablaFotos td
       {
              
           border: 1px solid #C0C0C0;
           padding:5px;
           text-align:center;
           width:150px;
        }
        

        
        .style1
        {
            height: 17px;
        }
        

        
        .style2
        {
            height: 23px;
        }
        

        
    </style>
</head>
<body>
    <form id="form1" runat="server">
  
    <table width="100%">
        <tr>
            <td style="width: 100%; border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #0099FF;" 
                height="40px" bgcolor="#E6E6FA">
            <asp:Label ID="lblTitulo" runat="server" Text="Reporte de Actualización de Foto" 
                Font-Bold="True" Font-Size="11pt"></asp:Label></td>
        </tr>
     </table>
     <br />
     <table >
     <tr>
         <td > Carrera profesional: </td>
         <td >
            <asp:DropDownList ID="ddlEscuela" runat="server" Height="19px" Width="307px">
            </asp:DropDownList>
         </td>
         <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
         <td  colspan="5"> 
             <asp:CheckBox ID="chkIncluirFechas" runat="server" 
                 Text="Incluir rango de fechas" AutoPostBack="True" />
         </td>
      </tr>
     <tr>
         <td class="style2" > Año Egreso:</td>
         <td class="style2" >
             <asp:DropDownList ID="ddlEgreso" runat="server">
             </asp:DropDownList>
         </td>
         <td class="style2"></td>
         <td  colspan="5" class="style2"> 
             </td>
      </tr>
      <tr>
        <td > Apellidos o Nombres:</td>
        <td >
                    <asp:TextBox ID="txtApellidoNombre" runat="server" Width="96%"></asp:TextBox>
        </td>
         <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td> 
      
         
        <td ><label runat="server" id="lbldesde" visible="False">Desde:</label></td>
      
         
        <td colspan="2" ><input type="text" runat="server" id="txtDesde" visible="False" /></td>
      
         
        <td ><label runat="server" id="lblhasta" visible="False" >Hasta</label></td>
      
         
        <td ><input type="text"  runat="server" id="txtHasta" visible="False" /></td>
       
        
        </tr>
      <tr>
        <td  colspan="8" class="style1"> &nbsp;</td>
        </tr>
       <tr>
          <td > 
                <asp:Button ID="btnBusca" runat="server" Text="Buscar" CssClass="buscar2"
                    Width="109px" Height="21px" />
                </td>
          <td >
                <asp:Button ID="btnExportar" runat="server" Text="Exportar" CssClass="excel" Width="120px" Height="22px" />          
                </td>
          <td  colspan="3"> 
                &nbsp;</td>
          <td  colspan="3"> 
                &nbsp;</td>
       </tr>

        </table><br />
                <asp:Label ID="lblMensajeFormulario" runat="server" Font-Bold="True" 
                    ForeColor="#3366CC" Width="100%" style="margin-top: 0px" Height=""></asp:Label>
        <br />
      
        <div runat="server" id="listaFotos">
        
           
        
        </div>
          <asp:GridView ID="gvEgresados" runat="server" CellPadding="4" 
                        ForeColor="#333333" GridLines="both" Visible="true">
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#999999" />
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    </asp:GridView>
        </form>
        
</body>
</html>
