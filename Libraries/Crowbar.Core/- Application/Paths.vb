﻿Imports System.IO

Public Module Paths

    Private Const PreviewsRelativePath As String = "previews"
    Private Const theAppSettingsFileName As String = "Crowbar Settings.xml"

    Public ReadOnly Property AppSettingsFilePath() As String
        Get
            Return Path.Combine(GetCustomDataPath(), theAppSettingsFileName)
        End Get
    End Property

    Private Const theSteamAppIDFileName As String = "steam_appid.txt"
    'Private Const theDataFolderName As String = "Data"

    Public Const AnimsSubFolderName As String = "anims"
    Public Const LogsSubFolderName As String = "logs"
    
    Private Const ErrorFileName As String = "unhandled_exception_error.txt"

    Public ReadOnly Property ErrorPathFileName() As String
        Get
            Return Path.Combine(Paths.GetCustomDataPath(), ErrorFileName)
        End Get
    End Property

    Public Function GetDebugPath(ByVal outputPath As String, ByVal modelName As String) As String
        'Dim logsPath As String

        'logsPath = Path.Combine(outputPath, modelName + "_" + App.LogsSubFolderName)

        'Return logsPath
        Return outputPath
    End Function

    'TODO: [GetCustomDataPath] Have location option where custom data and settings is saved.
    Public Function GetCustomDataPath() As String
        Dim customDataPath As String
        'Dim appDataPath As String

        '' If the settings file exists in the app's Data folder, then load it.
        'appDataPath = Me.GetAppDataPath()
        'If appDataPath <> "" Then
        '	customDataPath = appDataPath
        'Else
        'NOTE: Use "standard Windows location for app data".
        'NOTE: Using Path.Combine in case theStartupFolder is a root folder, like "C:\".
        customDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ZeqMacaw")
        customDataPath += Path.DirectorySeparatorChar
        'customDataPath += "Crowbar"
        customDataPath += AppConstants.EntryAssembly.GetName().Name
        customDataPath += " "
        customDataPath += AppConstants.EntryAssembly.GetName().Version.ToString(2)

        FileManager.CreatePath(customDataPath)
        'End If

        Return customDataPath
    End Function

    Public Function GetPreviewsPath() As String
        Dim customDataPath As String = GetCustomDataPath()
        Dim previewsPath As String = Path.Combine(customDataPath, PreviewsRelativePath)
        If FileManager.PathExistsAfterTryToCreate(previewsPath) Then
            Return previewsPath
        Else
            Return ""
        End If
    End Function

    Public Function GetAppSettingsFilePath() As String
        Return Path.Combine(GetCustomDataPath(), theAppSettingsFileName)
    End Function

End Module