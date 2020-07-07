<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ConfigurarEncuesta.aspx.vb" Inherits="academico_encuesta_EncuestaEvaluacionDocente_ConfigurarEncuesta" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8"/>            
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>Evaluación Docente</title>
    <script src="../../../scripts/js/jquery-1.12.3.min.js" type="text/javascript"></script>
    <link href="../../../scripts/css/bootstrap.css" rel="stylesheet" type="text/css" />    
    <script src="../../../scripts/js/bootstrap.min.js" type="text/javascript"></script>    
</head>
<body>
<br />
<br />
    <form id="form1" runat="server">
    <div class="container-fluid" style="width:85%"> 
     
     <div class="panel panel-default" id="pnlLista" runat="server">       
         <div class="panel-heading "><b>Configuración de Evaluación Docente</b></div>       
        
        <div class="panel-body">  
               <div class="row">
                    <div class="form-group">
                        <div class="col-md-6">
                            Seleccionar Semestre Académico
                        </div>
                       <div class="col-md-6">
                        <asp:DropDownList ID="ddlSemestre" CssClass="form-control" runat="server"> </asp:DropDownList>
                       </div>  
                    </div>                                                                                    
                </div>
        </div>
        
        <div class="panel-body">  
            
            <fieldset>
            <legend>1. Cronograma de Evaluación</legend>
            <div class="row">
                <asp:GridView ID="GridView2" runat="server" BackColor="White" 
                    BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                    ForeColor="Black" GridLines="Vertical" DataSourceID="SqlDataSource1" 
                    AutoGenerateColumns="False" DataKeyNames="codigo_cev">
                    <RowStyle BackColor="#F7F7DE" />
                    <Columns>
                        <asp:BoundField DataField="codigo_cev" HeaderText="codigo_cev" 
                            ReadOnly="True" />
                        <asp:BoundField DataField="descripcion_cev" HeaderText="descripcion_cev" 
                            ReadOnly="True" />
                        <asp:BoundField DataField="fechaIni_cev" HeaderText="Desde" DataFormatString="{0:dd/MM/yyyy}"/>
                        <asp:BoundField DataField="fechaFin_cev" HeaderText="Hasta" DataFormatString="{0:dd/MM/yyyy}" />
                        <asp:CommandField ShowEditButton="True" />
                    </Columns>
                    <FooterStyle BackColor="#CCCC99" />
                    <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:CNXBDUSAT %>" 
                        SelectCommand="EVAConfig_CronogramaEval" SelectCommandType="StoredProcedure" UpdateCommand="" UpdateCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="ddlSemestre" Name="codigo_cac" 
                                PropertyName="SelectedValue" Type="Int32" />                            
                        </SelectParameters>
                        <UpdateParameters>
                        
                        </UpdateParameters>
                    </asp:SqlDataSource>
            </div>
            </fieldset>            
            

        </div>    
        
        <div class="panel-body"> 
            <fieldset>
                <legend>2. Rangos de Calificación</legend>      
            </fieldset>  
            <div class="row">
                    <div class="form-group">
                        <div class="col-md-6">
                            Seleccionar Evaluación
                        </div>
                       <div class="col-md-6">
                        <asp:DropDownList ID="ddlTipoEncuesta" CssClass="form-control" runat="server">
                            <asp:ListItem Value="DE">ESTUDIANTES</asp:ListItem>
                            <asp:ListItem Value="DS">ESCUELAS</asp:ListItem>
                            <asp:ListItem Value="DO">DEPARTAMENTOS</asp:ListItem>
                           </asp:DropDownList>
                       </div>  
                    </div>                                                                                    
                </div><br />
            <div class="row">
                    <div class="form-group">
                        <div class="col-md-6">
                            Seleccionar Puntuación
                        </div>
                       <div class="col-md-6">
                        <asp:DropDownList ID="ddlTipoPuntuacion" CssClass="form-control" runat="server" 
                               AutoPostBack="True">
                            <asp:ListItem Value="GE">GENERAL</asp:ListItem>
                            <asp:ListItem Value="COM">POR COMPETENCIAS</asp:ListItem>
                            <asp:ListItem Value="CRI">POR CRITERIOS</asp:ListItem>
                           </asp:DropDownList>
                       </div>  
                    </div>                                                                                    
                </div> 
                <br />  
                   <div class="row">
                    <div class="form-group">
                        <div class="col-md-6">
                            Seleccionar Item
                        </div>
                       <div class="col-md-6">
                        <asp:DropDownList ID="ddlItem" CssClass="form-control" runat="server" 
                               AutoPostBack="True"> </asp:DropDownList>
                       </div>  
                    </div>                                                                                    
                </div>  
                <br /> 
                <br />  
                   <div class="row">
                    <div class="form-group">
                        <div class="col-md-12">                                                   
                                                        &nbsp; &nbsp;&nbsp;
                           
                       </div>  
                    </div>                                                                                    
                </div>  
                <br />        
            <div class="row">
                <asp:GridView ID="dgvItems" runat="server" BackColor="White" 
                    BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                    ForeColor="Black" GridLines="Vertical">
                    <RowStyle BackColor="#F7F7DE" />
                 
                    <FooterStyle BackColor="#CCCC99" />
                    <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
            </div>

        </div>
        
       <div class="panel-body">  
            <fieldset>
                <legend>3. Cronograma de Procesamiento de Resultados</legend>      
            </fieldset>  
           
            
            <div class="row">
                <asp:GridView ID="GridView3" runat="server">
                </asp:GridView>
            </div>

        </div>    
        
        
        </div>
    </div>
    </form>
</body>
</html>
