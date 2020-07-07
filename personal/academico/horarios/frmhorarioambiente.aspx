<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmhorarioambiente.aspx.vb" Inherits="academico_horarios_frmhorarioambiente" %>

<!DOCTYPE html>
<html lang="en">
<head id="Head1" >
   <meta http-equiv="X-UA-Compatible" content="IE=edge"/> 
  <meta http-equiv='X-UA-Compatible' content='IE=8' />
  <meta http-equiv='X-UA-Compatible' content='IE=10' />
  <meta http-equiv='X-UA-Compatible' content='IE=11' />
   <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0"/> <!--320-->
<link rel='stylesheet' href='../../assets/css/bootstrap.min.css?x=1'/>
<link rel='stylesheet' href='../../assets/css/material.css?x=1'/>
<link rel='stylesheet' href='../../assets/css/style.css?y=1'/>
<script type="text/javascript" src='../../assets/js/jquery.js?x=1'></script>
<script type="text/javascript" src='../../assets/js/app.js?x=1'></script>

<script type="text/javascript" src='../../assets/js/jquery-ui-1.10.3.custom.min.js?x=1'></script>
<script type="text/javascript" src='../../assets/js/bootstrap.min.js?x=1'></script>

<script type="text/javascript" src='../../assets/js/jquery.nicescroll.min.js?x=1'></script>
<script type="text/javascript" src='../../assets/js/wow.min.js?x=1'></script>
<script type="text/javascript" src='../../assets/js/jquery.loadmask.min.js?x=1'></script>
<script type="text/javascript" src='../../assets/js/jquery.accordion.js?x=1'></script>
<script type="text/javascript" src='../../assets/js/materialize.js?x=1'></script>
<script type="text/javascript" src='../../assets/js/funciones.js?a=1'></script>
<script type="text/javascript" src="../../assets/js/noty/jquery.noty.js"></script>
<script type="text/javascript" src='../../assets/js/noty/layouts/top.js'></script>
<script type="text/javascript" src='../../assets/js/noty/layouts/default.js'></script>
<script type="text/javascript" src="../../assets/js/noty/notifications-custom.js"></script>

<script type="text/javascript" src="../../assets/js/jquery.dataTables.min.js?x=1"></script>	
<link rel='stylesheet' href='../../assets/css/jquery.dataTables.min.css?z=1'/>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Registro de horarios por Ambiente</title>
      <link href="../../../private/estilo.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="JavaScript" src="../../../private/funciones.js"></script>
        <style type="text/css">
            .ie2{
                float:left; 
                width:50%;
                }
            .ie3{
                float:left; 
                width:33%;
                }  
        .row
        {
            margin-right: 0px;
            margin-left: 0px;
            padding: 3px;
        }
        .content .main-content
        {
            padding-right: 0px;
        }
        .content
        {
            margin-left: 0px;
        }
        .form-control
        {
            border-radius: 0px;
            box-shadow: none;
            border-color: #d9d9d9;
            font-weight: 300;  line-height: 40px;
            color: black;
        }
        .i-am-new
        {
            z-index: 100;
        }
        .form-group
        {
            margin: 0px;
        }
        .form-horizontal .control-label
        {
            padding-top: 0px;
        }
        .derecha
        {
            float:right;
            }
    </style>
     <script type="text/javascript" language="javascript">
         var event = jQuery.Event("DefaultPrevented");
             $(document).trigger(event);
             $(document).ready(function() {

             fnEstilo();
         });
         function fnEstilo() {
             var v = msieversion();
             if (v > 0) {
                 $("#div1").removeClass("ie2").addClass("ie2");
                 $("#div2").removeClass("ie2").addClass("ie2");
                 $("#div3").removeClass("ie2").addClass("ie2");
                 $("#div4").removeClass("ie2").addClass("ie2");

             } else {
                 $("#div1").removeClass("ie2");
                 $("#div2").removeClass("ie2");
                 $("#div3").removeClass("ie2");
                 $("#div4").removeClass("ie2");

             }
         }
         function msieversion() {

             var ua = window.navigator.userAgent;
             var msie = ua.indexOf("MSIE");
             var version = 0;
             if (msie > 0 || !!navigator.userAgent.match(/Trident.*rv\:11\./))  // If Internet Explorer, return version number
             {
                 //alert(parseInt(ua.substring(msie + 5, ua.indexOf(".", msie))));
                 version = parseInt(ua.substring(msie + 5, ua.indexOf(".", msie)));
             }
             else  // If another browser, return 0
             {
                 version = 0;
             }

             return version;
         }
     </script>
</head>
<body class="">
    <form id="form1" runat="server">
    <div id="loading" class="piluku-preloader text-center" runat="server">    
             <div class="loader">Loading...</div>
    </div>
    <div class="wrapper">
        <div class="content">
            <div class="main-content">
                <div  id="report">
                    <div id="PanelLista" runat="server">
                          <div class="row">                   
                            <div class="manage_buttons">
                                <div class="row">
                                </div>
                            </div>
                           </div>
                    </div>
                    <div id="PanelRegistro" runat="server">
                    
                    </div>
                </div>
            </div>
        </div>
    </div>

    </form>
</body>
</html>
