
Public Class MapContainer
    Public Width As Integer
    Public Height As Integer
    Public Scale As Integer

    Dim bytMap(,) As Integer
    Public Image_Position As ImagePosition
    Dim MapBitmap As Bitmap
    Public G As Graphics

    Public MapObjectList As New Dictionary(Of String, Base_Object)
    Public GrassList As New List(Of Base_Object)

    Public Sub New()
        Width = 26
        Height = 26
        Scale = 1
        ReDim bytMap(Width - 1, Height - 1)
        MapBitmap = New Bitmap(Width * 16 * Scale, Height * 16 * Scale)
        G = Graphics.FromImage(MapBitmap)
        'G.PageUnit = GraphicsUnit.Pixel
    End Sub

    Public Sub New(ByVal intW As Integer, ByVal intH As Integer, Optional ScaleXY As Integer = 1)
        Width = intW
        Height = intH
        Scale = ScaleXY
        ReDim bytMap(Width - 1, Height - 1)
        MapBitmap = New Bitmap(Width * 16 * Scale, Height * 16 * Scale)
        G = Graphics.FromImage(MapBitmap)
        'G.PageUnit = GraphicsUnit.Pixel
    End Sub

    Public Property MapValue As Integer(,)
        Get
            Return bytMap
        End Get
        Set(value As Integer(,))
            bytMap = value

            Dim iRow, iCol As Integer
            Dim MapObject As Base_Object

            MapObjectList.Clear()
            GrassList.Clear()

            For iRow = 0 To Height - 1
                For iCol = 0 To Width - 1
                    Select Case bytMap(iRow, iCol)
                        Case Base_Object.Object_Type.Bulid_Base
                            MapObject = New Base_Object
                            MapObject.Image = Image_Position.Base
                            MapObject.Type = Base_Object.Object_Type.Bulid_Base
                            MapObject.Position = New Point(iCol, iRow)
                            If Not MapObjectList.ContainsKey(CStr(iRow & "_" & iCol)) Then
                                MapObjectList.Add(CStr(iRow & "_" & iCol), MapObject)
                            End If
                            'G.DrawImage(ImagePosInfo.Base, iCol * 16 * Scale, iRow * 16 * Scale, 34 * Scale, 34 * Scale)

                        Case Base_Object.Object_Type.Bulid_BaseBroken
                            'G.DrawImage(ImagePosInfo.BaseBroken, iCol * 16 * Scale, iRow * 16 * Scale, 34 * Scale, 34 * Scale)
                            MapObject = New Base_Object
                            MapObject.Image = Image_Position.BaseBroken
                            MapObject.Type = Base_Object.Object_Type.Bulid_BaseBroken
                            MapObject.Position = New Point(iCol, iRow)
                            If Not MapObjectList.ContainsKey(CStr(iRow & "_" & iCol)) Then
                                MapObjectList.Add(CStr(iRow & "_" & iCol), MapObject)
                            End If

                        Case Base_Object.Object_Type.Bulid_Brick
                            'G.DrawImage(ImagePosInfo.BaseBroken, iCol * 16 * Scale, iRow * 16 * Scale, 34 * Scale, 34 * Scale)

                            MapObject = New Base_Object
                            MapObject.Image = Image_Position.Brick
                            MapObject.Type = Base_Object.Object_Type.Bulid_Brick
                            MapObject.Position = New Point(iCol, iRow)
                            If Not MapObjectList.ContainsKey(CStr(iRow & "_" & iCol)) Then
                                MapObjectList.Add(CStr(iRow & "_" & iCol), MapObject)
                            End If

                        Case Base_Object.Object_Type.Bulid_Grass
                            MapObject = New Base_Object
                            MapObject.Image = Image_Position.Grass
                            MapObject.Type = Base_Object.Object_Type.Bulid_Grass
                            MapObject.Position = New Point(iCol, iRow)
                            GrassList.Add(MapObject)

                        Case Base_Object.Object_Type.Bulid_River
                            MapObject = New Base_Object
                            MapObject.Image = Image_Position.River
                            MapObject.Type = Base_Object.Object_Type.Bulid_River
                            MapObject.Position = New Point(iCol, iRow)
                            If Not MapObjectList.ContainsKey(CStr(iRow & "_" & iCol)) Then
                                MapObjectList.Add(CStr(iRow & "_" & iCol), MapObject)
                            End If

                        Case Base_Object.Object_Type.Bulid_Snow
                            MapObject = New Base_Object
                            MapObject.Image = Image_Position.Snow
                            MapObject.Type = Base_Object.Object_Type.Bulid_Snow
                            MapObject.Position = New Point(iCol, iRow)
                            If Not MapObjectList.ContainsKey(CStr(iRow & "_" & iCol)) Then
                                MapObjectList.Add(CStr(iRow & "_" & iCol), MapObject)
                            End If

                        Case Base_Object.Object_Type.Bulid_Steel
                            MapObject = New Base_Object
                            MapObject.Image = Image_Position.Steel
                            MapObject.Type = Base_Object.Object_Type.Bulid_Steel
                            MapObject.Position = New Point(iCol, iRow)
                            If Not MapObjectList.ContainsKey(CStr(iRow & "_" & iCol)) Then
                                MapObjectList.Add(CStr(iRow & "_" & iCol), MapObject)
                            End If

                    End Select

                Next
            Next
        End Set
    End Property

    Public Sub SetMapValue(ByVal iRow As Integer, ByVal iCol As Integer, ByVal intValue As Integer)
        bytMap(iRow, iCol) = intValue
    End Sub

    Public ReadOnly Property Image As Bitmap
        Get
            Return MapBitmap
        End Get
    End Property

    Public Sub ClearMap()
        G.Clear(Color.Black)
    End Sub

    Public Sub RenderMap()
        Dim i As Integer
        Dim tmpMapObj As Base_Object
        ClearMap()

        For i = 0 To MapObjectList.Count - 1
            tmpMapObj = MapObjectList.Values(i)
            G.DrawImage(tmpMapObj.Image, tmpMapObj.Rect.X * Scale, tmpMapObj.Rect.Y * Scale, tmpMapObj.Rect.Width * Scale, tmpMapObj.Rect.Height * Scale)
        Next

    End Sub

    Public Sub RenderGrass()
        Dim i As Integer
        Dim tmpMapObj As Base_Object

        For i = 0 To GrassList.Count - 1
            tmpMapObj = GrassList.Item(i)
            G.DrawImage(tmpMapObj.Image, tmpMapObj.Rect.X * Scale, tmpMapObj.Rect.Y * Scale, tmpMapObj.Rect.Width * Scale, tmpMapObj.Rect.Height * Scale)
        Next
    End Sub


End Class
