
Public Class MainForm

    'Private WithEvents Keyboard As New SystemHook()
    Private Delegate Sub dlg_Play_Control(ByVal intDirection As Base_Object.Direction, ByVal intPlay As Base_Object.Object_Type, ByVal blnKeyDown As Boolean)

    Dim bitGame_Start As Bitmap
    Public intSelect As Integer
    Dim dlgSub As dlg_Play_Control
    Dim blnKeyState(255) As Boolean

    'Private Sub MainForm_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
    '    Dim dlgSub As dlg_Play_Control
    '    dlgSub = New dlg_Play_Control(AddressOf Play_Control)

    '    If blnBeginGame Then

    '        If e.KeyValue = Keys.Enter Then
    '            Enemy_LifeCount = 20
    '            Game_Start()
    '            Timer1.Start()
    '        End If

    '        'Play 1P
    '        If e.KeyValue = Keys.W Then
    '            e.Handled = True
    '            'Play_Control(Base_Object.Direction.Up, Base_Object.Object_Type.Tank_IP, True)
    '            Me.BeginInvoke(dlgSub, New Object() {Base_Object.Direction.Up, Base_Object.Object_Type.Tank_IP, True})
    '        End If

    '        If e.KeyValue = Keys.D Then
    '            e.Handled = True
    '            'Play_Control(Base_Object.Direction.Right, Base_Object.Object_Type.Tank_IP, True)
    '            Me.BeginInvoke(dlgSub, New Object() {Base_Object.Direction.Right, Base_Object.Object_Type.Tank_IP, True})
    '        End If

    '        If e.KeyValue = Keys.S Then
    '            e.Handled = True
    '            'Play_Control(Base_Object.Direction.Down, Base_Object.Object_Type.Tank_IP, True)
    '            Me.BeginInvoke(dlgSub, New Object() {Base_Object.Direction.Down, Base_Object.Object_Type.Tank_IP, True})
    '        End If
    '        If e.KeyValue = Keys.A Then
    '            e.Handled = True
    '            'Play_Control(Base_Object.Direction.Left, Base_Object.Object_Type.Tank_IP, True)
    '            Me.BeginInvoke(dlgSub, New Object() {Base_Object.Direction.Left, Base_Object.Object_Type.Tank_IP, True})
    '        End If

    '        If e.KeyValue = Keys.Space Then
    '            e.Handled = True
    '            If Not IsNothing(Tank_IP) AndAlso Tank_IP.Broken = False Then
    '                Tank_IP.Shoot()
    '            End If
    '        End If

    '        'Play 2P
    '        If e.KeyValue = Keys.Up Then
    '            e.Handled = True
    '            'Play_Control(Base_Object.Direction.Up, Base_Object.Object_Type.Tank_IIP, True)
    '            Me.BeginInvoke(dlgSub, New Object() {Base_Object.Direction.Up, Base_Object.Object_Type.Tank_IIP, True})
    '        End If

    '        If e.KeyValue = Keys.Right Then
    '            e.Handled = True
    '            'Play_Control(Base_Object.Direction.Right, Base_Object.Object_Type.Tank_IIP, True)
    '            Me.BeginInvoke(dlgSub, New Object() {Base_Object.Direction.Right, Base_Object.Object_Type.Tank_IIP, True})
    '        End If

    '        If e.KeyValue = Keys.Down Then
    '            e.Handled = True
    '            'Play_Control(Base_Object.Direction.Down, Base_Object.Object_Type.Tank_IIP, True)
    '            Me.BeginInvoke(dlgSub, New Object() {Base_Object.Direction.Down, Base_Object.Object_Type.Tank_IIP, True})
    '        End If
    '        If e.KeyValue = Keys.Left Then
    '            e.Handled = True
    '            'Play_Control(Base_Object.Direction.Left, Base_Object.Object_Type.Tank_IIP, True)
    '            Me.BeginInvoke(dlgSub, New Object() {Base_Object.Direction.Left, Base_Object.Object_Type.Tank_IIP, True})
    '        End If

    '        If e.KeyValue = Keys.PageDown Then
    '            e.Handled = True
    '            If Not IsNothing(Tank_IIP) AndAlso Tank_IIP.Broken = False Then
    '                Tank_IIP.Shoot()
    '            End If
    '        End If

    '    Else
    '        If blnGame_Over Then
    '            If e.KeyValue = Keys.Enter Then
    '                blnGame_Over = False
    '                DrawStartSelect(0)
    '            End If

    '        Else
    '            If e.KeyValue = Keys.Up Then
    '                intSelect -= 1
    '                If intSelect < 0 Then
    '                    intSelect = 2
    '                End If
    '                DrawStartSelect(intSelect)
    '            End If

    '            If e.KeyValue = Keys.Down Then
    '                intSelect += 1
    '                If intSelect > 2 Then
    '                    intSelect = 0
    '                End If
    '                DrawStartSelect(intSelect)
    '            End If

    '            If e.KeyValue = Keys.Enter Then
    '                Select Case intSelect
    '                    Case 0 '开始1P
    '                        blnBeginGame = True
    '                        Play_1P_LifeCount = 10
    '                        Play_2P_LifeCount = 0
    '                        Game_Start()
    '                        Timer1.Start()

    '                    Case 1 '开始2P
    '                        blnBeginGame = True
    '                        Play_1P_LifeCount = 10
    '                        Play_2P_LifeCount = 10
    '                        Game_Start()
    '                        Timer1.Start()

    '                    Case 2 '创建地图
    '                        Me.WindowState = FormWindowState.Minimized
    '                        CreateMap.Show()
    '                        CreateMap.Activate()
    '                End Select


    '            End If
    '        End If


    '    End If

    'End Sub

    'Private Sub MainForm_KeyUp(sender As Object, e As KeyEventArgs) Handles Me.KeyUp

    '    If blnBeginGame Then
    '        'Play 1P
    '        If e.KeyValue = Keys.W Then
    '            Play_Control(Base_Object.Direction.Up, Base_Object.Object_Type.Tank_IP, False)
    '        End If

    '        If e.KeyValue = Keys.D Then
    '            Play_Control(Base_Object.Direction.Right, Base_Object.Object_Type.Tank_IP, False)
    '        End If

    '        If e.KeyValue = Keys.S Then
    '            Play_Control(Base_Object.Direction.Down, Base_Object.Object_Type.Tank_IP, False)
    '        End If

    '        If e.KeyValue = Keys.A Then
    '            Play_Control(Base_Object.Direction.Left, Base_Object.Object_Type.Tank_IP, False)
    '        End If

    '        'Play 2P
    '        If e.KeyValue = Keys.Up Then
    '            Play_Control(Base_Object.Direction.Up, Base_Object.Object_Type.Tank_IIP, False)
    '        End If

    '        If e.KeyValue = Keys.Right Then
    '            Play_Control(Base_Object.Direction.Right, Base_Object.Object_Type.Tank_IIP, False)
    '        End If

    '        If e.KeyValue = Keys.Down Then
    '            Play_Control(Base_Object.Direction.Down, Base_Object.Object_Type.Tank_IIP, False)
    '        End If

    '        If e.KeyValue = Keys.Left Then
    '            Play_Control(Base_Object.Direction.Left, Base_Object.Object_Type.Tank_IIP, False)
    '        End If

    '    End If


    'End Sub


    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles Me.Load
        ImgPos = New ImagePosition
        ImgPos.Sprite_ImgPath = Application.StartupPath & "\Resource\pic\tank_sprite.png"

        MapContain = New MapContainer(26, 26, 1)
        MapContain.Image_Position = ImgPos

        MainForm_Resize(sender, e)

        CheckForIllegalCrossThreadCalls = False

        blnBeginGame = False

        bitGame_Start = Image.FromFile(Application.StartupPath & "\Resource\pic\tank_main.png")
        picMap.Image = bitGame_Start

        DrawStartSelect(0)
        ReadMap()

        My.Computer.Audio.Play(ImgPos.Audio_GameStar, AudioPlayMode.Background)

        dlgSub = New dlg_Play_Control(AddressOf Play_Control)

        'DrawPassStage()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Render()
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

    End Sub

    Private Sub DrawStartSelect(ByVal intPos As Integer)
        Dim tmpG As Graphics
        Dim tmpBit As Bitmap
        tmpBit = bitGame_Start.Clone
        tmpG = Graphics.FromImage(tmpBit)
        tmpG.DrawImage(ImgPos.dicImageList("1_1_1_0"), 190, 218 + intPos * 30, 20, 20)

        picMap.Image = tmpBit
    End Sub

    Private Sub 开始游戏ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 开始游戏ToolStripMenuItem.Click
        Timer1.Stop()

        My.Computer.Audio.Play(ImgPos.Audio_GameStar, AudioPlayMode.Background)
        DrawStartSelect(0)
        blnGame_Over = False
        blnBeginGame = False
    End Sub

    Private Sub 自定地图ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 自定地图ToolStripMenuItem.Click
        Me.WindowState = FormWindowState.Minimized
        CreateMap.Show()
        CreateMap.Activate()
    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        
        'Dim dlgSub As dlg_Play_Control
        'dlgSub = New dlg_Play_Control(AddressOf Play_Control)
        Dim intKey As Integer

        If blnBeginGame Then
            '检测各按键是否按下

            If CurrentKeyState(VirtualKeys.VK_RETURN) < 0 Then
                Enemy_LifeCount = 20
                Game_Start()
                Timer1.Start()
            End If

            'Play 1P
            intKey = CurrentKeyState(VirtualKeys.VK_W)
            If intKey < 0 Then
                'Play_Control(Base_Object.Direction.Up, Base_Object.Object_Type.Tank_IP, True)
                Me.Invoke(dlgSub, New Object() {Base_Object.Direction.Up, Base_Object.Object_Type.Tank_IP, True})
            ElseIf intKey = 1 Then
                Me.Invoke(dlgSub, New Object() {Base_Object.Direction.Up, Base_Object.Object_Type.Tank_IP, False})
            Else
                intKey = CurrentKeyState(VirtualKeys.VK_D)
                If intKey < 0 Then
                    'Play_Control(Base_Object.Direction.Right, Base_Object.Object_Type.Tank_IP, True)
                    Me.Invoke(dlgSub, New Object() {Base_Object.Direction.Right, Base_Object.Object_Type.Tank_IP, True})
                ElseIf intKey = 1 Then
                    Me.Invoke(dlgSub, New Object() {Base_Object.Direction.Right, Base_Object.Object_Type.Tank_IP, False})
                Else
                    intKey = CurrentKeyState(VirtualKeys.VK_S)
                    If intKey < 0 Then
                        'Play_Control(Base_Object.Direction.Down, Base_Object.Object_Type.Tank_IP, True)
                        Me.Invoke(dlgSub, New Object() {Base_Object.Direction.Down, Base_Object.Object_Type.Tank_IP, True})
                    ElseIf intKey = 1 Then
                        Me.Invoke(dlgSub, New Object() {Base_Object.Direction.Down, Base_Object.Object_Type.Tank_IP, False})
                    Else
                        intKey = CurrentKeyState(VirtualKeys.VK_A)
                        If intKey < 0 Then
                            'Play_Control(Base_Object.Direction.Left, Base_Object.Object_Type.Tank_IP, True)
                            Me.Invoke(dlgSub, New Object() {Base_Object.Direction.Left, Base_Object.Object_Type.Tank_IP, True})
                        ElseIf intKey = 1 Then
                            Me.Invoke(dlgSub, New Object() {Base_Object.Direction.Left, Base_Object.Object_Type.Tank_IP, False})
                        End If
                    End If
                End If
            End If

            If CurrentKeyState(VirtualKeys.VK_SPACE) Then
                If Not IsNothing(Tank_IP) AndAlso Tank_IP.Broken = False Then
                    Tank_IP.Shoot()
                End If
            End If

            'Play 2P
            intKey = CurrentKeyState(VirtualKeys.VK_UP)
            If intKey < 0 Then
                'Play_Control(Base_Object.Direction.Up, Base_Object.Object_Type.Tank_IIP, True)
                Me.Invoke(dlgSub, New Object() {Base_Object.Direction.Up, Base_Object.Object_Type.Tank_IIP, True})
            ElseIf intKey = 1 Then
                Me.Invoke(dlgSub, New Object() {Base_Object.Direction.Up, Base_Object.Object_Type.Tank_IIP, False})
            Else
                intKey = CurrentKeyState(Keys.Right)
                If intKey < 0 Then
                    'Play_Control(Base_Object.Direction.Right, Base_Object.Object_Type.Tank_IIP, True)
                    Me.Invoke(dlgSub, New Object() {Base_Object.Direction.Right, Base_Object.Object_Type.Tank_IIP, True})
                ElseIf intKey = 1 Then
                    Me.Invoke(dlgSub, New Object() {Base_Object.Direction.Right, Base_Object.Object_Type.Tank_IIP, False})
                Else
                    intKey = CurrentKeyState(Keys.Down)
                    If intKey < 0 Then
                        'Play_Control(Base_Object.Direction.Down, Base_Object.Object_Type.Tank_IIP, True)
                        Me.Invoke(dlgSub, New Object() {Base_Object.Direction.Down, Base_Object.Object_Type.Tank_IIP, True})
                    ElseIf intKey = 1 Then
                        Me.Invoke(dlgSub, New Object() {Base_Object.Direction.Down, Base_Object.Object_Type.Tank_IIP, False})
                    Else
                        intKey = CurrentKeyState(Keys.Left)
                        If intKey < 0 Then
                            'Play_Control(Base_Object.Direction.Left, Base_Object.Object_Type.Tank_IIP, True)
                            Me.Invoke(dlgSub, New Object() {Base_Object.Direction.Left, Base_Object.Object_Type.Tank_IIP, True})
                        ElseIf intKey = 1 Then
                            Me.Invoke(dlgSub, New Object() {Base_Object.Direction.Left, Base_Object.Object_Type.Tank_IIP, False})
                        End If
                    End If
                End If
            End If

            If CurrentKeyState(Keys.PageDown) Then
                If Not IsNothing(Tank_IIP) AndAlso Tank_IIP.Broken = False Then
                    Tank_IIP.Shoot()
                End If
            End If

        Else
            If blnGame_Over Then
                If CurrentKeyState(Keys.Enter) < 0 Then
                    blnGame_Over = False
                    DrawStartSelect(0)
                End If

                Threading.Thread.Sleep(20)
            Else
                If CurrentKeyState(Keys.Up) < 0 Then
                    intSelect -= 1
                    If intSelect < 0 Then
                        intSelect = 2
                    End If
                    DrawStartSelect(intSelect)
                    Threading.Thread.Sleep(100)
                End If

                If CurrentKeyState(Keys.Down) < 0 Then
                    intSelect += 1
                    If intSelect > 2 Then
                        intSelect = 0
                    End If
                    DrawStartSelect(intSelect)
                    Threading.Thread.Sleep(100)
                End If

                If CurrentKeyState(Keys.Enter) < 0 Then
                    Select Case intSelect
                        Case 0 '开始1P
                            blnBeginGame = True
                            Play_1P_LifeCount = 10
                            Play_2P_LifeCount = 0
                            Game_Start()
                            Timer1.Start()

                        Case 1 '开始2P
                            blnBeginGame = True
                            Play_1P_LifeCount = 10
                            Play_2P_LifeCount = 10
                            Game_Start()
                            Timer1.Start()

                        Case 2 '创建地图
                            Me.WindowState = FormWindowState.Minimized
                            CreateMap.Show()
                            CreateMap.Activate()
                    End Select

                End If
            End If

        End If

    End Sub

    ''' <summary>
    ''' 获取键盘按键状态
    ''' </summary>
    Private Function CurrentKeyState(ByVal KeyCode As Byte) As Integer
        'Static KeyState(255) As Boolean
        Dim n As Integer = GetAsyncKeyState(KeyCode)

        If n < 0 Then
            blnKeyState(KeyCode) = True
            Return n
        ElseIf blnKeyState(KeyCode) Then
            blnKeyState(KeyCode) = False
            Return 1
        End If
        Return 0

        '结果为 0时,则无按下
        '结果小于0 时,为按下
        '结果为1 时,则为弹起

    End Function

    Private Sub 经典坦克ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 经典坦克ToolStripMenuItem.Click
        ImgPos.Sprite_ImgPath = Application.StartupPath & "\Resource\pic\tank_sprite.png"
        经典坦克ToolStripMenuItem.Checked = True
        少女系列ToolStripMenuItem.Checked = False
        虫族敌人ToolStripMenuItem.Checked = False
        虫族系列ToolStripMenuItem.Checked = False
        机器敌人ToolStripMenuItem.Checked = False
        机器系列ToolStripMenuItem.Checked = False
    End Sub

    Private Sub 少女系列ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 少女系列ToolStripMenuItem.Click
        ImgPos.Sprite_ImgPath = Application.StartupPath & "\Resource\pic\girls_sprite.png"
        经典坦克ToolStripMenuItem.Checked = False
        少女系列ToolStripMenuItem.Checked = True
        虫族敌人ToolStripMenuItem.Checked = False
        虫族系列ToolStripMenuItem.Checked = False
        机器敌人ToolStripMenuItem.Checked = False
        机器系列ToolStripMenuItem.Checked = False
    End Sub

    Private Sub 虫族敌人ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 虫族敌人ToolStripMenuItem.Click
        ImgPos.Sprite_ImgPath = Application.StartupPath & "\Resource\pic\insect_sprite.png"
        经典坦克ToolStripMenuItem.Checked = False
        少女系列ToolStripMenuItem.Checked = False
        虫族敌人ToolStripMenuItem.Checked = True
        虫族系列ToolStripMenuItem.Checked = False
        机器敌人ToolStripMenuItem.Checked = False
        机器系列ToolStripMenuItem.Checked = False
    End Sub

    Private Sub 虫族系列ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 虫族系列ToolStripMenuItem.Click
        ImgPos.Sprite_ImgPath = Application.StartupPath & "\Resource\pic\insect_sprite_full.png"
        经典坦克ToolStripMenuItem.Checked = False
        少女系列ToolStripMenuItem.Checked = False
        虫族敌人ToolStripMenuItem.Checked = False
        虫族系列ToolStripMenuItem.Checked = True
        机器敌人ToolStripMenuItem.Checked = False
        机器系列ToolStripMenuItem.Checked = False
    End Sub

    Private Sub 机器敌人ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 机器敌人ToolStripMenuItem.Click
        ImgPos.Sprite_ImgPath = Application.StartupPath & "\Resource\pic\robots_sprite.png"
        经典坦克ToolStripMenuItem.Checked = False
        少女系列ToolStripMenuItem.Checked = False
        虫族敌人ToolStripMenuItem.Checked = False
        虫族系列ToolStripMenuItem.Checked = False
        机器敌人ToolStripMenuItem.Checked = True
        机器系列ToolStripMenuItem.Checked = False
    End Sub

    Private Sub 机器系列ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 机器系列ToolStripMenuItem.Click
        ImgPos.Sprite_ImgPath = Application.StartupPath & "\Resource\pic\robots_sprite_full.png"
        经典坦克ToolStripMenuItem.Checked = False
        少女系列ToolStripMenuItem.Checked = False
        虫族敌人ToolStripMenuItem.Checked = False
        虫族系列ToolStripMenuItem.Checked = False
        机器敌人ToolStripMenuItem.Checked = False
        机器系列ToolStripMenuItem.Checked = True
    End Sub

    Private Sub 选择关卡ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 选择关卡ToolStripMenuItem.Click
        Dim intRet As String

        intRet = InputBox("请选择关卡:(1-" & Map_Stage_List.Count & ")", "选择关卡", Play_Stage)

        If IsNumeric(intRet) Then
            If intRet > 0 And intRet <= Map_Stage_List.Count Then
                Play_Stage = CInt(intRet)
                Game_Start()
            End If
        End If
    End Sub
End Class
