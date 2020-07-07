<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmSolicitarAnulacion.aspx.vb" Inherits="administrativo_pec_frmSolicitarAnulacion" Theme="Acero" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <link href="../../../private/estilo.css" rel="stylesheet" type="text/css" />
   <script language="javascript" type="text/javascript">
       function validarDecimal(e, field) {
            key = e.keyCode ? e.keyCode : e.which
            if (key == 8) return true
            if (key > 47 && key < 58 || key == 46 || key == 13) {
               return true
            } else {
                alert("Debe ingresar un número mayor o igual a cero")      
                return false
            }
            
        }
        function sumar() {
           var cant;
           var suma = 0;
           var saldo;
           
           var grid = document.getElementById("gvResultado");
           for (i = 1; i < grid.rows.length - 1; i++) {
               if(grid.rows[i].cells[7].children[0].disabled == false){
                   cant = grid.rows[i].cells[7].children[0].value
                   saldo = grid.rows[i].cells[4].innerHTML
                   if (!/^([0-9])*[.]?[0-9]*$/.test(cant)) {
                       alert("Error al ingresar número decimal")
                       return false
                   }
                  
                   if (parseFloat(saldo) < parseFloat(cant)) {
                       alert("El importe a anular no debe exceder el saldo")
                       grid.rows[i].cells[7].children[0].value = parseFloat(saldo)
                       return false
                   }
                   if (cant == "") {
                       cant = 0
                   }
                   suma = suma + parseFloat(cant)
               }
            }
         
            grid.rows[grid.rows.length - 1].cells[7].children[0].value = suma
            return true
        }
        function confirmarAnulacion() {
            
            var grid = document.getElementById("gvResultado");
            var cb = document.getElementById("cboMotivo");
            
            if ((document.getElementById("txtObservacion").value).length == 0){
                alert("Debe ingresar observación")
                return false 
            }
            
            if (grid.rows[grid.rows.length - 1].cells[7].children[0].value > 0) {
                var rpta = confirm("¿Desea solicitar la anulación de S./" + grid.rows[grid.rows.length - 1].cells[7].children[0].value + " por " + cb.options[cb.selectedIndex].innerHTML + " ?")
                if (rpta) {
                    return true
                } else {
                    return false
                }
            } else {
                alert("El Total a anular debe ser mayor a cero")
                return false
            }
        }

   </script>
    <style type="text/css">
        #form1
        {
            height: 352px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <p class="usatTitulo">
        <asp:Label ID="lblTitulo" runat="server" Text=""></asp:Label></p>
        <p><asp:Button ID="cmdCancelar" runat="server" Text="Cerrar" 
            SkinID="BotonSalir" ValidationGroup="Salir" 
            onclientclick="self.parent.tb_remove();" UseSubmitBehavior="False" /></p>
        <asp:GridView ID="gvResultado" runat="server" AutoGenerateColumns="False" 
            SkinID="skinGridViewLineas" ShowFooter="True" 
        DataKeyNames="codigo_Deu">
            <Columns>
                <asp:BoundField DataField="Servicio" HeaderText="Servicio" ReadOnly="True" 
                    SortExpression="Servicio" />
                <asp:BoundField DataField="codigo_Deu" HeaderText="Codigo Deuda" 
                    ReadOnly="True" SortExpression="codigo_Deu" Visible="False" >
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Documento" HeaderText="Documento" ReadOnly="True" 
                    SortExpression="Documento" Visible="False" >
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="Fecha_Operacion" HeaderText="Fecha Operacion" 
                    ReadOnly="True" SortExpression="Fecha_Operacion" >
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Cargos" HeaderText="Cargos" ReadOnly="True" 
                    SortExpression="Cargos" >
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="Abonos" HeaderText="Abonos" ReadOnly="True" 
                    SortExpression="Abonos" >
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="Saldo" HeaderText="Saldo" ReadOnly="True" 
                    SortExpression="Saldo" >
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="Trans" HeaderText="Transf." ReadOnly="True" 
                    SortExpression="Trans" Visible="False" >
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="observacion_Deu" HeaderText="Observacion" 
                    ReadOnly="True" SortExpression="observacion_Deu" >
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="Genera Mora" SortExpression="GeneraMora" 
                    Visible="False">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" 
                            Text='<%# iif(eval("generamora")=1,"Sí","No") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:BoundField DataField="FechaVenc" HeaderText="Fecha Venc." ReadOnly="True" 
                    SortExpression="FechaVenc" >
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="descripcion_Cco" HeaderText="Centro de Costos" 
                    ReadOnly="True" SortExpression="descripcion_Cco" Visible="False" >
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="Orden" HeaderText="Orden" ReadOnly="True" 
                    SortExpression="Orden" Visible="False" />
            
                <asp:TemplateField HeaderText="Anular (S/.)" SortExpression="Saldo">
                    <FooterTemplate>
                        <asp:TextBox ID="txtTotal" runat="server" ></asp:TextBox>
                    </FooterTemplate>
                    <ItemTemplate>
                        <asp:TextBox name="txtCantidad" ID="txtCantidad" runat="server" 
                            onKeyPress = "return validarDecimal(event, this)" onKeyUp="return sumar()"></asp:TextBox>
                     </ItemTemplate>
                </asp:TemplateField>
                
            </Columns>
        </asp:GridView>
        <table cellpadding="3px">
            <tr>
                <td>
                    <asp:Label ID="Label3" runat="server" Text="Motivo " Width="30px"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="cboMotivo" runat="server" 
                        SkinID="ComboObligatorio" Width="154px" Height="16px">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="Obs. " Width="25px"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtObservacion" runat="server" Width="281px" MaxLength="1000" 
                        Height="23px"></asp:TextBox>
                </td>
                <td>
                    <p><asp:Button ID="btnAnular" runat="server" Text="Sol. Anulación" 
                            Enabled="False" onClientClick = "return confirmarAnulacion()" 
                            SkinID="BotonSinTextoGuardar" Width="120px"/></p>
                </td>
            </tr>
        </table>
     <asp:Label ID="lblInformacion" runat="server" Font-Bold="True" 
        ForeColor="#FF9900"></asp:Label>
    <br />
     <asp:Label ID="lblMensaje" runat="server" Font-Bold="True" CssClass="rojo"></asp:Label>
     </form>
</body>
</html>