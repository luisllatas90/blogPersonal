<%@ Page Language="VB" AutoEventWireup="false" CodeFile="lsthorarioregistradoV2.aspx.vb" Inherits="academico_horarios_lsthorarioregistradoV2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
  <meta charset="utf-8">
  <meta name="viewport" content="width=device-width, initial-scale=1">
  <link rel="stylesheet" href="bootstrap/bootstrap.min.css">
  <script src="bootstrap/jquery.min.js"></script>
  <script src="bootstrap/bootstrap.min.js"></script>
</head>
<body>
    
    <form id="form1" runat="server">
    <div class="table-responsive"> 
   
        <div class="alert alert-warning">
            <asp:Label ID="lblTitulo" runat="server" Text="Título" style="font-weight: 700"></asp:Label>
            <asp:HiddenField ID="difHoras" runat="server"  />
            <asp:HiddenField ID="difHorasMod" runat="server"  />
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"  class="table table-bordered bs-table"
       DataKeyNames="codigo_lho,codigo_cpf,dia_lho,nombre_hor,horafin_lho" Width="100%" Font-Size="X-Small">
       
        <Columns>
            <asp:BoundField DataField="codigo_lho" HeaderText="codigo_lho" 
                Visible="False" />
            <asp:TemplateField HeaderText="BORRAR" ItemStyle-HorizontalAlign="Center"> 
                <ItemTemplate>
                    <asp:ImageButton ID="btnEliminar" runat="server" 
                       CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                       CommandName="EliminarLinea"
                       ImageUrl="../../../images/Eliminar.gif" 
                       ToolTip="Eliminar Línea Horario"
                       />  
                </ItemTemplate>

<ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:TemplateField>
            <asp:BoundField DataField="dia_lho" HeaderText="DÍA" />
            <asp:BoundField DataField="ambiente" HeaderText="AMBIENTE" />
            
            <asp:BoundField DataField="nombre_Hor" HeaderText="H.INI" />
            <asp:BoundField DataField="horaFin_Lho" HeaderText="H.FIN" />
            <asp:BoundField DataField="ciclo_cur" HeaderText="CICLO" 
                ItemStyle-HorizontalAlign="Center" >
<ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="nombre_cur" HeaderText="ASIGNATURA" />
            <asp:BoundField DataField="docente" HeaderText="DOCENTE DEL CURSO" />
            <asp:BoundField DataField="nombre_cpf" HeaderText="CARRERA PROFESIONAL" />
            <asp:BoundField DataField="fechaInicio_cup" DataFormatString="{0:dd/MM/yyyy}" 
                HeaderText="F.INI" />
            <asp:BoundField DataField="fechaFin_cup" DataFormatString="{0:dd/MM/yyyy}" 
                HeaderText="F.FIN" />
            
                <asp:BoundField DataField="usuario_lho" HeaderText="usuario_lho" 
                Visible="False" />
            <asp:BoundField DataField="habilitado" HeaderText="habilitado" 
                Visible="False" />
            <asp:BoundField DataField="codigo_cpf" HeaderText="codigo_cpf" 
                Visible="False" />
            <asp:TemplateField HeaderText="EDITAR" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:ImageButton ID="btnEditar" runat="server" 
                       CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                       CommandName="EditarLinea"
                       ImageUrl="../../../images/Editar.gif" 
                       ToolTip="Editar Línea Horario"
                       />                                                     
                </ItemTemplate>

<ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:TemplateField>
        </Columns>
        <HeaderStyle BackColor="#D9534F" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Center"
                                        Font-Size="12px" />
    </asp:GridView>
        </div>
        </div>
    
    <div id="Mensaje" runat="server">
        <!--Mensaje a mostrar -->
    </div>
             
    <asp:Panel ID="PanelEditar" runat="server" Visible="false">             
    
    <div class="panel panel-default" id="pnlLista" runat="server">                         
          
        <div class="alert alert-success"> <asp:Label ID="lblEditar" runat="server" Text="Título" style="font-weight: 700"></asp:Label>
                
        <div class="panel-body">
            <div class="row">
            <label for="ddlAmbiente" class="col-md-1 col-form-label">Cambiar Ambiente</label>                                
                                <div class="col-md-11">
             <asp:DropDownList ID="ddlAmbiente" runat="server" CssClass="form-control">
                    </asp:DropDownList>
                    </div>
            </div>
        
            <div class="row">
                  <div class="col-md-3" style="float:left; width:30%">
                        <div class="form-group">
                            <label for="ddlDia" class="col-md-3 col-form-label">Día</label>                                
                                <div class="col-md-9">
                                    <asp:DropDownList ID="ddlDia" runat="server" CssClass="form-control" Width="70px" >
                                    <asp:ListItem Value="LU">LU</asp:ListItem>
                                    <asp:ListItem Value="MA" >MA</asp:ListItem>
                                    <asp:ListItem Value="MI" >MI</asp:ListItem>
                                    <asp:ListItem Value="JU" >JU</asp:ListItem>
                                    <asp:ListItem Value="VI" >VI</asp:ListItem>
                                    <asp:ListItem Value="SA" >SA</asp:ListItem>
                                    </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-3"  style="float:left; width:30%">
                        <div class="form-group">
                            <label for="ddlInicio" class="col-md-3 col-form-label">
                                Hora Inicio</label>
                            <div class="col-md-9">
                                <asp:DropDownList ID="ddlInicio" runat="server" CssClass="form-control">
                                <asp:ListItem>07:00</asp:ListItem>
                                <asp:ListItem>08:00</asp:ListItem>
                                <asp:ListItem>09:00</asp:ListItem>
                                <asp:ListItem>10:00</asp:ListItem>
                                <asp:ListItem>11:00</asp:ListItem>
                                <asp:ListItem>12:00</asp:ListItem>
                                <asp:ListItem>13:00</asp:ListItem>
                                <asp:ListItem>14:00</asp:ListItem>
                                <asp:ListItem>15:00</asp:ListItem>
                                <asp:ListItem>16:00</asp:ListItem>
                                <asp:ListItem>17:00</asp:ListItem>
                                <asp:ListItem>18:00</asp:ListItem>
                                <asp:ListItem>19:00</asp:ListItem>
                                <asp:ListItem>20:00</asp:ListItem>
                                <asp:ListItem>21:00</asp:ListItem>
                                <asp:ListItem>22:00</asp:ListItem>
                                <asp:ListItem>23:00</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                       <div class="col-md-3"  style="float:left; width:30%;">
                        <div class="form-group">
                            <label for="ddlInicio" class="col-md-3 col-form-label">
                                Hora Fin</label>
                            <div class="col-md-9">
                                    <asp:DropDownList ID="ddlFin" runat="server" CssClass="form-control">
                                    <asp:ListItem>07:00</asp:ListItem>
                                    <asp:ListItem>08:00</asp:ListItem>
                                    <asp:ListItem>09:00</asp:ListItem>
                                    <asp:ListItem>10:00</asp:ListItem>
                                    <asp:ListItem>11:00</asp:ListItem>
                                    <asp:ListItem>12:00</asp:ListItem>
                                    <asp:ListItem>13:00</asp:ListItem>
                                    <asp:ListItem>14:00</asp:ListItem>
                                    <asp:ListItem>15:00</asp:ListItem>
                                    <asp:ListItem>16:00</asp:ListItem>
                                    <asp:ListItem>17:00</asp:ListItem>
                                    <asp:ListItem>18:00</asp:ListItem>
                                    <asp:ListItem>19:00</asp:ListItem>
                                    <asp:ListItem>20:00</asp:ListItem>
                                    <asp:ListItem>21:00</asp:ListItem>
                                    <asp:ListItem>22:00</asp:ListItem>
                                    <asp:ListItem>23:00</asp:ListItem>
                                    </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    
            </div>
 
        </div></div>
    </div> 
        <div class="panel-footer">
           <asp:Button ID="btnGuardar" runat="server" Text="Actualizar Horario" CssClass="btn btn-info" /> &nbsp; &nbsp;&nbsp;           
           <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-danger"  />
        </div>
        <br />
         
    
    </asp:Panel>
   
    </form>
</body>
</html>
