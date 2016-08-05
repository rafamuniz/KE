Public Class clsUser
    Public Prefs As New uPrefs
    Public oElements As New uElements
    Public oAddr As New uAddress

    Public Structure uPrefs
        Dim sUID As String
        Dim sPWD As String
        Dim iusrID As Integer
        Dim sUsrFName As String
        Dim bAuthStat As Boolean
        Dim sRole As String
        Dim sMessage As String
    End Structure
    Public Structure uElements
        Dim usrID As Long
        Dim usrFName As String
        Dim usrLName As String
        Dim usrEmail As String
        Dim usrPwd As String
        Dim usrDefaultCustID As Long
        Dim usrRole As String
        Dim usrStatus As String
        Dim usrLastUpdate As DateTime
        Dim usrModFlag As String
        Dim usrGUID As String
    End Structure
    Public Structure uAddress
        Dim adID As Integer
        Dim adAddr1 As String
        Dim adAddr2 As String
        Dim adCity As String
        Dim adSt As String
        Dim adZip As String
        Dim adPh1 As String
        Dim adEmail As String
    End Structure

    Public Sub New()
        Prefs.sUID = ""
        Prefs.sPWD = ""
        Prefs.iusrID = 0
        Prefs.sUsrFName = ""
        Prefs.bAuthStat = False
        Prefs.sRole = ""
        Prefs.sMessage = ""
    End Sub

    Public Function Logon() As Boolean
        Dim dsPrefs As DataSet

        Try
            ' determine user specifics
            dsPrefs = clsGeneral.QueryDB("select * " & _
                                    "from WebUser  " & _
                                    "where usrEmail = '" & Prefs.sUID & "'")
            If dsPrefs.Tables(0).Rows.Count > 0 Then
                If Trim(dsPrefs.Tables(0).Rows(0).Item("usrPWD")) = Prefs.sPWD Then
                    Prefs.bAuthStat = True
                    Prefs.sUsrFName = Trim(dsPrefs.Tables(0).Rows(0).Item("usrFName"))
                    Prefs.sRole = Trim(dsPrefs.Tables(0).Rows(0).Item("usrRole"))
                    Prefs.iusrID = Trim(dsPrefs.Tables(0).Rows(0).Item("usrID"))

                    clsLogging.LogUser(Prefs.iusrID, "LogOn")

                    Prefs.sMessage = Prefs.sUsrFName + " logged on."
                    Return True
                Else
                    ' bad pwd
                    Prefs.bAuthStat = False
                    Prefs.sMessage = "Invalid Password."
                    Return False
                End If
            Else
                ' bad uid
                Prefs.bAuthStat = False
                Prefs.sMessage = "Invalid User ID."
                Return False
            End If

        Catch ex As Exception
            clsLogging.LogError("LogonFailed with " & Err.Number.ToString & ":" & Err.ToString, "Logon()", "stop", False)
            Prefs.sMessage = "Please contact us using the Contact Us link."
            Return False
        End Try

    End Function
    Public Function IsAuthorized() As Boolean
        Dim ReqPage As String
        ReqPage = System.IO.Path.GetFileName(System.Web.HttpContext.Current.Request.Url.AbsolutePath)

        Dim dsPrefs As DataSet
        Try
            ' query access rights table for access requested
            Dim qry As String = "select access " & _
                                    "from pagerights  " & _
                                    "where page = '" & ReqPage & "'" & _
                                    " and code = '" & Prefs.sRole & "'"

            dsPrefs = clsGeneral.QueryDB(qry)

            If dsPrefs.Tables(0).Rows.Count > 0 Then

                If CBool(dsPrefs.Tables(0).Rows(0).Item("access")) = True Then

                    clsLogging.LogUser(Prefs.iusrID, "AccessGranted:" & ReqPage)

                    ' grant access 
                    Return True

                Else

                    clsLogging.LogUser(Prefs.iusrID, "AccessDenied:" & ReqPage)

                    ' deny access
                    Return False
                End If
            Else
                Return False ' access not allowed due to record for this user type not found
            End If
        Catch
            Return False ' error - default to no access
        End Try

    End Function
    Public Function GetUsersCusts(uid As Integer) As DataSet
        Dim sql As String
        Dim ds As New DataSet

        Try
            ' 
            sql = "select tmid, tmdesc,'/images/'+tmimagenodim as tmimage from tankmodel"

            ds = clsGeneral.QueryDB(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                Return ds
            Else
                Return Nothing
            End If

        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Function getUsers(ByVal id As Long) As DataSet
        Dim sql As String
        Dim ds As New DataSet

        Try
            ' 
            sql = "select *, usrFName + ' ' + usrLName as usrName from webuser " & _
                "where usrDefaultCustID = " & id

            ds = clsGeneral.QueryDB(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                Return ds
            Else
                Return Nothing
            End If

        Catch ex As Exception
            Return Nothing
        End Try

    End Function
    Public Function AddUser() As Boolean
        '    ' add a Site Record and retain the record ID
        Dim strSQL As String

        Try 'to add this record 
            With oElements
                strSQL = "INSERT INTO [WebUser] " & _
                   "([usrFName]" & _
                   ",[usrLName]" & _
                   ",[usrEmail]" & _
                   ",[usrPwd]" & _
                   ",[usrDefaultCustID]" & _
                   ",[usrRole]" & _
                   ",[usrStatus]" & _
                   ",[usrDateLastModified]" & _
                   ",[usrModFlag]" & _
                   ",[usrGUID]" & _
                   ") VALUES (" & _
                    "  '" & .usrFName & "' " & _
                    ", '" & .usrLName & "' " & _
                    ", '" & .usrEmail & "' " & _
                    ", '" & .usrPwd & "' " & _
                    ",  " & .usrDefaultCustID & "  " & _
                    ", '" & .usrRole & "' " & _
                    ", '" & .usrStatus & "' " & _
                    ", '" & DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & "' " & _
                    ", 'U' " & _
                    ", newid() " & _
                    ")"

                .usrID = clsGeneral.ActionQuery(strSQL, True)
                If .usrID <= 0 Then
                    ' error adding rec
                    Return False
                End If
            End With
        Catch ex As Exception
            Return False
        End Try

        ' add an Address Record and retain record ID
        Dim oAddress As New clsAddress
        oAddress.oElements.adAddr1 = oAddr.adAddr1
        oAddress.oElements.adAddr2 = oAddr.adAddr2
        oAddress.oElements.adCity = oAddr.adCity
        oAddress.oElements.adSt = oAddr.adSt
        oAddress.oElements.adZip = oAddr.adZip
        oAddress.oElements.adPh1 = oAddr.adPh1
        oAddress.oElements.adEmail = oAddr.adEmail
        oAddress.oElements.adID = oAddress.AddAddress()
        If oAddress.oElements.adID < 0 Then
            ' error
            Return False
        End If

        ' write the Relation record
        Dim oRelation As New clsRelation
        oRelation.oElements.relType = "UA"
        oRelation.oElements.relID1 = oElements.usrID
        oRelation.oElements.relID2 = oAddress.oElements.adID

        If Not oRelation.addRelation() Then
            ' error
            Return False
        End If

        Return True
    End Function
    Public Function getUser(ByVal id As Long) As Boolean
        Dim sql As String
        Dim ds As New DataSet

        Try ' to get the site record
            ' 
            sql = "select * from webuser" & _
                " where usrid = " & id

            ds = clsGeneral.QueryDB(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                Try
                    With ds.Tables(0).Rows(0)
                        oElements.usrFName = clsGeneral.IsNull(.Item("usrFName"))
                        oElements.usrLName = clsGeneral.IsNull(.Item("usrLName"))
                        oElements.usrEmail = clsGeneral.IsNull(.Item("usrEmail"))
                        oElements.usrPwd = clsGeneral.IsNull(.Item("usrPwd"))
                        oElements.usrDefaultCustID = clsGeneral.IsZed(.Item("usrDefaultCustID"))
                        oElements.usrRole = clsGeneral.IsNull(.Item("usrRole"))
                        oElements.usrStatus = clsGeneral.IsNull(.Item("usrStatus"))
                        oElements.usrLastUpdate = clsGeneral.IsNull(.Item("usrDateLastModified"))
                        oElements.usrModFlag = clsGeneral.IsNull(.Item("usrModFlag"))
                        oElements.usrGUID = clsGeneral.IsNull(.Item("usrGUID"))
                    End With
                Catch ex As Exception
                    ' error
                    Return False
                End Try
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try

        ' try to get the first address record for this site
        Dim sqlr, sqla As String
        Dim dsr, dsa As New DataSet

        Try ' to get the first address record
            ' 
            sqlr = "select top(1) relID2 from Relation" & _
                " where relType = 'UA' " & _
                " and relID1 = " & id & _
                " order by relID asc"

            dsr = clsGeneral.QueryDB(sqlr)
            If dsr.Tables(0).Rows.Count > 0 Then
                sqla = "select * from [Address] where adID = " & dsr.Tables(0).Rows(0).Item("relID2")
                dsa = clsGeneral.QueryDB(sqla)
                If dsa.Tables(0).Rows.Count > 0 Then
                    With dsa.Tables(0).Rows(0)
                        oAddr.adAddr1 = .Item("adAddr1")
                        oAddr.adAddr2 = .Item("adAddr2")
                        oAddr.adCity = .Item("adCity")
                        oAddr.adSt = .Item("adSt")
                        oAddr.adZip = .Item("adZip")
                        oAddr.adPh1 = .Item("adPh1")
                        oAddr.adEmail = .Item("adEmail")
                    End With
                End If
            End If
        Catch
            ' error 
            Return False
        End Try

        Return True

    End Function
    Public Function updUser(ByVal id As Long) As Boolean
        Dim strUSQL As String
        Dim rec As Integer

        Try 'to update
            strUSQL = "update  [WebUser] " & _
                    " set " & _
                    " [usrFName] = '" & clsGeneral.IsNull(oElements.usrFName) & "' " & _
                    ",[usrLName] = '" & clsGeneral.IsNull(oElements.usrLName) & "' " & _
                    ",[usrEmail] = '" & clsGeneral.IsNull(oElements.usrEmail) & "' " & _
                    ",[usrPwd] = '" & clsGeneral.IsNull(oElements.usrPwd) & "' " & _
                    ",[usrDefaultCustID] = " & clsGeneral.IsZed(oElements.usrDefaultCustID) & " " & _
                    ",[usrRole] = '" & clsGeneral.IsNull(oElements.usrRole) & "' " & _
                    ",[usrStatus] = '" & clsGeneral.IsNull(oElements.usrStatus) & "' " & _
                    ",[usrDateLastModified] = '" & DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & "' " & _
                    ",[usrModFlag] = 'U' " & _
                    " where usrID = " & id

            rec = clsGeneral.ActionQuery(strUSQL, False)
            If rec <= 0 Then
                ' error adding rec
                Return False
            Else
                ' update the address for this user
                Dim strASQL As String
                Try 'to update
                    strASQL = "update  [Address] " & _
                            " set " & _
                            " [adAddr1] = '" & clsGeneral.IsNull(oAddr.adAddr1) & "' " & _
                            ",[adAddr2] = '" & clsGeneral.IsNull(oAddr.adAddr2) & "' " & _
                            ",[adCity] = '" & clsGeneral.IsNull(oAddr.adCity) & "' " & _
                            ",[adST] = '" & clsGeneral.IsNull(oAddr.adSt) & "' " & _
                            ",[adZip] = '" & clsGeneral.IsNull(oAddr.adZip) & "' " & _
                            ",[adPh1] = '" & clsGeneral.IsNull(oAddr.adPh1) & "' " & _
                            ",[adEmail] = '" & clsGeneral.IsNull(oAddr.adEmail) & "' " & _
                            " where adID in " & _
                            " (select relID2 from relation where relType = 'UA' and relid1 = " & id & ")"

                    rec = clsGeneral.ActionQuery(strASQL, False)
                    If rec <= 0 Then
                        ' error adding rec
                        Return False
                    Else
                        Return True
                    End If
                Catch
                    ' error
                    Return False
                End Try

            End If

        Catch ex As Exception
            Return False
        End Try
    End Function

End Class

    
