<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmDescargarDataHorario.aspx.vb" Inherits="academico_horarios_administrar_frmDescargarDataHorario" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <style type="text/css">
     body
        { font-family:Trebuchet MS;
          font-size:12px;
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
                height: 26px;
            }
                  
            </style>
</head>
<body>
    <form id="form1" runat="server">
     <table  cellpadding="3" cellspacing="0" style="border: 1px solid #C2CFF1; width:95%" >
        <tr style="background-color: #E8EEF7; font-weight: bold; height: 30px">
            <td colspan="7" style="font-size:14px"><b>Descargar Información de Entrada</b></td>            
        </tr>
    </table>
    <br />
    
    <table class="style1">
          
        <tr>
            <td>
                Semestre Académico</td>
            <td>
        <asp:DropDownList ID="ddlCiclo" runat="server" AutoPostBack="True">
        </asp:DropDownList>
            </td>
            <td>
                &nbsp;</td>
            <td>
                Escuela Profesional</td>
            <td>
                <asp:DropDownList ID="ddlCarreraProfesional" runat="server" AutoPostBack="True">
                </asp:DropDownList>
            </td>
        </tr>
          
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        </table>
        <table>  
        <tr>
            <td>
                <asp:LinkButton ID="LinkButton1" runat="server">Hoja 1: Escuela</asp:LinkButton>
            </td>
            <td>  </td>
            <td>
                <asp:LinkButton ID="LinkButton2" runat="server">Hoja 2: Aulas</asp:LinkButton>
            </td>
            <td>  </td>
            <td>
                <asp:LinkButton ID="LinkButton3" runat="server">Hoja 3: Profesores</asp:LinkButton>
            </td>
            <td>  </td>
            <td>
                <asp:LinkButton ID="LinkButton4" runat="server">Hoja 4: Secciones</asp:LinkButton>
            </td>
            <td>  </td>
            <td>
                <asp:Button ID="Button1" runat="server" Text="Descargar" class="btn"
                   />
            </td>
        </tr>
          
        </table>    <br />
     <asp:GridView ID="GridView1" runat="server" CellPadding="4" ForeColor="#333333" 
         GridLines="Both">
         <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
         <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
         <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
         <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
         <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
         <EditRowStyle BackColor="#999999" />
         <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
     </asp:GridView>
    </form>
</body>
</html>
