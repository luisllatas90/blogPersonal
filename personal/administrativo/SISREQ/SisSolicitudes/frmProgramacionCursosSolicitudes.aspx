<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmProgramacionCursosSolicitudes.aspx.vb" Inherits="administrativo_SISREQ_SisSolicitudes_frmProgramacionCursosSolicitudes" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        Solicitudes Aprobadas/Pagadas:<br />
                    <asp:GridView ID="gvLista" Width="100%" runat="server" BackColor="White" 
                        BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                        AutoGenerateColumns="False" EmptyDataText="No se encontraron registros.." 
                        AllowPaging="True" PageSize="6">
                        <RowStyle ForeColor="#000066" />
                        <EmptyDataRowStyle BackColor="#FFFF99" ForeColor="#FF3300" />
                        <Columns>
                            <asp:BoundField HeaderText="Código Solicitud" >
                                <ItemStyle Width="15px" />
                            </asp:BoundField>
                            
                            <asp:BoundField DataField="Codigo" HeaderText="Código Estudiante" />
                            <asp:BoundField DataField="Docente" HeaderText="Estudiante" />
                            <asp:BoundField DataField="nombre_Dac" HeaderText="Monto Pagado" />
                            <asp:BoundField DataField="descripcion_Cco" HeaderText="Monto Cargado" />
                            <asp:BoundField DataField="EstadoRev_svac" HeaderText="Saldo" />
                            
                            <asp:BoundField HeaderText="Estado" />
                            
                            <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkHeader" runat="server"  onclick="MarcarCursos(this)" />
                                    </HeaderTemplate>
                                    <ItemTemplate>                
                                        <asp:CheckBox ID="chkElegir" runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle Width="5px" />
                            </asp:TemplateField>
                            <asp:CommandField SelectText="" ShowSelectButton="True" />
                        </Columns>
                        
                        <FooterStyle BackColor="White" ForeColor="#000066" />
                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                        <SelectedRowStyle BackColor="#FFFFB1" Font-Bold="True" ForeColor="Blue" />
                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                    </asp:GridView>   
    
    </div>
    <div>
    
        Cursos Solicitados:<br />
                    <asp:GridView ID="gvLista2" Width="100%" runat="server" BackColor="White" 
                        BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                        AutoGenerateColumns="False" EmptyDataText="No se encontraron registros.." 
                        AllowPaging="True" PageSize="6">
                        <RowStyle ForeColor="#000066" />
                        <EmptyDataRowStyle BackColor="#FFFF99" ForeColor="#FF3300" />
                        <Columns>
                            <asp:BoundField HeaderText="Código Curso" >
                                <ItemStyle Width="15px" />
                            </asp:BoundField>
                            
                            <asp:BoundField DataField="Codigo" HeaderText="Curso" />
                            <asp:BoundField DataField="Docente" HeaderText="Total Créditos" />
                            <asp:BoundField DataField="nombre_Dac" HeaderText="total Horas" />
                            <asp:CommandField SelectText="" ShowSelectButton="True" />
                            <asp:BoundField HeaderText="Estado" />
                            
                            <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkHeader0" runat="server"  onclick="MarcarCursos(this)" />
                                    </HeaderTemplate>
                                    <ItemTemplate>                
                                        <asp:CheckBox ID="chkElegir0" runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle Width="5px" />
                            </asp:TemplateField>
                        </Columns>
                        
                        <FooterStyle BackColor="White" ForeColor="#000066" />
                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                        <SelectedRowStyle BackColor="#FFFFB1" Font-Bold="True" ForeColor="Blue" />
                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                    </asp:GridView>   
    
    </div>
    <div>
    
        Asignación de Profesor/Curso:<br />
                    <asp:GridView ID="gvLista3" Width="100%" runat="server" BackColor="White" 
                        BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                        AutoGenerateColumns="False" EmptyDataText="No se encontraron registros.." 
                        AllowPaging="True" PageSize="6">
                        <RowStyle ForeColor="#000066" />
                        <EmptyDataRowStyle BackColor="#FFFF99" ForeColor="#FF3300" />
                        <Columns>
                            <asp:BoundField HeaderText="Código Profesor" >
                                <ItemStyle Width="15px" />
                            </asp:BoundField>
                            
                            <asp:BoundField DataField="Codigo" HeaderText="Profesor" />
                            <asp:BoundField DataField="Docente" HeaderText="Escuela" />
                            <asp:CommandField SelectText="" ShowSelectButton="True" />
                            <asp:BoundField HeaderText="Estado" />
                            
                            <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkHeader1" runat="server"  onclick="MarcarCursos(this)" />
                                    </HeaderTemplate>
                                    <ItemTemplate>                
                                        <asp:CheckBox ID="chkElegir1" runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle Width="5px" />
                            </asp:TemplateField>
                        </Columns>
                        
                        <FooterStyle BackColor="White" ForeColor="#000066" />
                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                        <SelectedRowStyle BackColor="#FFFFB1" Font-Bold="True" ForeColor="Blue" />
                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                    </asp:GridView>   
    
    </div>
    <div>
    
        <br />
        <asp:Button ID="Button1" runat="server" Height="30px" Text="Guardar" 
            Width="103px" />
&nbsp;<asp:Button ID="Button2" runat="server" Height="30px" Text="Cancelar" 
            Width="103px" />
    
    </div>
    </form>
    <div>
    
    </div>
    </body>
</html>
