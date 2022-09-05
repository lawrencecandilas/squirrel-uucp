Imports System.ComponentModel.Design
Imports System.Diagnostics.Eventing
Imports System.Runtime.InteropServices
Imports System.Runtime.Serialization

Public Class Settings
    Public Filename As String = "squirrelconfig.txt"
    Private Persistent As Boolean = True
    Private Path As String = ""

    'Configuration options.
    'Not using anything elaborate since there are only a few.
    Public CYGROOT As String = ""
    Public DEFAULTSYS As String = ""
    Public RECOVERY As String = ""

    Public Sub PortableMode()
        DebugOut("SettingsClasses.PortableMode()")
        'Default value of Persistent is true.
        'External code should call this when it believes it won't be
        'reading or writing settings - i.e. "portable mode" and obviously
        'that should be done pretty early.
        Persistent = False
    End Sub

    Private Function SetPathAttempt(in_path)
        'Check if our config file exists in a path.
        'If it does, set Path to it if it does.
        'See DetermineSettingsFile() called by LoadSettings() and
        ' SaveSettings().
        SetPathAttempt = False
        If IO.File.Exists(in_path & "\" & Filename) Then
            DebugOut("Found UUCP Squirrel Caller settings at " & Chr(34) & in_path & Chr(34))
            DebugOut("NOTICE: If settings are modified, they will be saved to this file")
            Path = in_path
            SetPathAttempt = True
        End If
    End Function

    Public Sub DetermineSettingsFile()
        DebugOut("SettingsClasses.DetermineSettingsFile() ...")
        'Run down the list of possible settings files and use first one found.
        'If none found, we'll use %appdata% like we should.

        'LoadSettings() and SaveSettings() call this, and they won't call this
        'if Persistent is false, so this shouldn't happen.
        If Not Persistent Then
            DebugOut(" BUG: Persistent is False, this function shouldn't have been called")
            Exit Sub
        End If

        'Official filepath for settings.  Could be overriden if it appears elsewhere.
        Path = Environment.SpecialFolder.ApplicationData

        'Let's check for elsewhere.
        Do
            If SetPathAttempt(Environment.SpecialFolder.ApplicationData) Then Exit Do
            If SetPathAttempt(AppContext.BaseDirectory) Then Exit Do
            If SetPathAttempt("C:\") Then Exit Do
            Path = AppContext.BaseDirectory
            DebugOut("No settings file was found.  Using " & Chr(34) & Path & Chr(34))
            DebugOut("NOTICE: When Squirrel settings are saved, they will be saved to this file")
            DebugOut("NOTICE: Squirrel settings do not contain UUCP call, system, or port information")
            Exit Do
        Loop

    End Sub

    Public Sub LoadSettings()
        DebugOut("SettingsClasses.LoadSettings() ...")
        If Not Persistent Then
            DebugOut(" portable mode - not actually loading settings")
            Exit Sub
        End If
        If Path = "" Then DetermineSettingsFile()
        If Path = "" Then
            DebugOut(" ERROR: failed to determine a file to read settings from - settings not loaded")
            Exit Sub
        End If


        If Not IO.File.Exists(Path & "\" & Filename) Then
            DebugOut(" settings file " & Chr(34) & Filename & Chr(34) & " not found at expected path " & Chr(34) & Path & Chr(34))
            Exit Sub
        End If

        Dim ReadOK As Boolean = True

        Dim Changes As New Hashtable

        Try
            Dim Reader As System.IO.StreamReader
            Reader = System.IO.File.OpenText(Path & "\" & Filename)

            Dim LineCount As Integer = 0
            Dim splitLine(2) As String

            Do Until Reader.EndOfStream
                LineCount += 1
                If LineCount = 10 Then
                    ReadOK = False
                    DebugOut(" ERROR: settings file has more lines than allowed - treating as invalid")
                    Exit Do
                End If

                Dim fileLine As String = Trim(Reader.ReadLine)
                If fileLine = "" Then Continue Do

                If Len(fileLine) > 2048 Then
                    ReadOK = False
                    DebugOut(" ERROR: a line in the settings file has more than 2048 characters - treating as invalid")
                    Exit Do
                End If

                SplitOnFirstSpace(fileLine, splitLine)
                If Not Validation_settings_option(splitLine(1)) Then
                    ReadOK = False
                    DebugOut(" ERROR: an option in the settings file is not valid - treating as invalid")
                    Exit Do
                End If

                Changes(UCase(splitLine(1))) = Trim(splitLine(2))
            Loop
            Reader.Dispose()
        Catch ex As Exception
            ReadOK = False
            DebugOut(" ERROR: error occurred while reading settings at " & Chr(34) & Path & Chr(34))
        End Try

        If Not Changes.ContainsKey("COMPLETED") Then
            ReadOK = False
            DebugOut(" ERROR: COMPLETED keyword not found - last settings save may not have completed - treating as invalid")
        End If

        If ReadOK Then
            For Each Item In Changes
                Dim Assignedflag As Boolean = False
                DebugOut(" option " &
                         Chr(34) & Item.Key & Chr(34) &
                         "=" &
                         Chr(34) & Item.value & Chr(34))
                If Item.Key = "CYGROOT" Then
                    Assignedflag = True
                    CYGROOT = Item.Value
                End If
                If Item.Key = "DEFAULTSYS" Then
                    Assignedflag = True
                    DEFAULTSYS = Item.Value
                End If
                If Item.Key = "RECOVERY" Then
                    Assignedflag = True
                    RECOVERY = Item.Value
                End If
                If Item.Key = "COMPLETED" Then
                    Assignedflag = True
                End If
                If Not Assignedflag Then
                    DebugOut(" NOTICE: last option above is not supported by this version")
                End If
            Next
        End If

    End Sub

    Public Sub SaveSettings()
        DebugOut("SettingsClasses.SaveSettings() ...")
        If Not Persistent Then
            DebugOut(" portable mode - not actually saving settings")
            Exit Sub
        End If

        If Path = "" Then DetermineSettingsFile()
        If Path = "" Then
            DebugOut(" ERROR: failed to determine a file to store settings to - settings not saved")
            Exit Sub
        End If

        Dim WriteOK As Boolean = True

        Try
            Dim Writer = New System.IO.StreamWriter(Path & "\" & Filename, False)
            Writer.WriteLine("CYGROOT " & Trim(CYGROOT))
            Writer.WriteLine("DEFAULTSYS " & Trim(DEFAULTSYS))
            If RECOVERY <> "" Then
                Writer.WriteLine("RECOVERY" & Trim(RECOVERY))
            End If
            Writer.WriteLine("COMPLETED")
            Writer.Flush()
            Writer.Close()
        Catch ex As Exception
            WriteOK = False
            DebugOut(" ERROR: error occurred while writing settings at " & Chr(34) & Path & Chr(34))
            Throw
        End Try
    End Sub

End Class
