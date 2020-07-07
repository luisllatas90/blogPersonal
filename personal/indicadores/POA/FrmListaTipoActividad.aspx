<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmListaTipoActividad.aspx.vb" Inherits="indicadores_POA_PROTOTIPOS_Registrar_POA" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" lang="es">
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
            $("#boton").click(function() {
                $(this).attr("data-target","#myModal")
                $("#mymodal").show("slow")
            })
        });
    </script>
    
    <style type="text/css">
        #myModal 
        {
          display: block; /* Es necesario mostrarlo como un bloque */
          position: absolute; /* Esto es obligatorio solo cuando sea una división principal */
          margin: auto;
          left: 0px;
          top: 0px;
          bottom: 0px; /* Si definimos esta opción estará en el centro del navegador (centro de la pantalla) */
          width: 600px; /* Si esto no se define no se visualizará los resultados en el navegador (Internet Explorer) */       
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="Medium" 
            Text="TIPO DE ACTIVIDAD"></asp:Label>
    
    </div>
    <div style="border: 1px solid #CCCCFF; padding-top: 5px; padding-right: inherit; padding-bottom: inherit; padding-left: inherit;">
        <table>
        <tr>
        <td width="150px" >Actividad</td>
        <td>
            <asp:TextBox ID="TextBox1" runat="server" Width="324px"></asp:TextBox>
            </td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
        <td colspan="2"><asp:Button ID="btnBuscar" runat="server" Text="   Buscar" CssClass="buscar2" /></td></td>
        </tr>
        <tr>
        <td colspan="6"><asp:Button ID="btnNuevo" runat="server" Text="   Agregar" CssClass="agregar2" /></td>
        </tr>
        <tr><td colspan="6"></td></tr>
    </table>
        <asp:GridView ID="GridView1" runat="server" Width="57%"
            AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField HeaderText="ABREVIATURA" />
                <asp:BoundField HeaderText="ACTIVIDAD" />
                <asp:CommandField ShowEditButton="True" />
                <asp:CommandField ShowDeleteButton="True" />
            </Columns>
        </asp:GridView>
    </div>
    
      <!-- Button trigger modal -->
<button type="button" id="boton" class="btn btn-primary btn-ms" data-toggle="modal" >
  Launch demo modal
</button>

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
        <tr><td>Plan Estratégico</td><td>-</td>
        </tr>
        <tr><td>Plan Operativo Anual</td><td>-</td>
        </tr>
        <tr><td>Objetivo Estrátegico</td><td>-</td>
        </tr>
        <tr><td>Indicador</td><td>-</td>
        </tr>
        <tr>
        <td>Observación : </td><td><textarea id="txtobservacion" cols="50" name="txtobservacion"></textarea></td>
        </tr>
        </table>
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-default" data-dismiss="modal">Cancelar</button>
        <asp:Button runat="server" Text="Guardar" class="btn btn-primary" />
      </div>
    </div>
  </div>
</div>

    </form>
</body>
</html>
