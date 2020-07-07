<%@ Page Language="VB" AutoEventWireup="false" CodeFile="lstDirectorioAlumnos.aspx.vb" Inherits="librerianet_academico_lstDirectorioAlumnos" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Revisión de Directorio de estudiantes</title>
    <link href="../../private/estilo.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="JavaScript" src="../../private/funciones.js"></script>
    <script type="text/javascript" language="javascript">
        function HabilitarEnvio(idcheck)
        {
            var arrChk = document.getElementsByTagName('input');
            var total=0
            
            if (arrChk.length>0){
                for (var i = 0 ; i < arrChk.length ; i++){
                    var chk = arrChk[i];
                    //verificar si es Check
                    if (chk.type === "checkbox" && chk.id!="GridView1_ctl01_chkHeader"){
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
                form1.cmdGuardar0.disabled=true
                form1.cmdGuardar1.disabled=true
            }
            else{
                form1.cmdGuardar0.disabled=false
                form1.cmdGuardar1.disabled=false
            }
         }
         
        function PintarFilaMarcada(obj,estado)
        {
            if (estado==true){
                obj.style.backgroundColor="#EBEBEB"//#395ACC
            }
            else{
                obj.style.backgroundColor="white"
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <p class="usattitulo">Revisión de Directorio de estudiantes</p>
    <table cellpadding="0" cellspacing="0" style="width: 100%; border:1px solid #96ACE7" border="0">
        <tr bgcolor="#91b4de" style="height:30px">
            <td style="width:90%">
                &nbsp;&nbsp;&nbsp; Letra: <asp:DropDownList ID="dpLetra" runat="server">
                </asp:DropDownList>
                &nbsp;&nbsp;
                Ciclo de Matrícula: <asp:DropDownList ID="dpCiclo" runat="server">
                </asp:DropDownList>
                &nbsp;&nbsp; Estado de revisión: <asp:DropDownList ID="dpRevision" runat="server">
                    <asp:ListItem Value="0">No Revisados</asp:ListItem>                
                    <asp:ListItem Value="1">Revisados</asp:ListItem>
                </asp:DropDownList>
                &nbsp;
                <asp:Button ID="cmdBuscar" runat="server" Text="Buscar" />
            &nbsp;&nbsp;
            </td>
            <td style="width:10%" align="right">
    <asp:Button ID="cmdGuardar0" runat="server" Font-Bold="False" Font-Overline="False" 
        Font-Underline="False" 
        Text="Guardar" />
        &nbsp;&nbsp;
            </td>
        </tr>
        <tr>
            <td style="margin-left: 80px" valign="top" colspan="2">
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
        CellPadding="2" GridLines="Horizontal" BorderStyle="None" DataKeyNames="codigo_alu">
        <RowStyle BorderColor="#C2CFF1" BorderStyle="Solid" BorderWidth="1px" />
        <EmptyDataRowStyle CssClass="sugerencia" />
        <Columns>
            <asp:BoundField HeaderText="#">
                <ItemStyle Width="5%" />
            </asp:BoundField>
            <asp:BoundField DataField="codigouniver_alu" HeaderText="Código">
                <ItemStyle Width="10%" />
            </asp:BoundField>
            <asp:BoundField DataField="alumno" HeaderText="Estudiante">
                <ItemStyle Width="20%" Font-Size="7pt" />
            </asp:BoundField>
            <asp:BoundField DataField="abreviatura_cpf" HeaderText="Escuela Profesional">
                <ItemStyle Width="10%" Font-Size="7pt" />
            </asp:BoundField>
            <asp:BoundField DataField="cicloIng_alu" HeaderText="Ciclo Ing.">
                <ItemStyle Width="10%" />
            </asp:BoundField>
            <asp:BoundField DataField="Domicilio" HeaderText="Dirección" >
                <ItemStyle Width="50%" Font-Size="7pt" />
            </asp:BoundField>
            <asp:BoundField DataField="telefonoCasa_Dal" HeaderText="Teléfono" >
                <ItemStyle Width="5%" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="Revisar">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" 
                        Text='<%# Bind("revisiondatos_alu") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                                <asp:CheckBox ID="chkElegir" runat="server" 
                                    Visible='<%# iif(IsDBNull(eval("revisiondatos_alu"))=1,false,true) %>' />
                            </ItemTemplate>
                <ItemStyle Width="10%" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Datos">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Button ID="cmdVer" runat="server" Text="Ver.." />
                </ItemTemplate>
                <ItemStyle Width="5%" />
            </asp:TemplateField>
        </Columns>
        <EmptyDataTemplate>
            No se encontrarios estudiantes según los criterios seleccionados
        </EmptyDataTemplate>
        <HeaderStyle BackColor="#e8eef7" ForeColor="#3366CC" BorderColor="#99BAE2" 
            BorderStyle="Solid" BorderWidth="1px" />
    </asp:GridView>
    
            </td>
        </tr>
        <tr bgcolor="#91b4de" style="height:30px">
            <td align="right" colspan="2">
    <asp:Button ID="cmdGuardar1" runat="server" Font-Bold="False" Font-Overline="False" 
        Font-Underline="False" 
        Text="Guardar" />
            &nbsp;&nbsp;
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
