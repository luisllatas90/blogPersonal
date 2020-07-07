<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmListaEvaluarAlineamiento_V2.aspx.vb" Inherits="indicadores_POA_PROTOTIPOS_Registrar_POA" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <link href="../../css/estilo.css" rel="stylesheet" type="text/css" media="screen" />
    
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

          //          $("#btnObservar").click(function() {
          //              $("#Aprobar").css("display", "none")
          //              $("#tabobservacion").css("display", "block")
          //              $("#txtobservacion").val("")
          //              $("#Observar").css("display", "block")
          //          });

          $("img").click(function() {
              //              alert($(this).attr("val"))
              //              $("#Aprobar").css("display", "block")
              //              $("#tabobservacion").css("display", "none")
              //              $("#Observar").css("display", "none")
              if ($(this).attr("tipo") == "O") {
                  $(this).attr("data-target", "#myModal");
                  $("#lblacp").text($(this).attr("acp"))
                  $("#lblind").text($(this).attr("ind"))
                  $("#hdcodigo_acp").val($(this).attr("cod_acp"))
                  $("#mymodal").show();

              } else if ($(this).attr("tipo") == "A") {
                  $("#hdcodigo_acp").val($(this).attr("cod_acp"))
                  $("#btnAprobacion").click()
              }

          });

          $("#btncancelar").click(function() {
              //              $("#Aprobar").css("display", "block")
              //              $("#tabobservacion").css("display", "none")
              //              $("#Observar").css("display", "none")
              $("#txtobservacion").val("");
              $("#mymodal").hide()
          });
      });
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
            cursor:default;

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
    .mensajeExito
    {
        background-color: #d9edf7;
        border: 1px solid #808080;  
        font-weight:bold;
        color:#31708f;
        height:22px;
        padding:3px;
    } 
    .mensajeEliminado
    {
        color:#8a6d3b;
        background-color:#fcf8e3;
        border: 1px solid #C5BE51;
        font-weight:bold;
        height:22px;
        padding:3px;
    }   
    .mensajeError
    {
        background-color: #f2dede;
        border: 1px solid #E9ABAB;
        font-weight:bold;
        color:#a94442;
        height:22px;
        padding:3px;
    }
    
   .Dias
    {
        color:red;
    }
   
    .celda_combinada
    {
        border-color:rgb(169,169,169);
        border-style:solid;
        border-width:1px;
        padding:3px;
        cursor:default;
        font-size:9.2px;
    }
    .menuporelegir
    {
        border: 1px solid #808080;
        background-color: #FFCC66;
    }
    .menu_elegido
    {
        font-size:xx-small;
        border: 1px solid #808080;
        background-color: #FFCC66;
        
    }
        
    #div1 {
         overflow:scroll;
         height:625px;
         width:99.5%;
    }
    /* Centrar Modal de M... en IE */
   #myModal 
        {
          display: block; /* Es necesario mostrarlo como un bloque */
          position: absolute; /* Esto es obligatorio solo cuando sea una división principal */
          margin: auto;
          right:10%;
          left: 0px;
          top: 0px;
          bottom: 0px; /* Si definimos esta opción estará en el centro del navegador (centro de la pantalla) */
          width: 650px; /* Si esto no se define no se visualizará los resultados en el navegador (Internet Explorer) */       
     }
     /*--*/

       .nombre_prog
        {
            color:#aa6708;
            font-weight:bold;
            padding-top:3px;
            padding-bottom:3px;
            padding-left:4px;
            font-size:12px;
        }
        .indicador
        {
            color:#468847;
            font-weight:bold;
            padding-top:3px;
            padding-bottom:3px;
            padding-left:4px;
            font-size:10px;
        }
        .th_evaluacion
        {
            font-size: 9px; 
            font-weight:bold;
            border: rgb(169,169,169) 1px solid;
            color: white; 
            background-color: #3871b0;
            cursor:auto;
            text-align:center;
            vertical-align:middle;
            padding:2px;
        }
        .btnCancelar
        {
            border: 1px solid #e0a6af;
	        background: #f4dfe2 url('../../Images/menus/noconforme_small.gif') no-repeat 0% center;
	        color:Red;
	        height:32px;
	        font-weight:bold;
	        font-family: Verdana, Geneva, Arial, Helvetica, sans-serif;
	        font-size:8pt;
        }
        .btnCancelar:hover
        {
            border: 1px solid #cc6d7c;
	        cursor:pointer;
         }
         
        .btnGuardar
        {
            border: 1px solid #6dcc8e;
	        background: #ccedd7 url('../../images/guardar.gif') no-repeat 0% center;
	        color:Green;
	        font-weight:bold;
	        height:32px;
	        font-family: Verdana, Geneva, Arial, Helvetica, sans-serif;
	        font-size:8pt;
        }
        .btnGuardar:hover
        {
            border: 1px solid #189252;
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
        table tr
        {
            cursor:default;
        }
        a:link
        {
            font-weight:bold;
            color:#0d2d4a;
        }
         
        a:visited
        {
            font-weight:bold;
            color:#0d2d4a;
        }  
        a:hover
        {
            font-weight:bold;
            color:#cd8400;
        }    
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:HiddenField runat="server" ID="hdfila" Value="-1" />
    <div class="titulo_poa">
        <asp:Label ID="Label1" runat="server"
            Text="Evaluación Alineamiento de Plan Operativo Anual"></asp:Label>
    </div>
    <div class="contorno_poa">
        <table width="100%" id="tabla" runat="server">
        <tr style="height:30px;">
        <td width="140px" >Plan Estratégico</td>
        <td width="510px"><asp:DropDownList ID="ddlplan" runat="server" Width="500" AutoPostBack="true"></asp:DropDownList></td>
        <td width="50px"></td>
        <td width="140px">Ejercicio Presupuestal</td>
        <td><asp:DropDownList ID="ddlEjercicio" runat="server" Width="140" AutoPostBack="true"></asp:DropDownList></td>
        <td><asp:Button ID="btnBuscar" runat="server" Text="   Buscar" CssClass="btnBuscar" /></td>
        </tr>
        <tr>
        <td>Plan Operativo Anual</td>
        <td>
        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
        <ContentTemplate>
        <asp:DropDownList ID="ddlPoa" runat="server" Width="500">
        <asp:ListItem Value="0">--SELECCIONE--</asp:ListItem>
        </asp:DropDownList> 
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlplan" EventName="SelectedIndexchanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlEjercicio" EventName="SelectedIndexchanged" />
        </Triggers>
        </asp:UpdatePanel>
        </td>
        <td width="50px"></td>
        <td>Estado de Actividad</td>
         <td>
         <asp:DropDownList ID="ddlestado" runat="server" Width="140" >
            <asp:ListItem Value="6">Pendientes</asp:ListItem>
            <asp:ListItem Value="3">Observados</asp:ListItem>
            <asp:ListItem Value="9">Aprobados</asp:ListItem>
            <asp:ListItem Value="T">Todos</asp:ListItem>
        </asp:DropDownList>
        </td>
        <td></td>
        </tr>
        <tr style="height:30px">
        <td>Situacion  </td>
        <td colspan="5">
            <img alt="" src="" style="background-color:#87CEEB" width="8px" height="8px" /><asp:Label ID="Label2" runat="server"> Pendiente</asp:Label>
            &nbsp;&nbsp;
            <img alt="" src="" style="background-color:#F08080" width="8px" height="8px" /><asp:Label ID="Label4" runat="server"> Observado</asp:Label>
            &nbsp;&nbsp;
            <img alt="" src="" style="background-color:#90EE90" width="8px" height="8px" /><asp:Label ID="Label5" runat="server"> Aprobado</asp:Label>
        </td>
        </tr>
        <tr>
            <td colspan="6">
                <div runat="server" id="aviso">
                    <asp:Label ID="lblrpta" runat="server" Font-Bold="true"></asp:Label>
                </div>
            </td> 
        </tr>
        <tr style="height:10px;">
        <td colspan="6"></td>
        </tr>
        </table>

        <div id='TablaAlineamiento' width="100%" runat="server">       
        </div>
        
            <asp:TreeView ID="treeAlineamiento" runat="server" ExpandDepth="0" 
                Font-Size="XX-Small" MaxDataBindDepth="4">
                <Nodes>
                </Nodes>
                <HoverNodeStyle CssClass="menuporelegir" />
                <RootNodeStyle Font-Bold="true" />
            </asp:TreeView>
 
        </div>
        <!-- Modal -->
<div class="modal fade" id="myModal" tabindex="-1" role="dialog" style="display:none" >
  <div class="modal-dialog" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
        <h4 class="modal-title" id="myModalLabel">Observación de Programa / Proyecto</h4>
      </div>
      <div class="modal-body">
        <table>
        <tr><td style="width:130px;" >Programa/Proyecto  </td>
        <td><asp:HiddenField runat="server" ID="hdcodigo_acp" /><div class="nombre_prog"><asp:label ID="lblacp" runat="server"></asp:label></div></td>
        </tr>
        <tr>
        <td style="width:130px;">Observación  </td>
        <td><textarea id="txtobservacion" runat="server" cols="50" rows="5" name="txtobservacion" ></textarea></td>
        </tr>
        </table>
        <%--<tr><td>Indicador  </td>
        <td><div class="indicador"><asp:Label ID="lblind" runat="server"></asp:Label></div></td>
        </tr>--%>
        <%--<table id="tabobservacion">
        <tr>
        <td style="width:130px;">Observación  </td>
        <td><textarea id="txtobservacion" runat="server" cols="50" rows="5" name="txtobservacion"></textarea></td>
        </tr>
        </table>--%>
      </div>
       <div class="modal-footer" id="Aprobar" style="display:none">
        <button type="button" class="btn btn-danger" id="btnObservar" >Observar</button>
        <asp:Button ID="btnAprobacion" runat="server" Text="Aprobar" class="btn btn-primary" OnClientClick="return confirm('Esta Seguro que Desea Aprobar Alineamiento?.')" />
      </div>
      <div class="modal-footer" id="Observar" >
        <button type="button" class="btnCancelar" data-dismiss="modal" id="btncancelar">&nbsp;&nbsp;Cancelar</button>
        <asp:Button ID="btnObservacion" runat="server" Text="   Guardar" class="btnGuardar" OnClientClick="return confirm('Esta Seguro que Desea Registrar Observación?.')" />
      </div>
    </div>
  </div>
</div>
    </form>
</body>
</html>
