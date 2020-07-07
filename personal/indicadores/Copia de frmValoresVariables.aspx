<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmValoresVariables.aspx.vb" Inherits="Indicadores_Formularios_frmValoresVariables" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
       <link href="../css/estilo.css" rel="stylesheet" type="text/css" media="screen" />
       <script type="text/javascript" language="JavaScript" src="../../private/funciones.js"></script>
        
       
        <script type="text/javascript" language="javascript">
        function PintarFilaElegida(obj) {
            if (obj.style.backgroundColor == "white") {
                obj.style.backgroundColor = "#E6E6FA"//#395ACC
            }
            else {
                obj.style.backgroundColor = "white"
            }
        }
        function cmdNuevo_onclick() {

        }
    </script>
       
    <style type="text/css">
        .usatFormatoCampo
        {
        	float:left; 
        	font-weight:bold;
        	padding-top:10px;        	
        	padding-left:30px;
        	color:#27333c;
        	font-family: Arial;
        	height:20px;
        	width:200px;        	
        }
        
        .usatFormatoCampoAncho
        {
        	float:left; 
        	font-weight:bold;
        	padding-top:10px;        	
        	padding-left:30px;
        	color:#27333c;
        	font-family: Arial;
        	height:20px;
        	width:75%;
        	/*border:1px solid red;*/
        }
        
        .usatTituloAzul {
	        font-family: Arial;
	        font-size: 12pt;
	        font-weight: bold;
	        color: #3063c5;
            height: 19px;
            width: 831px;
            height: 40px;
        }
        
        .usatDescripcionTitulo  
        {
        	font-family: Arial;
	        font-size: 10pt;	        
	        color: #27333c;
        }                        
        .usatPanelConsulta
        {        
            border: 1px solid #C0C0C0;            
	        max-height:1000px;	        
	        -moz-border-radius: 15px; 
	        /*padding-top:10px;*/	
	        margin-top:15px;  
	    }
              
        .cuadroMensajes
        {
        	/*background-color:Red;*/
        	height:30px;
        	padding-top:10px;        	
        }
        
         #lblConsulta
        {
        	position:absolute;
        	top: 115px;
        	left: 20px;
        	font-size:10pt;
        	color:#3063c5;
        	font-weight:bold;
        	background-color:White;
        	width:220px;        
        	text-align:center;
        }
                
        .GridviewDiv {font-size: 100%; font-family: 'Lucida Grande', 'Lucida Sans Unicode', Verdana, Arial, Helevetica, sans-serif; color: #303933; width:100%; margin-top:15px}
        Table.Gridview{border:solid 1px #df5015;}
        .GridviewTable{border:none}
        .GridviewTable td{margin-top:0;padding: 0; vertical-align:middle }
        .GridviewTable tr{color: White; background-color: #6B696B; height: 30px; text-align:center}
        .Gridview th{color:#FFFFFF;border-right-color:#abb079;border-bottom-color:#abb079;padding:0.5em 0.5em 0.5em 0.5em;text-align:center} 
        .Gridview td{border-bottom-color:#f0f2da;border-right-color:#f0f2da;padding:0.5em 0.5em 0.5em 0.5em;}
        .Gridview tr{color: Black; background-color: White; text-align:left}
        :link,:visited { color: #DF4F13; text-decoration:none }       
    </style>
      
</head>
<body>
    <form id="form1" runat="server">
    <div class="usatTituloAzul">Consultar y&nbsp; Registrar Valores de Variables<asp:Label 
            ID="lblCod" runat="server" Text="Label" Visible="False"></asp:Label>
    </div>
    <div class="usatDescripcionTitulo">Consulta y registra el valor de las variables.</div>
    <div class="cuadroMensajes"><asp:Label ID="lblMensaje" runat="server" Visible="False"></asp:Label>
            <asp:Label ID="lblCodigo_per" runat="server" Font-Bold="True" 
            ForeColor="Blue" Visible="False"></asp:Label>
            <div style="clear:both; height:5px;"></div>
        </div>
     
<%--    <div class="usatPanel">
        <asp:Label ID="lblSubtitulo" runat="server" Text="Seleccione la variable"></asp:Label>
        <div style="margin-top:10px;">
    
    <div style="height:15px;"></div>             
    
            <div class="usatFormatoCampo">
            <asp:Label ID="lblVariable" runat="server" Text="Variable"></asp:Label>
            </div>
            <div class="usatFormatoCampoAncho">
                <asp:DropDownList ID="ddlVariable" runat="server" Width="80%" 
                    AutoPostBack="True">
                </asp:DropDownList>                
                <asp:CompareValidator ID="CompareValidator1" runat="server" 
                    ErrorMessage="(*)  Debe seleccionar una Variable" 
                    ControlToValidate="ddlVariable" Display="Dynamic" Operator="NotEqual" 
                    ValidationGroup="grupo1" ValueToCompare="0"></asp:CompareValidator> 
                <asp:Label ID="lblPeriodicidad" runat="server" Text=""></asp:Label>
                <asp:Label ID="lblcodigo_peri" runat="server" Text="" Visible="false"></asp:Label>
            </div>
            <div style="clear:both; height:5px;"></div>        
        </div>           
    </div> 
    
    <div style="height:15px"></div>  --%> 
    
    <%--<div class="usatPanel2">
        <asp:Label ID="lblPanel2" runat="server" Text="Seleccione el Periodo"></asp:Label>
        <div style="margin-top:10px;">
            <div class="usatFormatoCampo">
            <asp:Label ID="lbldesde" runat="server" Text="Desde"></asp:Label>
            </div>
            <div class="usatFormatoCampoAncho">
                <asp:DropDownList ID="ddlDesde" runat="server" Width="80%" 
                    AutoPostBack="True">
                </asp:DropDownList>                
                <asp:CompareValidator ID="CompareValidator2" runat="server" 
                    ErrorMessage="(*)  Debe seleccionar una Variable" 
                    ControlToValidate="ddlDesde" Display="Dynamic" Operator="NotEqual" 
                    ValidationGroup="grupo1" ValueToCompare="0"></asp:CompareValidator> 
                <asp:Label ID="Label3" runat="server" Text=""></asp:Label>
            </div>            
            <div style="clear:both; height:5px;"></div>        
        </div>    
        <div>
            <div class="usatFormatoCampo">
            <asp:Label ID="Label4" runat="server" Text="Hasta"></asp:Label>
            </div>
            <div class="usatFormatoCampoAncho">
                <asp:DropDownList ID="ddlHasta" runat="server" Width="80%" 
                    AutoPostBack="True" Enabled="False">
                </asp:DropDownList>                
                <asp:CompareValidator ID="CompareValidator3" runat="server" 
                    ErrorMessage="(*)  Debe seleccionar una Variable" 
                    ControlToValidate="ddlHasta" Display="Dynamic" Operator="NotEqual" 
                    ValidationGroup="grupo1" ValueToCompare="0"></asp:CompareValidator> 
                <asp:Label ID="Label5" runat="server" Text=""></asp:Label>
            </div>            
            <div style="clear:both; height:5px;"></div>        
        </div>                  
    </div> --%>
    
    <asp:ScriptManager ID="ScriptManager2" runat="server"/>
    
    <div class="usatPanelConsulta">
        <asp:Label ID="lblConsulta" runat="server" 
            Text="Consulta de Valores de Variables"></asp:Label>

        <%--<asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <asp:Label ID="lbl" runat="server" Text="Label"></asp:Label>
        </ContentTemplate>        
        </asp:UpdatePanel>
        
        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
        <ContentTemplate>        
            <asp:Label ID="lbl2" runat="server" Text="Label2"></asp:Label>
         </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="cboSubvariable" 
                    EventName="SelectedIndexChanged" />
            </Triggers>
        </asp:UpdatePanel>
        
        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
        <ContentTemplate>
            <asp:Label ID="lbl3" runat="server" Text="Label"></asp:Label>
        </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="cboDimension" 
                    EventName="SelectedIndexChanged" />
            </Triggers>
        </asp:UpdatePanel>
        
        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
        <ContentTemplate>
        <asp:Label ID="lbl4" runat="server" Text="Label"></asp:Label>
        </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="cboSubdimension" 
                    EventName="SelectedIndexChanged" />
            </Triggers>
        </asp:UpdatePanel>
        
        <asp:UpdatePanel ID="UpdatePanel7" runat="server">
        <ContentTemplate>
        <asp:Label ID="lbl5" runat="server" Text="Label"></asp:Label>
        </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="cboPeriodo" 
                    EventName="SelectedIndexChanged" />
            </Triggers>
        </asp:UpdatePanel>--%>
        
              
        <div class="GridviewDiv">
             <div style="width: 100%;">             
                 <%--<table cellpadding="3" cellspacing="0" style="width: 100%; border:1px solid #96ACE7" border="0">                 --%>
                  <asp:UpdatePanel runat="server" id="gvup">
    <ContentTemplate>
    
        <div class="GridviewDiv">
             <div style="width: 100%;">
                 <%--<table cellpadding="3" cellspacing="0" style="width: 100%; border:1px solid #96ACE7" border="0">                 --%>
                 <table cellpadding="3" cellspacing="0"  class="" width="100%">                
                            <tr >
                                 <td align="left">
                                     <asp:Label ID="Label1" runat="server" Text="Seleccione una Variable  :"></asp:Label>
                                    
                                 </td>
                                 <td style="width: 95%;" colspan="5">
                                    <asp:DropDownList ID="cboVariable" runat="server" AutoPostBack="True" 
                                         Width="100%" BackColor="#CCFFFF" Font-Size="Smaller">                                      
                                    </asp:DropDownList>                        
                                </td>
                            </tr>
                        
                            <tr>
                                    <td align="left">
                                        <asp:Label ID="Label2" runat="server" Text="Seleccione una Subvariable  :"></asp:Label>
                                    </td>
                                    <td style="width: 95%;" colspan="5">
                                      <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>                                    
                                            <asp:DropDownList id="cboSubvariable" runat="server" AutoPostBack="True" 
                                                Width="100%" BackColor="#CCFFFF" Font-Size="Smaller">
                                            <asp:ListItem Text="Todos" Value="%"/>
                                            </asp:DropDownList>  
                                            
                                        </ContentTemplate>
                                            
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="cboVariable" 
                                                    EventName="SelectedIndexChanged" />
                                            </Triggers>
                                            
                                        </asp:UpdatePanel>                                                                          
                                    </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <asp:Label ID="Label3" runat="server" Text="Seleccione una Dimensión    :"></asp:Label>
                                </td>
                                 <td style="width: 95%;" colspan="5">
                                        <asp:UpdatePanel ID="upDimension" runat="server" >
                                            <ContentTemplate>
                                                <asp:DropDownList id="cboDimension" runat="server" AutoPostBack="true" 
                                                    Width="100%" BackColor="#CCFFFF" Font-Size="Smaller">
                                                <asp:ListItem Text="Todos" Value="%"/>
                                                </asp:DropDownList>                                        
                                            </ContentTemplate>        
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="cboSubvariable" 
                                                        EventName="SelectedIndexChanged" />
                                                </Triggers>
                                       </asp:UpdatePanel>
                                 </td>
                            
                            </tr>
                            
                            <tr>
                                <td align="left">
                                    <asp:Label ID="Label4" runat="server" Text="Selecione una Subdimensión  :"></asp:Label>
                                </td>
                                 <td style="width: 95%;" colspan="5">
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="cboSubdimension" runat="server" AutoPostBack="true" 
                                                Width="100%" BackColor="#CCFFFF" Font-Size="Smaller">
                                            <asp:ListItem Text="Todos" Value="%"/>
                                        </asp:DropDownList>                                        
                                        </ContentTemplate>
                                             <Triggers>
                                                 <asp:AsyncPostBackTrigger ControlID="cboDimension" 
                                                     EventName="SelectedIndexChanged" />
                                             </Triggers>
                                        </asp:UpdatePanel>
                                </td>
                            </tr>
                            
                            <tr>
                                <td align="left">
                                    <asp:Label ID="Label5" runat="server" Text="Seleccione un Periodo  :"></asp:Label>
                                </td>
                                <td style="width: 95%;" colspan="5">
                                    <asp:UpdatePanel ID="upPeriodo" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="cboPeriodo" runat="server" AutoPostBack="true" 
                                                Width="100%" BackColor="#FFFFCC" Font-Size="Smaller">
                                        </asp:DropDownList>        
                                        </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="cboVariable" 
                                                    EventName="SelectedIndexChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                </td>
                            </tr>
                            
                            <tr >
                            <td style="width: 20%; height:2px">
                            
                            </td>
                            <td style="width: 20%; height:2px" >
                            
                            </td>
                            <td style="width: 20%; height:2px">
                            
                            </td>
                            <td style="width: 15%; height:2px">
                            
                            </td>
                            <td style="width: 5%; height:2px">
                            
                            </td>
                             <td style="width: 10%; height:2px">
                            
                            </td>            
                            </tr>
                            
                         <tr>
                            <td colspan="6" style="text-align:center; font-weight:bold; ">[ LISTADO DE VALORES 
                                DE VARIABLE ]</td>
                        </tr>
                        <tr>
                           <td style="text-align:center" align="center" colspan="6">
                           <asp:GridView ID="gvValores" runat="server" AllowPaging="True"
                           DataSourceID="odsValores" AutoGenerateColumns="False" 
                                    
                                   
                                   
                                   DataKeyNames="Codigo,codigo_var,codigo_aux,codigo_dim,codigo_sub,importado,sumatoria_var,DetalleNivel,UltimoNivel,desc_niv2,existebd_var" CellPadding="3" 
                                    Width="100%" PageSize="20" Font-Size="XX-Small" BackColor="White" 
                                   BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px">
                                    <RowStyle ForeColor="#000066" />
                                    <Columns>                                                                                
                                        <asp:BoundField DataField="Codigo" HeaderText="Codigo" >                                                                                        
                                        </asp:BoundField>

                                        <asp:BoundField DataField="Variable" HeaderText="Variable" 
                                            ItemStyle-Width="30%"  >                                                                                                                                                                                                                                            
                                            <ItemStyle Width="30%" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Subvariable" HeaderText="Subvariable" 
                                            ItemStyle-Width="20%" >                                        
                                           <ItemStyle Width="20%" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Dimension" HeaderText="Dimension" 
                                            ItemStyle-Width="20%" >                                        
                                            <ItemStyle Width="20%" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Subdimension" HeaderText="Subdimension" 
                                            ItemStyle-Width="20%" >                                        
                                          <ItemStyle Width="20%" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Periodo" HeaderText="Periodo" 
                                            ItemStyle-Width="5%">                                        
                                            <ItemStyle Width="5%" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                      
                                        <asp:BoundField DataField="importado" HeaderText="importado" Visible="False" >
                                    
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                    
                                       <asp:TemplateField HeaderText="Valor" ItemStyle-Width="10%" SortExpression="Valor">                                                 
                                            <EditItemTemplate>   
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Debe ingresar el valor de la Variable."
                                                ControlToValidate="txtEditValor" Display="Dynamic">*</asp:RequiredFieldValidator>                                                         
                                                 <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" 
                                                    ControlToValidate="txtEditValor" Display="Dynamic" ErrorMessage="El valor debe ser mayor o igual a cero y no puede incluir el símbolo actual." ValidationExpression="^\d+(\.\d{1,2})?$" 
                                                    SetFocusOnError="true" EnableTheming="True">*</asp:RegularExpressionValidator>
                                                <asp:TextBox ID="txtEditValor" runat="server" Width="70%" Text='<%#Bind("Valor", "{0:N}") %>'></asp:TextBox> 
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblValor" runat="server" Text='<%# Bind("Valor") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="10%" HorizontalAlign="Center" />
                                        </asp:TemplateField>                                        
                                        <asp:BoundField DataField="codigo_var" HeaderText="codigo_var" 
                                            Visible="False" />
                                        <asp:BoundField DataField="codigo_aux" HeaderText="codigo_aux" 
                                            Visible="False" />
                                        <asp:BoundField DataField="codigo_dim" HeaderText="codigo_dim" 
                                            Visible="False" />
                                        <asp:BoundField DataField="codigo_sub" HeaderText="codigo_sub" 
                                            Visible="False" />
                                        <asp:BoundField DataField="codigoperiodo" HeaderText="codigoperiodo" 
                                            Visible="False" >
                                                                                                                  
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                                                                                                  
                                        <asp:CommandField ButtonType="Image" EditImageUrl="../images/editar.gif" 
                                            ShowEditButton="True" CancelImageUrl="../images/cerrar.gif" 
                                            UpdateImageUrl="../images/guardar.gif"/>
                                    
                                        <asp:BoundField DataField="sumatoria_var" HeaderText="sumatoria_var" 
                                            Visible="False" />
                                    
                                        <asp:BoundField DataField="DetalleNivel" HeaderText="DetalleNivel" 
                                            Visible="False" />
                                        <asp:BoundField DataField="UltimoNivel" HeaderText="UltimoNivel" 
                                            Visible="False" />
                                    
                                        <asp:BoundField DataField="desc_niv2" HeaderText="desc_niv2" Visible="False" />
                                        <asp:BoundField DataField="existebd_var" HeaderText="existebd_var" 
                                            Visible="False" />
                                    
                                    </Columns>
                                    <FooterStyle BackColor="White" ForeColor="#000066" />
                                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                    <HeaderStyle BackColor="#284775" Font-Bold="True" ForeColor="White" />
                                </asp:GridView>  
                                
                                                                                           
                          </td>
                    </tr>
                </table>            
            </div>                          
        </div>        

     <asp:ObjectDataSource ID="odsValores" runat="server" 
         SelectMethod="ListarValoresVariable" TypeName="clsIndicadores" 
            UpdateMethod="ActualizarValorVariable">
         <UpdateParameters>
             <asp:Parameter Name="variable" Type="String" />
             <asp:Parameter Name="subvariable" Type="String" />
             <asp:Parameter Name="dimension" Type="String" />
             <asp:Parameter Name="subdimension" Type="String" />
             <asp:Parameter Name="periodo" Type="String" />
             <asp:Parameter Name="valor" Type="Decimal" />
             <asp:Parameter Name="codigo" Type="Int32" />
             <asp:Parameter Name="codigo_var" Type="String" />
             <asp:Parameter Name="codigo_aux" Type="String" />
             <asp:Parameter Name="codigo_dim" Type="String" />
             <asp:Parameter Name="codigo_sub" Type="String" />
             <asp:Parameter Name="importado" Type="Boolean" />
             <asp:Parameter Name="sumatoria_var" Type="String" />
             <asp:Parameter Name="DetalleNivel" Type="Int32" />
             <asp:Parameter Name="UltimoNivel" Type="Int32" />
             <asp:Parameter Name="desc_niv2" Type="String" />
             <asp:Parameter Name="existebd_var" Type="String" />
         </UpdateParameters>
        <SelectParameters>
            <asp:ControlParameter ControlID="cboVariable" DefaultValue="%" 
                Name="codigo_var" PropertyName="SelectedValue" Type="String" />
            <asp:ControlParameter ControlID="cboSubvariable" DefaultValue="%" 
                Name="codigo_aux" PropertyName="SelectedValue" Type="String" />
            <asp:ControlParameter ControlID="cboDimension" DefaultValue="%" 
                Name="codigo_dim" PropertyName="SelectedValue" Type="String" />
            <asp:ControlParameter ControlID="cboSubdimension" DefaultValue="%" 
                Name="codigo_sub" PropertyName="SelectedValue" Type="String" />
            <asp:ControlParameter ControlID="cboPeriodo" DefaultValue="%" Name="codigo_pdo" 
                PropertyName="SelectedValue" Type="String" />
            <asp:ControlParameter ControlID="lblCodigo_per" DefaultValue="0" 
                Name="codigo_per" PropertyName="Text" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>        

        <asp:ValidationSummary ID="ValidationSummary1" runat="server" />

    </ContentTemplate>    
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="cboVariable" 
                EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="cboSubvariable" 
                EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="cboDimension" 
                EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="cboSubdimension" 
                EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="cboPeriodo" 
                EventName="SelectedIndexChanged" />
        </Triggers>    
    </asp:UpdatePanel>           
  </div>                            
        </div>
    </div>    
   
   
  </form>
</body>
</html>
