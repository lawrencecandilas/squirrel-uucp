Imports System.ComponentModel
Imports System.IO
Imports System.IO.Enumeration

Namespace ConfigClasses

    '-------------------------------------------------------------------------
    'Port class (and helper class Feeling for MakeTransportObject method)
    '
    'A port object may contain a transport object.
    'A port object may be contained by a system object.
    '-------------------------------------------------------------------------

    Public Class Feeling
        'Rather simple encapsulation around an integer.      
        'Used to track level of confidence that a port is one of various
        'transport types.  See UUCPPort class below.
        Public Level As Integer = 0
        Public Sub Stronger(in_amount As Integer)
            Level += in_amount
        End Sub
    End Class

    Public Class UUCPPort
        'If a UUCP system has a port defined, then one of these objects should
        'be instantiated and associated with the UUCP system object.
        '
        Public Name As String = "[NO_NAME_SPECIFIED]"
        Public port_conflines As New Collection
        Public port_confline_type As String = ""
        Public port_confline_command As String = ""
        Public IsTransportObjectDefined As Boolean = False
        Public TransportObject As New Object
        Public Sub New(in_name As String)
            Name = in_name
        End Sub
        Public Sub MakeTransportObject()
            DebugOut("> MakeTransportObject() for port '" & Name & "'")
            'This will examine a few properties and create an appropriate
            'object to assign to TransportObject.
            '
            'The object assigned to TransportObject can provide
            'transport-specific properties and methods.

            Dim params As New Hashtable

            Select Case port_confline_type
                Case "none-known-system"
                    DebugOut(">> type is none-known-system")
                    TransportObject = Transports.Make("KnownSystem", params)
                    IsTransportObjectDefined = True
                Case "pipe"
                    DebugOut(">> type is pipe")
                    DebugOut(">> Attempting to discern transport from command '" & port_confline_command & "'")
                    'Port type "pipe" can be anything.  Need to examine
                    'the specified command for clues and discern according to
                    'our levels of confidence.
                    '
                    'Variables for parsing.
                    Dim cstart As Integer = 1
                    Dim cend As Integer = Len(port_confline_type)
                    Dim ctoken As String = ""
                    Dim cnextflag As Integer = 1

                    'Tracking our confidences.
                    Dim Confidence As New Collection
                    For Each Supported In Transports.AvailableTransports
                        Confidence.Add(New Feeling, Supported.Key)
                    Next

                    'We gather data as we see it.  Recognized parameters will
                    'be added to the hashtable in a format recognizable by 
                    'transport objects in the Transports module.

                    'Let's start going through the command text word by word.
                    'cstart and cend are pointers into the string, we extract
                    'words based on where we find spaces, then set flags to
                    'take actions.
                    Do
                        cend = InStr(cstart, port_confline_command, " ") - 1
                        If cend = -1 Then
                            cend = Len(port_confline_command)
                            cnextflag = 2
                        End If
                        ctoken = Mid(port_confline_command, cstart, (cend - cstart) + 1)
                        cstart = cend + 2
                        DebugOut(">>> token '" & ctoken & "'")
                        Select Case ctoken
                            Case "-i" : cnextflag = 100 : Continue Do
                            Case "-l" : cnextflag = 101 : Continue Do
                            Case "-p" : cnextflag = 102 : Continue Do
                        End Select
                        Select Case cnextflag
                            Case 1 'beginning of string/first token
                                params("INTERNAL-SSHBinPath") = ctoken
                                If ctoken = "/bin/ssh" Then Confidence.Item("SSHTransport").Stronger(10)
                                If ctoken = "/usr/bin/ssh" Then Confidence.Item("SSHTransport").Stronger(10)
                                If ctoken = "ssh" Then Confidence.Item("SSHTransport").Stronger(10)
                            Case 2 'end of string/last token, also time to exit
                                params("ssh-server") = ctoken
                                Exit Do
                            Case 100 '-i option for SSH key path
                                params("INTERNAL-SSHKeyPath") = ctoken
                                Confidence.Item("SSHTransport").Stronger(1)
                            Case 101 '-l option for SSH login name
                                params("ssh-username") = ctoken
                                Confidence.Item("SSHTransport").Stronger(1)
                            Case 102 '-p option for SSH port
                                params("INTERNAL-SSHPort") = ctoken
                                Confidence.Item("SSHTransport").Stronger(1)
                        End Select
                        cnextflag = 0
                    Loop

                    'Find out which type we have the most confidnce in.
                    'This doesn't handle ties gracefully - FCFS.
                    Dim top As Integer = 0
                    Dim topKind As String = "meaningless"

                    For Each Supported In Transports.AvailableTransports
                        If Confidence.Item(Supported.Key).Level > top Then
                            top = Confidence.Item(Supported.Key).Level
                            topKind = Supported.Key
                        End If
                    Next

                    DebugOut(">> I have the most confidence that this is '" & topKind & "'")

                    'Create object based on what we think.
                    'If we don't know and topKind = "meaningless", then we
                    'don't make an object, and don't set
                    'IsTransportObjectDefined.
                    If topKind = "meaningless" Then
                        DebugOut(">> Not creating a transport object for this port.")
                        Exit Sub
                    End If

                    'Fixup for SSHTransport if port is not specified.
                    If topKind = "SSHTransport" Then
                        If params("INTERNAL-SSHPort") <> "" And params("INTERNAL-SSHPort") <> "22" Then
                            params("ssh-server") &= ":" & params("INTERNAL-SSHPort")
                        End If
                    End If

                    'Make the object.
                    DebugOut(">> Creatng a new " & topKind & " for this port")

                    TransportObject = Transports.Make(topKind, params)

                    If Not IsNothing(TransportObject) Then
                        IsTransportObjectDefined = True
                    End If

            End Select
        End Sub
    End Class

    '-------------------------------------------------------------------------
    'System class
    '
    'A system object may contain a port object.
    '-------------------------------------------------------------------------

    Public Class UUCPSystem
        Public sys_conflines As New Collection
        Private call_confline As String
        Public Name As String = "[NO_NAME_SPECIFIED]"
        Public Port As Object
        Public IsPortDefined As Boolean = False
        Public uucicoUsername As String = "[NO_USERNAME_SPECIFIED]"
        Public ForwardTo As Boolean = False
        Public ForwardFrom As Boolean = False

        Public Sub New(in_configFilePath As String, in_name As String)
            Name = in_name
        End Sub

        Public Function GetTransportParameters() As Hashtable
            Dim out_hashtable As New Hashtable
            Dim CallGetProperties As Boolean = True
            If IsNothing(Port) Or Not IsPortDefined Then
                CallGetProperties = False
            End If
            If Not Port.IsTransportObjectDefined Then
                CallGetProperties = False
            End If
            If CallGetProperties Then
                For Each TransportProperty In Port.TransportObject.GetProperties()
                    DebugOut(" " & TransportProperty.key & " = " & TransportProperty.value)
                    out_hashtable(TransportProperty.Key) = TransportProperty.Value
                Next
                out_hashtable("INTERNAL-PortName") = Port.Name
            End If

            'Standard properties that will be in any transport parameter
            'hashtable.
            out_hashtable("system-name") = Name
            out_hashtable("uucico-call-username") = uucicoUsername

            GetTransportParameters = out_hashtable
        End Function

    End Class

    '-------------------------------------------------------------------------
    'Config class
    '
    'A config object has much of the UUCP configuration slurped in and
    'abstrcted to an object model.
    '
    'If the file-based config is bad, Valid will be false.
    'Update it anytime by calling Refresh().
    '-------------------------------------------------------------------------

    Public Class Config
        Public SetRootFromConfig As Boolean = True
        Public Valid = False
        Public Problems As Collection = New Collection
        Public uuname As String = My.Computer.Name

        'Cygwin environment root, for Windows access.
        '
        'These defaults will be immediately overridden by SetCygwinRoot() -
        'either when the 'cygwin-uucp' folder is noticed in the same folder
        'the application is running from, or when the saved path is read from
        'the settings.  Still included for safety.
        '
        'AppContext.BaseDirectory is best way to get the current path that the
        ' application is running from:
        'https://stackoverflow.com/questions/2593458/executable-directory-where-application-is-running-from
        '
        Public Winpathof_cygwinroot As String = "C:\squirrel\cygwin-uucp"

        'Cygwin directory where uucp configuration files are.
        'Initialized by UNIXPaths() below.
        Public Winpathof_etcuucp = Winpathof_cygwinroot & "\etc\uucp"

        'Executable for uuto called by Windows.
        'Initialized by UNIXPaths() below.
        Public Winpathof_uuto As String = Winpathof_cygwinroot & "\bin\uucp.exe"

        'Location of uucico_front bash script.
        'Invoked indirectly through bash -c, so this is a UNIX path.
        'Initialized by UNIXPaths() below.
        Public UNIXpathof_uucico_front As String = "/usr/local/bin/uucico-front"

        'Executable for uustat called by Windows.
        'Initialized by UNIXPaths() below.
        Public Winpathof_uustat As String = Winpathof_cygwinroot & "\bin\uustat.exe"

        'UUCP call log file.
        'Initialized by UNIXPaths() below.
        Public Winpathof_uulog As String = Winpathof_cygwinroot & "\usr\spool\uucp\Log"

        'uucppublic directory, contains received files.
        'Initialized by UNIXPaths() below.
        Public Winpathof_uucppublic As String = Winpathof_cygwinroot & "\usr\spool\uucppublic\receive"

        'Executable for bash called by Windows.
        'Initialized by UNIXPaths() below.
        Public Winpathof_bash As String = Winpathof_cygwinroot & "\bin\bash.exe"

        Public Ports As New Collection
        Public Systems As New Collection
        Public uucicoUsers As New Hashtable

        'See btnFrmInvite_Click in Form2.vb
        Public AddKnownSystems As List(Of String)

        Public Sub New()
            DebugOut("Config.New")
            'Form1.vb early initialization should be the one typically making
            'an instance of this object.
            '
            'Assume false until proven true. :)
            Valid = False

            'If we find a 'cygwin-uucp' folder in the same folder Squirrel is
            'running from, then we'll use that as the Cygwin environment and
            'tell CurrentSettings we're in "portable mode" - where settings
            'are not loaded/saved.
            If IO.Directory.Exists(AppContext.BaseDirectory & "\cygwin-uucp") Then
                SetCygwinRoot(AppContext.BaseDirectory & "\cygwin-uucp")
                CurrentSettings.PortableMode()
                SetRootFromConfig = False
            End If

            If SetRootFromConfig Then
                CurrentSettings.LoadSettings()
                If CurrentSettings.CYGROOT = "" Then
                    Problems.Add(ConfigProblems(10))
                    SquirrelComms.Item(29).SystemError("")
                Else
                    SetCygwinRoot(CurrentSettings.CYGROOT)
                End If
            End If

            'If the Cygwin environent is OK...
            If CygwinEnvironmentCheck(Me) Then
                DebugOut("Config.New: Cygwin environment OK, doing initial Refresh")
                'then we'll read and parse the config.
                Valid = Refresh()
            End If
            'otherwise leave Valid = False.

            'The valid boolean tells external code (Form1) whether or not we
            'were able to read and parse the config.
        End Sub

        Public Sub SetCygwinRoot(in_path As String)
            Winpathof_cygwinroot = Slashes(in_path)
            Winpathof_etcuucp = Winpathof_cygwinroot & "\etc\uucp"
            Winpathof_uuto = Winpathof_cygwinroot & "\bin\uucp.exe"
            UNIXpathof_uucico_front = "/usr/local/bin/uucico-front"
            Winpathof_uustat = Winpathof_cygwinroot & "\bin\uustat.exe"
            Winpathof_uulog = Winpathof_cygwinroot & "\usr\spool\uucp\Log"
            Winpathof_uucppublic = Winpathof_cygwinroot & "\usr\spool\uucppublic\receive"
            Winpathof_bash = Winpathof_cygwinroot & "\bin\bash.exe"
        End Sub
        Public Function Refresh()
            'Reads UUCP configuration files and assembles our understanding of
            'them.  This will be a number of Systems and Ports objects.
            '
            'External code (Form1) can use these to present the UI.
            DebugOut("ConfigClasses.Config.Refresh() ...")

            'Kill anything in our collections.
            'Yes, it is possible for Refresh to be called outside of
            'initialization.
            '
            'Currently: it is called after a new system is added or removed
            Ports.Clear()
            Systems.Clear()
            uucicoUsers.Clear()

            Dim ShouldReadConfig As Boolean = True
            Dim ShouldReadPort As Boolean = True
            Dim ShouldReadSys As Boolean = True

            Dim StringSplitter As New List(Of String)

            '==== Call File ====
            '
            'Let's read the call file and populate uucicoUsers with it.
            'If it exists...
            DebugOut(" reading UUCP config file '" & Winpathof_etcuucp & "\call' if it exists ...")

            If System.IO.File.Exists(Slashes(Winpathof_etcuucp & "\call")) Then
                'It does exist.  We'll snag the users from it and stuff it
                'into the uucicoUsers hashtable.
                DebugOut("  call file found, now processing")

                Dim reader3 As System.IO.StreamReader
                reader3 = System.IO.File.OpenText(Winpathof_etcuucp & "\call")

                Do Until reader3.EndOfStream

                    Dim fileLine As String = Trim(reader3.ReadLine)

                    'ignore blank lines
                    If fileLine = "" Then Continue Do
                    'ignore unix comment lines
                    If Left(fileLine, 1) = "#" Then Continue Do

                    'extract system name and username from call file line
                    'IF line and everything in it is valid.                    
                    StringSplitter.Clear()
                    StringSplitter = SplitOnWhitespace(fileLine)
                    If StringSplitter.Count = 3 Then
                        'make sure what we're extracting is valid
                        '
                        'Test if system name is valid, report problem and skip
                        'this line if not.
                        If Not Validation_callfile_systemname(StringSplitter(0)) Then
                            Problems.Add(ConfigProblems(7))
                            DebugOut("  ! invalid system name " & Chr(34) & StringSplitter(0) & Chr(34))
                            Continue Do
                        End If
                        '
                        'Test if username is valid, report problem and skip
                        'this line if not.
                        If Not Validation_callfile_username(StringSplitter(1)) Then
                            DebugOut("  ! invalid username " & Chr(34) & StringSplitter(0) & Chr(34))
                            Problems.Add(ConfigProblems(7))
                            Continue Do
                        End If
                        '
                        'Line is OK.                        
                        DebugOut("  system " & StringSplitter(0) & ", user " & StringSplitter(1))
                        uucicoUsers(StringSplitter(0)) = StringSplitter(1)
                    Else
                        '
                        'Lines with less then 3 fields are bad.
                        'Report and skip line in this case.
                        DebugOut("  ! line found in call file with less than 3 tokens")
                        Problems.Add(ConfigProblems(7))
                    End If
                Loop
                reader3.Dispose()
            Else
                DebugOut(" it doesn't exist, making it")
                If Not NewUUCPCallFile(Winpathof_etcuucp & "\call") Then
                    'if something goes wrong while making the file. :O
                    Problems.Add(ConfigProblems(6))
                    ShouldReadConfig = False
                    ShouldReadPort = False
                    ShouldReadSys = False
                End If
            End If

            Dim splitLine(2) As String

            '==== Config File ====

            'Now, we're wanting to get the nodename from the UUCP config
            '-----------------------------------------------------------------
            DebugOut(" reading UUCP config file '" & Winpathof_etcuucp & "\config' ...")
            '
            'Let's do check if that exists.
            If Not Validation_DoesFileExist(Winpathof_etcuucp & "\config") Then
                '
                'If it doesn't, give the user the opportunity to specify a
                'nodename and we'll make a fresh file.
                DebugOut("  doesn't exist - generating one after asking user for a nodename")

                Dim Dialog0 As New frmUUCPNodenameChange
                'NOTE: uuname will be local computer name by default
                frmUUCPNodenameChange.tbxNewUUCPNodename.Text = uuname
                frmUUCPNodenameChange.ShowDialog()
                'If the user didn't cooperate ...
                If Not frmUUCPNodenameChange.NewNameSet Then
                    '... then that's a problem.
                    DebugOut("  user cancelled out of dialog, not reading config")
                    SquirrelComms.Item(12).UserError()
                    Problems.Add(ConfigProblems(0))
                    ShouldReadConfig = False
                Else
                    '...otherwise make the file,
                    DebugOut("  user provided a new nodename, making new config file")
                    'more specifically, *try* to make the file...
                    If Not NewUUCPConfigFile(Winpathof_etcuucp & "\config",
                                             frmUUCPNodenameChange.tbxNewUUCPNodename.Text) Then
                        'if something goes wrong while making the file, then
                        'gosh darn, report and obviously we won't be reading
                        'it.
                        Problems.Add(ConfigProblems(1))
                        ShouldReadConfig = False
                    End If
                End If
                'done with the dialog.
                frmUUCPNodenameChange.Dispose()
            End If

            If ShouldReadConfig Then

                Dim reader0 As System.IO.StreamReader
                reader0 = System.IO.File.OpenText(Winpathof_etcuucp & "\config")

                Do Until reader0.EndOfStream

                    Dim fileLine As String = Trim(reader0.ReadLine)
                    DebugOut("  config line read: '" & fileLine & "'")

                    'ignore blank lines
                    If fileLine = "" Then Continue Do
                    'ignore unix comment lines
                    If Left(fileLine, 1) = "#" Then Continue Do

                    'TODO: `config` file contains pointers to other files.  We
                    'should parse, validate, and understand those options.

                    SplitOnFirstSpace(fileLine, splitLine)
                    If LCase(splitLine(1)) = "nodename" Then
                        uuname = splitLine(2)
                    End If
                Loop
                DebugOut(" uuname is " & uuname)
                reader0.Dispose()
            End If

            '==== Port File ====

            'Next, read ports from the UUCP Port file.
            '-----------------------------------------------------------------
            DebugOut(" reading UUCP Port file '" & Winpathof_etcuucp & "\Port' ...")
            'Let's check if that exists.
            If Not Validation_DoesFileExist(Winpathof_etcuucp & "\Port") Then
                'If it doesn't, we'll make a new blank one.
                'Deferring telling user until we know what's going on with the
                '/etc/uucp/sys.
                '
                'While Squirrel UUCP Caller will require at least one system
                'that points to a port defined in 'Port', because that
                'condition is required to make call, absence of one is not a
                'configuration error.
                '
                DebugOut("  doesn't exist, will make a new blank one")
                If Not NewUUCPPortFile(Winpathof_etcuucp & "\Port") Then
                    'if something goes wrong while making the file. :O
                    Problems.Add(ConfigProblems(2))
                    Problems.Add(ConfigProblems(3))
                    ShouldReadPort = False
                End If
            End If

            'So, given the stuff about it being entirely possible for a system
            'to be defined in the configuration files without pointing to a
            'valid port ...
            '
            'we'll model the case where:
            ' a system is defined in the sys file,
            ' there is no port line,
            ' there is nothing else except a protocol line
            'as a fake "port"
            'So we'll go ahead and instantiate that "port" now
            Ports.Add(New UUCPPort("KnownSystem"), "KnownSystem")
            Ports.Item("KnownSystem").port_confline_type = "none-known-system"

            If ShouldReadPort Then
                Dim reader As System.IO.StreamReader
                reader = System.IO.File.OpenText(Winpathof_etcuucp & "\Port")

                Dim currentPortName As String = ""
                Dim currentPortType As String

                Do Until reader.EndOfStream
                    Dim fileLine As String = Trim(reader.ReadLine)
                    DebugOut("  Port line read: '" & fileLine & "'")

                    'ignore blank lines
                    If fileLine = "" Then Continue Do
                    'ignore unix comment lines
                    If Left(fileLine, 1) = "#" Then Continue Do

                    SplitOnFirstSpace(fileLine, splitLine)

                    If LCase(splitLine(1)) = "port" Then
                        currentPortName = LCase(splitLine(2))
                        If Not Validation_Portfile_portname(currentPortName) Then
                            currentPortName = ""
                            Problems.Add(ConfigProblems(8))
                            DebugOut("  ! invalid port name " & Chr(34) & StringSplitter(0) & Chr(34))
                            Continue Do
                        Else
                            currentPortType = ""
                            DebugOut("   adding port '" & currentPortName & "'")
                            Ports.Add(New UUCPPort(currentPortName), currentPortName)
                            Ports.Item(currentPortName).port_conflines.Add(fileLine)
                            Continue Do
                        End If
                    End If

                    If currentPortName = "" Then Continue Do

                    Ports.Item(currentPortName).port_conflines.Add(fileLine)
                    If splitLine(1) = "type" Then
                        DebugOut("   port '" & currentPortName & "': type is '" & LCase(splitLine(2)) & "'")
                        Ports.Item(currentPortName).port_confline_type = LCase(splitLine(2))
                    End If
                    If splitLine(1) = "command" Then
                        DebugOut("   port '" & currentPortName & "': command is '" & LCase(splitLine(2)) & "'")
                        Ports.Item(currentPortName).port_confline_command = LCase(splitLine(2))
                    End If

                Loop
                reader.Dispose()

                'For each port, look through the gathered config lines and
                'determine the transport.  Is expected primarily to be SSH at
                'this time.
                DebugOut("  making transport objects for each port")
                For Each i In Ports
                    i.MakeTransportObject
                Next
            End If

            'Now, read systems from the UUCP sys config file.
            '-----------------------------------------------------------------

            DebugOut(" reading UUCP sys file '" & Winpathof_etcuucp & "\sys' ...")
            'Let's check if that exists.
            If Not Validation_DoesFileExist(Winpathof_etcuucp & "\sys") Then
                DebugOut("  doesn't exist, will make a new blank one")
                If Not NewUUCPSysFile(Winpathof_etcuucp & "\sys") Then
                    'if something goes wrong while making the file. :O
                    Problems.Add(ConfigProblems(4))
                    Problems.Add(ConfigProblems(5))
                    ShouldReadSys = False
                End If
            End If

            Dim forwardTos As New List(Of String)
            Dim forwardFroms As New List(Of String)

            Dim currentSystemName As String = ""

            If ShouldReadSys Then
                DebugOut(" reading UUCP sys file '" & Winpathof_etcuucp & "\sys' ...")
                Dim reader2 As System.IO.StreamReader
                reader2 = System.IO.File.OpenText(Winpathof_etcuucp & "\sys")

                'So... we're going to read the Cygwin UUCP /etc/uucp/sys file and 
                'create a UUCPSystem object for each system defined there.
                Do Until reader2.EndOfStream
                    Dim fileLine As String = Trim(reader2.ReadLine)
                    DebugOut("  sys line read: '" & fileLine & "'")

                    If fileLine = "" Then Continue Do
                    If Left(fileLine, 1) = "#" Then Continue Do

                    SplitOnFirstSpace(fileLine, splitLine)

                    If LCase(splitLine(1)) = "forward-to" Then
                        DebugOut("  read forward-to line")
                        forwardTos = SplitBySpaces(splitLine(2))
                        Continue Do
                    End If

                    If LCase(splitLine(1)) = "forward-from" Then
                        DebugOut("  read forward-from line")
                        forwardFroms = SplitBySpaces(splitLine(2))
                        Continue Do
                    End If

                    'The configuration file defines systems in blocks.
                    'A block starts with a line that begins with "system"
                    'So if we run into one, we make a new UUCPSystem object.
                    '
                    'We are also storing the lines of the configuration file that
                    'are pertinent to a system in the UUCPSystem object's
                    'sys_conflines collection.
                    '
                    If LCase(splitLine(1)) = "system" Then
                        'The only thing that ends a 'system' block in the UUCP
                        'sys file is the start of a new 'system' block.
                        '
                        'Therefore, when we find a 'system' line - which is
                        'the start of a new 'system' block - the previous one
                        'is ending.
                        '
                        'So before we initialize handling the next block, we
                        'need to check if the first block fits the criteria of
                        'a "known system" - which won't have a 'port' line or
                        'much else except the protocol.                        
                        If currentSystemName <> "" Then
                            CheckIfKnownSystem(currentSystemName)
                        End If

                        'Now we can go ahead and make a new system.
                        '
                        'if it's a valid system name, of course.
                        If Not Validation_sysfile_systemname(LCase(splitLine(2))) Then
                            DebugOut("  ! invalid system name " & Chr(34) & LCase(splitLine(2)) & Chr(34))
                            Problems.Add(ConfigProblems(9))
                            Continue Do
                        End If
                        '
                        'and since we've established it is...
                        '
                        currentSystemName = LCase(splitLine(2))
                        DebugOut("   adding system '" & currentSystemName & "'")
                        Systems.Add(New UUCPSystem(Winpathof_etcuucp & "\sys", currentSystemName), currentSystemName)
                        '
                        'If this system has a line in the call file, associate
                        'it with this system object.
                        If uucicoUsers.ContainsKey(currentSystemName) Then
                            DebugOut("  user found for this system")
                            Systems.Item(currentSystemName).uucicoUsername = uucicoUsers(currentSystemName)
                        End If
                        'Associate the conflines with it too.
                        Systems.Item(currentSystemName).sys_conflines.Add(fileLine)
                        Continue Do
                    End If

                    'If we haven't run into a "system" line in the configuration
                    'file yet, we'll ignore any other lines until we do.                
                    If currentSystemName = "" Then Continue Do

                    'At this point, we are going through lines beneath a "system"
                    'line in the configuration file, which will have some info we
                    'want to pull into the system's UUCPSystem object.
                    '
                    'We're also slurping in the configuration lines for the
                    'system definitions too.
                    Systems.Item(currentSystemName).sys_conflines.Add(fileLine)

                    If splitLine(1) = "port" Then
                        DebugOut("   port '" & splitLine(2))
                        Systems.Item(currentSystemName).Port = Ports(splitLine(2))
                        Systems.Item(currentSystemName).IsPortDefined = True
                        Ports.Item(Systems.Item(currentSystemName).Port.Name).port_confline_command = LCase(splitLine(2))
                    End If

                Loop
                reader2.Dispose()
            End If
            'So ... we're done with the sys file ... but if the last system we
            'were looking at didn't have a 'port' line, then it might be a
            'a known system that we won't catch because there is no next 
            ''system' line.
            If currentSystemName <> "" Then
                If Not Systems.Item(currentSystemName).IsPortDefined Then
                    CheckIfKnownSystem(currentSystemName)
                End If
            End If

            'Now we go through our lists of forward-to's and forward-for's and
            'set booleans.
            For Each f In forwardTos
                For Each s In Systems
                    If s.Name = f Then
                        s.ForwardTo = True
                    End If
                Next
            Next
            For Each f In forwardFroms
                For Each s In Systems
                    If s.Name = f Then
                        s.ForwardFrom = True
                    End If
                Next
            Next

            If Problems.Count = 0 Then Refresh = True Else Refresh = False
        End Function
        Private Sub CheckIfKnownSystem(in_systemName)
            If Not Systems.Item(in_systemName).IsPortDefined Then
                'TODO: Actually check if system is just a known
                'system and not something else.
                Systems.Item(in_systemName).Port = Ports("KnownSystem")
                Systems.Item(in_systemName).IsPortDefined = True
            End If
        End Sub

        Public Sub ModifyForwardFrom(in_enableOrDisable As Boolean, in_systemName As String)
            DebugOut("ConfigClasses.ModifyForwardFrom(" & in_enableOrDisable.ToString &
                                                    "," & in_systemName
                                                    )
            Dim SysFileBuffer As String = ""
            Dim SysGlobalOptions As New Hashtable
            SysGlobalOptions = PrepareToModifySys(SysFileBuffer)
            ModifyForward(SysGlobalOptions, "forward-from", in_enableOrDisable, in_systemName)
            CommitModifiedSys(SysFileBuffer, SysGlobalOptions)
            Systems.Item(in_systemName).ForwardFrom = in_enableOrDisable
            DebugOut("ConfigClasses.ModifyForwardFrom complete")
        End Sub
        Public Sub ModifyForwardTo(in_enableOrDisable As Boolean, in_systemName As String)
            DebugOut("ConfigClasses.ModifyForwardTo(" & in_enableOrDisable.ToString &
                                                    "," & in_systemName
                                                    )
            Dim SysFileBuffer As String = ""
            Dim SysGlobalOptions As New Hashtable
            SysGlobalOptions = PrepareToModifySys(SysFileBuffer)
            ModifyForward(SysGlobalOptions, "forward-to", in_enableOrDisable, in_systemName)
            CommitModifiedSys(SysFileBuffer, SysGlobalOptions)
            Systems.Item(in_systemName).ForwardTo = in_enableOrDisable
            DebugOut("ConfigClasses.ModifyForwardTo complete")
        End Sub

        Public Function PrepareToModifySys(ByRef in_bufferRef As String) As Hashtable
            DebugOut("ConfigClasses.PrepareToModifySys(in_bufferRef) ...")
            'So basically what this function does is:
            '
            '- Read the entire sys file into in_bufferRef
            '   The related Commit function will want it and will use to to
            '   rewrite the file on commit.
            '
            '- Extract certain options into a hashtable - right now, options
            '  relating to forwarding that appear before the first 'system'
            '  block.
            '  *One other thing is put into the hashtable - a value called
            '   "boundary" that tells what line number the system blocks
            '   start in the file.  Hashtable options change "defaults" which
            '   apply to all system blocks and have to appear before any of
            '   them.
            '  *External code can add/remove/change the hashtable and the
            '   commit function will add those lines, or update them in place
            '   if found.

            in_bufferRef = ""

            Dim SysGlobalOptions As New Hashtable
            SysGlobalOptions("boundary") = -1

            Dim FileLine As String = ""
            Dim SplitLine(2) As String
            Dim CurrentLine As Integer = 0

            Dim Reader As System.IO.StreamReader
            Reader = System.IO.File.OpenText(Slashes(Winpathof_etcuucp & "\sys"))

            Do Until Reader.EndOfStream
                FileLine = Reader.ReadLine
                in_bufferRef &= FileLine & vbLf

                If SysGlobalOptions("boundary") <> -1 Then
                    CurrentLine += 1
                    Continue Do
                End If

                SplitOnFirstSpace(FileLine, SplitLine)

                If LCase(SplitLine(1)) = "system" Then
                    SysGlobalOptions("boundary") = CurrentLine
                    Continue Do
                End If

                If LCase(SplitLine(1)) = "forward-to" Then
                    SysGlobalOptions(SplitLine(1)) = SplitLine(2)
                End If
                If LCase(SplitLine(1)) = "forward-from" Then
                    SysGlobalOptions(SplitLine(1)) = SplitLine(2)
                End If

                CurrentLine += 1
            Loop
            Reader.Dispose()

            If SysGlobalOptions("boundary") = -1 Then SysGlobalOptions("boundary") = CurrentLine

            PrepareToModifySys = SysGlobalOptions
        End Function

        Function CommitModifiedSys(ByRef in_BufferRef As String, ByRef in_SysGlobalOptions As Hashtable) As Boolean
            DebugOut("Config.ClassesCommitModifiedSys(ByRef in_BufferRef,ByRef in_SysGlobalOptions) ...")
            'See PrepareToModifySys() for details.
            'That function should have been called first.
            '
            'This also uses AddKnownSystems which can be set externally,
            'particularly by btnInviteFile_Click in Form2.vb.

            'Let's treat our cached sys file like an in-memory stream
            Dim Reader As New System.IO.StringReader(in_BufferRef)

            Dim SplitLine(2) As String
            Dim BufferLine As String = ""

            Dim currentLine As Integer = 0
            Dim PastBoundary As Boolean = False
            Dim WroteRequest As Boolean = False

            Dim EncounteredSystems As New List(Of String)

            System.IO.File.Move(Slashes(Winpathof_etcuucp & "\sys"), Slashes(Winpathof_etcuucp & "\sys.bak"), True)
            DebugOut(" existing sys file backed up")

            Dim Writer = New System.IO.StreamWriter(Slashes(Winpathof_etcuucp & "\sys"), False)
            Do While Reader.Peek > -1
                BufferLine = Reader.ReadLine
                SplitOnFirstSpace(BufferLine, SplitLine)

                If LCase(SplitLine(1)) = "system" Then
                    DebugOut(" first line for system " & Chr(34) & SplitLine(2) & Chr(34))
                    EncounteredSystems.Add(LCase(SplitLine(2)))
                End If

                If currentLine = in_SysGlobalOptions("boundary") Then
                    PastBoundary = True
                    For Each x In in_SysGlobalOptions.Keys
                        If x = "boundary" Then Continue For
                        DebugOut(" " & Chr(34) & x & Chr(34) & " - wasn't in file before, adding it")
                        Writer.Write(x & " " & Trim(in_SysGlobalOptions(x)) & vbLf)
                    Next
                End If

                If PastBoundary Then
                    currentLine += 1
                    Writer.Write(BufferLine & vbLf)
                    Continue Do
                End If

                WroteRequest = False
                For Each x In in_SysGlobalOptions.Keys
                    If x = "boundary" Then Continue For
                    If LCase(SplitLine(1)) = x Then
                        DebugOut(" " & Chr(34) & x & Chr(34) & " - updating existing line in file")
                        Writer.Write(x & " " & Trim(in_SysGlobalOptions(x)) & vbLf)
                        in_SysGlobalOptions.Remove(x)
                        WroteRequest = True
                        Exit For
                    End If
                Next

                currentLine += 1

                If Not WroteRequest Then
                    Writer.Write(BufferLine & vbLf)
                End If
            Loop

            Reader.Dispose()

            'All right, let's deal with AddKnownSystems now
            If Not IsNothing(AddKnownSystems) Then
                If AddKnownSystems.Count <> 0 Then
                    DebugOut(" processing deferred need to add known systems")
                    For Each x In AddKnownSystems
                        DebugOut("  something wanted to add " & Chr(34) & x & Chr(34) & " as a known system")
                        Dim DontDoIt As Boolean = False
                        For Each y In EncounteredSystems
                            If x = y Then
                                DebugOut("  already written into sys file")
                                DontDoIt = True
                            End If
                            Exit For
                        Next
                        If Not DontDoIt Then
                            Writer.Write(vbLf &
                                         "system " & x & vbLf &
                                         "protocol i" & vbLf &
                                         vbLf
                                         )
                            DebugOut("  added to sys file")
                        End If
                    Next
                End If
                AddKnownSystems.Clear()
            End If

            Writer.Dispose()

            CommitModifiedSys = True
        End Function
        Public Sub ModifyForward(ByRef in_SysGlobalOptions As Hashtable,
                                 in_Option As String,
                                 in_enableOrDisable As Boolean,
                                 in_systemName As String)
            DebugOut("ConfigClasses.ModifyForward(ByRef in_SysGlobalOptions" &
                                                  "," & Chr(34) & in_Option & Chr(34) &
                                                  "," & in_enableOrDisable.ToString &
                                                  "," & in_systemName
                                                  )
            'Changes forwarding options in a SysGlobalOptions hashtable.
            '
            'The change will be to add or remove the name of a system from a 
            'space separated list of names.
            '
            'The hashtable is one that comes from PrepareToModifySys()            
            '
            'Caller responsible for calling CommitModifiedSys() to etch the
            'change in.

            Dim Systems As New List(Of String)
            Dim NewForwardLine As String = ""

            'Remove
            If in_enableOrDisable = False Then
                If Not in_SysGlobalOptions.Contains(in_Option) Then in_SysGlobalOptions(in_Option) = ""
                Systems = SplitBySpaces(in_SysGlobalOptions(in_Option))
                For Each f In Systems
                    If LCase(f) <> LCase(in_systemName) Then
                        NewForwardLine &= " " & f
                    End If
                Next
                in_SysGlobalOptions(in_Option) = NewForwardLine
            End If

            'Add
            If in_enableOrDisable = True Then
                If Not in_SysGlobalOptions.Contains(in_Option) Then in_SysGlobalOptions(in_Option) = ""
                Systems = SplitBySpaces(in_SysGlobalOptions(in_Option))
                Dim Addflag = True
                For Each f In Systems
                    If LCase(f) = LCase(in_systemName) Then
                        Addflag = False
                    End If
                Next
                If Addflag Then
                    in_SysGlobalOptions(in_Option) = in_SysGlobalOptions(in_Option) & " " & in_systemName
                End If
            End If

            'CommitModifiedSys(Sysfile_Buffer, in_SysGlobalOptions)
            'Systems.Item(in_systemName).ForwardTo = in_enableOrDisable
        End Sub

    End Class

End Namespace
