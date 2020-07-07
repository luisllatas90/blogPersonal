<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmasignarambientev2Detalle.aspx.vb" Inherits="academico_horarios_administrar_frmasignarambientev2Detalle" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
      
        body
        { font-family:Trebuchet MS;
          font-size:11px;
          cursor:hand;
          background-color:#F0F0F0;	
        }
        
    </style>
</head>
<body>
    <form id="form1" runat="server">
           <tr>
            <td>
                <h3>Editar Asignación de Ambientes&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </h3></td>
        </tr>
                <asp:GridView ID="gridDetalle" runat="server" AutoGenerateColumns="False" 
                    BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" 
                    CellPadding="4" ForeColor="Black" GridLines="Vertical" DataKeyNames="codigo_daa"
                    DataSourceID="SqlDataSource1">
                    <RowStyle BackColor="#F7F7DE" />
                    <Columns>
                        <asp:CommandField ShowDeleteButton="True" ButtonType="Image" 
                            CancelImageUrl="~/academico/horarios/administrar/images/back.png" 
                            DeleteImageUrl="~/academico/horarios/administrar/images/delete.png" 
                            HeaderText="Eliminar" >                        
                        <ControlStyle CssClass="btn" />
                        </asp:CommandField>
                        <asp:BoundField HeaderText="codigo_daa" Visible="False" ReadOnly="true"/>
                        <asp:BoundField HeaderText="Escuela Profesional" DataField="nombre_cpf" ReadOnly="true"></asp:BoundField>
                        <asp:BoundField HeaderText="Ambiente Ficticio" DataField="descripcion_Amb" ReadOnly="true"/>
                        <asp:BoundField HeaderText="Ambiente Real" DataField="descripcionReal_Amb" ReadOnly="true"/>
                        <asp:BoundField HeaderText="Fecha Inicio" DataField="fechaini_daa" DataFormatString="{0:g}"/>
                        <asp:BoundField HeaderText="Fecha Fin" DataField="fechafin_daa" DataFormatString="{0:g}"/>
                        <asp:CommandField ShowEditButton="True" ButtonType="Image" 
                            CancelImageUrl="~/academico/horarios/administrar/images/back.png" 
                            EditImageUrl="~/academico/horarios/administrar/images/edit.png" 
                            UpdateImageUrl="~/academico/horarios/administrar/images/check.png" 
                            HeaderText="Editar" />
                    </Columns>
                    <FooterStyle BackColor="#CCCC99" />
                    <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
   
    <table class="style1">

        <tr>
            <td>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                        ConnectionString="<%$ConnectionStrings:CNXBDUSAT%>" 
                        SelectCommand="AsingarAmbiente_ConsultarAsignacion" SelectCommandType="StoredProcedure"><%--revisar--%>
                        <SelectParameters>
                            <asp:QueryStringParameter Type="Int32" Name="codigo_amb" QueryStringField="codigo_amb"/>
                            <asp:SessionParameter Type="Int32" Name="codigo_cac" SessionField="scodigo_cac" />                           
                        </SelectParameters>
                </asp:SqlDataSource>
            </td>
        </tr>
    </table>
   
    </form>
</body>
</html>
