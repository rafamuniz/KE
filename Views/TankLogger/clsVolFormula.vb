Public Class clsVolFormula
    Dim oTank As New clsTank
    Dim oLog As New clsLogdata
    Public params As New uParams
    Public Structure uParams

    End Structure
    Public Function CurrStateToTanklist(ByRef dt As DataTable) As Boolean

        Try
            If oTank Is Nothing Then
                oTank = New clsTank
            End If

            ' add calculated fields
            dt.Columns.Add("calcAlarm", GetType(Integer))
            dt.Columns.Add("calcStateImage", GetType(String))
            dt.Columns.Add("calcAlarmBool", GetType(Boolean))

            ' for each record calculate the current state
            For Each tdsr As DataRow In dt.Rows

                ' prepare the default list data
                tdsr.Item("calcAlarm") = oTank.GetAlarmCount(tdsr.Item("tdefID"))
                If tdsr.Item("calcAlarm") > 0 Then
                    tdsr.Item("calcAlarmBool") = True
                Else
                    tdsr.Item("calcAlarmBool") = False
                End If
                tdsr.Item("calcStateImage") = oTank.GetStateImage(tdsr.Item("tdefID"), -1.ToString)

                Dim tenspct As Integer = Math.Round(tdsr.Item("PctFull") / 10, 0) * 10
                tdsr.Item("calcStateImage") = oTank.GetStateImage(tdsr.Item("tdefID"), tenspct.ToString)
            Next

        Catch ex As Exception
            ' error
            Return False
        End Try

        Return True
    End Function
    'Public Function CalcVolume(ByVal TankID As Integer, _
    '                           ByVal headdist As Integer, _
    '                           ByRef PctFull As Integer, _
    '                           ByRef GalRem As Integer) As Integer 'returns total capacity

    '    Return 0

    '    If oTank Is Nothing Then
    '        oTank = New clsTank
    '    End If

    '    Dim cubicunits As Double
    '    Dim remcubicunits As Double
    '    Dim area As Double

    '    ' get this tank's dimensions
    '    If oTank.GetTank(TankID) Then
    '        Dim height As Integer = oTank.Elements.Height
    '        Dim length As Integer = oTank.Elements.Length
    '        Dim width As Integer = oTank.Elements.Width
    '        Dim dim4 As Integer = oTank.Elements.Dim4
    '        Dim dim5 As Integer = oTank.Elements.Dim5

    '        Dim maxdist As Integer = oTank.Elements.MaxDist
    '        Dim mindist As Integer = oTank.Elements.MinDist

    '        ' limit the headdist by max/min for this tank
    '        If headdist > maxdist Then
    '            headdist = maxdist
    '        End If
    '        headdist = headdist - mindist


    '        ' get this tank's geometry
    '        If oTank.GetTankModel(oTank.Elements.Model) Then
    '            Dim geometry As String = oTank.MdlElem.Geometry

    '            Select Case geometry
    '                Case "Elliptical Horizontal"
    '                    If (height > 0 And length > 0 And width > 0) Then
    '                        ' calculate the total volume
    '                        '   calculate the total area of the elliptical end : pi() * height/2 * width/2
    '                        area = Math.PI * (height / 2) * (width / 2)

    '                        ' calculate the total volume : multiply the area by the length
    '                        cubicunits = area * length

    '                        ' calculate the remaining volume
    '                        remcubicunits = VolHorzEll(length, width, height, headdist)
    '                    Else
    '                        Return 0

    '                    End If

    '                Case "Cylinder Horizontal"
    '                    If (height > 0 And length > 0) Then
    '                        ' calculate the total volume : A=pi * r^2
    '                        area = Math.PI * (height / 2) ^ 2

    '                        ' calculate the total volume : multiply the area by the length
    '                        cubicunits = area * length

    '                        ' calculate the remaining volume
    '                        ' make width = height and use ellipse formula
    '                        remcubicunits = VolHorzEll(length, height, height, headdist)
    '                    Else
    '                        Return 0

    '                    End If

    '                Case "Cylinder Vertical"
    '                    If (height > 0 And width > 0) Then
    '                        ' calculate the area of the round end
    '                        ' calculate the total volume : A=pi * r^2
    '                        area = Math.PI * (width / 2) ^ 2

    '                        ' calculate the total volume : multiply the area by the length
    '                        cubicunits = area * height

    '                        ' calculate the remaining volume 
    '                        remcubicunits = area * (height - headdist)
    '                    Else
    '                        Return 0
    '                    End If

    '                Case "Stadium Vertical"
    '                    If (height > 0 And length > 0 And width > 0) Then
    '                        ' calculate the area of the cube section
    '                        Dim area1 As Double = length * width

    '                        ' calculate the area of the round ends
    '                        Dim area2 As Double = Math.PI * (width / 2) ^ 2

    '                        ' multiply by height for total cubic
    '                        cubicunits = height * (area1 + area2)

    '                        ' multiply by (height - headdist) for remaining cubic
    '                        remcubicunits = (height - headdist) * (area1 + area2)
    '                    Else
    '                        Return 0

    '                    End If

    '                Case "Stadium Horizontal"
    '                    If (height > 0 And length > 0 And width > 0) Then
    '                        ' calculate the area of the square section
    '                        Dim area1 As Double = height * dim4

    '                        ' calculate the area of the round section(s)
    '                        Dim area2 As Double = Math.PI * (height / 2) ^ 2

    '                        ' multiply by length for total cubic
    '                        cubicunits = length * (area1 + area2)

    '                        ' calculate the remaining volume for the circlular crossections
    '                        Dim remcubic1 = VolHorzEll(length, height, height, headdist)

    '                        ' calculate the remaining volume for the square crosssection
    '                        Dim remcubic2 = (length * dim4) * (height - headdist)

    '                        ' calculate total remaining
    '                        remcubicunits = remcubic1 + remcubic2
    '                    Else
    '                        Return 0

    '                    End If

    '                Case "Cube Horizontal"
    '                    If (height > 0 And length > 0 And width > 0) Then
    '                        ' calculate the overall cubic dimensions
    '                        cubicunits = length * width * height

    '                        ' calculate the remaining volume
    '                        remcubicunits = (length * width) * (height - headdist)

    '                    End If


    '                Case Else
    '                    ' error
    '            End Select

    '            ' calculate the percentage full
    '            PctFull = CInt(Math.Round(100 * (remcubicunits / cubicunits), 0))

    '            ' convert cubic(inches) to gallons (231 cubic inches per gallon of water
    '            GalRem = CInt(Math.Round(remcubicunits / 231, 0))

    '            Return cubicunits / 231

    '        End If
    '    End If
    'End Function
    'Private Function VolHorzEll(ByVal length As Integer, ByVal width As Integer, ByVal height As Integer, ByVal headdist As Integer) As Double
    '    ' calculate the remaining volume
    '    '   first calculate the area of the partial ellipse
    '    '   area = ((height)(width)/4)[arccos(1 - 2h/(height)) - (1 - 2h/(height))sqrt(4h/(height) - 4h^2/(height)^2)]
    '    '   where h=height-headdist
    '    Dim h As Double = height - headdist
    '    Dim a1 As Double = (height * width) / 4
    '    Dim t1 As Double = Math.Acos(1 - (2 * h) / height)
    '    Dim t2 As Double = (1 - (2 * h) / height)
    '    Dim t3 As Double = Math.Sqrt(((4 * h) / height) - ((4 * h ^ 2) / height ^ 2)) ' error here - sqrt of negative
    '    Return CDbl(length) * (a1 * (t1 - (t2 * t3)))

    'End Function
End Class
