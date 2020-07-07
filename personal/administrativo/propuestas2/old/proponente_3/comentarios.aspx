<%@ Page Language="VB" AutoEventWireup="false" CodeFile="comentarios.aspx.vb" Inherits="proponente_comentarios" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<link href="../estilo.css"rel="stylesheet" type="text/css">
<script type="text/javascript" src="../funciones.js"> </script>
    <title>Página sin título</title>
    <style type="text/css">
        .style1
        {
            height: 33px;
        }
        .style2
        {
            height: 47px;
        }
        </style>
</head>
<body style="margin:0,0,0,0">
    <form id="form1" runat="server">
    
        <table width="100%">
            <tr>
                <td width="50%">
                    <table class="style1" width="100%">
                        <tr>
                            <td align="right" class="style2">
                                <table bgcolor="Silver" cellpadding="0" cellspacing="0" class="contornotabla">
                                    <tr>
                                        <td>
                                <asp:Button ID="cmdNuevo" runat="server" CssClass="nuevocomentario" 
                                    Height="27px" Text="          Nuevo Comentario" Width="150px" Font-Overline="False" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:DataList ID="DataList1" runat="server" DataKeyField="codigo_Cop" 
                                    DataSourceID="SqlDataSource1">
                                    <ItemTemplate>
                                        &nbsp;<table class="contornotabla">
                                            <tr>
                                                <td width="2%">
                                                    <img alt="" src="../images/rpta.GIF" /></td>
                                                <td width="58%">
                                                    Fecha:
                                                    <asp:Label ID="fecha_CopLabel" runat="server" ForeColor="Black" 
                                                        Text='<%# Eval("fecha_Cop") %>' />
                                                </td>
                                                <td rowspan="3" valign="top" width="40%">
                                                    <asp:Button ID="cmdLeido" runat="server" CssClass="agregar2" 
                                                        Text="Marcar como leído" 
                                                        Visible='<%# iif(Eval("leido")=0,true,false) %>' 
                                                        CausesValidation="False" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;</td>
                                                <td>
                                                    De:<asp:Label ID="de" runat="server" ForeColor="Black" 
                                                        Text='<%# Eval("personal") %>' />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;</td>
                                                <td>
                                                    Asunto:
                                                    <asp:Label ID="asunto_CopLabel" runat="server" Font-Bold="True" 
                                                        ForeColor="Black" Text='<%# Eval("asunto_Cop") %>' />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;</td>
                                                <td colspan="2">
                                                    <asp:Label ID="comentario_CopLabel" runat="server" ForeColor="#003399" 
                                                        Text='<%# Eval("comentario_Cop") %>' />
                                                </td>
                                            </tr>
                                        </table>
                                        <br />
                                    </ItemTemplate>
                                </asp:DataList>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    
    <br />
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:CNXBDUSAT %>" 
        SelectCommand="ConsultarComentarios" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:Parameter DefaultValue="NU" Name="tipo" Type="String" />
            <asp:QueryStringParameter DefaultValue="" Name="param1" 
                QueryStringField="codigo_prp" Type="Int32" />
            <asp:QueryStringParameter Name="param2" QueryStringField="codigo_per" 
                Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <br />
    </form>
</body>
</html>
