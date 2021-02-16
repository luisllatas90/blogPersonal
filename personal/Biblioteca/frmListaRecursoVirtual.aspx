<%@ Page Language="VB" EnableEventValidation="false" AutoEventWireup="false" CodeFile="frmListaRecursoVirtual.aspx.vb" Inherits="Biblioteca_frmListaRecursoVirtual" %>
<!DOCTYPE html>
<html lang="es">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv="X-UA-Compatible" content="IE=7" />
    <meta http-equiv="X-UA-Compatible" content="IE=8" />
    <meta http-equiv="X-UA-Compatible" content="IE=10" />    
    <title>Biblioteca Virtual</title>

    <!-- Estilos externos -->
    <link rel="stylesheet" href="../assets/bootstrap-4.1/css/bootstrap.min.css">
    <link rel="stylesheet" href="../assets/fontawesome-5.2/css/all.min.css">
    <link rel="stylesheet" href="../Alumni/css/sweetalert/sweetalert2.min.css">

    <style>
        .loader {
            position: fixed;
            left: 0px;
            top: 0px;
            width: 100%;
            height: 100%;
            z-index: 9999;
            background: url('../Alumni/img/loading.gif') 50% 50% no-repeat rgb(249, 249, 249);
            opacity: .8;
        }

        .card-header{
            background-color: #E33439;
            font-weight: bold;
            color: white;
        }

        .card-image {
            text-align: center;
            padding-top: 10px;
        }
    </style>

    <!-- Scripts externos -->
    <script src="../assets/jquery/jquery-3.3.1.js"></script>
    <script src="../assets/bootstrap-4.1/js/bootstrap.bundle.js"></script>
    <script src="../Alumni/js/popper.js"></script>    
    <script src="../Alumni/js/sweetalert/es6-promise.auto.min.js"></script>
    <script src="../Alumni/js/sweetalert/sweetalert2.js"></script>

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
    <div class="loader"></div>

    <form id="frmListaRecursoVirtual" runat="server"> 
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="udpScripts" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false"></asp:UpdatePanel>

        <asp:UpdatePanel ID="udpLista" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
            <ContentTemplate>
                <asp:LinkButton ID="btnContar" runat="server" CssClass="btn btn-accion btn-celeste" Visible="false">
                    <i class="fa fa-sync-alt"></i>
                    <span class="text">Contar</span>
                </asp:LinkButton> 

                <div id="divRecursosVirtuales" class="card" runat="server">

                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>

    <script type="text/javascript">
        /* Ejecutar funciones una vez cargada en su totalidad la página web. */
        $(document).ready(function() {   
            /*Ocultar cargando*/   
            $('.loader').fadeOut("slow");
        });

        function ContarAccesos(codigo_biv){
            __doPostBack('btnContar', codigo_biv);
        }

        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(function (sender, args) {

        });
    </script>
</body>
</html>
