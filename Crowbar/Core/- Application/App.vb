Imports System.Collections.ObjectModel
Imports System.Globalization
Imports System.IO
Imports System.Reflection
Imports System.Resources
Imports System.Text

Public Class App
	Implements IDisposable

#Region "Create and Destroy"

	Public Sub New()
		Me.IsDisposed = False
	End Sub

#Region "IDisposable Support"

	Public Sub Dispose() Implements IDisposable.Dispose
		' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) below.
		Dispose(True)
		GC.SuppressFinalize(Me)
	End Sub

	Protected Overridable Sub Dispose(ByVal disposing As Boolean)
		If Not Me.IsDisposed Then
			If disposing Then
				Me.Free()
			End If
			'NOTE: free shared unmanaged resources
		End If
		Me.IsDisposed = True
	End Sub

	'Protected Overrides Sub Finalize()
	'	' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
	'	Dispose(False)
	'	MyBase.Finalize()
	'End Sub

#End Region

#End Region

#Region "Init and Free"

	Public Sub Init()
		Me.theAppPath = Application.StartupPath
		'NOTE: Needed for using DLLs placed in folder separate from main EXE file.
		Environment.SetEnvironmentVariable("path", Paths.GetCustomDataPath(), EnvironmentVariableTarget.Process)

		Dim resourceManager = new ResourceManager("Crowbar.Resources", AppConstants.EntryAssembly)
		For Each entry In [Enum].GetValues(Of ResourceStrings.Entry)()
			Dim a = resourceManager.GetString(entry.ToString())
			ResourceStrings.Values(entry) = a
		Next

		Me.WriteRequiredFiles()
		Me.LoadAppSettings()

		If Me.Settings.SteamLibraryPaths.Count = 0 Then
			Dim libraryPath As New SteamLibraryPath()
			Me.Settings.SteamLibraryPaths.Add(libraryPath)
		End If

		Me.theUnpacker = New Unpacker()
		Me.theDecompiler = New Decompiler()
		Me.theCompiler = New Compiler()
		Me.thePacker = New Packer()
		'Me.theModelViewer = New Viewer()

		Dim documentsPath As String
		documentsPath = Path.Combine(Me.theAppPath, "Documents")
		AppConstants.HeaderComment = GetHeaderComment()
		AppConstants.HelpTutorialLink = Path.Combine(documentsPath, AppConstants.HelpTutorialLink)
		AppConstants.HelpContentsLink = Path.Combine(documentsPath, AppConstants.HelpContentsLink)
		AppConstants.HelpIndexLink = Path.Combine(documentsPath, AppConstants.HelpIndexLink)
		AppConstants.HelpTipsLink = Path.Combine(documentsPath, AppConstants.HelpTipsLink)
	End Sub

	Private Sub Free()
		If AppSettings.Instance IsNot Nothing Then
			Me.SaveAppSettings()
		End If
		'If Me.theCompiler IsNot Nothing Then
		'End If
	End Sub

#End Region

#Region "Properties"

	Public ReadOnly Property Settings() As AppSettings
		Get
			Return AppSettings.Instance
		End Get
	End Property

	Public ReadOnly Property SteamAppInfos() As List(Of SteamAppInfoBase)
		Get
			Return SteamAppInfoBase.SteamAppInfos
		End Get
	End Property

	Public ReadOnly Property CommandLineOption_Settings_IsEnabled() As Boolean
		Get
			Return Me.theCommandLineOption_Settings_IsEnabled
		End Get
	End Property

	Public ReadOnly Property Unpacker() As Unpacker
		Get
			Return Me.theUnpacker
		End Get
	End Property

	Public ReadOnly Property Decompiler() As Decompiler
		Get
			Return Me.theDecompiler
		End Get
	End Property

	Public ReadOnly Property Compiler() As Compiler
		Get
			Return Me.theCompiler
		End Get
	End Property

	Public ReadOnly Property Packer() As Packer
		Get
			Return Me.thePacker
		End Get
	End Property

	'Public ReadOnly Property Viewer() As Viewer
	'	Get
	'		Return Me.theModelViewer
	'	End Get
	'End Property

	'Public Property ModelRelativePathFileName() As String
	'	Get
	'		Return Me.theModelRelativePathFileName
	'	End Get
	'	Set(ByVal value As String)
	'		Me.theModelRelativePathFileName = value
	'	End Set
	'End Property

	Public ReadOnly Property SmdFileNames() As List(Of String)
		Get
			Return SourceFileNamesModule.SmdFileNames
		End Get
	End Property

#End Region

#Region "Methods"

	Public Function CommandLineValueIsAnAppSetting(ByVal commandLineValue As String) As Boolean
		Return commandLineValue.StartsWith(App.SettingsParameter)
	End Function

	Public Sub WriteRequiredFiles()
		'NOTE: Only write settings file if it does not exist.
		Try
			If Not File.Exists(Paths.AppSettingsFilePath) Then
				File.WriteAllText(Paths.AppSettingsFilePath, My.Resources.Crowbar_Settings)
			End If
		Catch ex As Exception
			Console.WriteLine("EXCEPTION: " + ex.Message)
			'Throw New Exception(ex.Message, ex.InnerException)
			Exit Sub
		Finally
		End Try
	End Sub

	Public Sub SaveAppSettings()
		Dim appSettingsPath As String = FileManager.GetPath(Paths.AppSettingsFilePath)

		If FileManager.PathExistsAfterTryToCreate(appSettingsPath) Then
			FileManager.WriteXml(AppSettings.Instance, Paths.AppSettingsFilePath)
		End If
	End Sub

#End Region

#Region "Private Methods"

	Private Sub LoadAppSettings()

		Dim commandLineOption_Settings_IsEnabled As Boolean = False
		Dim commandLineValues As New ReadOnlyCollection(Of String)(System.Environment.GetCommandLineArgs())
		If commandLineValues.Count > 1 AndAlso commandLineValues(1) <> "" Then
			Dim command As String = commandLineValues(1)
			If command.StartsWith(App.SettingsParameter) Then
				commandLineOption_Settings_IsEnabled = True
				Dim oldAppSettingsPathFileName As String = command.Replace(App.SettingsParameter, "")
				oldAppSettingsPathFileName = oldAppSettingsPathFileName.Replace("""", "")
				If File.Exists(oldAppSettingsPathFileName) Then
					File.Copy(oldAppSettingsPathFileName, Paths.AppSettingsFilePath, True)
				End If
			End If
		End If

		If File.Exists(Paths.AppSettingsFilePath) Then
			Try
				VersionModule.ConvertSettingsFile(Paths.AppSettingsFilePath)
				AppSettings.Instance = CType(FileManager.ReadXml(GetType(AppSettings), Paths.AppSettingsFilePath), AppSettings)
			Catch ex As Exception
				Me.CreateAppSettings()
			End Try
		Else
			' File not found, so init default values.
			Me.CreateAppSettings()
		End If
	End Sub

	Private Sub CreateAppSettings()
		AppSettings.Instance = New AppSettings()

		Dim gameSetup As New GameSetup()
		AppSettings.Instance.GameSetups.Add(gameSetup)

		Dim aPath As New SteamLibraryPath()
		AppSettings.Instance.SteamLibraryPaths.Add(aPath)

		Me.SaveAppSettings()
	End Sub

	'Private Function GetAppDataPath() As String
	'	Dim appDataPath As String
	'	Dim appDataPathFileName As String

	'	appDataPath = Path.Combine(Me.theAppPath, App.theDataFolderName)
	'	appDataPathFileName = Path.Combine(appDataPath, App.theAppSettingsFileName)

	'	If File.Exists(appDataPathFileName) Then
	'		Return appDataPath
	'	Else
	'		Return ""
	'	End If
	'End Function

	Private Sub WriteResourceToFileIfDifferent(ByVal dataResource As Byte(), ByVal pathFileName As String)
		Try
			Dim isDifferentOrNotExist As Boolean = True
			If File.Exists(pathFileName) Then
				Dim resourceHash() As Byte
				Dim sha As New Security.Cryptography.SHA512Managed()
				resourceHash = sha.ComputeHash(dataResource)

				Dim fileStream As FileStream = File.Open(pathFileName, FileMode.Open)
				Dim fileHash() As Byte = sha.ComputeHash(fileStream)
				fileStream.Close()

				isDifferentOrNotExist = False
				For x As Integer = 0 To resourceHash.Length - 1
					If resourceHash(x) <> fileHash(x) Then
						isDifferentOrNotExist = True
						Exit For
					End If
				Next
			End If

			If isDifferentOrNotExist Then
				File.WriteAllBytes(pathFileName, dataResource)
			End If
		Catch ex As Exception
			Console.WriteLine("EXCEPTION: " + ex.Message)
			'Throw New Exception(ex.Message, ex.InnerException)
			Exit Sub
		Finally
		End Try
	End Sub
#End Region

#Region "Data"

	Private IsDisposed As Boolean

	'NOTE: Use slash at start to avoid confusing with a pathFileName that Windows Explorer might use with auto-open.
	Public Const SettingsParameter As String = "/settings="
	Private theCommandLineOption_Settings_IsEnabled As Boolean

	' Location of the exe.
	Private theAppPath As String

	Private theUnpacker As Unpacker
	Private theDecompiler As Decompiler
	Private theCompiler As Compiler
	Private thePacker As Packer
	'Private theModelViewer As Viewer
	'Private theModelRelativePathFileName As String

#End Region

End Class
