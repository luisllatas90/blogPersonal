<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmListaAlumnosProfesionalizacionF.aspx.vb" Inherits="frmListaAlumnosProfesionalizacion" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Programar las Invitaciones a Cursos de Nivelación - Profesionalización.</title>
    <!--<link href="Styles/estilo.css" rel="stylesheet" type="text/css" />-->
    <link rel="stylesheet" type="text/css" href="../../../private/estilo.css" />
    
    
    
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
            }
            else {
                obj.style.backgroundColor = "white"
            }
        }        
    </script>
    <style type="text/css">
            .styled-button-2 {
	            -webkit-box-shadow:rgba(0,0,0,0.2) 0 1px 0 0;
	            -moz-box-shadow:rgba(0,0,0,0.2) 0 1px 0 0;
	            box-shadow:rgba(0,0,0,0.2) 0 1px 0 0;
	            border-bottom-color:#333;
	            border:1px solid #61c4ea;
	            background-color:#7cceee;
	            border-radius:5px;
	            -moz-border-radius:5px;
	            -webkit-border-radius:5px;
	            color:#333;
	            font-family:'Verdana',Arial,sans-serif;
	            font-size:14px;
	            text-shadow:#b2e2f5 0 1px 0;
	            padding:5px
            }
</style>
    
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table cellpadding="3" cellspacing="0" style="border: 1px solid #C2CFF1; width:100%">
            <tr>
                <td bgcolor="#D1DDEF" colspan="2" height="30px">
                    <b>
                        <asp:Label ID="lblTitulo" runat="server" Text=""></asp:Label></b>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Panel ID="pnlConsulta" runat="server" BackColor="Azure">
                        <table cellpadding="3" cellspacing="0" style="border: 1px solid #C2CFF1; width:100%">
                        <tr>
                            <td style="width:8%">
                                <asp:Label ID="Label2" runat="server" Text="PROGRAMA:" ForeColor="Black"></asp:Label>
                            </td>
                            <td style="border:1" >
                                <asp:Label ID="lblPrograma" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td >
                                <asp:Label ID="Label3" runat="server" Text="CURSO:" ForeColor="Black"></asp:Label>
                            </td>
                            <td >
                                <asp:Label ID="lblCurso" runat="server" Text="" ></asp:Label>
                            </td>
                            <td>
                                <asp:HiddenField ID="hdfAccion" runat="server" />
                            </td>
                        </tr>
                    </table>
                    </asp:Panel>
                    
                    <asp:Panel ID="pnlMatricula" BackColor="Azure" BorderColor="Orange" runat="server">
                    <table cellpadding="3" cellspacing="0" style="border: 1px solid #C2CFF1; width:100%">
                        <tr>
                            <td>
                                 <table>
                            <tr>
                                <td>
                                    <asp:Label ID="Label4" runat="server" Text="CICLO ACADEMICO: " Font-Bold="True"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblCicloAcademico" runat="server" Text=""></asp:Label>
                                </td>
                                
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label5" runat="server" Text="PLAN ESTUDIOS: " Font-Bold="True"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblPlanEstudios" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label6" runat="server" Text="NOMBRE DEL CURSO: " 
                                        Font-Bold="True"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblCursoMatricula" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label7" runat="server" Text="GRUPO HORARIO: " Font-Bold="True"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblGrupoHorario" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label8" runat="server" Text="ESCUELA PROFESIONAL: " 
                                        Font-Bold="True"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblEscuelaProfesional" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                             <tr>
                                <td>
                                    <asp:Label ID="Label1" runat="server" Text="FECHA INICIO: " Font-Bold="True"></asp:Label>
                                </td>
                                <td style="width:60%">
                                    <asp:Label ID="lblFechaInicio_cup" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                             <tr>
                                <td>
                                    <asp:Label ID="Label9" runat="server" Text="FECHA FINAL: " Font-Bold="True"></asp:Label>
                                </td>
                                <td style="width:60%">
                                    <asp:Label ID="lblFechaFin_cup" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                        </table>    
                            </td>
                            <td valign="top" align="right">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/prof_leyenda.png" />
                                        </td>
                                    </tr>
                                </table>    
                            </td>
                        </tr>
                    </table>
                        
                        
                        
                    </asp:Panel>
                    <asp:Panel ID="pnlBotones" runat="server" BorderWidth="2" BorderColor="Azure">
                        <table cellpadding="3" cellspacing="0" style="border: 1px solid #C2CFF1; background="#FFFFDF" width:100%">
                        <tr valign="bottom">
                            
                            <td>
                            
                                             <asp:Button ID="btnPrematricula" runat="server"  
                                                 Font-Bold="True" Text="Pre-Matricula" CssClass="usatBoton" />
                            </td>
                            <td>
                               
                                             <asp:Button ID="cmdEliminar" runat="server"  
                                                 Font-Bold="True" Text="Eliminar Pre - Matricula" CssClass="usatBoton" 
                                                 Enabled="False" />
                            </td>
                            <td>
                                <asp:Button ID="cmdExportar" runat="server" 
                                    Font-Bold="True" Text="Exportar" CssClass="usatBoton" />
                            </td>
                        </tr>
                    </table>
                    </asp:Panel>
                    
                    
                    
                </td>
                
            </tr>
                  <tr>
                    <td>
                        <asp:Panel ID="pnlTabs" runat="server">
                            <table cellspacing="0" cellpadding="0" style="border-collapse: collapse;bordercolor: #111111;width:100%">
                            <tr>
                                <td class="pestanabloqueada" id="Td2" align="center" style="height:25px;width:15%" onclick="ResaltarPestana2('0','','');">
                                    <asp:LinkButton ID="lnkConsulta" Text="Listado de Alumnos" runat="server" Font-Bold="True" 
                                        Font-Underline="True" ForeColor="Black"></asp:LinkButton>
                                </td>
                                <td class="bordeinf" style="height:25px;width:1%">&nbsp;</td>
                                <td class="pestanabloqueada" id="tab" align="center" style="height:25px;width:15%" onclick="ResaltarPestana2('1','','');">
                                    <asp:LinkButton ID="lnkPrematriculados" runat="server" Font-Bold="True" 
                                        Text="Alumnos Pre-Matriculados" Font-Underline="True" ForeColor="Black">Alumnos Pre-Matriculados</asp:LinkButton>
                                </td>
			                    <td class="bordeinf" style="height:25px;width:1%">&nbsp;</td>
			                    <td class="pestanabloqueada" id="Td1" align="center" style="height:25px;width:15%" onclick="ResaltarPestana2('1','','');">
                                    <asp:LinkButton ID="lbkMatriculados" runat="server" Text="Alumnos Matriculados" 
                                        Font-Bold="True" Font-Underline="True" ForeColor="Black">Alumnos Matriculados</asp:LinkButton>
                                </td>
                                <td class="bordeinf" style="height:25px;width:1%">&nbsp;</td>
			                    
                            </tr>
                        
                        </table>
                        </asp:Panel>
                    </td>
                  
                  </tr>
                  
                  <tr>
                
                        <td>
                                <asp:GridView   ID="gvListaAlumnos" 
                                    runat="server" 
                                    AutoGenerateColumns="False" 
                                    BorderColor="Silver" 
                                    BorderStyle="Solid" 
                                    CaptionAlign="Top" 
                                    CellPadding="2" 
                                    EnableModelValidation="True" 
                                    Width="100%" 
                                    
                                    DataKeyNames="codigo_Alu,eMail_Alu,email2_Alu,alumno,Coodinador,email_Per,Codigo_cpf">
                        <RowStyle BorderColor="#C2CFF1" BorderStyle="Solid" BorderWidth="1px" />
                        <EmptyDataRowStyle BorderStyle="None" BorderWidth="0px" Font-Bold="True" ForeColor="Red" />
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:CheckBox ID="chkHeader" runat="server" onclick="MarcarCursos(this)" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkElegir" runat="server" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="#">
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="codigouniver_alu" HeaderText="Código">
                            <ItemStyle Font-Size="7pt" />
                            </asp:BoundField>
                            <asp:HyperLinkField DataNavigateUrlFields="codigo_alu" 
                                
                                
                                DataNavigateUrlFormatString="frmConsultaDatosProfesionalizacion.aspx?id={0}" DataTextField="alumno" 
                                HeaderText="Apellidos y Nombres" Target="_blank">
                            <ControlStyle Font-Underline="True" ForeColor="Blue" />
                            </asp:HyperLinkField>
                            <asp:BoundField DataField="cicloing_alu" HeaderText="Ciclo Ingreso" />
                            <asp:BoundField DataField="estadoactual_alu" HeaderText="Estado Actual">
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
<asp:BoundField DataField="UltimoCicloMatricula" HeaderText="Ultima Matricula">
</asp:BoundField>
                            <asp:BoundField DataField="email_alu" HeaderText="E-mail 1" Visible="False">
                            </asp:BoundField>
                            <asp:BoundField DataField="email2_alu" HeaderText="E-mail 2" Visible="False">
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemStyle Font-Size="7pt" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="cCreditosAprobados_alu" HeaderText="Crd. Aprobados" 
                                Visible="False">
                            <ItemStyle Font-Size="7pt" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="notaFinal_Dma" HeaderText="Nota" />
                            <asp:BoundField DataField="descripcion_Cco" HeaderText="Programa" />
                            <asp:BoundField DataField="eMail_Alu" HeaderText="Email Nº1" />
                            <asp:BoundField DataField="email2_Alu" HeaderText="Email Nº2" />
                            <asp:BoundField />
                            <asp:BoundField DataField="cntDeudas" HeaderText="N. Deudas">
                            <ItemStyle Font-Bold="True" HorizontalAlign="Right" />
                            </asp:BoundField>
                        </Columns>
                        <HeaderStyle BackColor="#e8eef7" BorderColor="#99BAE2" BorderStyle="Solid" 
                            BorderWidth="1px" ForeColor="#3366CC" />
                    </asp:GridView>
                        </td>    
                
                
                
                
            </tr>
        </table>
    </div>
    </form>
</body>
</html>