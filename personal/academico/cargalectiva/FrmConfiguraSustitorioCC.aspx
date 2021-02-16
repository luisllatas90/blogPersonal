<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmConfiguraSustitorioCC.aspx.vb" Inherits="academico_cargalectiva_FrmConfiguraSustitorioCC" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../../private/estilo.css" rel="stylesheet" type="text/css" />        
    <script type="text/javascript" language="javascript">
        function MarcarCursos(obj)
        {
           //asignar todos los controles en array
            var arrChk = document.getElementsByTagName('input');
           
            for (var i = 0 ; i < arrChk.length ; i++){
                var chk = arrChk[i];
                //verificar si es Check
                if (chk.type == "checkbox") {                    
                    chk.checked = obj.checked;
                    if (chk.id!=obj.id){
                       // PintarFilaMarcada(chk.parentNode.parentNode,obj.checked)
                    }
                }
            }
        }
        
         
        function PintarFilaMarcada2(obj,estado)
        {
            if (estado==true){
                obj.style.backgroundColor="#FFE7B3"
            }
            else{
                obj.style.backgroundColor = "#F7F6F3"
            }
        }
        function PintarFilaMarcada1(obj, estado) {
            if (estado == true) {
                obj.style.backgroundColor = "#FFE7B3"
            }
            else {
                obj.style.backgroundColor = "#CCCCCC"
            }
        }
        function PintarFilaMarcada3(obj, estado) {
            
            if (estado == true) {
                obj.style.backgroundColor = "#CEF6CE"
            }
            else {
                obj.style.backgroundColor = "white"
            }
        }
        
    </script>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            height: 23px;
        }
         body
        { font-family:Trebuchet MS;
          font-size:11.5px;
          cursor:hand;
          background-color:white;	 
        }
     .btn
       {
            border:1px solid #5D7B9D; 
            background:#F7F6F3 ; 
            font-family:Tahoma; 
            font-size:8pt; 
            font-weight:bold;  padding:3px; 
       }
    
        .style1
        {
            height: 36px;
        }
        .style12
        {
            height: 26px;
        }
        .style13
        {
            height: 17px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    
   <asp:HiddenField ID="hdFn" runat="server" /> 
    <div>
        <asp:Label ID="lblMensaje" runat="server" Font-Bold="True" Font-Size="Small" 
            ForeColor="Red"></asp:Label>
    <table>
        <tr>
            <td runat="server" id="texto">Carrera Profesional:</td>            
            <td>
                <asp:DropDownList ID="cboEscuela" runat="server">
                </asp:DropDownList>
            </td>                        
            <td>Estado:</td>
            <td>
                <asp:DropDownList ID="cboEstado" runat="server">
                    <asp:ListItem Text="Por programar" Value="0"></asp:ListItem>
                    <asp:ListItem Text="Programado" Value="1"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td></td>            
        </tr>
        <tr>
            <td>Semestre académico:</td>
            <td>
                <asp:DropDownList ID="cboCiclo" runat="server" AutoPostBack=true>
                </asp:DropDownList>            
            </td>                
            <td>Curso:</td>
            <td>
                <asp:TextBox ID="txtCurso" runat="server" width="250px"></asp:TextBox>
            </td>
            <td>
                <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn" />
            </td>            
        </tr>
        <tr>
            <td>Ciclo del Curso</td>
            <td>
                <asp:DropDownList ID="ddlCiclo_cur" runat="server">
                    <asp:ListItem Selected="True" Value="0">TODOS</asp:ListItem>
                    <asp:ListItem>1</asp:ListItem>
                    <asp:ListItem>2</asp:ListItem>
                    <asp:ListItem>3</asp:ListItem>
                    <asp:ListItem>4</asp:ListItem>
                    <asp:ListItem>5</asp:ListItem>
                    <asp:ListItem>6</asp:ListItem>
                    <asp:ListItem>7</asp:ListItem>
                    <asp:ListItem>8</asp:ListItem>
                    <asp:ListItem>9</asp:ListItem>
                    <asp:ListItem>10</asp:ListItem>
                    <asp:ListItem>11</asp:ListItem>
                    <asp:ListItem>12</asp:ListItem>
                    <asp:ListItem>13</asp:ListItem>
                    <asp:ListItem>14</asp:ListItem>
                </asp:DropDownList>
            </td>                
            <td>&nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>            
        </tr>
    </table>    
    </div>
    <br />
    <asp:Button ID="btnGenerar" runat="server" Text="Generar Ex. Recuperacion" CssClass="btn"/>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="btnCopiarHorario" runat="server" Text="Copiar Horario" 
        CssClass="btn"/>
        <br />
        <br />
         <asp:Panel ID="PanelExaRec" runat="server" >
        <asp:Panel ID="PanelEditarDocente" runat="server"  Visible=false>
        <center>
        <table class="style1" style="padding-left:15px; padding-top:10px; width:50%">
            <thead>
            <tr>
            <th colspan="2" style="text-align:center" class="style13">
                    EDITAR DOCENTE
            </th>
            </tr>
            </thead>
            <tbody>
            <tr>
            <td class="style1" colspan="2">
            <asp:Label ID="lblNombreCur2" runat="server" Text="Label"></asp:Label>
            </td>
            </tr>
            <tr>
                <td class="style2" style="width:50%">
                        <asp:HiddenField id="hdCupEdit" runat="server" />
                        <asp:DropDownList ID="ddlDocenteEdit" runat="server">
                        </asp:DropDownList></td>
                <td class="style2" style="width:50%"> <asp:Button ID="btnGuardarDocenteEdit" runat="server" Text="Guardar" CssClass="btn" /> &nbsp;<asp:Button ID="btnCancelarDocenteEdit" runat="server" Text="Cancelar" CssClass="btn" /></td>
            </tr>                
            </tbody>
            
        </table>
        </center>
        </asp:Panel>
        <asp:GridView ID="gvProgramado" runat="server" Width="100%" 
            AutoGenerateColumns="False" 
            DataKeyNames="codigo_cup,nombre_Cur,grupoHor_Cup,ambiente, nombre_cpf, codigo_Per, codigo_lho" CellPadding="4" 
            ForeColor="#333333">
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <Columns>
                <asp:TemplateField HeaderText="Copiar">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkElegirPadre" runat="server" onclick="PintarFilaMarcada2(this.parentNode.parentNode,this.checked)" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="codigo_cup" HeaderText="CODIGO" Visible="False" />
                <asp:BoundField DataField="nombre_cpf" HeaderText="CARRERA PROFESIONAL" ReadOnly="true" />
                <asp:BoundField DataField="nombre_cur" HeaderText="CURSO" ReadOnly="true" />
                <asp:BoundField DataField="grupoHor_cup" HeaderText="GRUPO" ReadOnly="true" /> 
                
                
                <asp:TemplateField HeaderText="Pegar">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkElegirHijo" runat="server" onclick="PintarFilaMarcada3(this.parentNode.parentNode,this.checked)" />
                    </ItemTemplate>
                </asp:TemplateField>
                               
                <asp:BoundField DataField="asistenciarec_cup" HeaderText="ASIST. (%)" />
                <asp:BoundField DataField="docente" HeaderText="DOCENTE" />
                <asp:TemplateField HeaderText="">
                    <ItemTemplate>
                    <table>
                    <tr>
                    <td> <asp:Button ID="btnEditarDocenteProgramado" CssClass="btn" runat="server" Text="Editar Docente"
                             CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                             CommandName="EditarDocente" />
                             </td>
              
                    </tr>
                    </table>
                       
                              
                             
                    </ItemTemplate>
                </asp:TemplateField> 
                <asp:CommandField ShowDeleteButton="True" DeleteText="Eliminar Curso" 
                    ButtonType="Button" ControlStyle-CssClass="btn"   />
                    
                <asp:BoundField DataField="ambiente" HeaderText="AMBIENTE" />
                <asp:TemplateField HeaderText="HORARIO">
                    <ItemTemplate>
                    <table>
                    <tr>
                    <td> <asp:Button ID="Button1" CssClass="btn" runat="server" Text="Asignar"
                             CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                             CommandName="SolicitarAmbiente" />
                             </td>
                    <td> <asp:Button ID="btnEliminarHorario" CssClass="btn" runat="server" Text="Borrar"
                             CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                             CommandName="EliminarHorario" /></td>
                    </tr>
                    </table>
                       
                              
                             
                    </ItemTemplate>
                </asp:TemplateField>     
                <asp:BoundField DataField="codigo_lho" HeaderText="codigo_lho" Visible="false" />                                                                                                                                                                                             
                <asp:BoundField DataField="vacantes_cup" HeaderText="VAC." Visible="true"/> 
            </Columns>
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />                    
            <EditRowStyle BackColor="#999999" />
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        </asp:GridView>
        
        
        <asp:GridView ID="gvDatos" runat="server" Width="100%" 
            AutoGenerateColumns="False" DataKeyNames="codigo_cup,codigo_per" 
            BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" 
            CellPadding="4" ForeColor="Black" GridLines="Vertical">
            <RowStyle BackColor="#F7F7DE" />
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:CheckBox ID="chkHeader" runat="server" onclick="MarcarCursos(this)" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="chkElegir" runat="server" onclick="PintarFilaMarcada1(this.parentNode.parentNode,this.checked)"/>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                </asp:TemplateField>                
                <asp:BoundField DataField="codigo_cup" HeaderText="CODIGO" Visible="False" />
                <asp:BoundField DataField="nombre_cpf" HeaderText="CARRERA PROFESIONAL" ReadOnly="true" />
                <asp:BoundField DataField="nombre_Cur" HeaderText="CURSO" ReadOnly="true" />
                <asp:BoundField DataField="grupoHor_Cup" HeaderText="GRUPO" ReadOnly="true"></asp:BoundField>
                <asp:BoundField DataField="ciclo_Cur" HeaderText="CICLO" ReadOnly="true" />
                
                <asp:TemplateField HeaderText="VAC">
                    <EditItemTemplate>                        
                        <asp:TextBox ID="txtVacantes" runat="server"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("vacantes_Cup") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                
                
                <asp:BoundField DataField="Refcodigo_Cup" HeaderText="Refcodigo_Cup" 
                    Visible="False" />
                <asp:TemplateField HeaderText="DOCENTE">
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlDocente" runat="server">
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("Docente") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>                                                    
                <asp:TemplateField HeaderText="ASIST. REQ. (%)">
                    <EditItemTemplate>                        
                        <asp:TextBox ID="txtAsistencia" runat="server"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("asistenciarec_cup") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                
                
                
                 
                <asp:BoundField DataField="codigo_per" HeaderText="codigo_per" 
                    Visible="False" />
                <asp:CommandField ShowEditButton="True" />
            </Columns>
            <FooterStyle BackColor="#CCCC99" />
            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#6B696B" ForeColor="White" Font-Bold="True" />
            <AlternatingRowStyle BackColor="White" />
        </asp:GridView>
    <div>
    </div>
    <br />
    <div>
    </div>
    <asp:HiddenField ID="HdTipo" runat="server" />        
    </asp:Panel> <!--Page Recuperación-->
    
    
    <asp:Panel ID="PanelHorarioRegistro" runat="server" Visible="False" >
        <table class="style1" style="padding-left:15px; padding-top:10px;">
            <tr>
                <td colspan="2" class="style1">
                    <table  cellpadding="3" cellspacing="0" style="border: 1px solid #C2CFF1; width:95%" >
                    <tr style="background-color: #E8EEF7; font-weight: bold; height: 30px">
                    <td><b>Primer Paso: Registro de Solicitud de Ambiente&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        
                        </b>
                    </td>  
                    <td><asp:Button ID="btnCancelar" runat="server" CssClass="btn" Text="Regresar" /></td>          
                    </tr>
                    </table>                
                </td>
            </tr>           
            <tr>
                <td class="style2">Carrera Profesional</td>
                <td class="style2"><b>
                    <asp:Label ID="lblNombreCarrera" runat="server" Text="Label"></asp:Label>
                    </b>
                </td>
            </tr>
            <tr>
                <td class="style2">
                    Curso de Recuperación</td>
                <td class="style2">
                    <b>
                    <asp:Label ID="lblNombreCur" runat="server" Text="Label"></asp:Label>
                    </b>
                </td>
            </tr>
            <tr>
                <td class="style2">
                    Fecha</td>
                <td class="style2">
                    <asp:DropDownList ID="ddlDia" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    Hora Inicio</td>
                <td>
                    <asp:DropDownList ID="ddlInicioHora" runat="server"></asp:DropDownList>
                    <asp:DropDownList ID="ddlInicioMinuto" runat="server"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    Horario Fin</td>
                <td>
                    <asp:DropDownList ID="ddlFinHora" runat="server"></asp:DropDownList>
                    <asp:DropDownList ID="ddlFinMinuto" runat="server"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    Capacidad del Ambiente</td>
                <td>
                    <asp:DropDownList ID="ddlCap" runat="server"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    Descripción del Evento</td>
                <td>
                    <asp:TextBox ID="txtDescripcion" runat="server" Height="46px" MaxLength="500" 
                        TextMode="MultiLine" Width="415px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    <asp:Button ID="btnRegistrarPers" runat="server" CssClass="btn" 
                        Text="1. Registrar Solicitud" />
                </td>
            </tr>
        </table>
        <br />
    </asp:Panel>
     <asp:Panel ID="PanelBuscar" runat="server" Visible="False" >
         <table class="style1" >
            <tr>
                <td colspan="2" >
                    <table  cellpadding="3" cellspacing="0" style="border: 1px solid #C2CFF1; width:95%" >
                    <tr style="background-color: #E8EEF7; font-weight: bold; height: 30px">
                    <td><b>Segundo Paso: Buscar disponibilidad de ambiente&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                       
                        </b>
                    </td>  
                    <td> <asp:Button ID="Button2" runat="server" CssClass="btn" Text="Regresar" /></td>          
                    </tr>
                    </table>                
                </td>
            </tr> 
             <tr>
                 <td>
                     
                         Carrera Profesional
                 </td>
                 <td>
                     <b>
                     <asp:Label ID="lblNombreCarrera0" runat="server" Text="Label"></asp:Label>
                     </b>
                 </td>
             </tr>
             <tr>
                 <td>
                     Curso de Recuperación</td>
                 <td>
                     <b>
                     <asp:Label ID="lblNombreCur0" runat="server" Text="Label"></asp:Label>
                     </b>
                 </td>
             </tr>
             <tr>
                 <td>
                     Horario</td>
                 <td>
                     <b>
                     <asp:Label ID="lblHorario" runat="server" Text="Label"></asp:Label>
                     </b>
                 </td>
             </tr>
             <tr>
                 <td class="style12">
                     Tipo de Ambiente</td>
                 <td class="style12">
                     <asp:DropDownList ID="ddlTipoAmbiente" runat="server">
                     </asp:DropDownList>
                 </td>
             </tr>
             <tr>
                 <td>
                     &nbsp;</td>
                 <td>
                     <asp:Button ID="btnBuscarH" runat="server" CssClass="btn" 
                         Height="25px" Text="2. Buscar" Width="129px" />
                 </td>
             </tr>
             <tr>
                 <td colspan="2">
                     <asp:GridView ID="gridAmbientes" runat="server" BackColor="White" 
                         BorderColor="#C2CFF1" BorderStyle="Solid" BorderWidth="1px" CellPadding="2" 
                         DataKeyNames="Accion, Tipo" Width="65%" AutoGenerateColumns="False">
                         <RowStyle BackColor="White" BorderColor="#C2CFF1" Font-Size="X-Small" 
                             ForeColor="#333333" HorizontalAlign="Center" />
                         <Columns>
                             <asp:BoundField DataField="Tipo" HeaderText="Tipo" />
                             <asp:BoundField DataField="Ubicación" HeaderText="Ubicación" />
                             <asp:BoundField DataField="Ambiente" HeaderText="Ambiente" />
                             <asp:BoundField DataField="Cap" HeaderText="Cap" />
                             <asp:BoundField DataField="Accion" HeaderText="Accion" Visible="False" />
                             <asp:TemplateField>
                                 <ItemTemplate>
                                     <asp:Button ID="Button3" runat="server" CssClass="btn" 
                                         Text="Asignar Ambiente" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                             CommandName="AsignarAmbiente" />
                                 </ItemTemplate>
                             </asp:TemplateField>
                         </Columns>
                         <FooterStyle BackColor="White" ForeColor="#333333" />
                         <PagerStyle BackColor="#C2CFF1" ForeColor="White" HorizontalAlign="Center" />
                         <SelectedRowStyle BackColor="#C2CFF1" Font-Bold="True" ForeColor="White" />
                         <HeaderStyle BackColor="#E8EEF7" Font-Bold="True" ForeColor="#587ECB" />
                         <EmptyDataTemplate>
                             <div>
                                 <i>No se encontraron ambientes.</i></div>
                         </EmptyDataTemplate>
                     </asp:GridView>
                 </td>
             </tr>
         </table>
     </asp:Panel>
     <div style="text-align:center;">
        <asp:Panel ID="pnlPregunta" runat="server" BorderColor="#5D7B9D" 
        BorderStyle="Solid" BorderWidth="1px" style="text-align: center; padding:5px;" 
        Visible="False" Width="25%" BackColor="#F7F6F4">
        <b><span class="style1">
              No ha concluído el registro.¿Desea regresar a la página inicial?. Se borrará la solicitud de ambiente.</span></b><br />
        <asp:Label ID="lblFecha" runat="server" 
            style="color: #3366CC;  font-weight: 700;" Text="Label"></asp:Label>
        <br />
        <asp:Label ID="lblActividad" runat="server" 
            style="color: #000000; font-weight: 700;" Text="Label"></asp:Label>
        <br />
        <br />
        <asp:Button ID="btnSi" runat="server" CssClass="btn" Text="Sí" Width="50px" />
        &nbsp;&nbsp;
        <asp:Button ID="btnNo" runat="server" CssClass="btn" Text="No" Width="50px" />
        <br />      
    </asp:Panel>
    </div>
    
    </form>
</body>
</html>
