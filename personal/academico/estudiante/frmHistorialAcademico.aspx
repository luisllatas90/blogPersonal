<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmHistorialAcademico.aspx.vb" Inherits="academico_estudiante_frmHistorialAcademico" %>

<!DOCTYPE html>
<html lang="es">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv="X-UA-Compatible" content="IE=7" />
    <meta http-equiv="X-UA-Compatible" content="IE=8" />
    <meta http-equiv="X-UA-Compatible" content="IE=10" />       
    <title>Historial Académico</title>

    <!-- Estilos externos -->
    <link rel="stylesheet" href="../../assets/bootstrap-4.1/css/bootstrap.min.css">
    <link rel="stylesheet" href="../../assets/smart-wizard/css/smart_wizard.min.css">
    <link rel="stylesheet" href="../../assets/bootstrap-select-1.13.1/css/bootstrap-select.min.css">    
    <link rel="stylesheet" href="../../assets/fontawesome-5.2/css/all.min.css">                 
    <link rel="stylesheet" href="../../Alumni/css/sweetalert/sweetalert2.min.css"> 
    
    <!-- Estilos propios -->        
    <link rel="stylesheet" href="../../Alumni/css/estilos.css">

    <style>
        .totales{
            font-size: 0.8rem; 
            font-weight: bold;
            font-family: Verdana, Geneva, Tahoma, sans-serif;
        }

        .table-datos-1{             
            font-weight: bold;
            text-align: right !important;
        }

        .table-datos-2{             
            font-weight: bold;
            text-align: left !important;
            color: blue;
        }        
    </style>

    <!-- Scripts externos -->
    <script src="../../assets/jquery/jquery-3.3.1.js"></script>
    <script src="../../assets/bootstrap-4.1/js/bootstrap.bundle.js"></script>
    <script src="../../assets/bootstrap-select-1.13.1/js/bootstrap-select.js"></script>            
    <script src="../../Alumni/js/popper.js"></script>    
    <script src="../../Alumni/js/sweetalert/es6-promise.auto.min.js"></script>
    <script src="../../Alumni/js/sweetalert/sweetalert2.js"></script>
    
    <!-- Scripts propios -->
    <script src="../../Alumni/js/funciones.js?1"></script>    

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
        
        function AlternarLoading(retorno, elemento) {
            var $loadingGif ;
            var $elemento ;

            switch (elemento) {
                case 'Lista':
                    $loadingGif = $('.loader');   
                    $elemento = $('#udpLista');               
                    break;                                 

            }

            if ($loadingGif != undefined) {
                if (!retorno) {
                    $loadingGif.fadeIn('slow');  
                    if ($elemento != undefined) {
                        $elemento.addClass("oculto");
                    }                     
                } else {
                    $loadingGif.fadeOut('slow');
                    if ($elemento != undefined) {
                        $elemento.removeClass("oculto");
                    }                    
                }
            } 
        }  
        
        function imprimir(modo,panel,titulo){
            if (modo=="N"){
                window.document.title=titulo
                window.print()
            }
            else{
                window.parent.frames[panel].document.title=titulo
                window.parent.frames[panel].focus()
                window.parent.frames[panel].print()
            }
        }

        function darclick(field, event) {
            var keyCode = event.keyCode ? event.keyCode : event.which ? event.which : event.charCode;
            if (keyCode == 13) {
                var obj = document.getElementById("<%=btnBuscar.ClientID%>");
                obj.click();
            }
        }

        function Enter(field, event) {
            var keyCode = event.keyCode ? event.keyCode : event.which ? event.which : event.charCode;
            if (keyCode == 13) {
                var i;
                for (i = 0; i < field.form.elements.length; i++)
                    if (field == field.form.elements[i])
                        break;
                i = (i + 1) % field.form.elements.length;
                field.form.elements[i].focus();
                return false;
            }
            else
                return true;

        }        
    </script>
</head>
<body>
    <div class="loader"></div>

    <form id="frmHistorialAcademico" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="udpScripts" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false"></asp:UpdatePanel>

        <div class="container-fluid">
            <div class="card div-title">                        
                <div class="row title">HISTORIAL ACADÉMICO</div>
            </div>
            <div class="tab-content" id="contentTabs">
                <div class="tab-pane show active" id="listado" role="tabpanel" aria-labelledby="listado-tab">
                    <div class="panel-cabecera">
                        <asp:UpdatePanel ID="udpFiltros" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                            <ContentTemplate>
                                <div class="card">
                                    <div class="card-body">
                                        <div class="row">
                                            <label for="txtCodigoFiltro" class="col-sm-2 form-control-sm">Código Universitario:</label>
                                            <div class="col-sm-3">                                                                                
                                                <asp:TextBox ID="txtCodigoFiltro" runat="server" CssClass="form-control form-control-sm uppercase" AutoComplete="off"
                                                    onKeypress="return Enter(this,event)"   onKeydown="return darclick(this, event)"/>
                                            </div>                                            
                                        </div>
                                        <hr/>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <asp:LinkButton ID="btnBuscar" runat="server" CssClass="btn btn-accion btn-celeste">
                                                    <i class="fa fa-sync-alt"></i>
                                                    <span class="text">Buscar</span>
                                                </asp:LinkButton> 
                                                <asp:LinkButton ID="btnImprimir" runat="server" CssClass="btn btn-accion btn-azul"
                                                    OnClientClick="imprimir('N');">
                                                    <i class="fa fa-print"></i>
                                                    <span class="text">Imprimir</span>
                                                </asp:LinkButton> 
                                                <asp:LinkButton ID="btnExportar" runat="server" CssClass="btn btn-accion btn-verde">
                                                    <i class="fa fa-file-excel"></i>
                                                    <span class="text">Exportar</span>
                                                </asp:LinkButton>                                                                                                                                                
                                            </div>
                                        </div>                                          
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <asp:UpdatePanel ID="udpLista" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                        <ContentTemplate>
                            <div id="tblDatos" runat="server" class="table-responsive">

                            </div>                                                        
                            <div id="tblHistorial" runat="server" class="table-responsive">

                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </form>

    <script type="text/javascript">
        var controlId = '';
        /* Ejecutar funciones una vez cargada en su totalidad la página web. */
        $(document).ready(function() {             
            /*Ocultar cargando*/   
            $('.loader').fadeOut("slow");
        });    
        
        /* Mostrar y ocultar gif al realizar un procesamiento. */
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(function (sender, args) {
            var elem = args.get_postBackElement();
            controlId = elem.id;

            switch (controlId) {                
                case 'btnBuscar':
                    AlternarLoading(false, 'Lista');                      
                    break; 
                                    
            }
        });   
        
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function (sender, args) {
            var error = args.get_error();
            if (error) {                
                AlternarLoading(true, '');               
                return false;
            }

            switch (controlId) {                                
                case 'btnBuscar':
                    AlternarLoading(true, 'Lista');                                    
                    break;   
                            
            }
        });         
    </script>
</body>
</html>
