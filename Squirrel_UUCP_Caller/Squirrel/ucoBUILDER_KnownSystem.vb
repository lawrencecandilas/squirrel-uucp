Public Class ucoBUILDER_KnownSystem
    'Required Public Variables
    Public Modified As Boolean = False
    Public valid As Boolean = False

    'These are required and set by New()
    Public Caller As Object = Nothing
    Public Winpathof_etcuucp As String = ""
    Public Ports As Collection
    Public Systems As Collection
    Private Sub ucoBUILDER_KnownSystem_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dock = DockStyle.Fill
    End Sub
    Public Sub New(in_caller As Object, in_Winpathof_etcuucp As String, in_ports As Object, in_systems As Object)
        ' This call is required by the designer.
        InitializeComponent()

        Caller = in_caller
        Winpathof_etcuucp = in_Winpathof_etcuucp
        Ports = in_ports
        Systems = in_systems
    End Sub
    Public Function CheckIfValid(in_NewSystemHashtable As Hashtable, Optional vmode As Integer = 0) As String
        'If returned string is null, there are no problems.
        CheckIfValid = ""
        For Each Thing In in_NewSystemHashtable
            CheckIfValid &= ValidateAs(Thing.Value, Thing.Key, vmode)
        Next
    End Function
    Public Function MakeFromUI() As Hashtable
        'Pull bits from UI and stuff them in a hashtable.
        '
        'Unlike reading from an invite file, this should always succeed.
        Dim NVPs As New Hashtable
        NVPs("transport-type") = "KnownSystemNullTransport"

        Dim OfThisOne As Control
        For Each OfThisOne In Me.Controls
            If IsNothing(OfThisOne.Tag) Then Continue For
            NVPs(OfThisOne.Tag) = Extract(OfThisOne)
        Next

        NVPs("failed") = False
        NVPs("whyfailed") = ""
        MakeFromUI = NVPs
    End Function

    Public Function EtchInFiles(in_NewSystemHashtable As Hashtable, in_conf As Object) As Boolean
        'EtchInFiles assumes it's dealing with a completed and validated
        'hashtable describing a new system!
        '
        'That's responsibily of calling routine, which is expected to be
        'Form2.
        '

        EtchInFiles = False

        Dim i As Integer = 0

        Dim NewSystemName = in_NewSystemHashtable("system-name")

        'At this point we need to examine the current configuration and
        'determine if we are going to...
        'A. Append port/system to existing Port/sys conf files
        'B. Overwrite existing system in sys file, add new port to Port file
        'C. Do nothing because it already exists.

        'Check existing systems in config
        Dim ExistingSystemFound As Boolean = False
        For Each ExistingSystem In Systems
            If (ExistingSystem.Name = NewSystemName) Then
                ExistingSystemFound = True
                Exit For
            End If
        Next
        If ExistingSystemFound Then
            SquirrelComms.Item(16).UserError()
            Exit Function
        End If

        'Add new known system to our config file.
        'N/A for a known system

        'Append new port definition to Port file.
        'N/A for a known system

        'Append to new system to sys file
        Try
            Dim SysDefaultOptions As New Hashtable
            Dim SysFileBuffer As String = ""
            SysDefaultOptions = in_conf.PrepareToModifySys(SysFileBuffer)

            If in_NewSystemHashtable.Contains("please-forward-from-me") Then
                in_conf.ModifyForward(SysDefaultOptions, "forward-from", True, NewSystemName)
            End If
            If in_NewSystemHashtable.Contains("forward-to-me") Then
                in_conf.ModifyForward(SysDefaultOptions, "forward-to", True, NewSystemName)
            End If

            SysFileBuffer &=
                vbLf &
                    "system " & NewSystemName &
                    vbLf &
                    "protocol i" &
                    vbLf
            in_conf.CommitModifiedSys(SysFileBuffer, SysDefaultOptions)

            DebugOut("successfully appended new system " &
                Chr(34) & NewSystemName & Chr(34) &
                " to " &
                Chr(34) & Winpathof_etcuucp & "\sys" & Chr(34)
                )
        Catch
            SquirrelComms.Item(19).SystemError("")
            DebugOut("FAILED while appending new system " &
                     Chr(34) & NewSystemName & Chr(34) &
                     " to " &
                     Chr(34) & Winpathof_etcuucp & "\sys" & Chr(34)
                     )
            EtchInFiles = False
            Exit Function
        End Try

        EtchInFiles = True
    End Function

    Private Sub tbxNewSystemName_TextChanged(sender As Object, e As EventArgs) Handles tbxNewSystemName.TextChanged
        Modified = True
        Caller.PanelChange("")
    End Sub

End Class
