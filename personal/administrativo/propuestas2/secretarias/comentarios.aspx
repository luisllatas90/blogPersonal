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
        .style3
        {
            width: 100%;
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
                                <table class="style3">
                                    <tr>
                                        <td align="left" width="90%">
                                <asp:Label ID="lblPropuesta" runat="server" Font-Bold="True" Font-Size="X-Large" 
                                    ForeColor="Blue"></asp:Label>
                                        </td>
                                        <td width="10%">
                                            <asp:LinkButton ID="LinkButton1" runat="server" Font-Bold="True" 
                                                Font-Size="Medium" ForeColor="Maroon">Volver a Propuesta</asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:DataList ID="DataList1" runat="server" DataKeyField="codigo_Cop" 
                                    DataSourceID="SqlDataSource1" Font-Size="Medium">
                                    <ItemTemplate>
                                        &nbsp;<table class="contornotabla">
                                            <tr>
                                                <td width="2%">
                                                    <img alt="" src="../images/rpta.GIF" /></td>
                                                <td width="58%" style="font-size: large">
                                                    Fecha:
                                                    <asp:Label ID="fecha_CopLabel" runat="server" ForeColor="Black" 
                                                        Text='<%# Eval("fecha_Cop") %>' />
                                                </td>
                                                <td rowspan="3" valign="top" width="40%">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;</td>
                                                <td style="font-size: large">
                                                    De:<asp:Label ID="de" runat="server" ForeColor="Black" 
                                                        Text='<%# Eval("personal") %>' />
                                                </td>
                                            </tr>
                                            <tr style="font-size: large">
                                                <td>
                                                    &nbsp;</td>
                                                <td style="font-size: large">
                                                    Asunto:
                                                    <asp:Label ID="asunto_CopLabel" runat="server" Font-Bold="True" 
                                                        ForeColor="Black" Text='<%# Eval("asunto_Cop") %>' />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;</td>
                                                <td colspan="2" style="font-size: large">
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
            <asp:Parameter DefaultValue="RE" Name="tipo" Type="String" />
            <asp:QueryStringParameter DefaultValue="" Name="param1" 
                QueryStringField="codigo_prp" Type="Int32" />
            <asp:Parameter DefaultValue="0" Name="param2" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <br />
    </form>
</body>
</html>
