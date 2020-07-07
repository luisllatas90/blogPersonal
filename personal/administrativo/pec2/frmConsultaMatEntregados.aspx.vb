
Partial Class administrativo_pec2_frmConsultaMatEntregados
    Inherits System.Web.UI.Page

    Dim cabeceranombre(100), cabeceravalor(100) As String
    Dim lstparticipantes(5000) As String
    Dim lstparticipantes_codigo_pso(5000) As Integer
    Dim arrayval(5000, 3) As Integer
    Dim ncols As Integer
    Dim nrows As Integer
    Dim entregado_participante As Integer = 0
    Dim entregado_total As Integer = 0
    Dim total_participantes As Integer = 0
    Dim por_entregar As Integer = 0
    Dim por_entregar_total As Integer = 0
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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
            objfun.CargarListas(DropDownList2, tablacecos, "codigo_Cco", "Nombre", ">> Seleccione<<")
            obj.CerrarConexion()
            obj = Nothing
            objfun = Nothing
        End If
    End Sub


    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        cabeceras()
        llenar_participantes()
        llenararray()
        llenar_grid()
    End Sub
    Protected Sub cabeceras()
        Dim obj As New ClsConectarDatos
        Dim tabla As New System.Data.DataTable
        Dim i As Integer
        ncols = 0
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        tabla = obj.TraerDataTable("EVE_ConsultaMatEntregados", Me.DropDownList2.SelectedValue, "C")
        If tabla.Rows.Count > 0 Then
            ncols = tabla.Rows.Count
            For i = 0 To (tabla.Rows.Count) - 1
                ' Response.Write(tabla.Rows(i).Item(5)) ' ver nombres de cabeceras de col
                cabeceranombre(i + 1) = tabla.Rows(i).Item(5).ToString ' nombre de la columna grid
                cabeceravalor(i + 1) = tabla.Rows(i).Item(0).ToString  ' codigo_mev asociado a la col
            Next
        Else
        End If
        obj.CerrarConexion()
        obj = Nothing
    End Sub

    Protected Sub llenar_participantes()
        Dim obj As New ClsConectarDatos
        Dim tabla As New System.Data.DataTable
        Dim i As Integer
        nrows = 0
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        tabla = obj.TraerDataTable("EVE_ConsultaMatEntregados", Me.DropDownList2.SelectedValue, "P")
        If tabla.Rows.Count > 0 Then
            nrows = tabla.Rows.Count
            For i = 0 To (tabla.Rows.Count) - 1
                lstparticipantes(i + 1) = tabla.Rows(i).Item(0).ToString ' aqui el nombre del participante
                lstparticipantes_codigo_pso(i + 1) = tabla.Rows(i).Item(1) ' aqui el codigo_pso
            Next
        Else
        End If
        obj.CerrarConexion()
        obj = Nothing
    End Sub
    Protected Sub llenararray() 'Llenar el array de 2 columnas
        ' primera columna es el codigo_pso y la segunda el codigo mev
        Dim obj As New ClsConectarDatos
        Dim tabla2 As New System.Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        tabla2 = obj.TraerDataTable("EVE_ConsultaMatEntregados", Me.DropDownList2.SelectedValue, "T")
        Dim i As Integer
        For i = 1 To tabla2.Rows.Count
            arrayval(i, 1) = tabla2.Rows(i - 1).Item(1) 'codigo_pso
            arrayval(i, 2) = tabla2.Rows(i - 1).Item(0) 'codigo mev
        Next
        obj.CerrarConexion()
        obj = Nothing
    End Sub
    Protected Sub llenar_grid()
        Dim i, j As Integer
        Dim rowCtr As Integer
        Dim cellCtr As Integer
        ' llenar la cabecera
        Dim tcabecera As New TableRow
        For cellCtr = 0 To ncols
            Dim tcell As New TableCell
            Dim lblcabecera As New Label
            lblcabecera.Visible = True
            lblcabecera.ID = "cab" & Format(cellCtr, "000")
            If cellCtr = 0 Then
                lblcabecera.Text = "Participante"
            Else
                lblcabecera.Text = cabeceranombre(cellCtr)
            End If
            tcell.Controls.Add(lblcabecera)
            tcabecera.Cells.Add(tcell)
            If cellCtr = ncols Then
                Dim tcell1, tcell2 As New tablecell
                Dim lblcabecera1, lblcabecera2 As New label
                lblcabecera1.visible = True : lblcabecera2.visible = True
                lblcabecera1.id = "cab" & format(cellCtr + 1, "000") : lblcabecera2.id = "cab" & format(cellCtr + 2, "000")
                lblcabecera1.text = "Entr" : lblcabecera2.text = "S/Entr"
                lblcabecera1.tooltip = "Entregados" : lblcabecera2.tooltip = "Sin Entregar"
                tcell1.controls.add(lblcabecera1)
                tcabecera.cells.add(tcell1)
                tcell2.controls.add(lblcabecera2)
                tcabecera.cells.add(tcell2)
            End If
        Next
        Table1.Rows.Add(tcabecera)

        'llenar las filas y colocar los checks
        'arrayval(i, 1) codigo_pso
        'arrayval(i, 2) codigo mev
        'lstparticipantes(i)  aqui el nombre del participante
        'lstparticipantes_codigo_pso(i) aqui el codigo_pso
        'cabeceranombre(i) cabecera nombre de la columna 
        'cabeceravalor(i) cabecera valor detras de esa columna
        'entregado_participante  -- cuantos se ha entregado a c/participante
        'entregado_total As Integer -- cuanto se entrego en total
        'total_participantes As Integer -- cuantos participantes hay
        'por_entregar As Integer -- cuantos materiales quedan por entregar

        For rowCtr = 0 To nrows - 1
            Dim tRow As New TableRow
            For cellCtr = 0 To ncols
                Dim tcell As New TableCell
                If cellCtr = 0 Then
                    Dim lblparticipante As New Label
                    lblparticipante.Visible = True
                    lblparticipante.ID = lstparticipantes_codigo_pso(rowCtr + 1)
                    lblparticipante.Text = lstparticipantes(rowCtr + 1)
                    lblparticipante.ForeColor = Drawing.Color.Black
                    tcell.Text = lstparticipantes(rowCtr)
                    tcell.Controls.Add(lblparticipante)
                    tRow.Cells.Add(tcell)
                Else
                    Dim chk1 As New CheckBox
                    chk1.Visible = True
                    chk1.ID = "chk_r" & Format(rowCtr, "000") & "_f" & Format(cellCtr, "000")
                    chk1.Checked = False
                    'buscar en arrayval si ese codigo mev esta presente para activar el checkbox
                    For i = 1 To nrows + 1
                        If arrayval(i, 1) = lstparticipantes_codigo_pso(rowCtr + 1) Then
                            If arrayval(i, 2) = cabeceravalor(cellCtr) Then
                                chk1.Checked = True
                                entregado_participante = entregado_participante + 1
                            End If
                        End If
                    Next
                    tcell.Controls.Add(chk1)
                    tRow.Cells.Add(tcell)
                End If
            Next
            Dim tcell1, tcell2 As New TableCell
            Dim lbl1, lbl2 As New label

            lbl1.visible = True : lbl2.visible = True
            lbl1.id = "row" & Format(rowCtr, "000") & "_f" & Format(cellCtr + 1, "000")
            lbl2.id = "row" & Format(rowCtr, "000") & "_f" & Format(cellCtr + 2, "000")
            lbl1.text = entregado_participante.ToString
            lbl2.text = (ncols - entregado_participante).ToString
            por_entregar = por_entregar + (ncols - entregado_participante)
            tcell1.controls.add(lbl1)
            tRow.cells.add(tcell1)
            tcell2.controls.add(lbl2)
            tRow.cells.add(tcell2)
            entregado_total = entregado_total + entregado_participante
            por_entregar_total = por_entregar_total + por_entregar
            entregado_participante = 0
            por_entregar = 0
            Table1.Rows.Add(tRow)

        Next
        Dim salida As String
        salida = "<table border=1 width=39% cellspacing=0 cellpadding=0>"
        salida = salida + "<tr>"
        salida = salida + "<td width=274><font face='Arial' size=2>"
        salida = salida + "Material Entregado a Participantes</font></td>"
        salida = salida + "<td>" & entregado_total.ToString & "</td>"
        salida = salida + "</tr>"
        salida = salida + "<tr>"
        salida = salida + "<td width=274><font face='Arial' size=2>Material Faltante por entregar</font></td>"
        salida = salida + "<td>" & por_entregar_total.ToString & "</td>"
        salida = salida + "</tr>"
        salida = salida + "</table>"
        Me.totentregado.text = salida
    End Sub
End Class
