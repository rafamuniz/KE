Imports System
Imports System.IO
Imports System.Data.SqlClient
Imports System.Data
Imports System.Data.SqlTypes

Public Class clsLogdata
    Dim oTank As clsTank
    Dim oFormula As clsVolFormula
    Public Elements As New uElements

    Public Structure uElements
        Dim iLogID As Long
        Dim sUnitID As String
        Dim dTimestamp As DateTime
        Dim sDesc As String
        Dim sValue As String
    End Structure
    'Public Function get_latest_log(ByVal tankid As Integer, ByVal sensor As String) As String
    '    Dim ds As DataSet = GetLog(tankid, sensor)
    '    If Not ds Is Nothing Then

    '        If ds.Tables(0).Rows.Count > 0 Then
    '            Dim value As Integer = 0
    '            Dim age As String = "9999/99/99:00:00:00"

    '            value = clsGeneral.IsZed(ds.Tables(0).Rows(0).Item("tlogValue"))
    '            age = clsGeneral.IsNull(ds.Tables(0).Rows(0).Item("tlogDateTime"))

    '            Return value.ToString + "," + age
    '        Else
    '            Return "UNK, UNK"
    '        End If
    '    Else
    '        Return "UNK, UNK"
    '    End If

    'End Function
    Public Function get_latest_data(ByVal id As String) As DataRow
        Dim sql As String
        Dim ds As New DataSet

        Try
            ' 
            ' sql = "select top(1) * from tankdata" & _
            sql = "select top(1) * from vLatestSamples " & _
                " where tdefID = '" & id & "' " & _
                " order by tdDateTime desc"

            ds = clsGeneral.QueryDB(sql)

            If ds.Tables(0).Rows.Count > 0 Then
                Return ds.Tables(0).Rows(0)
            Else
                Return Nothing
            End If

        Catch ex As Exception
            Return Nothing
        End Try

    End Function
    Public Function get_Latest_Coords(ByVal tankid As Integer) As String

        Return "UNK"
    End Function
    Public Function getGraphData(ByVal tankid As String, Optional ByVal mode As String = "D") As DataSet
        Dim sql As String
        Dim ds As New DataSet
        Dim viewName As String

        Select Case mode
            Case "R"
                viewName = "tdatRealtimeData"
            Case "D"
                viewName = "tdatDailyData"
            Case "H"
                viewName = "tdatHourlyData"
            Case Else
                viewName = "tdatDailyData"
        End Select

        Try
            ' 
            sql = "select * from " & viewName & _
                " where tdSensorID = '" & tankid & "'" & _
                " order by SampleTime asc"

            Dim galrem As Integer = 0
            Dim pctfull As Integer = 0
            Dim totvol As Integer = 0
            If oFormula Is Nothing Then
                oFormula = New clsVolFormula
            End If
            ds = clsGeneral.QueryDB(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                ' the code below has been supplanted by the view and sql scalar functions
                'ds.Tables(0).Columns.Add("wVol", GetType(Integer))
                'For Each samplerow In ds.Tables(0).Rows
                '    totvol = oFormula.CalcVolume(samplerow.Item("tdefID"), samplerow.Item("wRange"), pctfull, galrem)
                '    samplerow.item("wVol") = galrem
                'Next

                Return ds
            Else
                Return New DataSet
            End If

        Catch ex As Exception
            Return New DataSet
        End Try
    End Function
    'Public Function ImportLogFiles() As Integer

    '    Dim filePaths() As String = Directory.GetFiles(System.Web.HttpContext.Current.Server.MapPath("~/Logfiles/"))
    '    Dim count As Integer = 0

    '    For Each filePath As String In filePaths
    '        '            If ImportLogFromCSV(Path.GetFileName(filePath)) Then
    '        If ImportLogFromCSV(filePath) Then
    '            ' delete it
    '            File.Delete(filePath)

    '            ' bump the count
    '            count += 1
    '        End If
    '    Next

    '    Return count

    'End Function
    'Public Function ImportLogFromCSV(ByVal sfilename As String) As Boolean

    '    Dim filestream As StreamReader

    '    Dim filedate As DateTime
    '    filedate = File.GetCreationTime(sfilename)

    '    filestream = File.OpenText(sfilename)
    '    Dim textdelimiter As String
    '    textdelimiter = ","

    '    ' define field positions (zero relative: 2=3rd column in input file)
    '    Dim iUnitNumPos As Integer = 0
    '    Dim iTimeStampPos As Integer = 1
    '    Dim iDescPos As Integer = 2
    '    Dim iValuePos As Integer = 3

    '    Dim icount As Integer = 0
    '    Try ' to write all the records
    '        With Elements
    '            While Not filestream.EndOfStream
    '                Dim readstr As String = filestream.ReadLine
    '                Try
    '                    Dim splitfields = Split(readstr, textdelimiter)
    '                    'Dim splitfields = SplitAroundQuotes(filestream.ReadLine, textdelimiter)

    '                    .sUnitID = Trim(splitfields(iUnitNumPos))
    '                    If Len(Trim(.sUnitID)) > 0 Then
    '                        .dTimestamp = Trim(splitfields(iTimeStampPos))
    '                        .sDesc = Trim(splitfields(iDescPos))
    '                        .sValue = Trim(splitfields(iValuePos))

    '                        ' add record
    '                        If Not AddLog(Elements) Then
    '                            ' error encountered
    '                        End If
    '                    End If
    '                Catch ex As Exception
    '                    ' ignore this record
    '                End Try
    '            End While
    '            filestream.Close()
    '            Return True
    '        End With
    '    Catch ex As Exception
    '        filestream.Close()
    '        Return False
    '    End Try

    'End Function
    'Public Function AddLog(ByVal oElements As uElements) As Boolean
    '    Dim strSQL As String

    '    Try 'to add this record to the log table
    '        With oElements
    '            strSQL = "INSERT INTO [tankLog] " & _
    '               "([tlogTankID]" & _
    '               ",[tlogDateTime]" & _
    '               ",[tlogSensor]" & _
    '               ",[tlogValue]" & _
    '            ") VALUES (" & _
    '                "  '" & .sUnitID & "' " & _
    '                ", '" & .dTimestamp & "' " & _
    '                ", '" & .sDesc & "'" & _
    '                ", '" & .sValue & "'" & _
    '                ")"

    '            .iLogID = clsGeneral.ActionQuery(strSQL, True)
    '            If .iLogID <= 0 Then
    '                ' error adding rec
    '            Else
    '                Return True
    '            End If
    '        End With
    '    Catch ex As Exception
    '        Return False
    '    End Try
    'End Function
    'Function SplitAroundQuotes(ByVal TextToSplit As String, _
    '         Optional ByVal Delimiter As String = ",") As String()
    '    Dim QuoteDelimited() As String
    '    Dim WorkingArray() As String
    '    QuoteDelimited = Split(TextToSplit, """")
    '    For x = 1 To UBound(QuoteDelimited) Step 2
    '        QuoteDelimited(x) = Replace$(QuoteDelimited(x), Delimiter, System.Convert.ToChar(0))
    '    Next
    '    TextToSplit = Join(QuoteDelimited, """")
    '    TextToSplit = Replace$(TextToSplit, Delimiter, System.Convert.ToChar(1))
    '    TextToSplit = Replace$(TextToSplit, System.Convert.ToChar(0), Delimiter)
    '    '  Uncomment the following line if you want the quote marks to be removed
    '    TextToSplit = Replace$(TextToSplit, """", "")
    '    WorkingArray = Split(TextToSplit, System.Convert.ToChar(1))
    '    SplitAroundQuotes = WorkingArray
    'End Function
End Class
