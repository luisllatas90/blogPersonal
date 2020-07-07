<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmHorarioAmbiente.aspx.vb" Inherits="horario_FrmHorarioAmbiente" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Lista de ambientes</title>
    <script src="jquery-1.4.2.min.js" type="text/javascript"></script>
    <link href="estilo.css" rel="stylesheet" type="text/css" />       
    <script type="text/javascript">
        $(document).ready(function() {
            function MuestraTabla() {
                var numElem = $("table[name=tblhorario]").size();
                var NoBloquear = $('#valor').val();

                for (var i = 0; i < numElem; i++) {
                    if (i == NoBloquear) {
                        var ubi = $("#Ub-" + i).val().toUpperCase();
                        var pis = $("#Pi-" + i).val();

                        if (pis == "0") {
                            $("#TituloAmbiente").html(ubi);
                        } else {
                            $("#TituloAmbiente").html(ubi + " - PISO " + pis);
                        }

                        $("#tb-" + i).show(0);
                        $("#tb-" + i).show("fast");
                    } else {
                        $("#tb-" + i).hide(0);
                        $("#tb-" + i).hide("fast");
                    }
                }

                $('#valor').val(parseInt(NoBloquear) + 1);
                if (parseInt($('#valor').val()) == numElem) {
                    $('#valor').val(0);
                }

                var tiempo = new Date();
                var minuto = tiempo.getMinutes();
                var param = $("#HdActualizar").val();
                if (parseInt(minuto) == 55 && param == "1") {
                    $("#HdActualizar").val("0")                    
                    location.reload();
                } else {
                    if (parseInt(minuto) == 54) {
                        $("#HdActualizar").val("1");
                    } else {
                        $("#HdActualizar").val("0");
                    }

                }
                /*
                if (parseInt(minuto) > 44) {
                location.reload;
                window.location.href = "FrmHorarioAmbiente.aspx?x=0";
                }
                */
            }

            setInterval(MuestraTabla, 15000);
        });
        
        
    </script>
</head>
<body>
    <form id="form1" runat="server">     
        <input type="hidden" id="HdActualizar" value="0" />            
        <table width="100%">
            <tr align="center">                
                <td style="width:10%;">
                   <img alt="" src="usat.png" style="width: 90px; height: 60px" />
                </td>
                <td style="width:35%; font-size:large;" colspan="2">
                    <b>
                    UNIVERSIDAD CAT&Oacute;LICA<br/>SANTO TORIBIO DE MOGROVEJO                
                    </b>                    
                </td>
                <td style="width:55%;">
                    <table style="font-size:small">
                        <tr>
                            <td style="background:#E3CD54">&nbsp;&nbsp;</td>
                            <td>Pre Grado</td>
                            <td style="background:#96C1EB">&nbsp;&nbsp;</td>
                            <td>PostGrado</td>
                            <td style="background:#A2A0A2">&nbsp;&nbsp;</td>
                            <td>Profesio.</td>
                            <td style="background:#FFA200">&nbsp;&nbsp;</td>
                            <td>2da Esp.</td>
                            <td style="background:#DEB0EF">&nbsp;&nbsp;</td>
                            <td>Esc. Pre.</td>
                        </tr>
                        <tr>
                            <td style="background:#56FFEE">&nbsp;&nbsp;</td>
                            <td>Complement.</td>
                            <td style="background:#36A168">&nbsp;&nbsp;</td>
                            <td>Academic.</td>
                            <td style="background:#FF2D29">&nbsp;&nbsp;</td>
                            <td>Sust. Tesis</td>
                            <td style="background:#BC995C">&nbsp;&nbsp;</td>
                            <td>Eventos</td>
                            <td style="background:#D3D3D3">&nbsp;&nbsp;</td>
                            <td>Mantenim.</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="3" >
                    <br />
                    <asp:Label ID="lblFecha" runat="server" Font-Bold="True" Font-Size="Medium"></asp:Label>                    
                </td>                
                <td></td>
            </tr>
            
        </table>     
        <div id="tabla" runat="server">                        
        </div>          
        <input type='hidden' id='valor' value='0'/>
    </form>
</body>
</html>
