<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmSolEscolaridad.aspx.vb" Inherits="escolaridad_frmSolEscolaridad" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <link href="../../private/estilo.css?x=11" rel="stylesheet" type="text/css" />
        <link href="css/estilodj.css?x=1"   rel="stylesheet" type="text/css" />
        <script type="text/javascript"  language="JavaScript" src="../../private/funciones.js?x=2"></script>
        
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <!-- Proceso de Solicitud Escolaridad -->
        <asp:Panel ID="pnlSolicitud" runat="server">
            <table style="width: 100%" class="contornotabla">
            <tr>
                <td bgcolor="#EFF3FB" height="35px">
                    <b>
                    <asp:Label ID="Label11" runat="server" Text="SOLICITUDES DE ESCOLARIDAD"></asp:Label></b>
                </td>
            </tr>
            <tr>
                <td align="left" height="40px" class="contornotabla" 
                    style="background-color:#FFFFCC";>
                    <asp:Label ID="lblInstrucciones" runat="server" Text=""></asp:Label>   
                </td>
            </tr>
            <tr>
            <td align="left" height="40px" class="contornotabla" 
                    style="background-color:#FFFFCC";>
             <asp:Label ID="lblInstrucciones2" runat="server" Text=""></asp:Label>  
            </td>
            </tr>
            <tr>
                <td align="left" class="contornotabla" style="background-color:#EFF3FB";>
                    <table style="width: 100%">
                        <tr>
                            <td align"left">
                                  
                                  <asp:Button 
                                        ID="btnDeclaracionJurada" 
                                        CssClass="imprimirescolaridad" 
                                        ToolTip="Permite enviar una solicitud de Escolaridad." 
                                        runat="server" 
                                        Enabled="false"  
                                        Visible="false"
                                        Height="40px"  Width="165px"  
                                        Text="         Declaración Jurada" />
                                        
                                        
                                
                            </td>
                            <td align="right">
                                <asp:Label ID="lblnumeroregistros"  runat="server" ForeColor="Blue" ></asp:Label>
                            </td>
                        </tr>
                    </table>
                   
                </td>
            </tr>
            <tr>
                <td class="contornotabla" style="width:100%; background-color:#006699;">
                    <asp:GridView 
                                ID="gvLista" 
                                runat="server" 
                                Width="100%" 
                                BackColor="White" 
                                BorderColor="#CCCCCC" 
                                BorderStyle="None" 
                                BorderWidth="1px" 
                                CellPadding="3" 
                                AutoGenerateColumns="False" 
                                EmptyDataText="No se encontraron registros.." 
                                AllowPaging="True" 
                                DataKeyNames="codigo_dhab">
                        <RowStyle ForeColor="#000066" />
                        <EmptyDataRowStyle BackColor="#FFFF99" ForeColor="#FF3300" />
                        <Columns>
                            <asp:BoundField HeaderText="#" >
                                <ItemStyle Width="15px" />
                            </asp:BoundField>
                            
                            <asp:TemplateField>
                                    <ItemTemplate>                
                                        <asp:CheckBox ID="chkElegir" runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle Width="5px" />
                            </asp:TemplateField>
                            
                            <asp:BoundField DataField="codigo_dhab" HeaderText="ID" >
                            <ItemStyle Width="10px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="derechohabiente" HeaderText="Apellidos Nombres " >
                            <ItemStyle Width="180px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fechaNacimiento_dhab" HeaderText="Fecha Nacimiento" >
                            <ItemStyle Width="30px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Edad" HeaderText="Edad" >
                            
                            <ItemStyle Width="15px" />
                            </asp:BoundField>
                            
                            <asp:TemplateField>
                                <HeaderTemplate>
                                      <label>Centro de Estudios (Nivel - Nombre del Centro)</label>
                                 </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddlNivel" runat="server" OnSelectedIndexChanged="ddlNivel_SelectedIndexChanged" AutoPostBack="True">
                                    </asp:DropDownList>
                                    <asp:TextBox ID="txtCentroEstudios" runat="server"></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle Width="350px" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <label>Grado</label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddlGrado" Width="40px" Enabled="false" runat="server">
                                    </asp:DropDownList>
                                </ItemTemplate>
                                <ItemStyle Width="20px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="C.A" Visible="False">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkApli" runat="server" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="estado_soe" HeaderText="Estado" >
                            <ItemStyle Width="15px" />
                            </asp:BoundField>
                        </Columns>
                        <FooterStyle BackColor="White" ForeColor="#000066" />
                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                        <SelectedRowStyle BackColor="#FFFFB1" Font-Bold="True" ForeColor="Blue" />
                        <HeaderStyle BackColor="#EFF3FB" Font-Bold="True" ForeColor="Blue" />
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td class="contornotabla" style="width:100%; background-color:#FFFFCC;">
                    <asp:Label ID="lblDoc" runat="server" Text="Documentos de Sustento" 
                        ForeColor="blue"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtDocumentos" Width="99.3%" Height="50px" runat="server"></asp:TextBox>
                </td>
            </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnSolicitar" runat="server" CssClass="solicitarescolaridad" 
                            Height="40px" Text="         Solicitar Escolaridad" 
                            ToolTip="Permite enviar una solicitud de Escolaridad." Width="165px" />
                    </td>
                </tr>
        </table>
        </asp:Panel>
        <table style="width: 100%" class="contornotabla">
            <tr>
                <td>
                    <div id="boton" runat="server" > </div>
                </td>
            </tr>
        </table>
        <!-- ::::::::::::::::::::::::::::::::::::: -->
        <!-- Declaracion Jurada -->
        <asp:Panel ID="pnlDeclaracionJurada" runat="server" Visible="false">
            <table style="width: 100%" class="contornotabla">
                 <tr>
                    <td bgcolor="#EFF3FB" height="35px">
                        <b>
                        <asp:Label ID="Label1" runat="server" Text="SOLICITUDES DE ESCOLARIDAD"></asp:Label></b>
                    </td>
                </tr>
                <tr>
                    <td align="left" height="40px" class="contornotabla" 
                        style="background-color:#FFFFCC";>
                        <asp:Label ID="lblInstruccionesDJ" runat="server" Text=""></asp:Label>   
                    </td>
                </tr>   
                <tr>
                    <td>
                        
                    </td>
                </tr>
                <tr>
                    <!-- Aqui vamos a contruir el modelo de DJ -->
                    <td>
                         <div class="format">
                            <div id="datosDeclarante_html" runat="server"></div>
                            <div id="datosDerechoHabientes_html" runat="server"></div>  
                            <div id="datosPieDeclarante_html" runat="server"></div>                          
                         </div>
                    </td>
                    <!-- fin del modelo de DJ -->
                </tr>
            </table>   
        </asp:Panel>
        
    </div>
    </form>
</body>
</html>
