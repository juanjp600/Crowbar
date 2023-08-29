Public Module FileManagerEvents

    Public Sub ParsePath(ByVal sender As Object, ByVal e As ConvertEventArgs)
        If e.DesiredType IsNot GetType(String) Then
            Exit Sub
        End If
        If CStr(e.Value) <> "" Then
            e.Value = FileManager.GetCleanPath(CStr(e.Value), True)
        End If
    End Sub

    Public Sub ParsePathFileName(ByVal sender As Object, ByVal e As ConvertEventArgs)
        If e.DesiredType IsNot GetType(String) Then
            Exit Sub
        End If
        If CStr(e.Value) <> "" Then
            e.Value = FileManager.GetCleanPathFileName(CStr(e.Value), True)
        End If
    End Sub

End Module