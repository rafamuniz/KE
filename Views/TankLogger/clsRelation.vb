Imports System
Imports System.IO
Imports System.Data.SqlClient
Imports System.Data
Imports System.Data.SqlTypes
Public Class clsRelation
    Public oElements As New uElements
    Public Structure uElements
        Dim relID As Integer
        Dim relType As String
        Dim relID1 As Integer
        Dim relID2 As Integer
    End Structure

    Public Function addRelation() As Boolean
        Dim strSQL As String

        Try 'to add this record to the relations table
            With oElements
                strSQL = "INSERT INTO [Relation] " & _
                   "([relType]" & _
                   ",[relID1]" & _
                   ",[relID2]" & _
                ") VALUES (" & _
                    "  '" & .relType & "' " & _
                    ",  " & .relID1 & "  " & _
                    ",  " & .relID2 & "  " & _
                    ")"

                Return clsGeneral.ActionQuery(strSQL, True)
                If .relID <= 0 Then
                    ' error adding rec
                    Return False
                Else
                    Return True
                End If
            End With
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function delRelation() As Boolean
        Dim strSQL As String

        Try 'to add this record to the relations table
            With oElements
                strSQL = "delete from [Relation] where relID = " + .relID

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