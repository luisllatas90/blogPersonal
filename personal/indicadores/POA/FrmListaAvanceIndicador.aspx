<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmListaAvanceIndicador.aspx.vb" Inherits="indicadores_POA_FrmListaAvanceIndicador" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">

<head id="Head1" runat="server">
    <title></title>
    <script type="text/javascript" language="JavaScript" src="../../../private/funciones.js"></script>
    <link href="css/estilo_poa.css" rel="stylesheet" type="text/css" media="screen" />
    <style type="text/css">
   /* .titulo_poa 
    {
        position:absolute;
        top:15px;
        left:15px;
        font-size:14px;
        font-weight:bold;
        font-family:"Helvetica Neue",Helvetica,Arial,sans-serif;
        color:#337ab7;
        background-color:White;
        padding-bottom:10px;
        padding-left:5px;
        padding-right:5px;    
        z-index:1;    
    }
    .contorno_poa
    {
        position:relative;
        top:10px;
        border:1px solid #C0C0C0;
        padding-left:4px;
        padding-top:20px;
        padding-right:4px;
    }
    .mensajeExito
    {
        background-color: #d9edf7;
        border: 1px solid #808080;  
        font-weight:bold;
        color:#31708f;
        height:18px;
        padding-top:3px;
        padding-bottom:3px;
    } 
    .mensajeEliminado
    {
        color:#8a6d3b;
        background-color:#fcf8e3;
        border: 1px solid #C5BE51;
        font-weight:bold;
        height:18px;
        padding-top:3px;
        padding-bottom:3px;
    }   
    .mensajeError
    {
        background-color: #f2dede;
        border: 1px solid #E9ABAB;
        font-weight:bold;
        color:#a94442;
        height:18px;
        padding-top:3px;
        padding-bottom:3px;
    }
    .tab_activo
    {
        width:200px;
        vertical-align:middle;
        font-weight:bold;
        color:White;
        background-color:#3871b0;
        border-color:#285e8e;
        border-style:inset;
        border-width:1px;
        border-bottom-width:0px;
        font-size:12px;
        font-family:"Helvetica Neue",Helvetica,Arial,sans-serif;
    }
        
    .tab_inactivo
    {
        width:200px;
        vertical-align:middle;
        font-weight:bold;
        color:#FFF;
        background-color:#337ab7;
        filter:alpha(opacity=65);
        border-color:#ccc;
        border-style:solid;
        border-width:1px;
        border-bottom-color:#337ab7 ;
        font-size:12px;
        font-family:"Helvetica Neue",Helvetica,Arial,sans-serif;
    }
    .celda_combinada
    {
        border-color:rgb(169,169,169);
        border-style:solid;
        border-width:1px;
    }
    */
    .caja_poa
    {
        font-family:Verdana;
        font-size:8.5pt;
        width:50px;
        text-align:right;
    }  
            
     
    </style>
</head>

<body>
    <form id="form1" runat="server">
    <div class="titulo_poa">
        <asp:Label ID="Label1" runat="server"
            Text="Avance de Meta de Indicador POA"></asp:Label>
    </div>
      
    <div class="contorno_poa">
        <table width="100%" id="tabla" runat="server">
            <tr style="height:30px;">
                <td width="140px" >Plan Estratégico</td>
                <td width="510px"><asp:DropDownList ID="ddlplan" runat="server" Width="400" AutoPostBack="true"></asp:DropDownList></td>
                <td width="50px"></td>
                <td width="140px">Ejercicio Presupuestal</td>
                <td><asp:DropDownList ID="ddlEjercicio" runat="server" Width="140" AutoPostBack="true"></asp:DropDownList></td>
                <td><asp:Button ID="btnBuscar" runat="server" Text="   Buscar" CssClass="btnBuscar" /></td>
            </tr>
            
            <tr>
                <td>Plan Operativo Anual</td>
                <td><asp:DropDownList ID="ddlPoa" runat="server" Width="400" AutoPostBack="true"></asp:DropDownList> 
                </td>
                <td width="50px"></td>
                <td>Actividad</td>
                <td><asp:DropDownList ID="ddlActividad" runat="server" Width="340">
                    <asp:ListItem Value="0">--SELECCIONE--</asp:ListItem></asp:DropDownList></td>
                <td></td>
            </tr>
            
            <tr>
                <td colspan="6">&nbsp;</td>
            </tr>
            <tr>
                <td colspan="6">
                    <asp:GridView ID="dgv_ListaAvance" runat="server" Width="100%" 
                        AutoGenerateColumns="False" 
                        CellPadding="3" 
                        DataKeyNames="codigo_pobj,codigo_acp,codigo_pind,codmeta1,codmeta2,codmeta3,codmeta4,codavance1,codavance2,codavance3,codavance4" >
                        <Columns>
                            <asp:BoundField DataField="actividad" HeaderText="TIPO DE ACTIVIDAD" >
                            <HeaderStyle Width="60px" />
                            <ItemStyle CssClass="celda_combinada" Width="40px" Height="20px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="OBJETIVO" DataField="objetivo" >
                            <ItemStyle CssClass="celda_combinada"  Width="200px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="INDICADOR" DataField="indicador" >
                            <HeaderStyle Width="200px" />
                            <ItemStyle CssClass="celda_combinada" Width="150px" />
                            </asp:BoundField>
                            
                            <asp:TemplateField HeaderText="AVANCE 1 (%)">
                                <ItemTemplate>
                                    <asp:textbox  ID="txtavance1" class="caja_poa"
                                    runat="server" Text='<%# Bind("avance1") %>' ></asp:textbox>
                                </ItemTemplate>
                                <ItemStyle CssClass="celda_combinada" Width="40px" Height="20px" HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:BoundField DataField="meta1" HeaderText="META 1 (%)" >
                            <ItemStyle CssClass="celda_combinada" HorizontalAlign="Right" Width="60px" Font-Bold="True" BackColor="#FFFFA8" />
                            </asp:BoundField>
                           
                            <asp:TemplateField HeaderText="AVANCE 2 (%)">
                                <ItemTemplate>
                                    <asp:textbox  ID="txtavance2" class="caja_poa"
                                    runat="server" Text='<%# Bind("avance2") %>' ></asp:textbox>
                                </ItemTemplate>
                                <ItemStyle CssClass="celda_combinada" Width="40px" Height="20px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            
                            <asp:BoundField DataField="meta2" HeaderText="META 2 (%)" >      
                            <ItemStyle CssClass="celda_combinada" HorizontalAlign="Right" Width="60px" Font-Bold="True" BackColor="#FFFFA8" />
                            </asp:BoundField>
                            
                            <asp:TemplateField HeaderText="AVANCE 3 (%)">
                                <ItemTemplate>
                                    <asp:textbox  ID="txtavance3" class="caja_poa" 
                                    runat="server" Text='<%# Bind("avance3") %>' ></asp:textbox>
                                </ItemTemplate>
                                <ItemStyle CssClass="celda_combinada" Width="40px" Height="20px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                                                        
                            <asp:BoundField DataField="meta3" HeaderText="META 3 (%)" >
                            <ItemStyle CssClass="celda_combinada" HorizontalAlign="Right" Width="60px" Font-Bold="True" BackColor="#FFFFA8" />
                            </asp:BoundField>

                            <asp:TemplateField HeaderText="AVANCE 4 (%)">
                                <ItemTemplate>
                                    <asp:textbox  ID="txtavance4" class="caja_poa"
                                    runat="server" Text='<%# Bind("avance4") %>' ></asp:textbox>
                                </ItemTemplate>
                                <ItemStyle CssClass="celda_combinada" Width="40px" Height="20px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                                                        
                            <asp:BoundField DataField="meta4" HeaderText="META 4 (%)" >
                            <ItemStyle CssClass="celda_combinada" HorizontalAlign="Right" Width="60px" Font-Bold="True" BackColor="#FFFFA8" />
                            </asp:BoundField>
                            
<%--                            <asp:BoundField DataField="codigo_pobj" HeaderText="codigo_pobj" Visible="False" />
                            <asp:BoundField DataField="codigo_acp" HeaderText="codigo_acp" Visible="False" />
                            <asp:BoundField DataField="codigo_pind" HeaderText="codigo_pind" Visible="False" />
                            <asp:BoundField DataField="codmeta1" HeaderText="codmeta1" Visible="False" />
                            <asp:BoundField DataField="codmeta2" HeaderText="codmeta2" Visible="False" />
                            <asp:BoundField DataField="codmeta3" HeaderText="codmeta3" Visible="False" />
                            <asp:BoundField DataField="codmeta4" HeaderText="codmeta4" Visible="False" />
                            <asp:BoundField DataField="codavance1" HeaderText="codavance1" Visible="False" />
                            <asp:BoundField DataField="codavance2" HeaderText="codavance2" Visible="False" />
                            <asp:BoundField DataField="codavance3" HeaderText="codavance3" Visible="False" />
                            <asp:BoundField DataField="codavance4" HeaderText="codavance4" Visible="False" />--%><asp:TemplateField 
                                HeaderText="GUARDAR" ShowHeader="False">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" 
                                        CommandName="Edit" ImageUrl="../../images/guardar.gif" Text="Editar" />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="True" 
                                        CommandName="Update" Text="Actualizar" />
                                    &nbsp;<asp:ImageButton ID="ImageButton2" runat="server" CausesValidation="False" 
                                        CommandName="Cancel" Text="Cancelar" />
                                </EditItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="60px" />
                            </asp:TemplateField>
                            
                        </Columns>
                        <EmptyDataTemplate>
                            No se Encontraron Registros
                        </EmptyDataTemplate>
                        <HeaderStyle BackColor="#3871b0" ForeColor="White" Height="25px" />
                    </asp:GridView>                
                </td>
            </tr>
            
            <tr>
                <td colspan="6">
                    <br />
                </td>
            </tr>
            
            </table>           
        <table width="100%">
            <tr>
                <td runat="server" id="aviso"><asp:Label ID="lblmensaje" runat="server" ></asp:Label></td>
            </tr>
        </table>
                
    </div>
    </form>
</body>
</html>
