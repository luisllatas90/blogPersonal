<%@ Page Language="VB" AutoEventWireup="false" CodeFile="firmojuramento.aspx.vb" Inherits="firmojuramento" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Consulta de Personal que no ha firmado juramento de fidelidad</title>
        <link rel="STYLESHEET" href="../private/estilo.css"/>
    <style type="text/css">
        .style1
        {
        }
        .style2
        {
            width: 651px;
        }
        .style3
        {
            width: 33px;
        }
    </style>
    <script type="text/javascript" language="javascript">

        function HabilitarEnvio(idcheck)
        {
            var arrChk = document.getElementsByTagName('input');
            var total=0
            
            if (arrChk.length>0){
                for (var i = 0 ; i < arrChk.length ; i++){
                    var chk = arrChk[i];
                    //verificar si es Check
                    if (chk.type === "checkbox" && chk.id!="dgwResultados_ctl01_chkHeader"){
                        if (chk.checked ==true){
                            total=total+1
                        }
                    }
                }
            }
            else{
	            if (idcheck.checked==true)
		            {total=1}
            }
            //Pintar Fila
		    if (idcheck.parentNode.parentNode.tagName=="TR"){
		        PintarFilaMarcada(idcheck.parentNode.parentNode,idcheck.checked)
		    }	
	        //Habilitar botón
            if (total==0){
                form1.cmdGuardar.disabled=true
            }
            else{
                form1.cmdGuardar.disabled=false
            }
         }
         
        function PintarFilaMarcada(obj,estado)
        {
            if (estado==true){
                obj.style.backgroundColor="#E6E6FA"//#395ACC
            }
            else{
                obj.style.backgroundColor="white"
            }
        }
    </script>
    
</head>
<body text="Consulta de Personal que no ha firmado juramento de fidelidad">
    <form id="form1" runat="server">
    <div>
    
        <table style="width:100%;">
            <tr>
                <td class="style1" colspan="3">
    
        <asp:Label ID="Label1" runat="server" 
                        Text="Consulta de Personal que no ha firmado juramento de fidelidad" 
                        CssClass="usatTitulousat"></asp:Label>
    
                </td>
            </tr>
            <tr>
                <td class="style3">
                    &nbsp;</td>
                <td class="style2">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style3">
                    &nbsp;</td>
                <td class="style2">
                    Resumen:</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style3">
                    &nbsp;</td>
                <td class="style2">
                    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
                        BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" 
                        CellPadding="3" DataSourceID="SqlDataSource2" Width="179px">
                        <FooterStyle BackColor="White" ForeColor="#000066" />
                        <RowStyle ForeColor="#000066" />
                        <Columns>
                            <asp:BoundField DataField="Tipo" HeaderText="Tipo" SortExpression="Tipo" />
                            <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" 
                                SortExpression="Cantidad" />
                        </Columns>
                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                    </asp:GridView>
                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:CNXBDUSAT %>" 
                        SelectCommand="HOJ_ConsultarFirmoJuramentoFidelidad" 
                        SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:Parameter DefaultValue="RE" Name="tipo" Type="String" />
                            <asp:Parameter DefaultValue="X" Name="param1" Type="String" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style3">
                    &nbsp;</td>
                <td class="style2">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style3">
                    &nbsp;</td>
                <td class="style2">
                    Detalle:<asp:Button ID="cmdGuardar" runat="server" Text="Guardar" 
                        Enabled="False" />
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style3">
                    &nbsp;</td>
                <td class="style2">
                    <asp:GridView ID="dgwResultados" runat="server" DataSourceID="SqlDataSource1" 
                        Width="643px" CellPadding="4" ForeColor="#333333" GridLines="None" 
                        DataKeyNames="codigo_per" AutoGenerateColumns="False">
                        <Columns>
                            <asp:TemplateField>
                           <ItemTemplate>
                                <asp:CheckBox ID="chkElegir" runat="server" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
     
     
                            </asp:TemplateField>
                            <asp:BoundField DataField="Paterno" HeaderText="Ap. Paterno">
                                <ItemStyle Width="150px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Materno" HeaderText="Ap. Materno">
                                <ItemStyle Width="150px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Nombres" HeaderText="Nombres">
                                <ItemStyle Width="170px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Tipo" HeaderText="Tipo">
                                <ItemStyle Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Dedicación" HeaderText="Dedicación">
                                <ItemStyle Width="170px" />
                            </asp:BoundField>
                        </Columns>
                        <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                        <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                        <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                        <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:CNXBDUSAT %>" 
                        SelectCommand="HOJ_ConsultarFirmoJuramentoFidelidad" 
                        SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:Parameter DefaultValue="TO" Name="tipo" Type="String" />
                            <asp:Parameter DefaultValue="x" Name="param1" Type="String" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
