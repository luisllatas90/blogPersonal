<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmAcceso.aspx.vb" Inherits="Odontologia_Docente_frmAcceso" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../../private/estilo.css"rel="stylesheet" type="text/css" />     
    <link href="../../Scripts/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/js1_12/bootstrap.js" type="text/javascript"></script>
    <script src="../../Scripts/js1_12/jquery-1.12.3.min.js" type="text/javascript"></script>    
    <style type="text/css">
	    .login-form {
		    width: 340px;
    	    margin: 50px auto;
	    }
        .login-form form {
    	    margin-bottom: 15px;
            background: #f7f7f7;
            box-shadow: 0px 2px 2px rgba(0, 0, 0, 0.3);
            padding: 30px;
        }
        .login-form h2 {
            margin: 0 0 15px;
        }
        .form-control, .btn {
            min-height: 38px;
            border-radius: 2px;
        }
        .btn {        
            font-size: 15px;
            font-weight: bold;
        }
    </style>
     <script type="text/javascript">
        function ShowMessage(message, messagetype) {
            var cssclass;
            switch (messagetype) {
                case 'Success':
                    cssclass = 'alert-success'
                    break;
                case 'Error':
                    cssclass = 'alert-danger'
                    break;
                case '1':
                    cssclass = 'alert-danger'
                    break;

                case 'Warning':
                    cssclass = 'alert-warning'
                    break;
                default:
                    cssclass = 'alert-info'
            }

            if (cssclass != 'alert-danger') {
                $('#alert_container').append('<div id="alert_div" style="margin: 0 0.5%; -webkit-box-shadow: 3px 4px 6px #999;" class="alert alert-success"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><span>' + message + '</span></div>');
            } else {
                $('#alert_container').append('<div id="alert_div" style="margin: 0 0.5%; -webkit-box-shadow: 3px 4px 6px #999;" class="alert alert-danger"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><span>' + message + '</span></div>');
            }
        }
    </script>
</head>
<body>    
    <div class="messagealert" id="alert_container"></div>
    <div class="login-form">        
        <form id="form1" runat="server">
            <h2 class="text-center">Acceso al Sistema</h2> 
            <div class="form-group">
                <!--<input type="text" class="form-control" placeholder="Username" required="required">-->
                <asp:TextBox ID="txtUsuario" CssClass="form-control" runat="server"></asp:TextBox>
            </div> 
            <div class="form-group">
                <!--<input type="password" class="form-control" placeholder="Password" required="required">-->
                <asp:TextBox ID="txtClave" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>             
            </div>
            <div class="form-group">
                <!--<button type="submit" class="btn btn-primary btn-block">Log in</button>-->
                <asp:Button ID="btnIngresar" CssClass="btn btn-primary btn-block" runat="server" Text="Ingresar"/>
            </div>        
            <div class="clearfix">
                <!--<label class="pull-left checkbox-inline"><input type="checkbox"> Remember me</label>-->
                <asp:LinkButton ID="lnkClave" runat="server" class="pull-right">¿Olvidaste tu clave?</asp:LinkButton>
                <!--<a href="#" class="pull-right">¿Olvidaste tu clave?</a>-->
            </div>   
        </form>
    </div>
    
</body>
</html>
