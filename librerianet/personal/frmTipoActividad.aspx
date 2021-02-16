<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmTipoActividad.aspx.vb" Inherits="frmTipoActividad" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    
    <link href="../private/estilo.css" rel="stylesheet" type="text/css" />
    <link href="../private/estiloweb.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="JavaScript" src="../../private/funciones.js"></script>
    <script type="text/javascript" language="javascript">
        function MarcarCursos(obj)
        {
           //asignar todos los controles en array
            var arrChk = document.getElementsByTagName('input');
            for (var i = 0 ; i < arrChk.length ; i++){
                var chk = arrChk[i];
                //verificar si es Check
                if (chk.type == "checkbox"){
                    chk.checked = obj.checked;
                    if (chk.id!=obj.id){
                        PintarFilaMarcada(chk.parentNode.parentNode,obj.checked)
                    }
                }
            }
        }
      
         
        function PintarFilaMarcada(obj,estado)
        {
            if (estado==true){
                obj.style.backgroundColor="#FFE7B3"
            }
            else{
                obj.style.backgroundColor="white"
            }
        }        
    </script>
    
  <style type="text/css">
        .verticaltext
        {
            font:bold 10px Tahoma;
            color: #0000FF;
            /*writing-mode: tb-rl;
            filter: flipH() flipV();
            */

        }
  </style>
    
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table cellpadding="3" cellspacing="0" style="border: 1px solid #C2CFF1; width:100%">
            <tr>
                <td bgcolor="#003399" colspan="2" height="30px">
                    <b>
                        <asp:Label ID="lblTitulo" runat="server" Text="Lista de Actividad" 
                        ForeColor="White"></asp:Label></b>
                </td>
            </tr>
            <tr>
                <td>    
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="Label1" runat="server" Text="PERIODO"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlPeriodoLaboral" runat="server" AutoPostBack="True">
                                </asp:DropDownList>
                            </td>
                            
                            <td>
                                
                                <asp:CheckBox ID="chkFomrato" runat="server" AutoPostBack="True" Checked="True" 
                                    ForeColor="Red" Text="Sin Formato HH:MM" />
                            </td>
                            <td>
                                
                                <asp:Button ID="btnExportar" CssClass="excel" runat="server" Text="Exportar" 
                                    Width="132px" />
                                
                            </td>
                        </tr>
                        <tr>
                        <td >
                                <asp:Label ID="Label2" runat="server" Text="DEP.ACAD / ÁREA"></asp:Label>
                            </td>
                            <td colspan="3">
                                <asp:DropDownList ID="ddlDptAcad" runat="server" AutoPostBack="True">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:GridView ID="gvTipoActividad" Width="100%" runat="server" BackColor="White" 
                        BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                        AutoGenerateColumns="False">
                        <RowStyle BackColor="White" ForeColor="#003399" />
                        <Columns>
                            <asp:BoundField DataField="codigo_per" HeaderText="ID" Visible="False" />
                            <asp:BoundField HeaderText="#" />
                            <asp:BoundField DataField="trabajador" HeaderText="Apellidos - Nombres" />
                            <asp:BoundField DataField="Descripcion_Ded" HeaderText="Ded." />
                            <asp:BoundField DataField="descripcion_Tpe" HeaderText="T.Persona" />
                            <asp:BoundField DataField="Dedicacion" HeaderText="Acumulado" />
                            <asp:BoundField DataField="AsesoriaTesis" HeaderText="A.Tesis">
                            <HeaderStyle BackColor="#FA7806" Font-Bold="True" ForeColor="White" />
                            <ItemStyle BackColor="#FA7806" ForeColor="White" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CargaAdministrativa" HeaderText="C.Administrativa">
                            <HeaderStyle BackColor="#E49BD0" Font-Bold="True" ForeColor="#663300" />
                            <ItemStyle BackColor="#E49BD0" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CentroPre" HeaderText="C.Pre" Visible="False">
                            <HeaderStyle BackColor="#CA7F4C" Font-Bold="True" ForeColor="#003366" />
                            <ItemStyle BackColor="#CA7F4C" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CooperacionInterInstitucional" 
                                HeaderText="Coop.InterInstitucional" 
                                SortExpression="CooperacionInterInstitucional">
                            <HeaderStyle BackColor="#CC6600" />
                            <ItemStyle BackColor="#CA7F4C" />
                            </asp:BoundField>
                            <asp:BoundField DataField="GestinAcademica" HeaderText="G.Academica">
                            <HeaderStyle BackColor="#B59B3C" Font-Bold="True" ForeColor="White" />
                            <ItemStyle BackColor="#B59B3C" ForeColor="White" />
                            </asp:BoundField>
                            <asp:BoundField DataField="OtraGestionAcademicaPreGrado" 
                                HeaderText="OtraGestiónAcad.PreGrado" 
                                SortExpression="OtraGestionAcademicaPreGrado" Visible="False">
                            <HeaderStyle BackColor="#B59B3C" />
                            <ItemStyle BackColor="#B59B3C" ForeColor="White" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ApoyoInstitucional" HeaderText="A.Institucional">
                            <HeaderStyle BackColor="#08FAE9" Font-Bold="True" ForeColor="#003366" />
                            <ItemStyle BackColor="#08FAE9" />
                            </asp:BoundField>
                            <asp:BoundField DataField="PracticasExternas" HeaderText="P.Externas">
                            <HeaderStyle BackColor="#FFFF66" Font-Bold="True" ForeColor="#663300" />
                            <ItemStyle BackColor="#F9FF2E" ForeColor="#990000" />
                            </asp:BoundField>
                            <asp:BoundField DataField="SupervisionPracticasPreProfesionales" 
                                HeaderText="Superv.Pract.PreProfesionales" 
                                SortExpression="SupervisionPracticasPreProfesionales">
                            <HeaderStyle BackColor="#CE06FA" />
                            <ItemStyle BackColor="#CE06FA" ForeColor="White" />
                            </asp:BoundField>
                            <asp:BoundField DataField="AsistencialesClinicaUsat" HeaderText="A.C.Usat" 
                                Visible="False">
                            <HeaderStyle BackColor="#AE906B" Font-Bold="True" ForeColor="#003366" />
                            <ItemStyle BackColor="#AE906B" />
                            </asp:BoundField>
                            <asp:BoundField DataField="HorasLectivas" HeaderText="H.Lectivas">
                            <HeaderStyle BackColor="#98E54B" Font-Bold="True" ForeColor="#003366" />
                            <ItemStyle BackColor="#98E54B" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Investigacion" HeaderText="G. Investigacion">
                            <HeaderStyle BackColor="#99CCFF" Font-Bold="True" ForeColor="White" />
                            <ItemStyle BackColor="#99CCFF" ForeColor="White" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ResponsabilidadSocial" HeaderText="R.Social">
                            <HeaderStyle BackColor="#8C9286" HorizontalAlign="Center" 
                                VerticalAlign="Middle" Font-Bold="True" />
                            <ItemStyle BackColor="#8C9286" Font-Bold="False" ForeColor="#003366" />
                            </asp:BoundField>
                            <asp:BoundField DataField="InvestigacioneInnovacion" 
                                HeaderText="Inv. &amp; Innov" SortExpression="InvestigacioneInnovacion" >
                                <HeaderStyle BackColor="#226AE5" />
                                <ItemStyle BackColor="#226AE5" />
                            </asp:BoundField>
                            <asp:BoundField DataField="PlandeFormacionDocente" 
                                HeaderText="Form. Docente" SortExpression="PlandeFormacionDocente" >
                                <HeaderStyle BackColor="#009966" />
                                <ItemStyle BackColor="#009966" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Tutoria" HeaderText="Tutoria">
                            <HeaderStyle BackColor="#FC4450" HorizontalAlign="Center" 
                                VerticalAlign="Middle" Font-Bold="True" ForeColor="White" />
                            <ItemStyle BackColor="#FC4450" Font-Bold="False" ForeColor="White" />
                            </asp:BoundField>
<asp:BoundField DataField="TutoriaGO" HeaderText="Tutoria GO">
    <HeaderStyle BackColor="#FC4450" />
    <ItemStyle BackColor="#FC4450" />
</asp:BoundField>
                            <asp:BoundField DataField="codigo_per" HeaderText="id" />
                            <asp:BoundField DataField="DepAcadArea" HeaderText="Dep.Acad/Área" />
                            <asp:BoundField DataField="totalhorascar" HeaderText="CargaAcad" />
                        </Columns>
                        <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                        <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                        <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" 
                            HorizontalAlign="Left" />
                        <HeaderStyle BackColor="#003399" CssClass="verticaltext" Font-Bold="True" ForeColor="#CCCCFF" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
