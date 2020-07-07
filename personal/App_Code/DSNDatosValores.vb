Imports Microsoft.VisualBasic
Imports System.Collections.Generic
Imports System.Text.RegularExpressions
Imports System

Public Class DSNDatosValores
    Public Sub New()

    End Sub
    Private strDato As String
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Dato() As String
        Get
            Return strDato
        End Get
        Set(ByVal value As String)
            strDato = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Public Valor As New List(Of String)
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="dato"></param>
    ''' <param name="valor"></param>
    ''' <remarks></remarks>
    Public Sub New(ByVal dato As String, ByVal valor As List(Of String))
        Me.Dato = CheckIsNull(dato)
        If valor Is Nothing Then
            valor = New List(Of String)()
        End If
        Me.Valor = valor
    End Sub

    Public Function CheckIsNull(ByVal strCadena As String) As String
        If strCadena Is Nothing Then
            strCadena = ""
        End If
        Return strCadena
    End Function
    Public Shared Function GetDatos(ByVal strHtml As String) As List(Of DSNDatosValores)
        Dim info As New List(Of DSNDatosValores)()
        'equivale a amostrar
        Dim elemtentos As String()
        elemtentos = strHtml.Split("<"c)
        Dim datos As DSNDatosValores = Nothing
        Dim linea As String = ""
        Dim dat As String = ""
        For Each line As String In elemtentos
            If line.ToUpper().StartsWith("TD") Then
                linea = line
                linea = Convert.ToString("<") & linea
                Dim r As New Regex("([<])([^>]*)([>])", RegexOptions.Multiline)
                Dim result As String = r.Replace(linea, "")
                If Not "".Equals(result.Trim()) Then
                    If linea.Trim().EndsWith(":") Then
                        If datos IsNot Nothing Then
                            info.Add(datos)
                            datos = Nothing
                        End If
                        datos = New DSNDatosValores()
                        datos.Dato = ("" + result.ToUpper().Trim())
                        dat = dat & result
                    Else
                        If datos Is Nothing Then
                            datos = New DSNDatosValores()
                        End If
                        datos.Valor.Add(result.ToUpper().Trim())
                    End If
                End If
            End If
            If linea.ToUpper().StartsWith("OPTION") Then
                linea = line
                linea = Convert.ToString("<") & linea
                Dim r As New Regex("([<])([^>]*)([>])", RegexOptions.Multiline)
                Dim result As String = r.Replace(linea, "")
                If Not "".Equals(result.Trim()) Then
                    If linea.Trim().EndsWith(":") Then
                        If datos IsNot Nothing Then
                            info.Add(datos)
                            datos = Nothing
                        End If
                        datos = New DSNDatosValores()
                        datos.Dato = ("" + result.ToUpper().Trim())
                        dat = dat & result
                    Else
                        If datos Is Nothing Then
                            datos = New DSNDatosValores()
                        End If
                        datos.Valor.Add(result.ToUpper().Trim())
                    End If
                End If

            End If
        Next

        Return info
    End Function

End Class
