<%@ Application Language="VB" %>

<script runat="server">

    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Código que se ejecuta al iniciarse la aplicación
    End Sub
    
    Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Código que se ejecuta durante el cierre de aplicaciones
    End Sub
        
    Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
        ' Código que se ejecuta al producirse un error no controlado
        
        'Falta: Crear sesion del error para que la de mensajes lo muestre
        Response.Write("<label onclick='javascript:history.back()' style='color:#003399; text-decoration:underline; cursor:hand' > Ha ocurrido un error interno, favor volver a intenar: <== RETORNAR</label>")
        
    End Sub

    Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Código que se ejecuta cuando se inicia una nueva sesión
    End Sub

    Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Código que se ejecuta cuando finaliza una sesión. 
        ' Nota: El evento Session_End se desencadena sólo con el modo sessionstate
        ' se establece como InProc en el archivo Web.config. Si el modo de sesión se establece como StateServer 
        ' o SQLServer, el evento no se genera.
    End Sub
       
</script>