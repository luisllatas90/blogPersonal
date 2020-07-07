﻿<!DOCTYPE html> 
<html>
<head runat="server">
    <script src="../estudiante/Scripts/jquery-2.1.1.js" type="text/javascript"></script>
	<meta charset="utf-8">
    <title>::Libros Electrónicos:: Universidad Católica Santo Toriobio de Mogrovejo - USAT</title>
    <style>
    body
    { font-size:12px;
      font-weight:bold;        
    }
    div 
    {
        padding:5px;
        border:1px solid green;
        width:80px;
        cursor:hand;
        background-color:#AECF74;
        color:White;
        }
    </style>
    

		<!-- Esta ruta de script debe de ser la carpeta donde se encuentren los archivos .js de jQuery-->
    
		<!-- <script src="//ajax.googleapis.com/ajax/libs/jquery/1.11.0/jquery.min.js"></script> -->
    <script type="text/javascript">

        var un; //Nombre del usuario codificado.
        var ul1; //Apellido paterno del usuario codificado.
        var ul2; //Apellido materno del usuario codificado.
        var ue; //Correo electrónico del usuario codificado.
        var ui; //Dirección IP del usuario codificada.
        var i; //ID de la institución en la base de datos de Pearson (entero)
        var ic; //ID del campus de la institución en la base de datos de Pearson (entero)
        var ik; //Institución Key en la base de datos de Pearson (entero)

        function GetUrlBV() {
            un = "Usuario";
            ul1 = "Usat";
            ul2 = "";
            ue = "usuariofijo@usat.edu.pe";
            ui = "";
            i = "205";
            ic = "";
            ik = "u54t3Du2016"

            GetURLPage();
        };

        function GetURLPage() {
            var urlInitial = "http://www.biblionline.pearson.com/Services/GenerateURLAccess.svc/GetUrl?firstname=" + un + "&lastname1=" + ul1 + "&lastname2=" + ul2 + "&email=" + ue + "&ip=" + ui + "&idInstitution=" + i + "&idCampus=" + ic + "&institutionKey=" + ik + "&$callback=successCall&$format=json";

            $.ajax({
                dataType: "jsonp",
                contentType: "application/json; charset=utf-8",
                url: urlInitial,
                jsonpCallback: "successCall",
                error: function() {
                    alert("Error");
                },
                success: successCall
            });

            function parseJSON(jsonData) {
                return jsonData.Message;
            }


            function successCall(result) {

                res = parseJSON(result.GetUrlAccessResult);
                //location.replace(res); //Redirecciona a la página de la BV.
                document.getElementById("lnkUrl").href = res; //Establece la URL de un hipervínculo en la página.
                //document.getElementById("txtUrlResult").value = res; //Establece el valor de un elemento oculto con el texto de la URL generada.
                //document.getElementById("txtUrl").innerHTML = res; //Muestra el texto de la URL generada.
            }
        };

    </script>   
     <script type ="text/javascript" >
         $(document).ready(function() {
                          alert('hola');
             return GetUrlBV();
         });
         
    </script>
</head>
<body>
<!--<script type="text/javascript" src="../estudiante/Scripts/jquery-2.1.1.js?x=1"></script> -->

    <center>
    <br /><br />
        <img alt="Biblioteca" longdesc="Biblioteca" src="../images/pearson.png" 
                style="width: 490px; height: 250px" /><br />
	   <div><a id="lnkUrl" target="_blank" href="" >INGRESAR</a></div>
	</center> 
</body>
</html>