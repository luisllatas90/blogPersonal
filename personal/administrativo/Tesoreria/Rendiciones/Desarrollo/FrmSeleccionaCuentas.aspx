<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmSeleccionaCuentas.aspx.vb" Inherits="administrativo_Tesoreria_Rendiciones_AppRendiciones_FrmSeleccionaCuentas" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Seleccionar Cuentas Bancarias</title>
    <meta charset="utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0"/>
    <link href="../../../../scripts/bootstrap-4.6.0/css/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="../../../../scripts/bootstrap-4.6.0/fontawesome-5.15.2/css/all.css"  rel="stylesheet" type="text/css">
    <link href="../../../../scripts/bootstrap-4.6.0/css/bootstrap-toogle.min.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container-fluid">
            <div class="col-lg-12">
                <div class="card">
                    <div class="card-header bg-danger text-white">
                        Seleccionar número de cuentas bancarias
                    </div>
                    <!--<div class="card-body">
                        <button type="button" class="btn btn-success"><i class="fa fa-plus"></i> Agregar Banco</button>
                    </div>-->
                    <div class="card-body">
                        <div class="table-responsive">
                            <table class="table">
                                <!--<input type="checkboxtoggle"  data-toggle="toggle"  data-off="✕" data-offstyle="danger"/>-->
                                <tbody id="tbCtasBancarias" runat="server">
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <script src="../../../../scripts/bootstrap-4.6.0/js/jquery-3.5.1.min.js" type="text/javascript"></script>
    <script src="../../../../scripts/bootstrap-4.6.0/js/popper.min.js" type="text/javascript"></script>
    <!--<script src="../../../../scripts/bootstrap-4.6.0/js/jquery-3.5.1.slim.min.js" type="text/javascript"></script>-->
    <script src="../../../../scripts/bootstrap-4.6.0/js/bootstrap.min.js" type="text/javascript"></script> 
    <script src="../../../../scripts/bootstrap-4.6.0/js/bootstrap-toogle.min.js" type="text/javascript"></script>
    <script src="../../../../scripts/bootstrap-4.6.0/js/sweetalert2.js" type="text/javascript"></script>
    <script src="js/usat.recursos.js" type="text/javascript"></script>
    <script src="js/frmSeleccionarCuentas.js" type="text/javascript"></script>
</body>
</html>
