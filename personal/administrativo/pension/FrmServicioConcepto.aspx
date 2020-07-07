<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmServicioConcepto.aspx.vb" Inherits="administrativo_pension_FrmServicioConcepto" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Servicio Concepto</title>
    <link href="../../../private/estilo.css" rel="stylesheet" type="text/css" />    
    <script type="text/javascript">    
        var patron = new Array(2, 2, 4)
        var patron2 = new Array(1, 3, 3, 3, 3)
        function mascara(d, sep, pat, nums) {
            if (d.valant != d.value) {
                val = d.value
                largo = val.length
                val = val.split(sep)
                val2 = ''
                for (r = 0; r < val.length; r++) {
                    val2 += val[r]
                }
                if (nums) {
                    for (z = 0; z < val2.length; z++) {
                        if (isNaN(val2.charAt(z))) {
                            letra = new RegExp(val2.charAt(z), "g")
                            val2 = val2.replace(letra, "")
                        }
                    }
                }
                val = ''
                val3 = new Array()
                for (s = 0; s < pat.length; s++) {
                    val3[s] = val2.substring(0, pat[s])
                    val2 = val2.substr(pat[s])
                }
                for (q = 0; q < val3.length; q++) {
                    if (q == 0) {
                        val = val3[q]
                    }
                    else {
                        if (val3[q] != "") {
                            val += sep + val3[q]
                        }
                    }
                }
                d.value = val
                d.valant = val
            }
        }        
</script>
<script type="text/javascript" language="javascript">
    function MarcarCursos(obj) {
        //asignar todos los controles en array
        var arrChk = document.getElementsByTagName('input');
        for (var i = 0; i < arrChk.length; i++) {
            var chk = arrChk[i];
            //verificar si es Check
            if (chk.type == "checkbox") {
                chk.checked = obj.checked;
                if (chk.id != obj.id) {
                    PintarFilaMarcada(chk.parentNode.parentNode, obj.checked)
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
            <td colspan="2">Semestre Académico: 
                <asp:DropDownList ID="cboCiclo" runat="server">
                </asp:DropDownList>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="Label3" runat="server" Text="Tipo:"></asp:Label>
                <asp:DropDownList ID="cboTipo" runat="server" AutoPostBack="True">
                    <asp:ListItem Value="false" Text="No Complementario"></asp:ListItem>
                    <asp:ListItem Value="true" Text="Complementario"></asp:ListItem>
                </asp:DropDownList>
                <br />
                <asp:Label ID="lblMensaje" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width:50%" valign="top">
                <asp:GridView ID="gvServicios" runat="server" Width="100%" 
                    AutoGenerateColumns="False" DataKeyNames="codigo_sco">
                    <Columns>
                        <asp:BoundField DataField="codigo_Sco" HeaderText="codigo_Sco" 
                            Visible="False" />
                        <asp:BoundField DataField="descripcion_Sco" HeaderText="Concepto" />
                        <asp:BoundField DataField="esComplementario_Sco" HeaderText="Tipo" />
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkHeader" runat="server" onclick="MarcarCursos(this)" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkElegir" runat="server"  />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:HiddenField ID="hdcodigo_dsb" runat="server" />
            </td>
            <td valign="top">
                <asp:Button ID="btnConfigurar" runat="server" Text="Configurar Fechas" CssClass="modificar2" Width="130px" Height="22px" />
                <br />
                <table id="tbDatos" width="100%" runat="server">
                    <tr>
                        <td><asp:Label ID="Label1" runat="server" Text="Fecha:"></asp:Label></td>
                        <td><asp:TextBox ID="txtfecha" runat="server" onkeyup="mascara(this,'/',patron,true)"></asp:TextBox></td>
                    </tr>                    
                    <tr>
                        <td><asp:Label ID="Label2" runat="server" Text="Mes:"></asp:Label></td>
                        <td>
                        <asp:DropDownList ID="cboMes" runat="server">
                        </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td align="right">
                            <asp:Button ID="btnCerrar" runat="server" Text="Volver" CssClass="regresar2" Width="100px" Height="22px" />
                        </td>
                    </tr>
                </table>                                
                <br />
                <asp:Button ID="btnAgregar" runat="server" Text="Agregar" CssClass="agregar2" Width="100px" Height="22px" />                            
                <asp:Label ID="lblMensajeDetalle" runat="server" Font-Bold="True" 
                    ForeColor="Red"></asp:Label>                            
                <asp:GridView ID="gvFechas" runat="server" Width="100%" 
                    AutoGenerateColumns="False" DataKeyNames="codigo_dsb,codigo_sco,mes_dsb">
                    <Columns>
                        <asp:BoundField DataField="codigo_dsb" HeaderText="codigo_dsb" 
                            Visible="False" />
                        <asp:BoundField DataField="codigo_sco" HeaderText="codigo_sco" 
                            Visible="False" />
                        <asp:BoundField DataField="mes_dsb" HeaderText="mes_dsb" Visible="False" />
                        <asp:BoundField DataField="descripcion_Sco" HeaderText="Concepto" />
                        <asp:BoundField DataField="MesNombre" HeaderText="Mes" />
                        <asp:BoundField DataField="fechaBanco_dsb" HeaderText="F. Banco"></asp:BoundField>
                        <asp:CommandField ButtonType="Image" EditImageUrl="../../../images/editar.gif" 
                            ShowEditButton="True">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:CommandField>
                        <asp:CommandField ShowDeleteButton="True" ButtonType="Image" 
                            DeleteImageUrl="../../../images/eliminar.gif" DeleteText="" >
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:CommandField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
    </div>
    </form>
</body>
</html>
