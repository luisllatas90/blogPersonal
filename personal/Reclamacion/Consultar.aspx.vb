Imports System.Collections.Generic
Imports System.Net
Imports System.IO
Imports System.Xml
'Imports iTextSharp.text
'Imports iTextSharp.text.html.simpleparser
'Imports iTextSharp.text.pdf

Partial Class Consultar
    Inherits System.Web.UI.Page
    Private C As ClsConectarDatos
    Private cod As String = ""
    Private per As String = ""
    Private sol As String = ""
    Private ped As String = ""
    Private nom As String = ""
    
    Sub New()
        If C Is Nothing Then
            C = New ClsConectarDatos
            C.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'MostrarDetalle("01/01/2000", "01/01/2000", "%", "%")
            btnBuscar_Click(sender, e)
        Else
            'btnBuscar_Click(sender, e)
            RefreshGrid()
        End If
    End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        Try
            Dim desde As String = Request.Form("dtpDesde")
            Dim hasta As String = Request.Form("dtpHasta")
            Dim filtro As String = Trim(Request.Form("txtFiltro") & "%")
            Dim estado As String = Trim(Request.Form("cboEstado"))
            Dim fecha As Date = Date.Today
            Dim fechaFinal As Date

            If String.IsNullOrEmpty(desde) Then
                fechaFinal = New Date(fecha.Year, fecha.Month, 1)
                desde = obtenerFecha(fechaFinal)
            End If

            If String.IsNullOrEmpty(hasta) Then
                fechaFinal = New Date(fecha.Year, fecha.Month, Date.DaysInMonth(fecha.Year, fecha.Month))
                hasta = obtenerFecha(fechaFinal)
            End If

            If String.IsNullOrEmpty(filtro) Then filtro = "%"
            If String.IsNullOrEmpty(estado) Then estado = "%"

            'Response.Write("<script> alert('" & desde & " - " & hasta & " - " & filtro & " - " & estado & "'); </script>")
            MostrarDetalle(desde, hasta, filtro, estado)
        Catch ex As Exception
            Response.Write("<script> alert('" & ex.Message & "'); </script>")
        End Try
    End Sub

    Public Sub btnRead_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim tipo, bien, nomb, ndoc, correo, telf, solucion, domicilio, apoderado, monto, descripcion As String
            Dim button As HtmlButton = DirectCast(sender, HtmlButton)
            cod = button.Attributes("codigo")
            tipo = button.Attributes("tipo")
            tipo = tipo.Split("/")(0).Trim().Substring(0, 1)
            bien = button.Attributes("tipo")
            bien = bien.Split("/")(1).Trim().Substring(0, 1)
            nomb = button.Attributes("nomb")
            ndoc = button.Attributes("ndoc")
            correo = button.Attributes("mail")
            correo = correo.Split("/")(0).Trim()
            telf = button.Attributes("mail")
            telf = telf.Split("/")(1).Trim()
            ped = button.Attributes("pedido")
            correo = button.Attributes("mail")
            correo = correo.Split("/")(0).Trim()
            solucion = button.Attributes("solucion")
            domicilio = button.Attributes("domicilio")
            apoderado = button.Attributes("apoderado")
            monto = button.Attributes("monto")
            descripcion = button.Attributes("descripcion")

            ClientScript.RegisterStartupScript(Me.GetType, "Pop", "<script>abrirModalDetalle('" & cod & "', '" & nomb & "', '" & ndoc & "', '" & telf & "', '" & correo & "', '" & domicilio & "', '" & apoderado & "', '" & bien & "', '" & monto & "', " & escapeString(descripcion) & ", '" & tipo & "', " & escapeString(ped) & ", " & escapeString(solucion) & ");</script>")
        Catch ex As Exception
            Response.Write("<script> alert('" & ex.Message & "'); </script>")
        End Try
    End Sub

    Public Sub btnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim tipo, bien, nomb, ndoc, correo, telf, estado, solucion, atendido, fecha, domicilio As String
            Dim button As HtmlButton = DirectCast(sender, HtmlButton)
            Dim ref As String = ""

            cod = button.Attributes("codigo")
            ped = button.Attributes("pedido")
            tipo = button.Attributes("tipo")
            tipo = tipo.Split("/")(0).Trim().Substring(0, 1)
            bien = button.Attributes("tipo")
            bien = bien.Split("/")(1).Trim().Substring(0, 1)
            nomb = button.Attributes("nomb")
            ndoc = button.Attributes("ndoc")
            correo = button.Attributes("mail")
            correo = correo.Split("/")(0).Trim()
            telf = button.Attributes("mail")
            telf = telf.Split("/")(1).Trim()
            estado = button.Attributes("estado")
            solucion = button.Attributes("solucion")
            atendido = button.Attributes("atendido")
            domicilio = button.Attributes("domicilio")
            fecha = button.Attributes("fecha")

            If estado.Equals("ATENDIDO") Then
                ref = "PDF.aspx?"
                ref += "acc=ATENDIDO&"
                ref += "cod=" & cod & "&"
                ref += "nom=" & nomb & "&"
                ref += "dir=" & domicilio & "&"
                ref += "mai=" & correo & "&"
                ref += "sol=" & escapeString(solucion) & "&"
                ref += "ate=" & atendido & "&"
                ref += "fec=" & fecha & "&"
                ref += "env=NO"

                If Not String.IsNullOrEmpty(cod) Then
                    Response.Redirect(ref)
                End If

                ClientScript.RegisterStartupScript(Me.GetType, "Pop", "<script>cerrarModalAtender();</script>")
            Else
                ref = "PDF.aspx?"
                ref += "acc=NAN&"
                ref += "cod=" & cod & "&"
                ref += "dni=" & ndoc & "&"
                ref += "nom=" & nomb & "&"
                ref += "tel=" & telf & "&"
                ref += "mai=" & correo & "&"
                ref += "dir=" & domicilio & "&"
                ref += "ped=" & escapeString(ped) & "&"
                ref += "tip=" & tipo & "&"
                ref += "tbc=" & bien

                If Not String.IsNullOrEmpty(cod) Then
                    Response.Redirect(ref)
                End If
            End If
        Catch ex As Exception
            Response.Write("<script> alert('" & ex.Message & "'); </script>")
        End Try
    End Sub

    Public Sub btnSend_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            llenarCombo()
            Dim button As HtmlButton = DirectCast(sender, HtmlButton)
            cod = button.Attributes("codigo")

            ClientScript.RegisterStartupScript(Me.GetType, "Pop", "<script>abrirModalAsignar('" & cod & "');</script>")
        Catch ex As Exception
            Response.Write("<script> alert('" & ex.Message & "'); </script>")
        End Try
    End Sub

    Public Sub btnCheck_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim id, correo, domicilio As String
            Dim button As HtmlButton = DirectCast(sender, HtmlButton)
            id = button.ID
            cod = button.Attributes("codigo")
            per = button.Attributes("personal")
            ped = button.Attributes("pedido")
            sol = button.Attributes("solucion")
            nom = button.Attributes("nomb")
            correo = button.Attributes("mail")
            correo = correo.Split("/")(0).Trim()
            domicilio = button.Attributes("domicilio")
            Me.txtMail.Value = correo
            Me.txtDir.Value = domicilio
            Me.txtFecha.Value = button.Attributes("fecha")

            'Obtener el código de la sesión del personal
            If String.IsNullOrEmpty(per) Or per.Equals("0") Then per = Session("id_per")
            If String.IsNullOrEmpty(ped) Then ped = ""
            If String.IsNullOrEmpty(sol) Then sol = ""

            ClientScript.RegisterStartupScript(Me.GetType, "Pop", "<script>abrirModalAtender('" & cod & "', '" & per & "', " & escapeString(ped) & ", " & escapeString(sol) & ", '" & nom & "', '" & id & "');</script>")
        Catch ex As Exception
            Response.Write("<script> alert('" & ex.Message & "'); </script>")
        End Try
    End Sub

    Public Sub btnGuardar_click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim domicilio, correo, fecha As String
            cod = Trim(Me.txtCod.Value)
            per = Trim(Me.txtPer.Value)
            sol = Trim(Me.txtSol.Text)
            nom = Trim(Me.txtNom.Value)
            domicilio = Trim(Me.txtDir.Value)
            correo = Trim(Me.txtMail.Value)
            fecha = Trim(Me.txtFecha.Value)

            If String.IsNullOrEmpty(per) Or per.Equals("0") Then per = Request.QueryString("id")

            C.AbrirConexion()
            If C.Ejecutar("ACT_LibroReclamos", cod, per, sol, "S") > 0 Then
                'Response.Write("<script> alert('Los datos fueron actualizados con éxito'); </script>")

                Dim atendido, mes, ref As String
                mes = CDate(Date.Today).ToString("MMMM").ToString
                mes = mes.Substring(0, 1).ToUpper & mes.Substring(1).ToLower
                atendido = CDate(Date.Today).ToString("dd").ToString & " de " & mes & " de " & Date.Today.Year.ToString

                ref = "PDF.aspx?"
                ref += "acc=ATENDIDO&"
                ref += "cod=" & cod & "&"
                ref += "nom=" & nom & "&"
                ref += "dir=" & domicilio & "&"
                ref += "mai=" & correo & "&"
                ref += "sol=" & escapeString(sol) & "&"
                ref += "ate=" & atendido & "&"
                ref += "fec=" & fecha & "&"
                ref += "env=SI"

                If Not String.IsNullOrEmpty(cod) Then
                    Response.Redirect(ref)

                    Me.btnBuscar.Focus()
                    'btnBuscar_Click(sender, e)
                End If
            Else
                Response.Write("<script> alert('No se pudo completar la operación, intente nuevamente'); </script>")
                'btnBuscar_Click(sender, e)
            End If
        Catch ex As Exception
            Response.Write("<script> alert('" & ex.Message & "'); </script>")
        End Try
    End Sub

    Public Sub btnAsignar_click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            cod = Trim(Me.txtCod.Value)
            per = Trim(Me.ddlPersonal.SelectedValue.ToString())

            C.AbrirConexion()
            If C.Ejecutar("ACT_LibroReclamos", cod, per, "", "N") > 0 Then
                Response.Write("<script> alert('El reclamo fue asignado con éxito'); </script>")
            Else
                Response.Write("<script> alert('No se pudo completar la operación, intente nuevamente'); </script>")
            End If

            btnBuscar_Click(sender, e)
        Catch ex As Exception
            Response.Write("<script> alert('" & ex.Message & "'); </script>")
        End Try
    End Sub

    Protected Sub gvReclamos_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvReclamos.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim celda As TableCellCollection = e.Row.Cells
            Dim codigo As String = Me.gvReclamos.DataKeys(e.Row.RowIndex).Values.Item("codigo")
            Dim personal As String = Me.gvReclamos.DataKeys(e.Row.RowIndex).Values.Item("personal")
            Dim pedido As String = Me.gvReclamos.DataKeys(e.Row.RowIndex).Values.Item("pedido")
            Dim solucion As String = Me.gvReclamos.DataKeys(e.Row.RowIndex).Values.Item("solucion")
            Dim atendido As String = Me.gvReclamos.DataKeys(e.Row.RowIndex).Values.Item("atendido")
            Dim domicilio As String = Me.gvReclamos.DataKeys(e.Row.RowIndex).Values.Item("domicilio")
            Dim apoderado As String = Me.gvReclamos.DataKeys(e.Row.RowIndex).Values.Item("apoderado")
            Dim monto As String = Me.gvReclamos.DataKeys(e.Row.RowIndex).Values.Item("monto")
            Dim descripcion As String = Me.gvReclamos.DataKeys(e.Row.RowIndex).Values.Item("descripcion")
            Dim idx As Integer = e.Row.RowIndex + 1

            If String.IsNullOrEmpty(personal) Then personal = "0"
            If String.IsNullOrEmpty(pedido) Then pedido = ""
            If String.IsNullOrEmpty(solucion) Then solucion = ""
            If String.IsNullOrEmpty(atendido) Then atendido = ""

            'Cambiar a color ROJO cuando los días transcurridos superen los 20 
            If Not e.Row.Cells(6).Text.Trim().Equals("ATENDIDO") And CInt(e.Row.Cells(7).Text.ToString()) >= 20 Then
                e.Row.ForeColor = System.Drawing.Color.Red
            End If

            'Leer el reclamo
            Dim btnRead As New HtmlButton()
            With btnRead
                .ID = "btnRead" & idx
                .Attributes.Add("codigo", codigo)
                .Attributes.Add("ndoc", e.Row.Cells(2).Text.Trim())
                .Attributes.Add("nomb", e.Row.Cells(3).Text.Trim())
                .Attributes.Add("mail", e.Row.Cells(4).Text.Trim())
                .Attributes.Add("tipo", e.Row.Cells(5).Text.Trim())
                .Attributes.Add("domicilio", domicilio)
                .Attributes.Add("pedido", pedido)
                .Attributes.Add("solucion", solucion)
                .Attributes.Add("apoderado", apoderado)
                .Attributes.Add("monto", monto)
                .Attributes.Add("descripcion", descripcion)
                .Attributes.Add("class", "btn btn-info btn-sm")
                .Attributes.Add("type", "button")
                .Attributes.Add("title", "Leer caso")
                .InnerHtml = "<i class='fa fa-book' title='Leer caso'></i>"

                AddHandler .ServerClick, AddressOf btnRead_Click
            End With
            celda(13).Controls.Add(btnRead)

            'Imprimir reclamo
            Dim btnPrint As New HtmlButton
            With btnPrint
                .ID = "btnPrint" & idx
                .Attributes.Add("codigo", codigo)
                .Attributes.Add("ndoc", e.Row.Cells(2).Text.Trim())
                .Attributes.Add("nomb", e.Row.Cells(3).Text.Trim())
                .Attributes.Add("mail", e.Row.Cells(4).Text.Trim())
                .Attributes.Add("tipo", e.Row.Cells(5).Text.Trim())
                .Attributes.Add("estado", e.Row.Cells(6).Text.Trim())
                .Attributes.Add("domicilio", domicilio)
                .Attributes.Add("pedido", pedido)
                .Attributes.Add("solucion", solucion)
                .Attributes.Add("atendido", atendido)
                .Attributes.Add("fecha", e.Row.Cells(1).Text.Trim())
                .Attributes.Add("class", "btn btn-warning btn-sm")
                .Attributes.Add("type", "button")
                .Attributes.Add("title", "Imprimir caso")
                .InnerHtml = "<i class='fa fa-print' title='Imprimir caso'></i>"

                AddHandler .ServerClick, AddressOf btnPrint_Click
            End With
            celda(14).Controls.Add(btnPrint)

            'Asignar usuario que solucionará el reclamo
            Dim btnSend As New HtmlButton
            With btnSend
                .ID = "btnSend" & idx
                .Attributes.Add("codigo", codigo)
                .Attributes.Add("personal", personal)
                .Attributes.Add("atendido", atendido)
                .Attributes.Add("class", "btn btn-primary btn-sm")
                .Attributes.Add("type", "button")
                .Attributes.Add("title", "Asignar personal")
                .InnerHtml = "<i class='fa fa-share-square' title='Asignar personal'></i>"

                ' Si ya fue asignado el personal, cambiar el estado del botón de envío
                If Not personal.Equals("0") Then
                    .Attributes.Add("disabled", True)
                Else
                    .Attributes.Remove("disabled")
                End If

                ' Si ya está atendido, no se debe asignar a ningún personal
                If e.Row.Cells(6).Text.Trim().Equals("ATENDIDO") Then
                    .Attributes.Add("disabled", True)
                Else
                    .Attributes.Remove("disabled")
                End If

                AddHandler .ServerClick, AddressOf btnSend_Click
            End With
            celda(15).Controls.Add(btnSend)

            'Confirmar atención del reclamo
            Dim btnCheck As New HtmlButton
            With btnCheck
                .ID = "btnCheck" & idx
                .Attributes.Add("codigo", codigo)
                .Attributes.Add("personal", personal)
                .Attributes.Add("pedido", pedido)
                .Attributes.Add("solucion", solucion)
                .Attributes.Add("nomb", e.Row.Cells(3).Text.Trim())
                .Attributes.Add("mail", e.Row.Cells(4).Text.Trim())
                .Attributes.Add("domicilio", domicilio)
                .Attributes.Add("fecha", e.Row.Cells(1).Text.Trim())
                .Attributes.Add("class", "btn btn-success btn-sm")
                .Attributes.Add("type", "button")
                .Attributes.Add("title", "Atender caso")
                .InnerHtml = "<i class='fa fa-check-square' title='Atender caso'></i>"

                ' Si ya está atendido, cambiar el estado del botón de atención
                If e.Row.Cells(6).Text.Trim().Equals("ATENDIDO") Then
                    .Attributes.Add("disabled", True)
                Else
                    .Attributes.Remove("disabled")
                End If

                AddHandler .ServerClick, AddressOf btnCheck_Click
            End With
            celda(16).Controls.Add(btnCheck)

            gvReclamos.HeaderRow.TableSection = TableRowSection.TableHeader
        End If
    End Sub

    Private Sub MostrarDetalle(ByVal desde As String, ByVal hasta As String, ByVal filtro As String, ByVal estado As String)
        Try
            Dim dt As New Data.DataTable("LibroReclamos")
            C.AbrirConexion()
            dt = C.TraerDataTable("LST_LibroReclamos", desde, hasta, filtro, estado)

            gvReclamos.DataSource = dt
            gvReclamos.DataBind()
            dt.dispose()
        Catch ex As Exception
            Response.Write("<script> alert('" & ex.Message & "'); </script>")
        End Try
    End Sub

    Private Function obtenerFecha(ByVal fecha As Date) As String
        Dim dia, mes As String
        dia = fecha.Day.ToString.PadLeft(2, "0")
        dia = dia.Substring(dia.Length - 2)
        mes = fecha.Month.ToString.PadLeft(2, "0")
        mes = mes.Substring(mes.Length - 2)

        Return dia & "/" & mes & "/" & fecha.Year.ToString
    End Function

    Private Sub llenarCombo()
        Try
            If ddlPersonal.Items.Count <= 0 Then
                Dim dt As New Data.DataTable("personal")
                C.AbrirConexion()
                dt = C.TraerDataTable("LST_PersonalLibroReclamos", "%")

                ddlPersonal.DataSource = dt
                ddlPersonal.DataTextField = "personal"
                ddlPersonal.DataValueField = "codigo"
                ddlPersonal.DataBind()
                dt.Dispose()
            End If
        Catch ex As Exception
            Response.Write("<script> alert('" & ex.Message & "'); </script>")
        End Try
    End Sub

    Private Function escapeString(ByVal cad As String) As String
        Dim res As String = ""

        Try
            Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
            res = serializer.Serialize(cad)
        Catch ex As Exception
            res = ""
        End Try

        Return res
    End Function

    'Este método me permite llamar manualmente al evento RowDataBound que vuelve a reenderizar los botones de acción
    Private Sub RefreshGrid()
        For Each _Row As GridViewRow In gvReclamos.Rows
            gvReclamos_RowDataBound(gvReclamos, New GridViewRowEventArgs(_Row))
        Next
    End Sub

End Class
