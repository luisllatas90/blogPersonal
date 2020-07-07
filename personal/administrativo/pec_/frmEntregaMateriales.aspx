<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmEntregaMateriales.aspx.vb"
    Inherits="personal_administrativo_pec2_frmEntregaMateriales" Theme="Acero" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Página sin título</title>
    <link rel="stylesheet" type="text/css" href="../../../private/estilo.css">
    

    <script type= "text/javascript" >
       function mostrar() {
            div = document.getElementById('participantes');
            div.style.display = '';
        }
        function ocultar() {
            div = document.getElementById('participantes');
            div.style.display = 'none';
        }
        function marcartodo() { 
           for (i=0;i<document.form1.elements.length;i++) 
                if(document.form1.elements[i].type == "checkbox") 
                document.form1.elements[i].checked=1 
        }

        function desmarcartodo() {
            for (i = 0; i < document.form1.elements.length; i++)
                if (document.form1.elements[i].type == "checkbox")
                document.form1.elements[i].checked = 0
        }

        function arraymarcados() { 
          var cadena =''
           for (i=0;i<document.form1.elements.length;i++) 
                if(document.form1.elements[i].type == "checkbox") 
                   if (document.form1.elements[i].checked==1)
                      cadena = cadena + '1'
                   else
                      cadena = cadena + '0' 
          return cadena             
        }

    </script> 
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <H3>ENTREGA DE MATERIALES</H3>
        1.-Seleccione Evento: <br />       
        <asp:DropDownList ID="DropDownList3" runat="server" Enabled="False" 
            Height="20px" Visible="False" Width="5px">
        </asp:DropDownList>
        <asp:DropDownList ID="DropDownList4" runat="server" Height="20px" Width="600px">
        </asp:DropDownList>
        &nbsp;<br />
        <div>
        <table style="width: 600px"><tr>
<td width="300px" bgcolor="#cccccc" align ="center" style="text-decoration :none;"><a href="#" onclick="mostrar();">ver lista de participantes</a></td>
<td width="300px" bgcolor="#cccccc" align ="center" style="text-decoration :none;"><a href="#" onclick="ocultar();">ocultar lista de participantes</a></td>
</tr>
</table>
</div>
<br />
        <div style="background: #eeeeff; height: 620px; width: 928px;">
            <br />
            <table cellpadding="0" cellspacing="0" style="width: 650px; height: 601px">
                <tr>
                    <td height="400px" width="200px" valign="top" rowspan="3" class="style1">
                        <asp:CheckBoxList ID="CheckBoxList1" runat="server" Height="32px" Width="230px" 
                            Visible="False" AutoPostBack="True"  >
                        </asp:CheckBoxList><br />
                        <div id="divchk">
                        <a href="#" onclick="marcartodo()" style="font-size:9px;">Marcar Todos</a><br />
                        <a href="#" onclick="desmarcartodo()" style="font-size:9px;">Desmarcar Todos</a>
                            <asp:Button ID="Button5" runat="server" style="height: 26px" Text="prueba" />
                        </div>
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:CNXBDUSAT %>" 
                            SelectCommand="EVE_ConsultarMaterialesEvento" 
                            SelectCommandType="StoredProcedure">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="DropDownList4" DefaultValue="0" Name="codigo_cco" 
                                    PropertyName="SelectedValue" Type="Int32" />
                                <asp:ControlParameter ControlID="Calendar1" DbType="Date" DefaultValue="" 
                                    Name="fecha" PropertyName="SelectedDate" />
                                <asp:Parameter DefaultValue="T" Name="modo" Type="String" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                        
                        <asp:SqlDataSource ID="SqlDataSource3" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:CNXBDUSAT %>" 
                            SelectCommand="EVE_ConsultarunInscrito" SelectCommandType="StoredProcedure">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="DropDownList4" Name="Codigo_Cco" 
                                    PropertyName="SelectedValue" Type="Int32" />
                                <asp:ControlParameter ControlID="TextBox2" Name="NroDoc" PropertyName="Text" 
                                    Type="String" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                        
                    </td>
                    <td valign="top" class="style3">
                        2.-Haga clic en el dia para ver materiales :<br />&nbsp;<asp:Calendar ID="Calendar1" 
                            runat="server" Height="180px" Width="200px" BackColor="White" 
                            BorderColor="#999999" CellPadding="4" 
                            DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" 
                            ForeColor="Black" style="text-align: center">
                            <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                            <SelectorStyle BackColor="#CCCCCC" />
                            <WeekendDayStyle BackColor="#FFFFCC" />
                            <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
                            <OtherMonthDayStyle ForeColor="#808080" />
                            <NextPrevStyle VerticalAlign="Bottom" />
                            <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
                            <TitleStyle BackColor="#999999" BorderColor="Black" 
                                Font-Bold="True" />
                        </asp:Calendar>
                        <div id="participantes" style="position: absolute; top: 125px; left: 45px; display:none ">
                            <br />
                        <a href="#" onclick="ocultar();"><h4>clic aqui para ocultar lista de participantes</h4></a>
                        </div>
                        <div  style="margin-top:20px; width: 197px">  Tipo doc :
                        <asp:DropDownList ID="DropDownList2" runat="server" Height="20px" Width="118px">
                            <asp:ListItem>DNI</asp:ListItem>
                            <asp:ListItem>PAS</asp:ListItem>
                            <asp:ListItem>CE.</asp:ListItem>
                        </asp:DropDownList>
                        </div>
                        <br />
                        <br />
                        Número :
                        <asp:TextBox ID="TextBox2" runat="server" CausesValidation="True" MaxLength="8"></asp:TextBox>
                       <br /> <asp:CustomValidator ID="CustomValidator1" runat="server" 
                            ClientValidationFunction="validaDNI" ControlToValidate="TextBox2">DNI no existe o no es valido</asp:CustomValidator>
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                            CellPadding="4" DataSourceID="SqlDataSource2" Font-Names="Arial" 
                            Font-Size="11px" ForeColor="#333333" GridLines="None" 
                            Width="673px">
                            <RowStyle BackColor="#EFF3FB" Height="16px" HorizontalAlign="Right" />
                            <Columns>
                                <asp:BoundField DataField="TipoDoc" HeaderText="TDoc" SortExpression="TipoDoc">
                                <ItemStyle Font-Size="7px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="NroDoc" HeaderText="NroDoc" SortExpression="NroDoc">
                                <ItemStyle Font-Size="8px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Participante" HeaderText="Participante" 
                                    ReadOnly="True" SortExpression="Participante">
                                <ItemStyle Font-Names="Arial" Font-Size="9px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CodUniversitario" HeaderText="Cod Uni " 
                                    ReadOnly="True" SortExpression="CodUniversitario">
                                <ItemStyle Font-Size="8px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Estado" HeaderText="Est" ReadOnly="True" 
                                    SortExpression="Estado">
                                <ItemStyle Font-Size="8px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="SaldoTotal" HeaderText="Saldo" ReadOnly="True" 
                                    SortExpression="SaldoTotal" />
                                <asp:BoundField DataField="Carrera" HeaderText="Carrera" ReadOnly="True" 
                                    SortExpression="Carrera">
                                <ItemStyle Font-Size="8px" />
                                </asp:BoundField>
                            </Columns>
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#2461BF" />
                            <AlternatingRowStyle BackColor="White" />
                        </asp:GridView>
                        <asp:GridView ID="gvParticipante" runat="server" AutoGenerateColumns="False" 
                            DataSourceID="SqlDataSource3" Font-Size="10px" Height="16px" Width="393px" 
                            BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" 
                            CellPadding="3" GridLines="Horizontal">
                            <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
                            <Columns>
                                <asp:BoundField DataField="Participante" HeaderText="Participante" 
                                    SortExpression="Participante" />
                                <asp:BoundField DataField="TipoDoc" HeaderText="TD" 
                                    SortExpression="TipoDoc" Visible="False" />
                                <asp:BoundField DataField="NroDoc" HeaderText="ND" SortExpression="NroDoc" />
                                <asp:BoundField DataField="SaldoTotal" HeaderText="Sld" 
                                    SortExpression="SaldoTotal" />
                                <asp:BoundField DataField="codigo_Pso" HeaderText="C_pso" 
                                    SortExpression="codigo_Pso" />
                            </Columns>
                            <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                            <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                            <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                            <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                            <AlternatingRowStyle BackColor="#F7F7F7" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td valign="top" class="style4">
                        Observaciones:<br />
                        <asp:TextBox ID="TextBox3" runat="server" Height="84px" TextMode="MultiLine" Width="417px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td height="121" valign="top" class="style2">
                        <asp:Button ID="Button1" runat="server" Height="26px" Text="Registrar" 
                            Width="82px" />
                         <!--<input id="btnDefAceptar" type="button" value="Ver Registrados" Height="26px" Width="115px" onclick="mostrardiv();"/>-->
                         <input id="Button4" type="button" value="Ver Participantes" onclick="mostrar();"/> 
                         <asp:Button ID="Button3" runat="server" Height="26px" Text="Cancelar" 
                            Width="96px" />
                              
                        <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:CNXBDUSAT %>" 
                            SelectCommand="EVE_ConsultarInscritos" SelectCommandType="StoredProcedure">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="DropDownList4" Name="Codigo_Cco" 
                                    PropertyName="SelectedValue" Type="Int32" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </td>
                </tr>
            </table>
        </div>
        <asp:Label ID="Label1" runat="server" Text="."></asp:Label><br />
        <br />
        
    </div>
    <div id="inscritos" 
        
        style ="position:absolute; top:16px; left:41px; height:auto; width: 689px; display: none;">
                        <p align="right"><strong><a href="javascript:cerrar();" style="font-family :'Arial'; font-size: 15px; color:Red; text-decoration:none;"> Clic aqui para ocultar el listado</a></strong></p>
                        </div>
    </form>
</body>
</html>
