<%@ Page Language="VB" AutoEventWireup="false" CodeFile="index2020.aspx.vb" Inherits="index2020" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <!-- Global site tag (gtag.js) - Google Analytics -->
    <script async src="https://www.googletagmanager.com/gtag/js?id=UA-171567264-1"></script>
    <script>
        window.dataLayer = window.dataLayer || [];
        function gtag() { dataLayer.push(arguments); }
        gtag('js', new Date());

        gtag('config', 'UA-171567264-1');
    </script>  
    
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <meta name="google" value="notranslate" />
    <link rel="icon" href="css/favicon.ico"> 
    <title>Campus Virtual USAT:<%=session("gdescri_apl")%></title>
    
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv='X-UA-Compatible' content='IE=7' />
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />

    <link href="css/bootstrap.min.css" rel="Stylesheet" type="text/css" />
    <script src="js/jquery.min.js" type="text/javascript"></script>
    <script src="js/bootstrap.min.js" type="text/javascript"></script>

    <link href="../assets/fontawesome-5.2/css/regular.min.css" rel="stylesheet" type="text/css" />
    <script src="../assets/fontawesome-5.2/js/all.min.js" type="text/javascript"></script>
    
    <link href="css/side-menu.css" rel="Stylesheet" type="text/css" />
    
    <script type="text/javascript">

        function fc_AbrirPagina(enlace) {
            //console.log(enlace);
            $('#fraPrincipal').prop('src', enlace);
            //$('#fraPrincipal').prop('src', '../../personal/GestionCurricular/FrmMiembrosComite.aspx');
        }

        function resizeIframe(obj) {
            obj.style.height = obj.contentWindow.document.documentElement.scrollHeight + 'px';
        }
    
    </script>
    
     <style type="text/css">
    /* Remove the navbar's default margin-bottom and rounded borders */ 
    .navbar {
      margin-bottom: 0;
      border-radius: 0;
    }
    
    /* Set height of the grid so .sidenav can be 100% (adjust as needed) */
    /*.row.content {height: 450px}*/
    
    /* Set gray background color and 100% height */
    .sidenav {
      padding-top: 20px;
      background-color: #f1f1f1;
      height: 100%;
    }
    
    /* Set black background color, white text and some padding */
    footer {
      background-color: #555;
      color: white;
      padding: 15px;
    }
    
    /* On small screens, set height to 'auto' for sidenav and grid */
    /*@media screen and (max-width: 767px) {
      .sidenav {
        height: auto;
        padding: 15px;
      }
      .row.content {height:auto;} 
    }*/
    
    .navbar-inverse
    {
    	background-color: #e33439;
    	border-color: #e33439;
    }
    
    .navbar-inverse .navbar-brand
    {
    	color: #fff;
    }
    
    .navbar-inverse .navbar-nav > li > a
    {
    	color: #fff;
    }
    
    footer 
    {
    	background-color: #e33439;
    }
    	
    .navbar-inverse .navbar-toggle:focus, .navbar-inverse .navbar-toggle:hover 
    {
    	background-color: #e33439;
    }
    
    .navbar-inverse .navbar-toggle 
    {
        border-color: #fff;
    }
    
    .navbar-inverse .navbar-collapse, .navbar-inverse .navbar-form 
    {
        border-color: #fff;
    }
    
  </style>

  
</head>
<body>

    <nav class="navbar navbar-inverse navbar-fixed-top">
        <div class="container-fluid">
            <div class="navbar-header">
                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#myNavbar">
                <span class="icon-bar"></span>
                <span class="icon-bar"></span>
                <span class="icon-bar"></span>                        
              </button>
              <a class="navbar-brand" href="#" ><span><img src="css/logoblanco.png" style="width:45px; height:25px;" /></span>Campus Virtual USAT</a>
            </div>
            <div class="collapse navbar-collapse" id="myNavbar" runat="server">
                <ul class="nav navbar-nav navbar-right">
                  <%--<li><a href="#">Usuario: <label ID="lblUser" runat="server">Aqui va el nombre del usuario</label> </a></li>
                  <li><a href="../servicios/GuiaCambioPassword.pdf">¿Cambiar Contraseña? <label id="lblDias" runat="server"></label></a></li>
                  <li><a href="#">Notificaciones <span class="badge">4</span></a></li>--%>
                  <li><a href="../acceder2020.asp">Ir a Menú Principal</a></li>
                  <li><a href="../cerrar.asp">[Cerrar Sesión]</a></li>
                </ul>
            </div>
        </div>
    </nav>
    
    <div class="container-fluid" style="margin-top:60px">   
        <div class="row content">
            <div class="col-sm-3 sidenav" id="divMenu" runat="server">
                <%--<div class='well' style='padding: 0px;'>
                <!-- Menu -->
                <div class="side-menu">
                    <nav class="navbar navbar-default" role="navigation">
                        <!-- Brand and toggle get grouped for better mobile display -->
                        <div class="navbar-header">
                            <div class="brand-wrapper">
                                <!-- Hamburger -->
                                <button type="button" class="navbar-toggle">
                                    <span class="sr-only">Toggle navigation</span>
                                    <span class="icon-bar"></span>
                                    <span class="icon-bar"></span>
                                    <span class="icon-bar"></span>
                                </button>

                                <!-- Brand -->
                                <div class="brand-name-wrapper">
                                    <a class="navbar-brand" href="#">
                                        Nombre de la Aplicación
                                    </a>
                                </div>

                            </div>
                        </div>
                        <!-- Main Menu -->
                        <div class="side-menu-container">
                            <ul class="nav navbar-nav">

                                <li><a href="#"><img src="../../images/menus/computadora2.gif" width="32px" height="32px"> Aula Virtual</a></li>
                                <li><a href="#"><img src="../../images/menus/hojas.gif" width="32px" height="32px"> Tesis</a></li>
                                <li><a href="#"><img src="../../images/menus/icomanual.png" width="32px" height="32px"> Manual de Usuario</a></li>

                                <!-- Dropdown-->
                                <li class="panel panel-default" id="dropdown">
                                    <a data-toggle="collapse" href="#dropdown-lvl1">
                                        <img src="../../images/menus/redactar.gif" width="32px" height="32px"> Notas Finales<span class="caret"></span>
                                    </a>

                                    <!-- Dropdown level 1 -->
                                    <div id="dropdown-lvl1" class="panel-collapse collapse">
                                        <div class="panel-body">
                                            <ul class="nav navbar-nav">
                                                <li><a href="#"><span class="fa fa-file-alt"></span> Link</a></li>
                                                <li><a href="#"><span class="fa fa-file-alt"></span> Link</a></li>
                                                <li><a href="#"><span class="fa fa-file-alt"></span> Link</a></li>

                                                <!-- Dropdown level 2 -->
                                                <li class="panel panel-default" id="dropdown">
                                                    <a data-toggle="collapse" href="#dropdown-lvl2">
                                                        <span class="fa fa-book"></span> Sub Level <span class="caret"></span>
                                                    </a>
                                                    <div id="dropdown-lvl2" class="panel-collapse collapse">
                                                        <div class="panel-body">
                                                            <ul class="nav navbar-nav">
                                                                <li><a href="#"><span class="fa fa-file-alt"></span> Link</a></li>
                                                                <li><a href="#"><span class="fa fa-file-alt"></span> Link</a></li>
                                                                <li><a href="#"><span class="fa fa-file-alt"></span> Link</a></li>
                                                            </ul>
                                                        </div>
                                                    </div>
                                                </li>
                                            </ul>
                                        </div>
                                    </div>
                                </li>

                                <li><a href="#"><img src="../../images/menus/buscar_small.gif" width="32px" height="32px"> Consultas</a></li>

                            </ul>
                        </div><!-- /.navbar-collapse -->
                    </nav>
                </div>
                </div>--%>
            </div>
            
            <div class="col-sm-9 text-left" id="divForm" runat="server">
                <iframe src="about:blank" name="fraPrincipal" id="fraPrincipal" style="border:0; width:100%" onload="resizeIframe(this)">

  	            </iframe> 
            </div>
            
        </div>
    </div>
    
   <%-- <footer class="container-fluid text-center">
      <p>Copyright 2020: USAT - Todos los derechos reservados.</p>
    </footer>--%>

    <%--<form id="form1" runat="server">
    <div>
    
    </div>
    </form>--%>
    
    <script type="text/javascript">
        $(function() {
            $('.navbar-toggle').click(function() {
                $('.navbar-nav').toggleClass('slide-in');
                $('.side-body').toggleClass('body-slide-in');
                $('#search').removeClass('in').addClass('collapse').slideUp(200);

                /// uncomment code for absolute positioning tweek see top comment in css
                //$('.absolute-wrapper').toggleClass('slide-in');

            });

            // Remove menu for searching
            $('#search-trigger').click(function() {
                $('.navbar-nav').removeClass('slide-in');
                $('.side-body').removeClass('body-slide-in');

                /// uncomment code for absolute positioning tweek see top comment in css
                //$('.absolute-wrapper').removeClass('slide-in');

            });
        });
    </script>
    
</body>
</html>
