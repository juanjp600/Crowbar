﻿Imports System.ComponentModel
Imports System.IO
Imports System.Web.Script.Serialization
Imports BackwardsCompatibility
Imports Steamworks

Public Class GarrysModSteamAppInfo
	Inherits SteamAppInfoBase

	Public Sub New()
		MyBase.New()

		Me.ID = New AppId_t(4000)
		Me.Name = "Garry's Mod"
		Me.UsesSteamUGC = True
		Me.CanUseContentFolderOrFile = True
		Me.ContentFileExtensionsAndDescriptions.Add("gma", "Garry's Mod GMA Files")
	End Sub

	Public Overrides Function ProcessFileAfterDownload(ByVal givenPathFileName As String, ByVal bw As BackgroundWorkerEx) As String
		Dim processedPathFileName As String = ""

		If Directory.Exists(givenPathFileName) Then
			' If the folder contains only one GMA file, then move+rename GMA file from folder and delete the folder.
			Dim sourcePathFileNames As List(Of String) = FileManager.GetFolderFiles(givenPathFileName, "*.gma")
			If sourcePathFileNames.Count = 1 Then
				Dim sourcePathFileName As String = sourcePathFileNames(0)
				If File.Exists(sourcePathFileName) Then
					Dim givenPath As String = Path.GetDirectoryName(givenPathFileName)
					Dim givenFolder As String = Path.GetFileName(givenPathFileName)
					Dim fileName As String = givenFolder + ".gma"
					processedPathFileName = Path.Combine(givenPath, fileName)
					processedPathFileName = FileManager.GetTestedPathFileName(processedPathFileName)
					File.Move(sourcePathFileName, processedPathFileName)
					Dim entries As String() = Directory.GetFileSystemEntries(givenPathFileName)
					If entries.Length = 0 Then
						Directory.Delete(givenPathFileName)
					End If
				End If
			Else
				processedPathFileName = givenPathFileName
			End If
		Else
			Dim processedGivenPathFileName As String = Path.ChangeExtension(givenPathFileName, ".lzma")
			Try
				If File.Exists(givenPathFileName) Then
					File.Move(givenPathFileName, processedGivenPathFileName)
					bw.ReportProgress(0, "Renamed """ + Path.GetFileName(givenPathFileName) + """ to """ + Path.GetFileName(processedGivenPathFileName) + """" + vbCrLf)
				End If
			Catch ex As Exception
				bw.ReportProgress(0, "Crowbar tried to rename the file """ + Path.GetFileName(givenPathFileName) + """ to """ + Path.GetFileName(processedGivenPathFileName) + """ but Windows gave this message: " + ex.Message)
			End Try

			processedPathFileName = Path.ChangeExtension(processedGivenPathFileName, ".gma")

			bw.ReportProgress(0, "Decompressing downloaded Garry's Mod workshop file into a GMA file." + vbCrLf)
			Try
				Using archive As New SevenZipExtractor.ArchiveFile(processedGivenPathFileName)
					archive.Extract(processedPathFileName, True)
				End Using
			Catch ex As Exception
				Throw New System.Exception("Crowbar tried to decompress the file """ + processedGivenPathFileName + """ to """ + processedPathFileName + """ but Windows gave this message: " + ex.Message)
			Finally
				bw.ReportProgress(0, "Decompress done." + vbCrLf)
			End Try

			Try
				If File.Exists(processedGivenPathFileName) Then
					File.Delete(processedGivenPathFileName)
					bw.ReportProgress(0, "Deleted: """ + processedGivenPathFileName + """" + vbCrLf)
				End If
			Catch ex As Exception
				bw.ReportProgress(0, "Crowbar tried to delete the file """ + processedGivenPathFileName + """ but Windows gave this message: " + ex.Message)
			End Try
		End If

		Return processedPathFileName
	End Function

	Public Overrides Function ProcessFileBeforeUpload(ByVal item As WorkshopItem, ByVal bw As BackgroundWorkerEx) As String
		Dim processedPathFileName As String = item.ContentPathFolderOrFileName
		Me.theBackgroundWorker = bw

		' Create a folder in the Windows Temp path, to prevent potential file collisions and to provide user a more obvious folder name.
		Dim guid As Guid
		guid = Guid.NewGuid()
		Dim tempCrowbarFolder As String
		tempCrowbarFolder = "Crowbar_" + guid.ToString()
		Dim tempPath As String = Path.GetTempPath()
		Me.theTempCrowbarPath = Path.Combine(tempPath, tempCrowbarFolder)
		Try
			FileManager.CreatePath(Me.theTempCrowbarPath)
		Catch ex As Exception
			Throw New System.Exception("Crowbar tried to create folder path """ + Me.theTempCrowbarPath + """, but Windows gave this message: " + ex.Message)
		End Try

		Dim gmaPathFileName As String
		If Directory.Exists(item.ContentPathFolderOrFileName) Then
			'NOTE: File name is all lowercase in case Garry's Mod needs that on Linux.
			If item.IsDraft Then
				gmaPathFileName = Path.Combine(Me.theTempCrowbarPath, "new_item_via_crowbar.gma")
			Else
				gmaPathFileName = Path.Combine(Me.theTempCrowbarPath, item.ID + "_via_crowbar.gma")
			End If

			'TODO: Create GMA file without calling gmad.exe.
			Dim appInstallPath As String = ""
			Dim steamPipe As New SteamPipe()
			Dim result As String = steamPipe.Open("GetAppInstallPath", Nothing, "")
			If result = "success" Then
				appInstallPath = steamPipe.GetAppInstallPath(Me.ID.ToString())
			End If
			steamPipe.Shut()
			If appInstallPath <> "" Then
				Dim garrysModBinPath As String = Path.Combine(appInstallPath, "bin")
				Dim garrysModBinGmadExe As String = Path.Combine(garrysModBinPath, "gmad.exe")
				Dim garrysModWin64Path As String = Path.Combine(garrysModBinPath, "win64")
				Dim garrysModWin64PathGmadExe As String = Path.Combine(garrysModWin64Path, "gmad.exe")
				Dim garrysModGmadExe As String = ""
				If File.Exists(garrysModWin64PathGmadExe) Then
					garrysModGmadExe = garrysModWin64PathGmadExe
				ElseIf File.Exists(garrysModBinGmadExe) Then
					garrysModGmadExe = garrysModBinGmadExe
				End If
				If garrysModGmadExe <> "" Then
					Me.theBackgroundWorker.ReportProgress(0, "Creating GMA file via """ + garrysModGmadExe + """." + vbCrLf)
					Dim addonJsonPathFileName As String = Me.CreateAddonJsonFile(item.ContentPathFolderOrFileName, item.Title, item.Tags)
					If File.Exists(addonJsonPathFileName) Then
						Dim gmadExeProcess As New Process()
						Try
							gmadExeProcess.StartInfo.UseShellExecute = False
							'NOTE: From Microsoft website: 
							'      On Windows Vista and earlier versions of the Windows operating system, 
							'      the length of the arguments added to the length of the full path to the process must be less than 2080. 
							'      On Windows 7 and later versions, the length must be less than 32699. 
							gmadExeProcess.StartInfo.FileName = garrysModGmadExe
							'gmad.exe create -folder "<FULL PATH TO ADDON FOLDER>" -out "<FULL PATH TO OUTPUT .gma FILE>"
							gmadExeProcess.StartInfo.Arguments = "create -folder """ + item.ContentPathFolderOrFileName + """ -out """ + gmaPathFileName + """"
							gmadExeProcess.StartInfo.RedirectStandardOutput = True
							gmadExeProcess.StartInfo.RedirectStandardError = True
							gmadExeProcess.StartInfo.RedirectStandardInput = True
#If DEBUG Then
							gmadExeProcess.StartInfo.CreateNoWindow = False
#Else
								gmadExeProcess.StartInfo.CreateNoWindow = True
#End If
							AddHandler gmadExeProcess.OutputDataReceived, AddressOf Me.myProcess_OutputDataReceived
							AddHandler gmadExeProcess.ErrorDataReceived, AddressOf Me.myProcess_ErrorDataReceived

							gmadExeProcess.Start()
							gmadExeProcess.StandardInput.AutoFlush = True
							gmadExeProcess.BeginOutputReadLine()
							gmadExeProcess.BeginErrorReadLine()
							gmadExeProcess.WaitForExit()
							'gmadExeProcess.Close()
						Catch ex As Exception
							Throw New System.Exception("Crowbar tried to create the file """ + gmaPathFileName + """ with Garry's Mod gmad.exe, but got this error message: " + ex.Message)
						Finally
							gmadExeProcess.Close()
							RemoveHandler gmadExeProcess.OutputDataReceived, AddressOf Me.myProcess_OutputDataReceived
							RemoveHandler gmadExeProcess.ErrorDataReceived, AddressOf Me.myProcess_ErrorDataReceived

							'If Not File.Exists(gmaPathFileName) Then
							'	Throw New System.Exception("Crowbar tried to create the file """ + gmaPathFileName + """ with Garry's Mod gmad.exe, but the file was not created.")
							'End If
						End Try
					Else
						Throw New System.Exception("Crowbar tried to create the file """ + addonJsonPathFileName + """, but the file was not created.")
					End If
				Else
					Throw New System.Exception("Crowbar tried to run """ + garrysModGmadExe + """, but the file was not found. Note that Garry's Mod must be installed for this to work.")
				End If
			End If
		Else
			gmaPathFileName = item.ContentPathFolderOrFileName
		End If

		processedPathFileName = gmaPathFileName
		Return processedPathFileName
	End Function

	Public Overrides Sub CleanUpAfterUpload(ByVal bw As BackgroundWorkerEx)
		If Directory.Exists(Me.theTempCrowbarPath) Then
			Try
				Directory.Delete(Me.theTempCrowbarPath, True)
			Catch ex As Exception
				bw.ReportProgress(0, "Crowbar tried to delete its temp folder """ + Me.theTempCrowbarPath + """ but Windows gave this message: " + ex.Message)
			End Try
		End If
		Me.theTempCrowbarPath = ""
	End Sub

	'Example 01:
	'{
	'	"title"		:	"My Server Content",
	'	"type"		:	"ServerContent",
	'	"tags"		:	[ "roleplay", "realism" ],
	'	"ignore"	:
	'	[
	'		"*.psd",
	'		"*.vcproj",
	'		"*.svn*"
	'	]
	'}
	'Example 02:
	'{
	'	"title": "Ragdoll Fight",
	'	"type": "tool",
	'	"tags": 
	'	[
	'		"scenic",
	'		"fun"
	'	]
	'}
	Public Function CreateAddonJsonFile(ByVal addonJsonPath As String, ByVal itemTitle As String, ByVal itemTags As BindingListEx(Of String)) As String
		Dim addonJsonPathFileName As String = Path.Combine(addonJsonPath, "addon.json")

		ArrangeTagsForEasierUseInAddonJsonFile(itemTags)

		Try
			If File.Exists(addonJsonPathFileName) Then
				'NOTE: User's data in Crowbar overrides data in "addon.json" file.
				File.Delete(addonJsonPathFileName)
			End If
		Catch ex As Exception
			Throw New System.Exception("Crowbar tried to delete an old temp file """ + addonJsonPathFileName + """ but Windows gave this message: " + ex.Message)
		End Try

		' Remove the "Addon" tag because it should not go into the json file.
		itemTags.Remove("Addon")

		Dim fileStream As StreamWriter
		fileStream = File.CreateText(addonJsonPathFileName)
		fileStream.AutoFlush = True
		Try
			Dim jss As JavaScriptSerializer = New JavaScriptSerializer()
			If File.Exists(addonJsonPathFileName) Then
				fileStream.WriteLine("{")
				fileStream.WriteLine(vbTab + """title"": " + jss.Serialize(itemTitle) + ",")
				If itemTags.Count > 1 Then
					fileStream.WriteLine(vbTab + """type"": " + jss.Serialize(itemTags(0)) + ",")
					fileStream.WriteLine(vbTab + """tags"": ")
					fileStream.WriteLine(vbTab + "[")
					If itemTags.Count > 2 Then
						fileStream.WriteLine(vbTab + vbTab + jss.Serialize(itemTags(1)) + ",")
						fileStream.WriteLine(vbTab + vbTab + jss.Serialize(itemTags(2)))
					Else
						fileStream.WriteLine(vbTab + vbTab + jss.Serialize(itemTags(1)))
					End If
					fileStream.WriteLine(vbTab + "]")
				Else
					fileStream.WriteLine(vbTab + """type"": " + jss.Serialize(itemTags(0)))
				End If
				fileStream.WriteLine("}")
				fileStream.Flush()
			End If
		Catch ex As Exception
			'NOTE: This is here in case I missed something, such as itemTags being empty.
			Dim debug As Integer = 4242
		Finally
			If fileStream IsNot Nothing Then
				fileStream.Flush()
				fileStream.Close()
				fileStream = Nothing
			End If

			'NOTE: Add the "Addon" tag back because itemTags is an object used throughout app and is not a local copy.
			itemTags.Add("Addon")
		End Try

		Return addonJsonPathFileName
	End Function

	Public Function ReadDataFromAddonJsonFile(ByVal addonJsonPathFileName As String, ByRef itemTitle As String, ByRef itemTags As BindingListEx(Of String)) As AppEnums.StatusMessage
		Dim readIsSuccess As AppEnums.StatusMessage = StatusMessage.Success

		If File.Exists(addonJsonPathFileName) Then
			Dim fileStream As New StreamReader(addonJsonPathFileName)
			Dim addonFileContents As String = Nothing

			Try
				addonFileContents = fileStream.ReadToEnd()
			Catch ex As Exception
				Dim debug As Integer = 4242
			Finally
				fileStream.Close()
			End Try

			Try
				If addonFileContents IsNot Nothing AndAlso addonFileContents <> "" Then
					Dim jss As JavaScriptSerializer = New JavaScriptSerializer()
					Dim addon As GarrysMod_AddonJson = jss.Deserialize(Of GarrysMod_AddonJson)(addonFileContents)

					itemTitle = addon.title
					itemTags.Clear()
					If addon.type IsNot Nothing Then
						itemTags.Add(addon.type)
					End If
					If addon.tags IsNot Nothing Then
						For Each tag As String In addon.tags
							itemTags.Add(tag)
						Next
					End If
					For tagIndex As Integer = 0 To itemTags.Count - 1
						If itemTags(tagIndex) <> "ServerContent" AndAlso itemTags(tagIndex) <> "Addon" Then
							If itemTags(tagIndex).Length > 1 Then
								itemTags(tagIndex) = itemTags(tagIndex).Substring(0, 1).ToUpper() + itemTags(tagIndex).Substring(1)
							ElseIf itemTags(tagIndex).Length = 1 Then
								itemTags(tagIndex) = itemTags(tagIndex).ToUpper()
							End If
						End If
					Next
				End If
			Catch ex As Exception
				' Gets here if bad json format.
				readIsSuccess = StatusMessage.Error
			End Try
		End If

		Return readIsSuccess
	End Function

	Private Sub ArrangeTagsForEasierUseInAddonJsonFile(ByRef tags As BindingListEx(Of String))
		Dim anEnumList As IList
		anEnumList = EnumHelper.ToList(GetType(GarrysModTypeTags))
		Dim index As Integer
		For Each tag As String In tags
			index = EnumHelper.IndexOfKeyAsCaseInsensitiveString(tag, anEnumList)
			If index <> -1 Then
				tags.Remove(tag)
				tags.Insert(0, tag)
				Exit For
			End If
		Next
		For tagIndex As Integer = 0 To tags.Count - 1
			If tags(tagIndex) <> "ServerContent" AndAlso tags(tagIndex) <> "Addon" Then
				tags(tagIndex) = tags(tagIndex).ToLowerInvariant()
			End If
		Next
		'NOTE: Not sure how this became empty for me in testing, so let's make sure there is a tag so Crowbar does not show exception window.
		If tags.Count = 0 Then
			tags.Add("ServerContent")
		End If
	End Sub

	Private Sub myProcess_OutputDataReceived(ByVal sender As Object, ByVal e As System.Diagnostics.DataReceivedEventArgs)
		Dim myProcess As Process = CType(sender, Process)
		Dim line As String

		Try
			line = e.Data
			If line IsNot Nothing Then
				'Me.theProcessHasOutputData = True
				Me.theBackgroundWorker.ReportProgress(1, line + vbCrLf)
			End If
		Catch ex As Exception
			Dim debug As Integer = 4242
		Finally
			'If Me.CancellationPending Then
			'	Me.StopPack(True, myProcess)
			'ElseIf Me.theSkipCurrentModelIsActive Then
			'	Me.StopPack(True, myProcess)
			'End If
		End Try
	End Sub

	Private Sub myProcess_ErrorDataReceived(ByVal sender As Object, ByVal e As System.Diagnostics.DataReceivedEventArgs)
		Dim myProcess As Process = CType(sender, Process)
		Dim line As String

		Try
			line = e.Data
			If line IsNot Nothing Then
				Me.theBackgroundWorker.ReportProgress(1, line + vbCrLf)
			End If
		Catch ex As Exception
			Dim debug As Integer = 4242
		Finally
			'If Me.CancellationPending Then
			'	Me.StopPack(True, myProcess)
			'ElseIf Me.theSkipCurrentModelIsActive Then
			'	Me.StopPack(True, myProcess)
			'End If
		End Try
	End Sub

	Public Enum GarrysModTypeTags
		<Description("Effects")> Effects
		<Description("Game Mode")> Gamemode
		<Description("Map")> Map
		<Description("Model")> Model
		<Description("NPC")> NPC
		<Description("Server Content")> ServerContent
		<Description("Tool")> Tool
		<Description("Vehicle")> Vehicle
		<Description("Weapon")> Weapon
	End Enum

	Private theBackgroundWorker As BackgroundWorkerEx
	Private theTempCrowbarPath As String

End Class
