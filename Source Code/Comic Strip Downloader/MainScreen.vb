Imports System.Threading
Imports System.ComponentModel
Imports System.IO
Imports System.Net
'Imports System.Security.Cryptography
Imports System.Net.Mail
Imports System.Text
Imports System.Net.Configuration



Public Class Main_Screen
    Private filesdownloadednum As Integer = 0
    Private filesdownloadednumthiscycle As Integer = 0
    Private busyworking As Boolean = False
    Private primaryprecount As Long = 0
    Private datelaunched As Date = Now()
    Private pretestdone As Boolean = False
    Private primary_PercentComplete As Integer = 0
    Private primary_highestPercentageReached As Integer = 0
    Private username As String = ""
    Private password As String = ""

    'Private mCSP As SymmetricAlgorithm

    'Private optDES As Boolean = False
    'Private optTripleDES As Boolean = True
    'Private Key As String = ""
    'Private IV As String = ""

    Private mailserver1 As String = ""
    Private mailserver1port As String = ""
    Private mailserver2 As String = ""
    Private mailserver2port As String = ""
    Private webmasteraddress As String = ""
    Private webmasterdisplay As String = ""
    Private webroot As String = ""
    Private webroottranslate As String = ""

    Private AutoUpdate As Boolean = False

    Private LastReport As Date
    Private todaydownloads As Integer = 0

    Public Shared Sub Display(ByVal credential As NetworkCredential)
        Console.WriteLine(ControlChars.Cr + "Username : {0} ,Password : {1} ,Domain : {2}", credential.UserName, credential.Password, credential.Domain)
    End Sub 'Display


    'Private Function SetEnc() As SymmetricAlgorithm
    '    If optDES = True Then
    '        Return New DESCryptoServiceProvider
    '    Else
    '        If optTripleDES = True Then
    '            Return New TripleDESCryptoServiceProvider
    '        End If
    '    End If
    '    Return New TripleDESCryptoServiceProvider
    'End Function

    'Private Sub GenerateKey()
    '    mCSP = SetEnc()
    '    mCSP.GenerateKey()
    '    Key = Convert.ToBase64String(mCSP.Key)
    'End Sub

    'Private Sub GenerateIV()
    '    mCSP.GenerateIV()
    '    IV = Convert.ToBase64String(mCSP.IV)
    'End Sub

    'Private Function EncryptString(ByVal Value As String) As String
    '    Dim ct As ICryptoTransform
    '    Dim ms As MemoryStream
    '    Dim cs As CryptoStream
    '    Dim byt() As Byte

    '    ct = mCSP.CreateEncryptor(mCSP.Key, mCSP.IV)

    '    byt = System.Text.Encoding.UTF8.GetBytes(Value)

    '    ms = New MemoryStream
    '    cs = New CryptoStream(ms, ct, CryptoStreamMode.Write)
    '    cs.Write(byt, 0, byt.Length)
    '    cs.FlushFinalBlock()

    '    cs.Close()

    '    Return Convert.ToBase64String(ms.ToArray())
    'End Function

    'Private Function DecryptString(ByVal Value As String) _
    ' As String
    '    Dim ct As ICryptoTransform
    '    Dim ms As MemoryStream
    '    Dim cs As CryptoStream
    '    Dim byt() As Byte

    '    ct = mCSP.CreateDecryptor(mCSP.Key, mCSP.IV)

    '    byt = Convert.FromBase64String(Value)

    '    ms = New MemoryStream
    '    cs = New CryptoStream(ms, ct, CryptoStreamMode.Write)
    '    cs.Write(byt, 0, byt.Length)
    '    cs.FlushFinalBlock()

    '    cs.Close()

    '    Return System.Text.Encoding.UTF8.GetString(ms.ToArray())
    'End Function

    Private Sub Error_Handler(ByVal ex As Exception, Optional ByVal identifier_msg As String = "")
        Try
            If ex.Message.IndexOf("Thread was being aborted") < 0 Then
                Dim Display_Message1 As New Display_Message()
                Display_Message1.Message_Textbox.Text = "The Application encountered the following problem: " & vbCrLf & identifier_msg & ": " & ex.Message.ToString
                'Display_Message1.Message_Textbox.Text = "The Application encountered the following problem: " & vbCrLf & identifier_msg & ": " & ex.ToString
                Display_Message1.Timer1.Interval = 1000
                Display_Message1.ShowDialog()
                Dim dir As System.IO.DirectoryInfo = New System.IO.DirectoryInfo((Application.StartupPath & "\").Replace("\\", "\") & "Error Logs")
                If dir.Exists = False Then
                    dir.Create()
                End If
                dir = Nothing
                Dim filewriter As System.IO.StreamWriter = New System.IO.StreamWriter((Application.StartupPath & "\").Replace("\\", "\") & "Error Logs\" & Format(Now(), "yyyyMMdd") & "_Error_Log.txt", True)
                filewriter.WriteLine("#" & Format(Now(), "dd/MM/yyyy hh:mm:ss tt") & " - " & identifier_msg & ": " & ex.ToString)
                filewriter.WriteLine(" ")
                filewriter.Flush()
                filewriter.Close()
                filewriter = Nothing
            End If
        Catch exc As Exception
            MsgBox("An error occurred in the application's error handling routine. The application will try to recover from this serious error.", MsgBoxStyle.Critical, "Critical Error Encountered")
        End Try
    End Sub


    Private Sub Activity_Handler(ByVal Message As String)
        Try
            Dim dir As System.IO.DirectoryInfo = New System.IO.DirectoryInfo((Application.StartupPath & "\").Replace("\\", "\") & "Activity Logs")
            If dir.Exists = False Then
                dir.Create()
            End If
            dir = Nothing
            Dim filewriter As System.IO.StreamWriter = New System.IO.StreamWriter((Application.StartupPath & "\").Replace("\\", "\") & "Activity Logs\" & Format(Now(), "yyyyMMdd") & "_Activity_Log.txt", True)
            filewriter.WriteLine("#" & Format(Now(), "dd/MM/yyyy hh:mm:ss tt") & " - " & Message)
            filewriter.Flush()
            filewriter.Close()
            filewriter = Nothing
        Catch ex As Exception
            Error_Handler(ex, "Activity_Logger")
        End Try
    End Sub

    Private Sub Status(ByVal Message As String)
        Try
            ToolStripStatusLabel1.Text = Message
        Catch ex As Exception
            Error_Handler(ex, "Status Message")
        End Try
    End Sub

    Private Sub SubStatus(ByVal Message As String)
        Try
            ToolStripStatusLabel2.Text = Message
        Catch ex As Exception
            Error_Handler(ex, "SubStatus Message")
        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            Dim newdl As NewDownload = New NewDownload()
            If DownloadPath.Items.Count > 0 Then
                If DownloadPath.SelectedIndex > -1 Then
                    newdl.DownloadURL.Text = DownloadPath.Items.Item(DownloadPath.SelectedIndex)
                    newdl.DownloadFile.Text = DownloadImage.Items.Item(DownloadPath.SelectedIndex)
                    newdl.DownloadFilePrefix.Text = DownloadPrefix.Items.Item(DownloadPath.SelectedIndex)
                    newdl.DownloadComic.Text = DownloadComic.Items.Item(DownloadPath.SelectedIndex)
                Else
                    newdl.DownloadURL.Text = DownloadPath.Items.Item(DownloadPath.Items.Count - 1)
                    newdl.DownloadFile.Text = DownloadImage.Items.Item(DownloadImage.Items.Count - 1)
                    newdl.DownloadFilePrefix.Text = DownloadPrefix.Items.Item(DownloadPrefix.Items.Count - 1)
                    newdl.DownloadComic.Text = DownloadComic.Items.Item(DownloadComic.Items.Count - 1)
                End If
            End If
            Dim result As DialogResult = newdl.ShowDialog()
            If result = Windows.Forms.DialogResult.OK Then
                If newdl.DownloadComic.Text.Length > 0 And newdl.DownloadFilePrefix.Text.Length > 0 And newdl.DownloadFile.Text.Length > 0 And newdl.DownloadURL.Text.Length > 7 Then
                    DownloadPath.Items.Add(newdl.DownloadURL.Text)
                    DownloadImage.Items.Add(newdl.DownloadFile.Text)
                    DownloadPrefix.Items.Add(newdl.DownloadFilePrefix.Text)
                    DownloadComic.Items.Add(newdl.DownloadComic.Text)
                    Status("Download Details Added.")
                Else
                    Status("Incomplete Data Detected. Download Not Added.")
                End If
            Else
                Status("Download Details Cancellation Detected.")
            End If
            newdl.Close()
            newdl.Dispose()
            NumberDownloadEntries.Text = DownloadPath.Items.Count & " Download Entries"
        Catch ex As Exception
            Error_Handler(ex, "Add Download Request")
        End Try
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try
            If DownloadPath.SelectedIndex >= 0 Then
                Dim index As Integer = DownloadPath.SelectedIndex
                DownloadPath.Items.RemoveAt(index)
                DownloadImage.Items.RemoveAt(index)
                DownloadPrefix.Items.RemoveAt(index)
                DownloadComic.Items.RemoveAt(index)
                Status("Removed Download Request")
            End If
            NumberDownloadEntries.Text = DownloadPath.Items.Count & " Download Entries"
        Catch ex As Exception
            Error_Handler(ex, "Remove Download")
        End Try
    End Sub

    Private Sub DownloadComic_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DownloadComic.SelectedIndexChanged
        Try
            If DownloadComic.SelectedIndex >= 0 Then
                DownloadImage.SelectedIndex = DownloadComic.SelectedIndex
                DownloadPrefix.SelectedIndex = DownloadComic.SelectedIndex
                DownloadPath.SelectedIndex = DownloadComic.SelectedIndex
                CurrentlySelected.Text = "Currently Selected: " & DownloadPath.SelectedIndex + 1 & " of " & DownloadPath.Items.Count
            Else
                CurrentlySelected.Text = "Currently Selected: 0 of " & DownloadPath.Items.Count
            End If
        Catch ex As Exception
            Error_Handler(ex, "DownloadPath_SelectedIndexChanged")
        End Try
    End Sub

    Private Sub DownloadPath_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DownloadPath.SelectedIndexChanged
        Try
            If DownloadPath.SelectedIndex >= 0 Then
                DownloadImage.SelectedIndex = DownloadPath.SelectedIndex
                DownloadPrefix.SelectedIndex = DownloadPath.SelectedIndex
                DownloadComic.SelectedIndex = DownloadPath.SelectedIndex
                CurrentlySelected.Text = "Currently Selected: " & DownloadPath.SelectedIndex + 1 & " of " & DownloadPath.Items.Count
            Else
                CurrentlySelected.Text = "Currently Selected: 0 of " & DownloadPath.Items.Count
            End If
        Catch ex As Exception
            Error_Handler(ex, "DownloadPath_SelectedIndexChanged")
        End Try
    End Sub

    Private Sub DownloadImage_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DownloadImage.SelectedIndexChanged
        Try
            If DownloadImage.SelectedIndex >= 0 Then
                DownloadPath.SelectedIndex = DownloadImage.SelectedIndex
                DownloadPrefix.SelectedIndex = DownloadImage.SelectedIndex
                DownloadComic.SelectedIndex = DownloadImage.SelectedIndex
                CurrentlySelected.Text = "Currently Selected: " & DownloadPath.SelectedIndex + 1 & " of " & DownloadPath.Items.Count
            Else
                CurrentlySelected.Text = "Currently Selected: 0 of " & DownloadPath.Items.Count
            End If
        Catch ex As Exception
            Error_Handler(ex, "DownloadImage_SelectedIndexChanged")
        End Try
    End Sub

    Private Sub DownloadPrefix_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DownloadPrefix.SelectedIndexChanged
        Try
            If DownloadPrefix.SelectedIndex >= 0 Then
                DownloadPath.SelectedIndex = DownloadPrefix.SelectedIndex
                DownloadImage.SelectedIndex = DownloadPrefix.SelectedIndex
                DownloadComic.SelectedIndex = DownloadPrefix.SelectedIndex
                CurrentlySelected.Text = "Currently Selected: " & DownloadPath.SelectedIndex + 1 & " of " & DownloadPath.Items.Count
            Else
                CurrentlySelected.Text = "Currently Selected: 0 of " & DownloadPath.Items.Count
            End If
        Catch ex As Exception
            Error_Handler(ex, "DownloadPrefix_SelectedIndexChanged")
        End Try
    End Sub

    Private Sub Load_Settings()
        Try


            Dim configfile As String = (Application.StartupPath & "\config.sav").Replace("\\", "\")
            If My.Computer.FileSystem.FileExists(configfile) Then
                Dim reader As StreamReader = New StreamReader(configfile)
                Dim lineread As String
                Dim variablevalue As String
                While reader.Peek <> -1
                    lineread = reader.ReadLine
                    If lineread.IndexOf("=") <> -1 Then

                        variablevalue = lineread.Remove(0, lineread.IndexOf("=") + 1)

                        If lineread.StartsWith("SavePath=") Then
                            Dim dinfo As DirectoryInfo = New DirectoryInfo(variablevalue)
                            If dinfo.Exists Then
                                FolderBrowserDialog1.SelectedPath = variablevalue
                                SavePath.Text = variablevalue
                            End If
                            dinfo = Nothing
                        End If

                        If lineread.StartsWith("Silent=") Then
                            Silent_CheckBox.Checked = variablevalue
                        End If

                        If lineread.StartsWith("SendMail=") Then
                            SendMail_CheckBox.Checked = variablevalue
                        End If

                        If lineread.StartsWith("DownloadPath=") Then
                            DownloadPath.Items.Add(variablevalue)
                        End If
                        If lineread.StartsWith("DownloadImage=") Then
                            DownloadImage.Items.Add(variablevalue)
                        End If
                        If lineread.StartsWith("DownloadPrefix=") Then
                            DownloadPrefix.Items.Add(variablevalue)
                        End If
                        If lineread.StartsWith("DownloadComic=") Then
                            DownloadComic.Items.Add(variablevalue)
                        End If
                        If lineread.StartsWith("mailserver1=") Then
                            mailserver1 = variablevalue
                        End If
                        If lineread.StartsWith("mailserver1port=") Then
                            mailserver1port = variablevalue
                        End If
                        If lineread.StartsWith("mailserver2=") Then
                            mailserver2 = variablevalue
                        End If
                        If lineread.StartsWith("mailserver2port=") Then
                            mailserver2port = variablevalue
                        End If
                        If lineread.StartsWith("webmasteraddress=") Then
                            webmasteraddress = variablevalue
                        End If
                        If lineread.StartsWith("webmasterdisplay=") Then
                            webmasterdisplay = variablevalue
                        End If
                    End If
                End While

                If mailserver1.Length < 1 Then
                    mailserver1 = "mail.uct.ac.za"
                End If
                If mailserver1port.Length < 1 Then
                    mailserver1port = "25"
                End If
                If mailserver2.Length < 1 Then
                    mailserver2 = "obe1.com.uct.ac.za"
                End If
                If mailserver2port.Length < 1 Then
                    mailserver2port = "25"
                End If
                If webmasteraddress.Length < 1 Then
                    webmasteraddress = "com-webmaster@uct.ac.za"
                End If
                If webmasterdisplay.Length < 1 Then
                    webmasterdisplay = "Commerce Webmaster"
                End If

                reader.Close()
                reader = Nothing
            End If


            Status("Loaded Program Saved Values")
        Catch ex As Exception
            Error_Handler(ex, "Load Settings")
        End Try
    End Sub

    Private Sub Save_Settings()

        Try
            Dim configfile As String = (Application.StartupPath & "\config.sav").Replace("\\", "\")

            Dim writer As StreamWriter = New StreamWriter(configfile, False)

            If SavePath.Text.Length > 0 Then
                Dim dinfo As DirectoryInfo = New DirectoryInfo(SavePath.Text)
                If dinfo.Exists Then
                    writer.WriteLine("SavePath=" & SavePath.Text)
                End If
                dinfo = Nothing
            End If

            writer.WriteLine("Silent=" & Silent_CheckBox.Checked.ToString)
            writer.WriteLine("SendMail=" & SendMail_CheckBox.Checked.ToString)


            Dim str As String = ""
            For Each str In DownloadPath.Items
                writer.WriteLine("DownloadPath=" & str)
            Next
            For Each str In DownloadImage.Items
                writer.WriteLine("DownloadImage=" & str)
            Next
            For Each str In DownloadPrefix.Items
                writer.WriteLine("DownloadPrefix=" & str)
            Next
            For Each str In DownloadComic.Items
                writer.WriteLine("DownloadComic=" & str)
            Next

            writer.WriteLine("mailserver1=" & mailserver1)
            writer.WriteLine("mailserver1port=" & mailserver1port)
            writer.WriteLine("mailserver2=" & mailserver2)
            writer.WriteLine("mailserver2port=" & mailserver2port)
            writer.WriteLine("webmasteraddress=" & webmasteraddress)
            writer.WriteLine("webmasterdisplay=" & webmasterdisplay)


            writer.Flush()
            writer.Close()
            writer = Nothing



            Status("Saved Program Saved Values")
        Catch ex As Exception
            Error_Handler(ex, "Save Settings")
        End Try
    End Sub

    Private Sub Main_Screen_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Text = My.Application.Info.ProductName & " " & Format(My.Application.Info.Version.Major, "0000") & Format(My.Application.Info.Version.Minor, "00") & Format(My.Application.Info.Version.Build, "00") & "." & Format(My.Application.Info.Version.Revision, "00") & ""
            Control.CheckForIllegalCrossThreadCalls = False
            LastReport = New Date(Now.Year, Now.Month, Now.Day, 0, 0, 0, 0, DateTimeKind.Local)
            Load_Settings()
            Timer1.Start()
            downloadcycle.ForeColor = Color.Green
            NumberDownloadEntries.Text = DownloadPath.Items.Count & " Download Entries"
            CurrentlySelected.Text = "Currently Selected: 0 of " & DownloadPath.Items.Count
            Status("Application Loaded")
            SendNotificationEmail("Startup")
        Catch ex As Exception
            MsgBox(ex.ToString)
            Error_Handler(ex, "Load Application")
        End Try
    End Sub

    Private Sub Main_Screen_Close(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        Try
            Timer1.Stop()
            downloadcycle.ForeColor = Color.Red
            SendNotificationEmail("Shutdown")
            Save_Settings()
            If AutoUpdate = True Then
                If My.Computer.FileSystem.FileExists((Application.StartupPath & "\AutoUpdate.exe").Replace("\\", "\")) = True Then
                    Dim startinfo As ProcessStartInfo = New ProcessStartInfo
                    startinfo.FileName = (Application.StartupPath & "\AutoUpdate.exe").Replace("\\", "\")
                    startinfo.Arguments = "force"
                    startinfo.CreateNoWindow = False
                    Process.Start(startinfo)
                End If
            End If
        Catch ex As Exception
            Error_Handler(ex, "Close Application")
        End Try
    End Sub

    Private Sub main_screen_formclosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try
            NotifyIcon1.Dispose()

        Catch ex As Exception
            Error_Handler(ex, "main_screen_formclosing")
        End Try
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Try
            Dim result As DialogResult
            Dim folderExists As Boolean
            folderExists = My.Computer.FileSystem.DirectoryExists(SavePath.Text)
            If folderExists = True Then
                FolderBrowserDialog1.SelectedPath = SavePath.Text
            End If
            result = FolderBrowserDialog1.ShowDialog
            If result = Windows.Forms.DialogResult.OK Then

                folderExists = My.Computer.FileSystem.DirectoryExists(FolderBrowserDialog1.SelectedPath)
                If folderExists = True Then
                    SavePath.Text = FolderBrowserDialog1.SelectedPath
                    Status("File Save Path Selected")
                Else
                    My.Computer.FileSystem.CreateDirectory(FolderBrowserDialog1.SelectedPath)
                    SavePath.Text = FolderBrowserDialog1.SelectedPath
                    Status("File Save Path Selected. (Folder Created)")
                End If
            Else
                Status("File Save Path Selection Cancelled")
            End If
        Catch ex As Exception
            Error_Handler(ex, "Select Image Save Path")
        End Try
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Try
            Dim value As Integer = Integer.Parse(downloadcycle.Text, Globalization.NumberStyles.Integer)
            value = value - 1
            If value = 0 Then
                StartWorker()
            End If
            If value = -1 Then
                value = 3599
            End If
            downloadcycle.Text = value
            Dim dt As Date = New Date(Now.Year, Now.Month, Now.Day, 0, 0, 0, 0, DateTimeKind.Local)
            If dt > LastReport Then
                Send_Report(Now, Format(LastReport, "yyyyMMdd"))
                LastReport = dt
            End If
        Catch ex As Exception
            Error_Handler(ex, "Timer Tick")
        End Try
    End Sub


    Private Sub StartWorker()
        Try
            If busyworking = False Then
                busyworking = True
                Controls_Enabler("run")
                If SavePath.Text.Length < 1 Then
                    SavePath.Text = "C:\"
                End If
                ' Start the asynchronous operation.
                BackgroundWorker1.RunWorkerAsync()
            End If
        Catch ex As Exception
            Error_Handler(ex, "StartWorker")
        End Try
    End Sub 'startAsyncButton_Click

    Private Sub cancelAsyncButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click

        ' Cancel the asynchronous operation.
        Me.BackgroundWorker1.CancelAsync()

        ' Disable the Cancel button.
        Button4.Enabled = False

    End Sub 'cancelAsyncButton_Click

    ' This event handler is where the actual work is done.
    Private Sub backgroundWorker1_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs) Handles BackgroundWorker1.DoWork

        ' Get the BackgroundWorker object that raised this event.
        Dim worker As BackgroundWorker = CType(sender, BackgroundWorker)

        ' Assign the result of the computation
        ' to the Result property of the DoWorkEventArgs
        ' object. This is will be available to the 
        ' RunWorkerCompleted eventhandler.
        e.Result = MainWorkerFunction(worker, e)
    End Sub 'backgroundWorker1_DoWork

    ' This event handler deals with the results of the
    ' background operation.
    Private Sub backgroundWorker1_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted
        busyworking = False
        ' First, handle the case where an exception was thrown.
        If Not (e.Error Is Nothing) Then
            Error_Handler(e.Error, "backgroundWorker1_RunWorkerCompleted")
            SubStatus("")
            Status("Operation encountered a critical error at " & primary_PercentComplete & "% complete...")
        ElseIf e.Cancelled Then
            ' Next, handle the case where the user canceled the 
            ' operation.
            ' Note that due to a race condition in 
            ' the DoWork event handler, the Cancelled
            ' flag may not have been set, even though
            ' CancelAsync was called.
            SubStatus("")
            Status("Operation was cancelled at " & primary_PercentComplete & "% complete...")

        Else
            ' Finally, handle the case where the operation succeeded.
            filesdownloaded.Text = filesdownloadednum
            filesdownloadedthiscycle.Text = filesdownloadednumthiscycle
            LinkLabel1.Visible = True
            SubStatus("")
            Status("Download Cycle Complete")

        End If


        Controls_Enabler("stop")

    End Sub 'backgroundWorker1_RunWorkerCompleted

    Private Sub Controls_Enabler(ByVal action As String)
        Select Case action.ToLower
            Case "run"
                Me.Button1.Enabled = False
                Me.Button2.Enabled = False
                Me.Button3.Enabled = False
                Me.Button5.Enabled = False
                Me.Button6.Enabled = False
                Me.Button7.Enabled = False
                Me.Button8.Enabled = False
                Me.Button9.Enabled = False
                Me.Button10.Enabled = False
                MenuStrip1.Enabled = False
                Me.DownloadPath.Enabled = False
                Me.DownloadComic.Enabled = False
                Me.DownloadImage.Enabled = False
                Me.DownloadPrefix.Enabled = False
                ' Enable the Cancel button.
                Me.Button4.Enabled = True
                ToolStripProgressBar1.Visible = True
                Exit Select
            Case "stop"
                Me.Button1.Enabled = True
                Me.Button2.Enabled = True
                Me.Button3.Enabled = True
                Me.Button5.Enabled = True
                Me.Button6.Enabled = True
                Me.Button7.Enabled = True
                Me.Button8.Enabled = True
                Me.Button9.Enabled = True
                Me.Button10.Enabled = True
                MenuStrip1.Enabled = True
                Me.DownloadPath.Enabled = True
                Me.DownloadComic.Enabled = True
                Me.DownloadImage.Enabled = True
                Me.DownloadPrefix.Enabled = True
                ' Disable the Cancel button.
                Me.Button4.Enabled = False
                ToolStripProgressBar1.Visible = False
                Exit Select
            Case Else
                Me.Button1.Enabled = False
                Me.Button2.Enabled = False
                Me.Button3.Enabled = False
                Me.Button5.Enabled = False
                Me.Button6.Enabled = False
                Me.Button7.Enabled = False
                Me.Button8.Enabled = False
                Me.Button9.Enabled = False
                Me.Button10.Enabled = False
                MenuStrip1.Enabled = False
                Me.DownloadPath.Enabled = False
                Me.DownloadComic.Enabled = False
                Me.DownloadImage.Enabled = False
                Me.DownloadPrefix.Enabled = False
                ' Enable the Cancel button.
                Me.Button4.Enabled = True
                ToolStripProgressBar1.Visible = True
                Exit Select
        End Select
    End Sub

    ' This event handler updates the progress bar.
    Private Sub backgroundWorker1_ProgressChanged(ByVal sender As Object, ByVal e As ProgressChangedEventArgs) Handles BackgroundWorker1.ProgressChanged
        filesdownloaded.Text = filesdownloadednum
        filesdownloadedthiscycle.Text = filesdownloadednumthiscycle
        ToolStripProgressBar1.Value = e.ProgressPercentage
        Status("Operation is " & e.ProgressPercentage & "% complete...")
    End Sub

    ' This is the method that does the actual work. 
    Function MainWorkerFunction(ByVal worker As BackgroundWorker, ByVal e As DoWorkEventArgs) As String
        Dim result As String = ""
        Try

            ' Abort the operation if the user has canceled.
            ' Note that a call to CancelAsync may have set 
            ' CancellationPending to true just after the
            ' last invocation of this method exits, so this 
            ' code will not have the opportunity to set the 
            ' DoWorkEventArgs.Cancel flag to true. This means
            ' that RunWorkerCompletedEventArgs.Cancelled will
            ' not be set to true in your RunWorkerCompleted
            ' event handler. This is a race condition.

            If worker.CancellationPending Then
                e.Cancel = True
            End If

            'If My.Computer.FileSystem.FileExists((Application.StartupPath & "\proxy_details.txt").Replace("\\", "\")) = True Then
            '    Dim reader As StreamReader = New StreamReader((Application.StartupPath & "\proxy_details.txt").Replace("\\", "\"), System.Text.Encoding.UTF8)
            '    Dim variables As ArrayList = New ArrayList
            '    While reader.Peek <> -1
            '        variables.Add(reader.ReadLine())
            '    End While
            '    reader.Close()
            '    reader = Nothing
            '    If variables.Count >= 4 Then
            '        Key = variables(0)
            '        IV = variables(1)
            '        mCSP = SetEnc()
            '        mCSP.Key = Convert.FromBase64String(Key)
            '        mCSP.IV = Convert.FromBase64String(IV)
            '        username = DecryptString(variables(2))
            '        password = DecryptString(variables(3))
            '    End If
            '    variables = Nothing
            'Else
            '    username = ""
            '    password = ""
            'End If



            primaryprecount = DownloadPath.Items.Count
            primary_PercentComplete = 0
            primary_highestPercentageReached = 0

            filesdownloadednumthiscycle = 0
            worker.ReportProgress(primary_PercentComplete)
            Dim URL As String
            Dim cindex As Integer = 0
            Dim dcindex As Integer = 0
            For dcindex = 0 To DownloadPath.Items.Count - 1
                'For Each URL In DownloadPath.Items
                DownloadPath.SelectedIndex = dcindex
                URL = DownloadPath.Items.Item(dcindex).ToString
                If worker.CancellationPending Then
                    e.Cancel = True
                    Exit For
                End If
                ParseAndDownload(URL, DownloadImage.Items.Item(cindex), SavePath.Text, DownloadPrefix.Items.Item(cindex), DownloadComic.Items.Item(cindex))
                If primaryprecount > 0 Then
                    primary_PercentComplete = CSng(cindex + 1) / CSng(primaryprecount) * 100
                Else
                    primary_PercentComplete = 100
                End If
                primary_PercentComplete = primary_PercentComplete
                If primary_PercentComplete > primary_highestPercentageReached Then
                    primary_highestPercentageReached = primary_PercentComplete
                    worker.ReportProgress(primary_PercentComplete)
                End If
                cindex = cindex + 1
                If worker.CancellationPending Then
                    e.Cancel = True
                    Exit For
                End If
            Next



        Catch ex As Exception
            Error_Handler(ex, "MainWorkerFunction")
        End Try

        Return result

    End Function

    Private Sub PreCount_Function(ByVal filename As String)
        Try
            primaryprecount = 0
        Catch ex As Exception
            Error_Handler(ex, "PreCount_Function")
        End Try
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        StartWorker()
    End Sub

    Public Sub ParseAndDownload(ByVal URL As String, ByVal ImageName As String, ByVal Save As String, ByVal PrefixName As String, ByVal ComicName As String)
        Try
            Dim nummatches As Integer = 0
            Dim numdownloads As Integer = 0
            Dim urlstring As String = ""
            URL = URL.Trim
            Dim savelevel As Integer = 0
            Dim counturlstring As Integer
            counturlstring = 0
            Activity_Handler("URL to Parse: " & URL)
            Activity_Handler("Search String: " & ImageName)
            counturlstring = counturlstring + 1
            Try
                urlstring = URL
                urlstring = urlstring.Trim
                If urlstring.EndsWith("/") = False Then

                    If urlstring.LastIndexOf(".") = -1 Then
                        urlstring = urlstring & "/"
                    Else
                        If urlstring.LastIndexOf(".") < urlstring.LastIndexOf("/") Then
                            urlstring = urlstring & "/"
                        End If
                    End If
                End If

                Dim finfo As FileInfo
                finfo = New FileInfo((Application.StartupPath & "\toParse.htm").Replace("\\", "\"))
                If finfo.Exists = True Then
                    finfo.Delete()
                End If
                finfo = Nothing

                Dim displayGUI As Boolean = False
                If Silent_CheckBox.Checked = True Then
                    displayGUI = False
                Else
                    displayGUI = True
                End If

                SubStatus("Downloading HTML file to parse")
                'My.Computer.Network.DownloadFile(urlstring, (Application.StartupPath & "\toParse.htm").Replace("\\", "\"), username, password, displayGUI, 100000, True)

                Dim WbReq As New Net.WebClient
                WbReq.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials
                'Dim credcache As New CredentialCache()
                'Dim ncred As NetworkCredential = New NetworkCredential("01370106@wf.uct.ac.za", "", "wf.uct.ac.za")
                'credcache.Add(New Uri("http://campusnet.uct.ac.za"), "ntlm", ncred)
                'WbReq.Proxy.Credentials = credcache
                WbReq.DownloadFile(New Uri(urlstring), (Application.StartupPath & "\toParse.htm").Replace("\\", "\"))
                WbReq.Dispose()

                finfo = New FileInfo((Application.StartupPath & "\toParse.htm").Replace("\\", "\"))
                If finfo.Exists = True Then

                    Dim streamer As System.IO.StreamReader = New System.IO.StreamReader((Application.StartupPath & "\toParse.htm").Replace("\\", "\"))

                    Dim stringtoanalyze As String
                    Dim substring As String

                    Dim addeditem As Boolean
                    SubStatus("Parsing downloaded HTML file")
                    While streamer.Peek() <> -1

                        stringtoanalyze = streamer.ReadLine.ToLower()


                        Try
                            While stringtoanalyze.IndexOf("img ") > 0 And stringtoanalyze.IndexOf(" src=") > 0
                                If stringtoanalyze.IndexOf("href=""javascript:") < 0 And stringtoanalyze.IndexOf("href=javascript:") < 0 And stringtoanalyze.IndexOf("href='javascript:") < 0 Then
                                    stringtoanalyze = stringtoanalyze.Replace("=/""", "=""")
                                    Dim getmoreinfo As Boolean = False


                                    If (stringtoanalyze.IndexOf(">", stringtoanalyze.IndexOf(" src=")) > -1) Then
                                        If stringtoanalyze.Length < (stringtoanalyze.IndexOf(">", stringtoanalyze.IndexOf(" src=") + 5)) - (stringtoanalyze.IndexOf(" src=") + 5) + (stringtoanalyze.IndexOf(" src=") + 5) Then
                                            getmoreinfo = True
                                        Else
                                            getmoreinfo = False
                                        End If
                                    Else
                                        getmoreinfo = True
                                    End If

                                    While getmoreinfo = True
                                        If streamer.Peek() <> -1 Then
                                            stringtoanalyze = stringtoanalyze & vbCrLf & streamer.ReadLine.ToLower()
                                            stringtoanalyze = stringtoanalyze.Replace("=/""", "=""")
                                            If (stringtoanalyze.IndexOf(">", stringtoanalyze.IndexOf(" src=")) > -1) Then
                                                If stringtoanalyze.Length < (stringtoanalyze.IndexOf(">", stringtoanalyze.IndexOf(" src=") + 5)) - (stringtoanalyze.IndexOf(" src=") + 5) + (stringtoanalyze.IndexOf(" src=") + 5) Then
                                                    getmoreinfo = True
                                                Else
                                                    getmoreinfo = False
                                                End If
                                            Else
                                                getmoreinfo = True
                                            End If
                                        Else
                                            getmoreinfo = False
                                        End If
                                    End While

                                    substring = stringtoanalyze.Substring(stringtoanalyze.IndexOf(" src=") + 5, (stringtoanalyze.IndexOf(">", stringtoanalyze.IndexOf(" src=") + 5)) - (stringtoanalyze.IndexOf(" src=") + 5))
                                    stringtoanalyze = stringtoanalyze.Remove(0, (stringtoanalyze.IndexOf(">", stringtoanalyze.IndexOf(" src=") + 5)) - 1)

                                    Dim hrefwithquotations As Boolean
                                    hrefwithquotations = False

                                    If substring.StartsWith("""") = True Or substring.StartsWith("'") = True Then
                                        substring = substring.Remove(0, 1)
                                        hrefwithquotations = True
                                    End If
                                    'MsgBox(substring)
                                    If hrefwithquotations = False Then
                                        If substring.IndexOf(" ") > -1 Then
                                            substring = substring.Remove(substring.IndexOf(" "), substring.Length - substring.IndexOf(" "))
                                        End If
                                    End If
                                    'MsgBox(substring)
                                    If substring.IndexOf("""") > -1 Then
                                        substring = substring.Remove(substring.IndexOf(""""), substring.Length - substring.IndexOf(""""))
                                    End If
                                    If substring.EndsWith("'") = True And hrefwithquotations = True Then
                                        substring = substring.Remove(substring.Length - 1, 1)
                                    End If
                                    If substring.EndsWith("/") Then
                                        substring = substring.Remove(substring.Length - 1, 1)
                                    End If
                                    'MsgBox(substring)
                                    Dim tempurlstring As String
                                    tempurlstring = urlstring

                                    If tempurlstring.LastIndexOf("?") > -1 Then
                                        'MsgBox(tempurlstring & "   " & tempurlstring.LastIndexOf("?") & "   " & tempurlstring.Length)
                                        tempurlstring = tempurlstring.Remove(tempurlstring.LastIndexOf("?"), tempurlstring.Length - tempurlstring.LastIndexOf("?"))
                                        'MsgBox(tempurlstring & "   " & tempurlstring.LastIndexOf("?") & "   " & tempurlstring.Length)
                                    End If

                                    If Not substring.StartsWith("http:") Then
                                        If substring.StartsWith("/") Then

                                            substring = tempurlstring.Substring(0, (tempurlstring.IndexOf("/", 7))) & substring
                                        Else

                                            substring = tempurlstring.Substring(0, (tempurlstring.LastIndexOf("/") + 1)) & substring
                                        End If
                                    End If
                                    addeditem = False
                                    ' For Each acceptable In acceptable_items

                                    If addeditem = False Then
                                        Dim workwith As String = substring
                                        Dim match As Boolean = True
                                        Dim proceed As Boolean = True
                                        If ImageName.IndexOf("+") <> -1 Then
                                            Dim splitstr As String() = ImageName.Split("+")
                                            If splitstr.Length > 1 Then
                                                If substring.IndexOf("/") <> -1 Then
                                                    workwith = substring.Substring(substring.LastIndexOf("/") + 1)
                                                Else
                                                    workwith = substring
                                                End If


                                                'MsgBox(workwith.ToLower & " -- " & splitstr(0).ToLower & " -- " & splitstr(1).ToLower)
                                                If workwith.ToLower.StartsWith(splitstr(0).ToLower) = True And workwith.ToLower.EndsWith(splitstr(1).ToLower) = True Then
                                                    match = True
                                                    proceed = False
                                                End If
                                            End If
                                        End If

                                        If proceed = True Then
                                            If substring.Length >= ImageName.Length Then
                                                workwith = substring.Substring(substring.Length - ImageName.Length)
                                                'MsgBox(workwith & " --> " & ImageName)
                                                Dim i As Integer

                                                For i = 0 To ImageName.Length - 1

                                                    If Not ImageName.Chars(i) = "*" Then
                                                        If Not ImageName.ToLower.Chars(i) = workwith.ToLower.Chars(i) Then
                                                            match = False
                                                        End If
                                                    End If
                                                    If workwith.ToLower.Chars(i) = "/" Then
                                                        match = False
                                                    End If
                                                Next
                                            Else
                                                match = False
                                            End If
                                        End If

                                        If match = True Then
                                            SubStatus("Parsing revealed pattern match URL")
                                            Dim newfilegrabbed As Boolean = False
                                            nummatches = nummatches + 1

                                            'MsgBox("!!!MATCH!!! -- " & substring & " -- " & substring.Substring(substring.Length - ImageName.Length))
                                            Dim ffinfo As FileInfo
                                            ffinfo = New FileInfo((Save & "\" & PrefixName & substring.Substring(substring.Length - ImageName.Length)).Replace("\\", "\"))

                                            'If ffinfo.Exists = False Then
                                            If My.Computer.FileSystem.DirectoryExists((Application.StartupPath & "\Temp").Replace("\\", "\")) = False Then
                                                My.Computer.FileSystem.CreateDirectory((Application.StartupPath & "\Temp").Replace("\\", "\"))
                                            End If
                                            Dim tempFile As String = ((Application.StartupPath & "\Temp").Replace("\\", "\") & "\" & ffinfo.Name).Replace("\\", "\")
                                            '************************
                                            'Special Rule Application
                                            '************************
                                            Try
                                                SubStatus("Checking special rules against match URL")
                                                Dim SpecialFile As String = (Application.StartupPath & "\" & "Special_Rules.txt").Replace("\\", "\")
                                                If My.Computer.FileSystem.FileExists(SpecialFile) = True Then
                                                    Dim readerSpecialFile As StreamReader = My.Computer.FileSystem.OpenTextFileReader(SpecialFile)
                                                    Dim linereadSpecialFile As String = ""
                                                    While readerSpecialFile.Peek <> -1
                                                        linereadSpecialFile = readerSpecialFile.ReadLine
                                                        If linereadSpecialFile.IndexOf("|") <> -1 Then
                                                            Dim replacesSpecialFile As String() = linereadSpecialFile.Split("|")
                                                            'MsgBox(replacesSpecialFile.Length & " | " & replacesSpecialFile(0) & " | " & replacesSpecialFile(1) & " | " & substring)
                                                            If replacesSpecialFile.Length = 2 Then
                                                                substring = substring.Replace(replacesSpecialFile(0), replacesSpecialFile(1))
                                                            End If
                                                        End If
                                                    End While
                                                    readerSpecialFile.Close()
                                                    readerSpecialFile = Nothing
                                                End If
                                            Catch ex As Exception
                                                Error_Handler(ex, "Special Rule Application")
                                            End Try
                                            '************************
                                            Activity_Handler("Matched: " & substring)

                                            Dim moveexistingfile As Boolean = False
                                            Dim moveexistingfilename As String = ffinfo.FullName
                                            Dim moveexistingfilenamenew As String = ffinfo.FullName.Insert(ffinfo.FullName.LastIndexOf("."), Format(ffinfo.LastWriteTime, "yyyyMMddHHmmss"))
                                            Dim runtests As Boolean = False
                                            SubStatus("Downloading matched URL to temporary location")
                                            My.Computer.Network.DownloadFile(substring, tempFile, username, password, displayGUI, 100000, True)
                                            SubStatus("Testing validity of downloaded image")
                                            If My.Computer.FileSystem.FileExists(tempFile) Then

                                                '************************
                                                'Test that the downloaded file is correct in size
                                                '************************
                                                Try


                                                    Dim sURL As String
                                                    sURL = substring
                                                    Dim wrGETURL As HttpWebRequest
                                                    wrGETURL = CType(HttpWebRequest.Create(sURL), HttpWebRequest)
                                                    wrGETURL.Proxy = WebProxy.GetDefaultProxy()
                                                    Dim ws As HttpWebResponse = CType(wrGETURL.GetResponse(), HttpWebResponse)
                                                    Dim testfile As FileInfo = New FileInfo(tempFile)

                                                    If ws.ContentLength = testfile.Length Then
                                                        'file has been downloaded successfully
                                                        runtests = True
                                                    End If
                                                    testfile = Nothing
                                                    ws.Close()
                                                    ws = Nothing
                                                    wrGETURL = Nothing
                                                Catch ex As Exception
                                                    Error_Handler(ex, "Test Downloaded File Size")
                                                    LastActionLabel.Text = "Last Action: " & ComicName & " - " & "Rejected. Incomplete File Download"
                                                End Try
                                                '************************

                                                If runtests = True Then
                                                    SubStatus("Testing downloaded image against existing files")

                                                    If ffinfo.Exists = False Then
                                                        My.Computer.FileSystem.MoveFile(tempFile, ffinfo.FullName, True)
                                                        newfilegrabbed = True
                                                    Else
                                                        Dim tempf As FileInfo = New FileInfo(tempFile)
                                                        If tempf.Length <> ffinfo.Length Then
                                                            moveexistingfile = True
                                                        Else
                                                            tempf.Delete()
                                                            newfilegrabbed = False
                                                            Activity_Handler("Download Rejected: File Has Previously Been Downloaded")
                                                            LastActionLabel.Text = "Last Action: " & ComicName & " - " & "Rejected. File Already Exists"
                                                        End If
                                                        tempf = Nothing
                                                    End If

                                                End If

                                            End If
                                            ffinfo = Nothing

                                            If runtests = True Then


                                                If moveexistingfile = True Then
                                                    My.Computer.FileSystem.MoveFile(moveexistingfilename, moveexistingfilenamenew, True)
                                                    My.Computer.FileSystem.MoveFile(tempFile, (Save & "\" & PrefixName & substring.Substring(substring.Length - ImageName.Length)).Replace("\\", "\"), True)
                                                    Dim resetinfo As FileInfo = New FileInfo((Save & "\" & PrefixName & substring.Substring(substring.Length - ImageName.Length)).Replace("\\", "\"))
                                                    resetinfo.CreationTime = Now
                                                    resetinfo = Nothing
                                                    newfilegrabbed = True
                                                End If



                                                If newfilegrabbed = True Then
                                                    SubStatus("New image successfully downloaded")
                                                    Activity_Handler("New Download URL: " & substring)
                                                    Activity_Handler("Download Accepted: All Criteria Met")
                                                    Activity_Handler("Downloaded To: " & (Save & "\" & PrefixName & substring.Substring(substring.Length - ImageName.Length)).Replace("\\", "\"))
                                                    LastActionLabel.Text = "Last Action: " & ComicName & " - " & "Accepted. File Successfully Stored"
                                                    numdownloads = numdownloads + 1
                                                    filesdownloadednum = filesdownloadednum + 1
                                                    todaydownloads = todaydownloads + 1
                                                    filesdownloadednumthiscycle = filesdownloadednumthiscycle + 1
                                                Else
                                                    SubStatus("Downloaded image has been rejected")
                                                End If
                                            Else
                                                If My.Computer.FileSystem.FileExists(tempFile) = True Then
                                                    Activity_Handler("Download Rejected: Incomplete File Downloaded")
                                                    LastActionLabel.Text = "Last Action: " & ComicName & " - " & "Rejected. Incomplete File Download"
                                                    My.Computer.FileSystem.DeleteFile(tempFile)
                                                End If
                                            End If

                                        End If

                                        'If substring.EndsWith(acceptable) And substring.StartsWith("http://www.dilbert.com/comics/dilbert/archive/images/dilbert") Then
                                        '    'Dim downitem As Download_Item = New Download_Item(substring.Replace("%5F", "_").Replace("%20", " ").Trim(), substring.Remove(0, substring.LastIndexOf("/") + 1).Replace("%5F", "_").Replace("%20", " "), download_item_queue.Count, True, savelevel)
                                        '    Dim downitem As Download_Item = New Download_Item(substring.Replace("%5F", "_").Replace("%20", " ").Trim(), "latestcomic.gif", download_item_queue.Count, True, savelevel)
                                        '    download_item_queue.Add(downitem)
                                        '    addeditem = True
                                        'End If
                                    End If
                                    'Next
                                Else
                                    stringtoanalyze = stringtoanalyze.Remove(stringtoanalyze.IndexOf("href=""javascript:"), 6)
                                End If
                            End While
                        Catch ex As Exception
                            'MsgBox(stringtoanalyze)
                            Error_Handler(ex)
                        End Try
                    End While
                    Activity_Handler("Number of Search Matches: " & nummatches)
                    Activity_Handler("Number of Downloads: " & numdownloads)
                    Activity_Handler(" ")

                    streamer.Close()

                    finfo.Delete()
                End If
                finfo = Nothing


            Catch ex As Exception
                Error_Handler(ex)
                LastActionLabel.Text = "Last Action: " & ComicName & " - " & "Error Encountered"
            End Try
            savelevel = savelevel + 1
        Catch ex As Exception
            Error_Handler(ex)
            LastActionLabel.Text = "Last Action: " & ComicName & " - " & "Error Encountered"
        End Try

    End Sub


    Private Sub Export_List()
        Try
            Dim result As DialogResult
            SaveFileDialog1.FileName = "ComicSD_DL_List" & Format(Now(), "yyyyMMdd")
            result = SaveFileDialog1.ShowDialog
            If result = Windows.Forms.DialogResult.OK Then
                Dim filewriter As System.IO.StreamWriter = New System.IO.StreamWriter(SaveFileDialog1.FileName, False)
                Dim str As String = ""
                Dim cindex As Integer = 0
                filewriter.WriteLine("Comic Strip Downloader Exported List: " & Format(Now(), "dd/MM/yyyy hh:mm:ss tt"))
                For Each str In DownloadPath.Items
                    filewriter.WriteLine(str)
                    filewriter.WriteLine(DownloadImage.Items.Item(cindex))
                    filewriter.WriteLine(DownloadPrefix.Items.Item(cindex))
                    filewriter.WriteLine(DownloadComic.Items.Item(cindex))
                    cindex = cindex + 1
                Next
                filewriter.Flush()
                filewriter.Close()
                filewriter = Nothing
                Status("Download List Successfully Exported")
            End If
        Catch ex As Exception
            Error_Handler(ex, "Export List")
        End Try
    End Sub


    Private Sub Import_List()
        Try
            Dim result, result2 As DialogResult
            'OpenFileDialog1.FileName = "ComicSD_DL_List" & Format(Now(), "yyyyMMdd")
            OpenFileDialog1.FileName = ""
            result = OpenFileDialog1.ShowDialog
            If result = Windows.Forms.DialogResult.OK Then
                result2 = MsgBox("Do you wish to clear your current download list?", MsgBoxStyle.YesNo, "Clear Current List")
                If result2 = Windows.Forms.DialogResult.Yes Then
                    DownloadPath.Items.Clear()
                    DownloadImage.Items.Clear()
                    DownloadPrefix.Items.Clear()
                    DownloadComic.Items.Clear()
                End If

                Dim filereader As System.IO.StreamReader = New System.IO.StreamReader(OpenFileDialog1.FileName)
                Dim str As String = ""
                If filereader.Peek <> -1 Then
                    filereader.ReadLine()
                End If
                While filereader.Peek <> -1
                    DownloadPath.Items.Add(filereader.ReadLine)
                    DownloadImage.Items.Add(filereader.ReadLine)
                    DownloadPrefix.Items.Add(filereader.ReadLine)
                    DownloadComic.Items.Add(filereader.ReadLine)
                End While
                filereader.Close()
                filereader = Nothing
                Status("Download List Successfully Imported")
            End If
        Catch ex As Exception
            Error_Handler(ex, "Import List")
        End Try
    End Sub

    Private Function File_Exists(ByVal file_path As String) As Boolean
        Dim result As Boolean = False
        Try
            If Not file_path = "" And Not file_path Is Nothing Then
                Dim dinfo As FileInfo = New FileInfo(file_path)
                If dinfo.Exists = False Then
                    result = False
                Else
                    result = True
                End If
                dinfo = Nothing
            End If
        Catch ex As Exception
            Error_Handler(ex, "File_Exists")
        End Try
        Return result
    End Function

    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Try
            If File_Exists((Application.StartupPath & "\").Replace("\\", "\") & "Activity Logs\" & Format(Now(), "yyyyMMdd") & "_Activity_Log.txt") = True Then
                Dim systemDirectory As String
                systemDirectory = System.Environment.SystemDirectory
                Dim finfo As FileInfo = New FileInfo((systemDirectory & "\notepad.exe").Replace("\\", "\"))
                If finfo.Exists = True Then
                    Dim apptorun As String
                    apptorun = """" & (systemDirectory & "\notepad.exe").Replace("\\", "\") & """ """ & (Application.StartupPath & "\").Replace("\\", "\") & "Activity Logs\" & Format(Now(), "yyyyMMdd") & "_Activity_Log.txt" & """"
                    Dim procID As Integer = Shell(apptorun, AppWinStyle.NormalFocus, False)
                End If
                finfo = Nothing
            End If
        Catch ex As Exception
            Error_Handler(ex, "Open Activity Log")
        End Try
    End Sub

    Private Sub LinkLabel2_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        Try
            If File_Exists((Application.StartupPath & "\").Replace("\\", "\") & "Error Logs\" & Format(Now(), "yyyyMMdd") & "_Error_Log.txt") = True Then
                Dim systemDirectory As String
                systemDirectory = System.Environment.SystemDirectory
                Dim finfo As FileInfo = New FileInfo((systemDirectory & "\notepad.exe").Replace("\\", "\"))
                If finfo.Exists = True Then
                    Dim apptorun As String
                    apptorun = """" & (systemDirectory & "\notepad.exe").Replace("\\", "\") & """ """ & (Application.StartupPath & "\").Replace("\\", "\") & "Error Logs\" & Format(Now(), "yyyyMMdd") & "_Error_Log.txt" & """"
                    Dim procID As Integer = Shell(apptorun, AppWinStyle.NormalFocus, False)
                End If
                finfo = Nothing
            End If
        Catch ex As Exception
            Error_Handler(ex, "Open Error Log")
        End Try
    End Sub

    Private Sub ImportDownloadListToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ImportDownloadListToolStripMenuItem.Click
        Timer1.Stop()
        downloadcycle.ForeColor = Color.Red
        Import_List()
        Timer1.Start()
        downloadcycle.ForeColor = Color.Green
    End Sub

    Private Sub ExportDownloadListToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExportDownloadListToolStripMenuItem.Click
        Timer1.Stop()
        downloadcycle.ForeColor = Color.Red
        Export_List()
        Timer1.Start()
        downloadcycle.ForeColor = Color.Green
    End Sub

    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Close()
    End Sub

    Private Sub NotifyIcon1_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles NotifyIcon1.MouseDoubleClick
        If Not Me.WindowState = FormWindowState.Normal Then
            show_main_application()
        Else
            hide_main_application()
        End If
    End Sub

    Private Sub InToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles InToolStripMenuItem.Click
        Me.Close()
    End Sub

    Private Sub MinimiseMaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MinimiseMaToolStripMenuItem.Click
        hide_main_application()
    End Sub

    Private Sub ShowMainScreenToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShowMainScreenToolStripMenuItem.Click
        show_main_application()
    End Sub

    Private Sub show_main_application()
        Try
            Me.Opacity = 1
            Me.BringToFront()
            Me.Refresh()
            Me.WindowState = FormWindowState.Normal
            Me.Focus()
            'Me.WindowState = FormWindowState.Normal
            'Me.Visible = True
            'Me.Opacity = 100
        Catch ex As Exception
            Error_Handler(ex, "show_main_application")
        End Try
    End Sub

    Private Sub hide_main_application()
        Try
            Me.WindowState = FormWindowState.Minimized
            If Me.WindowState = FormWindowState.Minimized Then
                NotifyIcon1.Visible = True
                Me.Opacity = 0
            End If
            'Me.WindowState = FormWindowState.Minimized
            'Me.Visible = False
            'Me.Opacity = 0
        Catch ex As Exception
            Error_Handler(ex, "hide_main_application")
        End Try
    End Sub

    Private Sub ToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem1.Click
        hide_main_application()
    End Sub

    Private Sub Label5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label5.Click
        hide_main_application()
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        Try
            Dim newdl As NewDownload = New NewDownload()
            If DownloadPath.Items.Count > 0 Then
                If DownloadPath.SelectedIndex > -1 Then
                    newdl.DownloadURL.Text = DownloadPath.Items.Item(DownloadPath.SelectedIndex)
                    newdl.DownloadFile.Text = DownloadImage.Items.Item(DownloadPath.SelectedIndex)
                    newdl.DownloadFilePrefix.Text = DownloadPrefix.Items.Item(DownloadPath.SelectedIndex)
                    newdl.DownloadComic.Text = DownloadComic.Items.Item(DownloadPath.SelectedIndex)

                    Dim result As DialogResult = newdl.ShowDialog()
                    If result = Windows.Forms.DialogResult.OK Then
                        If newdl.DownloadFilePrefix.Text.Length > 0 And newdl.DownloadFile.Text.Length > 0 And newdl.DownloadURL.Text.Length > 7 Then
                            DownloadPath.Items.Item(DownloadPath.SelectedIndex) = (newdl.DownloadURL.Text)
                            DownloadImage.Items.Item(DownloadPath.SelectedIndex) = (newdl.DownloadFile.Text)
                            DownloadPrefix.Items.Item(DownloadPath.SelectedIndex) = (newdl.DownloadFilePrefix.Text)
                            DownloadComic.Items.Item(DownloadPath.SelectedIndex) = (newdl.DownloadComic.Text)
                            Status("Download Details Edited.")
                        Else
                            Status("Incomplete Data Detected. Download Not Edited.")
                        End If
                    Else
                        Status("Download Details Cancellation Detected.")
                    End If
                    newdl.Close()
                End If
            End If
            newdl.Dispose()
            NumberDownloadEntries.Text = DownloadPath.Items.Count & " Download Entries"
        Catch ex As Exception
            Error_Handler(ex, "Edit Download Request")
        End Try
    End Sub

    Private Sub HelpToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HelpToolStripMenuItem.Click
        Try
            Me.ToolStripStatusLabel1.Text = "Help displayed"
            HelpBox1.ShowDialog()
        Catch ex As Exception
            Error_Handler(ex, "Display Help Screen")
        End Try
    End Sub

    

    Private Sub ToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem2.Click
        Timer1.Stop()
        downloadcycle.ForeColor = Color.Red
        Try
            Dim result, result2 As DialogResult
            'OpenFileDialog1.FileName = "ComicSD_DL_List" & Format(Now(), "yyyyMMdd")
            OpenFileDialog1.FileName = ""
            result = OpenFileDialog1.ShowDialog
            If result = Windows.Forms.DialogResult.OK Then

                SaveFileDialog2.FileName = "RSS Feed Generator"
                result2 = SaveFileDialog2.ShowDialog
                If result2 = Windows.Forms.DialogResult.OK Then
                    Dim filewriter As System.IO.StreamWriter = New System.IO.StreamWriter(SaveFileDialog2.FileName, False)
                    Dim str As String = ""
                    Dim cindex As Integer = 0
                    filewriter.WriteLine("<?xml version=""1.0"" encoding=""utf-8""?>")
                    filewriter.WriteLine("<?xml-stylesheet type=""text/css"" href=""feedstyle.css""?>")
                    filewriter.WriteLine("<rss version=""2.0"">")
                    filewriter.WriteLine("<channel>")
                    filewriter.WriteLine("<title>Daily Comics</title>")
                    filewriter.WriteLine("<description>Harvesting the best of Comics</description>")
                    filewriter.WriteLine("<link>http://www.commerce.uct.ac.za/Services/Daily Comic Strips/</link>")
                    filewriter.WriteLine("<language>en-us</language>")
                    filewriter.WriteLine("<generator>Classic ASP</generator>")
                    filewriter.WriteLine("")
                    filewriter.WriteLine("<image>")
                    filewriter.WriteLine("<url>http://www.commerce.uct.ac.za/Services/Daily Comic Strips/Logo/logo.jpg</url>")
                    filewriter.WriteLine("<title>Daily Comics</title>")
                    filewriter.WriteLine("<link>http://www.commerce.uct.ac.za/Services/Daily Comic Strips/</link>")
                    filewriter.WriteLine("<description>Harvesting the best of Comics</description>")
                    filewriter.WriteLine("</image>")
                    filewriter.WriteLine("<%")
                    filewriter.WriteLine("' In this demo, at least, we don't allow user to change directories...")
                    filewriter.WriteLine("' Change the DIRECTORY to point to any virtual directory of your choice.")
                    filewriter.WriteLine("CONST DIRECTORY = ""/Services/Daily Comic Strips/Images"" ' relative path in virtual directories")
                    filewriter.WriteLine("")
                    filewriter.WriteLine("' Specify one of these constants for ""sortBy""...")
                    filewriter.WriteLine("CONST FILE_NAME = 0")
                    filewriter.WriteLine("CONST ALTER_FILE_NAME = 1")
                    filewriter.WriteLine("CONST FILE_EXT = 2")
                    filewriter.WriteLine("CONST FILE_TYPE = 3")
                    filewriter.WriteLine("CONST FILE_SIZE = 4")
                    filewriter.WriteLine("CONST FILE_CREATED = 5")
                    filewriter.WriteLine("CONST FILE_MODIFIED = 6")
                    filewriter.WriteLine("CONST FILE_ACCESSED = 7")
                    filewriter.WriteLine("")
                    filewriter.WriteLine("' get requested sort order, if not first time here...")
                    filewriter.WriteLine("' (forward by name is default)")
                    filewriter.WriteLine("req = Request(""sortBy"")")
                    filewriter.WriteLine("If Len(req) < 1 Then")
                    filewriter.WriteLine("'sortBy = 1")
                    filewriter.WriteLine("sortBy = FILE_CREATED")
                    filewriter.WriteLine("priorSort = FILE_CREATED")
                    filewriter.WriteLine("Else")
                    filewriter.WriteLine("sortBy = CInt(req)")
                    filewriter.WriteLine("End if")
                    filewriter.WriteLine("req = Request(""priorSort"")")
                    filewriter.WriteLine("If Len(req) < 1 Then")
                    filewriter.WriteLine("sortBy = FILE_CREATED")
                    filewriter.WriteLine("priorSort = FILE_CREATED")
                    filewriter.WriteLine("'priorSort = -1")
                    filewriter.WriteLine("Else")
                    filewriter.WriteLine("priorSort = CInt(req)")
                    filewriter.WriteLine("End If")
                    filewriter.WriteLine("")
                    filewriter.WriteLine("")
                    filewriter.WriteLine("")
                    filewriter.WriteLine("'")
                    filewriter.WriteLine("' did user ask for same sort? to reverse the order?")
                    filewriter.WriteLine("' but if so, then zap priorSort so clicking again will do forward!")
                    filewriter.WriteLine("If sortBy = priorSort Then")
                    filewriter.WriteLine("reverse = true")
                    filewriter.WriteLine("priorSort = -1")
                    filewriter.WriteLine("Else")
                    filewriter.WriteLine("reverse = false")
                    filewriter.WriteLine("priorSort = sortBy")
                    filewriter.WriteLine("End If")
                    filewriter.WriteLine("")
                    filewriter.WriteLine("")
                    filewriter.WriteLine("' now start the *real* code...")
                    filewriter.WriteLine("'")
                    filewriter.WriteLine("path = Server.MapPath( DIRECTORY )")
                    filewriter.WriteLine("")
                    filewriter.WriteLine("Set fso = CreateObject(""Scripting.FileSystemObject"")")
                    filewriter.WriteLine("Set theCurrentFolder = fso.GetFolder( path )")
                    filewriter.WriteLine("Set curFiles = theCurrentFolder.Files")
                    filewriter.WriteLine("'")
                    filewriter.WriteLine("' And now a loop for the files")
                    filewriter.WriteLine("'")
                    filewriter.WriteLine("Dim theFiles( )")
                    filewriter.WriteLine("ReDim theFiles( 500 ) ' arbitrary size!")
                    filewriter.WriteLine("currentSlot = -1 ' start before first slot")
                    filewriter.WriteLine("")
                    filewriter.WriteLine("' We collect all the info about each file and put it into one")
                    filewriter.WriteLine("' ""slot"" in our ""theFiles"" array.")
                    filewriter.WriteLine("'")
                    filewriter.WriteLine("For Each fileItem in curFiles")
                    filewriter.WriteLine("fname = fileItem.Name")
                    filewriter.WriteLine("alteredfname = left(fileItem.Name,3) & fileItem.DateCreated & right(fileItem.Name,(len(fileItem.Name)-3))")
                    filewriter.WriteLine("fext = InStrRev( fname, ""."" )")
                    filewriter.WriteLine("If fext < 1 Then fext = """" Else fext = Mid(fname,fext+1)")
                    filewriter.WriteLine("ftype = fileItem.Type")
                    filewriter.WriteLine("fsize = fileItem.Size")
                    filewriter.WriteLine("fcreate = fileItem.DateCreated")
                    filewriter.WriteLine("fmod = fileItem.DateLastModified")
                    filewriter.WriteLine("faccess = fileItem.DateLastAccessed")
                    filewriter.WriteLine("currentSlot = currentSlot + 1")
                    filewriter.WriteLine("If currentSlot > UBound( theFiles ) Then")
                    filewriter.WriteLine("ReDim Preserve theFiles( currentSlot + 99 )")
                    filewriter.WriteLine("End If")
                    filewriter.WriteLine("' note that what we put here is an array!")
                    filewriter.WriteLine("theFiles(currentSlot) = Array(fname,alteredfname,fext,ftype,fsize,fcreate,fmod,faccess)")
                    filewriter.WriteLine("Next")
                    filewriter.WriteLine("'")
                    filewriter.WriteLine("' files are now in the array...")
                    filewriter.WriteLine("'")
                    filewriter.WriteLine("' As noted, it is actually an ARRAY *OF* ARRAYS. Which makes")
                    filewriter.WriteLine("' picking the column we will sort on easier!")
                    filewriter.WriteLine("'")
                    filewriter.WriteLine("' ...size and sort it...")
                    filewriter.WriteLine("fileCount = currentSlot ' actually, count is 1 more, since we start at 0")
                    filewriter.WriteLine("ReDim Preserve theFiles( currentSlot ) ' really not necessary...just neater!")
                    filewriter.WriteLine("")
                    filewriter.WriteLine("' First, determine which ""kind"" of sort we are doing.")
                    filewriter.WriteLine("' (VarType=8 means ""string"")")
                    filewriter.WriteLine("'")
                    filewriter.WriteLine("If VarType( theFiles( 0 )( sortBy ) ) = 8 Then")
                    filewriter.WriteLine("If reverse Then kind = 1 Else kind = 2 ' sorting strings...")
                    filewriter.WriteLine("Else")
                    filewriter.WriteLine("If reverse Then kind = 3 Else kind = 4 ' non-strings (numbers, dates)")
                    filewriter.WriteLine("End If")
                    filewriter.WriteLine("")
                    filewriter.WriteLine("'")
                    filewriter.WriteLine("' A simple bubble sort for now...easier to follow the code...")
                    filewriter.WriteLine("'")
                    filewriter.WriteLine("For i = fileCount TO 0 Step -1")
                    filewriter.WriteLine("minmax = theFiles( 0 )( sortBy )")
                    filewriter.WriteLine("minmaxSlot = 0")
                    filewriter.WriteLine("For j = 1 To i")
                    filewriter.WriteLine("Select Case kind ' which kind of sort are we doing?")
                    filewriter.WriteLine("' after the ""is bigger/smaller"" test (as appropriate),")
                    filewriter.WriteLine("' mark will be true if we need to ""remember"" this slot...")
                    filewriter.WriteLine("Case 1 ' string, reverse...we do case INsensitive!")
                    filewriter.WriteLine("mark = (strComp( theFiles(j)(sortBy), minmax, vbTextCompare ) < 0)")
                    filewriter.WriteLine("Case 2 ' string, forward...we do case INsensitive!")
                    filewriter.WriteLine("mark = (strComp( theFiles(j)(sortBy), minmax, vbTextCompare ) > 0)")
                    filewriter.WriteLine("Case 3 ' non-string, reverse ...")
                    filewriter.WriteLine("mark = (theFiles( j )( sortBy ) < minmax)")
                    filewriter.WriteLine("Case 4 ' non-string, forward ...")
                    filewriter.WriteLine("mark = (theFiles( j )( sortBy ) > minmax)")
                    filewriter.WriteLine("End Select")
                    filewriter.WriteLine("' so is the current slot bigger/smaller than the remembered one?")
                    filewriter.WriteLine("If mark Then")
                    filewriter.WriteLine("' yep, so remember this one instead!")
                    filewriter.WriteLine("minmax = theFiles( j )( sortBy )")
                    filewriter.WriteLine("minmaxSlot = j")
                    filewriter.WriteLine("End If")
                    filewriter.WriteLine("Next")
                    filewriter.WriteLine("' is the last slot the min (or max), as it should be?")
                    filewriter.WriteLine("If minmaxSlot <> i Then")
                    filewriter.WriteLine("' nope...so do the needed swap...")
                    filewriter.WriteLine("temp = theFiles( minmaxSlot )")
                    filewriter.WriteLine("theFiles( minmaxSlot ) = theFiles( i )")
                    filewriter.WriteLine("theFiles( i ) = temp")
                    filewriter.WriteLine("End If")
                    filewriter.WriteLine("Next")
                    filewriter.WriteLine("' Ta-da! The array is sorted!")
                    filewriter.WriteLine("'")
                    filewriter.WriteLine("")
                    filewriter.WriteLine("Dim lastwritten")
                    filewriter.WriteLine("lastwritten = """"")
                    filewriter.WriteLine("")
                    filewriter.WriteLine("")
                    filewriter.WriteLine("' With the array nicely sorted, this part is a piece of cake!")
                    filewriter.WriteLine("For i = 0 To 30")
                    filewriter.WriteLine("")
                    filewriter.WriteLine("response.write ""<item>"" & vbcrlf")
                    filewriter.WriteLine("")
                    filewriter.WriteLine("")
                    filewriter.WriteLine("response.write ""<title>""")
                    filewriter.WriteLine("lastwritten = lcase(left(theFiles(i)(0), 3))")
                    filewriter.WriteLine("Select Case lastwritten")
                    For Each str In DownloadPath.Items
                        filewriter.WriteLine("Case """ & DownloadPrefix.Items.Item(cindex) & """")
                        filewriter.WriteLine("Response.Write(""" & DownloadComic.Items.Item(cindex) & """)")
                        cindex = cindex + 1
                    Next
                    filewriter.WriteLine("Case Else")
                    filewriter.WriteLine("Response.Write(""Unknown Comic"")")
                    filewriter.WriteLine("End Select")
                    filewriter.WriteLine("response.write ""</title>"" & vbcrlf")
                    filewriter.WriteLine("response.write ""<description>&lt;br&gt;&lt;img src=&quot;http://www.commerce.uct.ac.za/"" & DIRECTORY & ""/"" & theFiles(i)(0) & ""&quot;&gt;"" & ""</description>"" & vbcrlf")
                    filewriter.WriteLine("")
                    filewriter.WriteLine("response.write ""<pubDate>"" & theFiles(i)(6) & ""</pubDate>"" & vbcrlf")
                    filewriter.WriteLine("response.write ""<link>http://www.commerce.uct.ac.za/Services/Daily Comic Strips/</link>"" & vbcrlf")
                    filewriter.WriteLine("response.write ""<guid>http://www.commerce.uct.ac.za/"" & DIRECTORY & ""/"" & theFiles(i)(0) & ""</guid>"" & vbcrlf")
                    filewriter.WriteLine("response.write ""</item>"" & vbcrlf")
                    filewriter.WriteLine("")
                    filewriter.WriteLine("Next")
                    filewriter.WriteLine("response.write ""<item>"" & vbcrlf")
                    filewriter.WriteLine("response.write ""<title>Full Listing</title>""")
                    filewriter.WriteLine("response.write ""<description>&lt;a href=&quot;http://www.commerce.uct.ac.za/Services/Daily Comic Strips/&quot;&gt;Click here to get a full listing of all the Daily Comics ever harvested&lt;/a&gt;</description>""")
                    filewriter.WriteLine("response.write ""<pubDate>"" & now() & ""</pubDate>"" & vbcrlf")
                    filewriter.WriteLine("response.write ""<link>http://www.commerce.uct.ac.za/Services/Daily Comic Strips/</link>"" & vbcrlf")
                    filewriter.WriteLine("response.write ""</item>"" & vbcrlf")
                    filewriter.WriteLine("response.write ""</channel>"" & vbcrlf")
                    filewriter.WriteLine("response.write ""</rss>"" & vbcrlf")
                    filewriter.WriteLine("%>")

                    filewriter.Flush()
                    filewriter.Close()
                    filewriter = Nothing
                    Status("RSS Code Successfully Generated")

                    If File_Exists(SaveFileDialog2.FileName) = True Then
                        Dim systemDirectory As String
                        systemDirectory = System.Environment.SystemDirectory
                        Dim finfo As FileInfo = New FileInfo((systemDirectory & "\notepad.exe").Replace("\\", "\"))
                        If finfo.Exists = True Then
                            Dim apptorun As String
                            apptorun = """" & (systemDirectory & "\notepad.exe").Replace("\\", "\") & """ """ & SaveFileDialog2.FileName & """"
                            Dim procID As Integer = Shell(apptorun, AppWinStyle.NormalFocus, False)
                        End If
                        finfo = Nothing
                    End If
                End If
            End If
        Catch ex As Exception
            Error_Handler(ex, "Generate RSS Code")
        End Try
        Timer1.Start()
        downloadcycle.ForeColor = Color.Green
    End Sub

    Private Sub Main_Screen_Resize(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        If Me.WindowState = FormWindowState.Minimized Then
            hide_main_application()
        End If
    End Sub

    Private Sub ProxyUsernamePasswordToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ProxyUsernamePasswordToolStripMenuItem.Click
        Timer1.Stop()
        downloadcycle.ForeColor = Color.Red
        Try
            Dim proxy As ProxyDetails = New ProxyDetails
            proxy.ShowDialog()

        Catch ex As Exception
            Error_Handler(ex, "Set Proxy Username and Password")
        End Try
        Timer1.Start()
        downloadcycle.ForeColor = Color.Green
    End Sub

    Private Sub GenerateWebPageCodeToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GenerateWebPageCodeToolStripMenuItem.Click
        Timer1.Stop()
        downloadcycle.ForeColor = Color.Red
        Try
            Dim result, result2 As DialogResult
            'OpenFileDialog1.FileName = "ComicSD_DL_List" & Format(Now(), "yyyyMMdd")
            OpenFileDialog1.FileName = ""
            result = OpenFileDialog1.ShowDialog
            If result = Windows.Forms.DialogResult.OK Then

                SaveFileDialog2.FileName = "Web Page Generator"
                result2 = SaveFileDialog2.ShowDialog
                If result2 = Windows.Forms.DialogResult.OK Then
                    Dim filewriter As System.IO.StreamWriter = New System.IO.StreamWriter(SaveFileDialog2.FileName, False)
                    Dim str As String = ""
                    Dim cindex As Integer = 0


                    filewriter.WriteLine("<%")
                    filewriter.WriteLine("")
                    filewriter.WriteLine("' In this demo, at least, we don't allow user to change directories...")
                    filewriter.WriteLine("' Change the DIRECTORY to point to any virtual directory of your choice.")
                    filewriter.WriteLine("CONST DIRECTORY = ""/Services/Daily Comic Strips/Images"" ' relative path in virtual directories")
                    filewriter.WriteLine("")
                    filewriter.WriteLine("' Specify one of these constants for ""sortBy""...")
                    filewriter.WriteLine("CONST FILE_NAME = 0")
                    filewriter.WriteLine("CONST ALTER_FILE_NAME = 1")
                    filewriter.WriteLine("CONST FILE_EXT = 2")
                    filewriter.WriteLine("CONST FILE_TYPE = 3")
                    filewriter.WriteLine("CONST FILE_SIZE = 4")
                    filewriter.WriteLine("CONST FILE_CREATED = 5")
                    filewriter.WriteLine("CONST FILE_MODIFIED = 6")
                    filewriter.WriteLine("CONST FILE_ACCESSED = 7")
                    filewriter.WriteLine("")
                    filewriter.WriteLine("' get requested sort order, if not first time here...")
                    filewriter.WriteLine("' (forward by name is default)")
                    filewriter.WriteLine("req = Request(""sortBy"")")
                    filewriter.WriteLine("If Len(req) < 1 Then")
                    filewriter.WriteLine("'sortBy = 1")
                    filewriter.WriteLine("sortBy = FILE_CREATED")
                    filewriter.WriteLine("priorSort = FILE_CREATED")
                    filewriter.WriteLine("Else")
                    filewriter.WriteLine("sortBy = CInt(req)")
                    filewriter.WriteLine("End if")
                    filewriter.WriteLine("req = Request(""priorSort"")")
                    filewriter.WriteLine("If Len(req) < 1 Then")
                    filewriter.WriteLine("sortBy = FILE_CREATED")
                    filewriter.WriteLine("priorSort = FILE_CREATED")
                    filewriter.WriteLine("'priorSort = -1")
                    filewriter.WriteLine("Else")
                    filewriter.WriteLine("priorSort = CInt(req)")
                    filewriter.WriteLine("End If")
                    filewriter.WriteLine("")
                    filewriter.WriteLine("")
                    filewriter.WriteLine("")
                    filewriter.WriteLine("'")
                    filewriter.WriteLine("' did user ask for same sort? to reverse the order?")
                    filewriter.WriteLine("' but if so, then zap priorSort so clicking again will do forward!")
                    filewriter.WriteLine("If sortBy = priorSort Then")
                    filewriter.WriteLine("reverse = true")
                    filewriter.WriteLine("priorSort = -1")
                    filewriter.WriteLine("Else")
                    filewriter.WriteLine("reverse = false")
                    filewriter.WriteLine("priorSort = sortBy")
                    filewriter.WriteLine("End If")
                    filewriter.WriteLine("")
                    filewriter.WriteLine("dim displaydate")
                    filewriter.WriteLine("displaydate = request.querystring(""displaydate"")")
                    filewriter.WriteLine("yy = Year(Now())")
                    filewriter.WriteLine("MM = Month(Now())")
                    filewriter.WriteLine("while len(MM) < 2")
                    filewriter.WriteLine("MM = ""0"" & MM")
                    filewriter.WriteLine("wend")
                    filewriter.WriteLine("dd = Day(Now())")
                    filewriter.WriteLine("while len(dd) < 2")
                    filewriter.WriteLine("dd = ""0"" & dd")
                    filewriter.WriteLine("wend")
                    filewriter.WriteLine("")
                    filewriter.WriteLine("")
                    filewriter.WriteLine("if displaydate = """" or not len(displaydate) = 8 then")
                    filewriter.WriteLine("displaydate = yy & MM & dd")
                    filewriter.WriteLine("end if")
                    filewriter.WriteLine("")
                    filewriter.WriteLine("dim testdisplaydate")
                    filewriter.WriteLine("testdisplaydate = CDate(left(displaydate,4) & ""/"" & mid(displaydate,5,2) & ""/"" & right(displaydate,2))")
                    filewriter.WriteLine("'response.write isdate(testdisplaydate)")
                    filewriter.WriteLine("'response.write formatdatetime(testdisplaydate,1)")
                    filewriter.WriteLine("%>")
                    filewriter.WriteLine("<html>")
                    filewriter.WriteLine("<head><title>Daily Comic Strips</title>")
                    filewriter.WriteLine("<link rel=""stylesheet"" type=""text/css"" href=""Stylesheet/Stylesheet.css"">")
                    filewriter.WriteLine("</head>")
                    filewriter.WriteLine("<body topmargin=""0"" leftmargin=""0"" rightmargin=""0"" bottommargin=""0"" marginwidth=""0"" marginheight=""0"">")
                    filewriter.WriteLine("<center>")
                    filewriter.WriteLine("<table border=""0"" width=""85%"" bgcolor=""#FFFFFF"" cellpadding=""20"" height=""100%""><tr><td bgcolor=""#FFFFFF"" valign=""top"" align=""center"" >")
                    filewriter.WriteLine("<table cellspacing=""15"" border=""0"" width=""100%"">")
                    filewriter.WriteLine("<tr><td valign=""top"" align=""left"">")
                    filewriter.WriteLine("<a name=""top""></a><h1>Daily Comic Strips</h1>")
                    filewriter.WriteLine("<%")
                    filewriter.WriteLine("")
                    filewriter.WriteLine("dim yesterday, yesterdaystring, tomorrow, tomorrowstring")
                    filewriter.WriteLine("yesterday = DateAdd(""d"",-1,testdisplaydate )")
                    filewriter.WriteLine("yy = Year(yesterday)")
                    filewriter.WriteLine("MM = Month(yesterday)")
                    filewriter.WriteLine("while len(MM) < 2")
                    filewriter.WriteLine("MM = ""0"" & MM")
                    filewriter.WriteLine("wend")
                    filewriter.WriteLine("dd = Day(yesterday)")
                    filewriter.WriteLine("while len(dd) < 2")
                    filewriter.WriteLine("dd = ""0"" & dd")
                    filewriter.WriteLine("wend")
                    filewriter.WriteLine("'response.write ""<h3>Yesterday's Comics: "" & yy & ""/"" & MM & ""/"" & dd  & ""</h3>""")
                    filewriter.WriteLine("yesterdaystring = ""<a href=default.asp?displaydate="" & yy & MM & dd & ""><font size=2>&lt;&lt; Yesterday's strips ("" & yy & ""/"" & MM & ""/"" & dd & "")</font></a>""")
                    filewriter.WriteLine("tomorrow = DateAdd(""d"",1,testdisplaydate )")
                    filewriter.WriteLine("yy = Year(tomorrow)")
                    filewriter.WriteLine("MM = Month(tomorrow)")
                    filewriter.WriteLine("while len(MM) < 2")
                    filewriter.WriteLine("MM = ""0"" & MM")
                    filewriter.WriteLine("wend")
                    filewriter.WriteLine("dd = Day(tomorrow)")
                    filewriter.WriteLine("while len(dd) < 2")
                    filewriter.WriteLine("dd = ""0"" & dd")
                    filewriter.WriteLine("wend")
                    filewriter.WriteLine("'response.write ""<h3>Tomorrow's Comics: "" & yy & ""/"" & MM & ""/"" & dd  & ""</h3>""")
                    filewriter.WriteLine("tomorrowstring = ""<a href=default.asp?displaydate="" & yy & MM & dd & ""><font size=2>Tomorrow's strips ("" & yy & ""/"" & MM & ""/"" & dd & "") &gt;&gt;</font></a>""")
                    filewriter.WriteLine("%>")
                    filewriter.WriteLine("")
                    filewriter.WriteLine("<table border=""0"" width=""100%""><tr><td align=""left"" valign=""middle""><% response.write yesterdaystring %></td><td align=""center"" valign=""middle""><% response.write ""<p><font size=2><b>Today's Comics ("" & left(displaydate,4) & ""/"" & mid(displaydate,5,2) & ""/"" & right(displaydate,2)  & "")</b></font></p>""%></td><td align=""right"" valign=""middle""><% response.write tomorrowstring %></td></tr></table>")
                    filewriter.WriteLine("</td></tr>")
                    filewriter.WriteLine("</table>")
                    filewriter.WriteLine("")
                    filewriter.WriteLine("")
                    filewriter.WriteLine("<%")
                    filewriter.WriteLine("")
                    filewriter.WriteLine("")
                    filewriter.WriteLine("' now start the *real* code...")
                    filewriter.WriteLine("'")
                    filewriter.WriteLine("path = Server.MapPath( DIRECTORY )")
                    filewriter.WriteLine("")
                    filewriter.WriteLine("Set fso = CreateObject(""Scripting.FileSystemObject"")")
                    filewriter.WriteLine("Set theCurrentFolder = fso.GetFolder( path )")
                    filewriter.WriteLine("Set curFiles = theCurrentFolder.Files")
                    filewriter.WriteLine("'")
                    filewriter.WriteLine("' And now a loop for the files")
                    filewriter.WriteLine("'")
                    filewriter.WriteLine("Dim theFiles( )")
                    filewriter.WriteLine("ReDim theFiles( 500 ) ' arbitrary size!")
                    filewriter.WriteLine("currentSlot = -1 ' start before first slot")
                    filewriter.WriteLine("")
                    filewriter.WriteLine("' We collect all the info about each file and put it into one")
                    filewriter.WriteLine("' ""slot"" in our ""theFiles"" array.")
                    filewriter.WriteLine("'")
                    filewriter.WriteLine("For Each fileItem in curFiles")
                    filewriter.WriteLine("fname = fileItem.Name")
                    filewriter.WriteLine("alteredfname = left(fileItem.Name,3) & fileItem.DateCreated & right(fileItem.Name,(len(fileItem.Name)-3))")
                    filewriter.WriteLine("fext = InStrRev( fname, ""."" )")
                    filewriter.WriteLine("If fext < 1 Then fext = """" Else fext = Mid(fname,fext+1)")
                    filewriter.WriteLine("ftype = fileItem.Type")
                    filewriter.WriteLine("fsize = fileItem.Size")
                    filewriter.WriteLine("fcreate = fileItem.DateCreated")
                    filewriter.WriteLine("fmod = fileItem.DateLastModified")
                    filewriter.WriteLine("faccess = fileItem.DateLastAccessed")
                    filewriter.WriteLine("")
                    filewriter.WriteLine("yy = Year(fileItem.DateCreated)")
                    filewriter.WriteLine("MM = Month(fileItem.DateCreated)")
                    filewriter.WriteLine("while len(MM) < 2")
                    filewriter.WriteLine("MM = ""0"" & MM")
                    filewriter.WriteLine("wend")
                    filewriter.WriteLine("dd = Day(fileItem.DateCreated)")
                    filewriter.WriteLine("while len(dd) < 2")
                    filewriter.WriteLine("dd = ""0"" & dd")
                    filewriter.WriteLine("wend")
                    filewriter.WriteLine("")
                    filewriter.WriteLine("'response.write ""<h2>"" & displaydate & "" -- "" &  yy & MM & dd & ""</h2>""")
                    filewriter.WriteLine("if displaydate = yy & MM & dd then")
                    filewriter.WriteLine("")
                    filewriter.WriteLine("currentSlot = currentSlot + 1")
                    filewriter.WriteLine("If currentSlot > UBound( theFiles ) Then")
                    filewriter.WriteLine("ReDim Preserve theFiles( currentSlot + 99 )")
                    filewriter.WriteLine("End If")
                    filewriter.WriteLine("' note that what we put here is an array!")
                    filewriter.WriteLine("theFiles(currentSlot) = Array(fname,alteredfname,fext,ftype,fsize,fcreate,fmod,faccess)")
                    filewriter.WriteLine("end if")
                    filewriter.WriteLine("Next")
                    filewriter.WriteLine("")
                    filewriter.WriteLine("fileCount = currentSlot")
                    filewriter.WriteLine("if not currentSlot = -1 then")
                    filewriter.WriteLine("")
                    filewriter.WriteLine("")
                    filewriter.WriteLine("")
                    filewriter.WriteLine("'")
                    filewriter.WriteLine("' files are now in the array...")
                    filewriter.WriteLine("'")
                    filewriter.WriteLine("' As noted, it is actually an ARRAY *OF* ARRAYS. Which makes")
                    filewriter.WriteLine("' picking the column we will sort on easier!")
                    filewriter.WriteLine("'")
                    filewriter.WriteLine("' ...size and sort it...")
                    filewriter.WriteLine("fileCount = currentSlot ' actually, count is 1 more, since we start at 0")
                    filewriter.WriteLine("ReDim Preserve theFiles( currentSlot ) ' really not necessary...just neater!")
                    filewriter.WriteLine("")
                    filewriter.WriteLine("' First, determine which ""kind"" of sort we are doing.")
                    filewriter.WriteLine("' (VarType=8 means ""string"")")
                    filewriter.WriteLine("'")
                    filewriter.WriteLine("If VarType( theFiles( 0 )( sortBy ) ) = 8 Then")
                    filewriter.WriteLine("If reverse Then kind = 1 Else kind = 2 ' sorting strings...")
                    filewriter.WriteLine("Else")
                    filewriter.WriteLine("If reverse Then kind = 3 Else kind = 4 ' non-strings (numbers, dates)")
                    filewriter.WriteLine("End If")
                    filewriter.WriteLine("")
                    filewriter.WriteLine("'")
                    filewriter.WriteLine("' A simple bubble sort for now...easier to follow the code...")
                    filewriter.WriteLine("'")
                    filewriter.WriteLine("For i = fileCount TO 0 Step -1")
                    filewriter.WriteLine("minmax = theFiles( 0 )( sortBy )")
                    filewriter.WriteLine("minmaxSlot = 0")
                    filewriter.WriteLine("For j = 1 To i")
                    filewriter.WriteLine("Select Case kind ' which kind of sort are we doing?")
                    filewriter.WriteLine("' after the ""is bigger/smaller"" test (as appropriate),")
                    filewriter.WriteLine("' mark will be true if we need to ""remember"" this slot...")
                    filewriter.WriteLine("Case 1 ' string, reverse...we do case INsensitive!")
                    filewriter.WriteLine("mark = (strComp( theFiles(j)(sortBy), minmax, vbTextCompare ) < 0)")
                    filewriter.WriteLine("Case 2 ' string, forward...we do case INsensitive!")
                    filewriter.WriteLine("mark = (strComp( theFiles(j)(sortBy), minmax, vbTextCompare ) > 0)")
                    filewriter.WriteLine("Case 3 ' non-string, reverse ...")
                    filewriter.WriteLine("mark = (theFiles( j )( sortBy ) < minmax)")
                    filewriter.WriteLine("Case 4 ' non-string, forward ...")
                    filewriter.WriteLine("mark = (theFiles( j )( sortBy ) > minmax)")
                    filewriter.WriteLine("End Select")
                    filewriter.WriteLine("' so is the current slot bigger/smaller than the remembered one?")
                    filewriter.WriteLine("If mark Then")
                    filewriter.WriteLine("' yep, so remember this one instead!")
                    filewriter.WriteLine("minmax = theFiles( j )( sortBy )")
                    filewriter.WriteLine("minmaxSlot = j")
                    filewriter.WriteLine("End If")
                    filewriter.WriteLine("Next")
                    filewriter.WriteLine("' is the last slot the min (or max), as it should be?")
                    filewriter.WriteLine("If minmaxSlot <> i Then")
                    filewriter.WriteLine("' nope...so do the needed swap...")
                    filewriter.WriteLine("temp = theFiles( minmaxSlot )")
                    filewriter.WriteLine("theFiles( minmaxSlot ) = theFiles( i )")
                    filewriter.WriteLine("theFiles( i ) = temp")
                    filewriter.WriteLine("End If")
                    filewriter.WriteLine("Next")
                    filewriter.WriteLine("' Ta-da! The array is sorted!")
                    filewriter.WriteLine("'")
                    filewriter.WriteLine("end if")
                    filewriter.WriteLine("%>")
                    filewriter.WriteLine("<FORM Name=""doSort"" Method=""Get"">")
                    filewriter.WriteLine("<INPUT Type=Hidden Name=priorSort Value=""<% = priorSort %>"">")
                    filewriter.WriteLine("<INPUT Type=Hidden Name=sortBy Value=""-1"">")
                    filewriter.WriteLine("</FORM>")
                    filewriter.WriteLine("")
                    filewriter.WriteLine("<SCRIPT Language=""JavaScript"">")
                    filewriter.WriteLine("function reSort( which )")
                    filewriter.WriteLine("{")
                    filewriter.WriteLine("document.doSort.sortBy.value = which;")
                    filewriter.WriteLine("document.doSort.submit( );")
                    filewriter.WriteLine("}")
                    filewriter.WriteLine("</SCRIPT>")
                    filewriter.WriteLine("")
                    filewriter.WriteLine("")
                    filewriter.WriteLine("<p><font size=2>")
                    filewriter.WriteLine("Comic Strips Located: <% = (fileCount+1) %>")
                    filewriter.WriteLine("</font></p>")
                    filewriter.WriteLine("<%")
                    filewriter.WriteLine("if fileCount = -1 then")
                    filewriter.WriteLine("response.write vbcrlf & ""<p><font size=2>Sorry, but no comic strips were downloaded for this day</font></p>"" & vbcrlf")
                    filewriter.WriteLine("else")
                    filewriter.WriteLine("%>")
                    filewriter.WriteLine("<CENTER>")
                    filewriter.WriteLine("<P>")
                    filewriter.WriteLine("Click on a column heading to sort by that column. Click the same column")
                    filewriter.WriteLine("again to reverse the sort.")
                    filewriter.WriteLine("<P>")
                    filewriter.WriteLine("<TABLE Border=0 CellPadding=8 width=""100%"">")
                    filewriter.WriteLine("<TR>")
                    filewriter.WriteLine("<TH><A HREF=""javascript:reSort(1);"">Adjusted File name</A></TH>")
                    filewriter.WriteLine("<TH><A HREF=""javascript:reSort(0);"">True File name</A></TH>")
                    filewriter.WriteLine("<TH><A HREF=""javascript:reSort(2);"">Extension</A></TH>")
                    filewriter.WriteLine("<TH><A HREF=""javascript:reSort(3);"">Type</A></TH>")
                    filewriter.WriteLine("<TH><A HREF=""javascript:reSort(4);"">Size</A></TH>")
                    filewriter.WriteLine("<TH><A HREF=""javascript:reSort(5);"">Created</A></TH>")
                    filewriter.WriteLine("<TH><A HREF=""javascript:reSort(6);"">Last modified</A></TH>")
                    filewriter.WriteLine("<TH><A HREF=""javascript:reSort(7);"">Last accessed</A></TH>")
                    filewriter.WriteLine("</TR>")
                    filewriter.WriteLine("<%")
                    filewriter.WriteLine("Dim lastwritten")
                    filewriter.WriteLine("lastwritten = """"")
                    filewriter.WriteLine("")
                    filewriter.WriteLine("if not currentSlot = -1 then")
                    filewriter.WriteLine("")
                    filewriter.WriteLine("' With the array nicely sorted, this part is a piece of cake!")
                    filewriter.WriteLine("For i = 0 To fileCount")
                    filewriter.WriteLine("")
                    filewriter.WriteLine("if not lcase(lastwritten) = lcase(left(theFiles(i)(0), 3)) then")
                    filewriter.WriteLine("lastwritten = lcase(left(theFiles(i)(0), 3))")
                    filewriter.WriteLine("response.write ""<tr><td colspan=""""7""""><h2>"" & vbcrlf")

                    filewriter.WriteLine("Select Case lastwritten")
                    For Each str In DownloadPath.Items
                        filewriter.WriteLine("Case """ & DownloadPrefix.Items.Item(cindex) & """")
                        filewriter.WriteLine("Response.Write(""<a name=""""" & DownloadComic.Items.Item(cindex) & """""></a>" & DownloadComic.Items.Item(cindex) & """)")
                        cindex = cindex + 1
                    Next
                    filewriter.WriteLine("Case Else")
                    filewriter.WriteLine("Response.Write(""<a name=""""Unknown Comic""""></a>Unknown Comic"")")
                    filewriter.WriteLine("End Select")

                    filewriter.WriteLine("")
                    filewriter.WriteLine("response.write ""</h2></td><td valign=""""top"""" align=""""right""""><a href=""""default.asp?priorSort=-1&sortBy="" & sortBy & ""#top"""">Top</a></td></tr>"" & vbcrlf")
                    filewriter.WriteLine("")
                    filewriter.WriteLine("end if")
                    filewriter.WriteLine("")
                    filewriter.WriteLine("")
                    filewriter.WriteLine("Response.Write ""<TR>"" & vbNewLine")
                    filewriter.WriteLine("Response.Write ""    <TD colspan=""""8""""><img src="""""" & DIRECTORY & ""/"" & theFiles(i)(0) & """""">""")
                    filewriter.WriteLine("Response.Write ""<br>"" & vbNewLine")
                    filewriter.WriteLine("response.write ""<table><tr>""")
                    filewriter.WriteLine("For j = 0 To UBound( theFiles(i) )")
                    filewriter.WriteLine("Response.Write ""<td>"" & theFiles(i)(j) & ""</td>"" & vbNewLine")
                    filewriter.WriteLine("Next")
                    filewriter.WriteLine("response.write ""</tr></table>""")
                    filewriter.WriteLine("")
                    filewriter.WriteLine("Next")
                    filewriter.WriteLine("")
                    filewriter.WriteLine("end if")
                    filewriter.WriteLine("end if")
                    filewriter.WriteLine("%>")
                    filewriter.WriteLine("</TABLE>")
                    filewriter.WriteLine("")
                    filewriter.WriteLine("</BODY>")
                    filewriter.WriteLine("</HTML>")



                   
                 
                    filewriter.Flush()
                    filewriter.Close()
                    filewriter = Nothing
                    Status("Web Page Successfully Generated")

                    If File_Exists(SaveFileDialog2.FileName) = True Then
                        Dim systemDirectory As String
                        systemDirectory = System.Environment.SystemDirectory
                        Dim finfo As FileInfo = New FileInfo((systemDirectory & "\notepad.exe").Replace("\\", "\"))
                        If finfo.Exists = True Then
                            Dim apptorun As String
                            apptorun = """" & (systemDirectory & "\notepad.exe").Replace("\\", "\") & """ """ & SaveFileDialog2.FileName & """"
                            Dim procID As Integer = Shell(apptorun, AppWinStyle.NormalFocus, False)
                        End If
                        finfo = Nothing
                    End If
                End If
            End If
        Catch ex As Exception
            Error_Handler(ex, "Generate Web Page")
        End Try
        Timer1.Start()
        downloadcycle.ForeColor = Color.Green
    End Sub

    Private Sub AboutToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AboutToolStripMenuItem1.Click
        Try
            Me.ToolStripStatusLabel1.Text = "About displayed"
            AboutBox1.ShowDialog()
        Catch ex As Exception
            Error_Handler(ex, "Display About Screen")
        End Try
    End Sub

    Private Sub AutoUpdateToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AutoUpdateToolStripMenuItem.Click
        Try
            AutoUpdate = True
            Me.Close()
        Catch ex As Exception
            Error_Handler(ex, "AutoUpdate")
        End Try
    End Sub

    Private Sub SendNotificationEmail(ByVal StartOrClose As String)
        Try
            If SendMail_CheckBox.Checked = True Then
                Dim obj As SmtpClient
                If mailserver1port.Length > 0 Then
                    obj = New SmtpClient(mailserver1, mailserver1port)
                Else
                    obj = New SmtpClient(mailserver1)
                End If

                Dim msg As System.Net.Mail.MailMessage = New System.Net.Mail.MailMessage

                If StartOrClose = "Startup" Then
                    msg.Subject = My.Application.Info.ProductName & ": Application Startup"
                    Status("Sending Startup Notification")
                Else
                    msg.Subject = My.Application.Info.ProductName & ": Application Shutdown"
                    Status("Sending Shutdown Notification")
                End If

                Dim fromaddress As MailAddress = New MailAddress(webmasteraddress, webmasterdisplay)
                msg.From = fromaddress
                msg.ReplyTo = fromaddress
                msg.To.Add(fromaddress)

                msg.IsBodyHtml = False

                Dim body As String
                If StartOrClose = "Startup" Then
                    body = "This is just a notification message to inform you that " & My.Application.Info.ProductName & " has been successfully started up."
                Else
                    body = "This is just a notification message to inform you that " & My.Application.Info.ProductName & " has been shutdown."
                End If

                body = body & vbCrLf & vbCrLf & "******************************" & vbCrLf & vbCrLf & "This is an auto-generated email submitted from " & My.Application.Info.ProductName & " at " & Format(Now(), "dd/MM/yyyy hh:mm:ss tt") & ", running on:"
                body = body & vbCrLf & vbCrLf & "Machine Name: " + Environment.MachineName
                body = body & vbCrLf & "OS Version: " & Environment.OSVersion.ToString()
                body = body & vbCrLf & "User Name: " + Environment.UserName
                msg.Body = body

                obj.DeliveryMethod = SmtpDeliveryMethod.Network
                obj.EnableSsl = False
                obj.UseDefaultCredentials = True


                obj.Send(msg)
                obj = Nothing
                Status("Notification Email Sent")
            End If
        Catch ex As Exception
            Error_Handler(ex, "Send Startup/Shutdown Email")
            Status("Error Encountered in Sending Notification Email")
        End Try
    End Sub

    Private Sub Send_Report(ByVal dt As Date, ByVal FileNamePrefix As String)
        '*********************
        'Send Mail Out - activity report
        Try
            If SendMail_CheckBox.Checked = True Then
                Status("Sending Daily Activity Report")
                Dim obj As SmtpClient
                If mailserver1port.Length > 0 Then
                    obj = New SmtpClient(mailserver1, mailserver1port)
                Else
                    obj = New SmtpClient(mailserver1)
                End If

                Dim msg As System.Net.Mail.MailMessage = New System.Net.Mail.MailMessage

                msg.Subject = My.Application.Info.ProductName & " Daily Report"
                Dim fromaddress As MailAddress = New MailAddress(webmasteraddress, webmasterdisplay)
                msg.From = fromaddress
                msg.ReplyTo = fromaddress
                msg.To.Add(fromaddress)

                msg.IsBodyHtml = False

                obj.DeliveryMethod = SmtpDeliveryMethod.Network
                obj.EnableSsl = False
                obj.UseDefaultCredentials = True

                If My.Computer.FileSystem.FileExists((Application.StartupPath & "\").Replace("\\", "\") & "Activity Logs\" & FileNamePrefix & "_Activity_Log.txt") = True Then
                    Dim att As Attachment = New Attachment((Application.StartupPath & "\").Replace("\\", "\") & "Activity Logs\" & FileNamePrefix & "_Activity_Log.txt")
                    msg.Attachments.Add(att)
                End If
                If My.Computer.FileSystem.FileExists((Application.StartupPath & "\").Replace("\\", "\") & "Error Logs\" & FileNamePrefix & "_Error_Log.txt") = True Then
                    Dim att2 As Attachment = New Attachment((Application.StartupPath & "\").Replace("\\", "\") & "Error Logs\" & FileNamePrefix & "_Error_Log.txt")
                    msg.Attachments.Add(att2)
                End If

                msg.Body = "This daily activity report from " & My.Application.Info.ProductName & " running on " & Environment.MachineName & " was generated at " & Format(dt, "dd/MM/yyyy HH:mm:ss") & "." & vbCrLf & vbCrLf & "The activity reports for today are attached. Currently the total number of comic strips downloaded for the day stands at " & todaydownloads & ", while the number of strip downloads since this session startup is " & filesdownloaded.Text & "."
                msg.Body = msg.Body & vbCrLf & vbCrLf & "******************************" & vbCrLf & vbCrLf & "This is an auto-generated email submitted from " & My.Application.Info.ProductName & " at " & Format(Now(), "dd/MM/yyyy hh:mm:ss tt") & ", running on:"
                msg.Body = msg.Body & vbCrLf & vbCrLf & "Machine Name: " + Environment.MachineName
                msg.Body = msg.Body & vbCrLf & "OS Version: " & Environment.OSVersion.ToString()
                msg.Body = msg.Body & vbCrLf & "User Name: " + Environment.UserName

                todaydownloads = 0
                obj.Send(msg)
                obj = Nothing
                'msg = Nothing
                Status("Daily Activity Report Sent")
            End If
        Catch ex As Exception
            Error_Handler(ex, "Send Report")
            Status("Failed to Send Daily Activity Report")
        End Try
    End Sub



    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        Try
            'sorts listboxes on URL
            Dim sortedlist As ArrayList = New ArrayList
            Dim counter As Integer = 0
            Dim pad As String = ""
            For Each item As String In DownloadPath.Items
                If counter < 10 Then
                    pad = "0"
                Else
                    pad = ""
                End If
                sortedlist.Add(item & pad & counter)
                counter = counter + 1
            Next
            sortedlist.Sort()
            DownloadPath.Items.Clear()
            Dim list1 As ArrayList = New ArrayList
            Dim list2 As ArrayList = New ArrayList
            Dim list3 As ArrayList = New ArrayList
            Dim index As Integer
            For Each item As String In sortedlist
                DownloadPath.Items.Add(item.Remove(item.Length - 2, 2))
                index = Integer.Parse(item.Substring(item.Length - 2, 2))
                list1.Add(DownloadComic.Items.Item(index))
                list2.Add(DownloadImage.Items.Item(index))
                list3.Add(DownloadPrefix.Items.Item(index))
            Next

            DownloadComic.Items.Clear()
            For Each item As String In list1
                DownloadComic.Items.Add(item)
            Next
            DownloadImage.Items.Clear()
            For Each item As String In list2
                DownloadImage.Items.Add(item)
            Next
            DownloadPrefix.Items.Clear()
            For Each item As String In list3
                DownloadPrefix.Items.Add(item)
            Next

            sortedlist.Clear()
            sortedlist = Nothing
            list1.Clear()
            list1 = Nothing
            list2.Clear()
            list2 = Nothing
            list3.Clear()
            list3 = Nothing

            'DownloadPath
            'DownloadComic
            'DownloadImage
            'DownloadPrefix

        Catch ex As Exception
            Error_Handler(ex, "Sort on URL")
        End Try
    End Sub

    
    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        Try
            'sorts listboxes on URL
            Dim sortedlist As ArrayList = New ArrayList
            Dim counter As Integer = 0
            Dim pad As String = ""
            For Each item As String In DownloadComic.Items
                If counter < 10 Then
                    pad = "0"
                Else
                    pad = ""
                End If
                sortedlist.Add(item & pad & counter)
                counter = counter + 1
            Next
            sortedlist.Sort()
            DownloadComic.Items.Clear()
            Dim list1 As ArrayList = New ArrayList
            Dim list2 As ArrayList = New ArrayList
            Dim list3 As ArrayList = New ArrayList
            Dim index As Integer
            For Each item As String In sortedlist
                DownloadComic.Items.Add(item.Remove(item.Length - 2, 2))
                index = Integer.Parse(item.Substring(item.Length - 2, 2))
                list1.Add(DownloadPath.Items.Item(index))
                list2.Add(DownloadImage.Items.Item(index))
                list3.Add(DownloadPrefix.Items.Item(index))
            Next

            DownloadPath.Items.Clear()
            For Each item As String In list1
                DownloadPath.Items.Add(item)
            Next
            DownloadImage.Items.Clear()
            For Each item As String In list2
                DownloadImage.Items.Add(item)
            Next
            DownloadPrefix.Items.Clear()
            For Each item As String In list3
                DownloadPrefix.Items.Add(item)
            Next

            sortedlist.Clear()
            sortedlist = Nothing
            list1.Clear()
            list1 = Nothing
            list2.Clear()
            list2 = Nothing
            list3.Clear()
            list3 = Nothing

            'DownloadPath
            'DownloadComic
            'DownloadImage
            'DownloadPrefix

        Catch ex As Exception
            Error_Handler(ex, "Sort on Comic")
        End Try
    End Sub

    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
        Try
            'sorts listboxes on URL
            Dim sortedlist As ArrayList = New ArrayList
            Dim counter As Integer = 0
            Dim pad As String = ""
            For Each item As String In DownloadImage.Items
                If counter < 10 Then
                    pad = "0"
                Else
                    pad = ""
                End If
                sortedlist.Add(item & pad & counter)
                counter = counter + 1
            Next
            sortedlist.Sort()
            DownloadImage.Items.Clear()
            Dim list1 As ArrayList = New ArrayList
            Dim list2 As ArrayList = New ArrayList
            Dim list3 As ArrayList = New ArrayList
            Dim index As Integer
            For Each item As String In sortedlist
                DownloadImage.Items.Add(item.Remove(item.Length - 2, 2))
                index = Integer.Parse(item.Substring(item.Length - 2, 2))
                list1.Add(DownloadPath.Items.Item(index))
                list2.Add(DownloadComic.Items.Item(index))
                list3.Add(DownloadPrefix.Items.Item(index))
            Next

            DownloadPath.Items.Clear()
            For Each item As String In list1
                DownloadPath.Items.Add(item)
            Next
            DownloadComic.Items.Clear()
            For Each item As String In list2
                DownloadComic.Items.Add(item)
            Next
            DownloadPrefix.Items.Clear()
            For Each item As String In list3
                DownloadPrefix.Items.Add(item)
            Next

            sortedlist.Clear()
            sortedlist = Nothing
            list1.Clear()
            list1 = Nothing
            list2.Clear()
            list2 = Nothing
            list3.Clear()
            list3 = Nothing

            'DownloadPath
            'DownloadComic
            'DownloadImage
            'DownloadPrefix

        Catch ex As Exception
            Error_Handler(ex, "Sort on Image")
        End Try
    End Sub

    Private Sub Button10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button10.Click
        Try
            'sorts listboxes on URL
            Dim sortedlist As ArrayList = New ArrayList
            Dim counter As Integer = 0
            Dim pad As String = ""
            For Each item As String In DownloadPrefix.Items
                If counter < 10 Then
                    pad = "0"
                Else
                    pad = ""
                End If
                sortedlist.Add(item & pad & counter)
                counter = counter + 1
            Next
            sortedlist.Sort()
            DownloadPrefix.Items.Clear()
            Dim list1 As ArrayList = New ArrayList
            Dim list2 As ArrayList = New ArrayList
            Dim list3 As ArrayList = New ArrayList
            Dim index As Integer
            For Each item As String In sortedlist
                DownloadPrefix.Items.Add(item.Remove(item.Length - 2, 2))
                index = Integer.Parse(item.Substring(item.Length - 2, 2))
                list1.Add(DownloadPath.Items.Item(index))
                list2.Add(DownloadComic.Items.Item(index))
                list3.Add(DownloadImage.Items.Item(index))
            Next

            DownloadPath.Items.Clear()
            For Each item As String In list1
                DownloadPath.Items.Add(item)
            Next
            DownloadComic.Items.Clear()
            For Each item As String In list2
                DownloadComic.Items.Add(item)
            Next
            DownloadImage.Items.Clear()
            For Each item As String In list3
                DownloadImage.Items.Add(item)
            Next

            sortedlist.Clear()
            sortedlist = Nothing
            list1.Clear()
            list1 = Nothing
            list2.Clear()
            list2 = Nothing
            list3.Clear()
            list3 = Nothing

            'DownloadPath
            'DownloadComic
            'DownloadImage
            'DownloadPrefix

        Catch ex As Exception
            Error_Handler(ex, "Sort on Prefix")
        End Try
    End Sub
End Class
