<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmRegistroEvento.aspx.vb"
    Inherits="GradosYTitulos_FrmRegistroEvento" %>

<html lang="es">
<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0">
    <title>Ejemplo Imágenes</title>

    <script src="assets/js/jquery.js" type="text/javascript"></script>

    <script src="assets/js/bootstrap.min.js" type="text/javascript"></script>

    <link href="assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <style>
        body
        {
            padding-top: 20px;
            padding-bottom: 20px;
        }
    </style>
</head>
<body>
    <div class="container">
        <div class="panel panel-primary">
            <div class="panel-body">
                <form name="form1" id="form1" method="post" action="guarda.php" enctype="multipart/form-data">
                <h4 class="text-center">
                    Cargar Multiple Archivos</h4>
                <div class="form-group">
                    <label class="col-sm-2 control-label">
                        Archivos</label>
                    <div class="col-sm-8">
                        <input type="file" class="form-control" id="archivo[]" name="archivo[]" multiple="multiple">
                    </div>
                    <button type="submit" class="btn btn-primary">
                        Cargar</button>
                </div>
                </form>
            </div>
        </div>
    </div>
</body>
</html>
