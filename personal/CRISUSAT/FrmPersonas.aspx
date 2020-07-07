<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmPersonas.aspx.vb" Inherits="CRISUSAT_FrmPersonas" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Personas - Investigadores DSpaceCRIS</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0" />
    <meta http-equiv="Pragma" content="no-cache" />
    <link href="../assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/css/material.css" rel="stylesheet" type="text/css" />
    <link rel='stylesheet' href='../assets/css/style.css?x=1' />

    <script src="../assets/js/jquery.js" type="text/javascript"></script>

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
        /*.checkbox label
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
        }*/</style>
</head>
<body>
    <div class="wrapper">
        <div class="content">
            <form id="form1" runat="server">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <div class="page_header">
                <div class="pull-left">
                    <i class="icon ti-bookmark-alt page_header_icon"></i><span class="main-text">Personas
                        - Investigadores DSpaceCRIS</span>
                </div>
            </div>
            <div class="panel panel-piluku">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        Peronas - Investigadores DSpaceCRIS
                    </h3>
                </div>
                <div class="panel-body">
                    <div class="form-group">
                        <asp:TextBox runat="server" ID="txtRespuesta" Width="100%" TextMode="MultiLine" Rows="5"></asp:TextBox>
                        <asp:GridView runat="server" ID="gvUnidadOrganizacional" Width="100%" AllowSorting="True">
                        </asp:GridView>
                    </div>
                </div>
            </div>
            </form>
        </div>
    </div>
</body>
</html>
