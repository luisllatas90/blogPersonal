<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmListaObjetivos.aspx.vb" Inherits="indicadores_POA_PROTOTIPOS_Registrar_POA" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../css/estilo.css" rel="stylesheet" type="text/css" media="screen" />
    <style type="text/css">
         .menuseleccionado
            {
                background-color: #FFCC66;
                border: 1px solid #808080;  
            }   
            
            .menuporelegir
            {
                border: 1px solid #808080;
                background-color: #FFCC66;
            }    
            .AlineadoDerecha{
                text-align:right;
            }
            
            .titulo_poa 
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
            } 
                
            .mensajeError
            {
                background-color: #f2dede;
                border: #ebccd1;  
                font-weight:bold;
                color:#a94442;
                height:18px;
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
            .caja_poa
            {
                font-family:Verdana;
                font-size:9pt;
            }
            
    </style>
</head>
<body>
    <form id="form1" runat="server">    
    <div class="titulo_poa">
        <asp:Label ID="Label2" runat="server" Text="Mantenimiento de Objetivos"></asp:Label>
    </div>
        
    <div class="contorno_poa">
        <table width="100%">
        <tr>
            <td width="150px" >Plan Estratégico</td>
            <td><asp:DropDownList ID="ddlPlan" runat="server" Width="400px" ></asp:DropDownList></td>
            <td>Ejercicio </td>
            <td><asp:DropDownList ID="ddlEjercicio" runat="server" Width="100px" ></asp:DropDownList></td>
            <td>Tipo Actividad</td>
            <td>
            <asp:DropDownList ID="ddl_tipoActividad" runat="server" 
                Width="200px" ></asp:DropDownList></td>
            <td></td>
            <td> <asp:Button ID="btnBuscar" runat="server" Text="   Buscar" CssClass="buscar2" /></td>
            
        </tr>
        <tr>
            <td colspan="8"></td>
        </tr>
        <tr>
            <td colspan="8">
                <asp:Button ID="btnNuevo" runat="server" 
                Text="   Agregar Objetivos" CssClass="agregar2" Width="130px" />
                </td>
        </tr>
        <tr>
            <td colspan="8"></td>
        </tr>
        <tr>
            <td colspan="8">
        <asp:GridView ID="gvwObjetivos" runat="server" Width="100%" 
            AutoGenerateColumns="False" 
            DataKeyNames="codigo_pla,codigo_ejp,codigo_acp,codigo_pobj" 
            CellPadding="3" >
            <Columns>
                <asp:BoundField DataField="nombre_poa" HeaderText="NOMBRE DE POA" >
                <FooterStyle Width="100px" />
                <HeaderStyle Width="200px" />
                <ItemStyle CssClass="celda_combinada" />
                </asp:BoundField>
                <asp:BoundField HeaderText="TIPO" DataField="actividad" >
                <HeaderStyle Width="100px" />
                <ItemStyle Width="30px" />
                <ItemStyle CssClass="celda_combinada" />
                </asp:BoundField>
                <asp:BoundField HeaderText="ACTIVIDAD" DataField="resumen_acp" >
                <HeaderStyle Width="200px" />
                <ItemStyle CssClass="celda_combinada" />
                </asp:BoundField>
                <asp:BoundField DataField="objetivo" HeaderText="OBJETIVO" >
                <ItemStyle Width="200px" />
                </asp:BoundField>
                <asp:CommandField ShowSelectButton="true" ButtonType="Image"  
                    HeaderText="EDITAR" SelectImageUrl="../../images/editar.gif" 
                    SelectText="Editar" >
                <ItemStyle HorizontalAlign="Center" Width="50px" />
                </asp:CommandField>
                
                
                <%--<asp:CommandField ShowDeleteButton="True" ButtonType="Image" 
                    DeleteImageUrl="../../images/eliminar.gif" HeaderText="ELIMINAR" 
                    Visible="true" >
                <ItemStyle HorizontalAlign="Center" Width="50px" />
                </asp:CommandField>--%>
                
                 <asp:TemplateField HeaderText="ELIMINAR" ShowHeader="False">
                    <ItemTemplate>
                        <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" 
                            CommandName="Delete" ImageUrl="../../images/eliminar.gif" Text="Eliminar" OnClientClick="return confirm('¿Esta Seguro que Desea Eliminar Actividad?.')" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:TemplateField>
                
                
                <asp:BoundField DataField="codigo_pla" HeaderText="codigo_pla" Visible="False" />
                <asp:BoundField DataField="codigo_ejp" HeaderText="codigo_ejp" Visible="False" />
                <asp:BoundField DataField="codigo_poa" HeaderText="codigo_poa" 
                    Visible="False" />      
                <asp:BoundField DataField="codigo_pobj" HeaderText="codigo_pobj" 
                    Visible="False" />
            </Columns>
            <EmptyDataTemplate>
                No se Encontraron Registros
            </EmptyDataTemplate>
            <HeaderStyle BackColor="#3871b0" ForeColor="White" Height="25px" />
        </asp:GridView>
            </td>
        </tr>
        
        
        
        <tr>
            <td colspan="10"></td>
        </tr>
        
        </table>
        
       
        <asp:Label ID="lblMensajeFormulario" runat="server"></asp:Label>
         
        
        
        
        <table width="95%">
            <tr>
                <td runat="server" id="aviso"><asp:Label ID="lblmensaje" runat="server" ></asp:Label></td>
            </tr>
        </table>
        
    </div>
        <asp:HiddenField ID="HdCodigo_poa" runat="server" />
    </form>
</body>
</html>
