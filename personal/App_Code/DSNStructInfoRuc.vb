Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Collections

Public Class DSNStructInfoRuc
    Dim strNumRuc As String
    Dim strNombreRazonSocial As String
    Dim strNombreComercial As String
    Dim strDepartamento As String
    Dim strProvincia As String
    Dim strDistrito As String
    Dim strDireccion As String
    Dim strEstadoContribuyente As String
    Dim distritosToReplace As New List(Of String)
    Dim strCodigo As Integer

    Public Sub New()
        strCodigo = 0
        strNumRuc = String.Empty
        strNombreRazonSocial = String.Empty
        strDireccion = String.Empty
        strNombreComercial = String.Empty
        strProvincia = String.Empty
        strDepartamento = String.Empty
        strDistrito = String.Empty
        distritosToReplace.Add("ANCO-HUALLO")
        distritosToReplace.Add("HUAC-HUAS")
        distritosToReplace.Add("HUANCA-HUANCA")
        distritosToReplace.Add("QUITO-ARMA")
        distritosToReplace.Add("RUPA-RUPA")
        distritosToReplace.Add("HUAY-HUAY")
        distritosToReplace.Add("ESTIQUE-PAMPA")
    End Sub
    Public Property Codigo() As Integer
        Get
            Return strCodigo
        End Get
        Set(ByVal value As Integer)
            strCodigo = value
        End Set
    End Property
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property NumRuc() As String
        Get
            Return strNumRuc
        End Get
        Set(ByVal value As String)
            strNumRuc = value
        End Set
    End Property
    ''' <summary>
    ''' Almacena el la razon social en consulta de la empresa
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property NombreRazonSocial() As String
        Get
            Return strNombreRazonSocial
        End Get
        Set(ByVal value As String)
            strNombreRazonSocial = value
        End Set
    End Property
    Public Property Departamento() As String
        Get
            Return strDepartamento
        End Get
        Set(ByVal value As String)
            strDepartamento = value
        End Set
    End Property
    Public Property Provincia() As String
        Get
            Return strProvincia
        End Get
        Set(ByVal value As String)
            strProvincia = value
        End Set
    End Property
    Public Property Distrito() As String
        Get
            Return strDistrito
        End Get
        Set(ByVal value As String)
            strDistrito = value
        End Set
    End Property
    Public Property Direccion() As String
        Get
            Return strDireccion
        End Get
        Set(ByVal value As String)
            strDireccion = value
        End Set
    End Property
    Public Property NombreComercial() As String
        Get
            Return strNombreComercial
        End Get
        Set(ByVal value As String)
            strNombreComercial = value
        End Set
    End Property

    Public Sub SetValue(ByVal info As DSNDatosValores)
        Dim field As String = info.Dato.ToLower()
        '  Me.NombreRazonSocial = "OKS"
        If info IsNot Nothing Then
            ' Me.NombreRazonSocial = "OKS2"
            Try
                Dim valores As List(Of String) = info.Valor
                If valores.Count() > 0 Then
                    ' Me.NombreRazonSocial = field.Trim()
                    If field.Trim().ToLower().Equals("número de ruc:", StringComparison.OrdinalIgnoreCase) Then
                        ' Me.NombreRazonSocial = "Ruc :" & NumRuc.ToString().Length()
                        If "".Equals(NumRuc.ToString()) Then

                            Me.NumRuc = valores(0).Substring(0, 11)
                            Me.NombreRazonSocial = valores(0).Substring(valores(0).IndexOf("-") + 2).ToUpper().Trim()
                        End If
                    End If

                    If field.Trim().ToLower().Equals("nombre comercial:") Then
                        If "".Equals(NombreComercial) Then
                            Me.NombreComercial = valores(0).ToUpper().Trim()
                        End If
                    End If

                    If field.Trim().ToLower().Equals("dirección del domicilio fiscal:") Then
                        'aqui sacamos departamento provincia y distrito
                        Dim strCadena As [String] = ""
                        Dim direccion As [String] = valores(0).ToUpper()

                        For Each item As String In distritosToReplace
                            direccion = direccion.Replace(item.ToLower(), item.ToLower().Replace("_", "_"))
                        Next


                        Dim strInf As [String]() = direccion.ToLower().Split("-"c)
                        If strInf.Length >= 3 Then
                            If "".Equals(Me.strDistrito) Then
                                strCadena = strInf(0).Replace("la libertad", "la_libertad")
                                strCadena = strCadena.Replace("madre de dios", "madre_de_dios")
                                strCadena = strCadena.Replace("san martin", "san_martin")
                                strCadena = strCadena.Trim()
                                Me.Distrito = strInf(strInf.Length - 1).ToUpper().Trim().Replace("_", "-")
                                Me.Provincia = strInf(strInf.Length - 2).ToUpper().Trim()
                                Dim strPedacitos As [String]() = strInf(strInf.Length - 3).Trim().Split(" "c)
                                Me.Departamento = strPedacitos(strPedacitos.Length - 1).ToUpper().Trim().Replace("_", " ")
                                strPedacitos(strPedacitos.Length - 1) = ""

                                strInf(strInf.Length - 1) = ""
                                strInf(strInf.Length - 2) = ""
                                strInf(strInf.Length - 3) = strInf(strInf.Length - 3).Trim()
                                strInf(strInf.Length - 3) = strInf(strInf.Length - 3).Substring(0, strInf(strInf.Length - 3).Length - Departamento.Length).ToUpper().Trim()

                                Me.Direccion = join(strInf, " ")
                            End If
                        End If
                    End If





                End If
                ' DSNUtilities.Imprimir("Error: " + ex.toString() + "\n");

            Catch ex As Exception
            End Try
        End If
    End Sub
    Private Shared Function htmlspecialcharstable() As Hashtable
        Dim html As New Hashtable()
        html.Add("&AACUTE;", "Á")
        html.Add("&EACUTE;", "É")
        html.Add("&IACUTE;", "Í")
        html.Add("&OACUTE;", "Ó")
        html.Add("&UACUTE;", "Ú")
        html.Add("&ORDM;", "°")
        html.Add("&DEG;", "°")
        html.Add("&AMP;NTILDE;", "Ñ")
        Return html
    End Function
    Public Function join(ByVal s() As String, ByVal delimiter As String) As String

        Dim builder As StringBuilder = New StringBuilder()
        If Not s Is DBNull.Value Then
            For Each cad As String In s
                If Not "".Equals(cad.Trim()) Then
                    builder.Append(cad.ToString().Trim()).Append(delimiter)
                End If

            Next
        End If
        Return builder.ToString()
    End Function
End Class
