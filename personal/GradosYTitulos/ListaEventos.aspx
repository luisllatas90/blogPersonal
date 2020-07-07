<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ListaEventos.aspx.vb" Inherits="GradosYTitulos_ListaEventos" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0">
    <!-- Cargamos css -->
    <link href="assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/css/material.css" rel="stylesheet" type="text/css" />
    <link href="assets/css/style.css" rel="stylesheet" type="text/css" />
    <%--<link href="assets/css/carousel.css" rel="stylesheet" type="text/css" />--%>
    <!-- Cargamos JS -->

    <script src="assets/js/jquery.js" type="text/javascript"></script>

    <%--<script src="assets/js/app.js" type="text/javascript"></script>--%>

    <script src="assets/js/jquery-ui-1.10.3.custom.min.js" type="text/javascript"></script>

    <script src="assets/js/bootstrap.min.js" type="text/javascript"></script>

    <%--    <script src="assets/js/jquery.nicescroll.min.js" type="text/javascript"></script>

    <script src="assets/js/wow.min.js" type="text/javascript"></script>

    <script src="assets/js/jquery.loadmask.min.js" type="text/javascript"></script>

    <script src="assets/js/jquery.accordion.js" type="text/javascript"></script>

    <script src="assets/js/materialize.js" type="text/javascript"></script>

    <script src="assets/js/bic_calendar.js" type="text/javascript"></script>

    <script src="assets/js/core.js" type="text/javascript"></script>

    <script src="assets/js/snap.js" type="text/javascript"></script>--%>
    <%--<script src="assets/js/carousel/carousel.js" type="text/javascript"></script>--%>
    <title>Carousel Template for Bootstrap</title>
</head>
<body class="">
    <div class="container">
        <div class="row">
            <div class="col-md-12">
                <!-- panel -->
                <div class="panel panel-piluku">
                    <%--<div class="panel-heading">
                        <h3 class="panel-title">
                            Custom Content <span class="panel-options"><a href="#" class="panel-refresh"><i class="icon ti-reload">
                            </i></a><a href="#" class="panel-minimize"><i class="icon ti-angle-up"></i></a><a
                                href="#" class="panel-close"><i class="icon ti-close"></i></a></span>
                        </h3>
                    </div>--%>
                    <div class="panel-body">
                        <div class="list-group demo-list-group">
                            <a href="#" class="list-group-item active">
                                <h4 class="list-group-item-heading">
                                    Archon Admin Template Archon Admin Template Archon Admin Template Archon Admin Template
                                    Archon Admin Template Archon Admin Template Archon Admin Template Archon Admin Template
                                    Archon Admin Template</h4>
                                <p class="list-group-item-text">
                                    Archon is a Flat , Responsive, Admin Dashboard template. It is complete set of modern
                                    standards and top notch design. Built on twitter bootstrap 3.0
                                </p>
                            </a><a href="#" class="list-group-item">
                                <h4 class="list-group-item-heading">
                                    Delighted Admin Template</h4>
                                <p class="list-group-item-text">
                                    Delighted is a Flat , Responsive, admin template. It is complete set of modern standards
                                    and top notch design. Built on twitter bootstrap 3.0
                                </p>
                            </a><a href="#" class="list-group-item">
                                <h4 class="list-group-item-heading">
                                    Cascade Admin Template</h4>
                                <p class="list-group-item-text">
                                    Cascade is a Flat , Responsive, Admin Dashboard template. It is complete set of
                                    modern standards and top notch design. Built on twitter bootstrap 3.0
                                </p>
                            </a>
                        </div>
                    </div>
                </div>
                <!-- /panel -->
            </div>
        </div>
        <div class="row">
            <!-- col-md-12-->
            <div class="col-md-12">
                <!-- *** Default Carousel ***-->
                <div class="panel panel-piluku">
                    <%-- <div class="panel-heading">
                        <h3 class="panel-title">
                            Default Carousel <span class="panel-options"><a href="#" class="panel-refresh"><i
                                class="icon ti-reload"></i></a><a href="#" class="panel-minimize"><i class="icon ti-angle-up">
                                </i></a><a href="#" class="panel-close"><i class="icon ti-close"></i></a></span>
                        </h3>
                    </div>--%>
                    <div class="panel-body">
                        <!--                        *** Default Carousel ***-->
                        <div id="carousel-one" class="carousel slide piluku-carousel" data-ride="carousel">
                            <!-- Indicators -->
                            <ol class="carousel-indicators">
                                <li data-target="#carousel-one" data-slide-to="0" class="active"></li>
                                <li data-target="#carousel-one" data-slide-to="1" class=""></li>
                                <li data-target="#carousel-one" data-slide-to="2" class=""></li>
                                <li data-target="#carousel-one" data-slide-to="3" class=""></li>
                                <li data-target="#carousel-one" data-slide-to="4" class=""></li>
                                <li data-target="#carousel-one" data-slide-to="5" class=""></li>
                                <li data-target="#carousel-one" data-slide-to="6" class=""></li>
                                <li data-target="#carousel-one" data-slide-to="7" class=""></li>
                                <li data-target="#carousel-one" data-slide-to="8" class=""></li>
                            </ol>
                            <!-- Wrapper for slides -->
                            <div class="carousel-inner" role="listbox">
                                <div class="item active">
                                    <img src="assets/images/breadcrumbbg.png" alt="...">
                                    <div class="carousel-caption">
                                        Lorem ipsum dolor sit amet, consectetur adipisicing elit.
                                    </div>
                                </div>
                                <div class="item">
                                    <img src="assets/images/sample.jpg" alt="...">
                                    <div class="carousel-caption">
                                        Lorem ipsum dolor sit amet, consectetur adipisicing elit.
                                    </div>
                                </div>
                                <div class="item">
                                    <img src="assets/images/profile-bg.jpg" alt="...">
                                    <div class="carousel-caption">
                                        Lorem ipsum dolor sit amet, consectetur adipisicing elit.
                                    </div>
                                </div>
                                <div class="item">
                                    <img src="assets/images/breadcrumbbg.png" alt="...">
                                    <div class="carousel-caption">
                                        Lorem ipsum dolor sit amet, consectetur adipisicing elit.
                                    </div>
                                </div>
                                <div class="item">
                                    <img src="assets/images/sample.jpg" alt="...">
                                    <div class="carousel-caption">
                                        Lorem ipsum dolor sit amet, consectetur adipisicing elit.
                                    </div>
                                </div>
                                <div class="item">
                                    <img src="assets/images/profile-bg.jpg" alt="...">
                                    <div class="carousel-caption">
                                        Lorem ipsum dolor sit amet, consectetur adipisicing elit.
                                    </div>
                                </div>
                                <div class="item">
                                    <img src="assets/images/breadcrumbbg.png" alt="...">
                                    <div class="carousel-caption">
                                        Lorem ipsum dolor sit amet, consectetur adipisicing elit.
                                    </div>
                                </div>
                                <div class="item">
                                    <img src="assets/images/sample.jpg" alt="...">
                                    <div class="carousel-caption">
                                        Lorem ipsum dolor sit amet, consectetur adipisicing elit.
                                    </div>
                                </div>
                                <div class="item">
                                    <img src="assets/images/profile-bg.jpg" alt="...">
                                    <div class="carousel-caption">
                                        Lorem ipsum dolor sit amet, consectetur adipisicing elit.
                                    </div>
                                </div>
                            </div>
                            <!-- Controls -->
                            <a class="left carousel-control" href="#carousel-one" role="button" data-slide="prev">
                                <span class="ti-angle-left" aria-hidden="true"></span><span class="sr-only">Previous</span>
                            </a><a class="right carousel-control" href="#carousel-one" role="button" data-slide="next">
                                <span class="ti-angle-right" aria-hidden="true"></span><span class="sr-only">Next</span>
                            </a>
                        </div>
                    </div>
                </div>
                <!-- *** /Default Carousel ***-->
                <!-- /panel -->
            </div>
            <!-- /col-md-12-->
        </div>
        <ul class="list-group">
            <li class="list-group-item list-group-item-info">
                <div class="row">
                    <div class="col-md-3">
                        <img src="assets/images/breadcrumbbg.png" alt="..." style="background-size: cover;
                            width: 100%; height: 100%;">
                    </div>
                    <div class="col-md-9">
                        <h5 class="flatBluec counter">
                            CASIOC AOSIOCDA SIOASOCIA SIAOS COAISCO IAOIC OAIS OIASDO AISODIAS ACIOAS ACSIOASC
                            ASCI OA CAOSIOC ASCOI AS CAS IOAISC ASOCI ASIC OAISC OAISOCIAOSICOAISO IOASC IOAISCO
                            IO AISOCIO ASCIOSAIOIASOICAS AOISCOIASO IOASICOAISOCIOSAICOAS OI</h5>
                        <h6>
                            EL EVENTO DEL OLEGIO NACIOANS AKDAK SJDKALSDJ ALJD JASJD ASD JASJDKLASJ JDASLJ ASLDLASJDKA
                            JKJKJKJKA JKASJDKASJLDKJASKDJASDJASDJKASDKASJDASDASDASDASD ASDASDSADASDASDASDASDASDAS
                            ASDASDASDDASDSDSDSADSAS SDSSSSSSSSSSSSSSSS SSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSS
                            SSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSS SSSSSSSSSSSSSS SSSSSSSSSSSSS SSSSSSSSSSSSSSSSSSSSSSSSSSSSS</h6>
                        <!-- /col-md-12-->
                    </div>
                </div>
            </li>
            <li class="list-group-item list-group-item-info">
                <div class="row">
                    <div class="col-md-3">
                        <a href="#">
                            <img src="assets/images/sample.jpg" alt="..." style="background-size: cover; width: 100%;
                                height: 100%;">
                        </a>
                    </div>
                    <div class="col-md-9">
                        <a href="#">
                            <h5 class="flatBluec counter">
                                CASIOC AOSIOCDA SIOASOCIA SIAOS COAISCO IAOIC OAIS OIASDO AISODIAS ACIOAS ACSIOASC
                                ASCI OA CAOSIOC ASCOI AS CAS IOAISC ASOCI ASIC OAISC OAISOCIAOSICOAISO IOASC IOAISCO
                                IO AISOCIO ASCIOSAIOIASOICAS AOISCOIASO IOASICOAISOCIOSAICOAS OI</h5>
                        </a>
                        <h6>
                            EL EVENTO DEL OLEGIO NACIOANS AKDAK SJDKALSDJ ALJD JASJD ASD JASJDKLASJ JDASLJ ASLDLASJDKA
                            JKJKJKJKA JKASJDKASJLDKJASKDJASDJASDJKASDKASJDASDASDASDASD ASDASDSADASDASDASDASDASDAS
                            ASDASDASDDASDSDSDSADSAS SDSSSSSSSSSSSSSSSS SSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSS
                            SSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSS SSSSSSSSSSSSSS SSSSSSSSSSSSS SSSSSSSSSSSSSSSSSSSSSSSSSSSSS...<a
                                href="#">Ver más</a></h6>
                        <!-- /col-md-12-->
                    </div>
                </div>
            </li>
        </ul>
    </div>
</body>
</html>
