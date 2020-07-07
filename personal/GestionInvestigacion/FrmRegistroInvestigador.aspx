<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmRegistroInvestigador.aspx.vb"
    Inherits="GestionInvestigacion_FrmRegistroInvestigador" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Registro de Investigador</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0" />
    <meta http-equiv="Pragma" content="no-cache" />
    <link href="../assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/css/material.css" rel="stylesheet" type="text/css" />
    <link rel='stylesheet' href='../assets/css/style.css?x=1' />

    <script src="../assets/js/jquery.js" type="text/javascript"></script>

    <script type="text/javascript">
        function CerrarSesionORCID() {
            $.ajax({
                //url: 'https://sandbox.orcid.org/userStatus.json?logUserOut=true',
                url: 'https://orcid.org/userStatus.json?logUserOut=true',
                dataType: 'jsonp',
                success: function(result, status, xhr) {
                    console.log("Logged In: " + result.loggedIn);
                },
                error: function(xhr, status, error) {
                    console.log(status);
                }
            });
        }
        function AbrirPestana(url) {
            window.open(url, "_blank")
        }
    </script>

    <style type="text/css">
        .content
        {
            margin-left: 0px;
        }
        .page_header
        {
            background-color: #FAFCFF;
        }
        .form-control
        {
            border-radius: 0px;
            box-shadow: none;
            border-color: #718FAB;
            height: 28px;
            font-weight: 300;
            color: black;
        }
        .row
        {
            margin-right: 0px;
            margin-left: 0px;
            padding: 4px;
            vertical-align: middle;
        }
        .checkbox label
        {
            padding-left: 1px;
        }
        input[type="checkbox"] + label
        {
            color: Black;
        }
        #btnORCID
        {
            border: 1px solid #D3D3D3;
            padding: .3em;
            background-color: #fff;
            border-radius: 8px;
            box-shadow: 1px 1px 3px #999;
            cursor: pointer;
            color: #995;
            font-weight: bold;
            line-height: 24px;
            vertical-align: middle;
        }
        #btnORCID:hover
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
            width: 24px;
            height: 24px;
        }
        .btnIr
        {
            border: 1px solid #D3D3D3;
            padding: .3em;
            background-color: #fff;
            border-radius: 8px;
            box-shadow: 1px 1px 3px #999;
            cursor: pointer;
            color: #339CFF;
            font-weight: bold;
            font-size: .9em;
            line-height: 24px;
            vertical-align: middle;
        }
        .btnIr:hover
        {
            border: 1px solid #338caf;
            color: #338caf;
        }
        :-ms-input-placeholder.form-control
        {
            line-height: 0px;
        }
    </style>
</head>
<body>
    <div class="wrapper">
        <div class="content">
            <form id="form1" runat="server">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <div class="page_header">
                <div class="pull-left">
                    <i class="icon ti-bookmark-alt page_header_icon"></i><span class="main-text">Actividad
                        de Investigación</span>
                </div>
            </div>
            <div class="panel panel-piluku">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        Registro de Colaborador con Actividad de Investigación
                    </h3>
                </div>
                <div class="panel-body">
                    <div class="form-group">
                        <div class="col-sm-12 col-md-12">
                            <div id="DivObservacionesInv" runat="server">
                            </div>
                            <div class="row">
                                <label class="col-sm-2 col-md-2 control-label ">
                                    Nombre</label>
                                <div class="col-sm-7 col-md-8">
                                    <asp:HiddenField runat="server" ID="hdInv" Value="0" Visible="false" />
                                    <asp:Label runat="server" ID="lblNombre" CssClass="h5">-</asp:Label>
                                </div>
                                <div class="col-sm-2 col-md-2">
                                    <div class="pull-right-btn">
                                        <asp:Button runat="server" CssClass="btn btn-primary" ID="btnRegistrarInvestigador"
                                            Text="Agregar" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <label class="col-sm-2 col-md-2 control-label ">
                                    <div id="divTipoDoc">
                                        N° Documento</div>
                                </label>
                                <div class="col-sm-3 col-md-3">
                                    <asp:Label runat="server" ID="lblNroDocumento" CssClass="h5">-</asp:Label>
                                </div>
                                <label class="col-sm-1 col-md-1 control-label ">
                                    Tipo</label>
                                <div class="col-sm-4 col-md-4">
                                    <asp:Label runat="server" ID="lblTipo" CssClass="h5">-</asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <label class="col-sm-2 col-md-2 control-label ">
                                    <div id="divAreaFacultad">
                                        Área</div>
                                </label>
                                <div class="col-sm-8 col-md-8">
                                    <asp:Label runat="server" ID="lblArea" CssClass="h5">-</asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <label class="col-sm-2 col-md-2 control-label ">
                                    Departamento</label>
                                <div class="col-sm-8 col-md-8">
                                    <asp:Label runat="server" ID="lblDepartamento" CssClass="h5">-</asp:Label>
                                </div>
                            </div>
                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                <ContentTemplate>
                                    <div class="row">
                                        <div id="DivMensaje" runat="server">
                                        </div>
                                    </div>
                                    <div class="row">
                                        <label class="col-sm-3 col-md-2 control-label ">
                                            Línea USAT</label>
                                        <div class="col-sm-8 col-md-8">
                                            <asp:DropDownList runat="server" ID="cboLinea" CssClass="form-control">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <h4>
                                            <label class="label label-primary">
                                                Línea OCDE</label>
                                        </h4>
                                    </div>
                                    <asp:UpdatePanel ID="UpdatePanelOCDE">
                                        <ContentTemplate>
                                            <div class="row">
                                                <label class="col-sm-3 col-md-2 control-label">
                                                    Área Temática:</label>
                                                <div class="col-sm-6 col-md-4">
                                                    <asp:DropDownList runat="server" ID="cboArea" CssClass="form-control" AutoPostBack="true">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <label class="col-sm-3 col-md-2 control-label">
                                                    Sub Área:</label>
                                                <div class="col-sm-8 col-md-6">
                                                    <asp:DropDownList runat="server" ID="cboSubArea" CssClass="form-control" AutoPostBack="true">
                                                        <asp:ListItem Value="">-- Seleccione --</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <label class="col-sm-3 col-md-2 control-label">
                                                    Disciplina:</label>
                                                <div class="col-sm-8 col-md-6">
                                                    <asp:DropDownList runat="server" ID="cboDisciplina" CssClass="form-control">
                                                        <asp:ListItem Value="">-- Seleccione --</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="cboArea" EventName="SelectedIndexChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="cboSubArea" EventName="SelectedIndexChanged" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                    <div class="row">
                                        <h4>
                                            <label class="label label-danger ">
                                                Identificadores</label>
                                        </h4>
                                    </div>
                                    <div class="row">
                                        <label class="col-md-1 control-label">
                                        </label>
                                        <div class="col-sm-3 col-md-2">
                                            <asp:CheckBox runat="server" ID="chkRegina" CssClass="col-md-12 checkbox" Text="RENACYT (REGINA)"
                                                AutoPostBack="true" />
                                        </div>
                                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                            <ContentTemplate>
                                                <div runat="server" id="DivRenacyt">
                                                    <div class="col-sm-4 col-md-2">
                                                        <asp:TextBox runat="server" ID="txtRenacyt" Font-Size="12px" CssClass="form-control"
                                                            placeholder="P0000000"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="chkDina" EventName="CheckedChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                    <div class="row">
                                        <label class="col-md-1 control-label">
                                        </label>
                                        <div class="col-sm-3 col-md-2">
                                            <asp:CheckBox runat="server" ID="chkDina" CssClass="col-md-12 checkbox" Text="CTI VITAE (DINA)"
                                                AutoPostBack="true" />
                                        </div>
                                        <asp:UpdatePanel runat="server">
                                            <ContentTemplate>
                                                <div runat="server" id="DivDina">
                                                    <div class="col-sm-6 col-md-6">
                                                        <asp:TextBox runat="server" ID="txtUrlDina" Font-Size="12px" CssClass="form-control"
                                                            placeholder="https://dina.concytec.gob.pe/appDirectorioCTI/VerDatosInvestigador.do?id_investigador=XXXXX"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-3 col-md-3">
                                                        <asp:Button runat="server" ID="btnDina" CssClass="btnIr" Text="IR A CTI VITAE" OnClientClick="AbrirPestana('https://dina.concytec.gob.pe/appDirectorioCTI/')" />
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="chkDina" EventName="CheckedChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                    <div class="row">
                                        <label class="col-md-1 control-label">
                                        </label>
                                        <div class="col-sm-3 col-md-2">
                                            <asp:CheckBox runat="server" ID="chkOrcid" CssClass="col-md-12 checkbox" Text="ORCID"
                                                AutoPostBack="true" />
                                        </div>
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <div runat="server" id="DivOrcid">
                                                    <div class="col-sm-4 col-md-6">
                                                        <asp:TextBox runat="server" ID="txtOrcid" CssClass="form-control" placeholder="XXXX-XXXX-XXXX-XXXX"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-5 col-md-3">
                                                        <button id="btnORCID" runat="server" onclick="CerrarSesionORCID();window.open('https://orcid.org/oauth/authorize?client_id=APP-ID59GM8T9DT29K07&amp;response_type=code&amp;scope=/authenticate&amp;family_names=Saavedra Sanchez&amp;given_names=Hugo Enrique&amp;email=esaavedra@usat.edu.pe&amp;lang=es&amp;show_login=true&amp;redirect_uri=https://orcid.org/my-orcid', '_blank', 'toolbar=no, scrollbars=yes, width=500, height=700, top=100, left=500'); return false;">
                                                            <img id="orcid-id-icon" src="https://orcid.org/sites/default/files/images/orcid_16x16.png"
                                                                alt="ORCID iD icon">
                                                            Registrar o Conectar ORCID</button>
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="chkOrcid" EventName="CheckedChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                    <div class="row">
                                        <label class="col-md-1 control-label">
                                        </label>
                                        <div class="col-sm-3 col-md-2">
                                            <asp:CheckBox runat="server" ID="chkScopus" CssClass="col-md-12 checkbox" Text="SCOPUS ID"
                                                AutoPostBack="true" />
                                        </div>
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <ContentTemplate>
                                                <div runat="server" id="DivScopus">
                                                    <div class="col-sm-6 col-md-6">
                                                        <asp:TextBox runat="server" ID="txtScopusID" CssClass="form-control" placeholder="XXXXXXXXXXX"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-3 col-md-3">
                                                        <asp:Button ID="btnScopus" runat="server" CssClass="btnIr" Text="IR A SCOPUS" OnClientClick="AbrirPestana('https://www.scopus.com/')" />
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="chkScopus" EventName="CheckedChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                    <div class="row">
                                        <label class="col-md-1 control-label">
                                        </label>
                                        <div class="col-sm-3 col-md-2">
                                            <asp:CheckBox runat="server" ID="chkResearcherID" CssClass="col-md-12 checkbox" Text="RESEARCHER ID"
                                                AutoPostBack="true" />
                                        </div>
                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                            <ContentTemplate>
                                                <div runat="server" id="DivResearcherID">
                                                    <div class="col-sm-6 col-md-6">
                                                        <asp:TextBox runat="server" ID="txtResearcherID" CssClass="form-control" placeholder="X-XXXX-XXXX"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-3 col-md-3">
                                                        <asp:Button ID="btnResearcherID" runat="server" CssClass="btnIr" Text="IR A RESEARCHER ID"
                                                            OnClientClick="AbrirPestana('https://www.researcherid.com/')" />
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="chkResearcherID" EventName="CheckedChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                    <div class="row">
                                        <div class="modal fade" id="mdRegistro" runat="server" role="dialog" aria-labelledby="myModalLabel"
                                            style="z-index: 0;" aria-hidden="true" data-backdrop="static" data-keyboard="false">
                                            <div runat="server" id="BloqueoModal" class='modal-backdrop fade in' style='height: 100%;'>
                                            </div>
                                            <div class="modal-dialog modal-lg" id="modalReg">
                                                <div class="modal-content">
                                                    <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
                                                        <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="float: right;">
                                                            <span aria-hidden="true" class="ti-close" style="color: White;"></span>
                                                        </button>
                                                        <h5 class="modal-title" id="myModalLabel3">
                                                            El registro será enviado a Vicerectorado de Investigación para su evaluación, ¿Seguro
                                                            que desea guardar?</h5>
                                                    </div>
                                                    <div class="modal-footer">
                                                        <center>
                                                            <asp:Button runat="server" ID="btnGuardar" Text="SI" CssClass="btn btn-primary" />
                                                            <asp:Button runat="server" ID="btnCancelar" Text="NO" CssClass="btn btn-danger" />
                                                        </center>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnRegistrarInvestigador" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>
            </form>
        </div>
    </div>
</body>
</html>
