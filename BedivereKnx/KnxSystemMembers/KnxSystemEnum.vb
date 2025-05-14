
Public Enum KnxGroupType As Integer
    Switch = 1
    Dimming
    Curtain
    Value
    EnableCtl
    Scene
End Enum

'Public Enum GroupValueType As Integer
'    Bool = 10
'    [Byte] = 20
'    BytePercent = 21
'    ByteArray = 30
'End Enum

Public Enum KnxObjectPart As Integer
    None
    SwitchControl
    SwitchFeedback
    ValueControl
    ValueFeedback
    DimmingControl
    SceneControl
End Enum

'Public Enum KnxObjectPart0 As Integer
'    Switch = 1
'    Value = 2
'End Enum

'Public Enum KnxObjectPartPoint As Integer
'    Control = 0
'    Feedback = 1
'End Enum

Public Enum KnxScheduleTimerState As Integer
    Stoped = 0
    Starting = 2
    Running = 1
End Enum

Public Enum KnxMessageType As Integer
    System = 0
    FromBus = 1
    ToBus = 2
End Enum
