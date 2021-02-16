Imports Microsoft.VisualBasic
Imports System.Web.UI.WebControls
Imports System.Collections.Generic
Imports System

Public Class ClsGlobales
#Region "Declaración de variables"
    Private Shared cnx As ClsConectarDatos
    Private Shared dt As System.Data.DataTable

    Public Shared ReadOnly KEY_PREV_TEXT As String = "prevText"
    Public Shared ReadOnly KEY_INDEX_COL As String = "indexCol"
    Public Shared ReadOnly KEY_CELLS_COL_OPER As String = "cellsColOper"
    Public Shared ReadOnly KEY_INDEX_NEW_ROW As String = "indexNewRow"

    Public Shared ReadOnly PATH_ASSETS As String = "../../../assets"
    Public Shared ReadOnly PATH_CSS As String = "../css"
    Public Shared ReadOnly PATH_JS As String = "../js"
    Public Shared ReadOnly PATH_IMG As String = "../img"
#End Region

#Region "Métodos"
    'CONSULTAS
    Public Shared Function fc_ListarCicloAcademico() As Data.DataTable
        Try
            dt = New System.Data.DataTable
            cnx = New ClsConectarDatos
            cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.AbrirConexion()
            dt = cnx.TraerDataTable("ConsultarCicloAcademico", "TO", "")
            cnx.CerrarConexion()
            Return dt
        Catch ex As System.Exception
            Throw ex
        End Try
    End Function

    Public Shared Function fc_ListarCentroCostos(ByVal tipoOperacion As String, ByVal codigoCac As Integer, ByVal codUsuario As Integer) As Data.DataTable
        Try
            dt = New System.Data.DataTable
            cnx = New ClsConectarDatos
            cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.AbrirConexion()
            dt = cnx.TraerDataTable("ADM_ConsultarEventoAdmision", tipoOperacion, codigoCac, "", 1, 1, codUsuario)
            cnx.CerrarConexion()
            Return dt
        Catch ex As System.Exception
            Throw ex
        End Try
    End Function

    Public Shared Function fc_ListarCarreraProfesional(Optional ByVal tipo As String = "TO", Optional ByVal param As Integer = 2) As Data.DataTable
        Try
            dt = New System.Data.DataTable
            cnx = New ClsConectarDatos
            cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.AbrirConexion()
            dt = cnx.TraerDataTable("ConsultarCarreraProfesional", tipo, param)
            cnx.CerrarConexion()
            Return dt
        Catch ex As System.Exception
            Throw ex
        End Try
    End Function

    Public Shared Function fc_ListarModalidadIngreso(Optional ByVal tipo As String = "TO", Optional ByVal param As String = "") As Data.DataTable
        Try
            dt = New System.Data.DataTable
            cnx = New ClsConectarDatos
            cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.AbrirConexion()
            dt = cnx.TraerDataTable("ConsultarModalidadIngreso", tipo, param)
            cnx.CerrarConexion()
            Return dt
        Catch ex As System.Exception
            Throw ex
        End Try
    End Function

    Public Shared Function fc_ListarGrupoAdmisionVirtual(ByVal tipoOperacion As String, ByVal codigoGru As Integer, ByVal codigoCco As String, ByVal codigoTge As Integer) As Data.DataTable
        Try
            dt = New System.Data.DataTable
            cnx = New ClsConectarDatos
            cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.AbrirConexion()
            dt = cnx.TraerDataTable("ADM_GrupoAdmisionVirtual_Listar", tipoOperacion, codigoGru, codigoCco, codigoTge)
            cnx.CerrarConexion()
            Return dt
        Catch ex As System.Exception
            Throw ex
        End Try
    End Function

    'UTILIDADES
    Public Shared Sub mt_AgruparFilas(ByVal gridViewRows As GridViewRowCollection, ByVal startIndex As Integer, ByVal totalColumns As Integer)
        If totalColumns = 0 Then Return
        Dim i As Integer, count As Integer = 1
        Dim lst As ArrayList = New ArrayList()
        lst.Add(gridViewRows(0))
        Dim ctrl As TableCell
        ctrl = gridViewRows(0).Cells(startIndex)
        For i = 1 To gridViewRows.Count - 1
            Dim nextTbCell As TableCell = gridViewRows(i).Cells(startIndex)
            If ctrl.Text = nextTbCell.Text Then
                count += 1
                nextTbCell.Visible = False
                lst.Add(gridViewRows(i))
            Else
                If count > 1 Then
                    ctrl.RowSpan = count
                    ctrl.VerticalAlign = VerticalAlign.Middle
                    mt_AgruparFilas(New GridViewRowCollection(lst), startIndex + 1, totalColumns - 1)
                End If
                count = 1
                lst.Clear()
                ctrl = gridViewRows(i).Cells(startIndex)
                lst.Add(gridViewRows(i))
            End If
        Next
        If count > 1 Then
            ctrl.RowSpan = count
            ctrl.VerticalAlign = VerticalAlign.Middle
            mt_AgruparFilas(New GridViewRowCollection(lst), startIndex + 1, totalColumns - 1)
        End If
        count = 1
        lst.Clear()
    End Sub

    Public Shared Function fn_RetornaEstructuraRowspan(ByVal ParamArray keys() As String) As Dictionary(Of String, Dictionary(Of String, Object))
        Dim estructura As Dictionary(Of String, Dictionary(Of String, Object)) = New Dictionary(Of String, Dictionary(Of String, Object))

        For Each key As String In keys
            estructura.Item(key) = New Dictionary(Of String, Object)
            With estructura.Item(key)
                .Item(KEY_PREV_TEXT) = ""
                .Item(KEY_INDEX_COL) = 0
                .Item(KEY_CELLS_COL_OPER) = 0
                .Item(KEY_INDEX_NEW_ROW) = 0
            End With
        Next

        Return estructura
    End Function

    Public Shared Sub mt_AgruparFilasCustom(ByVal gridview As GridView, ByVal values As Dictionary(Of String, Dictionary(Of String, Object)))
        For Each row As GridViewRow In gridview.Rows
            For Each key As String In values.Keys
                With values.Item(key)
                    If row.DataItemIndex > 0 Then
                        Dim prevRow As GridViewRow = gridview.Rows(row.DataItemIndex - 1)
                        .Item(KEY_PREV_TEXT) = prevRow.Cells(.Item(KEY_INDEX_COL)).Text
                    End If

                    Dim cellText As String = row.Cells(.Item(KEY_INDEX_COL)).Text
                    If cellText <> .Item(KEY_PREV_TEXT) OrElse row.DataItemIndex = gridview.Rows.Count - 1 Then
                        Dim rowSpan As Integer = row.DataItemIndex - .Item(KEY_INDEX_NEW_ROW)
                        If row.DataItemIndex = gridview.Rows.Count - 1 AndAlso cellText = .Item(KEY_PREV_TEXT) Then
                            rowSpan += 1
                            mt_OcultarCeldas(row, .Item(KEY_INDEX_COL), .Item(KEY_CELLS_COL_OPER))
                        End If

                        If rowSpan > 1 Then
                            'Aplico el rowspan
                            gridview.Rows(.Item(KEY_INDEX_NEW_ROW)).Cells(.Item(KEY_INDEX_COL)).RowSpan = rowSpan
                            If .Item(KEY_CELLS_COL_OPER) > 0 Then
                                gridview.Rows(.Item(KEY_INDEX_NEW_ROW)).Cells(.Item(KEY_INDEX_COL) + .Item(KEY_CELLS_COL_OPER)).RowSpan = rowSpan
                            End If
                        End If
                        .Item(KEY_INDEX_NEW_ROW) = row.DataItemIndex
                    Else
                        mt_OcultarCeldas(row, .Item(KEY_INDEX_COL), .Item(KEY_CELLS_COL_OPER))
                    End If
                End With
            Next
        Next
    End Sub

    Private Shared Sub mt_OcultarCeldas(ByVal row As GridViewRow, ByVal indexCol As Integer, Optional ByVal cellsColOper As Integer = 0)
        row.Cells(indexCol).Visible = False
        If cellsColOper > 0 Then
            row.Cells(indexCol + cellsColOper).Visible = False
        End If
    End Sub

    Public Shared Sub mt_AgruparCeldasRepetidas(ByVal rows As GridViewRowCollection, ByVal startIndex As Integer, ByVal totalColumns As Integer)
        Dim endColIndex As Integer = 0
        Dim upperText As String = "", actualText As String = ""
        Dim dRowSpan As New Dictionary(Of Integer, Dictionary(Of String, Integer))
        Dim rowSpan As Integer = 0
        Dim cell As DataControlFieldCell

        For i As Integer = 1 To rows.Count - 1
            endColIndex = Math.Min(rows(i).Cells.Count - 1, startIndex + totalColumns - 1)
            For j As Integer = startIndex To endColIndex
                If Not dRowSpan.ContainsKey(j) Then
                    dRowSpan.Item(j) = New Dictionary(Of String, Integer)
                    dRowSpan.Item(j).Item("row") = i - 1
                    dRowSpan.Item(j).Item("rowSpan") = 1
                End If

                cell = rows(i).Cells(j)
                If TypeOf cell.ContainingField Is TemplateField Then
                    Continue For
                End If

                upperText = rows(i - 1).Cells(j).Text
                actualText = cell.Text
                If actualText = upperText AndAlso i < rows.Count - 1 Then
                    dRowSpan.Item(j).Item("rowSpan") += 1
                    rows(i).Cells(j).Visible = False
                Else
                    rowSpan = dRowSpan.Item(j).Item("rowSpan")
                    If rowSpan > 1 Then
                        Dim cellRowsPan As DataControlFieldCell = rows.Item(dRowSpan.Item(j).Item("row")).Cells(j)

                        cellRowsPan.RowSpan = dRowSpan.Item(j).Item("rowSpan")
                        If TypeOf cellRowsPan.ContainingField Is TemplateField Then
                            'cellRowsPan.BackColor = Drawing.Color.Red
                            'cellRowsPan.Text = "ASDASD"
                        End If
                    End If
                    dRowSpan.Item(j).Item("row") = i
                    dRowSpan.Item(j).Item("rowSpan") = 1
                End If
            Next
        Next
    End Sub

    Public Shared Sub mt_LlenarListas(ByVal Combo As System.Object, ByVal Datos As System.Data.DataTable, _
    ByVal ColValor As System.Object, ByVal ColDescripcion As System.Object)
        Combo.Items.Clear()
        Combo.DataSource = Datos
        Combo.DataValueField = Datos.Columns(ColValor).ToString
        Combo.DataTextField = Datos.Columns(ColDescripcion).ToString
        Combo.DataBind()
    End Sub

    Public Shared Sub mt_LlenarListas(ByVal Combo As System.Object, ByVal Datos As System.Data.DataTable, _
    ByVal ColValor As System.Object, ByVal ColDescripcion As System.Object, ByVal Mensaje As String)
        'El valor predeterminado cuando no existe seleccion es (-2)
        Combo.Items.Clear()
        Combo.Items.Add(Mensaje)
        Combo.Items(0).Value = -1
        For i As Integer = 1 To Datos.Rows.Count
            Combo.Items.Add(Datos.Rows(i - 1).Item(ColDescripcion).ToString)
            Combo.Items(i).Value = Datos.Rows(i - 1).Item(ColValor).ToString
        Next
    End Sub

    'Cargar lista con con formato "abrebiatura: descripción", además permite indicar cantidad máxima de caracteres
    Public Shared Sub mt_LlenarListas(ByVal combo As System.Object, ByVal datos As System.Data.DataTable, _
    ByVal colValor As String, ByVal colDescripcion As String, _
    ByVal mensaje As String, Optional ByVal colAbreviatura As String = "", Optional ByVal maxLetrasDescripcion As Integer = 0)
        'El valor predeterminado cuando no existe seleccion es (-2)
        combo.Items.Clear()
        combo.Items.Add(mensaje)
        combo.Items(0).Value = -1

        Dim descripcion As String
        For i As Integer = 1 To datos.Rows.Count
            descripcion = datos.Rows(i - 1).Item(colDescripcion)
            If colAbreviatura <> "" Then descripcion = datos.Rows(i - 1).Item(colAbreviatura) + ": " + descripcion
            If maxLetrasDescripcion > 0 AndAlso descripcion.Length > maxLetrasDescripcion Then
                descripcion = descripcion.Substring(0, maxLetrasDescripcion) + "..."
            End If
            combo.Items.Add(descripcion)
            combo.Items(i).Value = datos.Rows(i - 1).Item(colValor).ToString
        Next
    End Sub

    Public Shared Sub mt_AgregarCabecera(ByVal objgridviewrow As GridViewRow, ByVal objtablecell As TableCell, ByVal colspan As Integer, _
                                 ByVal celltext As String, ByVal backcolor As String, Optional ByVal paint As Boolean = False)
        objtablecell = New TableCell()
        objtablecell.Text = celltext
        objtablecell.ColumnSpan = colspan

        If paint Then
            objtablecell.Style.Add("background-color", backcolor)
            objtablecell.Style.Add("font-weight", "bold")
            objtablecell.Style.Add("color", "#000000")
            objtablecell.Style.Add("BorderColor", "#BBBBBB")
        End If

        'objtablecell.Style.Add("CssClass", "thead-dark")

        objtablecell.HorizontalAlign = HorizontalAlign.Center
        objgridviewrow.Cells.Add(objtablecell)
    End Sub

    Public Shared Function fc_EncriptaTexto64(ByVal base64Decoded As String) As String
        Dim base64Encoded As String = ""
        Try
            Dim data As Byte()
            data = System.Text.UTF8Encoding.UTF8.GetBytes(base64Decoded)
            base64Encoded = System.Convert.ToBase64String(data)
        Catch ex As Exception
            Throw ex
        End Try
        Return base64Encoded
    End Function

    Public Shared Function fc_DesencriptaTexto64(ByVal base64Encoded As String) As String
        Dim base64Decoded As String = ""
        Try
            Dim data() As Byte
            data = System.Convert.FromBase64String(base64Encoded)
            base64Decoded = System.Text.UTF8Encoding.UTF8.GetString(data)
        Catch ex As Exception
            Throw ex
        End Try
        Return base64Decoded
    End Function
#End Region

End Class
