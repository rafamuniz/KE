Public Class datascan
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblResponse.Text = FindRecs()
    End Sub
    Public Function FindRecs() As String
        Dim reccount As Integer = 0

        Dim uds, uxds As New DataSet ' datasets for webuser and webuserxml records
        Dim usql, uxsql As String    ' query strings for webuser and webuserxml querys
        Dim ucsql As String          ' query string for clearing the webuser 'U'pdated flag

        ' get all webuser table changed rows
        usql = "select usrGUID from WebUser where usrModFlag = 'U'"
        uds = clsGeneral.QueryDB(usql)
        If uds.Tables.Count > 0 And uds.Tables(0).Rows.Count > 0 Then
            For Each udatarow In uds.Tables(0).Rows

                ' create an xml string for the record
                uxsql = "select " & _
                "  WebUser.* " & _
                " ,Address.* " & _
                " from [WebUser] " & _
                " join [Relation] " & _
                " on WebUser.usrID = Relation.relID1 and Relation.relType = 'UA' " & _
                " join [Address] on Address.adID = Relation.relID2  " & _
                " where usrGUID = '" & udatarow.item("usrGUID") & "' " & _
                " FOR XML auto, elements "

                uxds = clsGeneral.QueryDB(uxsql)
                If uxds.Tables.Count > 0 And uxds.Tables(0).Rows.Count > 0 Then
                    If ChangeRec(uxds.Tables(0).Rows(0).Item(0).ToString, "WebUser", udatarow.item("usrGUID")) Then
                        reccount += 1

                        ' clear webuser record's 'U'pdated flag
                        ucsql = "update webuser set usrModFlag = '' where usrGUID = '" + udatarow.item("usrGUID") + "'"
                        clsGeneral.QueryDB(ucsql)
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
                    If ChangeRec(txds.Tables(0).Rows(0).Item(0).ToString, "TankDef", tdatarow.item("tdefGUID")) Then
                        reccount += 1

                        ' clear tankdef record's 'U'pdated flag
                        ucsql = "update tankdef set tdefModFlag = '' where tdefGUID = '" + tdatarow.item("tdefGUID") + "'"
                        clsGeneral.QueryDB(ucsql)
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
                    If ChangeRec(sxds.Tables(0).Rows(0).Item(0).ToString, "SensorLimits", sdatarow.item("slGUID")) Then
                        reccount += 1

                        ' reset the tdefModFlag of the source record
                        ucsql = "update sensorlimits set slmodflag = '' where slGUID = '" & sdatarow.item("slGUID") & "'"
                        clsGeneral.QueryDB(ucsql)

                    End If
                End If

                sxds = Nothing

            Next
        End If


        LogIt(reccount.ToString + " Total Recs Updated.")

        Return reccount.ToString + " Total Recs Updated."
    End Function
    Private Sub LogIt(ByVal result As String)

        Dim sql As String = "INSERT INTO [dbo].[ErrLog] " & _
            "([errDesc],[errLocation],[errDateTime],[errActionTaken],[errNoticeSent] " & _
            ") VALUES ( " & _
            " 'DataScan Requested : " & result & " records updated'" & _
            ", isnull(@@SERVERNAME,''), GetDate(), 'None', 'N')"

        clsGeneral.QueryDB(sql)

    End Sub
    Private Function ChangeRec(ByVal xml As String, ByVal tblname As String, ByVal key As String) As Boolean
        Try

            Dim DataSyncService As New localhost.Service1

            Dim sql As String
            Dim ds As DataSet

            If ConfigurationManager.AppSettings("sitemode") = "Y" Then
                ' if this service is running on a site, get the target ip address to the main server
                DataSyncService.Url = Replace(ConfigurationManager.AppSettings("SyncToURL").ToString, "[hostname]", ConfigurationManager.AppSettings("SyncToHost"))

                ' call the service
                LogIt(DataSyncService.UpdTable(xml).ToString + " " + tblname)

            Else
                '   if this service is running on the main server; distribute to one or more sites

                Select Case tblname
                    Case "TankDef" ' for TankDef Records: hit the one site
                        sql = "select stIPAddress from [site] where stID in " & _
                            "(select tdefSiteID from tankdef where tdefGUID = '" & key & "')"
                        ds = clsGeneral.QueryDB(sql)
                        If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                            DataSyncService.Url = Replace(ConfigurationManager.AppSettings("SyncToURL").ToString, "[hostname]", ds.Tables(0).Rows(0).Item("stIPAddress"))

                            ' call the service
                            LogIt(DataSyncService.UpdTable(xml).ToString + " " + tblname)

                            Return True

                        End If

                    Case "WebUser" ' for WebUser Records: get a list of ip addresses to the target sites for this user and hit each one
                        '-- get all sites for this user
                        sql = "select stIPAddress from [site] where stid in " & _
                            " (select relid2 from [relation] where reltype = 'US' and relid1 = " & _
                                " (select usrID from webuser where usrGUID = '" & key & "'))"
                        ds = clsGeneral.QueryDB(sql)
                        If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                            For Each siterow In ds.Tables(0).Rows
                                DataSyncService.Url = Replace(ConfigurationManager.AppSettings("SyncToURL").ToString, "[hostname]", siterow.item("stIPAddress"))

                                ' call the service
                                LogIt(DataSyncService.UpdTable(xml).ToString + " " + tblname)

                                Return True
                            Next
                        End If

                    Case "SensorLimits" ' for SensorLimits Records: hit the one site
                        sql = "select stIPAddress from [site] where stID in " & _
                            "(select tdefSiteID from tankdef where tdefSensorID = " & _
                            "(select slSensorID from SensorLimits where slGUID = '" & key & "'))"

                        ds = clsGeneral.QueryDB(sql)
                        If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                            DataSyncService.Url = Replace(ConfigurationManager.AppSettings("SyncToURL").ToString, "[hostname]", ds.Tables(0).Rows(0).Item("stIPAddress"))

                            ' call the service
                            LogIt(DataSyncService.UpdTable(xml).ToString + " " + tblname)

                            Return True

                        End If

                    Case Else
                        Return False
                End Select

            End If

            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function

End Class