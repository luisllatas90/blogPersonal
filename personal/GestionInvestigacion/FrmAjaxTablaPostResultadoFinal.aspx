<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmAjaxTablaPostResultadoFinal.aspx.vb" Inherits="GestionInvestigacion_FrmAjaxTablaPostResultadoFinal" %>

<!DOCTYPE html>
<html>
<head>
    <%--Compatibilidad con IE--%>

<script type="text/javascript" src='js/TablaResultadoFinal.js'></script>

    <script type="text/javascript">
        $(document).ready(function() {
            $('[data-toggle="popover"]').popover(); 

        });

</script>
</head>
<body>

    <table id="tPostulacion" name="tPostulacion" class="display dataTable cell-border" width="100%">
        <thead>
            <tr>                             
                 <th style="width:5%;text-align:center;">N°</th>                             
                 <th style="width:45%;text-align:center;">TITULO</th>
                 <th style="width:20%;text-align:center;">RESPONSABLE</th>
                 <th style="width:10%;text-align:center;">NOTA FINAL</th>
                 <th style="width:10%;text-align:center;">AGREGAR</th>                             
                 <th style="width:10%;text-align:center;">GANADOR</th>                             
             </tr>
        </thead>   
        <tbody id ="tbPostulacion" runat ="server">
        </tbody>                             
            <tfoot>
            <tr>
            <th colspan="6"></th>
            </tr>
        </tfoot>
    </table>
</body>
</html>
