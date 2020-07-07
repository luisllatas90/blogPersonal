<%@ Page Language="VB" AutoEventWireup="false" CodeFile="avisoBloqueo.aspx.vb" Inherits="avisoBloqueo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta name="tipo_contenido" content="text/html;" http-equiv="content-type" charset="utf-8">
    <link href="assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        jQuery(document).ready(function() {

        });

        
        $(document).ready(function() {
           
        });

        //window.setTimeout($('#frmEstadoCuenta').submit(), 1000); 
    </script>

</head>
<body>
    <%--<form id="frmEstadoCuenta" action="https://10.10.1.61:9443/estadocuentaresumen.aspx"--%>
    <form id="frmEstadoCuenta">    
    <div class="main-content" style="padding-top: 0px; padding-left: 2px; padding-right: 2px;">
        <div class="panel panel-piluku">
            <div class="panel panel-danger">
                <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
                    <button type="button" class="close" data-dismiss="modal">
                        &times;</button>
                    <h4 class="modal-title">
                        <strong>¡Atención!</strong></h4>
                </div>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-8">
                        <!-- panel -->
                        <div class="panel panel-piluku">
                            <div class="panel-body">
                                <div class="alert alert-danger alert-dismissable">                                    
                                    <p>
                                        Estimado alumno <br />
                                        Actualmente el servicio no se encuentra disponible. Puedes acercarse a cualquier oficina o agente (BCP y BBVA) para realizar
                                        tu pago.
                                        <br /><br />
                                        <b>Tesorer&iacute;a USAT.</b>
                                    </p>
                                </div>
                            </div>
                        </div>
                        <!-- /panel -->
                    </div>
                    <!-- /col-md-4 -->
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
