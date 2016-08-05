Public Class tanklog
    Inherits System.Web.UI.Page
    Dim oLogdata As clsLogdata

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If Page.Request.QueryString("rec") <> "" Then
                If Not writetanklog2(Page.Request.QueryString("rec")) Then
                    ' error
                End If
            End If
        End If
    End Sub
    Protected Function writetanklog2(ByVal rec As String) As Boolean
        ' sample query string
        ' LS65001;T01001;R65;W87;A87;Z7/25/2014 8:43:48 AM;V0;P35.227087,-80.843127
        ' this sequence can repeat multiple times (determined by the submitter)
        ' call with
        ' http://localhost:58288/tanklog.aspx?rec=LS65001;T01001;R65;W87;A87;Z7/25/2014%208:43:48%20AM;V0;P35.227087,-80.843127


        Dim logs(), fields() As String
        Dim logstring, fieldstring As String

        Dim siteid As String = ""
        Dim tankid As String = ""
        Dim range As String = ""
        Dim rangetype As String = ""
        Dim water As String = ""
        Dim ambient As String = ""
        Dim timestamp As String = ""
        Dim voltage As String = ""
        Dim position As String = ""

        Try

            logs = rec.Split("L")

            For Each logstring In logs
                If logstring.Length > 0 Then
                    fields = logstring.Split(";")
                    For Each fieldstring In fields
                        Select Case fieldstring.Substring(0, 1)
                            Case "S"
                                siteid = fieldstring.Substring(1, fieldstring.Length - 1)
                            Case "T"
                                tankid = fieldstring.Substring(1, fieldstring.Length - 1)
                            Case "R", "D", "G"
                                rangetype = fieldstring.Substring(0, 1)
                                range = fieldstring.Substring(1, fieldstring.Length - 1)
                            Case "W"
                                water = fieldstring.Substring(1, fieldstring.Length - 1)
                            Case "A"
                                ambient = fieldstring.Substring(1, fieldstring.Length - 1)
                            Case "V"
                                voltage = fieldstring.Substring(1, fieldstring.Length - 1)
                            Case "Z"
                                timestamp = fieldstring.Substring(1, fieldstring.Length - 1)
                            Case "P"
                                position = fieldstring.Substring(1, fieldstring.Length - 1)
                            Case Else

                        End Select
                    Next

                    If siteid <> "" And tankid <> "" Then
                        If timestamp = "" Then
                            timestamp = Now()
                        End If

                        Dim sql As String = "INSERT INTO [tankData]  " & _
                       "([tdSiteID] " & _
                       ",[tdSensorID] " & _
                       ",[tdDateTime] " & _
                       ",[tdTankResp] " & _
                       ",[tdProcessed] " & _
                       ",[tdRangeType] " & _
                       ",[tdRange] " & _
                       ",[tdATemp] " & _
                       ",[tdWTemp] " & _
                       ",[tdVolts] " & _
                       ") VALUES( " & _
                       " '" & siteid & "'" & _
                       ",'" & tankid & "'" & _
                       ",'" & timestamp & "'" & _
                       ",'manual'" & _
                       ",'N'" & _
                       ",'" & rangetype & "'" & _
                       ", " & clsGeneral.IsZed(range) & " " & _
                       ", " & clsGeneral.IsZed(ambient) & " " & _
                       ", " & clsGeneral.IsZed(water) & " " & _
                       ", " & clsGeneral.IsZed(voltage) & " " & _
                       ") "

                        If clsGeneral.ActionQuery(sql, , ) < 1 Then
                            ' error
                            Return False
                        End If

                        Return True

                    End If

                End If
            Next

        Catch ex As Exception
            ' error
            Return False

        End Try

    End Function

    'Protected Function writetanklog(ByVal rec As String) As Boolean

    '    Dim fields(), logDate, logTime, sensor, value As String
    '    Dim gpsLat, gpsLatNS, gpsLon, gpsLonEW, gpsDate, gpsTime, gpsAlt, gpsSpd, gpsTimS As String
    '    Dim tankid As Integer
    '    Dim Sql As String

    '    Try

    '        fields = rec.Split(",")

    '        tankid = CInt(fields(0))
    '        logDate = fields(1)
    '        logTime = fields(2)
    '        sensor = fields(3)
    '        value = fields(4)
    '        gpsLat = fields(5)
    '        gpsLatNS = fields(6)
    '        gpsLon = fields(7)
    '        gpsLonEW = fields(8)
    '        gpsDate = fields(9)
    '        gpsTime = fields(10)
    '        gpsAlt = fields(11)
    '        gpsSpd = fields(12)
    '        gpsTimS = fields(13)

    '        If (logDate + logTime <> Nothing) And (sensor <> "") Then
    '            Sql = "INSERT INTO [tankLog] ([tlogTankID] ,[tlogDateTime] ,[tlogSensor] ,[tlogValue], [tlogLat], " & _
    '                "[tlogLatNS], [tlogLon], [tlogLonEW], [tlogGPSDate], [tlogGPSTime], [tlogAlt], [tlogSpd], [tlogTimS]) " & _
    '                "VALUES(" & tankid & ", '" & logDate & " " & logTime & "', '" & sensor & "', '" & value & "', " & _
    '                            " '" & gpsLat & "', '" & gpsLatNS & "', '" & gpsLon & "', '" & gpsLonEW & "', " & _
    '                            " '" & gpsDate & "', '" & gpsTime & "', '" & gpsAlt & "', '" & gpsSpd & "', '" & gpsTimS & "')"
    '            If clsGeneral.ActionQuery(Sql, , ) < 1 Then
    '                ' error
    '            End If
    '        End If

    '        Return True
    '    Catch ex As Exception
    '        Return False
    '    End Try

    'End Function
End Class