Imports System.Reflection.Metadata.Ecma335
Imports System.Security.Cryptography
Imports System.Windows.Forms.Design

Module Transports
    'This hashtable contains a mapping of names to internal data needed to 
    'make calls and funnel into a displayer if needed.
    Public AvailableTransports As New Hashtable
    'This hashtable contains a mapping of names to User Controls for
    'displaying information about existing systems.
    Public AvailableDisplayers As New Hashtable
    'This hashtable contains a mapping of names to User Controls for making
    'new systems.
    Public AvailableBuilders As New Hashtable

    Public Sub Setup()
        'EXTENDING: Types relating to new transports need to appear in these 
        'three hashtables.
        '
        'The hastable key needs to match what the 'transport-type' option
        'would be in the invite file.
        '
        AvailableTransports(LCase("KnownSystem")) = GetType(KnownSystemNullTransport)
        AvailableDisplayers(LCase("KnownSystem")) = GetType(ucoDISPLAY_KnownSystem)
        AvailableBuilders(LCase("KnownSystem")) = GetType(ucoBUILDER_KnownSystem)

        AvailableTransports(LCase("SSHtransport")) = GetType(SSHTransport)
        AvailableDisplayers(LCase("SSHTransport")) = GetType(ucoDISPLAY_SSHTransport)
        AvailableBuilders(LCase("SSHTransport")) = GetType(ucoBUILDER_SSHTransport)

        DebugOut("Supported Transport Abstractions:")
        For Each Item In AvailableTransports
            DebugOut(" " & Item.Key)
        Next
    End Sub

    Public Function Make(in_transport_name As String, in_params As Hashtable)
        If Not AvailableTransports.Contains(Trim(LCase(in_transport_name))) Then
            DebugOut(" Transports.Make(...): Can't make unsupported transport " & Chr(34) & in_transport_name & Chr(34))
            Make = Nothing
            Exit Function
        End If
        Dim NewTransport As New Object
        NewTransport = Activator.CreateInstance(AvailableTransports(Trim(LCase(in_transport_name))))
        NewTransport.SetProperties(in_params) '-------------------------------------------------------------------------
        Make = NewTransport
    End Function

    '-------------------------------------------------------------------------
    'Transport classes
    '
    'A port object may contain one of these classes.
    '-------------------------------------------------------------------------
    Public Class KnownSystemNullTransport
        Public ReadOnly TypeName As String = "KnownSystem"
        Private ConstituentParams = New List(Of String)()
        Private CurrentParams As New Hashtable

        Public Function SetProperties(in_param As Hashtable)
            'This transport has no parameters.
            'We do nothing here.
            SetProperties = True
        End Function

        Public Function GetProperties()
            Dim out_hashtable As New Hashtable
            GetProperties = CurrentParams
        End Function

    End Class

    Public Class SSHTransport
        Public ReadOnly TypeName As String = "SSHTransport"
        Private ConstituentParams = New List(Of String)(
            {"INTERNAL-SSHBinPath", "INTERNAL-SSHKeyPath", "ssh-username", "ssh-server"}
            )
        Private CurrentParams As New Hashtable

        Public Function SetProperties(in_params As Hashtable) As Boolean
            If Not IsNothing(CurrentParams) Then CurrentParams.Clear()
            For Each Param In in_params
                If ConstituentParams.Contains(Param.Key) Then
                    CurrentParams(Param.Key) = Param.Value
                Else
                    DebugOut(TypeName & ".SetProperties(...): property " & Chr(34) & Param.Key & Chr(34) & " is not a constituent property - this is a bug")
                End If
            Next
            SetProperties = True
        End Function
        Public Function GetProperties() As Hashtable
            GetProperties = CurrentParams
        End Function
    End Class

End Module
