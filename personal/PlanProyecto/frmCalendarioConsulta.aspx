<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmCalendarioConsulta.aspx.vb" Inherits="PlanProyecto_frmCalendarioConsulta" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../../private/estilo.css" rel="stylesheet" type="text/css" />    
    <link rel="stylesheet" href="../../private/jq/lbox/thickbox.css" type="text/css" media="screen" />    
    <script type="text/javascript" language="JavaScript" src="../../private/jq/jquery-1.4.2.min.js"></script> 
    <script type="text/javascript" language="JavaScript" src="../../private/jq/lbox/thickbox.js"></script>	     

    <link href="lib-tooltip/jquery.tooltip.css" rel="stylesheet" type="text/css" />
    <link href="css/jquery1.8.css" rel="stylesheet" type="text/css" />
    <link href="css/estiloCuadro.css" rel="stylesheet" type="text/css" />
    <script src="jquery/jquery-ui-1.8.2.custom.min.js" type="text/javascript"></script>    
    <!--     
    <script src="lib-tooltip/jquery.js" type="text/javascript"></script>
    <script src="lib-tooltip/jquery.bgiframe.js" type="text/javascript"></script>
    <script src="lib-tooltip/jquery.dimensions.js" type="text/javascript"></script>
    <script src="lib-tooltip/jquery.tooltip.js" type="text/javascript"></script>
    -->
    
    <style type="text/css">                       
        .style1
        {
            width: 10%;
        }                
        #enlace
        {
            top:0px;
            left:0px;            
        }
        .tablaBusqueda{
            position:absolute;
            top:0px;
            left:10px;            
            /*height:100%;*/
        }
        #leyenda{
            position:absolute;
            top:50px;        
            height:85px;
            width:100%;
        }
        .tabla{
            position:absolute;
            top:110px;            
            left:0px;            
            bottom: 130px;
        }
        #fradetalle
        {            
            position:absolute;
            top:215px;        
            height:500px;            
            width: 1330px;
            left:0px;            
        }
    </style>    
    <script type="text/javascript">
        var muestra = false;
        $(function() { //llamada tras la carga de la página        
            var enlace = $("a.popup").click(function() {  //indicamos que al hacer click en cualquier elemento "a" que tenga la clase "claseDelEnlace" ejecute la siguiente función            
                var destino = $(this).attr("href");
                var anchoventana = 300;
                var altoventana = 200;
                $("div.ventana").remove(); 
                $.ajax({
                    type: "GET",
                    url: destino,
                    success: function(data) { //indicamos la función que se ejecutará al recibir los datos en la variable data exitosamente
                        //limitamos el contenido del documento a lo que está dentro de la etiqueta body
                        var ini = data.indexOf("<body");
                        if (ini >= 0) {
                            ini = data.indexOf(">", ini) + 1;
                            var fin = data.indexOf("</body");
                            fin = fin - ini;
                            var datos = data.substr(ini, fin);
                        } else {
                            var datos = data;
                        }
                        //si el contenido del documento tiene la etiqueta title, la extraemos para utilizarla como titulo de la ventana
                        ini = 0;
                        ini = data.indexOf("<title");
                        if (ini >= 0) {
                            ini = data.indexOf(">", ini) + 1;
                            fin = 0;
                            fin = data.indexOf("</title");
                            fin = fin - ini;
                            var titulo = data.substr(ini, fin);
                        } else {
                            var titulo = "Ventana Emergente";
                        }
                        $("body").append('<div class="ventana">' + datos + '</div>'); //adjuntamos los datos recibidos en una capa con la clase "ventana" al final del documento
                        $("div.ventana").dialog({  //indicamos que las capas con la clase "ventana" son ventanas de dialogo
                            closeText: 'Cerrar',
                            title: titulo,
                            height: altoventana,
                            width: anchoventana,
                            close: function() { //indicamos la función que se ejecutará al cerrarse la ventana
                                $(this).remove(); //borramos la capa
                            }                                                            
                        });
                    }
                });
                return false;
            });
        });
</script>

</head>
<body>
    <div>
        <form id="form1" runat="server">    
        <table width="100%" >        
            <tr>
                <td class="style1">
                    <asp:Label ID="Label1" runat="server" Text="Calendario:"></asp:Label></td>
                <td>
                    <asp:DropDownList ID="dpProyecto" runat="server" AutoPostBack="True">
                    </asp:DropDownList>                                  
                </td>
                <td align="right" >
                    <asp:Button ID="btnExportar" runat="server" Text="Exportar" Width="100px" Height="22px" CssClass="excel" /> &nbsp;&nbsp;
                    <asp:Button ID="btnRefrescar" runat="server" Text="Refrescar" CssClass="salir" Width="100px" Height="22px" />
                </td>                
            </tr>            
        </table> <br />                                   
        <div id="leyenda" runat="server" border="0">
	    </div>    
        <div id='Cal1' class='tabla' runat="server">
        </div>        
        <iframe id="fradetalle" border="0" frameborder="0" runat="server">
	    </iframe>    	                          	    
        <asp:HiddenField ID="hfLeyenda" runat="server" />                
        <asp:HiddenField ID="hfAnio" runat="server" />                       
        </form>        
    </div>        
</body>
</html>