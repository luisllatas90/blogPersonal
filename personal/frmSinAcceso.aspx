<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmSinAcceso.aspx.vb" Inherits="frmSinAcceso" %>

<!DOCTYPE html>
<html lang="es">

<head>
	<meta charset="UTF-8">
    <title>Acceso denegado</title>
    
    <!-- Estilos externos -->
    <link rel="stylesheet" href="assets/bootstrap-4.1/css/bootstrap.min.css">   
    <link rel="stylesheet" href="Alumni/css/sweetalert/sweetalert2.min.css"> 
    
    <!-- Scripts externos -->
    <script src="assets/jquery/jquery-3.3.1.js"></script>
    <script src="assets/bootstrap-4.1/js/bootstrap.bundle.js"></script>             
    <script src="Alumni/js/popper.js"></script>    
    <script src="Alumni/js/sweetalert/es6-promise.auto.min.js"></script>
    <script src="Alumni/js/sweetalert/sweetalert2.js"></script>   
    
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
        
        function redirigirLogin(message, messagetype){
            setTimeout(function() {
                swal({ 
                    title: message,
                    type: messagetype ,
                    confirmButtonText: "OK" ,
                    confirmButtonColor: "#45c1e6"     
                }).then(function() {
                    window.top.location.href = "acceder.asp";                                                     
                }).catch(swal.noop); 
            }, 1000);            
        }
    </script>
</head>
<body>
    <form id="frmSinAcceso" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div style="position: absolute; left: 57%; top: 45%; margin-left: -285px; margin-top: -190px;">	
            <h1> Acceso Denegado </h1>
        </div>	
        <div style="position: absolute; left: 50%; top: 50%; margin-left: -285px; margin-top: -190px;">	
            <img src="../Aviso.png" alt="Acceso denegado">
        </div>
    </form>
</body>
</html>
