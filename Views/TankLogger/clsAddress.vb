Imports System
Imports System.IO
Imports System.Data.SqlClient
Imports System.Data
Imports System.Data.SqlTypes
Public Class clsAddress
    Public oElements As New uElements
    Public Structure uElements
        Dim adID As Integer
        Dim adAddr1 As String
        Dim adAddr2 As String
        Dim adCity As String
        Dim adSt As String
        Dim adZip As String
        Dim adPh1 As String
        Dim adEmail As String
    End Structure
    Public Function AddAddress() As Long
        Dim strSQL As String

        Try 'to add this record to the relations table
            With oElements
                strSQL = "INSERT INTO [Address] " & _
                   "([adAddr1]" & _
                   ",[adAddr2]" & _
                   ",[adCity]" & _
                   ",[adST]" & _
                   ",[adZip]" & _
                   ",[adPh1]" & _
                   ",[adEmail]" & _
                ") VALUES (" & _
                    "  '" & .adAddr1 & "' " & _
                    ", '" & .adAddr2 & "' " & _
                    ", '" & .adCity & "' " & _
                    ", '" & .adSt & "' " & _
                    ", '" & .adZip & "' " & _
                    ", '" & .adPh1 & "' " & _
                    ", '" & .adEmail & "' " & _
                    ")"

                Return clsGeneral.ActionQuery(strSQL, True)

            End With
        Catch ex As Exception
            Return 0
        End Try
    End Function

    Public Function delAddress() As Boolean
        Dim strSQL As String

        Try 'to add this record to the relations table
            With oElements
                strSQL = "delete from [Address] wher adID = " + .adID

                If clsGeneral.ActionQuery(strSQL) <= 0 Then
                    ' error removing rec
                    Return False
                Else
                    Return True
                End If
            End With
        Catch ex As Exception
            Return False
        End Try
    End Function

End Class
