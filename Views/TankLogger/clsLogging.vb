Public Class clsLogging
    Public Shared Function LogUser(ByVal iUID As Integer, _
                              ByVal sMode As String) As Boolean

        Dim logid As Integer

        Try ' to write the logins record
            Dim strSQL As String
            strSQL = "INSERT INTO [Logins] " & _
                    "([laUID]" & _
                    ",[laDateTime]" & _
                    ",[laMode]" & _
                    ") VALUES " & _
                    "( " & iUID & " " & _
                    ", getdate() " & _
                    ",'" & sMode & "'" & _
                    ")"

            logid = clsGeneral.ActionQuery(strSQL, True)


        Catch ex As Exception
            '  error - inserting rec
            Return 0
        End Try

        Return logid

    End Function
    Public Shared Function GetRepLogs(Optional ByVal usrid As Integer = 0) As DataSet

        Dim sql As String
        Dim ds As New DataSet

        Try ' all log records for this user

            sql = "select " & _
            "   lauid " & _
         ", (select usrfname + ' ' + usrlname from WEbUser where usrID = lauid) as usrname " & _
         ", CAST(ladatetime as DATE) as DateWorked " & _
         ", MIN(ladatetime) as FirstActivity " & _
         ", MAX(ladatetime) as LastActivity " & _
         ", (SELECT datediff(ss,MIN(ladatetime),MAX(ladatetime))/3600.00) as ElapsedHours " & _
         ", COUNT(laid) as NumberOperations " & _
         "from [logins]  "

            If usrid > 0 Then
                sql = sql + "where usrid = " & usrid & " "
            End If

            sql = sql + "group by lauid, CAST(ladatetime as DATE) " & _
                        "order by CAST(ladatetime as DATE) desc "

            ds = clsGeneral.QueryDB(sql)

            Return ds
        Catch ex As Exception
            Return New DataSet
        End Try

    End Function
    Shared Function LogError(ByVal desc As String, _
                             ByVal loc As String, _
                             ByVal action As String, _
                             Optional ByVal notify As Boolean = False) As Integer
        ' write an error to the log and return the record ID
        Dim strSQL As String
        Dim ident As Long

        Try ' to insert an invoice record

            strSQL = "INSERT INTO ErrLog (" & _
                         " errDesc" & _
                         ",errLocation" & _
                         ",errDateTime" & _
                         ",errActionTaken" & _
                         ",errNoticeSent" & _
                        ") VALUES (" & _
                        " '" & desc & "' " & _
                        ",'" & loc & "' " & _
                        ", getdate() " & _
                        ",'" & action & "' " & _
                        ",'" & "N" & "'  " & _
                        ")"

            ident = clsGeneral.ActionQuery(strSQL, True)

            ' send a notice if the failing module requests it, and if email notices are active
            'If notify And (clsGeneral.GetCodeDesc("ErrLog", "emailon") = "Y") Then
            '    Dim oEmail As New clsEmail
            '    oEmail.ErrLogData.iErrorID = ident
            '    oEmail.ErrLogData.sFromEmail = "MailAgent@theuplog.com"
            '    oEmail.ErrLogData.sFromName = "AccurIT"
            '    oEmail.ErrLogData.sToEmail = clsGeneral.GetCodeDesc("ErrLog", "email")
            '    oEmail.ErrLogData.sToName = "System Admin"

            '    If oEmail.SendErrorNotice() Then
            '        ' update the errlog record with the notice flag if the send succeeds
            '        strSQL = "update errlog set errNoticeSent = 'Y' where errid = " & ident.ToString
            '        clsGeneral.ActionQuery(strSQL, False)
            '    End If
            'End If

            If ident > 0 Then
                Return ident
            Else
                Return 0
                ' error updating user preferences
            End If

        Catch ex As Exception
            Return 0
            ' undetermined error
        End Try

    End Function
End Class
