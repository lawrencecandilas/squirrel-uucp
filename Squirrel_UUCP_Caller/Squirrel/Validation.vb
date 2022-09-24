Module Validation
    Function Validation_DoesFileExist(in_fullpath As String) As Boolean
        'Returns true or false answering the question "Does file 'in_fullpath'
        'exist?
        '
        Validation_DoesFileExist = False
        If in_fullpath = "" Then
            DebugOut("Validation_DoesFileExist: in_fullpath was null")
            Exit Function
        End If
        DebugOut("Validation_DoesFileExist: " & Chr(34) & in_fullpath & Chr(34) & "?")
        If System.IO.File.Exists(Slashes(in_fullpath)) Then
            Validation_DoesFileExist = True
            Exit Function
        End If
        DebugOut(" File doesn't exist")
    End Function

    Function Validation_DoesDirectoryExist_DontMake(in_fullpath As String) As Boolean
        'Returns true or false answering the question "Does folder
        ''in_fullpath' exist?" - it will not make the folder if it doesn't
        'exist.
        '
        Validation_DoesDirectoryExist_DontMake = False
        If in_fullpath = "" Then
            DebugOut("Validation_DoesDirectoryExist_DontMake: in_fullpath was null")
            Exit Function
        End If
        DebugOut("Validation_DoesDirectoryExist_DontMake " & Chr(34) & in_fullpath & Chr(34) & "?")
        If System.IO.Directory.Exists(Slashes(in_fullpath)) Then
            Validation_DoesDirectoryExist_DontMake = True
            Exit Function
        End If
        DebugOut(" Folder doesn't exist")
    End Function
    Function Validation_DoesDirectoryExist(in_fullpath As String) As Boolean
        'Returns true or false answering the question "Does folder
        ''in_fullpath' exist?" - it should normally always answer True because
        'it will make the folder if it doesn't exist.
        '
        Validation_DoesDirectoryExist = False
        If in_fullpath = "" Then
            DebugOut("Validation_DoesDirectoryExist: in_fullpath was null")
            Exit Function
        End If
        DebugOut("Validation_DoesDirectoryExist " & Chr(34) & in_fullpath & Chr(34) & "?")
        If System.IO.Directory.Exists(Slashes(in_fullpath)) Then
            Validation_DoesDirectoryExist = True
            Exit Function
        End If
        DebugOut(" Folder doesn't exist - creating...")
        Try
            System.IO.Directory.CreateDirectory(Slashes(in_fullpath))
            Validation_DoesDirectoryExist = True
            Exit Function
        Catch
            DebugOut(" Error while creating folder")
            Validation_DoesDirectoryExist = False
            Exit Function
        End Try
    End Function

    Function Validation_callfile_systemname(in_string As String)
        'Check if in_string is a valid system name that would appear in the
        'UUCP 'call' configuration file.
        Validation_callfile_systemname = False
        If in_string = "" Then Exit Function 'Reject blank strings
        If Len(in_string) > 32 Then Exit Function 'Reject more than 32 chars

        'First character must be a letter
        If Asc(UCase(Left(in_string, 1))) < 65 Or Asc(UCase(Left(in_string, 1))) > 91 Then
            Exit Function
        End If

        If Len(in_string) > 1 Then
            'Reject spaces, non-ASCII characters, and a few other characters
            For x = 2 To Len(in_string)
                Dim x2 As Char = Mid(in_string, x, 1)
                If Asc(x2) < 33 Or Asc(x2) = 127 Then Exit Function
                If InStr("/:'\[]{}()$#*?" & Chr(34), x2) <> 0 Then Exit Function
            Next
        End If

        Validation_callfile_systemname = True
    End Function

    Function Validation_callfile_username(in_string As String)
        'Check if in_string is a valid username that would appear in the UUCP
        ''call' configuration file.
        Validation_callfile_username = False
        If Trim(in_string) = "" Then Exit Function

        If in_string = "" Then Exit Function 'Reject blank strings
        If Len(in_string) > 32 Then Exit Function 'Reject more than 32 chars

        'Reject spaces, non-ASCII characters, and a few other characters
        For x = 1 To Len(in_string)
            Dim x2 As Char = Mid(in_string, x, 1)
            If Asc(x2) < 33 Or Asc(x2) = 127 Then Exit Function
            If InStr("/:'\[]{}()$#*?" & Chr(34), x2) <> 0 Then Exit Function
        Next

        Validation_callfile_username = True
    End Function

    Function Validation_Portfile_portname(in_string As String)
        'Check if in_string is a valid port name
        Validation_Portfile_portname = False
        If Trim(in_string) = "" Then Exit Function

        If in_string = "" Then Exit Function 'Reject blank strings
        If Len(in_string) > 48 Then Exit Function 'Reject more than 48 chars

        'Reject spaces, non-ASCII characters, and a few other characters
        For x = 1 To Len(in_string)
            Dim x2 As Char = Mid(in_string, x, 1)
            If Asc(x2) < 33 Or Asc(x2) = 127 Then Exit Function
            If InStr("/:'\[]{}()$#*?" & Chr(34), x2) <> 0 Then Exit Function
        Next

        Validation_Portfile_portname = True
    End Function

    Function Validation_sysfile_systemname(in_string)
        'Check if in_string is a valid system name
        'This should follow the same rules as the names in the call file, so
        'we'll just use the same routine.
        Validation_sysfile_systemname = Validation_callfile_systemname(in_string)
    End Function

    Function Validation_settings_option(in_string)
        'Check if Squirrel settings option name is valid
        'Decided it's OK for this to follow the same rules as names in the
        'call file, so we'll just use the same routine.)
        Validation_settings_option = Validation_callfile_username(in_string)
    End Function

    Function IsNodenameValid(ByRef in_Nodename As String)
        IsNodenameValid = False

        'Blank?  Invalid, bounce immediately.
        If in_Nodename = "" Then Exit Function

        Dim tempchr As String = UCase(Mid(in_Nodename, 1, 1))
        'First character not a letter?  Bounce immediately.
        If AscW(tempchr) < 65 Or AscW(tempchr) > 90 Then Exit Function

        'Loop and look for very illegal characters (outside of printable ASCII
        'range.  Convert sorta illegal ones (most punctuation) to underscores.
        'Don't feel like arguing with Windows UUCP users about nodenames too
        'much.
        Dim tempstr As String = ""
        Dim lastchr As String = ""
        Dim c As Integer = 1
        For c = 1 To Len(in_Nodename)
            lastchr = tempchr
            tempchr = LCase(Mid(in_Nodename, c, 1))
            'unprintable ASCII is bounced as invalid
            If AscW(tempchr) < 32 Or AscW(tempchr) > 127 Then
                Exit Function
            End If
            'convert these chars to underscores
            If InStr(" /\@#$%^&*()![]{};;',.<>?|=+-" + Chr(34), tempchr) <> 0 Then
                tempchr = "_"
            End If
            'max 1 underscore in a row.
            If lastchr = "_" And tempchr = "_" Then Continue For
            tempstr &= tempchr
        Next

        'Modify passed variable by reference with underscored version.
        in_Nodename = tempstr
        'We're good.
        IsNodenameValid = True
    End Function
End Module
