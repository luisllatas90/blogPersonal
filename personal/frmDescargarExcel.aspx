<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmDescargarExcel.aspx.vb" Inherits="frmDescargarExcel" %>

<!DOCTYPE html>
<html lang="es">

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv="X-UA-Compatible" content="IE=7" />
    <meta http-equiv="X-UA-Compatible" content="IE=8" />
    <meta http-equiv="X-UA-Compatible" content="IE=10" />          
    <title>Descarga Documento Excel</title>

    <!-- Estilos externos -->
    <link rel="stylesheet" href="assets/bootstrap-4.1/css/bootstrap.min.css">
    <link rel="stylesheet" href="assets/smart-wizard/css/smart_wizard.min.css">
    <link rel="stylesheet" href="assets/bootstrap-select-1.13.1/css/bootstrap-select.min.css">    
    <link rel="stylesheet" href="assets/fontawesome-5.2/css/all.min.css">                    
    <link rel="stylesheet" href="Alumni/css/sweetalert/sweetalert2.min.css">     

    <!-- Estilos propios -->        
    <link rel="stylesheet" href="Alumni/css/estilos.css?1">

    <!-- Scripts externos -->
    <script src="assets/jquery/jquery-3.3.1.js"></script>
    <script src="assets/bootstrap-4.1/js/bootstrap.bundle.js"></script>
    <script src="assets/bootstrap-select-1.13.1/js/bootstrap-select.js"></script>            
    <script src="Alumni/js/popper.js"></script>    
    <script src="Alumni/js/sweetalert/es6-promise.auto.min.js"></script>
    <script src="Alumni/js/sweetalert/sweetalert2.js"></script>
    <script src="assets/iframeresizer/iframeResizer.contentWindow.min.js"></script>   
    
    <!-- Scripts propios -->
    <script src="Alumni/js/funciones.js?1"></script>    

    <script type="text/javascript">
        /* Mostrar mensajes al usuario. */
        function showMessage(message, messagetype) {              
            swal({ 
                title: message,
                type: messagetype ,
                confirmButtonText: "OK" ,
                confirmButtonColor: "#45c1e6"
            }).catch(swal.noop);
        }     
    </script>
</head>
<body>
    <form id="frmDescargarExcel" runat="server">
        
    </form>
</body>
</html>
