Imports System.IO
Imports System.IO.Ports
Imports System.Threading
Imports System.Data.SqlClient
Public Class Form1
    Dim SiteID As String = ""
    Structure SerialDef
        Dim CycleSeconds As Integer
        Dim Comport As String
        Dim Baud As Integer
        Dim Parity As String
        Dim DataBits As Integer
        Dim Stopbits As Integer
    End Structure
    Dim RFComSet As SerialDef
    Dim RFPort As New SerialPort
    Structure tankdef
        Dim name As String
        Dim active As Boolean
        Dim timeout As Integer
    End Structure
    Dim tanks As New DataSet
    Dim tankindex As Integer
    Dim polltank As System.Threading.Timer
    Dim serverlog As System.Threading.Timer
    Dim datascan As System.Threading.Timer
    Dim serverlogurl As String ' the URL to post sensor samples to
    Dim serverlogcycle As Integer ' the number of minutes between server log posts
    Dim maxserverlogrecs As Integer ' the number of tank log records to include in each post
    Dim serverdataurl As String ' the URL to post data changes to
    Dim serverdatacycle As Integer ' the number of minutes between data scans

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        If Not System.Diagnostics.EventLog.SourceExists("FracSiteService") Then
            System.Diagnostics.EventLog.CreateEventSource("FracSiteService",
            "FracSiteLog")
        End If
        EventLog1.Source = "FracSiteService"

    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ' set up threading timers
        polltank = New System.Threading.Timer(AddressOf PollTank_Tick, Nothing, System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite)
        serverlog = New System.Threading.Timer(AddressOf ServerLog_Tick, Nothing, System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite)
        datascan = New System.Threading.Timer(AddressOf DataScan_Tick, Nothing, System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite)

        ' load settings
        LoadSettings()

        ' start the data scanning timer
        datascan.Change(0, serverdatacycle * 60 * 1000)
        EventLog1.WriteEntry("Datascan timer started.", EventLogEntryType.Information)

        ' check for tanks 
        If tanks.Tables.Count > 0 Then
            EventLog1.WriteEntry("Total Tanks:" & tanks.Tables(0).Rows.Count, EventLogEntryType.Information)
            If tanks.Tables(0).Rows.Count > 0 Then
                If ComSwitch(RFPort, RFComSet, "on") Then

                    EventLog1.WriteEntry("Polling Starting.", EventLogEntryType.Information)

                    '  start the timers tank data timers
                    polltank.Change(0, RFComSet.CycleSeconds * 1000)
                    serverlog.Change(0, serverlogcycle * 60 * 1000)
                Else
                    EventLog1.WriteEntry("Polling Aborted - no com port.", EventLogEntryType.Information)
                End If
            End If
        End If

    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        EventLog1.WriteEntry("Polling Stopping.", EventLogEntryType.Information)


        ' Stop the timers
        polltank.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite)
        polltank.Dispose()

        serverlog.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite)
        serverlog.Dispose()

        datascan.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite)
        datascan.Dispose()

        ' close com port
        ComSwitch(RFPort, RFComSet, "off")

        EventLog1.WriteEntry("App Stopping.", EventLogEntryType.Information)

    End Sub
    Private Sub LoadSettings()

        ' from text file
        EventLog1.WriteEntry("Opening settings file:" & My.Settings.SettingsFile, EventLogEntryType.Error)

        If System.IO.File.Exists(My.Settings.SettingsFile) = True Then

            ' txt file
            Dim objReader As New System.IO.StreamReader(My.Settings.SettingsFile)
            Dim line_elems As String()
            While Not objReader.EndOfStream
                line_elems = objReader.ReadLine.Split(New Char() {","c})
                Select Case line_elems(0)
                    Case "/" ' comment line
                        Continue While
                    Case "Z" 'Site ID
                        SiteID = line_elems(1)
                        EventLog1.WriteEntry("SiteID:" & SiteID, EventLogEntryType.Information)

                    Case "R" 'RF cycle time
                        RFComSet.CycleSeconds = CInt(line_elems(1))
                        RFComSet.Comport = line_elems(2)
                        RFComSet.Baud = line_elems(3)
                        RFComSet.Parity = line_elems(4)
                        RFComSet.DataBits = line_elems(5)
                        RFComSet.Stopbits = line_elems(6)
                        EventLog1.WriteEntry("RF Port:" & RFComSet.Comport, EventLogEntryType.Information)
                        EventLog1.WriteEntry("RF Cycle:" & RFComSet.CycleSeconds, EventLogEntryType.Information)

                    Case "S" ' sample log server
                        serverlogurl = line_elems(1) ' the url to post tankdata to
                        serverlogcycle = line_elems(2) ' the number of minutes between server log posts
                        maxserverlogrecs = line_elems(3) ' the number of tank log records to include in each post
                        EventLog1.WriteEntry("ServerURL(samples):" & serverlogurl, EventLogEntryType.Information)
                        EventLog1.WriteEntry("ServerCycle (mins):" & serverlogcycle, EventLogEntryType.Information)
                        EventLog1.WriteEntry("ServerMaxLogs:" & maxserverlogrecs, EventLogEntryType.Information)

                    Case "D" ' data update service
                        serverdataurl = line_elems(1) ' the url to post dataupdates to
                        serverdatacycle = line_elems(2) ' the number of minutes between data scans
                        EventLog1.WriteEntry("ServerURL(data):" & serverdataurl, EventLogEntryType.Information)
                        EventLog1.WriteEntry("ServerCycle (mins):" & serverdatacycle, EventLogEntryType.Information)

                    Case "C" 'connection string
                        My.Settings.ConnectString = line_elems(1)
                        EventLog1.WriteEntry("ConnectString:" & My.Settings.ConnectString, EventLogEntryType.Information)

                    Case Else 'ignore

                End Select
            End While
            objReader.Close()

            ' get tanks list from database
            Dim strSQL As String

            strSQL = "select tdefSensorID, tdefActive from vSiteTanks " & _
            "where stSiteID = " & SiteID & _
            "order by tdefSensorID"

            tanks = clsGeneral.QueryDB(strSQL)

        Else
            EventLog1.WriteEntry("Could not open settings file:" & My.Settings.SettingsFile, EventLogEntryType.Error)

        End If
    End Sub
    Private Function SelectNextTank() As Integer
        Try

            Do
                tankindex += 1

                If tankindex = tanks.Tables(0).Rows.Count Then
                    tankindex = 0
                End If

            Loop While clsGeneral.IsSensorActive(tanks.Tables(0).Rows(tankindex).Item("tdefSensorID")) = False 'skip inactive tanks

            Return tankindex

        Catch ex As Exception
            Return 0
        End Try
    End Function
    Private Sub PollTank_Tick(ByVal state As Object)
        ' this sub handles tank polling
        'EventLog1.WriteEntry("Poll Tick", EventLogEntryType.Information)

        ' poll the tank here
        Dim cmdstr As String = BuildTankCommand(SelectNextTank())

        'EventLog1.WriteEntry("Sending:" & cmdstr, EventLogEntryType.Information)

        RFPort.WriteLine(cmdstr)

        Try
            Dim resp As String = "no response"
            resp = RFPort.ReadLine()
            'EventLog1.WriteEntry("Received:" & resp, EventLogEntryType.Information)

            LogResults(resp)

            If resp = "no response" Then
                EventLog1.WriteEntry("Error - Sensor did not respond:" & cmdstr, EventLogEntryType.Information)
            End If

        Catch ex As TimeoutException
            EventLog1.WriteEntry("Error Unknown - Sensor did not respond:" & cmdstr, EventLogEntryType.Information)
        End Try

        System.Threading.Thread.Sleep(500)

    End Sub
    Private Function BuildTankCommand(ByVal index As Integer)
        '!01002*M65001~
        Dim cmdstr As String
        cmdstr = "!" + tanks.Tables(0).Rows(tankindex).Item("tdefSensorID") + "*" + "M" + SiteID + "~"
        Return cmdstr
    End Function
    Private Sub LogResults(ByVal respStr As String)
        '!65001*LI01002R274A1899W1899V4935~

        ' is this reponse for me?
        '   is this a log request
        '     get the tank ID
        '     get the range
        '     get the Amb temp
        '     get the water temp
        '     get the voltage
        '     replace any bad values

        '
        ' debug - remove me
        '
        'respStr = "!65001*LI01002R274A1899W1899V4935~"
        ' end debug

        Dim sitetest As String = "UNKNOWN"
        Dim sampletank As String = "UNKNOWN"
        Dim range As Integer = -1
        Dim atemp As Integer = -1
        Dim wtemp As Integer = -1
        Dim flow As Integer = -1
        Dim totalizer As Integer = -1
        Dim volts As Integer = -1

        Try

            ' target sitecode
            Dim codestart As Integer = 0
            Dim codeend As Integer = 0
            codestart = InStr(respStr, "!", CompareMethod.Text)
            codeend = InStr(respStr, "*", CompareMethod.Text)
            If (codestart > 0) And (codeend > 0) And (codeend > codestart) Then
                sitetest = respStr.Substring(codestart, codeend - codestart - 1)
            End If

            ' tankID
            codestart = 0
            codeend = 0
            codestart = InStr(respStr, "*L", )
            codeend = InStr(respStr, "R")
            If codeend = 0 Then
                codeend = InStr(respStr, "F")
            End If
            If (codestart > 0) And (codeend > 0) And (codeend > codestart) Then
                sampletank = respStr.Substring(codestart + 2, codeend - codestart - 3)
            End If

            ' Range
            codestart = 0
            codeend = 0
            codestart = InStr(respStr, "R", )
            codeend = InStr(respStr, "A")
            If (codestart > 0) And (codeend > 0) And (codeend > codestart) Then
                range = CInt(respStr.Substring(codestart, codeend - codestart - 1))
                ' Else
                ' sitetest = "0"
            End If

            ' Ambient Temp
            codestart = 0
            codeend = 0
            codestart = InStr(respStr, "A", )
            codeend = InStr(respStr, "W")
            If (codestart > 0) And (codeend > 0) And (codeend > codestart) Then
                If IsNumeric(respStr.Substring(codestart, codeend - codestart - 1)) Then
                    atemp = CInt(respStr.Substring(codestart, codeend - codestart - 1))
                End If
            End If

            ' Water Temp
            codestart = 0
            codeend = 0
            codestart = InStr(respStr, "W", )
            codeend = InStr(respStr, "V")
            If (codestart > 0) And (codeend > 0) And (codeend > codestart) Then
                If IsNumeric(respStr.Substring(codestart, codeend - codestart - 1)) Then
                    wtemp = CInt(respStr.Substring(codestart, codeend - codestart - 1))
                End If
            End If

            ' Flow
            codestart = 0
            codeend = 0
            codestart = InStr(respStr, "F", )
            codeend = InStr(respStr, "T")
            If (codestart > 0) And (codeend > 0) And (codeend > codestart) Then
                If IsNumeric(respStr.Substring(codestart, codeend - codestart - 1)) Then
                    flow = CInt(respStr.Substring(codestart, codeend - codestart - 1))
                End If
            End If

            ' Totalizer
            codestart = 0
            codeend = 0
            codestart = InStr(respStr, "T", )
            codeend = InStr(respStr, "V")
            If (codestart > 0) And (codeend > 0) And (codeend > codestart) Then
                If IsNumeric(respStr.Substring(codestart, codeend - codestart - 1)) Then
                    totalizer = CInt(respStr.Substring(codestart, codeend - codestart - 1))
                End If
            End If

            ' H2S Gas

            ' CI Gas

            ' volts
            codestart = 0
            codeend = 0
            codestart = InStr(respStr, "V", )
            codeend = InStr(respStr, "~")
            If (codestart > 0) And (codeend > 0) And (codeend > codestart) Then
                If IsNumeric(respStr.Substring(codestart, codeend - codestart - 1)) Then
                    volts = respStr.Substring(codestart, codeend - codestart - 1)
                End If
            End If

            If sitetest = SiteID And sampletank <> "UNKNOWN" Then

                ' write record 
                Dim qrystr As String = ""

                qrystr = "insert into tankData (" & _
                   " [tdSiteID] " & _
                   ",[tdSensorID] " & _
                   ",[tdDateTime] " & _
                   ",[tdTankResp] " & _
                   ",[tdProcessed] " & _
                   ",[tdRangeType] " & _
                   ",[tdRange] " & _
                   ",[tdATemp] " & _
                   ",[tdWTemp] " & _
                   ",[tdFlow] " & _
                   ",[tdTotalizer] " & _
                   ",[tdVolts] " & _
                   ") VALUES (" & _
                   "'" & sitetest & "' " & _
                   ",'" & sampletank & "' " & _
                   ",'" & Now() & "' " & _
                   ",'" & respStr & "' " & _
                   ", 'N'" & _
                   ", '" & "R" & "' " & _
                   ", " & range & "  " & _
                   ", " & atemp & "  " & _
                   ", " & wtemp & "  " & _
                   ", " & flow & "  " & _
                   ", " & totalizer & "  " & _
                   ", " & volts & "  " & _
                   ")"

                clsGeneral.ActionQuery(qrystr, True)
            End If


        Catch ex As Exception
            ' error

        End Try

    End Sub
    Private Function ComSwitch(ByRef port As SerialPort, ByVal comset As SerialDef, mode As String) As Boolean

        For Each machineport As String In SerialPort.GetPortNames
            If machineport = comset.Comport Then

                Select Case mode
                    Case "on"
                        port.PortName = comset.Comport
                        port.BaudRate = comset.Baud
                        port.Parity = Parity.None
                        port.Parity = CType([Enum].Parse(GetType(Parity), comset.Parity), Parity)
                        port.DataBits = comset.DataBits
                        port.StopBits = comset.Stopbits
                        port.ReadTimeout = 4000

                        port.Open()
                        If port.IsOpen() Then
                            Return True
                        Else
                            Return False
                        End If

                    Case "off"
                        port.Close()
                        port.Dispose()
                        Return True

                    Case Else

                End Select

            End If
        Next

        Return False

    End Function
    Private Sub ServerLog_Tick(ByVal state As Object)
        ' call the stored proc to build a temporary table of latest sensor readings, returns number of recs
        Dim totrecs As Integer = clsGeneral.PostLastLog()
        If totrecs > 0 Then
            ' get a dataset from the temporary table of latest sensor readings (tankdataTEMP)
            Dim strSQL As String
            Dim logrecs As DataSet
            strSQL = "select * from tankDataTEMP"
            logrecs = clsGeneral.QueryDB(strSQL)

            EventLog1.WriteEntry("Posting " & logrecs.Tables(0).Rows.Count.ToString & " latest sensor records to the server.", EventLogEntryType.Information)

            Dim recfragment As Integer = 0
            Dim rownumber As Integer = 0
            Dim logstring As String = ""

            For Each logrecs__row As DataRow In logrecs.Tables(0).Rows
                ' build the log string
                logstring = logstring + "L"
                logstring = logstring + "S" + logrecs__row.Item("tdSiteID") + ";"
                logstring = logstring + "T" + logrecs__row.Item("tdSensorID") + ";"
                logstring = logstring + logrecs__row.Item("tdRangeType") + logrecs__row.Item("tdRange").ToString + ";"
                logstring = logstring + "W" + logrecs__row.Item("tdWTemp").ToString + ";"
                logstring = logstring + "A" + logrecs__row.Item("tdATemp").ToString + ";"
                logstring = logstring + "Z" + logrecs__row.Item("tdDateTime") + ";"
                logstring = logstring + "V" + logrecs__row.Item("tdVolts").ToString + ";"
                logstring = logstring + "P" + "unknown,unknown"

                recfragment += 1
                rownumber += 1

                If (recfragment = maxserverlogrecs) Or (rownumber = logrecs.Tables(0).Rows.Count) Then
                    'post and clear the querystring
                    ' post to the server for each rec, up to (maxserverlogrecs) at a time
                    ' format to post
                    ' LS65001;T01001;R65;W87;A87;Z7/25/2014%208:43:48%20AM;V0;P35.227087,-80.843127

                    ' post it to the service
                    Try

                        Dim LoggingService As New localloggingservice.Service1
                        LoggingService.Url = serverlogurl
                        If LoggingService.writetanklog(logstring) Then
                            EventLog1.WriteEntry("Posting " & recfragment & " recs to " & LoggingService.Url & " succeeded.", EventLogEntryType.Information)
                        Else
                            ' error
                            EventLog1.WriteEntry("Posting " & recfragment & " recs to " & LoggingService.Url & " did not succeed.", EventLogEntryType.Information)
                        End If

                    Catch ex As Exception
                        ' error
                        EventLog1.WriteEntry("Posting failed with: " & Err.Description, EventLogEntryType.Information)
                    End Try

                    ' clear it
                    logstring = ""
                    recfragment = 0
                    rownumber = 0
                End If

            Next
        Else
            EventLog1.WriteEntry("No new tank data to post to the server.", EventLogEntryType.Information)
        End If

    End Sub
    Private Sub DataScan_Tick(ByVal state As Object)
        EventLog1.WriteEntry("Scanning for data changes.")

        EventLog1.WriteEntry(FindRecs())

    End Sub
    Public Function FindRecs() As String
        Dim reccount As Integer = 0

        Dim uds, uxds As New DataSet
        Dim usql, uxsql, updsql As String

        ' get all webuser table changed rows
        usql = "select usrGUID from WebUser where usrModFlag = 'U'"
        uds = clsGeneral.QueryDB(usql)
        If uds.Tables.Count > 0 And uds.Tables(0).Rows.Count > 0 Then
            For Each udatarow In uds.Tables(0).Rows

                ' create an xml string for the record
                uxsql = "select " & _
                "  WebUser.* " & _
                " ,Address.* " & _
                " from [webuser] " & _
                " join [Relation] " & _
                " on WebUser.usrID = Relation.relID1 and Relation.relType = 'UA' " & _
                " join [address] on Address.adID = Relation.relID2  " & _
                " where usrGUID = '" & udatarow.item("usrGUID") & "' " & _
                " FOR XML auto, elements "

                uxds = clsGeneral.QueryDB(uxsql)
                If uxds.Tables.Count > 0 And uxds.Tables(0).Rows.Count > 0 Then
                    If ChangeRec(uxds.Tables(0).Rows(0).Item(0).ToString, "WebUser", udatarow.item("usrGUID")) > 0 Then
                        reccount += 1

                        ' reset the tdefModFlag of the source record
                        updsql = "update WebUser set usrModFlag = '' where usrGUID = '" & udatarow.item("usrGUID") & "'"
                        clsGeneral.ActionQuery(updsql)
                    End If
                End If

                uxds = Nothing

            Next
        End If

        Dim tds, txds As New DataSet
        Dim tsql, txsql As String

        ' get all tankdef table changed rows
        tsql = "select tdefGUID from tankdef where tdefModFlag = 'U'"
        tds = clsGeneral.QueryDB(tsql)
        If tds.Tables.Count > 0 And tds.Tables(0).Rows.Count > 0 Then
            For Each tdatarow In tds.Tables(0).Rows

                ' create an xml string for the record
                txsql = "select " & _
                " * " & _
                " from [TankDef] " & _
                " where tdefGUID = '" & tdatarow.item("tdefGUID") & "' " & _
                " FOR XML auto, elements "

                txds = clsGeneral.QueryDB(txsql)
                If txds.Tables.Count > 0 And txds.Tables(0).Rows.Count > 0 Then
                    If ChangeRec(txds.Tables(0).Rows(0).Item(0).ToString, "TankDef", tdatarow.item("tdefGUID")) > 0 Then
                        reccount += 1

                        ' reset the tdefModFlag of the source record
                        updsql = "update tankdef set tdefmodflag = '' where tdefGUID = '" & tdatarow.item("tdefGUID") & "'"
                        clsGeneral.ActionQuery(updsql)

                    End If
                End If

                txds = Nothing

            Next
        End If

        Dim sds, sxds As New DataSet
        Dim ssql, sxsql As String

        ' get all sensorlimits changed rows
        ssql = "select slGUID from sensorlimits where slModFlag = 'U'"
        sds = clsGeneral.QueryDB(ssql)
        If sds.Tables.Count > 0 And sds.Tables(0).Rows.Count > 0 Then
            For Each sdatarow In sds.Tables(0).Rows

                ' create an xml string for the record
                sxsql = "select " & _
                " * " & _
                " from [SensorLimits] " & _
                " where slGUID = '" & sdatarow.item("slGUID") & "' " & _
                " FOR XML auto, elements "

                sxds = clsGeneral.QueryDB(sxsql)
                If sxds.Tables.Count > 0 And sxds.Tables(0).Rows.Count > 0 Then
                    If ChangeRec(sxds.Tables(0).Rows(0).Item(0).ToString, "SensorLimits", sdatarow.item("slGUID")) > 0 Then
                        reccount += 1

                        ' reset the tdefModFlag of the source record
                        updsql = "update sensorlimits set slmodflag = '' where slGUID = '" & sdatarow.item("slGUID") & "'"
                        clsGeneral.ActionQuery(updsql)

                    End If
                End If

                sxds = Nothing

            Next
        End If

        Return reccount.ToString + " Recs Updated."
    End Function
    Private Function ChangeRec(ByVal xml As String, ByVal tblname As String, ByVal key As String) As Integer

        ' to do: ensure datasyncservice is returning correctly when positive results
        Try
            Dim DataSyncService As New datasyncservice.Service1

            EventLog1.WriteEntry("Attempting to post a record to " & tblname & " using " & DataSyncService.Url & ".", EventLogEntryType.Information)

            DataSyncService.Url = serverdataurl

            EventLog1.WriteEntry("Calling " & DataSyncService.Url & ".", EventLogEntryType.Information)

            ' call the service
            Return DataSyncService.UpdTable(xml)

        Catch ex As Exception
            Return 0
        End Try

    End Function

End Class
