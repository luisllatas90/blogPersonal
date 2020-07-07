<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmCalificadorCurso_Exportar.aspx.vb"
    Inherits="GestionCurricular_frmCalificadorCurso_Exportar" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0" />
    <meta name="google" value="notranslate" />
    <title>Calificador Asignatura</title>
    <!-- custom scrollbar stylesheet -->
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv='X-UA-Compatible' content='IE=7' />
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />
    <link href="../scripts/css/bootstrap.css" rel="Stylesheet" type="text/css" />
    <link href="../assets/fontawesome-5.2/css/regular.min.css" rel="stylesheet" type="text/css" />

    <script src="../scripts/js/jquery-1.12.3.min.js" type="text/javascript"></script>

    <script src='../assets/js/jquery-ui-1.10.3.custom.min.js' type="text/javascript"></script>

    <script src="js/popper.js" type="text/javascript"></script>

    <script src="../scripts/js/bootstrap.min.js" type="text/javascript"></script>

    <script src="../assets/fontawesome-5.2/js/all.min.js" type="text/javascript"></script>

    <script src="js/bootbox.min.js" type="text/javascript"></script>

    <script src="js/gridviewscroll.js" type="text/javascript"></script>

    <script type="text/javascript">

//        window.onload = function() {
//            var gridViewScroll = new GridViewScroll({
//                elementID: "gvNotas", // Target element id
//                width : "100%", // Integer or String(Percentage)
//                height : 800, // Integer or String(Percentage)
//                //freezeColumn : true, // Boolean
//                //freezeFooter : false, // Boolean
//                freezeColumnCssClass: "", // String
//                freezeFooterCssClass : "", // String
//                freezeHeaderRowCount : 5, // Integer
//                freezeColumnCount : 2 // Integer
//                //onscroll: function (scrollTop, scrollLeft) // onscroll event callback
//            });
//            gridViewScroll.enhance();
//        }
    
        function StartCount() {
            var t = 300 * 1000;
            var x = setInterval(function() {
                t -= 1000;
                $("#contador").html((t / 1000) + " seg");

                if (t <= 0) {
                    clearInterval(x);
                    $("#contador").html("Expiró");
                }
            }, 1000);
        }

        function ShowMessage(message, messagetype) {
            var cssclss;
            switch (messagetype) {
                case 'Success':
                    cssclss = 'alert-success'
                    break;
                case 'Error':
                    cssclss = 'alert-danger'
                    break;
                case 'Warning':
                    cssclss = 'alert-warning'
                    break;
                default:
                    cssclss = 'alert-info'
            }
            $('#alert_container').append('<div id="alert_div" style="margin: 0 0.5%; -webkit-box-shadow: 3px 4px 6px #999;" class="alert ' + cssclss + '"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><span>' + message + '</span></div>');
        }

        function soloNumeros(e, txt) {
            var key = window.Event ? e.which : e.keyCode
            var artxt = txt.id.split("_")
            var nro = "";
            var nrox = 0;
            if (key == 13) {
                nro = artxt[1].substr(3)
                nrox = parseInt(nro) + 1;
                nro = nrox.toString();
                if (nro.length == 1) {
                    nro = "0" + nro;
                }
                $('#' + artxt[0] + "_" + "ctl" + nro + "_" + artxt[2]).focus();
                console.log(nro);
                console.log('#' + artxt[0] + "_" + "ctl" + nro + "_" + artxt[2]);
                return true;
            }
            return (key >= 48 && key <= 57)
        }

        function soloCalificacion(txt) {
            var nota = parseInt(txt.value)
            if (nota > 20) {
                $('#' + txt.id).val(0);
            }
            if (nota < 14) {
                txt.style.color = 'red';
            }
            else {
                txt.style.color = 'blue';
            }
        }

        function openModal() {
            $('#txtToken').val("");
            $('#txtToken').focus();
            $('#myModalSMS').modal('show');
        }

        function closeModal() {
            $('#txtToken').val("");
            $('#myModalSMS').modal('hide');
        }

        function showDivs(acc) {
            if ($('#divEnviar').is(':hidden')) {
                $('#divEnviar').show('slide', { direction: 'left' }, 1000);
            } else {
                $('#divEnviar').hide('slide', { direction: 'left' }, 1000);
            }

            if (acc == "hide") {
                $('#divEnviar').hide();
            }
        }

        function onChangeToken() {
            $('#divAlertModal').hide();
            $('#lblMensaje').val('');
        }
        
    </script>

    <style type="text/css">
        .no-border
        {
            border: 0;
            box-shadow: none;
        }
        .panel
        {
            margin-bottom: 0px;
        }
        .dvcolumns1
        {
        	width: 100px;
        }
        .dvcolumns2
        {
        	width: 300px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" method="post" autocomplete="off">
    <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release">
    </asp:ScriptManager>
    <div class="container-fluid">
        <br />
        <div class="panel panel-default" id="pnlLista" runat="server">
            <div class="panel panel-heading">
                <div class="row">
                </div>
            </div>
            <div class="panel panel-body">
                <div class="row">
                    <div class="col-md-12">
                        <%--<div id="gridContainer">--%>
                        <div class="table-responsive"> 
                        
                            <asp:UpdatePanel ID="updNotas" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                <ContentTemplate>
                                    <asp:GridView ID="gvNotas" runat="server" AutoGenerateColumns="false" ShowHeader="true"
                                        DataKeyNames="codigo_Dma, nombre_alu, codigo_pso, inhabilitado_dma" OnRowDataBound="gvNotas_OnRowDataBound"
                                        OnRowCreated="gvNotas_OnRowCreated" CssClass="table table-bordered table-hover">
                                        <Columns>
                                            <asp:BoundField DataField="codigoUniver_Alu" HeaderText="Código" HeaderStyle-Width="100px"
                                                ItemStyle-Width="100px" FooterStyle-Width="100px" ItemStyle-CssClass="dvcolumns1"/>
                                            <asp:BoundField DataField="nombre_alu" HeaderText="Apellidos y Nombres" HeaderStyle-Width="300px"
                                                ItemStyle-Width="300px" FooterStyle-Width="300px" ItemStyle-Wrap="true" ItemStyle-CssClass="dvcolumns2" />
                                        </Columns>
                                        <EmptyDataTemplate>
                                            No se encontraron Datos
                                        </EmptyDataTemplate>
                                        <HeaderStyle BackColor="#D9534F" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Center"
                                            Font-Size="10.5px" Font-Bold="true" />
                                        <RowStyle Font-Size="11px" />
                                        <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                                        <%--<PagerStyle ForeColor="#003399" HorizontalAlign="Center" />--%>
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            
                        </div>
                      </div> 
                    <%--</div>--%>   
                </div>
            </div>
        </div>
    </div>
    </form>
    
</body>
</html>
