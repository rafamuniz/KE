Imports System
Imports System.IO
Imports System.Data.SqlClient
Imports System.Data
Imports System.Data.SqlTypes

Public Class clsSite
    Public oElements As New uElements
    Public oAddr As New uAddress
    Public Structure uElements
        Dim siteID As Long
        Dim siteCustID As Integer
        Dim siteName As String
        Dim siteCode As String
        Dim siteContact As String
        Dim siteStatus As String
        Dim siteSPOT_ID As String
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
    Public Function GetUserSites(ByVal uid As Integer) As DataSet
        Dim sql As String
        Dim ds As New DataSet

        Try
            ' 
            sql = "select * from " & _
                "[Site] s join [relation] r on s.stID = r.relID2 and r.relType = 'US' and r.relID1 = " & uid

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
    Public Function OnSite() As Boolean
        Dim sitemode As String = ConfigurationManager.AppSettings("sitemode")
        If sitemode = "Y" Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Function AddSite() As Boolean
        ' add a Site Record and retain the record ID
        Dim strSQL As String

        Try 'to add this record to the relations table
            With oElements
                strSQL = "INSERT INTO [Site] " & _
                   "([stSiteID]" & _
                   ",[stCustID]" & _
                   ",[stName]" & _
                   ",[stSiteContact]" & _
                   ",[stStatus], stSPOT_ID" & _
                ") VALUES (" & _
                    "  '" & .siteCode & "' " & _
                    ",  " & .siteCustID & "  " & _
                    ", '" & .siteName & "' " & _
                    ", '" & .siteContact & "' " & _
                    ", '" & .siteStatus & "' " & _
                    ", '" & .siteSPOT_ID & "' " & _
                    ")"

                .siteID = clsGeneral.ActionQuery(strSQL, True)
                If .siteID <= 0 Then
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
        oRelation.oElements.relType = "SA"
        oRelation.oElements.relID1 = oElements.siteID
        oRelation.oElements.relID2 = oAddress.oElements.adID

        If Not oRelation.addRelation() Then
            ' error
            Return False
        End If

        Return True
    End Function
    Public Function getSites(ByVal id As Long) As DataSet
        Dim sql As String
        Dim ds As New DataSet

        Try
            ' 
            sql = "select * from site " & _
                "where stCustID = " & id

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
    Public Function getSite(ByVal id As Long) As Boolean
        Dim sql As String
        Dim ds As New DataSet

        Try ' to get the site record
            ' 
            sql = "select * from site" & _
                " where stid = " & id

            ds = clsGeneral.QueryDB(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                Try
                    With ds.Tables(0).Rows(0)
                        oElements.siteCode = clsGeneral.IsNull(.Item("stSiteID"))
                        oElements.siteCustID = clsGeneral.IsZed(.Item("stCustID"))
                        oElements.siteName = clsGeneral.IsNull(.Item("stName"))
                        oElements.siteContact = clsGeneral.IsNull(.Item("stSiteContact"))
                        oElements.siteStatus = clsGeneral.IsNull(.Item("stStatus"))
                        oElements.siteSPOT_ID = clsGeneral.IsNull(.Item("stSPOT_ID"))
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
                " where relType = 'SA' " & _
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
    Public Function updSite(ByVal id As Long) As Boolean

    End Function
End Class

