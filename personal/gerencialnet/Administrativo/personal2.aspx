<%@ Page Language="VB" AutoEventWireup="false" CodeFile="personal2.aspx.vb" Inherits="Academico_personal" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>:: Reportes Académicos :: Personalización.</title>
</head>
<body leftmargin="0" topmargin="0" rightmargin="0">
    <form id="form1" runat="server">
    <div>
       <table style="width: 780px">
           <tr>
               <td align="center" colspan="3" style="font-weight: bold; font-size: 12pt; color: black;
                   font-family: Verdana; text-align: center; background-color: #990000;">
               </td>
           </tr>
            <tr>
                <td style="font-weight: bold; font-size: 12pt; color: white; font-family: Verdana; height: 24px; text-align: center; background-color: #666632;" align="center" colspan="3">
                    Personalice sus Informes</td>
            </tr>
           <tr>
               <td align="left" colspan="3" style="font-weight: normal; font-size: 9pt; color: #660033;
                   font-family: Verdana; height: 31px; background-color: #ece9d8; text-align: justify">
                   <asp:Image ID="Image1" runat="server" Height="41px" ImageAlign="Left" ImageUrl="~/images/cubo_.jpg"
                       Width="41px" />Seleccione los Items en cada uno de las opciones mostradas y luego haga click en
                   mostrar para visualizar su informe. Tega en cuenta que al no seleccionar uno los
                   items se mostrara la información general totalizada segun las opciones mostradas.</td>
           </tr>
           <tr>
               <td valign="top">
               </td>
               <td valign="top">
               </td>
               <td align="right" valign="top">
                   <asp:Button ID="Button1" runat="server" Text="Mostrar" BackColor="Info" BorderStyle="Solid" BorderWidth="1px" Width="69px" /></td>
           </tr>
           <tr>
               <td colspan="2" valign="top" style="font-weight: normal; font-size: 8pt; color: maroon; font-family: Verdana">
                   &nbsp;Escuela Profesional&nbsp; :&nbsp;&nbsp;<asp:DropDownList ID="DDLEscuela"
                       runat="server" Width="316px" style="font-size: 8pt; font-family: Verdana" AutoPostBack="True">
                   </asp:DropDownList></td>
               <td align="right" valign="top">
               </td>
           </tr>
           <tr>
               <td colspan="2" valign="top" style="font-weight: normal; font-size: 8pt; color: maroon; font-family: Verdana">
                   &nbsp;Plan de Estudios &nbsp; &nbsp; &nbsp;: &nbsp;<asp:DropDownList ID="DDLPlan"
                       runat="server" AutoPostBack="True" Style="font-size: 8pt; font-family: Verdana"
                       Width="318px">
                   </asp:DropDownList>
                   </td>
               <td align="right" valign="top">
               </td>
           </tr>
           <tr>
               <td colspan="2" valign="top" style="font-weight: normal; font-size: 8pt; color: maroon; font-family: Verdana">
                   &nbsp;Cursos &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; : &nbsp;<asp:DropDownList
                       ID="DDLCursos" runat="server" Style="font-size: 8pt; font-family: verdana" Width="318px">
                   </asp:DropDownList></td>
               <td align="right" valign="top">
                   </td>
           </tr>
            <tr>
                <td style="width: 260px; overflow: auto;" valign="top">
                    <asp:TreeView ID="TreeDim3" runat="server" ImageSet="Simple" NodeIndent="10" NodeWrap="True" Width="260px" ShowExpandCollapse="False" ShowLines="True">
                        <ParentNodeStyle Font-Bold="False" />
                        <HoverNodeStyle Font-Underline="True" ForeColor="#DD5555" />
                        <SelectedNodeStyle Font-Underline="True" ForeColor="#DD5555" HorizontalPadding="0px"
                            VerticalPadding="0px" />
                        <NodeStyle Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" HorizontalPadding="0px"
                            NodeSpacing="0px" VerticalPadding="0px" />
                    </asp:TreeView>
                </td>
                <td style="width: 260px" valign="top">
                   <asp:TreeView ID="TreeDim1" runat="server" ImageSet="Simple" NodeIndent="10" NodeWrap="True" Width="260px" ShowExpandCollapse="False" ShowLines="True">
                        <ParentNodeStyle Font-Bold="False" />
                        <HoverNodeStyle Font-Underline="True" ForeColor="#DD5555" />
                        <SelectedNodeStyle Font-Underline="True" ForeColor="#DD5555" HorizontalPadding="0px"
                            VerticalPadding="0px" />
                        <NodeStyle Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" HorizontalPadding="0px"
                            NodeSpacing="0px" VerticalPadding="0px" />
                    </asp:TreeView>
                </td>
                <td style="width: 260px" valign="top">
                    
                    <asp:TreeView ID="TreeDim2" runat="server" ImageSet="Simple" NodeIndent="10" NodeWrap="True" Width="260px" ShowExpandCollapse="False" ShowLines="True">
                        <ParentNodeStyle Font-Bold="False" />
                        <HoverNodeStyle Font-Underline="True" ForeColor="#DD5555" />
                        <SelectedNodeStyle Font-Underline="True" ForeColor="#DD5555" HorizontalPadding="0px"
                            VerticalPadding="0px" />
                        <NodeStyle Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" HorizontalPadding="0px"
                            NodeSpacing="0px" VerticalPadding="0px" />
                    </asp:TreeView>
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
