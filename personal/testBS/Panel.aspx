<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Panel.aspx.vb" Inherits="testBS_Panel" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <!--Compatibilidad con IE-->
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />
    <!--Compatibilidad con IE->
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0" />
    <meta http-equiv="Pragma" content="no-cache" />
    <!-- Bootstrap CSS -->
    <link rel="stylesheet" href="assets/css/bootstrap.min.css">
    <!-- Optional JavaScript -->
    <!-- jQuery first, then Popper.js, then Bootstrap JS -->

    <script src="assets/js/jquery-3.3.1.slim.min.js" type="text/javascript"></script>

    <script src="assets/js/popper.min.js" type="text/javascript"></script>

    <script src="assets/js/bootstrap.min.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            $('.carousel').carousel({
                interval: 3000
            })
        });

    </script>

    <style type="text/css">
        .container
        {
            margin-left: auto;
            margin-right: auto;
            padding-left: 15px;
            padding-right: 15px;
        }
        .navbar-fixed-top .navbar-fixed-bottom
        {
            position: fixed;
            right: 0;
            left: 0;
            z-index: 1100;
        }
    </style>
</head>
<body>
    <header class=" bg-danger">
    <nav class="navbar-fixed-top navbar navbar-expand-lg navbar-dark align-content-center container">
        <a class="navbar-brand" href="#">
        <img src="assets/images/logo.png" style="float:left; width:60px;" />
            <span style="float:left;font-family:Calibri; font-size:13px; font-style:normal; font-weight:bold; padding:3px">
            HCANO<br />INGENIEROS<br />CONTRATISTAS
            </span>
        </a>
        <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarText" aria-controls="navbarText" aria-expanded="false" aria-label="Toggle navigation">
            <span class="navbar-toggler-icon"></span>
        </button>
        <div class="collapse navbar-collapse" id="navbarText">
            <ul class="navbar-nav ml-auto">
              <li class="nav-item active">
                <a class="nav-link" href="#">Inicio <span class="sr-only">(current)</span></a>
              </li>
              <li class="nav-item">
                <a class="nav-link" href="#">Nosotros</a>
              </li>
              <li class="nav-itemt">
             <a class="nav-link" href="#">Servicios</a>
              </li>
              <li class="nav-item">
                <a class="nav-link" href="#">Contacto</a>
              </li>
            </ul>
         </div>
    </nav>
</header>
    <div class="container-fluid">
        <div class="row">
            <div id="carouselExampleCaptions" class="carousel slide carousel-fade w-100" data-ride="carousel">
                <ol class="carousel-indicators">
                    <li data-target="#carouselExampleCaptions" data-slide-to="0" class="active"></li>
                    <li data-target="#carouselExampleCaptions" data-slide-to="1"></li>
                    <li data-target="#carouselExampleCaptions" data-slide-to="2"></li>
                </ol>
                <div class="carousel-inner">
                    <div class="carousel-item active">
                        <img src="assets/images/imagen1.png" height="500" class="d-block w-100" alt="...">
                        <div class="carousel-caption d-none d-md-block">
                            <h5>
                                First slide label</h5>
                            <p>
                                Nulla vitae elit libero, a pharetra augue mollis interdum.</p>
                        </div>
                    </div>
                    <div class="carousel-item">
                        <img src="assets/images/imagen2.png" height="500" class="d-block w-100" alt="...">
                        <div class="carousel-caption d-none d-md-block ">
                            <h5>
                                Second slide label</h5>
                            <p>
                                Lorem ipsum dolor sit amet, consectetur adipiscing elit.</p>
                        </div>
                    </div>
                    <div class="carousel-item">
                        <img src="assets/images/imagen3.png" height="500" class="d-block w-100" alt="...">
                        <div class="carousel-caption d-none d-md-block">
                            <h5>
                                Third slide label</h5>
                            <p>
                                Praesent commodo cursus magna, vel scelerisque nisl consectetur.</p>
                        </div>
                    </div>
                </div>
                <a class="carousel-control-prev" href="#carouselExampleCaptions" role="button" data-slide="prev">
                    <span class="carousel-control-prev-icon" aria-hidden="true"></span><span class="sr-only">
                        Previous</span> </a><a class="carousel-control-next" href="#carouselExampleCaptions"
                            role="button" data-slide="next"><span class="carousel-control-next-icon" aria-hidden="true">
                            </span><span class="sr-only">Next</span> </a>
            </div>
        </div>
        <div class="row">
            <div class="card bg-danger text-light col-md-12">
                <div class="card-header text-center c">
                    <h5 class="card-title">
                        HCANO Ingenieros Contratistas
                    </h5>
                </div>
                <div class="card-body">
                    <%--<h5 class="card-title">
                    </h5>--%>
                    <div class="row">
                        <div class="col-md-6 p-4">
                            <p class="card-text text-justify">
                                Somos una empresa dedicada a realizar servicios de consultoría, servicios de mantenimiento
                                preventivo y correctivo, Supervisión y ejecuciones de Obras Civiles.</p>
                            <p class="card-text text-justify">
                                En la actualidad HCANO INGENIEROS CONTRATISTAS SRL, cuenta con un staff de profesionales
                                altamente calificado, con lo último en herramientas, equipos y maquinaria especializada
                                para asegurar la más alta calidad y confiabilidad en la ejecución de su proyecto,
                                indicándole que contamos con experiencia en el rubro de Servicios de Consultoría,
                                Servicios de mantenimiento preventivo y correctivo, Supervisión y Ejecución de Obras
                                civiles.
                            </p>
                        </div>
                        <div class="col-md-6">
                            <img class="img-fluid" src="assets\images\trabajadores.svg" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="card bg-light">
                <div class="card-body">
                </div>
            </div>
        </div>
    </div>
    <form id="form1" runat="server">
    </form>
</body>
</html>
