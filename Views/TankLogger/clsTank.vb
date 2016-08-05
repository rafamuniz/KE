Imports System
Imports System.IO
Imports System.Data.SqlClient
Imports System.Data
Imports System.Data.SqlTypes

Public Class clsTank
    Public Elements As New uElements
    Public MdlElem As New uMdlElem
    Public Structure uElements
        Dim DefID As Long
        Dim SensorID As String
        Dim CustID As Long
        Dim SiteID As Long
        Dim Name As String
        Dim Desc As String
        Dim Lat As Double
        Dim Lng As Double
        Dim Active As String
        Dim Model As Integer
        Dim Addr1 As String
        Dim Addr2 As String
        Dim City As String
        Dim ST As String
        Dim Zip As String
        Dim SiteContact As String
        Dim SitePhone As String
        Dim Length As Integer
        Dim Width As Integer
        Dim Height As Integer
        Dim Dim4 As Integer
        Dim Dim5 As Integer
        Dim TZOffset As Integer
        Dim Timeout As Integer
        Dim MaxDist As Integer
        Dim MinDist As Integer
        Dim LastUpdate As DateTime
        Dim ModFlag As String
        Dim GUID As String
        Dim sSPOT_ID As String
    End Structure
    Public Structure uMdlElem
        Dim ID As Integer
        Dim Desc As String
        Dim Notes As String
        Dim Geometry As String
        Dim ImageDim As String
        Dim HtDesc As String
        Dim DftHt As Integer
        Dim LenDesc As String
        Dim DftLen As Integer
        Dim WidthDesc As String
        Dim DftWidth As Integer
        Dim Dim4Desc As String
        Dim DftDim4 As Integer
        Dim Dim5Desc As String
        Dim DftDim5 As Integer
        Dim FormulaDesc As String
        Dim FormulaMap As String
        Dim ImageNoDim As String
    End Structure

    Public oSPOT_Asset As clsSpot.clsSpotAsset

    Public Function GetTankModel(ByVal id As Integer) As Boolean
        Dim sql As String
        Dim ds As New DataSet

        Try
            ' 
            sql = "select * from tankmodel" & _
                " where tmid = " & id

            ds = clsGeneral.QueryDB(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                Try
                    With ds.Tables(0).Rows(0)
                        MdlElem.ID = clsGeneral.IsNull(.Item("tmID"))
                        MdlElem.Desc = clsGeneral.IsNull(.Item("tmDesc"))
                        MdlElem.Notes = clsGeneral.IsNull(.Item("tmNotes"))
                        MdlElem.Geometry = clsGeneral.IsNull(.Item("tmGeometry"))
                        MdlElem.ImageDim = clsGeneral.IsNull(.Item("tmImageDim"))
                        MdlElem.HtDesc = clsGeneral.IsNull(.Item("tmHtDesc"))
                        MdlElem.DftHt = clsGeneral.IsZed(.Item("tmDftHt"))
                        MdlElem.LenDesc = clsGeneral.IsNull(.Item("tmLenDesc"))
                        MdlElem.DftLen = clsGeneral.IsZed(.Item("tmDftLen"))
                        MdlElem.WidthDesc = clsGeneral.IsNull(.Item("tmWidthDesc"))
                        MdlElem.DftWidth = clsGeneral.IsZed(.Item("tmDftWidth"))
                        MdlElem.Dim4Desc = clsGeneral.IsNull(.Item("tmDim4Desc"))
                        MdlElem.DftDim4 = clsGeneral.IsZed(.Item("tmDftDim4"))
                        MdlElem.Dim5Desc = clsGeneral.IsNull(.Item("tmDim5Desc"))
                        MdlElem.DftDim5 = clsGeneral.IsZed(.Item("tmDftDim5"))
                        MdlElem.FormulaDesc = clsGeneral.IsNull(.Item("tmFormulaDesc"))
                        MdlElem.FormulaMap = clsGeneral.IsNull(.Item("tmFormulaMap"))
                        MdlElem.ImageNoDim = clsGeneral.IsNull(.Item("tmImageNoDim"))
                    End With
                Catch ex As Exception
                    ' error

                End Try


                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function GetTank(ByVal id As Integer) As Boolean
        Dim sql As String
        Dim ds As New DataSet
        Dim oSPOT As New clsSpot

        Try
            ' 
            sql = "select * from tankdef" & _
                " where tdefid = " & id

            ds = clsGeneral.QueryDB(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                Try
                    With ds.Tables(0).Rows(0)
                        Elements.DefID = clsGeneral.IsZed(.Item("tdefID"))
                        Elements.SensorID = clsGeneral.IsNull(.Item("tdefSensorID"))
                        Elements.CustID = clsGeneral.IsZed(.Item("tdefCustID"))
                        Elements.SiteID = clsGeneral.IsZed(.Item("tdefSiteID"))
                        Elements.Name = clsGeneral.IsNull(.Item("tdefName"))
                        Elements.Desc = clsGeneral.IsNull(.Item("tdefDesc"))
                        Elements.Lat = clsGeneral.IsNull(.Item("tdefLat"))
                        Elements.Lng = clsGeneral.IsNull(.Item("tdefLng"))
                        Elements.Active = clsGeneral.IsNull(.Item("tdefActive"))
                        Elements.Model = clsGeneral.IsZed(.Item("tdefModel"))
                        Elements.Addr1 = clsGeneral.IsNull(.Item("tdefAddr1"))
                        Elements.Addr2 = clsGeneral.IsNull(.Item("tdefAddr2"))
                        Elements.City = clsGeneral.IsNull(.Item("tdefCity"))
                        Elements.ST = clsGeneral.IsNull(.Item("tdefST"))
                        Elements.Zip = clsGeneral.IsNull(.Item("tdefZip"))
                        Elements.SiteContact = clsGeneral.IsNull(.Item("tdefSiteContact"))
                        Elements.SitePhone = clsGeneral.IsNull(.Item("tdefSitePhone"))
                        Elements.Height = clsGeneral.IsZed(.Item("tdefHeight"))
                        Elements.Length = clsGeneral.IsZed(.Item("tdefLength"))
                        Elements.Width = clsGeneral.IsZed(.Item("tdefWidth"))
                        Elements.Dim4 = clsGeneral.IsZed(.Item("tdefDim4"))
                        Elements.Dim5 = clsGeneral.IsZed(.Item("tdefDim5"))
                        Elements.TZOffset = clsGeneral.IsZed(.Item("tdefTZOffset"))
                        Elements.Timeout = clsGeneral.IsZed(.Item("tdefTimeout"))
                        Elements.MaxDist = clsGeneral.IsZed(.Item("tdefMaxDist"))
                        Elements.MinDist = clsGeneral.IsZed(.Item("tdefMinDist"))
                        Elements.LastUpdate = clsGeneral.IsADate(.Item("tdefLastUpdate"))
                        Elements.GUID = clsGeneral.IsNull(.Item("tdefGUID"))
                        Elements.sSPOT_ID = clsGeneral.IsNull(.Item("tdSPOT_ID"))

                        ' lets check for missing spot - if missing then attempt to get it from site
                        If clsGeneral.IsNull(Elements.sSPOT_ID) = "" Then
                            sql = "select stSPOT_ID from site where stId = '" & Elements.SiteID & "'"
                            ds = clsGeneral.QueryDB(sql)
                            If ds.Tables(0).Rows.Count > 0 Then
                                Elements.sSPOT_ID = clsGeneral.IsNull(ds.Tables(0).Rows(0).Item("stSPOT_ID"))
                            End If

                        End If

                        oSPOT_Asset = oSPOT.GetCurrentLocation(Elements.sSPOT_ID)

                    End With
                Catch ex As Exception
                    ' error
                    'Stop
                End Try

                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function GetTanks(ByVal id As Integer) As DataSet
        Dim sql As String
        Dim ds As New DataSet

        Try
            ' 
            sql = "select * from tankDef " & _
                "where tdefCustID = " & id & _
                " and tdefActive <> 'D'"

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
    Public Function GetSiteTanks(ByVal id As Integer, Optional ByVal mode As String = "S") As DataSet
        Dim sql As String = ""
        Dim ds As New DataSet

        Try
            Select Case mode
                Case "S"
                    ' get tanks for selected site
                    ' sql = "select * from tankDef " & _
                    sql = "select * from vLatestSamples " & _
                        "where tdefSiteID = " & id & _
                        " and tdefActive <> 'D'"
                Case "U"
                    ' get all sites for this user
                    sql = "select * from vLatestSamples " & _
                        "where tdefSiteID in (select relid2 from [relation]  where reltype = 'US' and relid1 = " & id & ")" & _
                        " and tdefActive <> 'D'"
                Case Else
            End Select
            If sql <> "" Then
                sql = sql + " order by tdefID"
            End If


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
    Public Function UpdTankCoord(ByVal id As Integer, ByVal lat As Double, ByVal lon As Double) As Boolean
        Dim strSQL As String
        Dim rec As Integer

        Dim latHH, lonHH, latMM, lonMM As Double

        latHH = CInt(lat / 100)
        latMM = (lat - (latHH * 100)) / 60

        lonHH = CInt(lon / 100)
        lonMM = (lon - (lonHH * 100)) / 60

        Try 'to add this record to the log table
            strSQL = "update  [tankDef] " & _
                    " set " & _
                    " tdefLat = '" & (latHH + latMM).ToString & "' " & _
                    ",tdefLng = '" & (lonHH + lonMM).ToString & "' " & _
                    " where tdefID = " & id
            rec = clsGeneral.ActionQuery(strSQL, False)
            If rec <= 0 Then
                ' error adding rec
                Return False
            Else
                Return True
            End If

        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function GetModels() As DataSet
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
    Public Function UpdTank(ByVal oElements As uElements) As Boolean
        Dim strSQL As String
        Dim rec As Integer

        Try 'to update
            strSQL = "update  [tankDef] " & _
                    " set " & _
                    " [tdefSensorID] = '" & clsGeneral.IsNull(Elements.SensorID) & "' " & _
                    ",[tdefCustID] =  " & clsGeneral.IsZed(Elements.CustID) & _
                    ",[tdefSiteID] =  " & clsGeneral.IsZed(Elements.SiteID) & _
                    ",[tdefName] = '" & clsGeneral.IsNull(Elements.Name) & "' " & _
                    ",[tdefDesc] = '" & clsGeneral.IsNull(Elements.Desc) & "' " & _
                    ",[tdefLat] = '" & clsGeneral.IsZed(Elements.Lat) & "' " & _
                    ",[tdefLng] = '" & clsGeneral.IsZed(Elements.Lng) & "' " & _
                    ",[tdefActive] = '" & clsGeneral.IsNull(Elements.Active) & "' " & _
                    ",[tdefModel] = '" & clsGeneral.IsZed(Elements.Model) & "' " & _
                    ",[tdefAddr1] = '" & clsGeneral.IsNull(Elements.Addr1) & "' " & _
                    ",[tdefAddr2] = '" & clsGeneral.IsNull(Elements.Addr2) & "' " & _
                    ",[tdefCity] = '" & clsGeneral.IsNull(Elements.City) & "' " & _
                    ",[tdefST] = '" & clsGeneral.IsNull(Elements.ST) & "' " & _
                    ",[tdefZip] = '" & clsGeneral.IsNull(Elements.Zip) & "' " & _
                    ",[tdefSiteContact] = '" & clsGeneral.IsNull(Elements.SiteContact) & "' " & _
                    ",[tdefSitePhone] = '" & clsGeneral.IsNull(Elements.SitePhone) & "' " & _
                    ",[tdefHeight] =  " & clsGeneral.IsZed(Elements.Height) & _
                    ",[tdefLength] =  " & clsGeneral.IsZed(Elements.Length) & _
                    ",[tdefWidth] =  " & clsGeneral.IsZed(Elements.Width) & _
                    ",[tdefDim4] =  " & clsGeneral.IsZed(Elements.Dim4) & _
                    ",[tdefDim5] =  " & clsGeneral.IsZed(Elements.Dim5) & _
                    ",[tdefTZOffset] =  " & clsGeneral.IsZed(Elements.TZOffset) & _
                    ",[tdefTimeout] =  " & clsGeneral.IsZed(Elements.Timeout) & _
                    ",[tdefMinDist] =  " & clsGeneral.IsZed(Elements.MinDist) & _
                    ",[tdefMaxDist] =  " & clsGeneral.IsZed(Elements.MaxDist) & _
                    ",[tdefLastUpdate] = '" & DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & "' " & _
                    ",[tdefModFlag] = 'U' " & _
                    ",[tdSPOT_ID] = '" & Elements.sSPOT_ID & "' " & _
                    " where tdefID = " & Elements.DefID

            rec = clsGeneral.ActionQuery(strSQL, False)
            If rec <= 0 Then
                ' error adding rec
                Return False
            Else
                Return True
            End If

        Catch ex As Exception
            Return False
        End Try

    End Function
    Public Function AddTank(ByVal oElements As uElements) As Boolean
        Dim strSQL As String

        Try 'to add this record to the log table
            With oElements
                strSQL = "INSERT INTO [tankDef] " & _
                   "([tdefSensorID]" & _
                   ",[tdefSiteID]" & _
                   ",[tdefCustID]" & _
                   ",[tdefName]" & _
                   ",[tdefDesc]" & _
                   ",[tdefLat]" & _
                   ",[tdefLng]" & _
                   ",[tdefActive]" & _
                   ",[tdefModel]" & _
                   ",[tdefAddr1]" & _
                   ",[tdefAddr2]" & _
                   ",[tdefCity]" & _
                   ",[tdefST]" & _
                   ",[tdefZip]" & _
                   ",[tdefSiteContact]" & _
                   ",[tdefSitePhone]" & _
                   ",[tdefHeight]" & _
                   ",[tdefLength]" & _
                   ",[tdefWidth]" & _
                   ",[tdefDim4]" & _
                   ",[tdefDim5]" & _
                   ",[tdefTZOffset]" & _
                   ",[tdefMinDist]" & _
                   ",[tdefMaxDist]" & _
                   ",[tdefLastUpdate]" & _
                   ",[tdefModFlag]" & _
                   ",[tdefGUID], tdSPOT_ID" & _
                ") VALUES (" & _
                    "  '" & .SensorID & "' " & _
                    ",  " & .SiteID & " " & _
                    ",  " & .CustID & "  " & _
                    ", '" & .Name & "' " & _
                    ", '" & .Desc & "' " & _
                    ",  " & .Lat & "  " & _
                    ",  " & .Lng & "  " & _
                    ", '" & .Active & "' " & _
                    ",  " & .Model & "  " & _
                    ", '" & .Addr1 & "' " & _
                    ", '" & .Addr2 & "' " & _
                    ", '" & .City & "' " & _
                    ", '" & .ST & "' " & _
                    ", '" & .Zip & "' " & _
                    ", '" & .SiteContact & "' " & _
                    ", '" & .SitePhone & "' " & _
                    ",  " & .Height & "  " & _
                    ",  " & .Length & "  " & _
                    ",  " & .Width & "  " & _
                    ",  " & .Dim4 & "  " & _
                    ",  " & .Dim5 & "  " & _
                    ",  " & .TZOffset & "  " & _
                    ",  " & .MinDist & "  " & _
                    ",  " & .MaxDist & "  " & _
                    ", '" & DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & "' " & _
                    ", 'U' " & _
                    ", newid(), '" & .sSPOT_ID & _
                    "')"

                .DefID = clsGeneral.ActionQuery(strSQL, True)
                If .DefID <= 0 Then
                    ' error adding rec
                Else
                    Return True
                End If
            End With
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function DelTank(ByVal ID As Long) As Boolean
        Dim strSQL As String

        Try ' to delete 

            strSQL = "Delete [tankdef]  " & _
                    "where tdefID = " & ID

            If clsGeneral.ActionQuery(strSQL) <= 0 Then
                ' error
                Return False
            Else
                Return True
            End If

        Catch ex As Exception
            ' undetermined error
            Return False
        End Try
    End Function
    Public Function GetStateImage(ByVal tankid As Integer, ByVal pctfull As Integer) As String
        ' returns the path and filename of an image corresponding to the percentage full
        Dim sql As String
        Dim ds As New DataSet

        Try
            ' get the base image name
            sql = "select tmid, '/images/'+tmimagedim as tmimage from tankmodel " & _
                    " where tmid = (select tdefModel from tankdef where tdefID = " & tankid & ")"

            ds = clsGeneral.QueryDB(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                If pctfull >= 0 Then
                    ' return the image representing the nearest 10% volume
                    Return ds.Tables(0).Rows(0).Item("tmimage").Replace(".png", "_" & pctfull.ToString & ".png")
                Else
                    ' return the image representing no data
                    Return ds.Tables(0).Rows(0).Item("tmimage").Replace(".png", "_NoData.png")
                End If
            Else
                Return Nothing
            End If

        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Function PreDeleteTank(ByVal ID As Long) As Boolean
        Dim strSQL As String

        Try ' to delete 

            strSQL = "update [tankdef]  " & _
                "set tdefActive = 'D' " & _
                    "where tdefID = " & ID

            If clsGeneral.ActionQuery(strSQL) <= 0 Then
                ' error
                Return False
            Else
                Return True
            End If

        Catch ex As Exception
            ' undetermined error
            Return False
        End Try
    End Function
    Public Function GetAlarmCount(SensorID As Long) As Integer
        Dim sql As String
        Dim ds As New DataSet

        Try
            ' 
            sql = "select dbo.fx_GetAlarmCount(" + SensorID.ToString + ") alarms"

            ds = clsGeneral.QueryDB(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                Try
                    Return clsGeneral.IsZed(ds.Tables(0).Rows(0).Item("alarms"))
                Catch ex As Exception
                    ' error
                    Return -1
                End Try
            Else
                Return -1
            End If
        Catch ex As Exception
            Return -1
        End Try
    End Function
End Class
