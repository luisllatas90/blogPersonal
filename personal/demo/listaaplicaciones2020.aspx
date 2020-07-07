<%@ Page Language="VB" AutoEventWireup="false" CodeFile="listaaplicaciones2020.aspx.vb" Inherits="demo_listaaplicaciones2020" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <meta charset="utf-8" />
  <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
  <meta name="google" value="notranslate" />
  <link rel="icon" href="css/favicon.ico"> 
  <title>Página Principal del Campus Virtual</title>
  
  <meta http-equiv="X-UA-Compatible" content="IE=edge" />
  <meta http-equiv='X-UA-Compatible' content='IE=7' />
  <meta http-equiv='X-UA-Compatible' content='IE=8' />
  <meta http-equiv='X-UA-Compatible' content='IE=10' />
  
  <link href="css/bootstrap.min.css" rel="Stylesheet" type="text/css" />
  <script src="js/jquery.min.js" type="text/javascript"></script>
  <script src="js/bootstrap.min.js" type="text/javascript"></script>
  
  <link href="../assets/fontawesome-5.2/css/regular.min.css" rel="stylesheet" type="text/css" />
  <script src="../assets/fontawesome-5.2/js/all.min.js" type="text/javascript"></script>
  
  <script type="text/javascript">

      function fc_AbrirFuncion(ctfu, capl, dapl, eapl, dtfu) {
          $.ajax({
              type: "POST",
              url: "Sesion.aspx",
              data: { 'ctfu': ctfu, 'capl': capl, 'dapl': dapl, 'dtfu': dtfu, 'eapl': eapl },
              dataType: "json",
              cache: false,
              success: function(data) {
                  console.log(data);
                  //alert("ok");
                  top.location.href = "../abriraplicacion2020.asp?codigo_tfu=" + ctfu + "&codigo_apl=" + capl + "&descripcion_apl=" + dapl + "&estilo_apl=" + eapl + "&descripcion_tfu=" + dtfu
              },
              error: function(response) {
                  console.log(response);
                  //alert("error");
              }
          });

          //top.location.href = "../abriraplicacion2020.asp?codigo_tfu=" + _ctfu + "&codigo_apl=" + capl + "&descripcion_apl=" + dapl + "&estilo_apl=" + eapl + "&descripcion_tfu=" + dtfu
      }

      function ReglamentoSST() {
          //AbrirPopUp('../avisos/progEsp/becas/universia/seg_confPER.asp', '600', '650', 'yes', 'yes', 'yes', 'beca')
          window.open('../../librerianet/reglamentos/PUBLICACIÓN RISST 2019.pdf', '_blank')
      }

      function AceptarReglamento(u) {
          var isChecked = document.getElementById('chkTerminos').checked;
          if (isChecked) {
              //var u = '<%=session("codigo_Usu") %>'
              if (u != '') {
                  var pagina = '../AceptarReglamento.aspx?u=' + u
                  window.open(pagina, '_blank')
                  window.location.reload();
              } else {
                  alert('Su sesión ha expirado, debe volver a ingresar para poder procesar la petición.')
              }
          } else {
              alert('Debe Aceptar haber leído y conocer las actualizaciones del reglamento')
          }
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
    
    .media-body > p
    {
        font-size: 10px;
        color: #000;
    }
    
    .media-body .media-heading
    {
        color: #000;
    }
    
    .dropdown-menu
    {
    	top: 80%;
    	left: 15px;
    	font-size: 11px;
    }
    
    .dropdown-menu li a:hover
    {
    	color: #e33439;
    }
    
    .dropdown-header
    {
    	color: #fff;
    	background-color: #e33439;
    }
    
    .thumbnail:hover
    {
    	border-color: #000;
    	background-color: #e33439;
    }
    
    .thumbnail a
    {
    	text-decoration: none;
    }
    
    .thumbnail:hover .media-body p
    {
        color: #fff;
    }
    
    .thumbnail:hover .media-body .media-heading
    {
        color: #fff;
    }
    
  </style>
  
</head>
<body>

<form id="form1" name="form1" action="#" runat="server" method="post">
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
        <%--<ul class="nav navbar-nav navbar-right">
          <li><a href="#">Usuario: <label ID="lblUser" runat="server">Aqui va el nombre del usuario</label> </a></li>
          <li><a href="../servicios/GuiaCambioPassword.pdf">¿Cambiar Contraseña? <label id="lblDias" runat="server"></label></a></li>
          <li><a href="#">Notificaciones <span class="badge">4</span></a></li>
          <li><a href="../cerrar.asp">[Cerrar Sesión]</a></li>
        </ul>--%>
    </div>
  </div>
</nav>
  
<div class="container-fluid text-center" style="margin-top:60px">    
  <div class="row content">
  
    <div class="col-sm-3 sidenav" id="divAlert" runat="server">
    
      <%--<div class="alert alert-success fade in">
        <a href="#" class="close" data-dismiss="alert" aria-label="close">×</a>
        <p><strong>Alert!</strong></p>
        People are looking at your profile. Find out who. <a href="#" class="alert-link">Click Here</a>.
      </div>
      
      <div class="alert alert-info fade in">
        <a href="#" class="close" data-dismiss="alert" aria-label="close">×</a>
        <p><strong>Alert!</strong></p>
        People are looking at your profile. Find out who. <a href="#" class="alert-link">Click Here</a>.
      </div>
      
      <div class="alert alert-warning fade in">
        <a href="#" class="close" data-dismiss="alert" aria-label="close">×</a>
        <p><strong>Alert!</strong></p>
        People are looking at your profile. Find out who. <a href="#" class="alert-link">Click Here</a>.
      </div>--%>
      
     <%--<div class="alert alert-danger fade in">
        <a href="#" class="close" data-dismiss="alert" aria-label="close">×</a>
        <p><strong>Alert!</strong></p>
        People are looking at your profile. Find out who. <a href="#" class="alert-link">Click Here</a>.
      </div>--%>
      
    </div>
    
    <div class="col-sm-7 text-left" id="divApl" runat="server"> 
      <%--<h3>Sistemas Académicos</h3>--%>
      <%--<div class="row">
      
          <div class="col-md-4">
            <div class="thumbnail">
              <a href="../../images/menus/aulavirtual.png">
              <div class="media">
                <div class="media-left">
                  <img src="../../images/menus/aulavirtual.png" class="media-object" style="width:60px">
                </div>
                <div class="media-body">
                  <h4 class="media-heading">Aula Virtual</h4>
                  <p>Perfiles</p>
                </div>
              </div>
              </a>
            </div>
          </div>
          
          <div class="col-md-4">
            <div class="thumbnail">
              <a href="../../images/menus/escribir.gif">
              <div class="media">
                <div class="media-left">
                  <img src="../../images/menus/escribir.gif" class="media-object" style="width:60px">
                </div>
                <div class="media-body">
                  <h4 class="media-heading">Gestión de Admisión</h4>
                  <p>Perfiles</p>
                </div>
              </div>
              </a>
            </div>
          </div>
          
          <div class="col-md-4">
            <div class="thumbnail">
              <a href="../../images/menus/pregrado.png">
              <div class="media">
                <div class="media-left">
                  <img src="../../images/menus/pregrado.png" class="media-object" style="width:60px">
                </div>
                <div class="media-body">
                  <h4 class="media-heading">Gestión de Admisión</h4>
                  <p>Perfiles</p>
                </div>
              </div>
              </a>
            </div>
          </div>
          
        </div>--%>

    </div>
    
    <div class="col-sm-2 sidenav" id="divAds" runat="server">
      <%--<div class="well">
        <p>ADS</p>
      </div>
      <div class="well">
        <p>ADS</p>
      </div>--%>
    </div>
    
  </div>
</div>

<footer class="container-fluid text-center">
  <p>Copyright 2020: USAT - Todos los derechos reservados.</p>
</footer>

</form>

<script type="text/javascript">

    $(document).ready(function() {
        $('[data-toggle="tooltip"]').tooltip();
    });
    
</script>

</body>
</html>

<%--<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Página sin título</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>
</body>
</html>--%>
