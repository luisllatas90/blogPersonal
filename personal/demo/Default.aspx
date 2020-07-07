<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="demo_Default" %>

<!DOCTYPE html>
<html lang="en">
  <head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <!-- The above 3 meta tags *must* come first in the head; any other head content must come *after* these tags -->
    <meta name="description" content="">
    <meta name="author" content="">
    <link rel="icon" href="css/favicon.ico">

    <title>Login</title>

    <!-- Bootstrap core CSS -->
    <link href="css/bootstrap.min.css" rel="stylesheet">

    <!-- IE10 viewport hack for Surface/desktop Windows 8 bug -->
    <link href="css/ie10-viewport-bug-workaround.css" rel="stylesheet">

    <!-- Custom styles for this template -->
    <link href="css/signin.css" rel="stylesheet">

    <!-- Just for debugging purposes. Don't actually copy these 2 lines! -->
    <!--[if lt IE 9]><script src="../../assets/js/ie8-responsive-file-warning.js"></script><![endif]-->
    <script src="js/ie-emulation-modes-warning.js"></script>

    <!-- HTML5 shim and Respond.js for IE8 support of HTML5 elements and media queries -->
    <!--[if lt IE 9]>
      <script src="https://oss.maxcdn.com/html5shiv/3.7.3/html5shiv.min.js"></script>
      <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
    <![endif]-->
    
    <link rel="stylesheet" href="css/sweetalert/sweetalert2.min.css">
    
    <script src="js/sweetalert/es6-promise.auto.min.js"></script>
    <script src="js/sweetalert/sweetalert2.js"></script>
    
    <link href="../assets/fontawesome-5.2/css/regular.min.css" rel="stylesheet" type="text/css" />
    <script src="../assets/fontawesome-5.2/js/all.min.js" type="text/javascript"></script>
    
    <script type="text/javascript">
    
        /* Mostrar mensajes al usuario. */
        function showMessage(message, messagetype) {              
            swal({ 
                title: message,
                type: messagetype ,
                confirmButtonText: "OK" ,
                confirmButtonColor: "#45c1e6"
            }).catch(swal.noop);
        }  

        function alertConfirm(ctl, event, titulo, icono) {
            // STORE HREF ATTRIBUTE OF LINK CTL (THIS) BUTTON
            var defaultAction = $(ctl).prop("href");          

            // CANCEL DEFAULT LINK BEHAVIOUR
            event.preventDefault();            
            
            swal({
                title: titulo,                
                type: icono,
                showCancelButton: true ,
                confirmButtonText: "SI" ,
                confirmButtonColor: "#45c1e6" ,
                cancelButtonText: "NO"
            }).then(function (isConfirm) {
                if (isConfirm) {
                    window.location.href = defaultAction;
                    return true;
                } else {
                    return false;
                }
            }).catch(swal.noop);
        } 
    
    </script>
    
  </head>

  <body>

      <form runat="server">
      
        <div class="container">
            <div class="col-sm-12">
                <center>
                <div class="panel panel-default col-sm-6" style="float: none;">
                    <div class="panel-body">
                        <div role="tabpanel" style=" text-align:center;">
                            <%--<h2 class="form-signin-heading">Acceso</h2>--%>
                            <div class='' style=" text-align:center;">
                                <h1 class="heading">
                                    <img alt="Logo USAT" src="../assets/images/logousat.png" style="width:110px; height: 110px; text-align: center;" /></h1>
                                <h1 class="heading">
                                    <img src="../assets/images/logocampus1.png" style="width:50%; height: 25px" /></h1>	 
                            </div>
                            
                            <br />

                            <ul class="nav nav-tabs piluku-tabs" role="tablist" >
					            <li role="presentationlogin" id="loginalumni" runat="server" style="width:33%;">
					                <a href="http://serverdev/campusestudiante/" aria-controls="home" role="tab" data-toggle="tab"> 
					                    <span class="fa fa-user"></span>
					                    <div id="labelrps">Estudiante</div>
					                </a>
					            </li>
					            <li role="presentationlogin" id="loginalumniegre"  runat="server" style="width:33%;">
					                <a href="http://serverdev/campusestudiante/" aria-controls="profile" role="tab" data-toggle="tab">
					                    <span class="fa fa-graduation-cap"></span>
					                    <div id="labelrps">Egresado</div>
					                </a>
					            </li>
					            <li role="presentationlogin" class="active" id="logincolaborador"  runat="server" style="width:33%;">
					                <a href="#logincolaborador_tab" aria-controls="profile" role="tab" data-toggle="tab">
					                    <span class="fa fa-user"></span>
					                    <div id="labelrps">Colaborador</div>
					                </a>
					            </li>
				            </ul>
                            
                            <br />
                            
                            <div class="tab-content piluku-tab-content">
                            
                                <div role="tabpanel" class="tab-pane " id="loginalumni_tab" runat="server">
                                    
                                </div>
                                
                                <div role="tabpanel" class="tab-pane " id="loginalumniegre_tab" runat="server">
                                    
                                </div>
                                
                                <div role="tabpanel" class="tab-pane active" id="logincolaborador_tab" runat="server">
                                
                                    <label for="inputEmail" class="sr-only">Usuario</label>
                                    <!--<input type="email" id="inputEmail" class="form-control" placeholder="Usuario" required autofocus>-->
                                    <asp:TextBox ID="txtUsuario" runat="server"  class="form-control" placeholder="Usuario"></asp:TextBox>
                                    
                                    <br />
                                    
                                    <label for="inputPassword" class="sr-only">Clave</label>
                                    <!--<input type="password" id="inputPassword" class="form-control" placeholder="Clave" required> -->
                                    <asp:TextBox ID="txtClave" class="form-control" placeholder="Clave" TextMode="Password" runat="server"></asp:TextBox>
                                    
                                    <br />
                                    <!--
                                    <div class="checkbox">
                                      <label>
                                        <input type="checkbox" value="remember-me"> Remember me
                                      </label>
                                    </div>
                                    -->
                                    
                                    <br />
                                    
                                    <asp:LinkButton ID="btnIngresar" class="btn btn-lg btn-danger btn-block" runat="server" Text='<i class="fa fa-user"></i> Ingresar' />
                                    <!--<button class="btn btn-lg btn-primary btn-block" type="submit">Sign in</button>-->
                                
                                </div>
                            
                            </div>
                            
                            <br /> 
                            
                            <div style="text-align:center; font-size: 10px">Universidad Católica Santo Toribio de Mogrovejo<br />
                                        Av. San Josemaría Escrivá de Balaguer N° 855 Chiclayo - Perú</div>
                        </div>
                    </div>
                </div>
                </center>
            </div>
        </div>
             
      </form>



    <!-- IE10 viewport hack for Surface/desktop Windows 8 bug -->
    <script src="../../assets/js/ie10-viewport-bug-workaround.js"></script>
  </body>
  
</html>
