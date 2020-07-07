<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmConsultaMatEntregados.aspx.vb" Inherits="administrativo_pec2_frmConsultaMatEntregados" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Consultar Entrega de Materiales a Participantes</title>
     <link rel="stylesheet" type="text/css" href="../../../private/estilo.css">
<style>
table { background:#D3E4E5;
 border:1px solid gray;
 border-collapse:collapse;
 color:#fff;
 font:normal 12px verdana, arial, helvetica, sans-serif;
}
caption { border:1px solid #5C443A;
 color:#5C443A;
 font-weight:bold;
 letter-spacing:20px;
 padding:6px 4px 8px 0px;
 text-align:center;
 text-transform:uppercase;
}
td, th { color:#363636;
 padding:.4em; text-align :center ; vertical-align :middle ;
}
tr { border:1px dotted gray;
}
thead th, tfoot th { background:#5C443A;
 color:#FFFFFF;
 padding:3px 10px 3px 10px;
 text-align:center;
 text-transform:uppercase;
}
tbody td a { color:#363636;
 text-decoration:none;
}
tbody td a:visited { color:gray;
 text-decoration:line-through;
}
tbody td a:hover { text-decoration:underline;
}
tbody th a { color:#363636;
 font-weight:normal;
 text-decoration:none;
}
tbody th a:hover { color:#363636;
}
tbody td+td+td+td a { background-image:url('bullet_blue.png');
 background-position:left center;
 background-repeat:no-repeat;
 color:#03476F;
 padding-left:15px;
}
tbody td+td+td+td a:visited { background-image:url('bullet_white.png');
 background-position:left center;
 background-repeat:no-repeat;
}
tbody th, tbody td { text-align:left;
 vertical-align:middle ;
}
tfoot td { background:#5C443A;
 color:#FFFFFF;
 padding-top:3px;
}
.odd { background:#fff;
}
tbody tr:hover { background:#99BCBF;
 border:1px solid #03476F;
 color:#000000;
}


</style>  
     
</head>
<body bgcolor="#f5f5f5">
    <form id="form1" runat="server">
    <div>
    <h3>Consultar Materiales Entregados por Participante</h3>
   
    <asp:DropDownList ID="DropDownList1" runat="server" Height="20px" 
        Visible="False" Width="24px">
    </asp:DropDownList>
    
    <asp:DropDownList ID="DropDownList2" runat="server" Height="20px" Width="600px">
    </asp:DropDownList>
    
    
    <asp:Button ID="Button1" runat="server" Text="Consultar" />
    
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:CNXBDUSAT %>" 
        SelectCommand="EVE_ConsultaMatEntregados" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:Parameter DefaultValue="T" Name="modo" Type="String" />
            <asp:ControlParameter ControlID="DropDownList2" Name="Cco" 
                PropertyName="SelectedValue" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    
        <br /><br />
    
        <asp:Table ID="Table1" runat="server" Height="313px" Width="676px" 
            BorderWidth="2px" GridLines="Both" BackColor="#C7EBFE" BorderStyle="Solid" 
            CellPadding="0" CellSpacing="0" 
            Font-Names="Arial" Font-Size="10px">
        </asp:Table>
        <br />
        <asp:Label ID="totentregado" runat="server"></asp:Label>
        </div>
    </form>
</body>
</html>
