Imports System.Numerics
Imports System.Security.Cryptography

Module Utility
    '==== Globally Accessible Settings Object ====
    'This object is globally accessible on purpose, because it contains the
    'application settings and anything should be able to access it.
    Public CurrentSettings As Object = New Settings

    '==== Globally Accessible Debug-Related Variables And Functions ====
    'Debug message behavior related variables.
    Public DebugMessageMode As Integer = 1
    Public DebugMessageEnable As Boolean = False
    Sub DebugOut(in_string As String)
        'Basic debug message output function.
        '
#If Not DEBUG Then
        'Debug can't be disabled in Debug mode.
        If DebugMessageEnable <> True Then Exit Sub
#End If
        If DebugMessageMode = 1 Then Debug.WriteLine(in_string)
    End Sub

    Public Class ConfigProblem
        Public Property Id As Integer
        Public Property Body As String
    End Class
    'Public list of all possible config file provlems
    Public ConfigProblems As New List(Of ConfigProblem) From
        {
            New ConfigProblem With {.Id = 0, .Body = "'config' file doesn't exist or is inaccessible"},
            New ConfigProblem With {.Id = 1, .Body = "Error when trying to create new 'config' file"},
            New ConfigProblem With {.Id = 2, .Body = "'Port' file doesn't exist or is inaccessible"},
            New ConfigProblem With {.Id = 3, .Body = "Error when trying to create new 'Port' file"},
            New ConfigProblem With {.Id = 4, .Body = "'sys' file doesn't exist or is inaccessible"},
            New ConfigProblem With {.Id = 5, .Body = "Error when trying to create new 'sys' file"},
            New ConfigProblem With {.Id = 6, .Body = "'call' file doesn't exist or is inaccessible, and unable to make one"},
            New ConfigProblem With {.Id = 7, .Body = "Bad system name, username, and/or missing field in 'call' file"},
            New ConfigProblem With {.Id = 8, .Body = "Bad port name in 'Port' file"},
            New ConfigProblem With {.Id = 9, .Body = "Bad system name in 'sys' file."},
            New ConfigProblem With {.Id = 10, .Body = "Cygwin environment location missing, undefined, or inaccessible."}
        }
    Public Class SquirrelComm
        'Handles communication with user, e.g. user or command errors.       
        Public Property ReportMode As Integer = 1
        '0 = Mute totally
        '1 = Msgbox
        'Other modes TBD
        Public Property Id As Integer
        Public Property Head As String
        Public Property Body As String
        Public Sub SetTbxCallInfo(in_tbx)
            in_tbx.Text = Body
        End Sub
        Public Sub UserNotice()
            If ReportMode = 1 Then MsgBox(Body, MsgBoxStyle.Information, Head)
        End Sub
        Public Sub UserError()
            DebugOut("Module1.SquirrelComm.UserError(): Begin error text")
            DebugOut("==============================================================================")
            DebugOut(Head)
            DebugOut(Body)
            DebugOut("==============================================================================")
            DebugOut("Module1.SquirrelComm.UserError(): End error text")

            If ReportMode = 1 Then MsgBox(Body, MsgBoxStyle.Exclamation, Head)
        End Sub
        Public Sub CommandError(in_commandExecuted As String, in_stdErrText As String)
            Dim AssembledBodyText As String =
                Body &
                vbCrLf & vbCrLf &
                "Command Executed: " & in_commandExecuted &
                vbCrLf & vbCrLf &
                "STDERR: " & in_stdErrText

            DebugOut("Module1.SquirrelComm.CommandError(): Begin error text")
            DebugOut("==============================================================================")
            DebugOut(Head)
            DebugOut(Body)
            DebugOut("==============================================================================")
            DebugOut("Module1.SquirrelComm.CommandError(): End error text")

            If ReportMode = 1 Then MsgBox(AssembledBodyText, MsgBoxStyle.Exclamation, Head)
        End Sub
        Public Sub SystemError(in_Info As String)
            Dim AssembledBodyText As String =
                Body &
                vbCrLf & vbCrLf &
                in_Info

            DebugOut("Module1.SquirrelComm.SystemError(): Begin error text")
            DebugOut("==============================================================================")
            DebugOut(Head)
            DebugOut(Body)
            DebugOut("==============================================================================")
            DebugOut("Module1.SquirrelComm.SystemError(): End error text")

            If ReportMode = 1 Then MsgBox(AssembledBodyText, MsgBoxStyle.Exclamation, Head)
        End Sub
    End Class

    'Public list of all possible errors and other communications.        
    Public SquirrelComms As New List(Of SquirrelComm) From
        {
            New SquirrelComm With {.Id = 0,
                                    .Head = "Can't Add File - No System Selected",
                                    .Body = "Select a system first from the [Systems] tab." & vbCrLf & vbCrLf &
                                            "Tip: If you set a system as a default system, it will automatically be selected when the application starts."
                                    },
            New SquirrelComm With {.Id = 1,
                                    .Head = "Can't Add File - Bad Recipient",
                                    .Body = "Recipient doesn't have a '!' in it." & vbCrLf & vbCrLf &
                                            "Example: remote-system-name!unix-name-of-recipient"
                                    },
            New SquirrelComm With {.Id = 2,
                                    .Head = "Can't Add File - Bad Recipient",
                                    .Body = "The first item in the recipient is the system you are calling." & vbCrLf &
                                            "You changed it to something else. " & vbCrLf &
                                            "It will be changed back to the currently selected system." & vbCrLf & vbCrLf &
                                            "To call a different system, select a different system in the Systems tab."
                                    },
            New SquirrelComm With {.Id = 3,
                                    .Head = "File Add Failed",
                                    .Body = "Error occurred when adding file."
                                    },
            New SquirrelComm With {.Id = 4,
                                    .Head = "Can't Add File - No Recipient",
                                    .Body = "Recipient must be a full UUCP bang path." & vbCrLf &
                                            "Example: remote-system-name!unix-name-of-recipient"
                                    },
            New SquirrelComm With {.Id = 5,
                                    .Head = "Executable Not Found",
                                    .Body = "The following executable wasn't found. " &
                                            "(You may need to update the Cygwin settings or refresh the Cygwin installation.)"
                                    },
            New SquirrelComm With {.Id = 6,
                                    .Head = "",
                                    .Body = "The big Call button below won't work until select a system to call from the Systems tab."
                                    },
            New SquirrelComm With {.Id = 7,
                                    .Head = "",
                                    .Body = "Press the big Call button below to call the selected system. " & vbCrLf & vbCrLf &
                                            "If that system has files waiting for you, it will send them to you when you call."
                                    },
            New SquirrelComm With {.Id = 8,
                                    .Head = "",
                                    .Body = "Multiple systems are selected.  To call one of them, make sure only one is selected."
                                    },
            New SquirrelComm With {.Id = 9,
                                    .Head = "",
                                    .Body = "Call process is now active." & vbCrLf & vbCrLf &
                                            "A console window opened to run the uucico command.  Check the console window for details."
                                    },
            New SquirrelComm With {.Id = 10,
                                    .Head = "",
                                    .Body = "Call process finished." & vbCrLf & vbCrLf &
                                            "Details from the call appear on the right." & vbCrLf & vbCrLf &
                                            "To make another call, press Call again."
                                    },
            New SquirrelComm With {.Id = 11,
                                    .Head = "",
                                    .Body = "Call process finished and reported an error." & vbCrLf & vbCrLf &
                                            "Details from the call appear on the right." & vbCrLf & vbCrLf &
                                            "To make another call, press Call again."
                                    },
            New SquirrelComm With {.Id = 12,
                                    .Head = "No 'etc\uucp\config' file",
                                    .Body = "'etc\uucp\config' doesn't exist or is inaccessible in the Cygwin root." & vbCrLf & vbCrLf &
                                            "The application will be mostly inactive until this is fixed."
                                    },
            New SquirrelComm With {.Id = 13,
                                    .Head = "No 'etc\uucp\sys' file",
                                    .Body = "'\etc\uucp\sys' doesn't exist or is inaccessible in the Cygwin root." & vbCrLf & vbCrLf &
                                            "A blank one will be created." & vbCrLf &
                                            "Use the Systems tab to add systems that you want to call."
                                    },
            New SquirrelComm With {.Id = 14,
                                    .Head = "Invalid nodename",
                                    .Body = "The UUCP nodename can't ..." & vbCrLf &
                                            "- be blank," & vbCrLf &
                                            "- have any spaces or non-ASCII characters," & vbCrLf &
                                            "- start with anything but a letter" & vbCrLf & vbCrLf &
                                            "Try again!"
                                    },
            New SquirrelComm With {.Id = 15,
                                    .Head = "",
                                    .Body = "No systems are setup to call.  Click on the Systems tab and use the Add Systems button to add one from a invitation file."
                                    },
            New SquirrelComm With {.Id = 16,
                                    .Head = "System already exists",
                                    .Body = "You can't have multiple systems with the same name, and overwriting them isn't supported (you need to remove the existing one first)."
                                    },
            New SquirrelComm With {.Id = 17,
                                    .Head = "SSH key file doesn't exist",
                                    .Body = "The SSH key file that's set doesn't exist, or is inaccessible."},
            New SquirrelComm With {.Id = 18,
                                    .Head = "SSH key file copying failed",
                                    .Body = "An error occurred while copying the SSH key file to the UUCP config folder."},
            New SquirrelComm With {.Id = 19,
                                    .Head = "Creating new port failed",
                                    .Body = "An error ocurred while writing a new port to the UUCP Port config file." & vbCrLf & vbCrLf &
                                            "Because of this, the system wasn't added."
                                    },
            New SquirrelComm With {.Id = 20,
                                    .Head = "Creating new system failed",
                                    .Body = "An error ocurred while writing a new system to the UUCP Port config file." & vbCrLf & vbCrLf &
                                            "Warning: The UUCP system file might be corrupt or the system may not work properly."
                                    },
            New SquirrelComm With {.Id = 21,
                                    .Head = "SSH key file copydown failed",
                                    .Body = "An error occurred while copying the SSH key file from the invite to the UUCP config folder." & vbCrLf & vbCrLf &
                                            "Because of this, the system wasn't added."
                                    },
            New SquirrelComm With {.Id = 22,
                                    .Head = "Invite problem",
                                    .Body = "Invite file is invalid." & vbCrLf &
                                            "Specific problem: No transport type specified" & vbCrLf & vbCrLf &
                                            "Because of this, the system wasn't added."
                                    },
            New SquirrelComm With {.Id = 23,
                                    .Head = "Invite problem",
                                    .Body = "Invite file is invalid." & vbCrLf &
                                            "Specific problem: Unsupported transport type" & vbCrLf & vbCrLf &
                                            "Because of this, the system wasn't added."
                                    },
            New SquirrelComm With {.Id = 24,
                                    .Head = "Completed",
                                    .Body = "Config recheck completed." & vbCrLf & vbCrLf &
                                            "Cygwin tab will show updated results now."
                                    },
            New SquirrelComm With {.Id = 25,
                                    .Head = "Saving call log failed",
                                    .Body = "An error occurred while trying saving the call log file.  If you are in a pinch you can copy and paste the content."
                                    },
            New SquirrelComm With {.Id = 26,
                                    .Head = "Deleting key file failed",
                                    .Body = "An error occurred while trying to delete the key file."
                                    },
            New SquirrelComm With {.Id = 27,
                                    .Head = "Warning",
                                    .Body = "This is what 'Forward For' means:" & vbCrLf & vbCrLf &
                                            "- Systems with 'Forward For' enabled can send you files for other systems" & vbCrLf &
                                            "- You might receive files to hold for other systems during calls with 'Forward For'-checked systems" & vbCrLf &
                                            "- Files you're holding can only go to systems that have 'Forward To' checked - when you call them" & vbCrLf &
                                            "- Changing this option doesn't notify anybody - you have to do that yourself" & vbCrLf &
                                            vbCrLf & "This is just to let you know what's going on."
                                    },
            New SquirrelComm With {.Id = 28,
                                    .Head = "Warning",
                                    .Body = "This is what 'Forward To' means:" & vbCrLf & vbCrLf &
                                            "- Systems that have 'Forward For' checked can send you files to hold for this system" & vbCrLf &
                                            "- Files you are holding for other systems will be delivered when you call them" & vbCrLf &
                                            "- Changing this option doesn't notify anybody - you have to do that yourself" & vbCrLf &
                                            vbCrLf & "This is just to let you know what's happening"
                                    },
            New SquirrelComm With {.Id = 29,
                                    .Head = "Where Is The Cygwin Environment?",
                                    .Body = "The Cygwin environment is missing or undefined.  Go to the [Cygwin] tab and specify where it is.  Please visit this application's Github page if you need to redownload the Cygwin environment."
                                    },
            New SquirrelComm With {.Id = 30,
                                    .Head = "Cygwin Environment Is Bad",
                                    .Body = "The Cygwin environment has one or more reported problems.  Go to the [Cygwin] tab to investigate and fix.  Please visit this application's Github page if you need to redownload the Cygwin environment."
                                    },
            New SquirrelComm With {.Id = 31,
                                    .Head = "Restart Needed",
                                    .Body = "Please restart Squirrel UUCP Caller for the changes to take effect."
                                    },
            New SquirrelComm With {.Id = 32,
                                    .Head = "",
                                    .Body = "The Call button is disabled because there is an issue with the UUCP configuration files." & vbCrLf & vbCrLf &
                                            "See the [Cygwin] tab."
                                    }
        }

    '-------------------------------------------------------------------------
    ' Command Invocation Functions
    '-------------------------------------------------------------------------

    Function ExecuteCommandInWindow(in_binpath As String, in_args As String) As Integer
        DebugOut("Module1.ExecuteCommandInWindow(" &
                 Chr(34) & in_binpath & Chr(34) & ", " &
                 Chr(34) & in_args & Chr(34) &
                 ") ...")
        'Runs a command-line executable, and lets it pop open a window.
        'Because the output is visible on the console window, we don't capture
        'stdout or stderr.
        '
        'I could not use CommandConnector below to successfully funnel Cygwin's
        'command stdout to a textbox live - when I tried, it would buffer and
        'not issue events until the command was done.
        '
        'This is a workaround.  Hoping in the future it can be figured out.
        '


        'Return -32767 if the command executable doesn't even exist.
        If Not Microsoft.VisualBasic.FileIO.FileSystem.FileExists(in_binpath) Then
            SquirrelComms.Item(5).SystemError(in_binpath)
            ExecuteCommandInWindow = -32767
            Exit Function
        End If

        Dim p As New Process()
        Dim pInfo As New ProcessStartInfo()

        pInfo.FileName = in_binpath
        pInfo.Arguments = in_args
        pInfo.CreateNoWindow = False
        pInfo.UseShellExecute = False
        pInfo.RedirectStandardOutput = False
        pInfo.RedirectStandardError = False
        p.EnableRaisingEvents = False
        p.StartInfo = pInfo

        p.Start()

        DebugOut(" started, waiting For Exit")
        p.WaitForExit()

        DebugOut(" command returned " & p.ExitCode())

        Return p.ExitCode()

    End Function
    Function ExecuteCommand(in_binpath As String, in_args As String) As Collection
        DebugOut("Module1.ExecuteCommand(" &
                 Chr(34) & in_binpath & Chr(34) & ", " &
                 Chr(34) & in_args & Chr(34) &
                 ") ...")
        'Runs a command-line executable, without showing a console window.

        'Return error if command doesn't even exist.
        If Not Microsoft.VisualBasic.FileIO.FileSystem.FileExists(in_binpath) Then
            SquirrelComms.Item(5).SystemError(in_binpath)
            ExecuteCommand = New Collection From {-32767, in_binpath & " didn't run because it wasn't found", in_binpath & " wasn't found"}
            Exit Function
        End If

        'Returns a collection with 3 items.
        '   Item(1) = return value of command
        '   Item(2) = stdout as string
        '   Item(3) = stderr as string

        Dim p As New Process()
        Dim pInfo As New ProcessStartInfo()

        pInfo.FileName = in_binpath
        pInfo.Arguments = in_args
        pInfo.CreateNoWindow = True
        pInfo.UseShellExecute = False
        pInfo.RedirectStandardOutput = True
        pInfo.RedirectStandardError = True
        p.EnableRaisingEvents = False
        p.StartInfo = pInfo

        p.Start()
        DebugOut(" started, waiting for exit")

        p.WaitForExit()

        ExecuteCommand = New Collection From {p.ExitCode, p.StandardOutput.ReadToEnd, p.StandardError.ReadToEnd}

        DebugOut(" Command completed")
        DebugOut(" == BEGIN STDOUT ========================================================")
        DebugOut(" " & ExecuteCommand.Item(2))
        DebugOut(" ==   END STDOUT ========================================================")
        DebugOut(" == BEGIN STDERR ========================================================")
        DebugOut(" " & ExecuteCommand.Item(3))
        DebugOut(" ==   END STDERR ========================================================")
        DebugOut(" command returned " & ExecuteCommand.Item(1))
    End Function
    Sub CommandConnector(in_workingdir As String, in_binpath As String, in_args As String, receiver As Object)
        DebugOut("Module1.CommandConnector(" &
            Chr(34) & in_workingdir & Chr(34) & ", " &
            Chr(34) & in_binpath & Chr(34) & ", " &
            Chr(34) & in_args & Chr(34) & ")" &
            ")")
        'Executes a command and forwards its stdout and stderr text out to an object.
        '"receiver" must implement CommandIsDone, EatStderrLine, and EatStdoutLine methods.
        '
        'TODO: Isn't this what Interfaces do?  

        'Make some noise if command doesn't even exist.
        If Not Microsoft.VisualBasic.FileIO.FileSystem.FileExists(in_binpath) Then
            SquirrelComms.Item(5).SystemError(in_binpath)
            Exit Sub
        End If

        Dim p As New Process()
        Dim pInfo As New ProcessStartInfo()

        pInfo.FileName = in_binpath
        pInfo.Arguments = in_args
        pInfo.CreateNoWindow = True
        pInfo.UseShellExecute = False
        pInfo.RedirectStandardOutput = True
        pInfo.RedirectStandardError = True
        pInfo.WorkingDirectory = in_workingdir
        p.EnableRaisingEvents = True
        p.StartInfo = pInfo

        AddHandler p.Exited, Sub(ByVal sender As Object, ByVal e As System.EventArgs)
                                 'receiver.CommandIsDone()
                                 Dim inv As New MethodInvoker(Sub() receiver.CommandIsDone)
                                 inv.Invoke()
                             End Sub

        AddHandler p.ErrorDataReceived, Sub(ByVal sender As Object, ByVal e As System.Diagnostics.DataReceivedEventArgs)
                                            If (Not String.IsNullOrEmpty(e.Data)) Then
                                                receiver.EatStderrLine(e.Data)
                                            End If
                                        End Sub

        AddHandler p.OutputDataReceived, Sub(ByVal sender As Object, ByVal e As System.Diagnostics.DataReceivedEventArgs)
                                             If (Not String.IsNullOrEmpty(e.Data)) Then
                                                 receiver.EatStdoutLine(e.Data)
                                             End If
                                         End Sub

        p.Start()
        DebugOut(" started")

        p.BeginOutputReadLine()
        p.BeginErrorReadLine()

    End Sub


    '-------------------------------------------------------------------------
    ' Parameter Processing Functions
    '-------------------------------------------------------------------------

    Sub SplitOnFirstSpace(in_string As String, ByRef out_strings() As String)
        'Takes a string that is 2 things separated by a space and puts each
        'thing in its own array element.
        '
        'out_strings() needs to be at least a 2-element string array
        '
        out_strings(1) = ""
        out_strings(2) = ""
        in_string = Trim(in_string)

        If in_string = "" Then Return
        Dim t As Integer = InStr(in_string, " ")

        If t = 0 Then
            out_strings(1) = in_string
            Return
        End If

        out_strings(1) = RTrim(Left(in_string, t))
        out_strings(2) = Trim(Mid(in_string, t + 1))
    End Sub
    Function SplitOnWhitespace(in_string As String) As List(Of String)
        'Converts a string of whitespace-delimited tokens into a list of
        'Strings.
        '
        SplitOnWhitespace = New List(Of String)
        If in_string = "" Then Exit Function
        Dim s As String = ""
        Dim i As Integer
        For i = 1 To Len(in_string)
            If Asc(Mid(in_string, i, 1)) > 33 Then
                s &= Mid(in_string, i, 1)
            Else
                If s <> "" Then
                    SplitOnWhitespace.Add(s)
                    s = ""
                End If
            End If
        Next

        If s <> "" Then
            SplitOnWhitespace.Add(s)
        End If
    End Function
    Function SplitBySpaces(in_string As String) As List(Of String)
        'Convert string of space-deliminted tokens to a list
        Dim Workspace As New List(Of String)
        in_string = Trim(in_string)

        If in_string = "" Then
            SplitBySpaces = Workspace
            Exit Function
        End If

        Dim cstart As Integer = 1
        Dim cend As Integer = Len(in_string)
        Dim ctoken As String = ""
        Dim cnextflag As Integer = 0
        Dim endflag As Boolean = False

        Do
            cend = InStr(cstart, in_string, " ") - 1
            If cend = -1 Then
                cend = Len(in_string)
                endflag = True
            End If
            Dim tempstr As String = Trim(Mid(in_string, cstart, (cend - cstart) + 1))
            If tempstr <> "" Then Workspace.Add(tempstr)
            cstart = cend + 2
            cnextflag = 0
        Loop Until endflag

        SplitBySpaces = Workspace
    End Function
    Sub ThrowOnThePile(ByRef in_pile As List(Of String), in_option As String)
        'Converts whitespace delimited items in 'in_option' and appends
        'tokens that don't already exist to the list at 'in_pile'.
        '
        If in_option = "" Then Exit Sub
        Dim TempList As New List(Of String)
        TempList = SplitOnWhitespace(in_option)
        If TempList.Count = 0 Then Exit Sub
        For Each x In TempList
            If Not in_pile.Contains(x) Then in_pile.Add(x)
        Next
    End Sub
    Function OptionListPreview(in_option_line As String) As String
        'Takes a list of strings and converts them back into a single string.
        'Intended so we can show the end user contents of a list in dialogs.
        '
        OptionListPreview = ""
        'Dim SkipFirstFlag As Boolean = True
        Dim s As String = ""
        Dim i As Integer
        in_option_line = Trim(in_option_line)
        For i = 1 To Len(in_option_line)
            If Asc(Mid(in_option_line, i, 1)) > 33 Then
                s &= Mid(in_option_line, i, 1)
            Else
                If s <> "" Then
                    'If SkipFirstFlag Then
                    ' SkipFirstFlag = False
                    'Else
                    OptionListPreview &= s & " "
                    'End If
                    s = ""
                End If
            End If
        Next

        If s <> "" Then
            OptionListPreview &= s
        End If
    End Function
    Sub uustatSplit(in_string As String, ByRef out_strings() As String, in_Winpathof_cygwinroot As String)
        '*** out_strings() needs to be at least a 5-element string array
        'Takes a line from uustat output and splits it into an array, so it
        ' can be added to the lsvFilesToSend list view most likely.
        '
        'Example uustat line:
        'tenebrarum.duckdns.org.NQKlzJmAAAK3 tenebrarum.duckdns.org lawrence 07-13 12:16 Sending /home/lawrence/taylor-uucp-master/uux.o (75127 bytes) to ~/receive/lawrence/sharkie/
        '
        out_strings(1) = "" 'Job ID, token 1 of uustat line
        out_strings(2) = "" 'Destination System, token 2 of uustat line
        out_strings(3) = "" 'Recipient, token 11 of uustat line
        out_strings(4) = "" 'File, token 7 of uustat line
        out_strings(5) = "" 'Size, token 8 of uustat line, minus preceding "("

        in_string = Trim(in_string)
        If in_string = "" Then Return

        'Variables for parsing.
        Dim cstart As Integer = 1
        Dim cend As Integer = Len(in_string)
        Dim ctoken As String = ""
        Dim clastflag As Boolean = False
        Dim tokens As New Collection

        Do
            cend = InStr(cstart, in_string, " ") - 1
            If cend = -1 Then
                cend = Len(in_string)
                clastflag = True
            End If
            ctoken = Mid(in_string, cstart, (cend - cstart) + 1)
            tokens.Add(ctoken)
            cstart = cend + 2
        Loop While clastflag = False

        out_strings(1) = tokens.Item(1)
        out_strings(2) = tokens.Item(2)
        out_strings(3) = tokens.Item(tokens.Count)
        out_strings(5) = Mid(tokens.Item(tokens.Count - 3), 2)

        For i As Integer = 7 To tokens.Count - 4
            out_strings(4) &= " " & tokens.Item(i)
        Next
        out_strings(4) = Cyg2WinPath(Trim(out_strings(4)), in_Winpathof_cygwinroot)

    End Sub
    Sub ExtractPort(in_string As String, ByRef out_strings() As String, DefaultPort As Integer)
        'Takes a string that is an address and a port separated by a colon and
        'puts each thing in its own array element.
        '
        'If port is not specified, the supplied default port will be populated
        'in the 2nd array.
        '
        'out_strings() needs to be at least a 2-element string array
        '
        out_strings(1) = ""
        out_strings(2) = ""
        in_string = Trim(in_string)

        If in_string = "" Then Return
        Dim t As Integer = InStr(in_string, ":")

        If t = 0 Then
            out_strings(1) = in_string
            out_strings(2) = DefaultPort.ToString
            Exit Sub
        End If

        If t = 1 Then
            out_strings(1) = ""
            out_strings(2) = ""
            Exit Sub
        End If

        out_strings(1) = RTrim(Left(in_string, t - 1))
        out_strings(2) = Trim(Mid(in_string, t + 1))

        If out_strings(2) = "" Then out_strings(2) = DefaultPort.ToString
    End Sub


    '-------------------------------------------------------------------------
    ' File Path Processing Functions
    '-------------------------------------------------------------------------

    Function RandStr() As String
        'Return string of 16 pseudo-random characters.
        'Not cryptographically safe.
        'Used to make random port names and key filenames. 
        '
        Randomize()
        RandStr = ""
        For i = 0 To 15
            RandStr &= Chr(Int(Rnd(1) * 26) + 65)
        Next
    End Function
    Function Slashes(in_string As String)
        'Convert forward slashes to backslashes.
        'Also converts double slashes to single slashes
        '
        Slashes = ""
        Dim out_string As String = ""
        If in_string = "" Then Exit Function

        Dim i As Integer
        Dim lt As Char
        Dim t As Char = ""

        For i = 1 To in_string.Length
            lt = t
            t = Mid(in_string, i, 1)
            If t = "\" And lt = "\" Then Continue For
            If t = "/" Then
                out_string &= "\"
            Else
                out_string &= t
            End If
        Next

        Slashes = out_string
    End Function

    Function Cyg2WinPath(in_cygpath As String, in_Winpathof_cygwinroot As String) As String
        'Convert Cygwin UNIX path to Windows path.
        '
        Cyg2WinPath = ""
        in_cygpath = Trim(in_cygpath)
        If in_cygpath = "" Then Exit Function

        Dim temp2 As String

        If Left(in_cygpath, 10) = "/cygdrive/" Then
            temp2 = UCase(Mid(in_cygpath, 11, 1)) & ":\" & Mid(in_cygpath, 13)
        Else
            temp2 = in_Winpathof_cygwinroot & "\" & Mid(in_cygpath, 2)
        End If

        Dim temp3 As String = ""
        Dim t As String = ""
        Dim lt As String = ""

        For i = 1 To Len(temp2)
            lt = t
            t = Mid(temp2, i, 1)
            If t = "/" Then
                If (lt <> "/" And lt <> "\") Then
                    temp3 &= "\"
                End If
            Else
                temp3 &= t
            End If
        Next

        Cyg2WinPath = temp3

    End Function
    Function Win2CygPath(in_winpath As String) As String
        'Takes a Windows path and converts it to a Cygwin UNIX path.
        '
        Win2CygPath = ""

        in_winpath = Trim(in_winpath)
        If in_winpath = "" Then Exit Function

        Dim temp2 As String = ""

        If (Mid(in_winpath, 2, 2) = ":\") Then
            temp2 = "\cygdrive\" & LCase(Left(in_winpath, 1)) & "\" & Mid(in_winpath, 4)
        Else
            temp2 = "\cygdrive\c\" & in_winpath 'todo: should use current drive
        End If

        Dim temp3 As String = ""
        Dim t As String = ""

        For i = 1 To Len(temp2)
            t = Mid(temp2, i, 1)
            If t = "\" Then
                temp3 &= "/"
            Else
                temp3 &= t
            End If
        Next

        Win2CygPath = temp3
    End Function


    '-------------------------------------------------------------------------
    ' Cygwin Environment Check
    '-------------------------------------------------------------------------

    Public Class CygwinCheck
        'Descriptions and result holders for individual Cygwin checks below.
        '
        'A CygwinCheck object is intended to be used by the code that builds
        'the listview to report the configuration status to the user.
        '
        Public Property Id As Integer
        Public Property Desc As String
        Public Property Pass As Boolean
    End Class

    Public CygwinChecks As New List(Of CygwinCheck)

    Function CygwinEnvironmentCheck(in_conf As Object) As Boolean
        DebugOut("Module1.CygwinEnvironmentCheck() ...")
        CygwinEnvironmentCheck = True
        'Check all non-negotiable aspects of the Cygwin environment.
        'A failure here means the Cygwin environment needs to be reinstalled,
        'redownloaded, or otherwise fixed.

        CygwinChecks.Clear()

        ' 1 - Does Cygwin root exist?
        CygwinChecks.Insert(0, New CygwinCheck With {.Id = 0, .Desc = "Cygwin root directory: " & Slashes(in_conf.Winpathof_cygwinroot)})
        CygwinChecks.Item(0).Pass = Validation_DoesDirectoryExist_DontMake(Slashes(in_conf.Winpathof_cygwinroot))
        If Not CygwinChecks.Item(0).Pass Then
            DebugOut(" Cygwin root not existing might mean it needs to be reinstalled - reporting failure now")
        Else
            ' 2 - Does the executable we're calling for uuto exist?
            CygwinChecks.Insert(0, New CygwinCheck With {.Id = 1, .Desc = "uuto executable: " & Slashes(in_conf.Winpathof_uuto)})
            CygwinChecks.Item(0).Pass = Validation_DoesFileExist(in_conf.Winpathof_uuto)
            ' 3 - Does the executable we're calling for uustat exist?
            CygwinChecks.Insert(0, New CygwinCheck With {.Id = 2, .Desc = "uustat executable: " & Slashes(in_conf.Winpathof_uustat)})
            CygwinChecks.Item(0).Pass = Validation_DoesFileExist(in_conf.Winpathof_uustat)
            ' 4 - Does the executable we're calling for bash exist?
            CygwinChecks.Insert(0, New CygwinCheck With {.Id = 3, .Desc = "bash executable: " & Slashes(in_conf.Winpathof_bash)})
            CygwinChecks.Item(0).Pass = Validation_DoesFileExist(in_conf.Winpathof_bash)
            ' 5 - Does /etc/uucp in the Cygwin environment exist?
            CygwinChecks.Insert(0, New CygwinCheck With {.Id = 4, .Desc = "/etc/uucp directory (in Cygwin root): " & Slashes(in_conf.Winpathof_etcuucp)})
            CygwinChecks.Item(0).Pass = Validation_DoesDirectoryExist(in_conf.Winpathof_etcuucp)
            ' 6 - Does /usr/spool/uucppublic/receive in the Cygwin environment
            CygwinChecks.Insert(0, New CygwinCheck With {.Id = 5, .Desc = "/usr/spool/uucp/uucppublic directory (in Cygwin root): " & Slashes(in_conf.Winpathof_uucppublic)})
            CygwinChecks.Item(0).Pass = Validation_DoesDirectoryExist(in_conf.Winpathof_uucppublic)
        End If

        For Each Result In CygwinChecks
            If Result.Pass = False Then CygwinEnvironmentCheck = False : Exit For
        Next

        DebugOut(" did it pass the check? " & CygwinEnvironmentCheck.ToString)
    End Function

    '-------------------------------------------------------------------------
    ' UUCP Default File Creation Functions
    '-------------------------------------------------------------------------

    Function NewUUCPCallFile(in_fullFilePath As String) As Boolean
        DebugOut("Module1.NewUUCPCallFile(" &
                 "in_fullFilePath = " & Chr(34) & in_fullFilePath & Chr(34) &
                 ") ..."
                )
        'Creates a new UUCP 'call' file.
        'The call file contains the UUCP usernames and passwords, per system.
        'The uucico on the answering end will want this.  It is not an actual
        'login username/password, just one looked up by the remote uucico.
        '

        'Returns true if no errors, false if there were errors

        Try
            System.IO.File.WriteAllText(in_fullFilePath,
            "#" & vbLf &
            "# call        Generated by Squirrel UUCP Caller" & vbLf &
            "#              " & Now & vbLf &
            "#" & vbLf &
            "# NOTE: For some weird reason at least one comment line needs to" & vbLf &
            "# exist before any system-username-password lines, otherwise" & vbLf &
            "# uucico produces the error 'Bad login'." & vbLf &
            "" & vbLf
            )
            DebugOut(" file written, reporting success")
            NewUUCPCallFile = True
        Catch
            DebugOut(" error writing new call file, reporting failure")
            NewUUCPCallFile = False
        End Try
    End Function
    Function NewUUCPSysFile(in_fullFilePath As String) As Boolean
        DebugOut("Module1.NewUUCPSysFile(" &
                 "in_fullFilePath = " & Chr(34) & in_fullFilePath & Chr(34) &
                 ") ..."
                )
        'Creates a new UUCP 'sys' file.
        'The 'sys' file names the systems that the node can either call, or
        'nodes that are simply known.  Systems in this file will point to a
        'port in the `Port` file, if they are intended to be called.
        '
        'Default forwarding and protocol options also live here.
        '

        'Returns true if no errors, false if there were errors

        Try
            System.IO.File.WriteAllText(in_fullFilePath,
            "#" & vbLf &
            "# sys          Generated by Squirrel UUCP Caller" & vbLf &
            "#              " & Now & vbLf &
            "# First some defaults.  These are for all other entries (unless overridden)" & vbLf &
            "#" & vbLf &
            "protocol gvG" & vbLf &
            "protocol-parameter G packet-size 1024" & vbLf &
            "# protocol-parameter G window 7" & vbLf &
            "protocol-parameter G short-packets" & vbLf &
            "" & vbLf
            )
            DebugOut(" file written, reporting success")
            NewUUCPSysFile = True
        Catch
            DebugOut(" error writing new Port file, reporting failure")
            NewUUCPSysFile = False
        End Try
    End Function
    Function NewUUCPPortFile(in_fullFilePath As String) As Boolean
        DebugOut("Module1.NewUUCPPortFile(" &
                 "in_fullFilePath = " & Chr(34) & in_fullFilePath & Chr(34) &
                 ") ..."
                 )
        'Creates a new UUCP 'Port' file.
        'The 'Port' file contains the settings and/or commands used to
        'carry UUCP data between caller and receiver.
        '

        'Returns true if no errors, false if there were errors

        Try
            System.IO.File.WriteAllText(in_fullFilePath,
            "#" & vbLf &
            "# Port         Generated by Squirrel UUCP Caller" & vbLf &
            "#              " & Now & vbLf &
            "#" & vbLf &
            "" & vbLf &
            "# Descriptionm for the TCP port - pretty trivial.  DON'T DELETE." &
            "#" & vbLf &
            "port TCP" & vbLf &
            "type TCP" & vbLf &
            "" & vbLf
            )
            DebugOut(" file written, reporting success")
            NewUUCPPortFile = True
        Catch
            DebugOut(" error writing new Port file, reporting failure")
            NewUUCPPortFile = False
        End Try

    End Function
    Function NewUUCPConfigFile(in_fullFilePath As String, in_desiredNodeName As String) As Boolean
        DebugOut("Module1.NewConfigFile(" &
                 "in_fullFilePath = " & Chr(34) & in_fullFilePath & Chr(34) & ", " &
                 "in_desiredNodeName = " & Chr(34) & in_desiredNodeName & Chr(34) &
                 ") ..."
                 )
        'Creates a new UUCP 'config' file.
        'The 'config' file contains the UUCP default nodename.
        '

        'Returns true if no errors, false if there were errors

        'Use current computer name as nodename if one is not specified.
        in_desiredNodeName = Left(Trim(in_desiredNodeName), 32)
        If in_desiredNodeName = "" Then
            in_desiredNodeName = My.Computer.Name
        End If

        Try
            System.IO.File.WriteAllText(in_fullFilePath,
            "#" & vbLf &
            "# config       Generated by Squirrel UUCP Caller" & vbLf &
            "#              " & Now & vbLf &
            "#" & vbLf &
            "" & vbLf &
            "nodename       " & in_desiredNodeName & vbLf &
            "" & vbLf &
            "# Defaults" & vbLf &
            "sysfile        /etc/uucp/sys" & vbLf &
            "portfile       /etc/uucp/Port" & vbLf &
            "dialfile       /etc/uucp/dial" & vbLf &
            "callfile       /etc/uucp/call" & vbLf &
            "passwdfile     /etc/uucp/passwd" & vbLf &
            "" & vbLf
            )
            DebugOut(" file written, reporting success")
            NewUUCPConfigFile = True
        Catch
            DebugOut(" error writing new config file, reporting failure")
            NewUUCPConfigFile = False
        End Try

    End Function
End Module
