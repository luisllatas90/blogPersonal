<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmAsignaAportePoa.aspx.vb" Inherits="indicadores_POA_FrmAsignaAportePoa" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Asignación de Aporte POA</title>
    <link href="css/estilo_poa.css" rel="stylesheet" type="text/css" media="screen" />
    
    <!-- Compatibilidad IE con JQuery / Bootstrap -->
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    
    <!-- Librería jQuery requerida por los plugins de JavaScript -->
    <script src="Jquery/jquery-1.12.3.min.js" type="text/javascript"></script>
    
    <!-- CSS de Bootstrap -->
    <link href="bootstrap-3.3.7-dist/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    
    <!-- Optional theme -->
    <link href="bootstrap-3.3.7-dist/css/bootstrap-theme.min.css" rel="stylesheet" type="text/css" />
    
    <!-- Latest compiled and minified JavaScript -->
    <script src="bootstrap-3.3.7-dist/js/bootstrap.min.js" type="text/javascript"></script>
    
    <script type="text/javascript">
        "use strict";
        $(document).ready(function() {
        
            $("#boton").click(function() {
                alerta();
            });
        });
        function Sumar(nro) {
            var a = 0;
            $("#TotalInd" + nro).val(0);
            $(".form-control").each(function() {
                if ($(this).attr("nro") == nro) {
                    if ($(this).val() != "") {
                        $(this).val(parseFloat($(this).val()).toFixed(2));
                        a += parseFloat($(this).val());
                    } else {
                    $(this).val("0.00");
                    }
                }
            });
            $("#TotalInd" + nro).val(a.toFixed(2));
            if (a == 100) {
                $("#TotalInd" + nro).css("background", "#D8F3D3");
            } else {
            $("#TotalInd" + nro).css("background", "#FFD5D5");
            } 
        }
        

        function Guardar(nro) {
            $(".form-control").each(function() {
                if ($(this).attr("nro") == nro) {
                    alert($(this).val())
                }
            });
        }
        
        function alerta() {
            var pei = $("#ddlPlan").val();
            var ejp = $("#ddlEjercicio").val();
            var poa = $("#ddlPoa").val();
            var estado = $("#ddlEstado").val();

            $.ajax({
                type: "POST",
                url: "Procesar.aspx",
                data: { "param0": pei, "param1": ejp, "param2": poa, "param3": estado },
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                syn: false,
                success: function(data) {
                    $("#ResultJson").html(data)
                },
                error: function(result) {
                    console.log(result);
                }
            });
        }

    </script>
    <style type="text/css">
        .contorno_poa
        {
            position:relative;
            top:25px;
            border:1px solid #C0C0C0;
            left:10px;
            right:18px;
            width:98%;
            padding-left:6px;
            padding-top:17px;
            padding-right:6px;
            padding-bottom:5px

        }
        .titulo_poa 
        {
            position:absolute;
            top:13px;
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
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"> </asp:ScriptManager>
     <div class="titulo_poa">
       <asp:Label ID="Label1" runat="server" Text="Asignación de Aporte Estrátegico"></asp:Label>
    </div>
    <div class="contorno_poa">
        <table width="100%" id="tabla" runat="server">
        <tr style="height:30px;">
        <td width="140px" >Plan Estratégico</td>
        <td width="510px"><asp:DropDownList ID="ddlPlan" runat="server" Width="500" AutoPostBack="true"></asp:DropDownList></td>
        <td width="50px"></td>
        <td width="140px">Ejercicio Presupuestal</td>
        <td><asp:DropDownList ID="ddlEjercicio" runat="server" Width="140" AutoPostBack="true"></asp:DropDownList></td>
        <td><asp:Button ID="btnBuscar" runat="server" Text="   Buscar" CssClass="btnBuscar" /></td>
        </tr>
        <tr>
        <td>Plan Operativo Anual</td>
        <td>
        <asp:DropDownList ID="ddlPoa" runat="server" Width="500" AutoPostBack="true">
        <asp:ListItem Value="0">--SELECCIONE--</asp:ListItem>
        </asp:DropDownList> 
        </td>
        <td width="50px"></td>
        <td>Estado de Asignación</td>
         <td>
         <asp:DropDownList ID="ddlEstado" runat="server" Width="140" >
            <asp:ListItem Value="P">Pendientes</asp:ListItem>
            <asp:ListItem Value="A">Asignados</asp:ListItem>
            <asp:ListItem Value="T">Todos</asp:ListItem>
        </asp:DropDownList>
        </td>
        <td><input type="button" value="   Alerta" id="boton" name="boton" class="btnBuscar" /></td>
        </tr>
        <tr>
        <td colspan="6">
            <div runat="server" id="aviso">
                <asp:Label ID="lblrpta" runat="server" Font-Bold="true"></asp:Label>
            </div>
        </td>
        </tr>
        </table>
        <br />
        <div id="ResultJson" ></div>
    </div>
    </form>
</body>
</html>
