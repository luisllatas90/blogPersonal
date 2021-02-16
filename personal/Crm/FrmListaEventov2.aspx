<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmListaEventov2.aspx.vb"
    Inherits="Crm_FrmListaEvento" %>

<!DOCTYPE html>
<html lang="en">
<head runat="server">
    <title></title>

    <script>
        window.fbAsyncInit = function() {
            FB.init({
                appId: '141332259763972',
                cookie: true,
                xfbml: true,
                version: 'v2.8'
            });
            FB.AppEvents.logPageView();
        };

        (function(d, s, id) {
            var js, fjs = d.getElementsByTagName(s)[0];
            if (d.getElementById(id)) { return; }
            js = d.createElement(s); js.id = id;
            js.src = "//connect.facebook.net/en_US/sdk.js";
            fjs.parentNode.insertBefore(js, fjs);
        } (document, 'script', 'facebook-jssdk'));


//        function checkLoginState() {
        FB.getAuthResponse()(function(response) {
                statusChangeCallback(response);
            });
//        }

//        function statusChangeCallback(response) {
//            console.log('statusChangeCallback');
//            console.log(response);
//            if (response.status === 'connected') {
//                testAPI();
//                testAPI2();
//            } else if (response.status === 'not_authorized') {
//                FB.login(function(response) {
//                    statusChangeCallback2(response);
//                }, { scope: 'public_profile,email' });
//            } else {
//                alert("not connected, not logged into facebook, we don't know");
//            }
//        }
//        function statusChangeCallback2(response) {
//            console.log('statusChangeCallback2');
//            console.log(response);
//            if (response.status === 'connected') {
//                testAPI();
//                testAPI2();
//            } else if (response.status === 'not_authorized') {
//                console.log('still not authorized!');

//            } else {
//                alert("not connected, not logged into facebook, we don't know");
//            }
//        }
//        function testAPI() {
//            console.log('Welcome!  Fetching your information.... ');
//            FB.api('/user_friends', function(response) {
//                console.log(response)
////                console.log('Successful login for: ' + response.first_name);
////                document.getElementById('status').innerHTML =
////                'Thanks for logging in, ' + response.first_name + '!';

//            });
//        }
//        function testAPI2() {
//            FB.api('/me/email', function(response) {
//                console.log(response)
//            });
//        }
    </script>

</head>
<body class="">
    <div class="fb-login-button" data-max-rows="1" data-size="large" data-button-type="continue_with"
        onlogin="checkLoginState();" data-show-faces="false" data-auto-logout-link="false"
        data-use-continue-as="false">
    </div>
    <fb:login-button scope="public_profile,email" onlogin="checkLoginState();">
    <br />
    <label id="status"></label>
</fb:login-button>
</body>
</html>
