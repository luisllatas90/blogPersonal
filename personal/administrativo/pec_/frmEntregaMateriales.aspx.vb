Partial Class personal_administrativo_pec2_frmEntregaMateriales
    Inherits System.Web.UI.Page
    Dim wcecos As Integer
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        iniciar()
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        ' este boton realiza se presiona cuando se tiene marcada la fecha, los materiales y el DNI o doc ID del
        ' participante, es el submit del form
        Label1.Text = ""
        If DropDownList2.SelectedValue = "DNI" Then
            Me.gvParticipante.DataBind()
            If (Me.gvParticipante.Rows.Count > 0) Then
                If Val(Me.gvParticipante.Rows(0).Cells(3).Text) > 0 Then
                    Response.Write("<SCRIPT LANGUAGE=""JavaScript"">alert('Tiene deuda no se registro')</SCRIPT>")
                    Label1.Text = "tiene deuda no se registro"
                    TextBox2.Focus()
                    Exit Sub
                Else
                    RegistrarSeleccionados()
                End If
            Else
                Response.Write("<SCRIPT LANGUAGE=""JavaScript"">alert('DNI no Existe o mal digitado')</SCRIPT>")
                Label1.Text = "DNI no existe o esta mal ingresado"
            End If
            TextBox3.Focus()
        End If
    End Sub

    Protected Sub CustomValidator1_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles CustomValidator1.ServerValidate
        'Validador de argumentos de DNI via control ASP NET
        'Obtiene valor inicial de true
        Dim Valor As Boolean = True
        Try
            'obtener el booleano para ver si el DNI es true o false
            Valor = validaDNI(TextBox2.Text)
            If Valor Then
                args.IsValid = True
            Else
                args.IsValid = False
            End If
        Catch
            args.IsValid = False
            Exit Sub
        End Try
    End Sub

    Protected Sub Calendar1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Calendar1.SelectionChanged
        ' cuando se selecciona una fecha distinta en el calendario se vuelve a llenar el
        ' grupo de controles CheckBoxList1
        llenar_checkboxlist()
    End Sub

    Protected Sub DropDownList4_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDownList4.SelectedIndexChanged
        'Cuando se elige un evento distinto se vuelve a reiniciar todo el formulario
        ' para empezar de cero
        iniciar()
    End Sub

    Protected Sub iniciar()
        'Ocurre cuando se inicia el form y cuando se cambia el evento
        If Not IsPostBack Then
            Dim objfun As New ClsFunciones
            Dim obj As New ClsConectarDatos
            Dim tablacecos As New System.Data.DataTable
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            tablacecos = obj.TraerDataTable("EVE_ConsultarCentroCostosXPermisos", Request.QueryString("ctf"), Request.QueryString("id"), "", Request.QueryString("mod"))
            objfun.CargarListas(DropDownList4, tablacecos, "codigo_Cco", "Nombre", ">> Seleccione<<")
            obj.CerrarConexion()
            CheckBoxList1.Items.Clear()
            llenar_checkboxlist()
            obj = Nothing
            objfun = Nothing
        End If
    End Sub
    Private Sub llenar_checkboxlist()
        ' Se llena cuando se selecciona un evento y se selecciona una fecha del calendario
        Dim obj As New ClsConectarDatos
        Dim tabla As New System.Data.DataTable
        Dim i As Integer
        CheckBoxList1.Visible = False
        CheckBoxList1.Items.Clear()
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        tabla = obj.TraerDataTable("EVE_ConsultarMaterialesEvento", Me.DropDownList4.SelectedValue, Me.Calendar1.SelectedDate, "T")
        If tabla.Rows.Count > 0 Then
            CheckBoxList1.Visible = True
            For i = 0 To (tabla.Rows.Count) - 1
                CheckBoxList1.Items.Add("chklst" & Format(i, "000"))
                CheckBoxList1.Items(i).Text = tabla.Rows(i).Item(1).ToString
                CheckBoxList1.Items(i).Value = tabla.Rows(i).Item(2)
            Next
        Else
            CheckBoxList1.Visible = False
        End If
        obj.CerrarConexion()
        obj = Nothing
        act_fecha()
    End Sub

    Private Sub act_fecha()
        ' Marca con true o false los checkboxs segun la fecha seleccionada
        ' que consulta que materiales por defecto se entregaran el dia seleccionado
        If CheckBoxList1.Items.Count > 0 Then
            Dim i As Integer

            For i = 0 To CheckBoxList1.Items.Count - 1
                If CheckBoxList1.Items(i).Value = 1 Then
                    CheckBoxList1.Items(i).Selected = True
                Else
                    CheckBoxList1.Items(i).Selected = False
                End If
            Next

        End If
    End Sub
    Private Sub RegistrarSeleccionados()
        ' ocurre al presionar el boton guardar
        Dim objfun As New ClsFunciones
        Dim obj As New ClsConectarDatos
        Dim TablaMaterialesEvento As New System.Data.DataTable
        Dim Mat_ya_Entregados As New System.Data.DataTable
        Dim wcodigo_pso As String
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        TablaMaterialesEvento = obj.TraerDataTable("EVE_ConsultarMaterialesEvento", Me.DropDownList4.SelectedValue, Me.Calendar1.SelectedDate, "T")
        Dim i As Integer
        wcodigo_pso = Me.gvParticipante.Rows(0).Cells(4).Text
        Mat_ya_Entregados = obj.TraerDataTable("EVE_AgregarEntregaMaterial", 0, wcodigo_pso, "", "V")

        Dim mensaje_advertencia As Boolean = False
        For i = 0 To TablaMaterialesEvento.Rows.Count - 1
            Response.Write(checkboxlist1.items(i).value)
            Response.Write(checkboxlist1.items(i).selected & "<-sele ")
            If CheckBoxList1.Items(i).Value = 1 Then
                Dim j As Integer
                Dim si_guardar As Boolean = True
                Dim porentregar_codigo_mev As Integer = CInt(TablaMaterialesEvento.Rows(i).Item(4).ToString)

                Response.Write(porentregar_codigo_mev.ToString & "--")
                For j = 1 To Mat_ya_Entregados.Rows.Count
                    If Mat_ya_Entregados.Rows(j - 1).Item(0) = porentregar_codigo_mev Then
                        si_guardar = False
                        mensaje_advertencia = True
                    End If
                Next
                If si_guardar Then
                    guarda_material_participante(CInt(TablaMaterialesEvento.Rows(i).Item(4).ToString), CInt(wcodigo_pso), Me.TextBox3.Text)
                End If
            End If
        Next
        If mensaje_advertencia Then
            Response.Write("<SCRIPT LANGUAGE=""JavaScript"">alert('Algunos Materiales ya habian sido registrados')</SCRIPT>")
            mensaje_advertencia = False
        End If
        Mat_ya_Entregados.Dispose()
        TablaMaterialesEvento.Dispose()
        obj.CerrarConexion()
        obj = Nothing
    End Sub
    Protected Sub guarda_material_participante(ByVal eve_codigo_mev As Integer, ByVal eve_codigo_pso As Integer, ByVal eve_observaciones_emat As String)
        ' registra en la base de datos los materiales entregados a los participantes
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Try
            obj.AbrirConexion()
            obj.Ejecutar("EVE_AgregarEntregaMaterial", eve_codigo_mev, eve_codigo_pso, eve_observaciones_emat, "I")
            obj.CerrarConexion()
            Response.Write("<SCRIPT LANGUAGE=""JavaScript"">alert('Registro Guardado')</SCRIPT>")
            'LimpiaControles()
        Catch ex As Exception
            Response.Write("<SCRIPT LANGUAGE=""JavaScript"">alert('Error al guardar')</SCRIPT>")
            obj.CerrarConexion()
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Function validaDNI(ByVal mydni As String) As Boolean
        ' Valida si un DNI es correcto ya que debe tener 8 digitos por fuerza
        ' y no contener ningun caracter extraño
        Dim cadena As String = "0123456789"
        If mydni.Length <> 8 Then
            Return False
        End If
        Dim i As Integer
        For i = 1 To 8
            If cadena.IndexOf(Mid(mydni, i, 1)) < 0 Then
                Return False
                Exit For
            End If
        Next
        Return True
    End Function

   
    Protected Sub CheckBoxList1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxList1.SelectedIndexChanged
        'act_fecha()
        If (CheckBoxList1.SelectedIndex <> -1) Then
            Response.Write(CheckBoxList1.SelectedItem.Value.tostring)
        End If

    End Sub

    Protected Sub Button5_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button5.Click
        'Response.Write("<script type= 'text/javascript'>document.write(arraymarcados());</script>")

    End Sub
    
End Class
