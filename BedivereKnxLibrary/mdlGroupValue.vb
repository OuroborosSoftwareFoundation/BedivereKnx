Imports Knx.Falcon
Imports Knx.Falcon.ApplicationData.DatapointTypes
Imports Knx.Falcon.ApplicationData.MasterData

Module mdlGroupValue

    Public Function GroupCtlType_GroupValue(Str As String, CtlType As GroupValueType) As GroupValue
        If String.IsNullOrEmpty(Str) Then
            Throw New ArgumentNullException("Group value string is null.")
            Return Nothing
        End If
        Dim val As GroupValue
        Select Case CtlType
            Case GroupValueType.Bool
                val = New GroupValue(Convert.ToByte(Str), 1)
            Case GroupValueType.Byte
                val = New GroupValue(Convert.ToByte(Str))
            Case GroupValueType.BytePercent
                val = New GroupValue(Convert.ToByte(Str * 2.55))
            Case GroupValueType.ByteArray '暂不支持字节数组
                Throw New ArgumentOutOfRangeException($"Unsupported GroupValueType in this version: '{CtlType}'.")
                Return Nothing
            Case Else
                Throw New ArgumentException($"Wrong or unsupported GroupValueType: '{CtlType}'.")
                Return Nothing
        End Select
        Return val
    End Function

    Private Sub test()

        For Each dpt As DatapointType In DptFactory.Default.AllDatapointTypes
            Debug.WriteLine($"{dpt.MainTypeNumber}.xxx_{dpt.Name}__{dpt.Text}")
            For Each s As DatapointSubtype In dpt.SubTypes
                Debug.WriteLine($"{dpt.MainTypeNumber}.{s.SubTypeNumber.ToString.PadLeft(3, "0"c)}_{s.Name}__{s.Text}")
            Next
        Next

    End Sub

End Module
