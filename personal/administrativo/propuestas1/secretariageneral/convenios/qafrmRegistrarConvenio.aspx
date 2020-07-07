<%@ Page Language="VB" AutoEventWireup="false" CodeFile="qafrmRegistrarConvenio.aspx.vb" Inherits="administrativo_propuestas1_secretariageneral_convenios_frmRegistrarConvenio" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
     body
        { font-family:Trebuchet MS;
          font-size:11.5px;
          cursor:hand;
          background-color:white;	
          padding:10px; 
        }
     .btn
       {
            border:1px solid #5D7B9D; 
            background:#F7F6F3; 
            font-family:Tahoma; 
            font-size:8pt; 
            font-weight:bold;  padding:3px;
            text-align: center;
            height: 25px;
        }
    TBODY {
	display: table-row-group;
}
      
        .style1
        {
            height: 20px;
        }
        .style2
        {
            height: 22px;
        }
        .style3
        {}
              
    </style>
   
  <link href="jquery/jquery-ui.css" rel="stylesheet" type="text/css" /> 
  <script src="jquery/jquery-1.10.2.js" type="text/javascript"></script>

    <script src="jquery/jquery-ui.js" type="text/javascript"></script>  
  

    <script src="jquery/jquery.ui.datepicker-es.js" type="text/javascript"></script>
  
  
    <script type="text/javascript">
        $(function() {
            $.datepicker.setDefaults($.datepicker.regional["es"]);
            $("#txtDesde").datepicker({
                firstDay: 0
            });
           
        });
</script>
</head>
<body>
    <form id="form1" runat="server">
    
      <table  cellpadding="3" cellspacing="0" style="border: 1px solid #C2CFF1; width:100%" >
        <tr style="background-color: #E8EEF7; font-weight: bold; height: 30px; font-size:15px;">
            <td  valign="middle" >REGISTRO DE CONVENIOS INSTITUCIONALES</td>            
        </tr>
        </table>


    <table class="">
        
        <tr>
            <td class="">
                &nbsp;</td>
            <td class="style7">
                &nbsp;</td>
            <td class="style1">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        
        <tr>
            <td class="" colspan="4" style="border-style: none none solid none;">
                <h4 style="margin:0px">Datos de Convenio</h4></td>
        </tr>
        
        <tr>
            <td class="">
                Código</td>
            <td class="style7">
                <asp:TextBox ID="txtCodigo" runat="server" MaxLength="100" ></asp:TextBox>
            </td>
            <td class="style1">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        
        <tr>
            <td class="style6">
                Institución</td>
            <td class="style6" colspan="3">
                <asp:DropDownList ID="ddlInstitucion" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        
        <tr>
            <td class="">
                Descripción</td>
            <td class="style7">
                &nbsp;</td>
            <td class="style1">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        
        <tr>
            <td class="style3" colspan="4">
                <asp:TextBox ID="txtDescripcion" runat="server" TextMode="MultiLine" 
                    Columns="100" MaxLength="8000" Rows="3"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="">
                Alcance</td>
            <td style="max-width:100px;" class="style7">
                &nbsp;</td>
            <td class="style1">
                Vinculos</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="" colspan="2">
                <asp:TextBox ID="txtAlcance" runat="server" TextMode="MultiLine"  Columns="60" MaxLength="8000" Rows="3"></asp:TextBox>
            </td>
            <td class="" colspan="2">
                <asp:TextBox ID="txtVinculo" runat="server" TextMode="MultiLine"  Columns="60" MaxLength="8000" Rows="3"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="">
                Duración</td>
            <td class="style7">
                <asp:TextBox ID="txtduracion" runat="server"></asp:TextBox>
                <asp:DropDownList ID="ddlPeriodo" runat="server">
                    <asp:ListItem Value="1">Días</asp:ListItem>
                    <asp:ListItem Value="4">Semanas</asp:ListItem>
                    <asp:ListItem Value="2">Meses</asp:ListItem>
                    <asp:ListItem Value="3">Años</asp:ListItem>
                </asp:DropDownList>
&nbsp;
                &nbsp;
                </td>
            <td class="style1">
                <asp:CheckBox ID="chkDuracionIndefinida" runat="server" Text="Indefinida" 
                    AutoPostBack="True" />
            </td>
            <td>
                <asp:CheckBox ID="chkRenovacion" runat="server" Text="Renovación" />
            </td>
        </tr>
        <tr>
            <td class="">
                Fecha de Inicio</td>
            <td class="style7">
                <asp:TextBox ID="txtDesde" runat="server"></asp:TextBox>
            </td>
            <td class="style1">
                Tipo</td>
            <td>
                <asp:DropDownList ID="ddlModalidad" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="">
                Ámbito</td>
            <td class="style7">
                <asp:DropDownList ID="ddlAmbito" runat="server">
                </asp:DropDownList>
            </td>
            <td class="style1">
                Sector</td>
            <td>
                <asp:DropDownList ID="ddlSector" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="">
                Adjuntar Archivo</td>
            <td class="style7">
                <asp:FileUpload ID="FileUpload1" runat="server" />
            </td>
            <td class="style1">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="">
                &nbsp;</td>
            <td class="style7">
                &nbsp;</td>
            <td class="style1">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
      <tr>
            <td class="" colspan="4" style="border-style: none none solid none;">
                <h4 style="margin:0px">Datos de Gestión</h4></td>
        </tr>
        <tr>
            <td class="style2">
                Área Coordinadora</td>
            <td class="style2" colspan="3">
                <asp:DropDownList ID="ddlCco" runat="server">
                </asp:DropDownList>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="">
                Responsable</td>
            <td class="style7" colspan="3">
                <asp:DropDownList ID="ddlResponsable" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="">
                Contacto</td>
            <td class="style7">
                <asp:TextBox ID="txtContacto" runat="server" MaxLength="8000" ></asp:TextBox>
            </td>
            <td>
                Sitio Web</td>
            <td>
                <asp:TextBox ID="txtSitioweb" runat="server" MaxLength="8000" ></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="">
                Email</td>
            <td class="style7">
                <asp:TextBox ID="txtEmail" runat="server" MaxLength="800"></asp:TextBox>
            </td>
            <td class="style1">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="">
                Observacion</td>
            <td class="style7">
                &nbsp;</td>
            <td class="style1">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="" colspan="4">
                <asp:TextBox ID="txtObservacion" runat="server" TextMode="MultiLine"  
                    Columns="60" MaxLength="200" Rows="3"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="" colspan="4">
                &nbsp;</td>
        </tr>
     
    <!--
       <tr>
            <td class="style3" colspan="4" style="border-style: none none solid none;">
                <h4 style="margin:0px">Beneficios</h4></td>
        </tr>
        <tr>
            <td class="style2">
                Registrados</td>
            <td class="style2">
                Lista Disponibles</td>
            <td class="style2">
                &nbsp;</td>
            <td class="style2">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style2">
                <asp:Button ID="btnQuitar" runat="server" Text="Quitar&gt;&gt;" 
                    CssClass="btn" />
                </td>
            <td class="style2">
                <asp:Button ID="btnAgregar" runat="server" Text="Agregar" 
                    CssClass="btn" />
                </td>
            <td class="style2">
                &nbsp;</td>
            <td class="style2">
                &nbsp;</td>
        </tr>
        <tr>
            <td >
                <asp:GridView ID="gvPalabrasRegistradas" runat="server" 
                    AutoGenerateColumns="False" 
                    EmptyDataText="Palabras claves no registradas para este convenio" 
                    DataKeyNames="codigo_ckw" CellPadding="4" ForeColor="#333333" 
                    GridLines="None" ShowHeader="False">
                    <RowStyle BackColor="#EFF3FB" />
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkElegirR" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="palabra" />
                    </Columns>
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#2461BF" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
            </td>
            <td >
                <asp:GridView ID="gvPalabrasDisponible" runat="server" 
                    AutoGenerateColumns="False" DataKeyNames="codigo_ckw" BackColor="White" 
                    BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                    ForeColor="Black" GridLines="Vertical" ShowHeader="False">
                    <RowStyle BackColor="#F7F7DE" />
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkElegirD" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="palabra" />
                    </Columns>
                    <FooterStyle BackColor="#CCCC99" />
                    <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
                </td>
            <td >
                &nbsp;</td>
        </tr> <!-->
        <tr>
            <td >
                &nbsp;</td>
            <td >
                &nbsp;</td>
            <td >
                &nbsp;</td>
            <td >
                &nbsp;</td>
        </tr>
        <tr>
            <td >
                &nbsp;</td>
            <td >
                &nbsp;</td>
            <td >
                &nbsp;</td>
            <td >
                &nbsp;</td>
        </tr>
   
        <tr>
            <td colspan="4" style="text-align: center" >
                <asp:Button ID="btn_guardar" runat="server" Text="Registrar Convenio" 
                    CssClass="btn" />
            </td>
        </tr>
        </table>
    </form>
</body>
</html>
