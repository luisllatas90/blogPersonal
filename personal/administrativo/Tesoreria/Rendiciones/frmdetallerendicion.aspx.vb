Imports System.Data

Partial Class frmdetallerendicion

    Inherits System.Web.UI.Page
    Public cn As New clsaccesodatos, dts As New DataSet
    Public Mostrarfinalizar As Boolean
    Public estado_rendicion As String

    Sub mostrarinformacion()
        ' verificar el estado de la rendicio
        Dim dtsrendicion As New DataSet
        cn.abrirconexion()
        Dim dtsdetalleegreso As New DataSet
        ' el detalle del egreso por el cpodigo de rendición
        cn.cerrarconexion()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            'Exit Sub
            If Me.IsPostBack = False Then

                Dim dtsestadorendicion As System.Data.DataSet, EstadoFinalizar As String

                cn.abrirconexion()
                dtsestadorendicion = cn.consultar("TES_ObtenerEstadoRendicion", Request.QueryString("codigo_rend"))
                cn.cerrarconexion()

                EstadoFinalizar = dtsestadorendicion.Tables("consulta").Rows(0).Item("estado_rend")
                If EstadoFinalizar = "P" Then
                    'Response.Write("P")
                    'cmdcancelar.Text = "cerrar 1"

                    Mostrarfinalizar = True
                Else

                    'Response.Write("ok")
                    'cmdcancelar.Text = "cerrar 2"
                    Mostrarfinalizar = False

                    ''Deshabilitar Controles
                    OcultarBoton()
                    '<input id="cmdagregar" style="width: 112px; background-repeat: no-repeat; background-color: lemonchiffon"
                    '    type="button" value="Agregar" language="javascript" onclick="RegistrarDetallerendicion()"
                    '    onclick="return cmdReactivar_onclick()" onclick="return cmdagregar_onclick()" />
                    'ShowMessage("fnAgruparTramitesSelect(): " & ex.Message.Replace("'", ""), MessageType.Error)
                End If
                estado_rendicion = EstadoFinalizar
                mostrarinformacion()
            End If
        Catch ex As Exception
            Response.Write("ERROR: " & ex.ToString)
        End Try

    End Sub

    Protected Sub OcultarBoton()
        Page.RegisterStartupScript("Boton", "<script>OcultarBotones();</script>")
    End Sub


    Protected Sub cmdagregar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        ' ShowDialogModal("urlPopup",window,"Propiedades");

        'Response.Write("<script> window.open('frmregistrardetallerendicion.aspx?codigo_rend=" & Me.hd.Value.ToString.Trim & "','frmadjuntararchivo','toolbar=no,width=980,height=380') </script>")
    End Sub

    Protected Sub lstinformacion_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) ' Handles lstinformacion.RowCreated

    End Sub

    Protected Sub lstinformacion_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) 'Handles lstinformacion.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
            'e.Row.Cells(5).Attributes.Add("onclick", "window.open('frmsubirarchivo.aspx?codigo_dren=" & e.Row.Cells(0).Text.ToString & "&estado=" & estado_rendicion & "','frmadjuntararchivo','toolbar=no,width=850,height=460')")
            e.Row.Attributes.Add("seleccionado", 0)
            'e.Row.Attributes.Add("onclick", "pintarfila(this,'" & Me.lstinformacion.ID.ToString & "');document.all.documentodetalle.src='frmadjuntararchivo.aspx?codigo_dren=" & e.Row.Cells(0).Text.ToString & "&estado=" & estado_rendicion & "'")
            e.Row.ID = e.Row.Cells(0).Text.ToString
            e.Row.Cells(7).Attributes.Add("ALT", "Quitar registro")
            'e.Row.Attributes.Add("onclick", "pintarfila(this)")


            e.Row.Attributes.Add("onmouseenter", "resaltar(this,1)")
            e.Row.Attributes.Add("onmouseleave", "resaltar(this,0)")
            'e.Row.Attributes.Add("onclick", "window.open('frmadjuntararchivo.aspx?codigo_dren=1','as','toolbar=no')")
        End If
    End Sub

    Protected Sub lstinformacion_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) 'Handles lstinformacion.RowDeleting
        Dim rpta As Integer, mensaje As String = ""

        cn.abrirconexiontrans()
        'cn.ejecutar("sp_quitardetallerendicion", False, rpta, mensaje, CInt(Me.lstinformacion.Rows(e.RowIndex).Cells(0).Text), 0, "")
        cn.cerrarconexiontrans()
        mostrarinformacion()
    End Sub

    Protected Sub cmdcancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdcancelar.Click
        Response.Write("<script>window.close(); </script>")
    End Sub

    
    
End Class
