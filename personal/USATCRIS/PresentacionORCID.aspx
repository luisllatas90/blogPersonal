<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PresentacionORCID.aspx.vb"
    Inherits="USATCRIS_PresentacionORCID" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script src="../assets/js/jquery.js" type="text/javascript"></script>

    <script type="text/javascript">

        $(document).ready(function() {
            var cod = undefined
            cod = ObtenerValorGET("code")
            if (cod != undefined) {
                Consultar(cod);
            }

        })

        function Consultar(cod) {

            var frm = { "client_id": "APP-1F3MW3HOFU4NP0H3", "client_secret": "1acc4edc-c731-48c0-9c3a-870c14fa2251", "grant_type": "authorization_code", "code": cod, "redirect_uri": "http://serverdev/campusvirtual/personal/USATCRIS/LoginORCID.aspx" }

            console.log(frm);
            $.ajax({
                type: "POST",
                url: "https://sandbox.orcid.org/oauth/token",
                data: frm,
                dataType: "json",
                headers: {
                    "Access-Control-Allow-Origin": "http://serverdev/campusvirtual/personal/USATCRIS/PresentacionORCID.aspx",
                    'Access-Control-Allow-Methods': 'POST'
                },
                accepts: {
                    text: "application/json"
                },
                success: function(data) {
                    console.log(data);
                },
                error: function(result) {
                    console.log(result);
                }
            });
            //        fnLoading(false)
        }


        function ObtenerValorGET(valor) {
            var valoraDevolver = "";
            // capturamos la url
            var loc = document.location.href;
            // si existe el interrogante
            if (loc.indexOf('?') > 0) {
                // cogemos la parte de la url que hay despues del interrogante
                var getString = loc.split('?')[1];
                // obtenemos un array con cada clave=valor
                var GET = getString.split('&');
                var get = {};
                // recorremos todo el array de valores
                for (var i = 0, l = GET.length; i < l; i++) {
                    var tmp = GET[i].split('=');
                    if (tmp[0] == valor) {
                        valoraDevolver = tmp[1]
                    }
                    //get[tmp[0]] = unescape(decodeURI(tmp[1]));
                }
                return valoraDevolver;
            }
        }


    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
    </div>
    </form>
</body>
</html>
