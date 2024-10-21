Public Class MsgShow

    Public Shared Function Info(text As String, Optional caption As String = "Prompt") As DialogResult
        Return MessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Function

    Public Shared Function Warn(text As String, Optional caption As String = "Warning") As DialogResult
        Return MessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
    End Function

    Public Shared Function Err(text As String, Optional caption As String = "Error") As DialogResult
        Return MessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Function

End Class
