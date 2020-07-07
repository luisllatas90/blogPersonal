<%@ Page Language="VB" AutoEventWireup="false" CodeFile="LoginORCID.aspx.vb" Inherits="USATCRIS_LoginORCID" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <%--Compatibilidad con IE--%>
<%--    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />--%>
    <%--Compatibilidad con IE--%>
    <%--<meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0" />--%>

    <%--<script src="../assets/js/jquery.js" type="text/javascript"></script>--%>

    <script type="text/javascript">

//        $(document).ready(function() {
//            var cod = undefined
//            cod = ObtenerValorGET("code")
//            alert(cod);
//            if (cod != undefined) {
//                alert(cod + " - 2")
//                Consultar(cod);
//            }

        //        })
/*
        var oauthWindow;
        function openORCID() {
            //var oauthWindow = window.open("https://orcid.org/oauth/authorize?client_id=APP-ID59GM8T9DT29K07&response_type=code&scope=/authenticate&redirect_uri=https://developers.google.com/oauthplayground", "_blank", "toolbar=no, scrollbars=yes, width=500, height=600, top=500, left=500");
            var oauthWindow = window.open("https://sandbox.orcid.org/oauth/authorize?client_id=APP-1F3MW3HOFU4NP0H3&response_type=code&scope=/authenticate&redirect_uri=http://serverdev/campusvirtual/personal/USATCRIS/PresentacionORCID.aspx", "_blank", "toolbar=no, scrollbars=yes, width=500, height=600, top=500, left=500");
        }
*/
    </script>

    <style type="text/css">
        #connect-orcid-button
        {
            border: 1px solid #D3D3D3;
            padding: .3em;
            background-color: #fff;
            border-radius: 8px;
            box-shadow: 1px 1px 3px #999;
            cursor: pointer;
            color: #999;
            font-weight: bold;
            font-size: .8em;
            line-height: 24px;
            vertical-align: middle;
        }
        #connect-orcid-button:hover
        {
            border: 1px solid #338caf;
            color: #338caf;
        }
        #orcid-id-icon
        {
            display: block;
            margin: 0 .5em 0 0;
            padding: 0;
            float: left;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <button id="connect-orcid-button" onclick="openORCID()">
            <img id="orcid-id-icon" src="https://orcid.org/sites/default/files/images/orcid_24x24.png"
                width="24" height="24" alt="ORCID iD icon" />Register or Connect your ORCID
            iD</button>
    </div>
    </form>
</body>
</html>
