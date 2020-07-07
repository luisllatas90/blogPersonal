Imports Microsoft.VisualBasic
Imports Microsoft.Win32
Imports NCrypto.Security.Cryptography
Imports System.Text

Public Enum RegistryHive
    HKLM ' HKEY_LOCAL_MACHINE
    HKCR ' HKEY_CLASSES_ROOT
    HKCU ' HKEY_CURRENT_USER
    HKU ' HKEY_USERS
    HKCC ' HKEY_CURRENT_CONFIG
End Enum

Public Class RegistryCryptoUtility
    Private Const COLON_DELIMITER As String = ":"
    Private Const COMMA_DELIMITER As String = ","
    Private Const BACKSLASH_DELIMITER As String = "\"
    Private Const REGISTRY_PREFIX As String = "registry:"

    ' Receives a string in the format:
    ' registry:HKLM\Software\ASP.NET\MyKey\ASPNET_SETREG,sqlConnectionString
    ' and pulls the value from the correct registry hive, and extracts and
    ' decrypts the connection string information
    Public Shared Function DecryptRegistryConnectionString(ByVal configConnectionSetting As String) As String
        Dim regKey As RegistryKey
        Dim registryBytes As Byte()
        If configConnectionSetting.StartsWith(REGISTRY_PREFIX) Then
            Dim regKeyPathAndKey As String = configConnectionSetting.Split(COLON_DELIMITER.ToCharArray())(1)
            Dim regKeyPath As String = regKeyPathAndKey.Split(COMMA_DELIMITER.ToCharArray())(0)
            Dim keyName As String = regKeyPathAndKey.Split(COMMA_DELIMITER.ToCharArray())(1)
            Dim regkeyHive As RegistryKey

            ' Open the proper Registry Hive
            If regKeyPath.StartsWith(System.Enum.GetName(GetType(RegistryHive), RegistryHive.HKLM)) Then
                regkeyHive = Registry.LocalMachine
            ElseIf regKeyPath.StartsWith(System.Enum.GetName(GetType(RegistryHive), RegistryHive.HKCR)) Then
                regkeyHive = Registry.ClassesRoot
            ElseIf regKeyPath.StartsWith(System.Enum.GetName(GetType(RegistryHive), RegistryHive.HKCU)) Then
                regkeyHive = Registry.CurrentUser
            ElseIf regKeyPath.StartsWith(System.Enum.GetName(GetType(RegistryHive), RegistryHive.HKU)) Then
                regkeyHive = Registry.Users
            ElseIf regKeyPath.StartsWith(System.Enum.GetName(GetType(RegistryHive), RegistryHive.HKCC)) Then
                regkeyHive = Registry.Users
            Else
                Throw New ApplicationException("Unknown Key reference: " & regKeyPath)
            End If

            Dim seperatorPosition As Integer = regKeyPath.IndexOf(BACKSLASH_DELIMITER, 0) + 1
            regKeyPath = regKeyPath.Substring(seperatorPosition, regKeyPath.Length - seperatorPosition)
            regKey = regkeyHive.OpenSubKey(regKeyPath)
            registryBytes = CType(regKey.GetValue(keyName), Byte())
            Return Encoding.Unicode.GetString(ProtectedData.Unprotect(registryBytes))
        Else
            ' return the Config string, registry not specified
            Return configConnectionSetting
        End If
    End Function


End Class


