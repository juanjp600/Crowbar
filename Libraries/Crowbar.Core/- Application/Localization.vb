Public Module Localization
    Public Enum Entry
        ErrorMessageSDKMissingCause
    End Enum

    Public ReadOnly Values As Dictionary(Of Entry, String) = New Dictionary(Of Entry,String)()

    Public Function GetString(entry as Entry) As String
        Dim foundStr As String
        If Values.TryGetValue(entry, foundStr) Then
            Return foundStr
        End If
        Return Entry.ToString()
    End Function
End Module