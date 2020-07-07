<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmClonarCurso.aspx.vb" Inherits="academico_FrmClonarCurso" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        body
        {
            font-size:smaller;
            font-family:Tahoma;
        }
        .col1
        {
            width: 10%;
        }
        .colCbo1
        {
            width: 30%;
        }
        .col2
        {
            width: 5%;
        }
        .btn
        {
            border:1px;
            border-style:solid;
            width:100px;
            height:22px;
        }
    </style>
    <script type="text/javascript" language="javascript">

        function MarcarCursos(obj) {
            //asignar todos los controles en array
            var arrChk = document.getElementsByTagName('input');
            for (var i = 0; i < arrChk.length; i++) {
                var chk = arrChk[i];
                //verificar si es Check
                if (chk.type == "checkbox" ) {
                    chk.checked = obj.checked;
                    if (chk.id != obj.id) {
                        PintarFilaMarcada(chk.parentNode.parentNode, obj.checked)
                    }
                    
                    if (chk.disabled == "disabled") {
                        chk.checked = false;                        
                    }                    
                }
            }
        }

        function PintarFilaMarcada(obj, estado) {
            if (estado == true) {
                obj.style.backgroundColor = "#FFE7B3"
            } else {
                obj.style.backgroundColor = "white"
            }
        }        
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="100%">
            <tr>
                <td class="col1">Del Semestre</td>
                <td class="colCbo1">
                    <asp:DropDownList ID="cboCicloInicio" runat="server" AutoPostBack="True">
                    </asp:DropDownList>
                </td>
                <td  class="col2">Al Semestre</td>
                <td class="colCbo1">
                    <asp:DropDownList ID="cboCicloFinal" runat="server" AutoPostBack="True">
                    </asp:DropDownList>
                </td>
                <td>
                    &nbsp;</td>                
            </tr>
            <tr>
                <td class="col1">Carrera profesional</td>
                <td>
                    <asp:DropDownList ID="cboEscuela" runat="server">
                    </asp:DropDownList>
                </td>
                <td class="col2"></td>
                <td>
                    <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn" />
                </td>
                <td></td>
            </tr>
            <tr>
                <td colspan="6">
                    <asp:Button ID="btnGenerar" runat="server" Text="Generar" CssClass="btn" />
                    <asp:Label ID="lblMensaje" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="6">                
                    <asp:GridView ID="gvCursos" runat="server" Width="100%" 
                        DataKeyNames="codigo_cup,codigo_Amb"
                        AutoGenerateColumns="False">
                        <Columns>
                            <asp:TemplateField HeaderText="" >
                                <HeaderTemplate>
                                    <asp:CheckBox ID="chkHeader" runat="server" onclick="MarcarCursos(this)"/>
                                </HeaderTemplate>
                                <ItemTemplate>                
                                    <asp:CheckBox ID="chkElegir" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="codigo_cup" HeaderText="ID" />
                            <asp:BoundField DataField="nombre_dac" HeaderText="DPTO. ACADEMICO" />
                            <asp:BoundField DataField="descripcion_pes" HeaderText="PLAN DE ESTUDIOS" />
                            <asp:BoundField DataField="nombre_cur" HeaderText="CURSO" />
                            <asp:BoundField DataField="grupoHor_Cup" HeaderText="GRUPO" />
                            <asp:BoundField DataField="creditos_cur" HeaderText="CRED." />
                            <asp:BoundField DataField="vacantes_cup" HeaderText="VAC." />
                            <asp:BoundField DataField="descripcion_tam" HeaderText="T. AMBIENTE" />
                            <asp:BoundField DataField="dia_Lho" HeaderText="DIA" />
                            <asp:BoundField DataField="horainicio_lho" HeaderText="INICIA" />
                            <asp:BoundField DataField="horaFin_Lho" HeaderText="FIN" />
                            <asp:BoundField DataField="codigo_Amb" HeaderText="COD. AMB." />
                            <asp:BoundField DataField="Estado" HeaderText="ESTADO" />
                            <asp:BoundField DataField="codigo_Hor" HeaderText="codigo_Hor" />
                        </Columns>
                        <HeaderStyle BackColor="#e33439" ForeColor="White" />                        
                        <RowStyle Font-Size="Small" />
                    </asp:GridView>                
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="HdUsuario" runat="server" />
    </form>
</body>
</html>
