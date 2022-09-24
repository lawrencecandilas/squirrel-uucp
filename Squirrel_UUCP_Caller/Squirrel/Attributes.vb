Imports System.Runtime.InteropServices
Imports System.Security.Cryptography

Module Attributes
    '-------------------------------------------------------------------------
    'Validation Need Descriptor Class
    '-------------------------------------------------------------------------
    Public Class ValidationNeed
        Public Property Name As String 'Required
        Public Property FriendlyName As String 'Required
        Public Property IsNumeric As Boolean = False
        Public Property MinLengthOrValue As Integer = 1
        Public Property MaxLengthOrValue As Integer = 255
        Public Property MustBeginWithLetter As Boolean = False
        Public Property CanBeBlank As Boolean = False
        Public Property ReplaceBlankWithDefault As Boolean = False
        Public Property DefaultValue As String = ""
        Public Property ForceToAlphanumeric As Boolean = False
        Public Property IsFileThatMustExist As Boolean = False
        Public Property DoNotValidate As Boolean = False
        Public Property IsUNIXFilepath As Boolean = False
        Public Property IsListOfSystems As Boolean = False
        Public Property MustBeValidTransportName As Boolean = False
    End Class

    'All of our lovely ValidationNeeds.
    'Most are transport object related, a few are system related.
    '
    Public ValidationNeeds As New List(Of ValidationNeed) From
        {
            New ValidationNeed With {
                .Name = "whyfailed",
                .FriendlyName = "Internal Attribute 'whyfailed'",
                .DoNotValidate = True
            },
            New ValidationNeed With {
                .Name = "failed",
                .FriendlyName = "Internal Attribute 'failed'",
                .DoNotValidate = True
            },
            New ValidationNeed With {
                .Name = "transport-type",
                .FriendlyName = "Transport Type",
                .MustBeValidTransportName = True
            },
            New ValidationNeed With {
                .Name = "forward-to-me",
                .FriendlyName = "Forward To Me",
                .IsListOfSystems = True
            },
            New ValidationNeed With {
                .Name = "please-forward-from-me",
                .FriendlyName = "Forward From Me",
                .IsListOfSystems = True
            },
            New ValidationNeed With {
                .Name = "system-name",
                .FriendlyName = "System Name",
                .MaxLengthOrValue = 32,
                .MustBeginWithLetter = True,
                .ForceToAlphanumeric = True
            },
            New ValidationNeed With {
                .Name = "server-name",
                .FriendlyName = "Server Name",
                .MaxLengthOrValue = 32,
                .MustBeginWithLetter = True,
                .ForceToAlphanumeric = True
            },
            New ValidationNeed With {
                .Name = "ssh-server",
                .FriendlyName = "SSH Server",
                .MaxLengthOrValue = 32,
                .MustBeginWithLetter = True,
                .ForceToAlphanumeric = True
            },
            New ValidationNeed With {
                .Name = "INTERNAL-SSHKeyPath",
                .FriendlyName = "SSH Key Path",
                .MaxLengthOrValue = 4096,
                .IsFileThatMustExist = True,
                .CanBeBlank = True
            },
            New ValidationNeed With {
                .Name = "INTERNAL-SSHBinPath",
                .FriendlyName = "SSH executable UNIX path",
                .MaxLengthOrValue = 4096,
                .IsFileThatMustExist = True,
                .IsUNIXFilepath = True
            },
            New ValidationNeed With {
                .Name = "ssh-username",
                .FriendlyName = "SSH Login Name",
                .MaxLengthOrValue = 32
            },
            New ValidationNeed With {
                .Name = "uucico-call-username",
                .FriendlyName = "uucico username",
                .MaxLengthOrValue = 32
            },
            New ValidationNeed With {
                .Name = "uucico-call-password",
                .FriendlyName = "uucico password",
                .MaxLengthOrValue = 4096
            },
            New ValidationNeed With {
                .Name = "INTERNAL-SSHPort",
                .FriendlyName = "SSH Port",
                .IsNumeric = True,
                .MinLengthOrValue = 1,
                .MaxLengthOrValue = 65535,
                .DefaultValue = 22
            },
            New ValidationNeed With {
                .Name = "INTERNAL-PortName",
                .FriendlyName = "Port Name"
            },
            New ValidationNeed With {
                .Name = "private-key-text",
                .FriendlyName = "SSH Private Key Text",
                .MaxLengthOrValue = 32768
            }
        }

    Public Function ValidateAs(ByRef in_data As String, in_attribute As String, Optional vmode As Integer = 0) As String
        'If a null string is returned, validation is successful.
        'Otherwise the string will be why it failed.
        ValidateAs = ""

        'If we don't receive the name of the attribute we wantto validate, we
        'aren't going very far
        If in_attribute = "" Then
            ValidateAs = "ValidateAs(...): in_attribue is null" & vbCrLf
            Exit Function
        End If

        'search current attribute objects by name
        Dim AttObj As Object = Nothing
        For Each Attribute In ValidationNeeds
            If LCase(Attribute.Name) = LCase(in_attribute) Then
                AttObj = Attribute
                Exit For
            End If
        Next

        'if none found, report error (if desired) and exit
        If IsNothing(AttObj) Then
            If vmode = 1 Then ValidateAs = "ValidateAs(...): No attribute with name " & Chr(34) & in_attribute & Chr(34) & " found" & vbCrLf
            Exit Function
        End If

        'if attribute says don't validate then that's it
        If AttObj.DoNotValidate Then
            Exit Function
        End If

        'vmode determines how we'll report errors via the return string
        Dim OutName As String
        If vmode = 1 Then
            OutName = AttObj.Name
        Else
            OutName = AttObj.FriendlyName
        End If

        'OK if blank?
        If Not AttObj.CanBeBlank And in_data = "" Then
            ValidateAs &= OutName & ": Can't be blank or unspecified" & vbCrLf
            Exit Function
        End If
        'Need this otherwise length constraints will kick in
        If AttObj.CanBeBlank And in_data = "" Then
            Exit Function
        End If

        'replace blank with default here if needed/desired
        If in_data = "" And AttObj.ReplaceBlankWithDefault Then
            in_data = AttObj.DefaultValue
            'Is valid!
            Exit Function
        End If

        'is attribute a file that must exist?
        'if so, test that here and end validation
        If AttObj.IsFileThatMustExist Then
            If IO.File.Exists(in_data) Then
                ValidateAs &= OutName & "File " & Chr(34) & in_data & Chr(34) & " doesn't exist or inaccessible" & vbCrLf
                Exit Function
            End If
        End If

        'is attribute a numeric?
        'if so, test that here and end validation
        If AttObj.IsNumeric Then
            For i As Integer = 1 To Len(in_data)
                If InStr("0123456789", Mid(in_data, i, 1)) = 0 Then
                    ValidateAs &= OutName & ": Only allowed to contain numbers '0123456789'" & vbCrLf
                    Exit Function
                End If
            Next
            If Val(in_data) > AttObj.MaxLengthOrValue Or Val(in_data) < AttObj.MinLengthOrValue Then
                ValidateAs &= OutName & ": Must be between " & AttObj.MinLengthOrValue & " and " & AttObj.MaxLengthOrValue & vbCrLf
                Exit Function
            End If
            'Is valid!
            Exit Function
        End If

        'AttObj.IsNumeric is false here

        'Does it have to begin with a letter
        If AttObj.MustBeginWithLetter = True Then
            If AscW(UCase(Left(in_data, 1))) < 65 Or AscW(UCase(Left(in_data, 1))) > 90 Then
                ValidateAs &= OutName & ": " & Chr(34) & in_data & Chr(34) & " must begin with a letter" & vbCrLf
            End If
        End If

        'String length check
        If Len(in_data) > AttObj.MaxLengthOrValue Or Len(in_data) < AttObj.MinLengthOrValue Then
            ValidateAs &= OutName & ": Must be between " & AttObj.MinLengthOrValue & " and " & AttObj.MaxLengthOrValue & " characters long" & vbCrLf
        End If

        'Remove nonalpha characters here if desired
        If AttObj.ForceToAlphanumeric Then
            'Loop and look for very illegal characters (outside of printable ASCII
            'range.  Convert sorta illegal ones (most punctuation) to underscores.
            Dim tempchr As Char
            Dim tempstr As String = ""
            Dim lastchr As String = ""
            For c As Integer = 1 To Len(in_data)
                lastchr = tempchr
                tempchr = LCase(Mid(in_data, c, 1))
                'unprintable ASCII is bounced as invalid
                If AscW(tempchr) < 32 Or AscW(tempchr) > 127 Then
                    ValidateAs &= OutName & ": Non-ASCII characters can't be present" & vbCrLf
                    tempstr = in_data
                    Exit For
                End If
                'convert these chars to underscores
                If InStr(" /\@#$%^&*()![]{}:;',.<>?|=+-" + Chr(34), tempchr) <> 0 Then
                    tempchr = "_"
                End If
                'max 1 underscore in a row.
                If lastchr = "_" And tempchr = "_" Then Continue For
                tempstr &= tempchr
            Next
            'Modify passed variable by reference with underscored version.
            in_data = tempstr
        End If

        If AttObj.IsListOfSystems Then
            Dim tempchr As Char
            For c As Integer = 1 To Len(in_data)
                tempchr = LCase(Mid(in_data, c, 1))
                If AscW(tempchr) < 32 Or AscW(tempchr) > 127 Then
                    ValidateAs &= OutName & ": Non-ASCII characters can't be present" & vbCrLf
                    Exit For
                End If
                If InStr("/\@#$%^&*()![]{}:;',.<>?|=+-" + Chr(34), tempchr) <> 0 Then
                    ValidateAs &= OutName & ": One or more system names contain invalid characters" & vbCrLf
                    Exit For
                End If
            Next
        End If

        If AttObj.MustBeValidTransportName Then
            If Not AvailableTransports.Contains(LCase(in_data)) Then
                ValidateAs &= OutName & ": Must be a supported transport - " & Chr(34) & in_data & Chr(34) & " isn't one." & vbCrLf
            End If
        End If

    End Function

    Public Function Extract(in_object As Object)
        Extract = "[Type Not Supported - Report As Bug]"
        Select Case TypeName(in_object)
            Case "TextBox"
                Extract = Trim(in_object.Text)
            Case Else
                DebugOut("Extract([object of type " & TypeName(in_object) & "]): Type not supported.")

        End Select
    End Function

    Public Sub Implant(in_object As Object, in_textdata As String)
        Select Case TypeName(in_object)
            Case "TextBox"
                in_object.Text = Trim(in_textdata)
            Case Else
                DebugOut("Implant([object of type " & TypeName(in_object) & "]," & Chr(34) & in_textdata & Chr(34) & "): Type not supported.")
        End Select
    End Sub

End Module
