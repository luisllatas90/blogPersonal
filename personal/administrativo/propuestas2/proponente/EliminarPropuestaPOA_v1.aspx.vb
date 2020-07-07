
Partial Class administrativo_propuestas2_proponente_EliminarPropuestaPOA_v1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.QueryString("id") = "" Then
            Response.Redirect("../../../../sinacceso.html")
        End If

        If (IsPostBack = False) Then
            Call wf_cargarEjercicioPresupuestal()
            Call wf_CargarListas()
        End If
    End Sub

    Sub wf_cargarEjercicioPresupuestal()
        Dim dtt As New Data.DataTable
        Dim obj As New ClsConectarDatos

        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        dtt = obj.TraerDataTable("PEI_ListarEjercicioPresupuestal")
        obj.CerrarConexion()

        Me.ddlEjercicio.DataSource = dtt
        Me.ddlEjercicio.DataTextField = "descripcion"
        Me.ddlEjercicio.DataValueField = "codigo"
        Me.ddlEjercicio.DataBind()
        dtt.Dispose()
        obj = Nothing

        Me.ddlEjercicio.SelectedIndex = Me.ddlEjercicio.Items.Count - 1
    End Sub

    Sub wf_CargarListas()
        Try
            Dim dtConsultar As New Data.DataTable
            Dim obj As New ClsConectarDatos

            Dim anio As String = Me.ddlEjercicio.SelectedItem.ToString
            Dim estado As Integer = IIf(Me.ddlEstado.Text = "ACTIVO", 1, 0)

            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            dtConsultar = obj.TraerDataTable("PRP_ListaPropuestasPOA", Request.QueryString("id"), anio, estado, Request.QueryString("ctf"))
            obj.CerrarConexion()

            dgvPropuestas.DataSource = dtConsultar
            dgvPropuestas.DataBind()
            dgvPropuestas.Dispose()
            obj = Nothing

            lbl_numeroItems.Text = dgvPropuestas.Rows.Count.ToString & " REGISTROS ENCONTRADOS"
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub cmdConsultar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdConsultar.Click
        Call wf_CargarListas()
    End Sub

    Protected Sub dgvPropuestas_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles dgvPropuestas.RowDeleting
        Try
            'Realizar una consulta en tabla EliminaPropuesta y verificar si puede Eliminar y si Envia Correo de Confirmación de Eliminación de Propuesta
            
            Dim Ndia As String = ""
            Dim dia As String = ""
            Dim mes As String
            Dim anio As String

            Ndia = WeekdayName(Weekday(Now))
            dia = Day(Now)
            mes = MonthName(Month(Now()))
            anio = Year(Now())
            Dim fechaLarga As String = Ndia & ", " & dia & " de " & mes & " del " & anio

            Dim dtt As New Data.DataTable
            Dim obj As New ClsConectarDatos
            Dim codigo_per As String = Request.QueryString("id")
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            dtt = obj.TraerDataTable("PRP_RegistrarEliminaPropuestas", "0", codigo_per, "0", "0", "0", "0", 0, "E")
            obj.CerrarConexion()

            If dtt.Rows.Count > 0 Then
                Dim objemail As New ClsMail
                Dim dt As New Data.DataTable
                Dim cuerpo As String = ""
                Dim receptor As String = ""
                Dim AsuntoCorreo As String = ""
                For i As Integer = 0 To dtt.Rows.Count - 1
                    If dtt.Rows(i).Item("tipo").ToString = 0 Then
                        receptor = dtt.Rows(i).Item("mail").ToString()
                    Else
                        If i = 0 Then
                            receptor = dtt.Rows(i).Item("mail").ToString
                        Else
                            receptor = receptor + ", " + dtt.Rows(i).Item("mail").ToString
                        End If
                    End If
                Next

                '=============== ENVIAR CORREO ===============================
                Dim persona As String = dtt.Rows(0).Item("persona").ToString
                Dim Propuesta As String = Me.dgvPropuestas.Rows(e.RowIndex).Cells(1).Text()

                cuerpo = "<html>"
                cuerpo = cuerpo & "<head>"
                cuerpo = cuerpo & "<title></title>"
                cuerpo = cuerpo & "<style>"
                cuerpo = cuerpo & ".FontType{font-family: calibri; font-size:12px; }"
                cuerpo = cuerpo & "</style>"
                cuerpo = cuerpo & "</head>"
                cuerpo = cuerpo & "<body>"
                cuerpo = cuerpo & "<table border=0 cellpadding=0 class=""FontType"">"
                cuerpo = cuerpo & "<tr><td colspan=2 ><b>Estimado(a):</b></td></tr>"
                cuerpo = cuerpo & "<tr><td colspan=2></br></br>El presente es para informarle que el día de hoy " & fechaLarga & " la Propuesta N° ID : <b>" & Propuesta & "</b>, HA SIDO ELIMINADA por autorización de Dirección de Planificación.</td></tr>"
                cuerpo = cuerpo & "<tr><td colspan=2></br></br>Saludos(Cordiales)</td></tr>"
                cuerpo = cuerpo & "</table>"
                cuerpo = cuerpo & "</body>"
                cuerpo = cuerpo & "</html>"

                receptor = dtt.Rows(0).Item("mail").ToString '"cfarfan@usat.edu.pe"
                AsuntoCorreo = "[ Propuesta Eliminada ]"
                objemail.EnviarMail("campusvirtual@usat.edu.pe", "Propuesta Eliminada", receptor, AsuntoCorreo, cuerpo, True)
                '=============== FIN ENVIAR CORREO ===============================

                Dim mensaje As String = "<h3 style=" & "color:blue;" & ">Mensaje Enviado con Exito</h3>"
                Response.Write(mensaje)

                If dtt.Rows(0).Item("elimina_elp") = 1 Then
                    obj.AbrirConexion()
                    obj.Ejecutar("PRP_EliminarPropuestaPOA_v1", txtelegido.Value, Request.QueryString("id"))
                    obj.CerrarConexion()
                End If
            End If
            Call wf_CargarListas()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub dgvPropuestas_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles dgvPropuestas.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim fila As Data.DataRowView
                fila = e.Row.DataItem
                e.Row.Attributes.Add("id", "" & fila.Row("codigo_prp").ToString & "")
                e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
                e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
                e.Row.Attributes.Add("OnClick", "HabilitarBoton('M',this)")
                e.Row.Attributes.Add("Class", "Sel")
                e.Row.Attributes.Add("Typ", "Sel")
            End If

            If Me.ddlEstado.Text <> "ACTIVO" Then
                e.Row.Cells(4).Text = ""
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub dgvPropuestas_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvPropuestas.SelectedIndexChanged

    End Sub
End Class
