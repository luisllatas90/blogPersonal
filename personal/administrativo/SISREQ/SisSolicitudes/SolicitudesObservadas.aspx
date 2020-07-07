<%@ Page Language="VB" EnableEventValidation="false" AutoEventWireup="false" CodeFile="SolicitudesObservadas.aspx.vb" Inherits="SisSolicitudes_SolicitudesObservadas" %>

<html xmlns="http://www.w3.org/1999/xhtml">  
<head runat="server">
    <title>Página sin título</title>    
    <link href="../private/estiloweb.css" rel="stylesheet" type="text/css" /> 
    <link href="../private/estilo.css" rel="stylesheet" type="text/css" />
    <link href="../../../../private/estilo.css" rel="stylesheet" type="text/css" />
    <script src="../../../../private/funciones.js" type ="text/javascript" language ="javascript"></script>
    <script type="text/javascript" language="javascript">
    function HabilitarBoton(tipo,fila, numero_sol)
  	{
  	    document.form1.txtelegido.value=fila.id
  	    
    	if (document.form1.cmdModificar!=undefined){
            document.form1.cmdModificar.disabled=false     
    	}
    	if (document.form1.cmdEliminar!=undefined){
            document.form1.cmdEliminar.disabled=false     
    	}
    	SeleccionarFila();
    	ResaltarPestana('0','','')
    	//alert(numero_sol)
    	//AbrirPestana('DatosDeSolicitud.aspx',numero_sol);
	}
	
    function AbrirPestana(pagina,numero_sol,tipo)
    {
	    //document.all.mensajedetalle.style.display="none"
	    fradetalle.location.href=pagina + "?" + tipo + "=" + numero_sol  //&codigo_per=<%=request.querystring("id")%>"   
    }
    function AbrirLink(pagina)
    {   fradetalle.location.href=pagina 
    }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <table style="height: 620px;" cellpadding="0" cellspacing="0" 
            width="100%" align="center">
            <tr id="trLista">
                <td style="width:100%; height: 9px;" valign="top" align="right"><table style="width: 100%" cellpadding="0" cellspacing="0">
                    <tr >
                        <td 
                            valign="top" align="right" bgcolor="#FFFFD9" height="18px">
                             <asp:Label ID="LblMensaje" runat="server">Para ver el detalle de una solicitud 
                             observada debe dar clic en una solicitud de la lista</asp:Label>
                             <asp:Image ID="Image1" runat="server" 
                                 ImageUrl="../../../../images/atencion.gif" />
                        </td>
                    </tr>
                    <tr >
                        <td
                            style="border-bottom:black 1px solid; height: 220px;
                            background-color: white; border-right: black 1px solid; border-left: black 1px solid; border-top: black 1px solid; border-color: #808080;" 
                            valign="top" align="center">
                             <table style="width:100%; height:100%;" cellpadding="1" cellspacing="0">
                             <tr class="TituloTabla" style="height:15px; height:3%;" >
                             <td style="width:10%;height:15px" align="center">Nro</td>
                             <td style="width:10%;height:15px" align="center">Fecha</td>
                             <td style="width:10%;height:15px" align="center">Cod. Universitario</td>
                             <td style="width:30%;height:15px" align="center">Estudiante</td>
                             <td style="width:25%;height:15px" align="center">Carrera Profesional</td>
                             <td style="width:15%;height:15px" align="center">Estado actual</td>
                             </tr>
                             <tr style="height:97%"><td colspan="6">
                             <div id="listadiv" 
                                     style=" height:100%; width:100%; top: 0; left: 0; vertical-align: top;" >
                    <asp:GridView ID="GvSolicitudes" runat="server" AutoGenerateColumns="False" 
                    DataKeyNames="numero_sol,codigouniver_alu" Width="100%" ShowHeader="False" 
                                     GridLines="Horizontal">
                        <RowStyle Height="20px" />
                        <Columns>
                            <asp:BoundField DataField="codigo_sol" HeaderText="codigo_sol" 
                            InsertVisible="False" ReadOnly="True" SortExpression="codigo_sol" 
                            Visible="False" />
                            <asp:BoundField DataField="codigo_alu" HeaderText="codigo_alu" 
                            InsertVisible="False" ReadOnly="True" SortExpression="codigo_alu" 
                            Visible="False" />
                            <asp:BoundField DataField="numero_sol" HeaderText="Nro. de Sol." 
                                SortExpression="numero_sol">
                                <ItemStyle Width="10%" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Fecha_Sol" HeaderText="Fecha de Sol." 
                                ReadOnly="True" SortExpression="Fecha_Sol" 
                                DataFormatString="{0:dd-MM-yyyy}">
                                <ItemStyle Width="10%" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="codigouniver_alu" HeaderText="codigouniver_alu" 
                            SortExpression="codigouniver_alu"  >
                                <ItemStyle Width="10%" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="alumno" HeaderText="Estudiante" ReadOnly="True" 
                            SortExpression="alumno" >
                                <ItemStyle Width="30%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="abreviatura_cpf" HeaderText="Carrera Profesional" 
                            SortExpression="abreviatura_cpf" >
                                <ItemStyle Width="25%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="responsable_sol" HeaderText="Responsable" 
                            SortExpression="responsable_sol" Visible="False" >
                            </asp:BoundField>
                            <asp:BoundField DataField="Estado" HeaderText="Estado" ReadOnly="True" 
                            SortExpression="Estado" >
                                <ItemStyle Width="10%" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:CommandField SelectText="  " ShowSelectButton="True" 
                                SelectImageUrl="~/images/Okey.gif" >
                                <ItemStyle Width="5%" />
                            </asp:CommandField>
                        </Columns>
                        <EmptyDataTemplate>
                            <asp:Label ID="Label1" runat="server" ForeColor="Red" 
                            Text="No se encontraron registros para este estudiante"></asp:Label>
                        </EmptyDataTemplate>
                        <SelectedRowStyle BackColor="#E1F1FB" />
                        <HeaderStyle CssClass="TituloTabla" Height="18px" />
                    </asp:GridView>
                             </div>
                              </tr>
                            </table>      
                        </td>
                    </tr>
                    </table>
                    &nbsp; </td>
            </tr>
            <tr >
                <td  style="width:auto; " height="120">
                <table style="width:100%;" id="TbAlumno">
                    <tr>
                        <td align="center" width="15%">
                <asp:Image ID="ImgFoto" runat="server" BorderColor="#666666" BorderWidth="1px" 
                    Height="95px" Width="75px" Visible="False" ImageAlign="Middle" />
                        </td>
                        <td id="idEstudiante" align="left" class="fondoblanco" >
                <asp:DetailsView ID="DvEstudiante" runat="server" AutoGenerateRows="False"
                    DataSourceID="SqlDataSource4" Height="50px" Width="100%" GridLines="None" 
                    BorderWidth="0px" >
                    <Fields>
                        <asp:BoundField DataField="codigouniver_alu" HeaderText="Código Universitario:" 
                            SortExpression="codigouniver_alu">
                            <HeaderStyle Width="15%" />
                            <ItemStyle Width="75%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="alumno" HeaderText="Apellidos y Nombres:" 
                            ReadOnly="True" SortExpression="alumno" />
                        <asp:BoundField DataField="sexo_alu" HeaderText="Sexo:" 
                            SortExpression="sexo_alu" />
                        <asp:BoundField DataField="nombre_cpf" HeaderText="Carrera Profesional:" 
                            SortExpression="nombre_cpf" />
                        <asp:BoundField DataField="descripcion_pes" HeaderText="Plan de Estudios:" 
                            SortExpression="descripcion_pes" />
                        <asp:BoundField DataField="estadoactual_alu" HeaderText="Estado Actual:" 
                            SortExpression="estadoactual_alu" />
                    </Fields>
                </asp:DetailsView>
                        </td>
                        <td >
                            &nbsp;</td>
                    </tr>
                </table>
                </td>
            </tr>
            <tr id="trDetalle">
                <td>
                
                <table id="TbPestanas" cellspacing="0" cellpadding="0" style="border-collapse: collapse; height: 100%;" bordercolor="#111111" width="100%">
      <tr>
        <td class="pestanaresaltada_1" id="tab" align="center" width="20%" onClick="ResaltarPestana_1('0','','');AbrirPestana('DatosDeSolicitud.aspx','<% response.write(Me.txtelegido.Value) %>', 'codigo_sol')" style="height: 32px" >
            De la solicitud<td width="1%" class="bordeinf" style="height: 32px" >&nbsp;</td>
        <td class="pestanabloqueada" id="tab" align="center" width="20%" 
              onClick="ResaltarPestana_1('1','','');AbrirPestana('ObservacionesDeSolicitud.aspx','<% response.write(me.txtelegido.value) %>', 'codigo_sol')" 
              style="height: 32px">
            Observaciones</td>
        <td width="1%" class="bordeinf" style="height: 32px" >&nbsp;</td>
		<td class="pestanabloqueada" id="tab" align="center" width="20%" 
              onClick="ResaltarPestana_1('2','','');AbrirLink('EditarEvaluacion.aspx?codigo_sol=<% response.write(Me.txtelegido.Value) %>&id=<% response.write(request.querystring("id")) %>')" 
              style="height: 32px">
            Editar Evaluación</td>
        <td width="45%" align="right" class="bordeinf">
        <asp:Button ID="CmdVerEvaluacion" runat="server" CssClass="boton" Text="Ver Evaluación" Width="100px" />&nbsp;
        &nbsp;</td>
        <td width="45%" class="bordeinf" style="height: 32px; width: 22%;" align="right">
        <img border="0" src="../../../../images/maximiza.gif" style="cursor:hand" 
                ALT="Maximizar ventana" onClick="Maximizar(this,'../../../../','100%','65%')"></td>
      </tr>
      <tr>
        <td width="100%" valign="top" colspan="7" class="pestanarevez" 
              style="height: 100%">
              
              
            <iframe id="fradetalle" height="100%" width="100%" border="0" 
                
                src="DatosDeSolicitud.aspx?codigo_sol=<% response.write(me.txtelegido.Value) %>" frameborder="0" 
                scrolling="yes" style="height: 90%; width: 100%;" name="fradetalle"> </iframe>
            </td>
      </tr>
    </table>
                    </td>
            </tr>
        </table>
    
    <asp:HiddenField  ID="txtelegido" runat="server" />
    <asp:HiddenField ID="hdcodigo_per" runat="server" />    
                <asp:SqlDataSource ID="SqlDataSource4" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:CNXBDUSAT %>" 
                    SelectCommand="SOL_ConsultarListaSolicitudes" 
                    SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="3" Name="tipo" Type="Int32" />
                        <asp:ControlParameter ControlID="txtelegido" DefaultValue="" Name="param1" 
                            PropertyName="Value" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>

    <asp:HiddenField ID="HddAlumno" runat="server" />

                <asp:HiddenField ID="HddNumero_sol" runat="server" />

    </form>
</body>
</html>
