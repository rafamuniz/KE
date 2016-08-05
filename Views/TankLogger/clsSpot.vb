Imports System.Data
Imports System.Net

Public Class clsSpot
    Private dsAssets As DataSet

    Public Class clsSpotAsset
        Public lID As Long
        Public sID As String
        Public sName As String
        Public dtDateTime As Date
        Public sMessageType As String
        Public fLatitude As Single
        Public fLongitude As Single
        Public sBattery As String
        Public sHidden As String
        Public sModel As String
    End Class
    Public ReadOnly Property AssetsDataset As DataSet
        Get
            If GetLatestLocationOfAssets() Then
                Return dsAssets
            Else
                Return Nothing
            End If

        End Get

    End Property

    Public ReadOnly Property GetCurrentLocation(sID As String) As clsSpotAsset
        Get
            Dim oRow As DataRow
            Dim oAsset As New clsSpotAsset

            Try
                If GetLatestLocationOfAssets() Then
                    For Each oRow In dsAssets.Tables("message").Rows
                        If oRow.Item("messengerId") = sID Then
                            ' found asset - populate class
                            With oRow
                                oAsset.lID = oRow("id")
                                oAsset.sID = oRow("messengerId")
                                oAsset.sName = oRow("messengerName")
                                oAsset.dtDateTime = oRow("datetime")
                                oAsset.sMessageType = oRow("messageType")
                                oAsset.fLatitude = oRow("latitude")
                                oAsset.fLongitude = oRow("longitude")
                                oAsset.sBattery = oRow("batteryState")
                                oAsset.sHidden = oRow("hidden")
                                oAsset.sModel = oRow("modelId")
                            End With

                            Exit For
                        End If
                    Next
                End If

                Return oAsset
            Catch ex As Exception
                Return oAsset
            End Try
        End Get
    End Property

    Private Function GetLatestLocationOfAssets() As Boolean

        Dim oRequest As HttpWebRequest
        Dim oResponse As HttpWebResponse = Nothing

        Try

            ' Create the web request  
            oRequest = DirectCast(WebRequest.Create(clsGeneral.GetLookup(100, 1)), HttpWebRequest)

            ' Get response  
            oResponse = DirectCast(oRequest.GetResponse(), HttpWebResponse)

            ' Load data into a dataset  
            dsAssets = New DataSet()
            dsAssets.ReadXml(oResponse.GetResponseStream())

            Return True

        Catch ex As Exception
            Return False

        Finally
            If Not oResponse Is Nothing Then oResponse.Close()
        End Try

    End Function
End Class
