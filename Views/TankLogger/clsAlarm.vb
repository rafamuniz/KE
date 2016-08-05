Imports System
Imports System.IO
Imports System.Data.SqlClient
Imports System.Data
Imports System.Data.SqlTypes

Public Class clsAlarm
    Public Elements As New uElements
    Public Structure uElements
        Dim slID As Integer
        Dim slSensorID As String
        Dim slDescription As String
        Dim slSensType As String
        Dim slSensValue As Integer
        Dim slSensEmail As String
        Dim slActive As String
        Dim slModFlag As String
        Dim slGUID As String
        Dim slLastUpdate As DateTime
    End Structure
    Public Function GetLimits(ByVal id As String) As DataSet
        Dim sql As String = ""
        Dim ds As New DataSet

        Try
            ' get all sites for this user
            sql = "select * from SensorLimits " & _
                "where slSensorID = " & id & _
                " and slActive = 'A'"

            ds = clsGeneral.QueryDB(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                Return ds
            Else
                Return New DataSet
            End If

        Catch ex As Exception
            Return New DataSet
        End Try

    End Function
    Public Function DeleteLimit(ByVal id As Integer) As Boolean
        ' [spx_DelSensorLimit]
        Try

            Dim db As SqlConnection
            db = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnString").ConnectionString)
            db.Open()

            Dim myCommand As New SqlCommand("spx_DelSensorLimit", db)
            myCommand.CommandType = CommandType.StoredProcedure

            'Create a SqlParameter object to hold the output parameter value
            Dim recid As New SqlParameter("@recid", SqlDbType.Int)
            myCommand.Parameters.Add(recid)
            recid.Value = id

            'Create a SqlParameter object to hold the output parameter value
            Dim retValParam As New SqlParameter("@recs", SqlDbType.Int)
            retValParam.Direction = ParameterDirection.Output
            myCommand.Parameters.Add(retValParam)

            'Call the sproc 
            Dim reader As SqlDataReader = myCommand.ExecuteReader()

            Return retValParam.Value

        Catch ex As Exception
            Return -1
        End Try

    End Function
    Public Function UpdSensorLimit() As Boolean
        ' [spx_AddUpdSensorLimit]
        Try

            Dim db As SqlConnection
            db = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnString").ConnectionString)
            db.Open()

            Dim myCommand As New SqlCommand("spx_AddUpdSensorLimit", db)
            myCommand.CommandType = CommandType.StoredProcedure

            'Create SqlParameters 
            Dim param_slID As New SqlParameter("@slID", SqlDbType.Int)
            myCommand.Parameters.Add(param_slID)
            param_slID.Value = Elements.slID

            Dim param_slSensorID As New SqlParameter("@slSensorID", SqlDbType.VarChar)
            myCommand.Parameters.Add(param_slSensorID)
            param_slSensorID.Value = Elements.slSensorID

            Dim param_slDescription As New SqlParameter("@slDescription", SqlDbType.VarChar)
            myCommand.Parameters.Add(param_slDescription)
            param_slDescription.Value = Elements.slDescription

            Dim param_slSensType As New SqlParameter("@slSensType", SqlDbType.VarChar)
            myCommand.Parameters.Add(param_slSensType)
            param_slSensType.Value = Elements.slSensType

            Dim param_slSensValue As New SqlParameter("@slSensValue", SqlDbType.Int)
            myCommand.Parameters.Add(param_slSensValue)
            param_slSensValue.Value = Elements.slSensValue

            Dim param_slSensEmail As New SqlParameter("@slSensEmail", SqlDbType.VarChar)
            myCommand.Parameters.Add(param_slSensEmail)
            param_slSensEmail.Value = Elements.slSensEmail

            Dim param_slActive As New SqlParameter("@slActive", SqlDbType.Char, 1)
            myCommand.Parameters.Add(param_slActive)
            param_slActive.Value = Elements.slActive

            Dim param_slGUID As New SqlParameter("@slGUID", SqlDbType.VarChar)
            myCommand.Parameters.Add(param_slGUID)
            param_slGUID.Value = Elements.slGUID

            Dim param_slModFlag As New SqlParameter("@slModFlag", SqlDbType.Char, 1)
            myCommand.Parameters.Add(param_slModFlag)
            param_slModFlag.Value = Elements.slModFlag

            Dim param_slLastUpdate As New SqlParameter("@slLastUpdate", SqlDbType.DateTime)
            myCommand.Parameters.Add(param_slLastUpdate)
            param_slLastUpdate.Value = Elements.slLastUpdate

            'Create a SqlParameter object to hold the output parameter value
            Dim retValParam As New SqlParameter("@recs", SqlDbType.Int)
            retValParam.Direction = ParameterDirection.Output
            myCommand.Parameters.Add(retValParam)

            'Call the sproc 
            Dim reader As SqlDataReader = myCommand.ExecuteReader()

            Return retValParam.Value

        Catch ex As Exception
            Return -1
        End Try


    End Function
End Class
