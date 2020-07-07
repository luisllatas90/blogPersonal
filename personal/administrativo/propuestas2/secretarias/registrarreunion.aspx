<%@ Page Language="VB" AutoEventWireup="false" CodeFile="registrarreunion.aspx.vb" Inherits="proponente_comentarios" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<link href="../estilo.css"rel="stylesheet" type="text/css">
<script type="text/javascript" src="../funciones.js"> </script>
    <title>Página sin título</title>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            height: 55px;
        }
        .style3
        {
            height: 109px;
        }
    </style>
</head>
<body style="margin:0,0,0,0">
    <form id="form1" runat="server">
    
                                <table class="contornotabla" align="center" 
            cellpadding="0" cellspacing="0" width="80%">
                                    <tr class="bordeinf" 
                                        style="background-color: #F0F0F0; border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #000000;">
                                        <td align="left" width="50%" class="style2" valign="middle">
                                            <asp:Button ID="cmdRegistrar" runat="server" CssClass="guardar_prp" 
                                                Height="37px" Text="     Registrar" Width="114px" 
                                                ValidationGroup="reunion" />
                                        </td>
                                        <td width="50%" align="right" class="style2" style="background-color: #F0F0F0">
                                            <asp:LinkButton ID="LinkButton1" runat="server" Font-Bold="True" 
                                                Font-Size="Smaller" ForeColor="Maroon">Volver a Propuesta</asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr class="bordesup">
                                        <td align="left" width="50%">
                                            &nbsp;</td>
                                        <td width="50%" align="right">
                                            &nbsp;</td>
                                    </tr>
                                    <tr class="bordesup">
                                        <td align="left" width="50%">
                                <asp:Label ID="lblPropuesta" runat="server" Font-Bold="True" Font-Size="Small" 
                                    ForeColor="Blue">Registro de Agenda para reunión</asp:Label>
                                            :</td>
                                        <td width="50%" align="left">
                                            <asp:Label ID="lblInstancia" runat="server" Font-Bold="True" Font-Size="Large" 
                                                ForeColor="Maroon"></asp:Label>
&nbsp;<asp:Label ID="lblFacultad" runat="server" Font-Bold="True" Font-Size="Large" ForeColor="Maroon"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" width="50%">
                                            <asp:Label ID="txtReunion" runat="server" Visible="False"></asp:Label>
                                            <asp:HiddenField ID="txtReunion4" runat="server" />
                                        </td>
                                        <td width="50%">
                                            <asp:HiddenField ID="txtInstancia4" runat="server" />
                                            <asp:Label ID="txtcodigo_Fac" runat="server" Visible="False"></asp:Label>
                                            <asp:HiddenField ID="txtcodigo_Fac4" runat="server" />
                                            <asp:Label ID="txtInstancia" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" width="50%" valign="top" class="style3">
                                            <table class="style1">
                                                <tr>
                                                    <td>
                                                        Nombre de la sesión:</td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtnombrereunion" runat="server" Width="90%"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                                            ControlToValidate="txtnombrereunion" 
                                                            ErrorMessage="Ingrese el nombre de la reunión" ValidationGroup="reunion">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                        Lugar:</td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtlugarreunion" runat="server" Width="90%"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                                            ControlToValidate="txtlugarreunion" 
                                                            ErrorMessage="Ingrese el lugar de la reunión" ValidationGroup="reunion">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                </table>
                                        </td>
                                        <td width="50%" class="style3" valign="top">
                                            <table class="style1">
                                                <tr>
                                                    <td>
                                                        Fecha:
                                                        <asp:Label ID="Label1" runat="server" ForeColor="Red" Text="(Ej. 23/05/2009)"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="calFecha" runat="server" MaxLength="10"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                                            ControlToValidate="calFecha" ErrorMessage="Ingrese la fecha de la reunión" 
                                                            ValidationGroup="reunion">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Tipo:</td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:DropDownList ID="ddlTiporeunion0" runat="server">
                                                            <asp:ListItem Value="O">ORDINARIA</asp:ListItem>
                                                            <asp:ListItem Value="E">EXTRAORDINARIA</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" width="50%">
                                            &nbsp;</td>
                                        <td width="50%">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td align="left" width="50%" style="font-weight: bold; color: #009933">
                                            Propuestas por programar:</td>
                                        <td width="50%" style="font-weight: bold; color: #800000">
                                            Agenda programada:</td>
                                    </tr>
                                    <tr>
                                        <td align="left" width="50%">
                                            &nbsp;</td>
                                        <td width="50%">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td align="left" width="50%" valign="top">
                                            <asp:GridView ID="dgvPendientes" runat="server" DataSourceID="SqlDataSource1" 
                                                AutoGenerateColumns="False" DataKeyNames="codigo_prp" Width="95%">
                                                <Columns>
                                                    <asp:BoundField DataField="codigo_Prp" HeaderText="cod" InsertVisible="False" 
                                                        ReadOnly="True" SortExpression="codigo_Prp" />
                                                    <asp:BoundField DataField="nombre_Prp" HeaderText="Propuestas por Asignar" 
                                                        SortExpression="nombre_Prp" />
                                                    <asp:ButtonField CommandName="cmdAsignar" 
                                                        DataTextField="Asignar" Text="Asignar" ValidationGroup="reunion" >
                                                        <ControlStyle ForeColor="#0000CC" />
                                                        <HeaderStyle ForeColor="#0000CC" />
                                                    </asp:ButtonField>
                                                </Columns>
                                            </asp:GridView>
                                            <asp:GridView ID="dgvPendientesRectorado" runat="server" DataSourceID="SqlDataSource3" 
                                                AutoGenerateColumns="False" DataKeyNames="codigo_prp" Width="95%">
                                                <Columns>
                                                    <asp:BoundField DataField="codigo_Prp" HeaderText="cod" InsertVisible="False" 
                                                        ReadOnly="True" SortExpression="codigo_Prp" />
                                                    <asp:BoundField DataField="nombre_Prp" HeaderText="Propuestas por Asignar Rectorado" 
                                                        SortExpression="nombre_Prp" />
                                                    <asp:ButtonField CommandName="cmdAsignar" 
                                                        DataTextField="Asignar" Text="Asignar" ValidationGroup="reunion" 
                                                        Visible="False" >
                                                        <ControlStyle ForeColor="#0000CC" />
                                                        <HeaderStyle ForeColor="#0000CC" />
                                                    </asp:ButtonField>
                                                </Columns>
                                            </asp:GridView>
                                            <asp:GridView ID="dgvPendientesConsejo" runat="server" DataSourceID="SqlDataSource4" 
                                                AutoGenerateColumns="False" DataKeyNames="codigo_prp" Width="95%">
                                                <Columns>
                                                    <asp:BoundField DataField="codigo_Prp" HeaderText="cod" InsertVisible="False" 
                                                        ReadOnly="True" SortExpression="codigo_Prp" />
                                                    <asp:BoundField DataField="nombre_Prp" HeaderText="Propuestas por Asignar Consejo de Universitario" 
                                                        SortExpression="nombre_Prp" />
                                                    <asp:ButtonField CommandName="cmdAsignar" 
                                                        DataTextField="Asignar" Text="Asignar" ValidationGroup="reunion" >
                                                        <ControlStyle ForeColor="#0000CC" />
                                                        <HeaderStyle ForeColor="#0000CC" />
                                                    </asp:ButtonField>
                                                </Columns>
                                            </asp:GridView>
                                            <asp:SqlDataSource ID="SqlDataSource4" runat="server" 
                                                ConnectionString="<%$ ConnectionStrings:CNXBDUSAT %>" 
                                                SelectCommand="PRP_ConsutarReunionesConsejo" 
                                                SelectCommandType="StoredProcedure">
                                                <SelectParameters>
                                                    <asp:Parameter DefaultValue="PC" Name="tipo" Type="String" />
                                                    <asp:Parameter DefaultValue="0" Name="idrec" Type="Int32" />
                                                    <asp:Parameter DefaultValue="0" Name="param1" Type="String" />
                                                    <asp:Parameter DefaultValue="0" Name="param2" Type="String" />
                                                </SelectParameters>
                                            </asp:SqlDataSource>
                                            <asp:SqlDataSource ID="SqlDataSource3" runat="server" 
                                                ConnectionString="<%$ ConnectionStrings:CNXBDUSAT %>" 
                                                SelectCommand="PRP_ConsutarReunionesConsejo" 
                                                SelectCommandType="StoredProcedure">
                                                <SelectParameters>
                                                    <asp:Parameter DefaultValue="PK" Name="tipo" Type="String" />
                                                    <asp:Parameter DefaultValue="0" Name="idrec" Type="Int32" />
                                                    <asp:Parameter DefaultValue="0" Name="param1" Type="String" />
                                                    <asp:Parameter DefaultValue="0" Name="param2" Type="String" />
                                                </SelectParameters>
                                            </asp:SqlDataSource>
                                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                                                ConnectionString="<%$ ConnectionStrings:CNXBDUSAT %>" 
                                                SelectCommand="PRP_ConsutarReunionesConsejo" 
                                                SelectCommandType="StoredProcedure">
                                                <SelectParameters>
                                                    <asp:Parameter DefaultValue="PF" Name="tipo" Type="String" />
                                                    <asp:ControlParameter ControlID="txtcodigo_Fac" DefaultValue="" Name="idrec" 
                                                        PropertyName="Text" Type="Int32" />
                                                    <asp:Parameter DefaultValue="&quot;" Name="param1" Type="String" />
                                                    <asp:Parameter DefaultValue="&quot;&quot;" Name="param2" Type="String" />
                                                </SelectParameters>
                                            </asp:SqlDataSource>
                                        </td>
                                        <td width="50%" valign="top">
                                            <asp:GridView ID="dgvProgramadas" runat="server" AutoGenerateColumns="False" 
                                                DataSourceID="SqlDataSource2" DataKeyNames="codigo_Rcp">
                                                <Columns>
                                                    <asp:BoundField DataField="codigo_Rcp" HeaderText="cod" 
                                                        SortExpression="codigo_Rcp" InsertVisible="False" ReadOnly="True" />
                                                    <asp:BoundField DataField="nombre_Prp" HeaderText="Propuesta" 
                                                        SortExpression="nombre_Prp" />
                                                    <asp:ButtonField CommandName="cmdQuitar" DataTextField="Quitar" Text="Quitar">
                                                        <ControlStyle ForeColor="Red" />
                                                    </asp:ButtonField>
                                                </Columns>
                                            </asp:GridView>
                                            <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                                                ConnectionString="<%$ ConnectionStrings:CNXBDUSAT %>" 
                                                SelectCommand="PRP_ConsultarSesionesConsejos" 
                                                SelectCommandType="StoredProcedure">
                                                <SelectParameters>
                                                    <asp:Parameter DefaultValue="AG" Name="tipo" Type="String" />
                                                    <asp:ControlParameter ControlID="txtReunion" DefaultValue="" Name="codigo_per" 
                                                        PropertyName="Text" Type="Int32" />
                                                </SelectParameters>
                                            </asp:SqlDataSource>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" width="50%">
                                            &nbsp;</td>
                                        <td width="50%">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td align="left" width="50%">
                                            &nbsp;</td>
                                        <td width="50%">
                                            &nbsp;</td>
                                    </tr>
                                </table>
    <br />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                                    ValidationGroup="reunion" Height="32px" />
    </form>
</body>
</html>
