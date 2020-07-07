<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmMantenimientoLimitePresupuestal.aspx.vb" Inherits="indicadores_POA_PROTOTIPOS_Registrar_POA" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/estilo_poa.css" rel="stylesheet" type="text/css" media="screen" />
        <script type="text/javascript" language="javascript">
        function Valida_Presupuesto(sender, arguments) {
            egreso = document.getElementById("txt_montoEgreso").value
            ingreso = document.getElementById("txt_montoIngreso").value

            //Quito todas las Comas para Poder Comparar
            egreso = egreso.replace(/,/g, "")
            ingreso = ingreso.replace(/,/g, "")
                        
            if (!/^([0-9]+(\.[0-9][0-9]?)?)*$/.test(ingreso)) {
                sender.errormessage = "Solo se admiten Números y el Punto Decimal en los Ingresos."
                arguments.IsValid = false
            } else if (!/^([0-9]+(\.[0-9][0-9]?)?)*$/.test(egreso)) {
                sender.errormessage = "Solo se admiten Números y el Punto Decimal en los Egresos."
                arguments.IsValid = false
            }
//             else {
//                if (parseFloat(presupuesto_act) > parseFloat(presupuesto_dis)) {
//                    sender.errormessage = "Presupuesto de Actividad : S/. " + presupuesto_act + " Debe Ser Menor a Presupuesto Disponible de POA : S/. " + parseFloat(presupuesto_dis)
//                    arguments.IsValid = false;
//                } else {
//                    arguments.IsValid = true;
//                }
//            }
                 
        }
    </script>
    <style type="text/css">
         .oculto
            {
             visibility:hidden;
            }
            .AlineadoDerecha{
                text-align:right;
                font-family:Verdana;
                font-size:8.5pt;
            }
            .titulo_rojo
            {
                position:absolute;
                top:42px;
                left:21px;
                font-size:12px;
                font-weight:bold;
                font-family:"Helvetica Neue",Helvetica,Arial,sans-serif;
                color:red;
                background-color:White;
                padding-bottom:10px;
                padding-left:3px;
                padding-right:3px;    
                z-index:1;    
            }
            .titulo_naranja
            {
                position:absolute;
                top:150px;
                left:21px;
                font-size:12px;
                font-weight:bold;
                font-family:"Helvetica Neue",Helvetica,Arial,sans-serif;
                color:#994C00;
                background-color:White;
                padding-bottom:10px;
                padding-left:3px;
                padding-right:3px;    
                z-index:1;    
            }
         
            .contorno_rojo
            {
                position:relative;
                top:5px;
                border:1px solid red;
                padding-left:4px;
                padding-top:20px;
                padding-right:4px;
                padding-bottom:4px;
            }
            .contorno_naranja
            {
                position:relative;
                top:25px;
                border:1px solid #994C00;
                padding-left:4px;
                padding-top:20px;
                padding-right:4px;
                padding-bottom:4px;
            }

        .nombre_poa
        {
            color:#468847;
            font-weight:bold;
            padding-top:3px;
            padding-bottom:3px;
            font-size:12px;
        }
        .utilidad_poa
        {
            color:#aa6708;
            font-weight:bold;
            padding-top:4px;
            padding-bottom:4px;
            text-align:right;
        }
            
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="titulo_poa">
        <asp:Label ID="Label2" runat="server" Text="Asignar Limite de Presupuesto POA"></asp:Label>
    </div>
    <div class="titulo_rojo">
        <asp:Label ID="Label1" runat="server" Text="Limite Presupuestal"></asp:Label>
    </div>
    <div class="titulo_naranja">
        <asp:Label ID="Label3" runat="server" Text="Centro Presupuestal"></asp:Label>
    </div>
   
    <div class="contorno_poa">
    
    <div class="contorno_rojo">
        <table style="width:100%">
            <tr>
                <td width="200px" ><asp:Label ID="Label4" CssClass="nombre_poa" Font-Underline="true" runat="server" Text="Plan Operativo Anual"></asp:Label></td>
                <td><asp:Label ID="txt_nombre_poa" CssClass="nombre_poa" Font-Underline="true"  runat="server"></asp:Label></td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>Monto de Ingreso</td>
                <td><asp:TextBox ID="txt_montoIngreso" runat="server" Width="140px" CssClass="AlineadoDerecha" TabIndex="2" AutoPostBack="true"></asp:TextBox></td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>Monto de Egreso</td>
                    <td><asp:TextBox ID="txt_montoEgreso" runat="server" Width="140px" CssClass="AlineadoDerecha" TabIndex="3" AutoPostBack="true"></asp:TextBox>
                       <asp:CustomValidator ID="CustomValidator2" runat="server" 
                        ClientValidationFunction="Valida_Presupuesto" 
                        ControlToValidate="txt_montoEgreso" display="Dynamic" ErrorMessage="" 
                        ValidationGroup="grupo1">&nbsp;</asp:CustomValidator>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr style="display:none;">
                <td>Utilidad</td>
                    <td><asp:Label ID="txt_utilidad" CssClass="utilidad_poa" Width="140px" runat="server"></asp:Label></td>
                <td>&nbsp;</td>
            </tr>
        </table>
    </div>
   
    <div class="contorno_naranja">
        <table style="width:100%">
          
            <tr>
                <td width="200px">Tipo de Actividad</td>
                <td width="500px"><asp:DropDownList ID="ddl_tipoActividad" runat="server" Width="185px" TabIndex="5" ></asp:DropDownList></td>
                <td width="100px"></td>
                <td width="100px"></td>
                <td width="100px"></td>
            </tr>
            <tr>
                <td>Centro de Costos</td>
                <td>&nbsp; 
                        <asp:TreeView ID="treePrueba" runat="server" ExpandDepth="0" 
                            Font-Size="XX-Small" MaxDataBindDepth="4">
                            <Nodes>
                            </Nodes>
                            <HoverNodeStyle CssClass="menuporelegir" />
                        </asp:TreeView>
                    <asp:TextBox ID="txt_nombre_cco" runat="server" Width='10px' CssClass="caja_poa" 
                        Enabled="false" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txt_codigo_cco" runat="server" Width="10px" CssClass="caja_poa" 
                        BackColor="#FFFFCC" Enabled="False" Visible="False"></asp:TextBox>
                    <asp:Button ID="btn_Agregar" runat="server" Text="   Agregar" 
                        CssClass="agregar2" Visible="False" />
                    <br />
                </td>
                <td></td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td></td>
                <td colspan="2">
                    <asp:GridView ID="dgv_ceco" runat="server" AutoGenerateColumns="False" 
                        Width="550px" DataKeyNames="codigo_cco,codigo_tac,codigo_asp">
                        <Columns>
                            <asp:BoundField HeaderText="codigo_cco" DataField="codigo_cco" Visible="False" >
                            <ItemStyle Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="descripcion_cco" HeaderText="CENTRO COSTO" />
                             
                            <asp:BoundField DataField="codigo_tac" HeaderText="codigo_tac" 
                                Visible="False" />
                            <asp:BoundField DataField="descripcion_tac" HeaderText="ACTIVIDAD" />
                             
                            <asp:TemplateField HeaderText="ELIMINAR" ShowHeader="False">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" 
                                        CommandName="Delete" ImageUrl="../../images/eliminar.gif" Text="Eliminar" OnClientClick="return confirm('¿Desea Eliminar Registro?.')"/>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="80px" />
                            </asp:TemplateField>
                
                
                            <asp:BoundField DataField="codigo_asp" HeaderText="codigo_asp" 
                                Visible="False" />
                
                
                        </Columns>
                        <HeaderStyle BackColor="#3871b0" ForeColor="White" Height="25px" />
                    </asp:GridView>
                    <br />
                </td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td colspan="5">
                        <div runat="server" id="aviso">
                            <asp:Label ID="lblrpta" runat="server" Font-Bold="true"></asp:Label>
                        </div>
                    </td>
                
            </tr>
                        
            </table>
       </div>
       <br />
       <br />
        <table style="width:100%">
            <tr>
                <td>
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                        HeaderText="Errores" ShowMessageBox="False" ShowSummary="True" 
                        ValidationGroup="grupo1" />
                </td>
            </tr>
            <tr>
            <td align="right">
                <asp:Button ID="cmdGuardar" runat="server" CssClass="btnGuardar" ValidationGroup="grupo1" Text="   Guardar"/>&nbsp;
                <asp:Button ID="cmdCancelar" runat="server" CssClass="btnCancelar" Text="  Cancelar"/>
            </td>
            </tr>
        </table>
 
        <asp:HiddenField ID="HdCodigo_poa" runat="server" />
        <asp:HiddenField ID="HdCodigo_cco" runat="server" />
    </div>


    </form>
</body>
</html>
