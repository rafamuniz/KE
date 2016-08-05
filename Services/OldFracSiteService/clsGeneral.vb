Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data
Imports System.Data.SqlTypes
Imports System.IO
Imports System.Xml
Imports System

Public Class clsGeneral
    Shared connectionString As String = My.Settings.ConnectString

    Public Shared Function QueryDB(ByVal sql As String) As DataSet
        Dim ds As New DataSet
        Dim dta As SqlDataAdapter
        Dim db As SqlConnection

        On Error GoTo err

        db = New SqlConnection(connectionString)

        dta = New SqlDataAdapter(sql, db)
        dta.Fill(ds, "rs")

        QueryDB = ds

        db.Close()


        Exit Function

err:
        QueryDB = ds
        db.Close()
    End Function
    Public Shared Function IsADate(ByVal o As Object) As Object
        If IsDBNull(o) Then
            Return "1/1/1990"
        ElseIf IsDate(o) Then
            Return o
        Else
            Return "1/1/1990"
        End If
    End Function
    Public Shared Function IsZed(ByVal o As Object) As Object
        If IsDBNull(o) Then
            Return 0
        ElseIf IsNumeric(o) Then
            Return o
        Else
            Return 0
        End If

    End Function
    Public Shared Function IsNull(ByVal o As Object) As Object
        If o Is DBNull.Value Then
            IsNull = ""

        Else
            IsNull = Trim(o.ToString)
        End If
    End Function
    Public Shared Function ActionQuery(ByVal sql As String, Optional ByVal bIdentityInsert As Boolean = False, Optional ByRef lReturnedError As Long = 0) As Long
        Dim ds As New DataSet
        Dim db As SqlConnection
        Dim cmd As SqlCommand
        Dim lRows As Long
        Dim dta As SqlDataAdapter


        On Error GoTo err

        db = New SqlConnection(connectionString)
        db.Open()

        cmd = New SqlCommand(sql, db)
        cmd.CommandTimeout = 0

        lRows = cmd.ExecuteNonQuery

        If lRows <= 0 Then GoTo err

        If bIdentityInsert Then
            ' get identity value just inserted
            sql = "select @@identity"
            dta = New SqlDataAdapter(sql, db)
            dta.Fill(ds, "rs")
            ActionQuery = CLng(Val(ds.Tables("rs").Rows(0).Item(0)))
        Else
            ActionQuery = lRows
        End If

        db.Close()

        Exit Function
err:
        lReturnedError = Err.Number
        ActionQuery = -1
        db.Close()
    End Function
    Public Shared Function GetCodeValue(ByVal sGroup As String, ByVal sCode As String) As String
        Dim sql As String
        Dim ds As New DataSet

        Try
            ' 
            sql = "select rtrim(cCode) as cCode, rtrim(cDesc) as cDesc " & _
                    "from [CodeValue] where cGroup = '" & sGroup & "' and cCode = '" & sCode & "'"

            ds = QueryDB(sql)
            If ds.Tables(0).Rows(0).Item("cDesc") <> "" Then
                Return ds.Tables(0).Rows(0).Item("cDesc")
            Else
                Return ""
            End If

        Catch ex As Exception
            Return "err"
        End Try

    End Function
    Public Shared Function GetCodeList(ByVal sGroup As String) As DataSet
        Dim sql As String
        Dim ds As New DataSet

        Try
            ' 
            sql = "select rtrim(cCode) as cCode, rtrim(cDesc) as cDesc " & _
                    "from [CodeValue] where cGroup = '" & sGroup & "'"

            ds = QueryDB(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                Return ds
            Else
                Return Nothing
            End If

        Catch ex As Exception
            Return Nothing
        End Try

    End Function
    Public Shared Function IsBool(ByVal test As String) As Boolean
        Select Case test
            Case "A", "Y", "T"
                Return True
            Case "I", "N", "F"
                Return False
            Case Else
                Return False
        End Select
    End Function
    Public Shared Function BoolTOAI(ByVal test As Boolean) As String
        Select Case test
            Case True
                Return "A"
            Case False
                Return "I"
            Case Else
                Return "A"
        End Select
    End Function
    Public Shared Function PostLastLog() As Integer
        Try


            Dim db As SqlConnection
            db = New SqlConnection(connectionString)
            db.Open()

            Dim myCommand As New SqlCommand("PostLastLog", db)
            myCommand.CommandType = CommandType.StoredProcedure

            'Create a SqlParameter object to hold the output parameter value
            Dim retValParam As New SqlParameter("@recs", SqlDbType.Int)
            retValParam.Direction = ParameterDirection.Output
            myCommand.Parameters.Add(retValParam)

            'Call the sproc to update the prize rec with the winner, and the contact rec with the prize won
            Dim reader As SqlDataReader = myCommand.ExecuteReader()

            Return retValParam.Value

        Catch ex As Exception
            Return -1
        End Try

    End Function
    Public Shared Function IsSensorActive(ByVal sSensorID As String) As Boolean
        Try
            Dim strSQL As String
            Dim ds As New DataSet

            strSQL = "select tdefActive from vSiteTanks  " & _
            "where tdefSensorID = '" & sSensorID & "'"

            ds = QueryDB(strSQL)
            If ds.Tables(0).Rows(0).Item("tdefActive") = "A" Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Return False
        End Try

    End Function
End Class

