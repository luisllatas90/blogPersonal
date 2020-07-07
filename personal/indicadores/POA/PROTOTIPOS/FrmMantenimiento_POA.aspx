<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmMantenimiento_POA.aspx.vb" Inherits="indicadores_POA_PROTOTIPOS_Registrar_POA" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <link href="../../../css/estilo.css" rel="stylesheet" type="text/css" media="screen" />

    <style type="text/css">
        .style1
        {
            width: 218px;
        }
        .style2
        {
            width: 218px;
            height: 26px;
        }
        .style3
        {
            height: 26px;
        }
        .style4
        {
            width: 444px;
        }
        .style5
        {
            height: 26px;
            width: 444px;
        }
        .busqueda
        {
            border:1px;
            border-color:Green;   
         }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="Medium" 
            Text="REGISTRO DE POA"></asp:Label>
    
        <table style="width:100%;">
            <tr>
                <td class="style1">
                    PEI</td>
                <td class="style4">
                    <asp:DropDownList ID="DropDownList1" runat="server" Width="353px">
                    </asp:DropDownList>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style1">
                    CECO - FACULTAD - DIRECCION</td>
                <td class="style4">
                    <asp:DropDownList ID="DropDownList3" runat="server" Width="353px" 
                        Enabled="False">
                    </asp:DropDownList>
                &nbsp;</td>
                <td>
                    CARGA AL SELECCIONAR EL PEI</td>
            </tr>
            <tr>
                <td class="style2">
                    CECO - AREA O ESCUELA</td>
                <td class="style5">
                    <asp:DropDownList ID="DropDownList7" runat="server" Width="353px">
                    </asp:DropDownList>
                </td>
                <td class="style3">
                    CARGA AL SELECCIONAR EL PEI&nbsp; TODAS LAS AREAS</td>
            </tr>
            <tr>
                <td class="style2">
                    RESPONSABLE POA DE AREA</td>
                <td class="style5">
                    <asp:DropDownList ID="DropDownList6" runat="server" Width="353px">
                    </asp:DropDownList>
                </td>
                <td class="style3">
                </td>
            </tr>
            <tr>
                <td class="style1">
                    NOMBRE POA DE AREA</td>
                <td class="style4">
                    <asp:TextBox ID="TextBox2" runat="server" Width="354px"></asp:TextBox>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style2">
                    EJERCICIO PRESUPUESTAL</td>
                <td class="style5">
                    <asp:DropDownList ID="DropDownList8" runat="server" Width="202px">
                    </asp:DropDownList>
                </td>
                <td class="style3">
                </td>
            </tr>
            <tr>
                <td class="style1">
                    &nbsp;</td>
                <td class="style4">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
    
    </div>
    <asp:Button ID="Button1" runat="server" Text="Guardar" />
    <asp:Button ID="Button2" runat="server" Text="Cancelar" />
    <br />
    <br />
    <div style="border: 1px solid #6699FF; padding-top: 5px; padding-right: inherit; padding-bottom: inherit; padding-left: inherit;">
    <asp:label ID="lblbusqueda" runat="server">Busqueda - Plan Operativo Anual</asp:label> 
    <table>
        <tr>
        <td width="150px" >Plan Estratégico</td>
        <td><asp:DropDownList ID="ddlPlan" runat="server" Width="400"></asp:DropDownList></td>
        <td>&nbsp; Ejercicio Presupuestal&nbsp;</td>
        <td><asp:DropDownList ID="ddlEjercicio" runat="server" Width="100"></asp:DropDownList></td>
        </tr>
        <tr><td colspan="4"></td></tr>
    </table>
        <asp:GridView ID="GridView1" runat="server" Width="80%" 
            AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField HeaderText="PLAN OPERATIVO ANUAL" />
                <asp:BoundField HeaderText="CENTRO DE COSTO" />
                <asp:BoundField HeaderText="RESPONSABLE" />
                <asp:BoundField HeaderText="EJERCICIO " />
                <asp:CommandField ShowEditButton="True" />
                <asp:CommandField ShowDeleteButton="True" />
            </Columns>
        </asp:GridView>
    </div>
    </form>
</body>
</html>
