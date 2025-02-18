﻿Imports System.IO

Public Class SourceModel53
	Inherits SourceModel49

#Region "Creation and Destruction"

	Public Sub New(ByVal mdlPathFileName As String, ByVal mdlVersion As Integer)
		MyBase.New(mdlPathFileName, mdlVersion)
	End Sub

#End Region

#Region "Properties"

	Public Overrides ReadOnly Property VtxFileIsUsed As Boolean
		Get
			Return False
		End Get
	End Property

	Public Overrides ReadOnly Property AniFileIsUsed As Boolean
		Get
			Return False
		End Get
	End Property

	Public Overrides ReadOnly Property VvdFileIsUsed As Boolean
		Get
			Return False
		End Get
	End Property

	Public Overrides ReadOnly Property HasTextureData As Boolean
		Get
			Return Not Me.theMdlFileDataGeneric.theMdlFileOnlyHasAnimations AndAlso Me.theMdlFileData.theTextures IsNot Nothing AndAlso Me.theMdlFileData.theTextures.Count > 0
		End Get
	End Property

	Public Overrides ReadOnly Property HasMeshData As Boolean
		Get
			If Not Me.theMdlFileData.theMdlFileOnlyHasAnimations _
					 AndAlso Me.theMdlFileData.theBones IsNot Nothing _
					 AndAlso Me.theMdlFileData.theBones.Count > 0 _
					 AndAlso Me.theVtxFileData IsNot Nothing Then
				Return True
			Else
				Return False
			End If
		End Get
	End Property

	Public Overrides ReadOnly Property HasLodMeshData As Boolean
		Get
			If Not Me.theMdlFileData.theMdlFileOnlyHasAnimations _
					 AndAlso Me.theMdlFileData.theBones IsNot Nothing _
					 AndAlso Me.theMdlFileData.theBones.Count > 0 _
					 AndAlso Me.theVtxFileData IsNot Nothing _
					 AndAlso Me.theVtxFileData.lodCount > 0 Then
				Return True
			Else
				Return False
			End If
		End Get
	End Property

	Public Overrides ReadOnly Property HasPhysicsMeshData As Boolean
		Get
			If Me.thePhyFileDataGeneric IsNot Nothing _
			 AndAlso Me.thePhyFileDataGeneric.theSourcePhyCollisionDatas IsNot Nothing _
			 AndAlso Not Me.theMdlFileData.theMdlFileOnlyHasAnimations _
			 AndAlso Me.theMdlFileData.theBones IsNot Nothing _
			 AndAlso Me.theMdlFileData.theBones.Count > 0 Then
				Return True
			Else
				Return False
			End If
		End Get
	End Property

	Public Overrides ReadOnly Property HasProceduralBonesData As Boolean
		Get
			If Me.theMdlFileData IsNot Nothing _
			 AndAlso Me.theMdlFileData.theProceduralBonesCommandIsUsed _
			 AndAlso Not Me.theMdlFileData.theMdlFileOnlyHasAnimations _
			 AndAlso Me.theMdlFileData.theBones IsNot Nothing _
			 AndAlso Me.theMdlFileData.theBones.Count > 0 Then
				Return True
			Else
				Return False
			End If
		End Get
	End Property

	Public Overrides ReadOnly Property HasBoneAnimationData As Boolean
		Get
			If Me.theMdlFileData.theAnimationDescs IsNot Nothing _
			 AndAlso Me.theMdlFileData.theAnimationDescs.Count > 0 Then
				Return True
			Else
				Return False
			End If
		End Get
	End Property

	Public Overrides ReadOnly Property HasVertexAnimationData As Boolean
		Get
			If Not Me.theMdlFileData.theMdlFileOnlyHasAnimations _
			 AndAlso Me.theMdlFileData.theFlexDescs IsNot Nothing _
			 AndAlso Me.theMdlFileData.theFlexDescs.Count > 0 Then
				Return True
			Else
				Return False
			End If
		End Get
	End Property

#End Region

#Region "Methods"

	Public Overrides Function CheckForRequiredFiles() As FilesFoundFlags
		Dim status As AppEnums.FilesFoundFlags = FilesFoundFlags.AllFilesFound

		'If Me.theMdlFileData.animBlockCount > 0 Then
		'	Me.theAniPathFileName = Path.ChangeExtension(Me.theMdlPathFileName, ".ani")
		'	If Not File.Exists(Me.theAniPathFileName) Then
		'		status = status Or FilesFoundFlags.ErrorRequiredAniFileNotFound
		'	End If
		'End If

		'If Not Me.theMdlFileDataGeneric.theMdlFileOnlyHasAnimations Then
		'	Me.thePhyPathFileName = Path.ChangeExtension(Me.theMdlPathFileName, ".phy")

		'	Me.theVtxPathFileName = Path.ChangeExtension(Me.theMdlPathFileName, ".dx11.vtx")
		'	If Not File.Exists(Me.theVtxPathFileName) Then
		'		Me.theVtxPathFileName = Path.ChangeExtension(Me.theMdlPathFileName, ".dx90.vtx")
		'		If Not File.Exists(Me.theVtxPathFileName) Then
		'			Me.theVtxPathFileName = Path.ChangeExtension(Me.theMdlPathFileName, ".dx80.vtx")
		'			If Not File.Exists(Me.theVtxPathFileName) Then
		'				Me.theVtxPathFileName = Path.ChangeExtension(Me.theMdlPathFileName, ".sw.vtx")
		'				If Not File.Exists(Me.theVtxPathFileName) Then
		'					Me.theVtxPathFileName = Path.ChangeExtension(Me.theMdlPathFileName, ".vtx")
		'					If Not File.Exists(Me.theVtxPathFileName) Then
		'						status = status Or FilesFoundFlags.ErrorRequiredVtxFileNotFound
		'					End If
		'				End If
		'			End If
		'		End If
		'	End If

		'	Me.theVvdPathFileName = Path.ChangeExtension(Me.theMdlPathFileName, ".vvd")
		'	If Not File.Exists(Me.theVvdPathFileName) Then
		'		status = status Or FilesFoundFlags.ErrorRequiredVvdFileNotFound
		'	End If
		'End If

		Return status
	End Function

	Public Overrides Function ReadPhyFile() As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		'If String.IsNullOrEmpty(Me.thePhyPathFileName) Then
		'	status = Me.CheckForRequiredFiles()
		'End If

		If status = StatusMessage.Success Then
			Try
				Me.ReadFile(Me.thePhyPathFileName, AddressOf Me.ReadPhyFile_Internal)
				If Me.thePhyFileDataGeneric.checksum <> Me.theMdlFileData.checksum Then
					'status = StatusMessage.WarningPhyChecksumDoesNotMatchMdl
					Me.NotifySourceModelProgress(ProgressOptions.WarningPhyFileChecksumDoesNotMatchMdlFileChecksum, "")
				End If
			Catch ex As Exception
				status = StatusMessage.Error
			End Try
		End If

		Return status
	End Function

	Public Overrides Function WriteLodMeshFiles(ByVal modelOutputPath As String) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		status = Me.WriteMeshSmdFiles(modelOutputPath, 1, Me.theVtxFileData.lodCount - 1)

		Return status
	End Function

	Public Overrides Function WriteBoneAnimationSmdFiles(ByVal modelOutputPath As String) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		Dim anAnimationDesc As SourceMdlAnimationDesc53
		Dim smdPath As String
		'Dim smdFileName As String
		Dim smdPathFileName As String
		Dim writeStatus As String

		Try
			For anAnimDescIndex As Integer = 0 To Me.theMdlFileData.theAnimationDescs.Count - 1
				anAnimationDesc = Me.theMdlFileData.theAnimationDescs(anAnimDescIndex)

				anAnimationDesc.theSmdRelativePathFileName = SourceFileNamesModule.CreateAnimationSmdRelativePathFileName(anAnimationDesc.theSmdRelativePathFileName, Me.Name, anAnimationDesc.theName)
				smdPathFileName = Path.Combine(modelOutputPath, anAnimationDesc.theSmdRelativePathFileName)
				smdPath = FileManager.GetPath(smdPathFileName)
				If FileManager.PathExistsAfterTryToCreate(smdPath) Then
					Me.NotifySourceModelProgress(ProgressOptions.WritingFileStarted, smdPathFileName)
					'NOTE: Check here in case writing is canceled in the above event.
					If Me.theWritingIsCanceled Then
						status = StatusMessage.Canceled
						Return status
					ElseIf Me.theWritingSingleFileIsCanceled Then
						Me.theWritingSingleFileIsCanceled = False
						Continue For
					End If

					writeStatus = Me.WriteBoneAnimationSmdFile(smdPathFileName, Nothing, anAnimationDesc)

					If writeStatus = "Success" Then
						Me.NotifySourceModelProgress(ProgressOptions.WritingFileFinished, smdPathFileName)
					Else
						Me.NotifySourceModelProgress(ProgressOptions.WritingFileFailed, writeStatus)
					End If
				End If
			Next
		Catch ex As Exception
			Dim debug As Integer = 4242
		End Try

		Return status
	End Function

	Public Overrides Function WriteVertexAnimationVtaFiles(ByVal modelOutputPath As String) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		Dim aBodyPart As SourceMdlBodyPart
		Dim vtaFileName As String
		Dim vtaPath As String
		Dim vtaPathFileName As String

		Try
			For aBodyPartIndex As Integer = 0 To Me.theMdlFileData.theBodyParts.Count - 1
				aBodyPart = Me.theMdlFileData.theBodyParts(aBodyPartIndex)

				If aBodyPart.theFlexFrames Is Nothing OrElse aBodyPart.theFlexFrames.Count = 0 Then
					Continue For
				End If

				vtaFileName = SourceFileNamesModule.GetVtaFileName(Me.Name, aBodyPartIndex)
				vtaPathFileName = Path.Combine(modelOutputPath, vtaFileName)
				vtaPath = FileManager.GetPath(vtaPathFileName)
				If FileManager.PathExistsAfterTryToCreate(vtaPath) Then
					Me.NotifySourceModelProgress(ProgressOptions.WritingFileStarted, vtaPathFileName)
					'NOTE: Check here in case writing is canceled in the above event.
					If Me.theWritingIsCanceled Then
						status = StatusMessage.Canceled
						Return status
					ElseIf Me.theWritingSingleFileIsCanceled Then
						Me.theWritingSingleFileIsCanceled = False
						Continue For
					End If

					Me.WriteVertexAnimationVtaFile(vtaPathFileName, aBodyPart)

					Me.NotifySourceModelProgress(ProgressOptions.WritingFileFinished, vtaPathFileName)
				End If
			Next
		Catch ex As Exception
			Dim debug As Integer = 4242
		End Try

		Return status
	End Function

	Public Overrides Function GetTextureFolders() As List(Of String)
		Dim textureFolders As New List(Of String)()

		For i As Integer = 0 To Me.theMdlFileData.theTexturePaths.Count - 1
			Dim aTextureFolder As String
			aTextureFolder = Me.theMdlFileData.theTexturePaths(i)

			textureFolders.Add(aTextureFolder)
		Next

		Return textureFolders
	End Function

	Public Overrides Function GetTextureFileNames() As List(Of String)
		Dim textureFileNames As New List(Of String)()

		For i As Integer = 0 To Me.theMdlFileData.theTextures.Count - 1
			Dim aTexture As SourceMdlTexture
			aTexture = Me.theMdlFileData.theTextures(i)

			textureFileNames.Add(aTexture.thePathFileName)
		Next

		Return textureFileNames
	End Function

	'Public Overrides Function GetSequenceInfo() As List(Of String)
	'	Dim sequenceFileNames As New List(Of String)()

	'	For i As Integer = 0 To Me.theMdlFileData.theSequenceDescs.Count - 1
	'		Dim aSequence As SourceMdlSequenceDesc
	'		aSequence = Me.theMdlFileData.theSequenceDescs(i)

	'		sequenceFileNames.Add(aSequence.theName)
	'	Next

	'	Return sequenceFileNames
	'End Function

#End Region

#Region "Private Methods"

	Protected Overrides Sub ReadAniFile_Internal()
		If Me.theAniFileData49 Is Nothing Then
			'Me.theAniFileData49 = New SourceAniFileData49()
			Me.theAniFileData49 = New SourceMdlFileData49()
			Me.theAniFileDataGeneric = Me.theAniFileData49
		End If

		If Me.theMdlFileData Is Nothing Then
			Me.theMdlFileData = New SourceMdlFileData53()
			Me.theMdlFileDataGeneric = Me.theMdlFileData
		End If

		Dim aniFile As New SourceAniFile49(Me.theInputFileReader, Me.theAniFileData49, Me.theMdlFileData)

		aniFile.ReadMdlHeader00("ANI File Header 00")
		aniFile.ReadMdlHeader01("ANI File Header 01")

		aniFile.ReadAnimationAniBlocks()
	End Sub

	Protected Overrides Sub ReadMdlFile_Internal()
		If Me.theMdlFileData Is Nothing Then
			Me.theMdlFileData = New SourceMdlFileData53()
			Me.theMdlFileDataGeneric = Me.theMdlFileData
		End If

		Dim mdlFile As New SourceMdlFile53(Me.theInputFileReader, Me.theMdlFileData)

		Me.theMdlFileData.theSectionFrameCount = 0
		Me.theMdlFileData.theModelCommandIsUsed = False
		Me.theMdlFileData.theProceduralBonesCommandIsUsed = False

		mdlFile.ReadMdlHeader00("MDL File Header 00")
		mdlFile.ReadMdlHeader01("MDL File Header 01")

		mdlFile.ReadMayaStrings()

		mdlFile.ReadBones()
		mdlFile.ReadBoneControllers()
		mdlFile.ReadAttachments()

		mdlFile.ReadHitboxSets()

		mdlFile.ReadBoneTableByName()

		If Me.theMdlFileData.localAnimationCount > 0 Then
			Try
				mdlFile.ReadLocalAnimationDescs()
				mdlFile.ReadAnimationSections()
				mdlFile.ReadAnimationMdlBlocks()
			Catch ex As Exception
				Dim debug As Integer = 4242
			End Try
		End If

		mdlFile.ReadSequenceDescs()
		mdlFile.ReadLocalNodeNames()
		mdlFile.ReadLocalNodes()

		'NOTE: Read flex descs before body parts so that flexes (within body parts) can add info to flex descs.
		'mdlFile.ReadFlexDescs()
		mdlFile.ReadBodyParts()
		'mdlFile.ReadFlexControllers()
		'NOTE: This must be after flex descs are read so that flex desc usage can be saved in flex desc.
		'mdlFile.ReadFlexRules()
		mdlFile.ReadIkChains()
		'mdlFile.ReadIkLocks()
		mdlFile.ReadPoseParamDescs()
		mdlFile.ReadModelGroups()

		mdlFile.ReadRui()

		'NOTE: V53 MDLs normally don't have more than one texture path due to how RPak materials work.
		mdlFile.ReadTexturePaths()
		'NOTE: ReadTextures must be after ReadTexturePaths(), so it can compare with the texture paths.
		mdlFile.ReadTextures()
		mdlFile.ReadSkinFamilies()

		mdlFile.ReadKeyValues()

		mdlFile.ReadBoneTransforms()
		mdlFile.ReadLinearBoneTable()

		mdlFile.ReadPerTriCollisionHeader()

		''TODO: ReadLocalIkAutoPlayLocks()
		'mdlFile.ReadFlexControllerUis()

		' Read VTX info, VVD info, and PHY info.
		If Me.theMdlFileData.phyOffset > 0 Then
			mdlFile.SetReaderToPhyOffset()
			Me.ReadPhyFile_Internal()
		End If
		If Me.theMdlFileData.vtxOffset > 0 Then
			mdlFile.SetReaderToVtxOffset()
			Me.ReadVtxFile_Internal()
		End If
		If Me.theMdlFileData.vvdOffset > 0 Then
			mdlFile.SetReaderToVvdOffset()
			Me.ReadVvdFile_Internal()
		End If

		

		mdlFile.ReadUnreadBytes()

		' Post-processing.
		mdlFile.CreateFlexFrameList()
		Common.ProcessTexturePaths(Me.theMdlFileData.theTexturePaths, Me.theMdlFileData.theTextures, Me.theMdlFileData.theModifiedTexturePaths, Me.theMdlFileData.theModifiedTextureFileNames)
	End Sub

	Protected Overrides Sub ReadPhyFile_Internal()
		If Me.thePhyFileDataGeneric Is Nothing Then
			Me.thePhyFileDataGeneric = New SourcePhyFileData()
		End If

		Dim phyFile As New SourcePhyFile(Me.theInputFileReader, Me.thePhyFileDataGeneric, Me.theMdlFileData.phyOffset + Me.theMdlFileData.phySize)

		phyFile.ReadSourcePhyHeader()
		If Me.thePhyFileDataGeneric.solidCount > 0 Then
			phyFile.ReadSourceCollisionData()
			phyFile.CalculateVertexNormals()
			phyFile.ReadSourcePhysCollisionModels()
			phyFile.ReadSourcePhyRagdollConstraintDescs()
			phyFile.ReadSourcePhyCollisionRules()
			phyFile.ReadSourcePhyEditParamsSection()
			phyFile.ReadCollisionTextSection()
		End If
		phyFile.ReadUnreadBytes()
	End Sub

	Protected Overrides Sub ReadVtxFile_Internal()
		If Me.theVtxFileData Is Nothing Then
			Me.theVtxFileData = New SourceVtxFileData07()
		End If

		Dim vtxFile As New SourceVtxFile07(Me.theInputFileReader, Me.theVtxFileData, Me.theInputFileReader.BaseStream.Position)

		vtxFile.ReadSourceVtxHeader()
		If Me.theVtxFileData.lodCount > 0 Then
			vtxFile.ReadSourceVtxBodyParts()
		End If
		vtxFile.ReadSourceVtxMaterialReplacementLists()
		vtxFile.ReadUnreadBytes()
	End Sub

	Protected Overrides Sub ReadVvdFile_Internal()
		If Me.theVvdFileData49 Is Nothing Then
			Me.theVvdFileData49 = New SourceVvdFileData04()
		End If

		Dim vvdFile As New SourceVvdFile04(Me.theInputFileReader, Me.theVvdFileData49, Me.theInputFileReader.BaseStream.Position)

		vvdFile.ReadSourceVvdHeader()
		vvdFile.ReadVertexes()
		vvdFile.ReadFixups()
		vvdFile.ReadUnreadBytes()
	End Sub

	Protected Overrides Sub WriteQcFile()
		'Dim qcFile As New SourceQcFile53(Me.theOutputFileTextWriter, Me.theQcPathFileName, Me.theMdlFileData, Me.theVtxFileData, Me.thePhyFileDataGeneric, Me.theAniFileData49, Me.theName)
		Dim qcFile As New SourceQcFile53(Me.theOutputFileTextWriter, Me.theQcPathFileName, Me.theMdlFileData, Me.theVtxFileData, Me.thePhyFileDataGeneric, Me.theName)

		Try
			qcFile.WriteHeaderComment()

			qcFile.WriteModelNameCommand()

			qcFile.WriteStaticPropCommand()
			qcFile.WriteConstDirectionalLightCommand()

			qcFile.WriteFadeDistanceCommand()

			'If Me.theMdlFileData.theModelCommandIsUsed Then
			'	qcFile.WriteModelCommand()
			'	qcFile.WriteBodyGroupCommand(1)
			'Else
			'	qcFile.WriteBodyGroupCommand(0)
			'End If
			qcFile.WriteBodyGroupCommand()
			qcFile.WriteGroup("lod", AddressOf qcFile.WriteGroupLod, False, False)

			qcFile.WriteSurfacePropCommand()
			qcFile.WriteJointSurfacePropCommand()
			qcFile.WriteContentsCommand()
			qcFile.WriteJointContentsCommand()
			qcFile.WriteIllumPositionCommand()

			qcFile.WriteEyePositionCommand()
			' Removed in V53
			'qcFile.WriteMaxEyeDeflectionCommand()
			qcFile.WriteNoForcedFadeCommand()
			qcFile.WriteForcePhonemeCrossfadeCommand()

			qcFile.WriteAmbientBoostCommand()
			qcFile.WriteOpaqueCommand()
			qcFile.WriteObsoleteCommand()
			qcFile.WriteCastTextureShadowsCommand()
			qcFile.WriteDoNotCastShadowsCommand()
			qcFile.WriteCdMaterialsCommand()
			qcFile.WriteTextureGroupCommand()
			If AppSettings.Instance.DecompileDebugInfoFilesIsChecked Then
				qcFile.WriteTextureFileNameComments()
			End If

			qcFile.WriteAttachmentCommand()

			qcFile.WriteGroup("box", AddressOf qcFile.WriteGroupBox, True, False)

			qcFile.WriteControllerCommand()
			qcFile.WriteScreenAlignCommand()

			qcFile.WriteGroup("bone", AddressOf qcFile.WriteGroupBone, False, False)

			qcFile.WriteGroup("animation", AddressOf qcFile.WriteGroupAnimation, False, False)

			qcFile.WriteGroup("collision", AddressOf qcFile.WriteGroupCollision, False, False)

			qcFile.WriteKeyValues(Me.theMdlFileData.theKeyValuesText, "$KeyValues")
		Catch ex As Exception
			Dim debug As Integer = 4242
		Finally
		End Try
	End Sub

	Protected Overrides Sub ReadMdlFileHeader_Internal()
		If Me.theMdlFileData Is Nothing Then
			Me.theMdlFileData = New SourceMdlFileData53()
			Me.theMdlFileDataGeneric = Me.theMdlFileData
		End If

		Dim mdlFile As New SourceMdlFile53(Me.theInputFileReader, Me.theMdlFileData)

		mdlFile.ReadMdlHeader00("MDL File Header 00")
		mdlFile.ReadMdlHeader01("MDL File Header 01")

		'If Me.theMdlFileData.fileSize <> Me.theMdlFileData.theActualFileSize Then
		'	status = StatusMessage.ErrorInvalidInternalMdlFileSize
		'End If
	End Sub

	Protected Overrides Sub ReadMdlFileForViewer_Internal()
		If Me.theMdlFileData Is Nothing Then
			Me.theMdlFileData = New SourceMdlFileData53()
			Me.theMdlFileDataGeneric = Me.theMdlFileData
		End If

		Dim mdlFile As New SourceMdlFile53(Me.theInputFileReader, Me.theMdlFileData)

		mdlFile.ReadMdlHeader00("MDL File Header 00")
		mdlFile.ReadMdlHeader01("MDL File Header 01")

		mdlFile.ReadTexturePaths()
		mdlFile.ReadTextures()
		mdlFile.ReadSequenceDescs()
	End Sub

	Protected Overrides Function WriteMeshSmdFiles(ByVal modelOutputPath As String, ByVal lodStartIndex As Integer, ByVal lodStopIndex As Integer) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		Dim smdFileName As String
		Dim smdPathFileName As String
		Dim aBodyPart As SourceVtxBodyPart07
		Dim aVtxModel As SourceVtxModel07
		Dim aBodyModel As SourceMdlModel
		Dim bodyPartVertexIndexStart As Integer

		bodyPartVertexIndexStart = 0
		If Me.theVtxFileData.theVtxBodyParts IsNot Nothing AndAlso Me.theMdlFileData.theBodyParts IsNot Nothing Then
			For bodyPartIndex As Integer = 0 To Me.theVtxFileData.theVtxBodyParts.Count - 1
				aBodyPart = Me.theVtxFileData.theVtxBodyParts(bodyPartIndex)

				If aBodyPart.theVtxModels IsNot Nothing Then
					For modelIndex As Integer = 0 To aBodyPart.theVtxModels.Count - 1
						aVtxModel = aBodyPart.theVtxModels(modelIndex)

						If aVtxModel.theVtxModelLods IsNot Nothing Then
							aBodyModel = Me.theMdlFileData.theBodyParts(bodyPartIndex).theModels(modelIndex)
							If aBodyModel.name(0) = ChrW(0) AndAlso aVtxModel.theVtxModelLods(0).theVtxMeshes Is Nothing Then
								Continue For
							End If

							For lodIndex As Integer = lodStartIndex To lodStopIndex
								'TODO: Why would this count be different than the file header count?
								If lodIndex >= aVtxModel.theVtxModelLods.Count Then
									Exit For
								End If

								smdFileName = SourceFileNamesModule.CreateBodyGroupSmdFileName(aBodyModel.theSmdFileNames(lodIndex), bodyPartIndex, modelIndex, lodIndex, Me.theName, Me.theMdlFileData.theBodyParts(bodyPartIndex).theModels(modelIndex).name)
								smdPathFileName = Path.Combine(modelOutputPath, smdFileName)

								Me.NotifySourceModelProgress(ProgressOptions.WritingFileStarted, smdPathFileName)
								'NOTE: Check here in case writing is canceled in the above event.
								If Me.theWritingIsCanceled Then
									status = StatusMessage.Canceled
									Return status
								ElseIf Me.theWritingSingleFileIsCanceled Then
									Me.theWritingSingleFileIsCanceled = False
									Continue For
								End If

								Me.WriteMeshSmdFile(smdPathFileName, lodIndex, aVtxModel, aBodyModel, bodyPartVertexIndexStart)

								Me.NotifySourceModelProgress(ProgressOptions.WritingFileFinished, smdPathFileName)
							Next

							bodyPartVertexIndexStart += aBodyModel.vertexCount
						End If
					Next
				End If
			Next
		End If

		Return status
	End Function

	Protected Overrides Sub WriteMeshSmdFile(ByVal lodIndex As Integer, ByVal aVtxModel As SourceVtxModel07, ByVal aModel As SourceMdlModel, ByVal bodyPartVertexIndexStart As Integer)
		Dim smdFile As New SourceSmdFile53(Me.theOutputFileTextWriter, Me.theMdlFileData, Me.theVvdFileData49)

		Try
			smdFile.WriteHeaderComment()

			smdFile.WriteHeaderSection()
			smdFile.WriteNodesSection(lodIndex)
			smdFile.WriteSkeletonSection(lodIndex)
			smdFile.WriteTrianglesSection(aVtxModel, lodIndex, aModel, bodyPartVertexIndexStart)
		Catch ex As Exception
			Dim debug As Integer = 4242
		End Try
	End Sub

	Protected Overrides Sub WritePhysicsMeshSmdFile()
		Dim physicsMeshSmdFile As New SourceSmdFile53(Me.theOutputFileTextWriter, Me.theMdlFileData, Me.thePhyFileDataGeneric)

		Try
			physicsMeshSmdFile.WriteHeaderComment()

			physicsMeshSmdFile.WriteHeaderSection()
			physicsMeshSmdFile.WriteNodesSection(-1)
			physicsMeshSmdFile.WriteSkeletonSection(-1)
			physicsMeshSmdFile.WriteTrianglesSectionForPhysics()
		Catch ex As Exception
			Dim debug As Integer = 4242
		Finally
		End Try
	End Sub

	Protected Overrides Sub WriteVrdFile()
		Dim vrdFile As New SourceVrdFile53(Me.theOutputFileTextWriter, Me.theMdlFileData)

		Try
			vrdFile.WriteHeaderComment()
			vrdFile.WriteCommands()
		Catch ex As Exception
			Dim debug As Integer = 4242
		Finally
		End Try
	End Sub

	Protected Overrides Sub WriteDeclareSequenceQciFile()
		Dim qciFile As New SourceQcFile53(Me.theOutputFileTextWriter, Me.theMdlFileData, Me.theName)

		Try
			qciFile.WriteHeaderComment()

			qciFile.WriteQciDeclareSequenceLines()
		Catch ex As Exception
			Dim debug As Integer = 4242
		End Try
	End Sub

	Protected Overrides Sub WriteBoneAnimationSmdFile(ByVal aSequenceDesc As SourceMdlSequenceDescBase, ByVal anAnimationDesc As SourceMdlAnimationDescBase)
		Dim smdFile As New SourceSmdFile53(Me.theOutputFileTextWriter, Me.theMdlFileData)

		Try
			smdFile.WriteHeaderComment()

			smdFile.WriteHeaderSection()
			smdFile.WriteNodesSection(-1)
			If Me.theMdlFileData.theFirstAnimationDesc IsNot Nothing AndAlso Me.theMdlFileData.theFirstAnimationDescFrameLines.Count = 0 Then
				smdFile.CalculateFirstAnimDescFrameLinesForSubtract()
			End If
			'If anAnimationDesc.animBlock > 0 AndAlso Me.theSourceEngineModel.MdlFileHeader.version >= 49 AndAlso Me.theSourceEngineModel.MdlFileHeader.version <> 2531 Then
			'	smdFile.WriteSkeletonSectionForAnimationAni_VERSION49(aSequenceDesc, anAnimationDesc)
			'Else
			'End If
			smdFile.WriteSkeletonSectionForAnimation(aSequenceDesc, anAnimationDesc)
		Catch ex As Exception
			Dim debug As Integer = 4242
		End Try
	End Sub

	Protected Overrides Sub WriteVertexAnimationVtaFile(ByVal bodyPart As SourceMdlBodyPart)
		Dim vertexAnimationVtaFile As New SourceVtaFile53(Me.theOutputFileTextWriter, Me.theMdlFileData, Me.theVvdFileData49)

		Try
			vertexAnimationVtaFile.WriteHeaderComment()

			vertexAnimationVtaFile.WriteHeaderSection()
			vertexAnimationVtaFile.WriteNodesSection()
			vertexAnimationVtaFile.WriteSkeletonSectionForVertexAnimation()
			vertexAnimationVtaFile.WriteVertexAnimationSection()
		Catch ex As Exception
			Dim debug As Integer = 4242
		Finally
		End Try
	End Sub

	Public Overrides Function WriteAccessedBytesDebugFiles(ByVal debugPath As String) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		Dim debugPathFileName As String

		If Me.theMdlFileDataGeneric IsNot Nothing Then
			debugPathFileName = Path.Combine(debugPath, Me.theName + " " + ResourceStrings.GetString(ResourceStrings.Entry.Decompile_DebugMdlFileNameSuffix))
			Me.NotifySourceModelProgress(ProgressOptions.WritingFileStarted, debugPathFileName)
			Me.WriteAccessedBytesDebugFile(debugPathFileName, Me.theMdlFileDataGeneric.theFileSeekLog)
			Me.NotifySourceModelProgress(ProgressOptions.WritingFileFinished, debugPathFileName)
		End If

		If Me.theAniFileDataGeneric IsNot Nothing Then
			debugPathFileName = Path.Combine(debugPath, Me.theName + " " + ResourceStrings.GetString(ResourceStrings.Entry.Decompile_DebugAniFileNameSuffix))
			Me.NotifySourceModelProgress(ProgressOptions.WritingFileStarted, debugPathFileName)
			Me.WriteAccessedBytesDebugFile(debugPathFileName, Me.theAniFileDataGeneric.theFileSeekLog)
			Me.NotifySourceModelProgress(ProgressOptions.WritingFileFinished, debugPathFileName)
		End If

		If Me.theVtxFileData IsNot Nothing Then
			debugPathFileName = Path.Combine(debugPath, Me.theName + " " + ResourceStrings.GetString(ResourceStrings.Entry.Decompile_DebugVtxFileNameSuffix))
			Me.NotifySourceModelProgress(ProgressOptions.WritingFileStarted, debugPathFileName)
			Me.WriteAccessedBytesDebugFile(debugPathFileName, Me.theVtxFileData.theFileSeekLog)
			Me.NotifySourceModelProgress(ProgressOptions.WritingFileFinished, debugPathFileName)
		End If

		If Me.theVvdFileData49 IsNot Nothing Then
			debugPathFileName = Path.Combine(debugPath, Me.theName + " " + ResourceStrings.GetString(ResourceStrings.Entry.Decompile_DebugVvdFileNameSuffix))
			Me.NotifySourceModelProgress(ProgressOptions.WritingFileStarted, debugPathFileName)
			Me.WriteAccessedBytesDebugFile(debugPathFileName, Me.theVvdFileData49.theFileSeekLog)
			Me.NotifySourceModelProgress(ProgressOptions.WritingFileFinished, debugPathFileName)
		End If

		If Me.thePhyFileDataGeneric IsNot Nothing Then
			debugPathFileName = Path.Combine(debugPath, Me.theName + " " + ResourceStrings.GetString(ResourceStrings.Entry.Decompile_DebugPhyFileNameSuffix))
			Me.NotifySourceModelProgress(ProgressOptions.WritingFileStarted, debugPathFileName)
			Me.WriteAccessedBytesDebugFile(debugPathFileName, Me.thePhyFileDataGeneric.theFileSeekLog)
			Me.NotifySourceModelProgress(ProgressOptions.WritingFileFinished, debugPathFileName)
		End If

		Return status
	End Function

	Protected Overrides Sub WriteMdlFileNameToMdlFile(ByVal internalMdlFileName As String)
		Dim mdlFile As New SourceMdlFile53(Me.theOutputFileBinaryWriter, Me.theMdlFileData)

		mdlFile.WriteInternalMdlFileName(internalMdlFileName)
	End Sub

#End Region

#Region "Data"

	'Private theAniFileData49 As SourceAniFileData49
	Private theAniFileData49 As SourceMdlFileData49
	Private theMdlFileData As SourceMdlFileData53
	'Private thePhyFileData49 As SourcePhyFileData49
	Private theVtxFileData As SourceVtxFileData07
	Private theVvdFileData49 As SourceVvdFileData04

#End Region

End Class
