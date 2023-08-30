Public Module EnvVars
    Public Sub SetSteamAppId(ByVal appID As UInteger)
        SetSteamAppId(appID.ToString())
    End Sub

    Public Sub SetSteamAppId(ByVal appID_text As String)
        Environment.SetEnvironmentVariable("SteamAppId", appID_text)
        Environment.SetEnvironmentVariable("SteamGameId", appID_text)
    End Sub
End Module