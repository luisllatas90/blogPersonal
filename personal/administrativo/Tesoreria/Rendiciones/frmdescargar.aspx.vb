﻿
Partial Class frmdescargar
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim archivo As String
        archivo = Me.Request.QueryString("ruta")
        '---------------------------------------------------------------------------------------------------------------
        'Fecha: 29.10.2012
        'Usuario: dguevara
        'Modificacion: Se modifico el http://www.usat.edu.pe por http://intranet.usat.edu.pe
        '---------------------------------------------------------------------------------------------------------------
        Response.Redirect("../../../../personal/administrativo/Tesoreria/Rendiciones/Archivosderendicion/" & archivo)

    End Sub
End Class
