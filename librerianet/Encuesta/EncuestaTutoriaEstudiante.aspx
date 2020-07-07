<%@ Page Language="VB" AutoEventWireup="false" CodeFile="EncuestaTutoriaEstudiante.aspx.vb" Inherits="EvaluacionDocente_Estudiante" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>USAT::ENCUESTA PARA EVALUAR EL SISTEMA DE TUTORÍA PARA ESTUDIANTES USAT</title>
    <link href="../../private/estilo.css" rel="stylesheet" type="text/css" /> 
    <link href="../../private/estiloweb.css" rel="stylesheet" type="text/css" /> 
    <script src="../../private/funciones.js" type ="text/javascript" language ="javascript"></script>
    <style type="text/css">
        .style1
        {
            text-align: center;
        }
        .style2
        {
            text-align: center;
            height: 25px;
        }
        .style3
        {
            font-size: small;
        }
        .style4
        {
            font-size: x-small;
        }
        .style5
        {
            text-align: left;
        }
    </style>
</head>
<body>
<center>
    <form id="form1" runat="server">
    <table style="width:80%;">
        <tr>
            <td align="center" colspan="2">
                <b>
                <asp:Image ID="Image1" runat="server" 
                     ImageUrl="https://intranet.usat.edu.pe/usat/files/2011/02/Logo-USAT-300x150.png" 
                     Width="142px" Height="79px" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span class="style4">ENCUESTA PARA EVALUAR EL SISTEMA DE TUTORÍA PARA ESTUDIANTES USAT</span></b></td>
        </tr>
        <tr>
            <td align="center" width="80%">
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td>
            <td align="right">
                
            </td>
        </tr>
        <tr>
            <td colspan="2" style=" text-align:left">
                <b>Estudiante:&nbsp;&nbsp;</b>
                <asp:Label ID="lblEstudiante" runat="server" Width="438px" 
                    style="text-align: left"></asp:Label>
            </td>
            </tr>
            <tr>
            <td colspan="2" style=" text-align:left">
                <b>Tutor:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</b>
                <asp:Label ID="lblTutor" runat="server" Width="438px" style="text-align: left"></asp:Label>
            </td>
            </tr>
            <tr>
            <td colspan="2" style=" text-align:left">
                <b>Escuela:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</b>
                <asp:Label ID="lblEscuela" runat="server" Width="438px" 
                    style="text-align: left"></asp:Label>
            </td>
            </tr>
            <tr>
            <td colspan="2" style=" text-align:left">
                <b>Semestre:&nbsp;&nbsp;&nbsp;&nbsp;</b>
               <asp:Label ID="lblSemestre" runat="server" Width="438px" 
                    style="text-align: left"></asp:Label>
            </td>
            </tr>
        
        <tr>
            <td colspan="2" class="style5">
                <br class="style3" />
                <span class="style3">Estimado estudiante, esta encuesta busca valorar la labor tutorial que has recibido durante el presente ciclo académico, a la vez, conocer tu grado de satisfacción con respecto al servicio recibido, a fin de mejorarlo. Agradecemos de antemano tu apoyo, pidiéndote, por favor, que responda a ella con veracidad </span>
             </td>
        </tr>
        <tr>
            <td colspan="2" align="justify">
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="2" align="justify" style="color: #FF0000">
            Preguntas
</td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="2" align="justify">
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="2" align="justify">
                Marque según el grado de valoración que le otorgue a cada una de las siguientes afirmaciones sobre la labor tutorial, de acuerdo a la escala que se muestra a continuación:</tr>
        <tr>
            <td colspan="2" style="font-weight: 700" class="style1">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Nunca&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                Rara Vez&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                Pocas Veces&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                Casi Siempre&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                Siempre</td>
        </tr>
        <tr>
            <td colspan="2" style="font-weight: 700" class="style2">
                -1--------------------2------------------------3------------------------4----------------------------------5-</td>
        </tr>
        <tr>
            <td colspan="2" class="style1">
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="2">
                <hr />
            </td>
        </tr>
        <tr>
            <td align="right" colspan="2">
                <asp:Button ID="cmdGuardarArriba" runat="server" Text="   Guardar" 
                    ValidationGroup="Guardar" CssClass="guardar" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:GridView ID="gvPreguntas" runat="server" AutoGenerateColumns="False" 
                    DataKeyNames="codigo_eva,conrespuesta_eva,orden_eva" 
                    HorizontalAlign="Center" GridLines="Horizontal" Width="98%">
                    <Columns>
                        <asp:BoundField DataField="numero_eva" HeaderText="Nº" >
                            <ItemStyle Width="15px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="pregunta_eva" HeaderText="Items de evaluación" >
                            <ItemStyle HorizontalAlign="Left" Width="90%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="1" />
                        <asp:BoundField HeaderText="2" />
                        <asp:BoundField HeaderText="3" />
                        <asp:BoundField HeaderText="4" />
                        <asp:BoundField HeaderText="5" />
                    </Columns>
                    <HeaderStyle Height="20px" BackColor="#0066CC" ForeColor="White" />
                    <AlternatingRowStyle BorderColor="#3366CC" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="2" style="font-weight: 700" class="style1">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Muy Insatisfecho&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                Insatisfecho&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                Ni Satisfecho/ Ni Insatisfecho&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                Satifecho&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                Muy Satisfecho</td>
        </tr>
        <tr>
            <td colspan="2" style="font-weight: 700" class="style2">
                -1-------------------------------2-----------------------------------3--------------------------------------------4-----------------------5---</td>
        </tr>
        <tr>
            <td colspan="2">
                &nbsp;&nbsp;&nbsp;
                    </td>
        </tr>
        <tr>
            <td colspan="2">
                &nbsp;&nbsp;&nbsp;
                    <asp:GridView ID="gvPreguntas0" runat="server" AutoGenerateColumns="False" 
                    DataKeyNames="codigo_eva,conrespuesta_eva,orden_eva" 
                    HorizontalAlign="Center" GridLines="Horizontal" Width="98%">
                    <Columns>
                        <asp:BoundField DataField="numero_eva" HeaderText="Nº" >
                            <ItemStyle Width="15px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="pregunta_eva" HeaderText="Items de evaluación" >
                            <ItemStyle HorizontalAlign="Left" Width="90%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="1" />
                        <asp:BoundField HeaderText="2" />
                        <asp:BoundField HeaderText="3" />
                        <asp:BoundField HeaderText="4" />
                        <asp:BoundField HeaderText="5" />
                    </Columns>
                    <HeaderStyle Height="20px" BackColor="#0066CC" ForeColor="White" />
                    <AlternatingRowStyle BorderColor="#3366CC" />
                </asp:GridView>
                    </td>
        </tr>
        <tr>
            <td colspan="2">
                    &nbsp;</td>
        </tr>
        <tr>
            <td colspan="2" align="right">
                <asp:Button ID="cmdGuardarAbajo" runat="server" Text="   Guardar" 
                    ValidationGroup="Guardar" CssClass="guardar" />
            </td>
        </tr>
    </table>
    <div>
    
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
        ValidationGroup="Guardar" ShowMessageBox="True" ShowSummary="False" />
    
    </div>
    <asp:HiddenField ID="hddCodigo_cev" runat="server" />
    <asp:HiddenField ID="hddcodigo_cac" runat="server" />
    </form>
    </center>
</body>
</html>
