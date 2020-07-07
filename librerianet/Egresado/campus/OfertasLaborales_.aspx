<%@ Page Language="VB" AutoEventWireup="false" CodeFile="OfertasLaborales_.aspx.vb" Inherits="Egresado_campus_OfertasLaborales" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            height: 22px;
            font-size: medium;
            text-align: left;
            color: #5D7B9D;
            font-weight: bold;
        }
        .style3
        {
            font-family: "Trebuchet MS";
            font-size: medium;
            font-weight: 700;
        }
        .style4
        {
            font-size: small;
            font-weight: normal;
            font-family: "Trebuchet MS";
        }
        .StyleImg
        {
            cursor:hand;
            }
        .style5
        {
            font-size: small;
            font-weight: normal;
            font-family: "Trebuchet MS";
            color: #3366CC;
        }
        
        .hdetalle
        {
             color: #5D7B9D;
             font-weight:bold;
               font-family: "Trebuchet MS";
               font-size:12px;
            }
                .hdetalle2
        {
             color: white;
             font-weight:bold;
               font-family: "Trebuchet MS";
               font-size:12px;
            }
        .style6
        {
            width: 208px;
        }
        .style7
        {
            
            font-family: 'Trebuchet MS'; font-size: small;
            color: #666666;
            text-align: justify;
            background-color: #FFFFFF;
        }
            .panel 
            {
                padding-left:10px;
                }
                .paneldetalles
                {
                    padding-left:30px;
                    border:1px solid;
                    }
        .style8
        {
        }
        .style10
        {
            background-color: #5D7B9D;
            color:White;
        }
        .style11
        {
            width: 66px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="padding-left:5px;">
    
        <table class="style1" 
            style=" border-style: solid; border-color: inherit; border-width: 1px; margin-bottom: 0px;">
            <tr>
                <td class="style2" colspan="2">
                    <span class="style3">Consulta de Ofertas Laborales<br />
                    <br />
                    <span class="style4">Lista de ofertas disponibles:</span></span></td>
            </tr>
            <tr>
                <td>
    
                    <span class="style3">
                    <asp:GridView ID="gvOfertas" runat="server" CellPadding="4" 
                        DataKeyNames="codigo_ofe,correocontacto_ofe" Font-Names="Trebuchet MS" 
                        Font-Size="Small" AutoGenerateColumns="False" 
                        EmptyDataText="No se encontraron ofertas laborales activas para tu carrera profesional" 
                        style="font-size: small" ForeColor="#333333" GridLines="Vertical" 
                        BorderStyle="Solid">
                        <RowStyle ForeColor="#333333" Width="40%" BackColor="#F7F6F3" />
                        <Columns>
                            <asp:BoundField DataField="oferta" HeaderText="Oferta Laboral" />
                            <asp:BoundField DataField="empresa" HeaderText="Empresa" />
                            <asp:BoundField DataField="contacto" HeaderText="Contacto" />
                            <asp:BoundField DataField="telefono" HeaderText="Teléfono" />
                            <asp:BoundField DataField="lugar" HeaderText="Lugar" />
                            <asp:BoundField DataField="fechaInicio" HeaderText="Fecha Inicio" />
                            <asp:BoundField DataField="fechaFin" HeaderText="Fecha Fin" />
                            <asp:TemplateField HeaderImageUrl="" 
                                HeaderText="Ver">                                
                                <EditItemTemplate>                                   
                                </EditItemTemplate>
                                <ItemTemplate >
                                    <asp:ImageButton ID="verDetalles" 
                                    runat="server" ImageUrl="~/images/busca.gif" Width="15px" 
                                    AlternateText="Clic para ver detalles" CssClass="StyleImg" onclick="verDetalles_Click" />
                                    
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderImageUrl="" 
                                HeaderText="Postular">                                
                                <EditItemTemplate>                                   
                                </EditItemTemplate>
                                <ItemTemplate >
                                    <asp:ImageButton ID="Postular" 
                                    runat="server" ImageUrl="~/images/postular.png" Width="15px" 
                                    AlternateText="Clic para enviar tu CV" CssClass="StyleImg" onclick="Postular_Click" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#999999" />
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    </asp:GridView>
                    </span>
    
                    <br />
                    <hr />
                    
                    <asp:Panel ID="PanelDetalles" runat="server" 
                        BorderStyle="None" BorderWidth="0px" Height="800px" Width="548px" CssClas="paneldetalles">      
                        <span class="style5">
                        <br />
                        <asp:Label ID="lblNombreOferta" runat="server" CssClass="style3" Text=""></asp:Label> </span>
                        &nbsp;<hr />

                        <table style="width: 100%">
                            <tr>
                                <td class="style10" colspan="2">
                                    <span class="hdetalle2">Descripción</span></td>
                            </tr>
                            <tr>
                                <td class="style11">
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                                <td class="style8">
                                    <asp:Label ID="lblDescripcion" runat="server" CssClass="style7" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="" colspan="2">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="style10" colspan="2">
                                    <span class="hdetalle2">Requisitos</span></td>
                            </tr>
                            <tr>
                                <td class="style11">
                                    &nbsp;</td>
                                <td>
                                    <asp:Label ID="lblRequisitos" runat="server" CssClass="style7" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="style11">
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="style10" colspan="2">
                                    <span class="hdetalle2">Correo de Contacto</span></td>
                            </tr>
                            <tr>
                                <td class="style11">
                                    &nbsp;</td>
                                <td>
                                    <asp:Label ID="lblCorreo" runat="server" CssClass="style7" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="style11">
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="style10" colspan="2">
                                    <span class="hdetalle2">Tipo de Trabajo</span></td>
                            </tr>
                            <tr>
                                <td class="style11">
                                    &nbsp;</td>
                                <td>
                                    <asp:Label ID="lblTipoTrabajo" runat="server" CssClass="style7" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="style11">
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                 <td class="style10" colspan="2">
                                    <span class="hdetalle2">Duración</span></td>
                            </tr>
                            <tr>
                                <td class="style11">
                                    &nbsp;</td>
                                <td>
                                    <asp:Label ID="lblDuracion" runat="server" CssClass="style7" Text=""></asp:Label>
                                </td>
                            </tr>
                            
                        </table>
                    </asp:Panel>
                </td>
                <td>
                    &nbsp;</td>
            </tr>                                      
        </table>
         <asp:Label ID="lblAviso2" style="color: #CC0000; font-family: 'Trebuchet MS'; font-size: small; background-color: #FFFFFF;" runat="server" CssClass="style7" BorderStyle="None"></asp:Label>
            <asp:Panel ID="PanelPostular" runat="server" BorderColor="#CCCCCC" 
               BorderStyle="Solid" BorderWidth="1px"  Width="615px" CssClass="panel" >   
               <span class="style5">
                <br />
                        Postular a la oferta laboral seleccionada</span>
                        <hr />                        
              <table style="height: 255px; width: 541px">
              <tr>
                <td class="style6">
                  <span class="hdetalle">De </span>
                </td>
                <td>
                  <asp:Label ID="lblDe" runat="server" CssClass="style7" BorderStyle="None"></asp:Label>
                </td>
              </tr>
             
              <tr>
                <td class="style6">
                  <span class="hdetalle">Para </span>
                </td>
                <td>
                  <asp:Label ID="lblPara" runat="server" CssClass="style7" Text=""></asp:Label>
                    <asp:HiddenField ID="codigo_ofeMail" runat="server" />
                </td>
              </tr>
               <tr>
                <td class="style6">
                  <span class="hdetalle">Con Copia a </span>
                </td>
                <td>
                  <asp:Label ID="lblCC" runat="server" CssClass="style7" BorderStyle="None"></asp:Label>
                </td>
              </tr>
               <tr>
                <td class="style6">
                  <span class="hdetalle">Responder a </span>
                </td>
                <td>
                  <asp:Label ID="lblResponderA" runat="server" CssClass="style7" BorderStyle="None"></asp:Label>
                </td>
              </tr>
              <tr>
                <td class="style6">
                  <span class="hdetalle">Asunto </span>
                </td>
                <td>
                  <asp:Label ID="lblAsunto" runat="server" CssClass="style7" Text=""></asp:Label>
                </td>
              </tr>
              <tr>
                <td class="style6">
                  <span class="hdetalle">Mensaje </span>
                </td>
                <td>
                  
                  <asp:TextBox ID="txtMensaje" TextMode="MultiLine" runat="server" Height="65px" 
                        Width="325px"></asp:TextBox>
                </td>
              </tr>
             
              <tr>
                <td class="style6">
                  <span class="hdetalle">CV Adjunto </span>
                </td>
                <td>
                  <a runat="server" id="enlaceAdjunto" class="style6" href="#"><asp:Label ID="lblNombreCV" runat="server" CssClass="style7" Text="CV-"></asp:Label> </a>
                    <asp:HiddenField ID="RutaAdjunto" runat="server" />
                </td>
              </tr>
               <tr>
              <td colspan="2">
                  <asp:Label ID="lblAviso" runat="server" 
                      
                      
                      style="color: #CC0000; font-family: 'Trebuchet MS'; font-size: small; background-color: #FFFFFF;"></asp:Label>
                      <br />
              </td>
              </tr>
              <tr style=" text-align:center">
                <td colspan="2">
                    <br />
                    <asp:Button ID="btnEnviar" runat="server" Text="Postular" /></td>
              </tr>                 
              </table>                                      
              </asp:Panel>    
    </div>
    </form>
</body>
</html>
