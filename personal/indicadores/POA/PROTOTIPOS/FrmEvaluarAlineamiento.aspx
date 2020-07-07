<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmEvaluarAlineamiento.aspx.vb" Inherits="indicadores_POA_PROTOTIPOS_Registrar_POA" %>

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



          $("#btnObservar").click(function() {
              $("#Aprobar").css("display", "none")
              $("#tabobservacion").css("display", "block")
              $("#txtobservacion").val("")
              $("#Observar").css("display", "block")
          });

          $("img").click(function() {
              //alert($(this).attr("val"))
              $("#Aprobar").css("display", "block")
              $("#tabobservacion").css("display", "none")
              $("#Observar").css("display", "none")
              $(this).attr("data-target", "#myModal")
              $("#lblacp").text($(this).attr("acp"))
              $("#lblind").text($(this).attr("ind"))
              $("#hdcodigo_acp").val($(this).attr("cod_acp"))
              $("#mymodal").show()
          });

          $("#btncancelar").click(function() {
              $("#Aprobar").css("display", "block")
              $("#tabobservacion").css("display", "none")
              $("#Observar").css("display", "none")
              $("#txtobservacion").val("")
          });
      });
    </script> 
    
<style type="text/css">
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
        top:27px;
        border:1px solid #C0C0C0;
        left:8px;
        right:18px;
        width:98%;
        padding-left:4px;
        padding-top:20px;

    }
    .mensajeExito
    {
        background-color: #d9edf7;
        border: 1px solid #808080;  
        font-weight:bold;
        color:#31708f;
        height:18px;
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
        padding:2px;
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
    table th
    {
        color:#FFF;
        background-color:#3871b0;
        font-weight:bold;
        border-collapse:collapse;
        border-width:0px;
        border-style:solid;
        border-color:rgb(169,169,169);
        font-size:9px;
        text-align:center;
        vertical-align:middle;
        padding:2px;
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
            font-size:10px;
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
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="titulo_poa">
        <asp:Label ID="Label1" runat="server"
            Text="Evaluación Alineamiento de Plan Operativo Anual"></asp:Label>
    </div>
    <div class="contorno_poa">
        <table width="100%" id="tabla" runat="server">
        <tr style="height:30px;">
        <td width="100px" >Plan Estratégico</td>
        <td width="510px"><asp:DropDownList ID="ddlplan" runat="server" Width="500" AutoPostBack="true"></asp:DropDownList></td>
        <td>Ejercicio Presupuestal</td>
        <td><asp:DropDownList ID="ddlEjercicio" runat="server" Width="140"></asp:DropDownList></td>
        <td>Estado</td>
        <td>
        <asp:DropDownList ID="ddlestado" runat="server">
            <asp:ListItem Value="P">Pendientes</asp:ListItem>
            <asp:ListItem Value="A">Asignados</asp:ListItem>
            <asp:ListItem Value="T">Todos</asp:ListItem>
        </asp:DropDownList>
        </td >
        <td><asp:Button ID="btnBuscar" runat="server" Text="   Buscar" CssClass="buscar2" /></td>
        </tr>
        <tr>
        <td>Plan Operativo Anual</td>
        <td>
        <asp:DropDownList ID="ddlPoa" runat="server" Width="500">
        <asp:ListItem Value="0">--SELECCIONE--</asp:ListItem>
        </asp:DropDownList> 
        </td>
        </tr>
        </table>
        <br />
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
        <h4 class="modal-title" id="myModalLabel">Evaluación de Alineamiento</h4>
      </div>
      <div class="modal-body">
        <table>
        <tr><td style="width:130px;" >Programa/Proyecto  </td>
        <td><asp:HiddenField runat="server" ID="hdcodigo_acp" /><div class="nombre_prog"><asp:label ID="lblacp" runat="server"></asp:label></div></td>
        </tr>
        </table>
        <%--<tr><td>Indicador  </td>
        <td><div class="indicador"><asp:Label ID="lblind" runat="server"></asp:Label></div></td>
        </tr>--%>
        <table id="tabobservacion" style="display:none">
        <tr>
        <td td style="width:130px;">Observación  </td>
        <td><textarea id="txtobservacion" runat="server" cols="50" rows="5" name="txtobservacion"></textarea></td>
        </tr>
        </table>
      </div>
       <div class="modal-footer" id="Aprobar">
        <button type="button" class="btn btn-danger" id="btnObservar" >Observar</button>
        <asp:Button ID="btnAprobacion" runat="server" Text="Aprobar" class="btn btn-primary" OnClientClick="return confirm('Esta Seguro que Desea Aprobar Alineamiento?.')" />
      </div>
      <div class="modal-footer" id="Observar" style="display:none">
        <button type="button" class="btn btn-default" id="btncancelar">Cancelar</button>
        <asp:Button ID="btnObservacion" runat="server" Text="Guardar" class="btn btn-primary" OnClientClick="return confirm('Esta Seguro que Desea Registrar Observación?.')" />
      </div>
    </div>
  </div>
</div>
    </form>
</body>
</html>
