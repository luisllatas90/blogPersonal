<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmListaTesis.aspx.vb" Inherits="administrativo_biblioteca_FrmListaTesis" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Mantenimiento de Tesis</title>
<style type="text/css">
        body
        {
            font-family: "Trebuchet MS" , "Lucida Console" , Arial, san-serif;
            color: Black;
            font-size: 10pt;
            font: normal;
        }
        /*==== DropDownList, Select =====*/
        select
        {
            font-family: Verdana;
            font-size: 9pt;
        }

        select:hover
        {
            cursor:pointer;
        }
        /*==== Fin DropDownList, Select =====*/
         .contorno_poa
        {
            position: relative;
            top: 10px;
            border: 1px solid #C0C0C0;
            padding-left: 4px;
            padding-top: 20px;
            padding-right: 4px;
        }
        
        .titulo_poa
        {
            position: absolute;
            top: 15px;
            left: 15px;
            font-size: 14px;
            font-weight: bold;
            font-family: "Helvetica Neue" ,Helvetica,Arial,sans-serif;
            color: #337ab7;
            background-color: White;
            padding-bottom: 10px;
            padding-left: 5px;
            padding-right: 5px;
            z-index: 1;
        }
         /*==== Mensajes =====*/
        .mensajeExito
        {
            background-color: #d9edf7;
            border: 1px solid #808080;
            font-weight: bold;
            color: #31708f;
            height: 25px;
            font-size: 13px;
            padding-top: 3px;
            padding-bottom: 3px;
            vertical-align:middle;
        }
        .mensajeEliminado
        {
            color: #8a6d3b;
            background-color: #fcf8e3;
            border: 1px solid #C5BE51;
            font-weight: bold;
            height: 25px;
            font-size: 13px;
            padding-top: 3px;
            padding-bottom: 3px;
            vertical-align:middle;
        }
        .mensajeError
        {
            background-color: #f2dede;
            border: 1px solid #E9ABAB;
            font-weight: bold;
            color: #a94442;
            height: 25px;
            font-size: 13px;
            padding-top: 3px;
            padding-bottom: 3px;
            vertical-align:middle;
        }
        /*==== Fin Mensajes =====*/
        
        /* ====== MANTENIMIENTO ACTIVIDAD_POA (REGISTRO PROGRAMAS Y PROYECTOS) ======== */
        /*==== Botones =====*/
        .btnCancelar
        {
            border: 1px solid #e0a6af;
	        background: #f4dfe2 url('../../../Images/menus/noconforme_small.gif') no-repeat 0% center;
	        color:Red;
	        height:32px;
	        font-weight:bold;
	        
        }
        .btnCancelar:hover
        {
            border: 1px solid #cc6d7c;
	        background:  url('../../../Images/menus/noconforme_small.gif') no-repeat 0% center;
	        cursor:pointer;
         }
         
        .btnGuardar
        {
            border: 1px solid #6dcc8e;
	        background: #ccedd7 url('../../../images/guardar.gif') no-repeat 0% center;
	        color:Green;
	        font-weight:bold;
	        height:32px;
        }
        .btnGuardar:hover
        {
            border: 1px solid #189252;
	        background: url('../../../images/guardar.gif') no-repeat 0% center;
	        cursor:pointer;
         }
         
        .btnExporta
        {
            border: 1px solid #6dcc8e;
	        background: #ccedd7 url('../../../images/excel.gif') no-repeat 0% center;
	        color:Green;
	        font-weight:bold;
	        height:25px;
        }
        .btnExporta:hover
        {
            border: 1px solid #189252;
	        background: url('../../../images/excel.gif') no-repeat 0% center;
	        cursor:pointer;
         }
         
        .btnGuardarCheck
        {
            border: 1px solid #6dcc8e;
	        background: #ccedd7 url('../../../Images/Okey.gif') no-repeat 0% center;
	        color:Green;
	        font-weight:bold;
	        height:32px;
        }
        .btnGuardarCheck:hover
        {
            border: 1px solid #189252;
	        background: url('../../../Images/Okey.gif') no-repeat 0% center;
	        cursor:pointer;
         }
         
        .btnNuevo
        {
            border: 1px solid #408ab2;
	        background: #cfe4ee url('../../../../Images/agregar.gif') no-repeat 0% center;
	        color:#397c9f;
	        font-weight:bold;
	        height:25px;
        }
        .btnNuevo:hover
        {
            border: 1px solid #5fa2c6;
	        background: url('../../../../Images/agregar.gif') no-repeat 0% center;
	        cursor:pointer; 
         }
        .btnBuscar
        {
            border: 1px solid #bfac4c;
	        background: #eee9cf url('../../../Images/buscar_poa.png') no-repeat 0% center;
	        color:#685d25;
	        font-weight:bold;
	        height:25px;
        }
        .btnBuscar:hover
        {
            border: 1px solid #9f8e39;
	        background: #f2f1b1 url('../../../Images/buscar_poa.png') no-repeat 0% center;
	        cursor:pointer; 
         }
         /*==== Fin Botones =====*/
        
</style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="titulo_poa">
        <asp:Label ID="Label1" runat="server" Text="Listado de Tesis"></asp:Label>
    </div>
    <div class="contorno_poa">
        <table width="100%">
            <tr>
                <td width="5%">
                    Año
                </td>
                <td width="10%">
                    <asp:DropDownList ID="ddlAnio" runat="server" Width="100%">
                    <%--<asp:ListItem Value="2017">2017</asp:ListItem>--%>
                    </asp:DropDownList>
                </td>
                <td width="23%" style="text-align:right">
                    Apellidos y Nombres / DNI / Cod. Univ. &nbsp;&nbsp;
                </td>
                <td width="32%">
                <asp:TextBox runat="server" ID="txtAutor"  Width="90%"></asp:TextBox>
<%--                    <asp:DropDownList ID="ddlEjercicio" runat="server" >
                    </asp:DropDownList>--%>
                </td>
                <td  width="10%" style="text-align:right">
                    Estado &nbsp;&nbsp;&nbsp;
                </td>
                <td  width="10%">
                    <asp:DropDownList ID="ddlEstado" runat="server"  Width="100%">
                     <asp:ListItem Value="T" Selected="True" >Todos</asp:ListItem>
                        <asp:ListItem Value="C">Con Url</asp:ListItem>
                        <asp:ListItem Value="S">Sin Url</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td  width="10%" align="center">
                    <asp:Button ID="btnBuscar" runat="server" Text="   Buscar" CssClass="btnBuscar" />
                </td>
            </tr>
        </table>
        <br />
        <asp:GridView ID="dgvTesis" runat="server" Width="100%" AutoGenerateColumns="False"
            DataKeyNames="codigo_tes" HeaderStyle-Height="20px" CellPadding="4" Font-Size="12px">
            <Columns>
                <asp:BoundField HeaderText="TESIS" DataField="titulo_Tes">
                    <HeaderStyle Width="50%" />
                    <ItemStyle HorizontalAlign="left" Width="40%" />
                </asp:BoundField>
                <asp:BoundField HeaderText="RESPONSABLE" DataField="Responsable">
                    <HeaderStyle Width="25%" />
                    <ItemStyle HorizontalAlign="left" Width="25%" />
                </asp:BoundField>
                <asp:BoundField HeaderText="URL" DataField="url_tes">
                    <HeaderStyle Width="20%" />
                    <ItemStyle HorizontalAlign="left" Width="20%" />
                </asp:BoundField>
                <asp:CommandField ShowSelectButton="true" ButtonType="Image"  
                    HeaderText="EDITAR" SelectImageUrl="../../../images/editar_poa.png" 
                    UpdateImageUrl="../../images/editar.gif" SelectText="Editar" >
                <HeaderStyle Width="5%" />
                <ItemStyle HorizontalAlign="Center" Width="5%" />
                </asp:CommandField>
            </Columns>
            <HeaderStyle BackColor="#3871b0" ForeColor="White" Height="25px" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <EmptyDataTemplate>
                No se Encontraron Registros</EmptyDataTemplate>
        </asp:GridView>
        <br />
        <asp:Label ID="lblMensajeFormulario" runat="server" Text=""></asp:Label>
        <br />
    </div>
    </form>
</body>
</html>
