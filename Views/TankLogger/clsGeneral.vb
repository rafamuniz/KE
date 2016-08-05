Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data
Imports System.Data.SqlTypes
Imports System.IO
Imports System.Xml
Imports System

Public Class clsGeneral

    Public Shared Function QueryDB(ByVal sql As String) As DataSet
        Dim ds As New DataSet
        Dim dta As SqlDataAdapter
        Dim db As SqlConnection

        On Error GoTo err

        db = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnString").ConnectionString)

        dta = New SqlDataAdapter(sql, db)
        dta.Fill(ds, "rs")

        QueryDB = ds

        db.Close()


        Exit Function

err:
        QueryDB = ds
        db.Close()
    End Function
    Public Shared Function FillDD(ByRef ddDropDown As Object, ByVal sGroup As String) As Boolean
        Dim sql As String
        Dim ds As New DataSet
        Try
            ' all Source for the reps company
            sql = "select rtrim(cCode) as cCode, rtrim(cDesc) as cDesc " & _
                    "from [CodeValue] where cGroup = '" & sGroup & "'"

            ds = QueryDB(sql)
            ddDropDown.DataSource = ds
            ddDropDown.DataMember = ds.Tables(0).TableName
            ddDropDown.DataTextField = "cDesc"
            ddDropDown.DataValueField = "cCode"
            ddDropDown.DataBind()

            Return True
        Catch ex As Exception
            Return False
        End Try

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

        db = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnString").ConnectionString)
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

            Return True
        Catch ex As Exception
            Return False
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
    Public Shared Function LoadXMLtoTable(ByRef tblTable As HtmlTable, ByVal sXMLFile As String, ByVal nodelist As String) As Boolean
        Dim doc As New XmlDocument
        Dim ndlst As XmlNodeList
        Dim node As XmlNode

        Try ' to fill the table with rows dynamically from the xml file


            'doc.Load(AppDomain.CurrentDomain.BaseDirectory & sXMLFile)
            doc.Load(HttpContext.Current.Server.MapPath("") & "\" & sXMLFile)
            ndlst = doc.SelectNodes(nodelist)

            For Each node In ndlst
                Dim desc As String = node.ChildNodes(0).InnerText
                Dim text As String = node.ChildNodes(1).InnerText
                Dim link As String = node.ChildNodes(2).InnerText

                If desc <> "" And link <> "" Then
                    Dim tblrow1 As New HtmlTableRow
                    Dim tblrow2 As New HtmlTableRow

                    Dim tblcell1 As New HtmlTableCell
                    tblcell1.Controls.Add(New LiteralControl("<a href='" & link & "' Target='_blank'>" & text & "</a>"))
                    tblrow1.Cells.Add(tblcell1)

                    tblTable.Rows.Add(tblrow1)

                    Dim tblcell2 As New HtmlTableCell
                    tblrow2.Cells.Add(tblcell2)
                    tblcell2.Controls.Add(New LiteralControl(desc & "<br /><br />"))
                    tblTable.Rows.Add(tblrow2)
                End If
            Next

            Return True
        Catch ex As Exception
            ' error
            Return False
        End Try
    End Function
    Public Shared Function GetBaseURL(ByVal sURL As Uri) As Uri

        Return New Uri(sURL.GetLeftPart(UriPartial.Authority))

    End Function
    Public Shared Function GetCodeDesc(ByVal sGroup As String, ByVal sCode As String) As String
        Dim sql As String
        Dim ds As New DataSet

        Try
            ' 
            sql = "select rtrim(cDesc) as cDesc " & _
                    "from [CodeValue] where cGroup = '" & sGroup & "' and cCode = '" & sCode & "'"

            ds = QueryDB(sql)
            If ds.Tables(0).Rows(0).Item("cDesc") <> "" Then
                Return ds.Tables(0).Rows(0).Item("cDesc")
            Else
                Return 0
            End If

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Shared Function GetLookup(ByVal lGroupId As Long, ByVal lEntryId As Long) As String
        Try
            Dim sql As String
            Dim ds As New DataSet

            sql = String.Format("select sEntry from tblLookup where lGroupId = {0} and lEntryId = {1}", lGroupId, lEntryId)
            ds = QueryDB(sql)

            Return ds.Tables(0).Rows(0).Item("sEntry").ToString

        Catch ex As Exception
            Return ""
        End Try
    End Function
End Class
