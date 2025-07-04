Imports Knx.Falcon
Imports Knx.Falcon.ApplicationData.DatapointTypes
Imports Ouroboros.Hmi.Library.Mapping

Public Class KnxHmiMapping : Inherits HmiMappingBase(Of GroupValue)

    'Private DPT As DptBase

    Public Sub New(values As GroupValue(), changeType As HmiValueChangeType)
        MyBase.New(values, changeType)
    End Sub

    ''' <summary>
    ''' 这种方式新建的对象只有RawValues属性被赋值，Values属性为空
    ''' </summary>
    ''' <param name="valsString"></param>
    Public Sub New(valsString As String)
        MyBase.New(valsString)
    End Sub

    Public Sub New(valsString As String, dpt As DptBase)
        MyBase.New(valsString)
        '此时基类的RawValues被设置值
        Dim valList As New List(Of GroupValue)
        For Each valStr As String In RawValues
            valList.Add(dpt.ToGroupValue(Convert.ToDecimal(valStr)))
        Next
        Values = valList.ToArray
    End Sub

    '''' <summary>
    '''' 字符串转GroupValue的方法
    '''' </summary>
    '''' <param name="str"></param>
    '''' <returns></returns>
    'Protected Overrides Function StringToTValue(str As String) As GroupValue
    '    Return DPT.ToGroupValue(Convert.ToDecimal(str))
    'End Function

End Class
