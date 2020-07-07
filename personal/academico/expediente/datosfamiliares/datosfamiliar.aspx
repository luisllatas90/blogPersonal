<%@ Page Language="VB" ValidateRequest="false" AutoEventWireup="false" CodeFile="datosfamiliar.aspx.vb" Inherits="datosfamiliar" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Página sin título</title>
    <style type="text/css">
        .style2
        {
            width: 723px;
        }
        .style4
        {
        }
        .style5
        {
            width: 255px;
        }
        .style6
        {
            width: 62px;
        }
        .style7
        {
            height: 33px;
        }
        .style9
        {
            width: 100%;
        }
        .style10
        {
            width: 69px;
        }
        .style11
        {
        }
        .style12
        {
            border: 1px solid #999999;
            height: 34px;
        }
        .style13
        {
            height: 32px;
        }
        .style15
        {
            height: 28px;
        }
        .style16
        {
            height: 35px;
        }
        .style17
        {}
        </style>
    
    <link href="../../../css/estilo.css" rel="stylesheet" type="text/css" />
    <link href="private/estilos.css" rel="stylesheet"  type="text/css" /> 
    <script src="../../../../private/funciones.js" type ="text/javascript" language ="javascript"></script>
    <script src="private/PopCalendar.js" language="javascript" type="text/javascript">function cmdFechaMat_onclick() {
}
</script>
<script type="text/javascript">
function OcultarBotones(obj){
    if (obj.value ==2 ){
        document.all.txtfechaMat.style.disabled=true
    }else{
        document.all.txtfechaMat.style.disabled=false
    }
}
</script>

    <script src="../../../../private/jq/jquery.js" type="text/javascript"></script>
    <script src="../../../../private/jq/jquery.mascara.js" type="text/javascript"></script>
    
 <script language="javascript" type="text/javascript">
     $(document).ready(function() {
         jQuery(function($) {
             $("#txtFecha").mask("99/99/9999");
         });
     })

     $(document).ready(function() {
         jQuery(function($) {
             $("#txtFechaMat").mask("99/99/9999");
         });
     })
    
  </script>

</head>
<body>
    <form id="frmsolicitud" runat="server">
    <%  response.write(clsfunciones.cargacalendario) %><br />
    
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanelGeneral" runat="server">
            <ContentTemplate>
                
                
        <table class="contornotabla" align="center" cellpadding="0" cellspacing="0" 
            width="90%">
            <tr>
                <td class="style7" bgcolor="#EFF3FB">
                    <SPAN class="e1">&nbsp;&nbsp;
                    <asp:Label ID="lblTipo" runat="server"></asp:Label>
                    &nbsp;DE DATOS DEL FAMILIAR </SPAN></td>
            </tr>
            <tr>
                <td bgcolor="#999999" height="1">
                    </td>
            </tr>
            <tr>
                <td bgcolor="#FFFFCC" class="style8" valign="top">
                    <table class="style9">
                        <tr>
                            <td class="style10" valign="top">
                                <asp:Label ID="Label1" runat="server" Font-Bold="True" ForeColor="#990000" 
                                    Text="Instrucciones:"></asp:Label>
                            </td>
                            <td>
                                . Llene los datos requeridos, todos los campos son obligatorios.<br />
                                . En caso de registrar al cónyuge también deberá incluir la fecha de matrimonio.<br />
                                . Para Guardar los datos haga clic en el botón
                                <asp:Label ID="Label4" runat="server" Font-Underline="True" ForeColor="Blue" 
                                    Text="Aceptar"></asp:Label>
                                , caso contrario haga clic en el botón Cancelar.</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td bgcolor="#A0A3A7" height="1">
                    </td>
            </tr>
            <tr>
                <td  align=center>
                    <table >
                        <tr>
                            <td class="style16" align="left" colspan="2" style="color: #FF0000">
                                Recuerde que debe realizar el registro de su cónyuge e hijos:</td>
                        </tr>
                        <tr>
                            <td class="style15" align="left">
                                <asp:RadioButtonList ID="rblVinculo" runat="server" AutoPostBack="True" 
                                    Font-Bold="True" Font-Size="16pt" ForeColor="#990000" 
                                    RepeatDirection="Horizontal" Height="16px" Width="235px">
                                    <asp:ListItem Value="2">Conyuge</asp:ListItem>
                                    <asp:ListItem Value="5">Hijo (a)</asp:ListItem>
                                    <asp:ListItem Value="1">Tutorado</asp:ListItem>
                                </asp:RadioButtonList>
                                
                            </td>
                            <td align="right" class="style15" >
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                                    ControlToValidate="rblVinculo" 
                                    ErrorMessage="Seleccione si registra a su CONYUGE o HIJO(A)">*
                                </asp:RequiredFieldValidator>
                                <asp:Label ID="lblVinculo" runat="server" Font-Bold="True" Font-Size="16pt" 
                                    ForeColor="#990000" Text="Vínculo Familiar"></asp:Label>
                            </td>
                        </tr>
                        
                        
                        <tr>
                            <td class="style11" colspan="4">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="style12" bgcolor="#FFFF99" colspan="4" style="color: #0000FF" 
                                valign="middle">
                                <table class="style9">
                                    <tr>
                                        <td class="style13" valign="middle">
                                <asp:Image ID="Image1" runat="server" ImageAlign="AbsMiddle" 
                                    ImageUrl="~/images/credito.gif" />
    &nbsp; Si el familiar que va a registrar pertenece a la familia Usat búscalo aquí:
                                            <asp:LinkButton ID="lnkEstudiante" runat="server" Font-Underline="True" 
                                                ForeColor="#990000" ValidationGroup="x">Estudiante</asp:LinkButton>
    &nbsp;o
                                            <asp:LinkButton ID="lnkPersonal" runat="server" Font-Underline="True" 
                                                ForeColor="#990000" ValidationGroup="x">Personal</asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table class="style9">
                                                <tr>
                                                    <td width="50%">
                    <asp:Panel ID="PanelEstudiante" runat="server" Height="155px" Visible="False" Width="100%">
                        <br />
                        <table class="style9">
                            <tr>
                                <td align="left">
                                    <asp:Image ID="Image2" runat="server" ImageAlign="AbsMiddle" 
                                        ImageUrl="~/images/der.gif" />
                                    &nbsp;Consulte y seleccione el Familiar (&nbsp;<asp:LinkButton ID="lnkEstudiante0" 
                                        runat="server" Font-Bold="True" Font-Underline="True" ForeColor="#990000" 
                                        ValidationGroup="x">ESTUDIANTE</asp:LinkButton>
                                    &nbsp;) de la lista</td>
                            </tr>
                            <tr>
                                <td valign="middle">
                                    <table class="style9">
                                        <tr>
                                            <td class="style17" style="color: #0000FF; font-weight: bold">
                                                Estudiante:&nbsp;
                                                <asp:TextBox ID="txtAlumno" runat="server" Width="200px"></asp:TextBox>
                                                &nbsp;&nbsp;
                                                <asp:Button ID="cmdBuscarPersonal" runat="server" Text="Consultar" 
                                                    ValidationGroup="x" />
                                            </td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="dgvAlumnos" runat="server" AllowPaging="True" 
                                        AllowSorting="True" AutoGenerateColumns="False" BackColor="White" 
                                        DataKeyNames="Cod" DataSourceID="SqlDataSource1" GridLines="Horizontal" 
                                        PageSize="15" Width="100%">
                                        <FooterStyle BackColor="#99CCFF" />
                                        <Columns>
                                            <asp:BoundField DataField="Cod" InsertVisible="False" ReadOnly="True" 
                                                SortExpression="Cod" Visible="False">
                                                <HeaderStyle Font-Size="X-Small" />
                                                <ItemStyle ForeColor="White" Width="1px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Paterno" HeaderText="Paterno" 
                                                SortExpression="Paterno">
                                                <HeaderStyle Font-Size="10pt" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Materno" HeaderText="Materno" 
                                                SortExpression="Materno">
                                                <HeaderStyle Font-Size="10pt" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Nombres" HeaderText="Nombres" 
                                                SortExpression="Nombres">
                                                <HeaderStyle Font-Size="10pt" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Sexo" HeaderText="Sexo" SortExpression="Materno">
                                                <HeaderStyle Height="10px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="FechaNac" HeaderText="Fecha Nac." ReadOnly="True" 
                                                SortExpression="Cod" />
                                            <asp:CommandField SelectText="" ShowSelectButton="True" />
                                        </Columns>
                                        <PagerStyle BackColor="#99CCFF" />
                                        <SelectedRowStyle BackColor="#99CCFF" />
                                        <HeaderStyle BackColor="#99CCFF" Font-Size="9pt" />
                                    </asp:GridView>
                                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                                        ConnectionString="<%$ ConnectionStrings:CNXBDUSAT %>" 
                                        SelectCommand="FAM_ConsultarFamiliaUSAT" SelectCommandType="StoredProcedure">
                                        <SelectParameters>
                                            <asp:Parameter DefaultValue="AL" Name="tipo" Type="String" />
                                            <asp:Parameter DefaultValue="%" Name="param" Type="String" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel ID="PanelPersonal" runat="server" Height="155px" Visible="False" Width="100%">
                        &nbsp;&nbsp;<table class="style9">
                            <tr>
                                <td>
                                    <asp:Image ID="Image4" runat="server" ImageAlign="AbsMiddle" 
                                        ImageUrl="~/images/der.gif" />
                                    &nbsp;Consulte y seleccione el Familiar (&nbsp;<asp:LinkButton ID="lnkPersonal0" 
                                        runat="server" Font-Bold="True" Font-Underline="True" ForeColor="#990000" 
                                        ValidationGroup="x">PERSONAL</asp:LinkButton>
                                    &nbsp;) de la lista</td>
                            </tr>
                            <tr>
                                <td valign="middle">
                                    <table class="style9">
                                        <tr>
                                            <td class="style17" style="font-weight: bold; color: #0000FF">
                                                Personal:&nbsp;
                                                <asp:TextBox ID="txtpersonal" runat="server" Width="200px"></asp:TextBox>
                                                &nbsp;&nbsp;
                                                <asp:Button ID="cmdBuscarPersonal0" runat="server" Text="Consultar" 
                                                    ValidationGroup="x" />
                                            </td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="dgvPersonal" runat="server" AllowPaging="True" 
                                        AllowSorting="True" AutoGenerateColumns="False" BackColor="White" 
                                        DataKeyNames="Cod" DataSourceID="SqlDataSource2" GridLines="Horizontal" 
                                        PageSize="15" Width="100%">
                                        <FooterStyle BackColor="#99CCFF" />
                                        <Columns>
                                            <asp:BoundField DataField="Cod" InsertVisible="False" ReadOnly="True" 
                                                SortExpression="Cod" Visible="False">
                                                <HeaderStyle Font-Size="X-Small" />
                                                <ItemStyle ForeColor="White" Width="1px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Paterno" HeaderText="Paterno" 
                                                SortExpression="Paterno">
                                                <HeaderStyle Font-Size="10pt" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Materno" HeaderText="Materno" 
                                                SortExpression="Materno">
                                                <HeaderStyle Font-Size="10pt" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Nombres" HeaderText="Nombres" 
                                                SortExpression="Nombres">
                                                <HeaderStyle Font-Size="10pt" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Sexo" HeaderText="Sexo" />
                                            <asp:BoundField DataField="FechaNac" HeaderText="Fecha Nac." />
                                            <asp:CommandField SelectText="" ShowSelectButton="True" />
                                        </Columns>
                                        <PagerStyle BackColor="#99CCFF" />
                                        <SelectedRowStyle BackColor="#99CCFF" />
                                        <HeaderStyle BackColor="#99CCFF" Font-Size="9pt" />
                                    </asp:GridView>
                                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                                        ConnectionString="<%$ ConnectionStrings:CNXBDUSAT %>" 
                                        SelectCommand="FAM_ConsultarFamiliaUSAT" SelectCommandType="StoredProcedure">
                                        <SelectParameters>
                                            <asp:Parameter DefaultValue="PE" Name="tipo" Type="String" />
                                            <asp:Parameter DefaultValue="%" Name="param" Type="String" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="style11" colspan="2" align="left">
                                <asp:CheckBox ID="chkUsat" runat="server" AutoPostBack="True" 
                                    Font-Size="X-Small" ForeColor="#990000" ValidationGroup="x" Visible="False" />
                            </td>
                            <td class="style6">
                                &nbsp;</td>
                            <td style="font-size: large; color: #0000FF">
                                <asp:TextBox ID="txtCodigo" runat="server" Width="176px" Visible="False"></asp:TextBox>
                            </td>
                        </tr>
                         <tr>
                                    <td colspan="4">
                                        <hr style="border-right: darkred 1px solid; border-top: darkred 1px solid; border-left: darkred 1px solid; border-bottom: darkred 1px solid; height: 1px" />
                                    </td>
                             </tr>
                             <tr>
                                <td colspan="4" bgcolor="#FFFF99" align="left">
                                    <asp:Label ID="Label6" runat="server" ForeColor="#990000" Text="Datos"></asp:Label>
                                 </td>
                             </tr>
                        <tr>
                            <td class="style11" align="left">
                                Apellido Paterno</td>
                            <td class="style5" align="left">
                                <asp:TextBox ID="txtPaterno" runat="server" Width="238px"></asp:TextBox>
                                <asp:RequiredFieldValidator 
                                    ID="RequiredFieldValidator1" 
                                    runat="server" 
                                    ControlToValidate="txtPaterno" 
                                    ErrorMessage="Ingrese Apellido Paterno">*
                                </asp:RequiredFieldValidator>
                            </td>
                            <td class="style6" align="left">
                                Apellido Materno</td>
                            <td align="left">
                                <asp:TextBox ID="txtMaterno" runat="server" Width="200px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                    ControlToValidate="txtMaterno" ErrorMessage="Ingrese Apellido Materno">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="style11" align="left">
                                Nombres</td>
                            <td class="style5" align="left">
                                <asp:TextBox ID="txtNombres" runat="server" Width="238px"></asp:TextBox>
                                <asp:RequiredFieldValidator 
                                    ID="RequiredFieldValidator3" 
                                    runat="server" 
                                    ControlToValidate="txtNombres" 
                                    ErrorMessage="Ingrese Nombres">*
                                </asp:RequiredFieldValidator>
                            </td>
                            <td class="style6" align="left">
                                Sexo</td>
                            <td align="left">
                                <asp:DropDownList ID="ddlSexo" runat="server" 
                                    Font-Size="Smaller">
                                    <asp:ListItem Selected="True" Value="0">Seleccionar</asp:ListItem>
                                    <asp:ListItem Value="1">Masculino</asp:ListItem>
                                    <asp:ListItem Value="2">Femenino</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RangeValidator 
                                    ID="RangeValidator2" 
                                    runat="server" 
                                    ControlToValidate="ddlSexo" 
                                    ErrorMessage="Seleccione un sexo" 
                                    MaximumValue="100" 
                                    MinimumValue="1" 
                                    Type="Integer">*</asp:RangeValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="style11" align="left">
                                Fecha Nacimiento</td>
                            <td class="style5" align="left">
                                    <asp:TextBox ID="txtfecha" runat="server" Height="22px" Width="80px"></asp:TextBox>
                                    <input id="Button1" type="button"  
                                    
                                        onclick="PopCalendar.selectWeekendHoliday(1,1);PopCalendar.show(document.frmsolicitud.txtfecha,'dd/mm/yyyy')" 
                                        style="height: 22px" value="..." /> 
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                                        ControlToValidate="txtfecha" ErrorMessage="Ingrese Fecha de Nacimiento">*</asp:RequiredFieldValidator>
                            </td>
                            <td class="style6" align="left">
                                Estudios</td>
                            <td align="left">
                                <asp:DropDownList ID="ddlEstudios" runat="server" 
                                    Font-Size="Smaller">
                                    <asp:ListItem Selected="True" Value="0">Seleccionar</asp:ListItem>
                                    <asp:ListItem Value="I">Inicial</asp:ListItem>
                                    <asp:ListItem Value="P">Primaria</asp:ListItem>
                                    <asp:ListItem Value="S">Secundaria</asp:ListItem>
                                    <asp:ListItem Value="U">Superior</asp:ListItem>
                                    <asp:ListItem Value="N">Ninguno</asp:ListItem>
                                </asp:DropDownList>
                                <asp:CompareValidator 
                                    ID="CompareValidator1" 
                                    runat="server" 
                                    ControlToValidate="ddlEstudios" 
                                    ErrorMessage="Ingrese Nivel de estudios" 
                                    Operator="NotEqual" 
                                    ValueToCompare="0">*
                               </asp:CompareValidator>
                              </td>
                        </tr>
                        <tr>
                            <td class="style11" align="left">
                                <asp:Label ID="lblFechaMat" runat="server" Text="Fecha Matrimonio"></asp:Label>
                            </td>
                            <td class="style5" align="left">
                                    <asp:TextBox ID="txtfechaMat" runat="server" Height="22px" Width="80px"></asp:TextBox>
                                <input runat="server"  id="cmdFechaMat" type="button"  
                                    
                                    onclick="PopCalendar.selectWeekendHoliday(1,1);PopCalendar.show(document.frmsolicitud.txtfechaMat,'dd/mm/yyyy')" 
                                    style="height: 22px" value="..." /> 
                                    </td>
                            <td class="style6">
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="style11" align="left">
                                Documento de Identidad</td>
                            <td class="style5" align="left">
                                    <asp:DropDownList ID="ddlTipoDocumento"  Width="238px" runat="server" 
                                        Font-Size="Smaller" AutoPostBack="True">
                                    </asp:DropDownList>
                                     <asp:CompareValidator 
                                        ID="CompareValidator2" 
                                        runat="server" 
                                        ControlToValidate="ddlTipoDocumento" 
                                        ErrorMessage="Seleccione Tipo Documento Identidad." 
                                        Operator="NotEqual" 
                                        ValueToCompare="0">*
                                    </asp:CompareValidator>
                            </td>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                

                            <td class="style6" align="left">
                                Número
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtDocumuento" Width="90%" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator 
                                    ID="RequiredFieldValidator6" 
                                    runat="server" 
                                    ControlToValidate="txtDocumuento" 
                                    ErrorMessage="Ingrese Número de documento">*
                                </asp:RequiredFieldValidator>
                            </td>
                            </tr>
                            <tr>
                                <td align="left">País Emisor</td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlPaisEmisor" Width="238px" runat="server" Font-Size="Smaller">
                                    </asp:DropDownList>
                                </td>
                                <td align="left">Mes Concepción (mm/aaaa)</td>
                                <td align="left">
                                    <asp:TextBox ID="txtmesconcepcion" Width="90%" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            
                               </ContentTemplate>
                            </asp:UpdatePanel>
                            <tr>
                                <td align="left">Fecha Alta</td>
                                <td class="style5" align="left">
                                    <asp:TextBox ID="txtFechaAlta" runat="server" Height="22px" Width="80px"></asp:TextBox>
                                    <input runat="server"  id="Button2" type="button"  
                                    
                                    onclick="PopCalendar.selectWeekendHoliday(1,1);PopCalendar.show(document.frmsolicitud.txtFechaAlta,'dd/mm/yyyy')" 
                                    style="height: 22px" value="..." /> 
                                </td>
                                <td align="left">Email</td>
                                <td align="left">
                                    <asp:TextBox ID="txtemail" Width="90%" runat="server"></asp:TextBox>
                                    <asp:RegularExpressionValidator   
                                    ID="RegularExpressionValidator1"  
                                    runat="server"   
                                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"  
                                    ControlToValidate="txtemail"  
                                    ErrorMessage="Ingrese un email correcto.">*  
                                </asp:RegularExpressionValidator>  
                                </td>
                            </tr>
                            <tr>
                                <td align="left">Código Teléfono</td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlCodigoLargaDistancia" Width="238px" runat="server" 
                                        Font-Size="Smaller" AutoPostBack="True">
                                    </asp:DropDownList>
                                </td>
                                <td align="left">Número Teléfono</td>
                                <td align="left">
                                    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txtNumeroFono" Width="90%" runat="server"></asp:TextBox>    
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="ddlCodigoLargaDistancia" 
                                                EventName="SelectedIndexChanged" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                             <tr>
                                    <td colspan="4">
                                        <hr style="border-right: darkred 1px solid; border-top: darkred 1px solid; border-left: darkred 1px solid; border-bottom: darkred 1px solid; height: 1px" />
                                    </td>
                             </tr>
                             <tr>
                                <td colspan="4" bgcolor="#FFFF99" align="left">
                                    <asp:Label ID="Label3" runat="server" ForeColor="#990000" Text="Vínculo Familiar "></asp:Label>
                                 </td>
                             </tr>
                             <tr>
                                <td align="left">Situación</td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlSituacion" Width="238px" runat="server" 
                                        Font-Size="Smaller">
                                        <asp:ListItem Value="0">--Seleccione Situación--</asp:ListItem>
                                        <asp:ListItem Value="10">Activo</asp:ListItem>
                                        <asp:ListItem Value="11">Baja</asp:ListItem>
                                    </asp:DropDownList>
                                 </td>
                                <td align="left">Vínculo Familiar</td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlVinculoFamiliar" Width="90%" runat="server" 
                                        Font-Size="Smaller">
                                    </asp:DropDownList>
                                 </td>
                            </tr>
                             <tr>
                                <td align="left">Doc.Sustenta V.Familiar</td>
                                <td colspan="3" align="left">
                                    <asp:DropDownList ID="ddlDocVinFamiliar" runat="server" Font-Size="Smaller" 
                                        Width="96%">
                                    </asp:DropDownList>
                                 </td>
                            </tr>
                             <tr>
                                <td align="left">Número del documento</td>
                                <td align="left">
                                <asp:TextBox ID="txtNumeroDocumento" runat="server" Width="238px"></asp:TextBox>
                                 </td>
                                <td align="left">Nacionalidad</td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlNacionalidad" Width="90%" runat="server" 
                                        Font-Size="Smaller">
                                    </asp:DropDownList>
                                 </td>
                            </tr>
                            <tr>
                                <td align="left">Indicador de domicilio</td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlIndicadorDomicilio" Width="238px" runat="server" 
                                        Font-Size="Smaller">
                                        <asp:ListItem Value="-1">--Seleccione Indicador --</asp:ListItem>
                                        <asp:ListItem Value="0">DOMICILIO DEL TRABAJADOR</asp:ListItem>
                                        <asp:ListItem Value="1">OTRO DOMICILIO</asp:ListItem>
                                    </asp:DropDownList>
                                 </td>
                                <td align="left">Fecha Baja</td>
                                  <td class="style5" align="left">
                                    <asp:TextBox ID="txtFechaBaja" runat="server" Height="22px" Width="80px"></asp:TextBox>
                                    <input runat="server"  id="Button3" type="button"  
                                    
                                    onclick="PopCalendar.selectWeekendHoliday(1,1);PopCalendar.show(document.frmsolicitud.txtFechaBaja,'dd/mm/yyyy')" 
                                    style="height: 22px" value="..." /> 
                                </td>
                            </tr>
                             <tr>
                                <td align="left">Motivo Baja</td>
                                <td colspan="3" align="left">
                                    <asp:DropDownList ID="ddlMotivoBaja" runat="server" Font-Size="Smaller" 
                                        Width="96%">
                                    </asp:DropDownList>
                                 </td>
                            </tr>
                             <tr>
                                    <td colspan="4">
                                        <hr style="border-right: darkred 1px solid; border-top: darkred 1px solid; border-left: darkred 1px solid; border-bottom: darkred 1px solid; height: 1px" />
                                    </td>
                             </tr>
                             <tr>
                                <td colspan="4" bgcolor="#FFFF99" align="left">
                                    <asp:Label ID="Label5" runat="server" ForeColor="#990000" Text="Dirección Nº 1 "></asp:Label>
                                 </td>
                             </tr>
                             <tr>
                                <td align="left">Tipo Via</td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlTipoVia1" Width="238px" runat="server" 
                                        Font-Size="Smaller">
                                    </asp:DropDownList>
                                 </td>
                                <td align="left">Nombre Via</td>
                                <td align="left">
                                    <asp:TextBox ID="txtNombreVia" Width="90%" runat="server"></asp:TextBox>
                                 </td>
                             </tr>
                             <tr>
                                <td align="left">Número</td>
                                <td align="left">
                                    <asp:TextBox ID="txtNumero1" runat="server"></asp:TextBox>
                                 </td>
                                <td align="left">Interior</td>
                                <td align="left">
                                    <asp:TextBox ID="txtInterior1" runat="server"></asp:TextBox>
                                 </td>
                             </tr>
                             <tr>
                                <td align="left">Tipo Zona</td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlTipoZona1" Width="238px" runat="server" 
                                        Font-Size="Smaller">
                                    </asp:DropDownList>
                                 </td>
                                <td align="left">Nombre Zona</td>
                                <td align="left">
                                    <asp:TextBox ID="txtNombreZona1" Width="90%" runat="server"></asp:TextBox>
                                 </td>
                             </tr>
                             <tr>
                                <td align="left">Referencia</td>
                                <td colspan="3" align="left">
                                    <asp:TextBox ID="txtReferencia1" Width="96%" runat="server"></asp:TextBox>
                                 </td>
                                
                             </tr>
                             <tr>
                                <td align="left">Ubigeo</td>
                                <td>
                                    <table>
                                        <tr>
                                            <td>
                                                 <asp:DropDownList ID="ddlDepartamento1" Width="180px" runat="server" 
                                                 Font-Size="Smaller" Height="20px" AutoPostBack="True">
                                                 </asp:DropDownList>
                                            </td>
                                            <td>
                                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                        <ContentTemplate>
                                                            <asp:DropDownList ID="ddlProvincia1" Width="198px" runat="server" 
                                                                Font-Size="Smaller" Height="16px" AutoPostBack="True">
                                                            </asp:DropDownList>
                                                            
                                                    </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="ddlDepartamento1" 
                                                                EventName="SelectedIndexChanged" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                            </td>
                                        </tr>     
                                    </table>
                                </td>
                                <td colspan="2" align="left">
                                   
                                    
                                    
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddlDistrito1" Width="198px" runat="server" 
                                            Font-Size="Smaller" Height="16px">
                                            </asp:DropDownList>
                                    </ContentTemplate>
                                    </asp:UpdatePanel>
                                    
                                    
                                    
                                    
                                 </td>
                             </tr>
                              <tr>
                                    <td colspan="4">
                                        <hr style="border-right: darkred 1px solid; border-top: darkred 1px solid; border-left: darkred 1px solid; border-bottom: darkred 1px solid; height: 1px" />
                                    </td>
                             </tr>
                             <tr>
                                <td colspan="4" bgcolor="#FFFF99" align="left">
                                    <asp:Label ID="Label2" runat="server" ForeColor="#990000" Text="Dirección Nº 2 "></asp:Label>
                                 </td>
                             </tr>
                               <tr>
                                <td align="left">Tipo Via</td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlTipoVia2" Width="238px" runat="server" 
                                        Font-Size="Smaller">
                                    </asp:DropDownList>
                                 </td>
                                <td align="left">Nombre Via</td>
                                <td align="left">
                                    <asp:TextBox ID="txtNombreVia2" Width="90%" runat="server"></asp:TextBox>
                                 </td>
                             </tr>
                             <tr>
                                <td align="left">Número</td>
                                <td align="left">
                                    <asp:TextBox ID="txtNumero2" runat="server"></asp:TextBox>
                                 </td>
                                <td align="left">Interior</td>
                                <td align="left">
                                    <asp:TextBox ID="txtInterior2" runat="server"></asp:TextBox>
                                 </td>
                             </tr>
                             <tr>
                                <td align="left">Tipo Zona</td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlTipoZona2" Width="238px" runat="server" 
                                        Font-Size="Smaller">
                                    </asp:DropDownList>
                                 </td>
                                <td align="left">Nombre Zona</td>
                                <td align="left">
                                    <asp:TextBox ID="txtNombreZona2" Width="90%" runat="server"></asp:TextBox>
                                 </td>
                             </tr>
                             <tr>
                                <td align="left">Referencia</td>
                                <td colspan="3" align="left">
                                    <asp:TextBox ID="txtReferencia2" Width="96%" runat="server"></asp:TextBox>
                                 </td>
                             </tr>
                             <tr>
                                <td align="left">Ubigeo</td>
                                <td>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:DropDownList ID="ddlDepartamento2" Width="180px" runat="server" 
                                                Font-Size="Smaller" Height="20px" AutoPostBack="True">
                                                </asp:DropDownList>        
                                            </td>
                                            <td>
                                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddlProvincia2" Width="198px" runat="server" 
                                                        Font-Size="Smaller" Height="16px" AutoPostBack="True">
                                                        </asp:DropDownList>
                                                 </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td colspan="2" align="left">
                                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddlDistrito2" Width="198px" runat="server" 
                                            Font-Size="Smaller" Height="16px">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="ddlProvincia2" 
                                                EventName="SelectedIndexChanged" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                 </td>
                             </tr>
                            <tr>
                                <td colspan="4"></td>
                            </tr>
                       
                        <tr>
                            <td class="style4" align="center" colspan="4">
                                <asp:Button ID="cmdAceptar" runat="server" Text="Aceptar" 
                                    CssClass="cmdprocesarXLS" />
    &nbsp;&nbsp;&nbsp;
                                <asp:Button ID="cmdCancelar" runat="server" Text="Cancelar" 
                                    CssClass="cmdprocesarXLS" ValidationGroup="Cancelar" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                
                            <asp:ImageButton ID="ibtnMostrarPopUpInforme" runat="server" Height="5px" CssClass="hidden" />
                            <asp:Panel ID="pnlContedorInforme" runat="server" Style="display: none; Width:400px"  CssClass="modalPopup">
                            <asp:Panel ID="pnlCabeceraInforme" runat="server" Style="cursor: move;background-color:#DDDDDD;border:solid 1px Gray;color:Black;height:300px">
                            
                            <table style="width: 100%;background-color:White;">
                            <tr>
                                <td style="width: 98%">
                                    <div style="background-color: #e8eef7; color: #3366CC; font-weight: bold;" >
                                        <asp:Label ID="lblTitPopUpInforme" runat="server" Text="DECLARACIÓN JURADA"></asp:Label>
                                    </div>
                                </td>
                                <td style="width: 2%">
                                    <asp:ImageButton ID="ImageButton3" runat="server" 
                                        ImageUrl="~/images/cerrar.gif" />
                                </td>
                            </tr>
                             <tr>
                                <td style="text-align:justify">
                                    <asp:Label ID="lblDeclarante" runat="server" Text=""></asp:Label>
                               </td>
                            </tr>
                            <tr>
                               <td style="text-align:justify">
                                    <asp:Label ID="Label21" runat="server" Text="1.- Los datos que consigno a continuación son  de carácter de DECLARACION JURADA y por tanto asumo plena responsabilidad por la veracidad de los mismos."></asp:Label>
                                </td>
                            </tr>
                            <tr>
                               <td style="text-align:justify">
                                    <asp:Label ID="Label19" runat="server" Text="2.- La información consignada deberá ser utilizada por la Universidad para informar a SUNAT en forma obligatoria, conforme  a las normas legales vigentes."></asp:Label>
                               </td>
                            </tr>
                            <tr>
                               <td style="text-align:justify">
                                    <asp:Label ID="Label22" runat="server" Text="3.- Dicha información deberá ser actualizada obligatoriamente por mi persona, previa coordinación con la Direcciónde Personal cuando haya un cambio en los datos, o cuando la USAT lo solicite."></asp:Label>
                               </td>
                            </tr>
                            <tr>
                               <td style="text-align:justify">
                                    <asp:Label ID="Label24" runat="server" Text="4.- Luego del presente registro me comprometo a entregar en un plazo de 5 días hábiles, las copias de los documentos sustentatorios de los datos consignados (DNI, actas de nacimiento y de matrimonio, etc) las cuales serán fiel copia de los originales que obran en mi poder."></asp:Label>
                               </td>
                            </tr>
                            <tr>
                               <td style="text-align:justify">
                                    <asp:Label ID="Label25" runat="server" Text="5.- Reconozco que la información que no sustente documentalmente no estará validada por la Dirección de Personal, y por tanto no será tomada en cuenta."></asp:Label>
                               </td>
                            </tr>
                            </table>
                        </asp:Panel>
                        <table style="width: 100%;background-color:White; height:30px">
                            <tr>
                                <td style="width: 50%" align="center">
                                    <asp:Button ID="btnGuardarInforme" 
                                    runat="server" Text="            Acepto" 
                                    CssClass="conforme1" 
                                    Height="35px" Width="100px" 
                                    ToolTip="Guardar" />
                                </td>
                                <td style="width: 50%" align="center">
                                    <asp:Button ID="btnCancelar" 
                                    runat="server" Text="           No Acepto" 
                                    CssClass="rechazar_inv" 
                                    Height="35px" Width="100px" 
                                    ToolTip="Guardar" />
                                </td>
                            </tr>
                            
                        </table>
                    </asp:Panel>
                    <ajaxtoolkit:modalpopupextender 
                    ID="mpeInforme" runat="server" 
                    TargetControlID="ibtnMostrarPopUpInforme"
                    PopupControlID="pnlContedorInforme"
                    BackgroundCssClass="modalBackground"
                    DropShadow="true"
                    PopupDragHandleControlID="pnlCabeceraInforme" 
                    />
                            
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="style2">
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                        ShowMessageBox="True" ShowSummary="False" />
                </td>
            </tr>
            <tr>
                <td class="style2">
                    &nbsp;</td>
            </tr>
        </table>
        
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnGuardarInforme"/>
                <asp:PostBackTrigger ControlID="cmdAceptar"/>
                <asp:PostBackTrigger ControlID="btnCancelar"/>
                <asp:PostBackTrigger ControlID="cmdCancelar"/>
                
                
            </Triggers>
        </asp:UpdatePanel>
    </form>
</body>
</html>

