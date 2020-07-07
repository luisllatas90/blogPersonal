﻿<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PruebaPaginacionJquery.aspx.vb" Inherits="administrativo_pec_PruebaPaginacionJquery" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <!--Referencias para Paginacion. mvillavicencio 31/07/12 -->
    <script src="../js/jquery-1.4.4.min.js" type="text/javascript"></script>
    <script src="../js/jquery.tablesorter.js" type="text/javascript"></script>
    <script src="../js/jquery.tablesorter.pager.js" type="text/javascript"></script> 
    <link href="../themes/blue/style.css" rel="stylesheet" type="text/css" />
    <!-- <script src="../js/jquery-latest.js" type="text/javascript"></script> -->
    <!--------------------------------------------------------------------------->
    
       
      <script type="text/javascript">
          /*$(document).ready(function() {
              $("#miTabla").tablesorter({ sortList: [[0, 0], [1, 0]] });
          }
            );*/

          $(document).ready(function() {
            $("#miTabla")
                .tablesorter({ widthFixed: true, widgets: ['zebra'] })
                .tablesorterPager({ container: $("#pager") });
          }); 

     </script>
      
</head>
<body>
    <div>
        <table class="tablesorter" id="miTabla">
            <thead>
                <tr>
                    <th>Last Name</th> 
                    <th>First Name</th> 
                    <th>Email</th> 
                    <th>Due</th> 
                    <th>Web Site</th> 
                </tr>
            </thead>
            <tbody>
               
                    <tr> 
                        <td>Smith</td> 
                        <td>John</td> 
                        <td>jsmith@gmail.com</td> 
                        <td>$50.00</td> 
                        <td>http://www.jsmith.com</td> 
                    </tr> 
                    <tr> 
                        <td>Bach</td> 
                        <td>Frank</td> 
                        <td>fbach@yahoo.com</td> 
                        <td>$50.00</td> 
                        <td>http://www.frank.com</td> 
                    </tr> 
                    <tr> 
                        <td>Doe</td> 
                        <td>Jason</td> 
                        <td>jdoe@hotmail.com</td> 
                        <td>$100.00</td> 
                        <td>http://www.jdoe.com</td> 
                    </tr> 
                    <tr> 
                        <td>Conway</td> 
                        <td>Tim</td> 
                        <td>tconway@earthlink.net</td> 
                        <td>$50.00</td> 
                        <td>http://www.timconway.com</td> 
                    </tr> 
                    
                    <tr> 
                        <td>Conway</td> 
                        <td>Tim</td> 
                        <td>tconway@earthlink.net</td> 
                        <td>$50.00</td> 
                        <td>http://www.timconway.com</td> 
                    </tr> 
                    
                    <tr> 
                        <td>Conway</td> 
                        <td>Tim</td> 
                        <td>tconway@earthlink.net</td> 
                        <td>$50.00</td> 
                        <td>http://www.timconway.com</td> 
                    </tr> 
                    
                    <tr> 
                        <td>Conway</td> 
                        <td>Tim</td> 
                        <td>tconway@earthlink.net</td> 
                        <td>$50.00</td> 
                        <td>http://www.timconway.com</td> 
                    </tr> 
            </tbody>
            <tfoot>
                <tr>
                    <th>Last Name</th> 
                    <th>First Name</th> 
                    <th>Email</th> 
                    <th>Due</th> 
                    <th>Web Site</th> 
                </tr>
            </tfoot>
        </table>
 
        <div id="pager">
            <form>
            <img src="../images/first.png" class="first"/>
            <img src="../images/prev.png" class="prev"/>
            <input type="text" class="pagedisplay" />
            <img src="../images/next.png" class="next"/>
            <img src="../images/last.png" class="last"/>
                        
            <select class="pagesize">
                <option selected="selected" value="5">5</option>
                <option value="10">10</option>
                <option value="20">20</option>
                <option value="30">30</option>
                <option value="40">40</option>
            </select>
            </form>
        </div>
        
    </div>
</body>
</html>
