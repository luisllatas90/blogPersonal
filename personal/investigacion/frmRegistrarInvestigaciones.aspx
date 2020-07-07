<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmRegistrarInvestigaciones.aspx.vb" Inherits="investigacion_frmRegistrarInvestigaciones" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Registro de Investigaciones</title>
       <link href="../css/estilo.css" rel="stylesheet" type="text/css" media="screen" />
        
        <script src="../../private/funciones.js" type ="text/javascript" language ="javascript"></script>
        <script src="../../private/PopCalendar.js" language="javascript" type="text/javascript"></script>
    
        <script src="../../private/jq/jquery.js" type="text/javascript"></script>
        <script src="../../private/jq/jquery.mascara.js" type="text/javascript"></script>
        
        <script type="text/javascript" language="javascript">
        function PintarFilaElegida(obj) {
            if (obj.style.backgroundColor == "white") {
                obj.style.backgroundColor = "#E6E6FA"//#395ACC
            }
            else {
                obj.style.backgroundColor = "white"
            }
        }
        function cmdNuevo_onclick() {

        }
    </script>
        <script language="javascript" type="text/javascript">
             $(document).ready(function() {
                 jQuery(function($) {
                 $("#txtFechaInicio").mask("99/99/9999");
                 });
             })

             $(document).ready(function() {
                 jQuery(function($) {
                    $("#txtFechaFin").mask("99/99/9999");
                 });
             })
    
         </script>
        <script type="text/javascript">
            
             var contenido_textarea = ""
             var num_caracteres_permitidos = 2000

             function valida_longitud() {
                 num_caracteres = document.forms[0].txtResumen.value.length
                 

                 if (num_caracteres > num_caracteres_permitidos) {
                     document.forms[0].txtResumen.value = contenido_textarea
                 } else {
                 contenido_textarea = document.forms[0].txtResumen.value
                 }

                 if (num_caracteres >= num_caracteres_permitidos) {
                    document.forms[0].txtCaracteres.style.color = "#ff0000";
                 } else {
                    document.forms[0].txtCaracteres.style.color = "#000000";
                 }

                 cuenta()
             }
             function cuenta() {
                 document.forms[0].txtCaracteres.value = document.forms[0].txtResumen.value.length
             }
    </script> 
        <script type="text/javascript">

            var contenido_textarea = ""
            var num_caracteres_permitidos = 2000

            function valida_longitudBene() {
                num_caracteres = document.forms[0].txtBeneficiarios.value.length


                if (num_caracteres > num_caracteres_permitidos) {
                    document.forms[0].txtBeneficiarios.value = contenido_textarea
                } else {
                contenido_textarea = document.forms[0].txtBeneficiarios.value
                }

                if (num_caracteres >= num_caracteres_permitidos) {
                    document.forms[0].txtCaracteresBene.style.color = "#ff0000";
                } else {
                    document.forms[0].txtCaracteresBene.style.color = "#000000";
                }

                cuentaBene()
            }
            function cuentaBene() {
                document.forms[0].txtCaracteresBene.value = document.forms[0].txtBeneficiarios.value.length
            }
    </script> 
         
</head>
<body>


    <form id="form1" runat="server">
    
        <% Response.Write(ClsFunciones.CargaCalendario())%>
    
    <div>
         <table cellpadding="3" cellspacing="0" style="border: 1px solid #C2CFF1; width:100%">
          <tr>
                <td bgcolor="#D1DDEF" colspan="4" height="30px">
                <b>
                    <asp:Label ID="Label4" runat="server" Text="Registro de Investigaciones"></asp:Label></b></td>
          </tr>
            <tr>
                <td width="20%">
                    <asp:Label ID="Label1" runat="server" Text="Título"></asp:Label>
                </td>
                <td width="80%" colspan="3">
                    <asp:TextBox ID="txtTitulo" runat="server" Width="98%" MaxLength="350" ></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td width="20%">
                    <asp:Label ID="Label2" runat="server" Text="Fecha Inicio"></asp:Label>
                </td>
                <td width="30%">
                        <asp:TextBox ID="txtFechaInicio" runat="server" Width="150px" 
                            ValidationGroup="Subasta" TabIndex="1"></asp:TextBox>
                        <input 
                                id="btnFechaInicio" 
                                onClick="PopCalendar.selectWeekendHoliday(1,1);PopCalendar.show(document.form1.txtFechaInicio,'dd/mm/yyyy')" class="cunia" type="button" />
                                <asp:RequiredFieldValidator 
                                        ID="rfvFechaInicio" 
                                        runat="server" 
                                        ControlToValidate="txtFechaInicio" 
                                        ErrorMessage="Debe de ingresar la Fecha de Inicio" 
                                        ValidationGroup="Enviar">*
                                </asp:RequiredFieldValidator>
                      </td>
                    <td width="20%" align="right" >
                        <asp:Label ID="Label3" runat="server" Text="Fecha Fin"></asp:Label>
                    </td>
                <td width="30%" align="left">
                                 <asp:TextBox ID="txtFechaFin" runat="server" Width="150px" 
                            ValidationGroup="Subasta" TabIndex="1"></asp:TextBox>
                        <input 
                                id="btnFechaFin" 
                                onClick="PopCalendar.selectWeekendHoliday(1,1);PopCalendar.show(document.form1.txtFechaFin,'dd/mm/yyyy')" class="cunia" type="button" />
                                <asp:RequiredFieldValidator 
                                        ID="RequiredFieldValidator1" 
                                        runat="server" 
                                        ControlToValidate="txtFechaFin" 
                                        ErrorMessage="Debe de ingresar la Fecha de Fin" 
                                        ValidationGroup="Enviar">*
                                        </asp:RequiredFieldValidator>
                </td>
            </tr>
             <tr>
                <td>
                    
                    <asp:Label ID="Label5" runat="server" Text="Presupuesto"></asp:Label>
                    
                </td>
                <td>
                    <asp:TextBox ID="txtPresupuesto" runat="server" TabIndex="3" ></asp:TextBox>
                </td>
                <td align="right">
                    
                    <asp:Label ID="Label6" runat="server" Text="Tipo Financiamiento"></asp:Label>
                    
                </td>
                <td align="left">
                    <asp:DropDownList ID="ddlTipoFinanciamiento" Width="96%" runat="server" 
                        TabIndex="4">
                        <asp:ListItem Value="0">--Seleccione--</asp:ListItem>
                        <asp:ListItem Value="P">Propio</asp:ListItem>
                        <asp:ListItem Value="E">Externo</asp:ListItem>
                        <asp:ListItem Value="U">USAT</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
             <tr>
                <td>
                    <table>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="Label7" runat="server" Text="Beneficiarios"></asp:Label>            
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label20" runat="server" Text="Max 2000 Caracteres" 
                                    Font-Size="XX-Small" ForeColor="Red"></asp:Label>
                            </td>
                            <td>    
                                <asp:TextBox ID="txtCaracteresBene" runat="server" Width="35px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                 </td>
                <td colspan="3">
                    <asp:TextBox 
                        ID="txtBeneficiarios" 
                        Width="98%" 
                        runat="server" 
                        Height="76px" 
                        onKeyUp="valida_longitudBene()"
                        onKeyDown="valida_longitudBene()" 
                        TextMode="MultiLine" 
                        TabIndex="5" 
                        MaxLength="2000" ></asp:TextBox>
                </td>
            </tr>
             <tr>
                <td>
                    <table>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="Label8" runat="server" Text="Resumen"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label18" runat="server" Text="Max 2000 Caracteres" 
                                    Font-Size="XX-Small" ForeColor="Red"></asp:Label>
                            </td>
                            <td>    
                                <asp:TextBox ID="txtCaracteres" runat="server" Width="35px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                 </td>
                <td colspan="3">
                    <asp:TextBox ID="txtResumen" 
                                 runat="server" 
                                 Height="76px" 
                                 Width="98%" 
                                 onKeyUp="valida_longitud()"
                                 onKeyDown="valida_longitud()" 
                                 TextMode="MultiLine" 
                                 TabIndex="6"  ></asp:TextBox>
                </td>
                
                
            </tr>
             <tr>
                <td>
                     <asp:Label ID="Label12" runat="server" Text="Etapa Investigación"></asp:Label>
                 </td>
                <td>
                     <asp:DropDownList ID="ddlEtapaInv" Width="98%" runat="server" 
                         Font-Size="Smaller" TabIndex="7">
                     </asp:DropDownList>
                </td>
                <td colspan="2" rowspan="8" valign="top">
                    <table cellpadding="3" cellspacing="0" style="border: 1px solid #C2CFF1; width:100%">
                        <tr>
                            <td width="25%">
                                <asp:CheckBox ID="chkTipoPersonal" runat="server" ForeColor="#3333FF" 
                                    Text="Personal" AutoPostBack="true" Checked="True" />
                            </td>
                            <td colspan="2">
                                <asp:DropDownList ID="ddlTipoPersonal" Width="98%" runat="server" 
                                    BackColor="#CCFFCC" Font-Size="Smaller" TabIndex="15">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label17" runat="server" Text="Tipo Participación"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlTipoParticipacion" Width="98%" runat="server" 
                                    Font-Size="Smaller" TabIndex="14">
                                </asp:DropDownList>
                            </td>
                            <td width="5%">
                                <asp:Button ID="btnAgregar" runat="server" BackColor="#EFEFEF" 
                                    BorderColor="#0099FF" BorderStyle="Ridge" Text="+" Height="20px" 
                                    TabIndex="16" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                            
                                <asp:GridView ID="gvListaResponsables" Width="100%" runat="server" BackColor="White" 
                                    BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                                    AutoGenerateColumns="False" 
                                    DataKeyNames="codigo_per,id_participacion,tipo_per" 
                                    PageSize="3">
                                    <RowStyle ForeColor="#000066" />
                                    <Columns>
                                        <asp:BoundField DataField="codigo_per" HeaderText="ID" />
                                        <asp:BoundField DataField="nombre" HeaderText="Participante" />
                                        <asp:BoundField DataField="id_participacion" HeaderText="id_participacion" 
                                            Visible="False" />
                                        <asp:BoundField DataField="nombre_participacion" 
                                            HeaderText="T.Participación" />
                                        <asp:BoundField DataField="tipo_per" HeaderText="Tipo" />
                                         <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="ibtnEliminar" runat="server" 
                                                            ImageUrl="~/Images/edit_remove.png" onclick="ibtnEliminar_Click" />
                                                    </ItemTemplate>
                                                    <ItemStyle Width="1%" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle BackColor="White" ForeColor="#000066" />
                                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                </asp:GridView>
                            
                            </td>
                        </tr>
                    </table>
                 </td>
            </tr>
             <tr>
                <td>
                     <asp:Label ID="Label16" runat="server" Text="Ambito Investigación"></asp:Label>
                 </td>
                <td>
                     <asp:DropDownList ID="ddlAmbitoInv" Width="98%" runat="server" 
                         Font-Size="Smaller" TabIndex="8">
                         <asp:ListItem Value="0">--SELECCIONE ÁMBITO--</asp:ListItem>
                         <asp:ListItem Value="I">INTERNACIONAL</asp:ListItem>
                         <asp:ListItem Value="N">NACIONAL</asp:ListItem>
                         <asp:ListItem Value="L">LOCAL</asp:ListItem>
                     </asp:DropDownList>
                 </td>
            </tr>
             <tr>
                <td>
                    <asp:Label ID="Label10" runat="server" Text="Comités"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlcomites" Width="98%" runat="server" 
                        Font-Size="Smaller" TabIndex="9" AutoPostBack="True" BackColor="#FFFFCC">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label13" runat="server" Text="Áreas"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlAreas" Width="98%" runat="server" 
                        Font-Size="Smaller" TabIndex="10" AutoPostBack="True" BackColor="#FFFFCC">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label11" runat="server" Text="Linea Investigación"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlLineaInv" Width="98%" runat="server" 
                        Font-Size="Smaller" TabIndex="11" BackColor="#FFFFCC">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                     <asp:Label ID="Label14" runat="server" Text="Tipo de Investigación"></asp:Label>
                 </td>
                <td>
                     <asp:DropDownList ID="ddlTipoInv" Width="98%" runat="server" 
                         Font-Size="Smaller" TabIndex="12">
                     </asp:DropDownList>
                 </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label15" runat="server" Text="Instancia Investigación"></asp:Label>
                 </td>
                <td>
                    <asp:DropDownList ID="ddlInstanciaInv" Width="98%" runat="server" 
                        Font-Size="Smaller" TabIndex="13">
                    </asp:DropDownList>
                 </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label9" runat="server" Text="Seleccione el Proyecto"></asp:Label>
                 </td>
                <td>
                    <asp:FileUpload ID="FileArchivo" runat="server" BorderStyle="Solid" 
                        BorderWidth="1px" Width="98%" TabIndex="17" />
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                                            <asp:GridView 
                                                        ID="gvDocumentos" 
                                                        runat="server" 
                                                        AutoGenerateColumns="False" 
                                                        BorderColor="#99BAE2" BorderStyle="Solid" 
                                                        BorderWidth="1px" 
                                                        CellPadding="4" 
                                                        ForeColor="#333333" 
                                                        GridLines="Horizontal" 
                                                        Width="95%">
                                                <Columns>
                                                    <asp:ImageField 
                                                        ConvertEmptyStringToNull="False" 
                                                        DataImageUrlField="extension" 
                                                        DataImageUrlFormatString="~/images/ext/{0}.gif" HeaderText="">
                                                        <HeaderStyle Width="1%" />
                                                        <ItemStyle HorizontalAlign="Center" Width="20px" /></asp:ImageField>
                                                    <asp:HyperLinkField DataNavigateUrlFields="documento" 
                                                        DataNavigateUrlFormatString="{0}" DataTextField="nombre" HeaderText="Documento" 
                                                        Target="_blank" >
                                                        <ItemStyle HorizontalAlign="Center" /></asp:HyperLinkField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:HiddenField 
                                                                ID="hfRuta" runat="server" 
                                                                 />
                                                            </ItemTemplate>
                                                            <ItemStyle Width="1%" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EmptyDataTemplate>
                                                    <table style="width:100%;">
                                                        <tr>
                                                            <td style="width:90%">
                                                                La investigación no tiene ningún Documento.</td>
                                                            <td style="width:10%">
                                                                <asp:Image ID="imgNingun" runat="server" ImageUrl="~/Images/cerrar.gif" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </EmptyDataTemplate>
                                                <HeaderStyle BackColor="#e8eef7" BorderColor="#99BAE2" BorderStyle="Solid" 
                                                    BorderWidth="1px" ForeColor="#3366CC" />
                                                <EditRowStyle BackColor="#999999" />
                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                            </asp:GridView>
                </td>
            </tr>
        </table>
         <table cellpadding="3" cellspacing="0" style="border: 1px solid #C2CFF1; width:100%">
            <tr>
                <td colspan="4" height="30px">
                    <b>
                        <asp:Button 
                                ID="cmdGuardar" 
                                runat="server" 
                                Text="        Guardar" 
                                CssClass="guardar_prp" 
                                Height="35px" 
                                Width="100px" 
                                ToolTip="Guardar Investigaciones" 
                                TabIndex="18" />
                        <asp:Button 
                                ID="cmdRegresar" 
                                runat="server" 
                                Text="            Regresar" 
                                CssClass="enviarpropuesta" 
                                Height="35px" 
                                Width="100px" 
                                ToolTip="Regresar a Gestion de Investigaciones" TabIndex="19" 
                                />
                    </b>
                </td>
                <td>
                       
                                  
                </td>
            </tr>
         </table>
    </div>
    </form>
</body>
</html>
