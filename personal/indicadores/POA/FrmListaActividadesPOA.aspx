﻿<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmListaActividadesPOA.aspx.vb"
    Inherits="indicadores_POA_PROTOTIPOS_Registrar_POA" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0" />
    <meta http-equiv="Pragma" content="no-cache" />
    <link rel='stylesheet' href='../../assets/css/bootstrap.min.css' />
    <link rel='stylesheet' href='../../assets/css/material.css' />
    <link rel='stylesheet' href='../../assets/css/style.css' />

    <script type="text/javascript" src='../../assets/js/jquery.js'></script>

    <script type="text/javascript" src='../../assets/js/app.js'></script>

    <%--<script type="text/javascript" src='../../assets/js/jquery-ui-1.10.3.custom.min.js'></script>--%>

    <script type="text/javascript" src='../../assets/js/bootstrap.min.js'></script>

    <script type="text/javascript" src='../../assets/js/jquery.nicescroll.min.js'></script>

    <script type="text/javascript" src='../../assets/js/wow.min.js'></script>

    <script type="text/javascript" src="../../assets/js/jquery.nicescroll.min.js"></script>

    <script type="text/javascript" src='../../assets/js/jquery.loadmask.min.js'></script>

    <%--    <script type="text/javascript" src='../../assets/js/jquery.accordion.js'></script>

    <script type="text/javascript" src='../../assets/js/materialize.js'></script>

    <script type="text/javascript" src='../../assets/js/bic_calendar.js'></script>

    <script type="text/javascript" src='../../assets/js/core.js'></script>--%>

    <script type="text/javascript" src='../../assets/js/jquery.countTo.js'></script>

    <script type="text/javascript" src="../../assets/js/noty/jquery.noty.js"></script>

    <script type="text/javascript" src='../../assets/js/noty/layouts/top.js'></script>

    <script type="text/javascript" src='../../assets/js/noty/layouts/default.js'></script>

    <script type="text/javascript" src="../../assets/js/noty/notifications-custom.js"></script>

    <script type="text/javascript" src='../../assets/js/jquery.dataTables.min.js'></script>

    <script type="text/javascript" src='../../assets/js/funciones.js'></script>

    <%--    <script type="text/javascript" src="../../assets/js/DataJson/jsselect.js?x=10"></script>

    <script type="text/javascript" src='../../assets/js/form-elements.js'></script>

    <script type="text/javascript" src='../../assets/js/select2.js'></script>

    <script type="text/javascript" src='../../assets/js/jquery.multi-select.js'></script>--%>

    <script type="text/javascript" src='../../assets/js/bootstrap-colorpicker.js'></script>

    <link rel='stylesheet' href='../../assets/css/jquery.dataTables.min.css' />
    <link href="css/estilo_poa.css" rel="stylesheet" type="text/css" media="screen" />

    <script language="javascript" type="text/javascript">

        $(document).ready(function() {
            //            fnLoading(false);
        });

        function ModalAdjuntar(cod_acp) {
            if (confirm("Esta Seguro que Desea Cerrar el Proyecto?")) {
                var res = cerrarProyecto(cod_acp)
                //                alert(res)
                if (res == 1) {
                    cambia_imagen(cod_acp)
                    if (confirm("¿Adjuntar Archivo?")) {
                        $(".CerrarProy").each(function() {
                            if ($(this).attr("src") == "../../Images/candadocerrado.png" && $(this).attr("cp") == cod_acp) {
                                $(this).attr("data-toggle", "modal");
                                $(this).attr("data-target", "#mdRegistro");
                            } else {
                                $(this).removeAttr("data-toggle");
                                $(this).removeAttr("data-target");
                            }
                        })
                        $("#cod_acp").val(cod_acp)
                        $("#txtfile").val("");
                        fnVer(cod_acp)
                    } else {
                        $('.CerrarProy').removeAttr("data-toggle");
                        $('.CerrarProy').removeAttr("data-target");
                    }
                } else if (res == -1) {
                    window.location.href = "../../../sinacceso.html"
                }
                else {
                    fnMensaje('warning', "No se Pudo Cerrar Proyecto");
                }
            } else {
                $('.CerrarProy').removeAttr("data-toggle");
                $('.CerrarProy').removeAttr("data-target");
            }
        }

        function cambia_imagen(cp) {
            $(".CerrarProy").each(function() {
                if ($(this).attr("cp") == cp) {
                    $(this).removeAttr("src");
                    $(this).removeAttr("OnClick");
                    $(this).removeAttr("data-toggle");
                    $(this).removeAttr("data-target");
                    $(this).attr("src", "../../Images/candadocerrado.png");
                }
            });
        }


        function cerrarProyecto(cod_acp) {
            var resultado = 0
            $.ajax({
                type: "POST",
                url: "../../DataJson/Poa/movimientos_POA.aspx",
                data: { "cod_acp": cod_acp, "action": "Cerrar" },
                dataType: "json",
                cache: false,
                async: false,
                success: function(data) {
                    //                    console.log(data);
                    if (data[0].msje == "ErrorSession") {
                        //                        fnMensaje('success', data[0].msje);
                        //                        fnMensaje('warning', data[0].msje);
                        //                        window.location.href = "'" + data.lnk + "'"
                        resultado = -1;
                    } else {
                        fnMensaje('success', data[0].msje);
                        resultado = 1;
                    }
                },
                error: function(result) {
                    console.log(result);
                    fnMensaje('warning', "No se Pudo Cerrar Proyecto");
                    resultado = 0;

                }
            });
            return resultado;
        }

        function ModalAdjuntar2(cod_acp) {
            $("#cod_acp").val(cod_acp)
            $("#txtfile").val("");
            $("#divMessage").html("")
            fnVer(cod_acp)
            $('.AdjuntoProy').attr("data-toggle", "modal");
            $('.AdjuntoProy').attr("data-target", "#mdRegistro");
        }

        function fnGuardar() {
            if ($("#txtfile").val() == "") {
                $("#divMessage").html("<p>Debe Selecionar un Archivo.</p>")
            } else {
                //                $("#btnGuardar").attr("disabled", true);
                //                fnLoadingDiv("divLoading", true);
                $("#divMessage").html("")
                SubirArchivo($("#cod_acp").val(), $("#cod_acp").val());
                //                fnLoadingDiv("divLoading", false);
                //                $("#btnGuardar").removeAttr("disabled");
                fnVer($("#cod_acp").val())
            }
        }

        function SubirArchivo(c, n) {
            var flag = false;
            var form = new FormData();
            var files = $("#txtfile").get(0).files;
            //            console.log(files);
            // Add the uploaded image content to the form data collection
            if (files.length > 0) {
                form.append("action", "Upload")
                form.append("cod_acp", $("#cod_acp").val())
                form.append("UploadedImage", files[0]);
            }
            //            console.log(form);
            $.ajax({
                type: "POST",
                url: "../../DataJson/Poa/movimientos_POA.aspx",
                data: form,
                dataType: "json",
                cache: false,
                contentType: false,
                processData: false,
                success: function(data) {
                    flag = true;
                    //                    console.log(data);
                    $("#txtfile").val("");
                    if (data[0].msje == "ErrorSession") {
                        window.location.href = "../../../sinacceso.html"
                    } else if (data[0].msje == "OK") {
                        $("#divMessage").html("<p>Archivo Cargado Correctamente.</p>")
                    } else {
                        $("#divMessage").html("<p>No se Pudo Subir Archivo.</p>")
                    }

                    //		              fnMensaje('warning', 'Subiendo Archivo');
                    //		              $('#divMessage').addClass('alert alert-success alert-dismissable');
                    //		              $fileupload = $('#fileData');
                    //		              $fileupload.replaceWith($fileupload.clone(true));
                },
                error: function(result) {
                    //                    console.log(result);
                    $("#divMessage").html("<p>" & result[0].msje & "</p>");
                    flag = false;
                }
            });
            return flag;
        }

        function fnVer(c) {
            $.ajax({
                type: "POST",
                url: "../../DataJson/POA/movimientos_POA.aspx",
                data: { "action": "Ver", "cod_acp": c },
                dataType: "json",
                cache: false,
                success: function(data) {
                    //                    console.log(data);
                    //                    if (data[0].msje == "ErrorSession") {
                    //                        window.location.href = "../../../sinacceso.html"
                    //                    }
                    var tb = '';
                    var i = 0;
                    var filas = data.length;
                    for (i = 0; i < filas; i++) {
                        tb += '<tr>';
                        tb += '<td>' + (i + 1) + "" + '</td>';
                        tb += '<td><i  class="' + data[i].nExtension + '"></i> ' + data[i].nArchivo + '</td>';
                        tb += '<td>';
                        tb += '<button onclick="fnDownload(\'' + data[i].cCod + '\');" class="btn btn-primary"><i  class=" ion-android-download"><span></span></i></button>';
                        tb += '</td>';
                        tb += '</tr>';
                    }
                    if (filas > 0) $('#mdFiles').modal('toggle');
                    $('#tbFiles').html(tb);
                },
                error: function(result) {
                    console.log(result);
                }
            });
        }

        function fnDownload(id_ar) {
            var flag = false;
            var form = new FormData();
            form.append("action", "Download");
            form.append("IdArchivo", id_ar);
            // alert();
            //            console.log(form);
            $.ajax({
                type: "POST",
                url: "../../DataJson/POA/movimientos_POA.aspx",
                data: form,
                dataType: "json",
                cache: false,
                contentType: false,
                processData: false,
                success: function(data) {
                    flag = true;
                    //                    if (data[0].msje == "ErrorSession") {
                    //                        window.location.href = "../../../sinacceso.html"
                    //                    }
                    //                    console.log(data);
                    jQuery.each(data, function(i, val) {
                        var file = 'data:application/octet-stream;base64,' + val.File;
                        downloadWithName(file, val.Nombre);
                        if (navigator.userAgent.indexOf("NET") > -1) {
                            var param = { 'Id': id_ar };
                            window.open("../../administrativo/Tesoreria/Rendiciones/AppRendiciones/DataJson/DescargarArchivo.aspx?Id=" + id_ar, 'ta', "");
                        }
                    });
                    //                    var link = document.createElement("a");
                    //                    link.download = data[0].Nombre;
                    //                    link.href = file;
                    //                    link.click();
                },
                error: function(result) {
                    console.log(result);
                    flag = false;
                }
            });
            return flag;

        }

        function downloadWithName(uri, name) {
            var link = document.createElement("a");
            link.download = name;
            link.href = uri;
            link.click();
            // alert(link);
        }
    </script>

    <style type="text/css">
        .contorno_poa
        {
            position: relative;
            top: 25px;
            border: 1px solid #C0C0C0;
            border-bottom: 1px solid #C0C0C0;
            left: 10px;
            right: 18px;
            bottom: 10px;
            width: 98%;
            padding-left: 6px;
            padding-top: 17px;
            padding-bottom: 30px;
            cursor: default;
            height: 100%;
        }
        .titulo_poa
        {
            position: absolute;
            top: 13px;
            left: 15px;
            font-size: 14px;
            font-weight: bold;
            font-family: "Helvetica Neue" ,Helvetica,Arial,sans-serif;
            color: #337ab7;
            background-color: White;
            padding-bottom: 10px;
            padding-left: 5px;
            padding-right: 5px;
            z-index: 1;
        }
        table tr th
        {
            border-color: rgb(169,169,169);
            border-style: solid;
            border-width: 1px;
            padding: 4px;
            text-align: center;
            font-weight: bold;
        }
        table tbody tr td
        {
            padding: 4px;
        }
        table tbody tr td input
        {
            cursor: pointer;
        }
        .mensajeExito
        {
            background-color: #d9edf7;
            border: 1px solid #808080;
            font-weight: bold;
            color: #31708f;
            height: 30px;
            padding: 3px;
        }
        .mensajeEliminado
        {
            color: #8a6d3b;
            background-color: #fcf8e3;
            border: 1px solid #C5BE51;
            font-weight: bold;
            height: 30px;
            padding: 3px;
        }
        .mensajeError
        {
            background-color: #f2dede;
            border: 1px solid #E9ABAB;
            font-weight: bold;
            color: #a94442;
            height: 30px;
            padding: 3px;
        }
    </style>
</head>
<body style="background-color: White; overflow-y: hidden">
    <form id="form1" runat="server">
    <asp:HiddenField runat="server" ID="hd_seleccion_codigoacp" Value="-1" />
    <div class="titulo_poa">
        <asp:Label ID="Label1" runat="server" Text="Programas Y Proyectos"></asp:Label>
    </div>
    <div class="contorno_poa">
        <table width="100%">
            <tr>
                <td width="100px">
                    Plan Estratégico
                </td>
                <td width="510px">
                    <asp:DropDownList ID="ddlPlan" runat="server" Width="500">
                    </asp:DropDownList>
                </td>
                <td>
                    Ejercicio Presupuestal
                </td>
                <td>
                    <asp:DropDownList ID="ddlEjercicio" runat="server" Width="140">
                    </asp:DropDownList>
                </td>
                <td>
                    Estado
                </td>
                <td>
                    <asp:DropDownList ID="ddlestado" runat="server">
                        <asp:ListItem Value="T">Todos</asp:ListItem>
                        <asp:ListItem Value="P">Pendientes</asp:ListItem>
                        <asp:ListItem Value="A">Asignados</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Button ID="btnBuscar" runat="server" Text="   Buscar" CssClass="btnBuscar" />
                </td>
            </tr>
        </table>
        <br />
        <div style="font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; font-size: 13px;
            color: #337ab7; padding-bottom: 10px; font-weight: bold">
            <asp:Label ID="Label3" runat="server" Text="Planes Operativos"></asp:Label>
        </div>
        <asp:GridView ID="dgvpoa" runat="server" Width="100%" AutoGenerateColumns="False"
            DataKeyNames="codigo_poa,responsable_poa" HeaderStyle-Height="20px" CellPadding="4">
            <Columns>
                <asp:BoundField HeaderText="PLAN OPERATIVO ANUAL" DataField="nombre_poa">
                    <HeaderStyle Width="40%" />
                </asp:BoundField>
                <asp:BoundField HeaderText="RESPONSABLE" DataField="responsable">
                    <HeaderStyle Width="25%" />
                </asp:BoundField>
                <asp:BoundField HeaderText="EJERCICIO" DataField="descripcion_ejp">
                    <HeaderStyle Width="5%" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="INGRESOS" DataField="limite_ingreso" DataFormatString="{0:N}">
                    <HeaderStyle Width="9%" />
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField HeaderText="EGRESOS" DataField="limite_egreso" DataFormatString="{0:N}">
                    <HeaderStyle Width="9%" />
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField HeaderText="EXCEDENTE" DataField="utilidad" DataFormatString="{0:N}">
                    <HeaderStyle Width="9%" />
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:CommandField HeaderText="ASIGNAR" ShowSelectButton="True" ButtonType="Image"
                    SelectImageUrl="../../images/previo.gif" SelectText="ASIGNAR">
                    <HeaderStyle Width="3%" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:CommandField>
                <asp:BoundField DataField="codigo_poa" HeaderText="codigo_poa" Visible="False" />
            </Columns>
            <HeaderStyle BackColor="#3871b0" ForeColor="White" Height="25px" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <EmptyDataTemplate>
                No se Encontraron Registros</EmptyDataTemplate>
        </asp:GridView>
        <br />
        <asp:Label ID="lblMensajeFormulario" runat="server" Text=""></asp:Label>
        <br />
        <br />
        <div id="DetProgramasProyectos" visible="false" runat="server">
            <asp:Label ID="Label2" runat="server" Text="Detalle de Programas y Proyectos" Style="font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif;
                font-size: 13px; color: #337ab7; padding-bottom: 10px; font-weight: bold"></asp:Label>
            <br />
            <table width="100%">
                <tr>
                    <td>
                        <asp:Button ID="btnNuevo" runat="server" Text="   Nuevo Programa/Proyecto" CssClass="btnNuevo"
                            Width="200px" />
                    </td>
                    <td style="text-align: right; vertical-align: middle">
                        <%--<img alt="" src="" style="background-color: #FFFFFF; border: solid 1px #000000; width: 7px; height: 7px;" />--%>
                        <div style="border: solid 1pt; width: 10px; height: 10px; float: left; background-color: #FFFFFF;
                            top: 3pt; vertical-align: middle; display: inline">
                        </div>
                        <div style="float: left">
                            &nbsp;<asp:Label ID="Label6" runat="server"> Proceso de Registro</asp:Label>&nbsp;&nbsp;
                        </div>
                        <%--<img alt="" src="" style="background-color: #87CEEB; width: 7px; height: 7px; vertical-align: middle;" />--%>
                        <div style="border: solid 1pt; width: 10px; height: 10px; float: left; background-color: #87CEEB;
                            vertical-align: middle">
                        </div>
                        <div style="float: left">
                            &nbsp;
                            <asp:Label runat="server"> Enviado a Planificación</asp:Label>&nbsp;&nbsp;
                        </div>
                        <%--<img alt="" src="" height="10" width="10" style="background-color: #F08080; width: 7px;
                            height: 7px; vertical-align: middle;" />--%>
                        <div style="border: solid 1pt; width: 10px; height: 10px; float: left; background-color: #F08080;
                            top: 3pt; vertical-align: middle; display: inline">
                        </div>
                        <div style="float: left">
                            &nbsp;
                            <asp:Label ID="Label4" runat="server"> Observado Por Planificación</asp:Label>
                            &nbsp;&nbsp;
                        </div>
                        <%--<img alt="" src="" style="background-color: #90EE90; width: 7px; height: 7px; vertical-align: middle;" />--%>
                        <div style="border: solid 1pt; width: 10px; height: 10px; float: left; background-color: #90EE90;
                            top: 3pt; vertical-align: middle; display: inline">
                        </div>
                        <div style="float: left">
                            &nbsp;
                            <asp:Label ID="Label5" runat="server"> Aprobado Por Planificación</asp:Label>&nbsp;&nbsp;
                        </div>
                        <%--<img alt="" src="" style="background-color: #8181F7; width: 7px; height: 7px; vertical-align: middle;" />--%>
                        <div style="border: solid 1pt; width: 10px; height: 10px; float: left; background-color: #8181F7;
                            top: 3pt; vertical-align: middle; display: inline">
                        </div>
                        <div style="float: left">
                            &nbsp;
                            <asp:Label ID="Label9" runat="server"> Enviado a Marketing</asp:Label>
                            &nbsp;&nbsp;
                        </div>
                        <%--<img alt="" src="" style="background-color: #FA5858; width: 7px; height: 7px; vertical-align: middle;" />--%>
                        <div style="border: solid 1pt; width: 10px; height: 10px; float: left; background-color: #FA5858;
                            top: 3pt; vertical-align: middle; display: inline">
                        </div>
                        <div style="float: left">
                            &nbsp;
                            <asp:Label ID="Label7" runat="server"> Observado Por Marketing</asp:Label>
                        &nbsp;&nbsp;
                    </td>
                </tr>
                <tr style="height: 5px;">
                    <td colspan="2">
                        <div id="aviso" runat="server">
                            <asp:Label ID="lblmensaje" runat="server" Text="" Font-Bold="True"></asp:Label>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan='2'>
                        <asp:GridView ID="dgvactividades" runat="server" Width="100%" CellPadding="4" DataKeyNames="codigo_acp"
                            AutoGenerateColumns="False" ShowFooter="True">
                            <Columns>
                                <asp:BoundField HeaderText="CODIGO IEP" DataField="codigo_iep" Visible="false" />
                                <asp:BoundField HeaderText="ACTIVIDAD" DataField="resumen_acp">
                                    <HeaderStyle Width="45%" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="TIPO ACTIVIDAD" DataField="descripcion_tac" ControlStyle-Width="40px" />
                                <asp:BoundField HeaderText="RESPONSABLE" DataField="responsable">
                                    <HeaderStyle Width="33%" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="INGRESOS(S/.)" DataField="ingresos_acp" DataFormatString="{0:N}">
                                    <ItemStyle HorizontalAlign="right" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="EGRESOS(S/.)" DataField="egresos_acp" DataFormatString="{0:N}">
                                    <ItemStyle HorizontalAlign="right" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="EJERCICIO" DataField="descripcion_ejp">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:CommandField ShowSelectButton="True" ButtonType="Image" SelectText="Editar"
                                    SelectImageUrl="../../../images/editar_poa.png" HeaderText="EDITAR">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:CommandField>
                                <asp:TemplateField HeaderText="ENVIAR" ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImageButton2" runat="server" CausesValidation="False" AlternateText="Enviar"
                                            CommandName="Edit" ImageUrl="../../images/inv_paso.png" Text="Editar" OnClientClick="return confirm('¿Está Seguro que desea Enviar el Programa/Proyecto?.')" />
                                    </ItemTemplate>
                                    <ControlStyle Height="17px" Width="17px" />
                                    <HeaderStyle Width="3%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ELIMINAR" ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" AlternateText="Eliminar"
                                            CommandName="Delete" ImageUrl="../../Images/menus/noconforme_small.gif" Text="Eliminar"
                                            OnClientClick="return confirm('¿Esta Seguro que Desea Eliminar Actividad?.')" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                                </asp:TemplateField>
                                <%-- <asp:TemplateField HeaderText="CERRAR">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="CerrarProy" name="CerrarProy" runat="server" CausesValidation="False"
                                            ImageUrl="../../Images/candadocerrado.png" Width="20px" Height="20px" ToolTip="Cerrar Proyecto"
                                            OnClick="ibtnCerrarProy_Click" OnClientClick="return confirm('¿Desea Cerrar El Proyecto?.')"
                                            Text="Cerrar Proyecto" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="CERRAR">
                                    <ItemTemplate>
                                        <img id="CerrarProy" name="CerrarProy" src="../../Images/candado.png" runat="server"
                                            style="width: 25px; height: 25px" alt="Cerrar" title="Cerrar Proyecto" class="CerrarProy" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <%--<asp:TemplateField HeaderText="ADJUNTO">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="AdjuntoProg" name="AdjuntoProg" runat="server" CausesValidation="False"
                                            ImageUrl="../../Images/adjuntar.png" Width="20px" Height="20px" ToolTip="Adjuntar Archivo"
                                            OnClick="ibtnCerrarProy_Click" OnClientClick="return confirm('¿Desea Cerrar El Proyecto?.')"
                                            Text="Adjuntar Archivo" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="ADJUNTO">
                                    <ItemTemplate>
                                        <img id="AdjuntoProy" name="AdjuntoProy" src="../../Images/adjuntar.png" runat="server"
                                            style="width: 20px; height: 20px" alt="Adjuntar" title="Adjuntar Archivo" class="AdjuntoProy" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle BackColor="#3871b0" ForeColor="White" Height="25px" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>
            <asp:Label ID="lblfilas_dap" runat="server" Text=""></asp:Label>
        </div>
    </div>
    </form>
    <div class="row">
        <div class="modal fade" id="mdRegistro" role="dialog" aria-labelledby="myModalLabel"
            aria-hidden="true" role="dialog" data-backdrop="static" data-keyboard="false">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header" style="background-color: #3871B0; color: White; font-weight: bold;">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="float: right;">
                            <span aria-hidden="true" class="ti-close" style="color: White;"></span>
                        </button>
                        <h4 class="modal-title" id="myModalLabel3">
                            Cerrar Proyecto</h4>
                    </div>
                    <div class="modal-body">
                        <div id="divMessage">
                        </div>
                        <form id="frmFiles" name="frmFiles" enctype="multipart/form-data" class="form-horizontal"
                        method="post" onsubmit="return false;" action="#">
                        <div class="row">
                            <div id="msje">
                            </div>
                        </div>
                        <div class="row">
                            <input type="hidden" id="cod_acp" value="" runat="server" />
                            <input type="hidden" id="action" value="" runat="server" />
                        </div>
                        <div class="row">
                            <table style="width: 97%;" class="display dataTable">
                                <thead>
                                    <tr>
                                        <th style="width: 10%; text-align: center">
                                            N°
                                        </th>
                                        <th style="width: 80%">
                                            Archivo
                                        </th>
                                        <th style="width: 10%">
                                            Opci&oacute;n
                                        </th>
                                    </tr>
                                </thead>
                                <tbody id="tbFiles">
                                </tbody>
                            </table>
                        </div>
                        </form>
                        <br />
                        <form id="frmRegistro" name="frmRegistro" enctype="multipart/form-data" class="form-horizontal"
                        method="post" onsubmit="return false;" action="#">
                        <div class="row">
                            <div class="form-group">
                                <label class="col-sm-3 control-label">
                                    Adjunto:</label>
                                <div class="col-sm-8">
                                    <input type="file" id="txtfile" name="txtfile" class="form-control" runat="server" />
                                </div>
                                <div style="float: left;" id="divLoading" class="hidden">
                                    <img id="imgload" src="../../assets/images/loading.GIF"></div>
                            </div>
                        </div>
                        </form>
                    </div>
                    <div class="modal-footer">
                        <center>
                            <button type="button" id="btnGuardar" class="btn btn-primary" onclick="fnGuardar();">
                                Guardar</button>
                            <button type="button" class="btn btn-danger" id="btnCancelarReg" data-dismiss="modal">
                                Cancelar</button>
                        </center>
                    </div>
                </div>
            </div>
        </div>
    </div>
</body>
</html>
