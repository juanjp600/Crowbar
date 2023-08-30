Public Module ResourceStrings
    Public Enum Entry
        ErrorMessageSDKMissingCause
        Decompile_DebugMdlFileNameSuffix
        Decompile_DebugVtxFileNameSuffix
        Decompile_DebugPhyFileNameSuffix
        Decompile_DebugSequenceGroupMDLFileNameSuffix
        Decompile_DebugTextureMDLFileNameSuffix
        Decompile_DebugAniFileNameSuffix
        Decompile_DebugVvdFileNameSuffix
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