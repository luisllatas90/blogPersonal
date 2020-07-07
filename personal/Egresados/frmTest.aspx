<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmTest.aspx.vb" Inherits="Egresados_frmTest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.2/jquery.min.js"></script>
    <script src="https://files.codepedia.info/files/uploads/iScripts/html2canvas.js"></script>

  <style type="text/css">
        .portada{
            background: url() no-repeat fixed center;
            -webkit-background-size: cover;
            -moz-background-size: cover;
            -o-background-size: cover;
            /*background-size: cover;*/
            height: 100%;
            width: 100% ;
            text-align: center; 
            }
            .text{
                margin: 30px 0px 30px 0px;	
                padding: 10px;
                background: rgba(0,0,0,0.5);
                /*display: inline-block;*/
           }
        </style>


</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>        
            <div id="html-content-holder" style="background-color: #F0F0F1; color: #00cc65; width: 500px;padding-left: 25px; padding-top: 10px;">
                <div class="portada">
	            	<div class="text">
			            <h1> div con imagen de fondo responsive</h1> 
			            <h3>Imagen adaptada a todas las resoluciones de pantalla</h3>
		            </div>
                </div>                
                <asp:TextBox ID="txtcodigo_ofe" runat="server"></asp:TextBox>
                <strong>Codepedia.info</strong><hr />
                <h3 style="color: #3e4b51;">
                    Html to canvas, and canvas to proper image
                </h3>
                <p style="color: #3e4b51;">
                    <b>Codepedia.info</b> is a programming blog. Tutorials focused on Programming ASP.Net,
                    C#, jQuery, AngularJs, Gridview, MVC, Ajax, Javascript, XML, MS SQL-Server, NodeJs,
                    Web Design, Software
                </p>
                <p style="color: #3e4b51;">
                    <b>html2canvas</b> script allows you to take "screenshots" of webpages or parts
                    of it, directly on the users browser. The screenshot is based on the DOM and as
                    such may not be 100% accurate to the real representation.
                </p>
            </div>
            <a id="btn-Convert-Html2Image" href="#">Download</a>
            <br />
            <div id="previewImage" style="display: none;">
            </div>
    </div>
    <script type="text/javascript">
    $(document).ready(function () {
            var element = $("#html-content-holder"); // global variable
            var getCanvas; // global variable

            html2canvas(element, {
                onrendered: function (canvas) {
                    $("#previewImage").append(canvas);
                    getCanvas = canvas;
                }
            });

            $("#btn-Convert-Html2Image").on('click', function () {
                var imgageData = getCanvas.toDataURL("image/png");
                // Now browser starts downloading it instead of just showing it
                var newData = imgageData.replace(/^data:image\/png/, "data:application/octet-stream");
                $("#btn-Convert-Html2Image").attr("download", "your_pic_name.png").attr("href", newData);
            });
        });
    </script>
    </form>
</body>
</html>
