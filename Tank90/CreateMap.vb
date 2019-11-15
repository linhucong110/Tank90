
Imports System.IO
Imports System.Text

Public Class CreateMap

    Dim imgPos As ImagePosition
    Dim bitMap As Bitmap
    Dim bytMap(25, 25) As Integer
    Dim intSelectMapObject As Integer
    Dim G As Graphics

    Dim blnSaveMap As Boolean
    Dim blnModify As Boolean
    Dim intModifyStage As Integer

    Private Sub NewMap()
        bitMap = New Bitmap(26 * 16, 26 * 16)
        G = Graphics.FromImage(bitMap)
        G.Clear(Color.Black)

        Dim iRow, iCol As Integer

        Array.Clear(bytMap, 0, bytMap.Length)

        For iCol = 0 To 25
            G.DrawLine(Pens.White, New Point(iCol * 16, 0), New Point(iCol * 16, 26 * 16))
        Next

        For iRow = 0 To 25
            G.DrawLine(Pens.White, New Point(0, iRow * 16), New Point(26 * 16, iRow * 16))
        Next

        For iRow = 23 To 25
            G.DrawImage(imgPos.Brick, 11 * 16, iRow * 16)
            G.DrawImage(imgPos.Brick, 14 * 16, iRow * 16)
            bytMap(iRow, 11) = Base_Object.Object_Type.Bulid_Brick
            bytMap(iRow, 14) = Base_Object.Object_Type.Bulid_Brick
        Next
        bytMap(23, 12) = Base_Object.Object_Type.Bulid_Brick
        bytMap(23, 13) = Base_Object.Object_Type.Bulid_Brick
        bytMap(24, 12) = Base_Object.Object_Type.Bulid_Base

        G.DrawImage(imgPos.Brick, 12 * 16, 23 * 16)
        G.DrawImage(imgPos.Brick, 13 * 16, 23 * 16)
        G.DrawImage(imgPos.Base, 12 * 16, 24 * 16)

        G.DrawImage(imgPos.dicImageList("Army_Disp_3"), 0 * 16, 0 * 16)
        G.DrawImage(imgPos.dicImageList("Army_Disp_3"), 12 * 16, 0 * 16)
        G.DrawImage(imgPos.dicImageList("Army_Disp_3"), 24 * 16, 0 * 16)

        G.DrawImage(imgPos.dicImageList("Army_Disp_3"), 9 * 16, 24 * 16)
        G.DrawImage(imgPos.dicImageList("Army_Disp_3"), 15 * 16, 24 * 16)

        picMap.Image = bitMap

    End Sub

    Private Sub CreateMap_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        MainForm.WindowState = FormWindowState.Normal
        MainForm.Activate()
    End Sub

    Private Sub CreateMap_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim bitBlank As Bitmap
        bitBlank = New Bitmap(32, 32)
        G = Graphics.FromImage(bitBlank)
        G.Clear(Color.Black)

        imgPos = New ImagePosition()
        imgPos.Sprite_ImgPath = Application.StartupPath & "\Resource\pic\tank_sprite.png"

        ImageList1.Images.Add(imgPos.Brick)
        ImageList1.Images.Add(imgPos.Steel)
        ImageList1.Images.Add(imgPos.River)
        ImageList1.Images.Add(imgPos.Grass)
        ImageList1.Images.Add(imgPos.Snow)
        ImageList1.Images.Add(bitBlank)

        ListView1.Items.Add("砖块", 0).Tag = Base_Object.Object_Type.Bulid_Brick
        ListView1.Items.Add("钢块", 1).Tag = Base_Object.Object_Type.Bulid_Steel
        ListView1.Items.Add("河流", 2).Tag = Base_Object.Object_Type.Bulid_River
        ListView1.Items.Add("树木", 3).Tag = Base_Object.Object_Type.Bulid_Grass
        ListView1.Items.Add("雪地", 4).Tag = Base_Object.Object_Type.Bulid_Snow
        ListView1.Items.Add("空白", 5).Tag = 0

        NewMap()

        ReadMap()

        Dim i As Integer
        lstMap.Items.Clear()

        For i = 0 To Map_Stage_List.Count - 1
            lstMap.Items.Add("第 " & Format(i + 1, "##") & " 关")
        Next


        MainForm_Resize(sender, e)
    End Sub

    Private Sub MainForm_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        If Me.Width < 595 Then Me.Width = 595
        If Me.Height < 498 Then Me.Height = 498

        picMap.Top = 30
        picMap.Left = 3
        picStatus.Top = 30
        picMap.Height = (Me.DisplayRectangle.Height - 36)
        picStatus.Height = picMap.Height
        picMap.Width = picMap.Height
        picMap.Left = (Me.DisplayRectangle.Width - picMap.Width - picStatus.Width - 9) / 2
        picStatus.Left = picMap.Left + picMap.Width + 3

        ListView1.Top = picStatus.Top
        ListView1.Left = picStatus.Left

        lstMap.Left = picStatus.Left
        lstMap.Top = picStatus.Height - lstMap.Height + picStatus.Top
        ListView1.Height = lstMap.Top - ListView1.Top
        'ListView1.Height = picStatus.Height

    End Sub

    Private Sub SetMapValue(ByVal intX As Integer, ByVal intY As Integer, ByVal intValue As Integer)
        If intX > 25 Or intX < 0 Then Exit Sub
        If intY > 25 Or intY < 0 Then Exit Sub

        bytMap(intY, intX) = intValue
    End Sub


    Private Sub ListView1_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles ListView1.ItemCheck
        If e.Index > -1 Then
            intSelectMapObject = ListView1.Items(e.Index).Tag
        End If
    End Sub

    Private Sub picMap_MouseClick(sender As Object, e As MouseEventArgs) Handles picMap.MouseClick
        Dim intX, intY As Integer
        Dim bitBlank As Bitmap
        Dim tmpG As Graphics

        bitBlank = New Bitmap(16, 16)
        tmpG = Graphics.FromImage(bitBlank)
        tmpG.Clear(Color.Black)
        tmpG.DrawRectangle(Pens.White, New Rectangle(0, 0, 16, 16))

        intX = e.X \ (16 * picMap.Width / 416)
        intY = e.Y \ (16 * picMap.Height / 416)

        If (intX = 12 And intY = 24) OrElse (intX = 13 And intY = 24) _
            OrElse (intX = 12 And intY = 25) OrElse (intX = 13 And intY = 25) Then

            MessageBox.Show("基地位置不能画其它内容!")
            
        Else
            SetMapValue(intX, intY, intSelectMapObject)

            If ListView1.SelectedItems.Count > 0 Then
                Select Case ListView1.SelectedItems(0).ImageIndex
                    Case 0  '砖块
                        G.DrawImage(imgPos.Brick, intX * 16, intY * 16)

                    Case 1  '钢块
                        G.DrawImage(imgPos.Steel, intX * 16, intY * 16)

                    Case 2  '河流
                        G.DrawImage(imgPos.River, intX * 16, intY * 16)

                    Case 3  '树林
                        G.DrawImage(imgPos.Grass, intX * 16, intY * 16)

                    Case 4  '雪地
                        G.DrawImage(imgPos.Snow, intX * 16, intY * 16)

                    Case 5  '空白
                        G.DrawImage(bitBlank, intX * 16, intY * 16)

                End Select
            End If

        End If

        picMap.Image = bitMap
    End Sub

    Private Sub picMap_MouseMove(sender As Object, e As MouseEventArgs) Handles picMap.MouseMove
        If e.Button = Windows.Forms.MouseButtons.Left Then
            Dim intX, intY As Integer
            Dim bitBlank As Bitmap
            Dim tmpG As Graphics

            bitBlank = New Bitmap(16, 16)
            tmpG = Graphics.FromImage(bitBlank)
            tmpG.Clear(Color.Black)
            tmpG.DrawRectangle(Pens.White, New Rectangle(0, 0, 16, 16))

            intX = e.X \ (16 * picMap.Width / 416)
            intY = e.Y \ (16 * picMap.Height / 416)

            If (intX = 12 And intY = 24) OrElse (intX = 13 And intY = 24) _
                OrElse (intX = 12 And intY = 25) OrElse (intX = 13 And intY = 25) Then

                MessageBox.Show("基地位置不能画其它内容!")

            Else
                SetMapValue(intX, intY, intSelectMapObject)

                If ListView1.SelectedItems.Count > 0 Then
                    Select Case ListView1.SelectedItems(0).ImageIndex
                        Case 0  '砖块
                            G.DrawImage(imgPos.Brick, intX * 16, intY * 16)

                        Case 1  '钢块
                            G.DrawImage(imgPos.Steel, intX * 16, intY * 16)

                        Case 2  '河流
                            G.DrawImage(imgPos.River, intX * 16, intY * 16)

                        Case 3  '树林
                            G.DrawImage(imgPos.Grass, intX * 16, intY * 16)

                        Case 4  '雪地
                            G.DrawImage(imgPos.Snow, intX * 16, intY * 16)

                        Case 5  '空白
                            G.DrawImage(bitBlank, intX * 16, intY * 16)

                    End Select
                End If
            End If

            picMap.Image = bitMap
        End If
    End Sub

    Private Sub 保存地图ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 保存地图ToolStripMenuItem.Click
        SaveMap()
    End Sub

    Private Sub 创建新地图ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 创建新地图ToolStripMenuItem.Click
        If Not blnSaveMap Then
            If MessageBox.Show(Me, "地图未保存,你要先保存地图吗?", "保存地图", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                SaveMap()
            End If
        End If

        NewMap()
        blnSaveMap = False
    End Sub

    Private Sub SaveMap()
        Dim strMapVal As String = ""
        Dim iRow, iCol As Integer

        For iRow = 0 To 25
            For iCol = 0 To 25
                strMapVal = strMapVal & bytMap(iRow, iCol) & ","
            Next
        Next
        strMapVal = Strings.Left(strMapVal, strMapVal.Length - 1)

        If blnModify Then
            '修改地图
            Map_Stage_List.Item(intModifyStage) = strMapVal
            blnModify = False
            blnSaveMap = True
        Else
            '添加新地图
            Map_Stage_List.Add(strMapVal)
            blnSaveMap = True

            lstMap.Items.Add("第 " & Format(lstMap.Items.Count + 1, "##") & " 关")
        End If

        Dim MapStreamW As IO.StreamWriter

        Try
            MapStreamW = New IO.StreamWriter(strMapFile, False)

            For iRow = 0 To Map_Stage_List.Count - 1
                MapStreamW.WriteLine(Map_Stage_List.Item(iRow))
            Next
            MapStreamW.Flush()
            MapStreamW.Close()

            MessageBox.Show("地图保存完成!")
            NewMap()
        Catch ex As Exception
            MessageBox.Show("地图保存出错!" & vbCrLf & ex.Message)
        End Try

    End Sub

    
    Private Sub LoadMap(ByVal intIndex As Integer)
        Dim iRow, iCol, intCnt As Integer
        Dim arrVal() As String

        If intIndex > -1 Then
            arrVal = Strings.Split(Map_Stage_List.Item(intIndex), ",")

            intCnt = 0
            G.Clear(Color.Black)

            For iRow = 0 To 25
                For iCol = 0 To 25
                    bytMap(iRow, iCol) = CInt(arrVal(intCnt))
                    intCnt += 1

                    Select Case bytMap(iRow, iCol)
                        Case Base_Object.Object_Type.Bulid_Brick  '砖块
                            G.DrawImage(imgPos.Brick, iCol * 16, iRow * 16)

                        Case Base_Object.Object_Type.Bulid_Steel  '钢块
                            G.DrawImage(imgPos.Steel, iCol * 16, iRow * 16)

                        Case Base_Object.Object_Type.Bulid_River  '河流
                            G.DrawImage(imgPos.River, iCol * 16, iRow * 16)

                        Case Base_Object.Object_Type.Bulid_Grass  '树林
                            G.DrawImage(imgPos.Grass, iCol * 16, iRow * 16)

                        Case Base_Object.Object_Type.Bulid_Snow  '雪地
                            G.DrawImage(imgPos.Snow, iCol * 16, iRow * 16)

                        Case Base_Object.Object_Type.Bulid_Base  '基地老鹰
                            G.DrawImage(imgPos.Base, iCol * 16, iRow * 16)
                    End Select

                Next
            Next
            picMap.Image = bitMap
        End If
    End Sub

    Private Sub lstMap_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles lstMap.DoubleClick
        If lstMap.Items.Count > 0 Then
            If lstMap.SelectedIndex > -1 Then
                LoadMap(lstMap.SelectedIndex)
            End If
        End If
    End Sub

    Private Sub 修改地图ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 修改地图ToolStripMenuItem.Click
        If lstMap.SelectedIndex > -1 Then
            LoadMap(lstMap.SelectedIndex)
            blnModify = True
            intModifyStage = lstMap.SelectedIndex
        End If
    End Sub

    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView1.SelectedIndexChanged
        If ListView1.SelectedItems.Count > 0 Then
            intSelectMapObject = ListView1.Items(ListView1.SelectedIndices(0)).Tag
        End If
    End Sub

    Private Sub 查看地图ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 查看地图ToolStripMenuItem.Click
        If lstMap.Items.Count > 0 Then
            If lstMap.SelectedIndex > -1 Then
                LoadMap(lstMap.SelectedIndex)
            End If
        End If
    End Sub

    Private Sub 删除地图ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 删除地图ToolStripMenuItem.Click
        Dim i As Integer

        If lstMap.Items.Count > 0 Then
            If lstMap.SelectedIndex > -1 Then
                'LoadMap(lstMap.SelectedIndex)
                Map_Stage_List.RemoveAt(lstMap.SelectedIndex)

                lstMap.Items.Clear()

                For i = 0 To Map_Stage_List.Count - 1
                    lstMap.Items.Add("第 " & Format(i + 1, "##") & " 关")
                Next
            End If
        End If
    End Sub
End Class