Partial Class academico_cargalectiva_frmprogramacursosbloques
    Inherits System.Web.UI.Page

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.lblMensaje.Text = ""
        Try
            If Session("XCodigoCup") IsNot Nothing And Session("cpTotalHoras") IsNot Nothing Then

                If Me.txtNroHoras.Text.Trim = "" Or Me.txtNroHoras.Text.Trim.Length = 0 Then
                    Me.lblMensaje.Text = "Ingrese la cantidad de horas a asignar"
                    Exit Sub
                Else
                    Me.lblMensaje.Text = ""
                End If
                Dim obj As New ClsConectarDatos
                obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString

                Dim maxh As Integer = 0
                Dim hasig As Integer = 0
                Me.hdTotalHoras.Value = CInt(Session("cpTotalHoras"))

                obj.AbrirConexion()
                hasig = obj.TraerDataTable("HConsultar_cursoProgramadoBloquesR", Session("XCodigoCup")).Rows(0).Item("horas").ToString

                maxh = CInt(Me.hdTotalHoras.Value) - hasig
                If CInt(Me.txtNroHoras.Text) > maxh Then
                    Me.lblMensaje.Text = "Aviso: No puede sobrepasar el límite de horas por curso. ( Max. " & Me.hdTotalHoras.Value & " horas) "
                Else
                    Me.lblMensaje.Text = ""
                    obj.AbrirConexion()
                    obj.Ejecutar("Hregistrar_cursoProgramadoBloques_Registrar", Session("XCodigoCup"), CInt(Me.txtNroHoras.Text), 0)

                    obj.CerrarConexion()
                    Me.gvBloque.DataBind()
                    obj = Nothing
                End If
            End If

        Catch ex As Exception
            Me.lblMensaje.Text = ex.message '= "Ha ocurrido un error" & "-" & Session("XCodigoCup") & "-" & CInt(Me.txtNroHoras.Text)
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.lblCurso.text = Session("cpCurso")
    End Sub
End Class
