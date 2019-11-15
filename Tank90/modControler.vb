
Module modController
    Public Play_Tank_List As New List(Of Tank_Object)
    Public Enemy_Tank_List As New List(Of Tank_Object)
    Public Bullet_List As New List(Of Bullet_Object)
    Public Bonus_List As New List(Of Base_Object)   '奖励

    Public ImgPos As ImagePosition
    Public MapContain As MapContainer
    Public Tank_IP As Tank_Object
    Public Tank_IIP As Tank_Object
    
    
    Public CanPlayAudio As Boolean = True

    Dim intCheckEnemyTimeCnt As Integer
    Dim Enemy_StopMove_Time As Integer
    Dim Play_StopMove_Time As Integer

    Private WithEvents StopTimer As New Timer
    Dim intEnemy_Appear_Pos As Integer = 0

    Public Function CheckCollasion(ByRef Obj1 As Base_Object, ByVal intDirection As Base_Object.Direction) As Integer
        Dim i As Integer
        Dim tmpObj As Base_Object
        Dim TestRect As RectangleF
        Dim tmpTank As Tank_Object
        Dim tmpBullet As Bullet_Object
        Dim blnBrickErase As Boolean

        If Obj1.Type >= 1 And Obj1.Type <= 6 Then
            tmpTank = Obj1.Parent

            Select Case intDirection
                Case Base_Object.Direction.Down
                    TestRect = New RectangleF(tmpTank.Rect.X, tmpTank.Rect.Y + tmpTank.Speedfactor, tmpTank.Rect.Width, tmpTank.Rect.Height)

                Case Base_Object.Direction.Left
                    TestRect = New RectangleF(tmpTank.Rect.X - tmpTank.Speedfactor, tmpTank.Rect.Y, tmpTank.Rect.Width, tmpTank.Rect.Height)

                Case Base_Object.Direction.Right
                    TestRect = New RectangleF(tmpTank.Rect.X + tmpTank.Speedfactor, tmpTank.Rect.Y, tmpTank.Rect.Width, tmpTank.Rect.Height)

                Case Base_Object.Direction.Up
                    TestRect = New RectangleF(tmpTank.Rect.X, tmpTank.Rect.Y - tmpTank.Speedfactor, tmpTank.Rect.Width, tmpTank.Rect.Height)

            End Select

            If TestRect.X > MapContain.Image.Width - tmpTank.Rect.Width Or TestRect.X < 0 _
                            Or TestRect.Y > MapContain.Image.Height - tmpTank.Rect.Height Or TestRect.Y < 0 Then

                If Obj1.Rect.Y < 0 Then
                    Obj1.Rect = New RectangleF(Obj1.Rect.X, 0, Obj1.Rect.Width, Obj1.Rect.Height)
                End If

                If Obj1.Rect.X < 0 Then
                    Obj1.Rect = New RectangleF(0, Obj1.Rect.Y, Obj1.Rect.Width, Obj1.Rect.Height)
                End If

                Select Case intDirection
                    Case Base_Object.Direction.Down
                        If Obj1.Rect.Y + Obj1.Rect.Height > MapContain.Image.Height - tmpTank.Rect.Height Then
                            Obj1.Rect = New RectangleF(Obj1.Rect.X, MapContain.Image.Height - tmpTank.Rect.Height, Obj1.Rect.Width, Obj1.Rect.Height)
                        End If

                    Case Base_Object.Direction.Left
                        If Obj1.Rect.X < 0 Then
                            Obj1.Rect = New RectangleF(0, Obj1.Rect.Y, Obj1.Rect.Width, Obj1.Rect.Height)
                        End If

                    Case Base_Object.Direction.Right
                        If Obj1.Rect.X + Obj1.Rect.Width > MapContain.Image.Width - tmpTank.Rect.Width Then
                            Obj1.Rect = New RectangleF(MapContain.Image.Width - tmpTank.Rect.Width, Obj1.Rect.Y, Obj1.Rect.Width, Obj1.Rect.Height)
                        End If

                    Case Base_Object.Direction.Up
                        If Obj1.Rect.Y < 0 Then
                            Obj1.Rect = New RectangleF(Obj1.Rect.X, 0, Obj1.Rect.Width, Obj1.Rect.Height)
                        End If

                End Select

                Return 100
            End If


        End If

        If Obj1.Type = 7 Then
            tmpBullet = Obj1.Parent

            Select Case intDirection
                Case Base_Object.Direction.Down
                    TestRect = New RectangleF(tmpBullet.Rect.X, tmpBullet.Rect.Y + tmpBullet.Speedfactor, tmpBullet.Rect.Width, tmpBullet.Rect.Height)

                Case Base_Object.Direction.Left
                    TestRect = New RectangleF(tmpBullet.Rect.X - tmpBullet.Speedfactor, tmpBullet.Rect.Y, tmpBullet.Rect.Width, tmpBullet.Rect.Height)

                Case Base_Object.Direction.Right
                    TestRect = New RectangleF(tmpBullet.Rect.X + tmpBullet.Speedfactor, tmpBullet.Rect.Y, tmpBullet.Rect.Width, tmpBullet.Rect.Height)

                Case Base_Object.Direction.Up
                    TestRect = New RectangleF(tmpBullet.Rect.X, tmpBullet.Rect.Y - tmpBullet.Speedfactor, tmpBullet.Rect.Width, tmpBullet.Rect.Height)

            End Select

            If TestRect.X > MapContain.Image.Width Or TestRect.X < 0 _
                            Or TestRect.Y > MapContain.Image.Height Or TestRect.Y < 0 Then

                'Bullet_List.Remove(Obj1.Parent)
                Return 0
            End If
        End If

        blnBrickErase = False
        Dim strKey() As String
        ReDim strKey(MapContain.MapObjectList.Count - 1)

        For i = MapContain.MapObjectList.Count - 1 To 0 Step -1
            MapContain.MapObjectList.Keys.CopyTo(strKey, 0)

            tmpObj = MapContain.MapObjectList.Item(strKey(i))

            If TestRect.IntersectsWith(tmpObj.Rect) Then
                Select Case Obj1.Type

                    Case 1 To 6
                        '坦克与地图物件相碰

                        Select Case tmpObj.Type
                            Case Base_Object.Object_Type.Bulid_Base, Base_Object.Object_Type.Bulid_Brick, _
                                Base_Object.Object_Type.Bulid_Steel

                                Select Case intDirection
                                    Case Base_Object.Direction.Down
                                        If Obj1.Rect.Y + Obj1.Rect.Height > tmpObj.Rect.Y Then
                                            Obj1.Rect = New RectangleF(Obj1.Rect.X, tmpObj.Rect.Y - Obj1.Rect.Height, Obj1.Rect.Width, Obj1.Rect.Height)
                                        End If
                                        If (Obj1.Rect.X + Obj1.Rect.Width > tmpObj.Rect.X) And (Obj1.Rect.X + Obj1.Rect.Width < tmpObj.Rect.X + 6) Then
                                            Obj1.Rect = New RectangleF(tmpObj.Rect.X - Obj1.Rect.Width, Obj1.Rect.Y, Obj1.Rect.Width, Obj1.Rect.Height)
                                        ElseIf (Obj1.Rect.X < tmpObj.Rect.X + tmpObj.Rect.Width) And (Obj1.Rect.X > tmpObj.Rect.X + tmpObj.Rect.Width - 6) Then
                                            Obj1.Rect = New RectangleF(tmpObj.Rect.X + tmpObj.Rect.Width, Obj1.Rect.Y, Obj1.Rect.Width, Obj1.Rect.Height)
                                        End If

                                    Case Base_Object.Direction.Left
                                        If Obj1.Rect.X < tmpObj.Rect.X + tmpObj.Rect.Width Then
                                            Obj1.Rect = New RectangleF(tmpObj.Rect.X - tmpObj.Rect.Width, Obj1.Rect.Y, Obj1.Rect.Width, Obj1.Rect.Height)
                                        End If
                                        If (Obj1.Rect.Y < tmpObj.Rect.Y + tmpObj.Rect.Height) And (Obj1.Rect.Y > tmpObj.Rect.Y + tmpObj.Rect.Height - 6) Then
                                            Obj1.Rect = New RectangleF(Obj1.Rect.X, tmpObj.Rect.Y + tmpObj.Rect.Height, Obj1.Rect.Width, Obj1.Rect.Height)
                                        ElseIf (Obj1.Rect.Y + Obj1.Rect.Height > tmpObj.Rect.Y) And (Obj1.Rect.Y + Obj1.Rect.Height < tmpObj.Rect.Y + 6) Then
                                            Obj1.Rect = New RectangleF(Obj1.Rect.X, tmpObj.Rect.Y - Obj1.Rect.Height, Obj1.Rect.Width, Obj1.Rect.Height)
                                        End If

                                    Case Base_Object.Direction.Right
                                        If Obj1.Rect.X + Obj1.Rect.Width > tmpObj.Rect.X Then
                                            Obj1.Rect = New RectangleF(tmpObj.Rect.X - Obj1.Rect.Width, Obj1.Rect.Y, Obj1.Rect.Width, Obj1.Rect.Height)
                                        End If
                                        If (Obj1.Rect.Y < tmpObj.Rect.Y + tmpObj.Rect.Height) And (Obj1.Rect.Y > tmpObj.Rect.Y + tmpObj.Rect.Height - 6) Then
                                            Obj1.Rect = New RectangleF(Obj1.Rect.X, tmpObj.Rect.Y + tmpObj.Rect.Height, Obj1.Rect.Width, Obj1.Rect.Height)
                                        ElseIf (Obj1.Rect.Y + Obj1.Rect.Height > tmpObj.Rect.Y) And (Obj1.Rect.Y + Obj1.Rect.Height < tmpObj.Rect.Y + 6) Then
                                            Obj1.Rect = New RectangleF(Obj1.Rect.X, tmpObj.Rect.Y - Obj1.Rect.Height, Obj1.Rect.Width, Obj1.Rect.Height)
                                        End If

                                    Case Base_Object.Direction.Up
                                        If Obj1.Rect.Y < tmpObj.Rect.Y + tmpObj.Rect.Height Then
                                            Obj1.Rect = New RectangleF(Obj1.Rect.X, tmpObj.Rect.Y - tmpObj.Rect.Height, Obj1.Rect.Width, Obj1.Rect.Height)
                                        End If
                                        If (Obj1.Rect.X + Obj1.Rect.Width > tmpObj.Rect.X) And (Obj1.Rect.X + Obj1.Rect.Width < tmpObj.Rect.X + 6) Then
                                            Obj1.Rect = New RectangleF(tmpObj.Rect.X - Obj1.Rect.Width, Obj1.Rect.Y, Obj1.Rect.Width, Obj1.Rect.Height)
                                        ElseIf (Obj1.Rect.X < tmpObj.Rect.X + tmpObj.Rect.Width) And (Obj1.Rect.X > tmpObj.Rect.X + tmpObj.Rect.Width - 6) Then
                                            Obj1.Rect = New RectangleF(tmpObj.Rect.X + tmpObj.Rect.Width, Obj1.Rect.Y, Obj1.Rect.Width, Obj1.Rect.Height)
                                        End If
                                End Select

                                Return 1

                            Case Base_Object.Object_Type.Bulid_River
                                If (Obj1.Parent.Extras_Bonus And Base_Object.Object_Bonus.Ship) = Base_Object.Object_Bonus.Null Then
                                    Select Case intDirection
                                        Case Base_Object.Direction.Down
                                            If Obj1.Rect.Y + Obj1.Rect.Height > tmpObj.Rect.Y Then
                                                Obj1.Rect = New RectangleF(Obj1.Rect.X, tmpObj.Rect.Y - Obj1.Rect.Height, Obj1.Rect.Width, Obj1.Rect.Height)
                                            End If
                                            If (Obj1.Rect.X + Obj1.Rect.Width > tmpObj.Rect.X) And (Obj1.Rect.X + Obj1.Rect.Width < tmpObj.Rect.X + 6) Then
                                                Obj1.Rect = New RectangleF(tmpObj.Rect.X - Obj1.Rect.Width, Obj1.Rect.Y, Obj1.Rect.Width, Obj1.Rect.Height)
                                            ElseIf (Obj1.Rect.X < tmpObj.Rect.X + tmpObj.Rect.Width) And (Obj1.Rect.X > tmpObj.Rect.X + tmpObj.Rect.Width - 6) Then
                                                Obj1.Rect = New RectangleF(tmpObj.Rect.X + tmpObj.Rect.Width, Obj1.Rect.Y, Obj1.Rect.Width, Obj1.Rect.Height)
                                            End If

                                        Case Base_Object.Direction.Left
                                            If Obj1.Rect.X < tmpObj.Rect.X + tmpObj.Rect.Width Then
                                                Obj1.Rect = New RectangleF(tmpObj.Rect.X - tmpObj.Rect.Width, Obj1.Rect.Y, Obj1.Rect.Width, Obj1.Rect.Height)
                                            End If
                                            If (Obj1.Rect.Y < tmpObj.Rect.Y + tmpObj.Rect.Height) And (Obj1.Rect.Y > tmpObj.Rect.Y + tmpObj.Rect.Height - 6) Then
                                                Obj1.Rect = New RectangleF(Obj1.Rect.X, tmpObj.Rect.Y + tmpObj.Rect.Height, Obj1.Rect.Width, Obj1.Rect.Height)
                                            ElseIf (Obj1.Rect.Y + Obj1.Rect.Height > tmpObj.Rect.Y) And (Obj1.Rect.Y + Obj1.Rect.Height < tmpObj.Rect.Y + 6) Then
                                                Obj1.Rect = New RectangleF(Obj1.Rect.X, tmpObj.Rect.Y - Obj1.Rect.Height, Obj1.Rect.Width, Obj1.Rect.Height)
                                            End If

                                        Case Base_Object.Direction.Right
                                            If Obj1.Rect.X + Obj1.Rect.Width > tmpObj.Rect.X Then
                                                Obj1.Rect = New RectangleF(tmpObj.Rect.X - Obj1.Rect.Width, Obj1.Rect.Y, Obj1.Rect.Width, Obj1.Rect.Height)
                                            End If
                                            If (Obj1.Rect.Y < tmpObj.Rect.Y + tmpObj.Rect.Height) And (Obj1.Rect.Y > tmpObj.Rect.Y + tmpObj.Rect.Height - 6) Then
                                                Obj1.Rect = New RectangleF(Obj1.Rect.X, tmpObj.Rect.Y + tmpObj.Rect.Height, Obj1.Rect.Width, Obj1.Rect.Height)
                                            ElseIf (Obj1.Rect.Y + Obj1.Rect.Height > tmpObj.Rect.Y) And (Obj1.Rect.Y + Obj1.Rect.Height < tmpObj.Rect.Y + 6) Then
                                                Obj1.Rect = New RectangleF(Obj1.Rect.X, tmpObj.Rect.Y - Obj1.Rect.Height, Obj1.Rect.Width, Obj1.Rect.Height)
                                            End If

                                        Case Base_Object.Direction.Up
                                            If Obj1.Rect.Y < tmpObj.Rect.Y + tmpObj.Rect.Height Then
                                                Obj1.Rect = New RectangleF(Obj1.Rect.X, tmpObj.Rect.Y - tmpObj.Rect.Height, Obj1.Rect.Width, Obj1.Rect.Height)
                                            End If
                                            If (Obj1.Rect.X + Obj1.Rect.Width > tmpObj.Rect.X) And (Obj1.Rect.X + Obj1.Rect.Width < tmpObj.Rect.X + 6) Then
                                                Obj1.Rect = New RectangleF(tmpObj.Rect.X - Obj1.Rect.Width, Obj1.Rect.Y, Obj1.Rect.Width, Obj1.Rect.Height)
                                            ElseIf (Obj1.Rect.X < tmpObj.Rect.X + tmpObj.Rect.Width) And (Obj1.Rect.X > tmpObj.Rect.X + tmpObj.Rect.Width - 6) Then
                                                Obj1.Rect = New RectangleF(tmpObj.Rect.X + tmpObj.Rect.Width, Obj1.Rect.Y, Obj1.Rect.Width, Obj1.Rect.Height)
                                            End If
                                    End Select

                                    Return 1

                                End If

                            Case Base_Object.Object_Type.Bulid_Snow
                                Obj1.Parent.Move(True, Obj1.Parent.Move_Direction)

                        End Select

                    Case Base_Object.Object_Type.Tank_Bullet
                        '子弹与地图物件相碰
                        Select Case tmpObj.Type
                            Case Base_Object.Object_Type.Bulid_Base
                                '子弹击中基地
                                tmpObj.Image = ImgPos.BaseBroken
                                tmpObj.Type = Base_Object.Object_Type.Bulid_BaseBroken
                                MapContain.MapObjectList.Item(i) = tmpObj
                                If CanPlayAudio Then
                                    My.Computer.Audio.Play(ImgPos.Audio_Base_Broken, AudioPlayMode.Background)
                                End If
                                Obj1.Parent.Broken = True

                                'Game Over
                                blnBeginGame = False
                                blnGame_Over = True

                                'Play_Bullet_List.Remove(Obj1.Parent)
                                Return 2

                            Case Base_Object.Object_Type.Bulid_Brick
                                '消除砖头
                                MapContain.MapObjectList.Remove(strKey(i))
                                Obj1.Parent.Broken = True
                                blnBrickErase = True

                            Case Base_Object.Object_Type.Bulid_Steel
                                '与钢板相碰
                                If (Obj1.Parent.Shooter.Extras_Bonus And Base_Object.Object_Bonus.Gun) = Base_Object.Object_Bonus.Gun Then
                                    '消除钢板
                                    MapContain.MapObjectList.Remove(strKey(i))
                                End If
                                Obj1.Parent.Broken = True

                                If CanPlayAudio Then
                                    Try
                                        'My.Computer.Audio.Play(ImgPos.Audio_Bullet_Destroy, AudioPlayMode.Background)
                                    Catch ex As Exception

                                    End Try

                                End If
                                Return 4

                        End Select

                End Select
            End If
        Next

        If blnBrickErase Then
            If CanPlayAudio Then
                My.Computer.Audio.Play(ImgPos.Audio_BrickErase, AudioPlayMode.Background)
            End If
            Return 3
        End If

        For i = 0 To Bullet_List.Count - 1
            tmpBullet = Bullet_List.Item(i)

            If Obj1.Parent.Equals(tmpBullet) Or tmpBullet.Broken Then
                Continue For
            End If

            If TestRect.IntersectsWith(tmpBullet.Rect) Then
                Select Case Obj1.Type
                    '坦克与子弹物件相碰
                    Case Base_Object.Object_Type.Tank_IP, Base_Object.Object_Type.Tank_IIP
                        '子弹与坦克相碰
                        If tmpBullet.Shooter.Type = Base_Object.Object_Type.Tank_IP _
                            Or tmpBullet.Shooter.Type = Base_Object.Object_Type.Tank_IIP Then

                            Continue For
                        Else
                            '我方中弹,如果有附加功能则丢失
                            If Obj1.Parent.Extras_Bonus <> Base_Object.Object_Bonus.Null Then
                                CreateBonus(Obj1.Parent.Extras_Bonus)
                                Obj1.Parent.Extras_Bonus = Base_Object.Object_Bonus.Null
                            Else
                                Obj1.Parent.Hit(tmpBullet.lethality)

                                If Obj1.Parent.Level > 1 Then   '等级降一级
                                    Obj1.Parent.Level -= 1
                                End If
                            End If

                            tmpBullet.Broken = True
                            If Obj1.Parent.Broken Then
                                tmpBullet.Shooter.Kill_List.Add(Obj1.Parent)
                            End If
                            Bullet_List.Item(i) = tmpBullet
                            Return 6

                        End If

                    Case 3 To 6 'Enemy Tank
                        If tmpBullet.Shooter.Type = Base_Object.Object_Type.Tank_IP _
                            Or tmpBullet.Shooter.Type = Base_Object.Object_Type.Tank_IIP Then

                            tmpBullet.Shooter.Score += (Obj1.Type - 2) * 100

                            If Obj1.Attach_Function <> Base_Object.Object_Bonus.Null Then
                                CreateBonus(Obj1.Attach_Function)
                                Obj1.Parent.Extras_Bonus = Base_Object.Object_Bonus.Null
                                If Obj1.Parent.Level > 1 Then   '等级降一级
                                    Obj1.Parent.Level -= 1
                                End If
                            Else
                                Obj1.Parent.Hit(tmpBullet.lethality)
                            End If

                            tmpBullet.Broken = True
                            If Obj1.Parent.Broken Then
                                tmpBullet.Shooter.Score += (Obj1.Type - 2) * 100
                                tmpBullet.Shooter.Kill_List.Add(Obj1.Parent)
                            End If
                            Bullet_List.Item(i) = tmpBullet
                            'Obj1.Parent.Shooter.Bullet_Count -= 1
                            Return 7
                        Else
                            Continue For
                        End If


                    Case Base_Object.Object_Type.Tank_Bullet
                        Select Case tmpBullet.Type
                            Case Base_Object.Object_Type.Tank_Bullet
                                '子弹与子弹相碰
                                If Not tmpBullet.Shooter.Equals(Obj1.Parent.Shooter) Then
                                    Obj1.Parent.Broken = True
                                    tmpBullet.Broken = True
                                    Bullet_List.Item(i) = tmpBullet
                                    Return 5
                                End If

                        End Select
                End Select
            End If
            
        Next

        For i = 0 To Play_Tank_List.Count - 1
            tmpTank = Play_Tank_List.Item(i)

            If Obj1.Parent.Equals(tmpTank) Then
                Continue For
            End If

            If TestRect.IntersectsWith(tmpTank.Rect) Then
                Select Case Obj1.Type
                    '坦克与坦克物件相碰
                    Case 1 To 6
                        Select Case intDirection
                            Case Base_Object.Direction.Down
                                If Obj1.Rect.Y + Obj1.Rect.Height > tmpTank.Rect.Y Then
                                    Obj1.Rect = New RectangleF(Obj1.Rect.X, tmpTank.Rect.Y - Obj1.Rect.Height, Obj1.Rect.Width, Obj1.Rect.Height)
                                End If

                            Case Base_Object.Direction.Left
                                If Obj1.Rect.X < tmpTank.Rect.X + tmpTank.Rect.Width Then
                                    Obj1.Rect = New RectangleF(tmpTank.Rect.X - tmpTank.Rect.Width, Obj1.Rect.Y, Obj1.Rect.Width, Obj1.Rect.Height)
                                End If

                            Case Base_Object.Direction.Right
                                If Obj1.Rect.X + Obj1.Rect.Width > tmpTank.Rect.X Then
                                    Obj1.Rect = New RectangleF(tmpTank.Rect.X - Obj1.Rect.Width, Obj1.Rect.Y, Obj1.Rect.Width, Obj1.Rect.Height)
                                End If

                            Case Base_Object.Direction.Up
                                If Obj1.Rect.Y < tmpTank.Rect.Y + tmpTank.Rect.Height Then
                                    Obj1.Rect = New RectangleF(Obj1.Rect.X, tmpTank.Rect.Y - tmpTank.Rect.Height, Obj1.Rect.Width, Obj1.Rect.Height)
                                End If

                        End Select

                        Return 9

                    Case Base_Object.Object_Type.Tank_Bullet
                        Select Case tmpTank.Type
                            Case 1 To 2
                                If Obj1.Parent.Shooter.Type = Base_Object.Object_Type.Tank_IP _
                                    Or Obj1.Parent.Shooter.Type = Base_Object.Object_Type.Tank_IIP Then

                                    Continue For
                                Else
                                    If tmpTank.Extras_Bonus <> Base_Object.Object_Bonus.Null Then
                                        If (tmpTank.Extras_Bonus And Base_Object.Object_Bonus.Helmet) <> Base_Object.Object_Bonus.Helmet Then
                                            CreateBonus(tmpTank.Extras_Bonus)
                                            tmpTank.Extras_Bonus = Base_Object.Object_Bonus.Null
                                        End If

                                    Else
                                        tmpTank.Hit(Obj1.Parent.lethality)
                                        If tmpTank.Level > 1 Then   '等级降一级
                                            tmpTank.Level -= 1
                                        End If
                                    End If

                                    Obj1.Parent.Broken = True

                                    If tmpTank.Broken Then
                                        Obj1.Parent.Shooter.Kill_List.Add(tmpTank)
                                    End If

                                    Play_Tank_List.Item(i) = tmpTank

                                    Return 10
                                End If

                            Case 3 To 6 'Enemy Tank
                                If Obj1.Parent.Shooter.Type = Base_Object.Object_Type.Tank_IP _
                                    Or Obj1.Parent.Shooter.Type = Base_Object.Object_Type.Tank_IIP Then

                                    Obj1.Parent.Shooter.Score += (tmpTank.Type - 2) * 100

                                    If tmpTank.Attach_Function > 0 Then
                                        CreateBonus(tmpTank.Attach_Function)
                                        tmpTank.Extras_Bonus = 0
                                        If tmpTank.Level > 1 Then   '等级降一级
                                            tmpTank.Level -= 1
                                        End If
                                    Else
                                        tmpTank.Hit(Obj1.Parent.lethality)
                                    End If

                                    Obj1.Parent.Broken = True

                                    If tmpTank.Broken Then
                                        Obj1.Parent.Shooter.Score += (tmpTank.Type - 2) * 100
                                        Obj1.Parent.Shooter.Kill_List.Add(tmpTank)
                                    End If
                                    Play_Tank_List.Item(i) = tmpTank

                                    Return 9
                                Else
                                    Continue For
                                End If

                        End Select
                End Select
            End If

        Next

        For i = 0 To Enemy_Tank_List.Count - 1
            tmpTank = Enemy_Tank_List.Item(i)

            If Obj1.Parent.Equals(tmpTank) Then
                Continue For
            End If

            If tmpTank.Broken Then
                Continue For
            End If

            If TestRect.IntersectsWith(tmpTank.Rect) Then
                Select Case Obj1.Type
                    '坦克与坦克物件相碰
                    Case 1 To 2
                        Return 9

                    Case 3 To 6
                        Return 8

                    Case Base_Object.Object_Type.Tank_Bullet
                        Select Case tmpTank.Type
                            Case 1 To 2
                                If Obj1.Parent.Shooter.Type = Base_Object.Object_Type.Tank_IP _
                                    Or Obj1.Parent.Shooter.Type = Base_Object.Object_Type.Tank_IIP Then

                                    Continue For
                                Else
                                    If tmpTank.Extras_Bonus > 0 Then
                                        If (tmpTank.Extras_Bonus And Base_Object.Object_Bonus.Helmet) <> Base_Object.Object_Bonus.Helmet Then
                                            CreateBonus(tmpTank.Extras_Bonus)
                                            tmpTank.Extras_Bonus = Base_Object.Object_Bonus.Null
                                            If tmpTank.Level > 1 Then   '等级降一级
                                                tmpTank.Level -= 1
                                            End If
                                        End If
                                    Else
                                        tmpTank.Hit(Obj1.Parent.lethality)
                                    End If

                                    Obj1.Parent.Broken = True
                                    If tmpTank.Broken Then
                                        Obj1.Parent.Shooter.Kill_List.Add(tmpTank)
                                    End If

                                    Enemy_Tank_List.Item(i) = tmpTank

                                    Return 12
                                End If

                            Case 3 To 6 'Enemy Tank
                                If Obj1.Parent.Shooter.Type = Base_Object.Object_Type.Tank_IP _
                                    Or Obj1.Parent.Shooter.Type = Base_Object.Object_Type.Tank_IIP Then

                                    If tmpTank.Attach_Function <> Base_Object.Object_Bonus.Null Then
                                        CreateBonus(tmpTank.Attach_Function)
                                        tmpTank.Attach_Function = Base_Object.Object_Bonus.Null
                                        If tmpTank.Level > 1 Then   '等级降一级
                                            tmpTank.Level -= 1
                                        End If
                                    Else
                                        tmpTank.Hit(Obj1.Parent.lethality)
                                    End If

                                    Obj1.Parent.Broken = True
                                    'tmpTank.Hit(Obj1.Parent.lethality)
                                    If tmpTank.Broken Then
                                        Obj1.Parent.Shooter.Score += (tmpTank.Type - 2) * 100
                                        Obj1.Parent.Shooter.Kill_List.Add(tmpTank)
                                    End If

                                    Enemy_Tank_List.Item(i) = tmpTank

                                    Return 11
                                Else
                                    Continue For
                                End If

                        End Select
                End Select
            End If

        Next

        Dim tmpBonus As Base_Object
        Dim t As Integer
        Dim MapObject As Base_Object

        For i = 0 To Bonus_List.Count - 1
            tmpBonus = Bonus_List.Item(i)

            If Obj1.Type >= 1 And Obj1.Type <= 6 Then
                If TestRect.IntersectsWith(tmpBonus.Rect) Then
                    Obj1.Parent.Score += 100    '加100分

                    Select Case tmpBonus.Attach_Function
                        Case Base_Object.Object_Bonus.Boom
                            If Obj1.Type < 3 Then
                                '炸弹,敌人全部爆炸
                                For t = 0 To Enemy_Tank_List.Count - 1
                                    tmpTank = Enemy_Tank_List.Item(t)
                                    tmpTank.Boom()
                                    tmpTank.Broken = True
                                    Enemy_Tank_List.Item(t) = tmpTank
                                Next
                            Else
                                'Player 爆炸
                                For t = 0 To Play_Tank_List.Count - 1
                                    tmpTank = Play_Tank_List.Item(t)
                                    tmpTank.Boom()
                                    tmpTank.Broken = True
                                    Play_Tank_List.Item(t) = tmpTank
                                Next
                            End If

                        Case Base_Object.Object_Bonus.Gun, Base_Object.Object_Bonus.Helmet, Base_Object.Object_Bonus.Ship, Base_Object.Object_Bonus.Star
                            Obj1.Parent.Got_Extras_Bonus(tmpBonus.Attach_Function)

                        Case Base_Object.Object_Bonus.Life
                            If Obj1.Type = Base_Object.Object_Type.Tank_IP Then
                                Play_1P_LifeCount += 1
                            End If
                            If Obj1.Type = Base_Object.Object_Type.Tank_IIP Then
                                Play_2P_LifeCount += 1
                            End If

                            If CanPlayAudio Then
                                My.Computer.Audio.Play(ImgPos.Audio_Bonus_life, AudioPlayMode.Background)
                            End If

                        Case Base_Object.Object_Bonus.Shovel    '铁锹
                            If Obj1.Type < 3 Then
                                For t = 23 To 25
                                    MapObject = New Base_Object
                                    MapObject.Image = ImgPos.Steel
                                    MapObject.Type = Base_Object.Object_Type.Bulid_Steel
                                    MapObject.Position = New Point(11, t)

                                    If MapContain.MapObjectList.ContainsKey(t & "_11") Then
                                        MapContain.MapObjectList.Item(t & "_11") = MapObject
                                    Else
                                        MapContain.MapObjectList.Add(t & "_11", MapObject)
                                    End If

                                    MapObject = New Base_Object
                                    MapObject.Image = ImgPos.Steel
                                    MapObject.Type = Base_Object.Object_Type.Bulid_Steel
                                    MapObject.Position = New Point(14, t)

                                    If MapContain.MapObjectList.ContainsKey(t & "_14") Then
                                        MapContain.MapObjectList.Item(t & "_14") = MapObject
                                    Else
                                        MapContain.MapObjectList.Add(t & "_14", MapObject)
                                    End If
                                Next

                                MapObject = New Base_Object
                                MapObject.Image = ImgPos.Steel
                                MapObject.Type = Base_Object.Object_Type.Bulid_Steel
                                MapObject.Position = New Point(12, 23)

                                If MapContain.MapObjectList.ContainsKey(23 & "_12") Then
                                    MapContain.MapObjectList.Item(23 & "_12") = MapObject
                                Else
                                    MapContain.MapObjectList.Add(23 & "_12", MapObject)
                                End If

                                MapObject = New Base_Object
                                MapObject.Image = ImgPos.Steel
                                MapObject.Type = Base_Object.Object_Type.Bulid_Steel
                                MapObject.Position = New Point(13, 23)

                                If MapContain.MapObjectList.ContainsKey(23 & "_13") Then
                                    MapContain.MapObjectList.Item(23 & "_13") = MapObject
                                Else
                                    MapContain.MapObjectList.Add(23 & "_13", MapObject)
                                End If

                            End If

                        Case Base_Object.Object_Bonus.Time
                            If Obj1.Type < 3 Then
                                Enemy_StopMove_Time = 10000
                            Else
                                Play_StopMove_Time = 10000
                            End If
                            StopTimer.Interval = 10000
                            StopTimer.Start()

                    End Select

                    tmpBonus.Attach_Function = Base_Object.Object_Bonus.Null
                End If
            Else
                Exit For
            End If
        Next

        Return 0
    End Function

    Public Sub Game_Over()
        Bullet_List.Clear()
        Play_Tank_List.Clear()
        Enemy_Tank_List.Clear()
        Bonus_List.Clear()

        HighScore = Math.Max(Play_IP_Score, Play_IIP_Score)

        DrawStaus()

        Play_Stage = 1
        Enemy_LifeCount = 20
        Play_IP_Score = 0
        Play_IIP_Score = 0
        blnBeginGame = False

        HighScore = 0
        MainForm.Timer1.Stop()
        MainForm.picMap.Image = Image.FromFile(Application.StartupPath & "\Resource\pic\tank_game_over.png")
        MainForm.picMap.Refresh()

        Tank_IP = Nothing
        Tank_IIP = Nothing

        My.Computer.Audio.Play(ImgPos.Audio_GameOver, AudioPlayMode.Background)
    End Sub

    Public Sub Game_Start()
        Dim iRow, iCol, intCnt As Integer
        Dim bytMap(25, 25) As Integer
        Dim arrVal() As String

        Bullet_List.Clear()
        Enemy_Tank_List.Clear()
        Bonus_List.Clear()

        Play_1P_KillList.Clear()
        Play_2P_KillList.Clear()

        Enemy_LifeCount = 20
        blnStagePassed = False

        If Play_Stage > Map_Stage_List.Count Then Play_Stage = 1

        arrVal = Strings.Split(Map_Stage_List.Item(Play_Stage - 1), ",")

        For iRow = 0 To 25
            For iCol = 0 To 25
                bytMap(iRow, iCol) = CInt(arrVal(intCnt))
                intCnt += 1
            Next
        Next

        MapContain.MapValue = bytMap

        If MainForm.intSelect = 0 Then
            If Tank_IP IsNot Nothing Then
                Tank_IP.Move(False, Base_Object.Direction.Up)
                Tank_IP.Position = Play_1P_Appear_Postion
                Tank_IP.Appear()
                Tank_IP.Bullet_Count = 0
                Tank_IP.Kill_List.Clear()

            Else
                Tank_Factory(Base_Object.Object_Type.Tank_IP, 4, Play_1P_Appear_Postion)
            End If

            If Tank_IIP IsNot Nothing Then
                Tank_IIP.Move(False, Base_Object.Direction.Up)
                Tank_IIP.Position = Play_2P_Appear_Postion
                Tank_IIP.Appear()
                Tank_IIP.Bullet_Count = 0
                Tank_IIP.Kill_List.Clear()
            Else
                'Tank_Factory(Base_Object.Object_Type.Tank_IIP, 1, Play_2P_Appear_Postion)
            End If
        End If

        If MainForm.intSelect = 1 Then
            If Tank_IP IsNot Nothing Then
                Tank_IP.Move(False, Base_Object.Direction.Up)
                Tank_IP.Position = Play_1P_Appear_Postion
                Tank_IP.Appear()
                Tank_IP.Bullet_Count = 0
                Tank_IP.Kill_List.Clear()
            Else
                Tank_Factory(Base_Object.Object_Type.Tank_IP, 4, Play_1P_Appear_Postion)
            End If

            If Tank_IIP IsNot Nothing Then
                Tank_IIP.Move(False, Base_Object.Direction.Up)
                Tank_IIP.Position = Play_2P_Appear_Postion
                Tank_IIP.Appear()
                Tank_IIP.Bullet_Count = 0
                Tank_IIP.Kill_List.Clear()
            Else
                Tank_Factory(Base_Object.Object_Type.Tank_IIP, 4, Play_2P_Appear_Postion)
            End If
        End If

    End Sub

    Public Sub Render()
        Dim tmpG As Graphics
        Dim i As Integer
        Dim tmpTank As Tank_Object
        Dim tmpBullet As Bullet_Object
        Dim tmpBonus As Base_Object
        'Dim intT As Integer
        
        'MainForm.Timer1.Stop()
        'intT = My.Computer.Clock.TickCount

        If blnGame_Over Then
            Game_Over()
            Exit Sub
        End If

        intCheckEnemyTimeCnt += 1

        If intCheckEnemyTimeCnt Mod 100 = 0 Then
            intCheckEnemyTimeCnt = 0
            CheckEnemy_TankCount()
        End If

        If blnStagePassed Then
            Exit Sub
        End If

        '移动敌人
        Enemy_AutoMove()

        '画状态内容
        DrawStaus()

        '画地图
        MapContain.RenderMap()
        tmpG = MapContain.G

        '子弹移动
        For i = Bullet_List.Count - 1 To 0 Step -1
            tmpBullet = Bullet_List.Item(i)
            If CheckCollasion(tmpBullet, tmpBullet.Shoot_Direction) = 0 Then
                tmpBullet.Move()
            End If
            Try
                Bullet_List.Item(i) = tmpBullet
            Catch ex As Exception

            End Try

        Next

        '画Player坦克
        For i = 0 To Play_Tank_List.Count - 1
            tmpTank = Play_Tank_List.Item(i)
            tmpG.DrawImage(tmpTank.Image, tmpTank.Rect)
        Next

        '画敌人坦克
        For i = 0 To Enemy_Tank_List.Count - 1
            tmpTank = Enemy_Tank_List.Item(i)
            tmpG.DrawImage(tmpTank.Image, tmpTank.Rect)
        Next

        '画子弹
        For i = 0 To Bullet_List.Count - 1
            tmpBullet = Bullet_List.Item(i)
            tmpG.DrawImage(tmpBullet.Image, tmpBullet.Rect)
        Next

        '画草地
        MapContain.RenderGrass()

        '画奖励
        For i = 0 To Bonus_List.Count - 1
            tmpBonus = Bonus_List.Item(i)
            If tmpBonus.Attach_Function <> Base_Object.Object_Bonus.Null Then
                tmpG.DrawImage(tmpBonus.Image, tmpBonus.Rect)
            End If

        Next

        MainForm.picMap.Image = MapContain.Image

        '检查敌人坦克状态并移除消灭的坦克
        For i = Enemy_Tank_List.Count - 1 To 0 Step -1
            tmpTank = Enemy_Tank_List.Item(i)
            If tmpTank.Broken Then
                Enemy_Tank_List.RemoveAt(i)
            End If
        Next

        '检查我方坦克状态并移除消灭的坦克
        For i = Play_Tank_List.Count - 1 To 0 Step -1
            tmpTank = Play_Tank_List.Item(i)
            
            If tmpTank.Broken Then
                If tmpTank.Type = Base_Object.Object_Type.Tank_IP Then
                    'Play_IP_Score += tmpTank.Score
                    Play_1P_LifeCount -= 1
                Else
                    'Play_IIP_Score += tmpTank.Score
                    Play_2P_LifeCount -= 1
                End If
                tmpTank.Score = 0

                Play_Tank_List.RemoveAt(i)
            End If
        Next

        '检查子弹状态并移除消灭的子弹
        For i = Bullet_List.Count - 1 To 0 Step -1
            tmpBullet = Bullet_List.Item(i)
            If tmpBullet.Broken Or tmpBullet.Rect.X > MapContain.Width * 16 Or tmpBullet.Rect.X < 0 _
                    Or tmpBullet.Rect.Y < 0 Or tmpBullet.Rect.Y > MapContain.Height * 16 Then

                tmpBullet.Shooter.Bullet_Count -= 1
                Bullet_List.RemoveAt(i)

            End If
        Next

        '检查奖励状态并移除捡过的
        For i = Bonus_List.Count - 1 To 0 Step -1
            tmpBonus = Bonus_List.Item(i)
            If tmpBonus.Attach_Function = Base_Object.Object_Bonus.Null Then
                Bonus_List.RemoveAt(i)
            End If
        Next

    End Sub

    Public Sub Tank_Factory(ByVal Tank_Type As Base_Object.Object_Type, ByVal intLevel As Integer, ByVal StartPos As Point, Optional ByVal intBonus As Integer = 0)
        Dim Enemy_Tank As Tank_Object
        Dim lstBonus() As Integer = {0, 1, 2, 4, 8, 16, 32, 64, 128}

        Select Case Tank_Type
            Case Base_Object.Object_Type.Tank_IP
                Tank_IP = New Tank_Object()
                Tank_IP.Image_Position = ImgPos
                Tank_IP.Type = Base_Object.Object_Type.Tank_IP
                Tank_IP.Level = intLevel
                Tank_IP.Speedfactor = 3
                Tank_IP.LifeValue = 100
                Tank_IP.Lethality = 100
                Tank_IP.Bullet_List = Bullet_List
                Tank_IP.Position = StartPos
                Tank_IP.LoadImage(Base_Object.Direction.Up, 0)
                Tank_IP.Move_Direction = Base_Object.Direction.Up
                Tank_IP.Appear()
                Tank_IP.Kill_List = Play_1P_KillList
                intBonus = Int(9 * Rnd())
                Tank_IP.Extras_Bonus = lstBonus(intBonus)
                Play_Tank_List.Add(Tank_IP)

            Case Base_Object.Object_Type.Tank_IIP
                Tank_IIP = New Tank_Object()
                Tank_IIP.Image_Position = ImgPos
                Tank_IIP.Type = Base_Object.Object_Type.Tank_IIP
                Tank_IIP.Level = intLevel
                Tank_IIP.Speedfactor = 3
                Tank_IIP.LifeValue = 100
                Tank_IIP.Lethality = 100
                Tank_IIP.Bullet_List = Bullet_List
                Tank_IIP.Position = StartPos
                Tank_IIP.LoadImage(Base_Object.Direction.Up, 0)
                Tank_IIP.Move_Direction = Base_Object.Direction.Up
                Tank_IIP.Appear()
                Tank_IIP.Kill_List = Play_2P_KillList
                intBonus = Int(9 * Rnd())
                Tank_IIP.Extras_Bonus = lstBonus(intBonus)
                Play_Tank_List.Add(Tank_IIP)

            Case Else
                Enemy_Tank = New Tank_Object
                Enemy_Tank.Image_Position = ImgPos
                Enemy_Tank.Type = Tank_Type
                Enemy_Tank.Level = intLevel
                Enemy_Tank.LifeValue = IIf(Tank_Type = Base_Object.Object_Type.Tank_Army4, 400, 100)
                Enemy_Tank.Lethality = 100
                Enemy_Tank.Speedfactor = IIf(Tank_Type = Base_Object.Object_Type.Tank_Army2, 3, 1)
                Enemy_Tank.Bullet_List = Bullet_List
                Enemy_Tank.Position = StartPos
                Enemy_Tank.LoadImage(Base_Object.Direction.Down, 0)
                Enemy_Tank.Move_Direction = Base_Object.Direction.Down
                Enemy_Tank.Attach_Function = lstBonus(intBonus)
                Enemy_Tank.Appear()
                Enemy_Tank_List.Add(Enemy_Tank)

        End Select
    End Sub

    Public Sub Enemy_AutoMove()
        Dim i As Integer
        Dim Enemy_Tank As Tank_Object
        Dim intColl As Integer
        Dim intDirect As Integer
        Dim intRnd As Integer

        If Enemy_StopMove_Time = 0 Then

            For i = Enemy_Tank_List.Count - 1 To 0 Step -1
                Enemy_Tank = Enemy_Tank_List.Item(i)

                If Not Enemy_Tank.blnAppear Then Continue For

                intRnd = Int(50 * Rnd())
                If intRnd = 6 Then
                    'Enemy_Tank.MoveStop(Enemy_Tank.Move_Direction)

                    Enemy_Tank.Shoot()
                End If

                intRnd = Int(40 * Rnd())

                If intRnd = 8 Then
                    intDirect = Enemy_Tank.Move_Direction
                    intDirect = Int(4 * Rnd()) ' (intDirect + 1) Mod 4

                    intColl = CheckCollasion(Enemy_Tank, intDirect)

                    If intColl = 0 Or intColl = 8 Then
                        Enemy_Tank.Move_Direction = intDirect
                        Enemy_Tank.Move(True, Enemy_Tank.Move_Direction)
                        Continue For
                    Else
                        'intDirect = Enemy_Tank.Move_Direction
                        'intDirect = (intDirect + 1) Mod 4
                        'Enemy_Tank.Move_Direction = intDirect

                        'intColl = CheckCollasion(Enemy_Tank, intDirect)
                        'If intColl = 0 Or intColl = 8 Then
                        '    Enemy_Tank.Move_Direction = intDirect
                        '    Enemy_Tank.Move(True, intDirect)
                        '    Continue For
                        'End If
                        Enemy_Tank.Shoot()
                    End If
                End If

                intColl = CheckCollasion(Enemy_Tank, Enemy_Tank.Move_Direction)

                Select Case intColl
                    Case 0, 8
                        Enemy_Tank.Move(True, Enemy_Tank.Move_Direction)

                    Case Else
                        Enemy_Tank.Shoot()

                        If Not Enemy_Tank.Broken Then
                            intDirect = Enemy_Tank.Move_Direction
                            intDirect = (intDirect + 1) Mod 4
                            Enemy_Tank.Move_Direction = intDirect

                            intColl = CheckCollasion(Enemy_Tank, intDirect)
                            If intColl = 0 Or intColl = 8 Then
                                Enemy_Tank.Move(True, intDirect)
                            End If

                        End If

                End Select

            Next
        End If


    End Sub

    Public Sub CheckEnemy_TankCount()
        Dim intTankType As Integer
        Dim intLevel As Integer
        Dim intPos As Integer
        Dim intSPos As Point
        Dim intBonus As Base_Object.Object_Bonus
        Dim lstBonus() As Integer = {0, 1, 2, 4, 8, 16, 32, 64, 128}

        Randomize(Now.ToOADate)

        If Enemy_LifeCount > 0 Then
            If Enemy_Tank_List.Count < 5 Then
                intTankType = Int((Base_Object.Object_Type.Tank_Army4 - Base_Object.Object_Type.Tank_Army1 + 1) * Rnd() + Base_Object.Object_Type.Tank_Army1)
                intLevel = Int((20) * Rnd() + 1)
                intPos = intEnemy_Appear_Pos

                If intLevel = 8 Then
                    '添加奖励
                    intLevel = 2
                    intBonus = Int(8 * Rnd() + 1)

                Else
                    intBonus = 0
                    intLevel = 1
                End If

                'intBonus = 8
                'intLevel = 2

                intSPos = Enemy_Appear_Postion(intPos)

                Tank_Factory(intTankType, intLevel, intSPos, intBonus)
                intEnemy_Appear_Pos += 1

                If intEnemy_Appear_Pos > 2 Then intEnemy_Appear_Pos = 0

                Enemy_LifeCount -= 1
            End If
        Else
            If Enemy_Tank_List.Count = 0 Then
                Play_Stage += 1
                blnStagePassed = True

                DrawPassStage()

            End If
        End If
    End Sub

    Public Sub CreateBonus(Optional ByVal intBonus As Base_Object.Object_Bonus = Base_Object.Object_Bonus.Null)
        Dim intR, intC As Integer
        Dim tmpBonus As Base_Object
        Dim lstBonus() As Integer = {0, 1, 2, 4, 8, 16, 32, 64, 128}

        Randomize()

        tmpBonus = New Base_Object()
        Do
            intR = Int((MapContain.Height - 1) * Rnd())
            intC = Int((MapContain.Width - 1) * Rnd())
        Loop While (intR = 24 And intC = 12)

        tmpBonus.Position = New Point(intC, intR)

        If intBonus = Base_Object.Object_Bonus.Null Then
            intBonus = lstBonus(Int(8 * Rnd() + 1))
        End If

        Select Case intBonus
            Case Base_Object.Object_Bonus.Boom
                tmpBonus.Image = ImgPos.Bonus_BoomShell
                tmpBonus.Attach_Function = Base_Object.Object_Bonus.Boom

            Case Base_Object.Object_Bonus.Gun
                tmpBonus.Image = ImgPos.Bonus_Gun
                tmpBonus.Attach_Function = Base_Object.Object_Bonus.Gun

            Case Base_Object.Object_Bonus.Helmet
                tmpBonus.Image = ImgPos.Bonus_Helmet
                tmpBonus.Attach_Function = Base_Object.Object_Bonus.Helmet

            Case Base_Object.Object_Bonus.Life
                tmpBonus.Image = ImgPos.Bonus_Life
                tmpBonus.Attach_Function = Base_Object.Object_Bonus.Life

            Case Base_Object.Object_Bonus.Ship
                tmpBonus.Image = ImgPos.Bonus_Ship
                tmpBonus.Attach_Function = Base_Object.Object_Bonus.Ship

            Case Base_Object.Object_Bonus.Shovel
                tmpBonus.Image = ImgPos.Bonus_Shovel
                tmpBonus.Attach_Function = Base_Object.Object_Bonus.Shovel

            Case Base_Object.Object_Bonus.Star
                tmpBonus.Image = ImgPos.Bonus_Star
                tmpBonus.Attach_Function = Base_Object.Object_Bonus.Star

            Case Base_Object.Object_Bonus.Time
                tmpBonus.Image = ImgPos.Bonus_TimeClock
                tmpBonus.Attach_Function = Base_Object.Object_Bonus.Time
        End Select

        Bonus_List.Add(tmpBonus)
    End Sub

    Private Sub StopTimer_Tick(sender As Object, e As EventArgs) Handles StopTimer.Tick
        If Enemy_StopMove_Time > 0 Then
            Enemy_StopMove_Time = 0
        End If
        If Play_StopMove_Time > 0 Then
            Play_StopMove_Time = 0
        End If
        StopTimer.Stop()
    End Sub

    Public Sub DrawStaus()
        Dim G As Graphics
        Dim i, t As Integer
        Dim tmpBit As Bitmap
        Dim intCnt As Integer

        Try

            tmpBit = New Bitmap(MainForm.picStatus.Width, MainForm.picStatus.Height)

            G = Graphics.FromImage(tmpBit)

            G.DrawString("数量: " & Enemy_LifeCount, New Font("SimSun", 14), Brushes.Black, New Point(1, 5))

            intCnt = 0
            For t = 1 To 4
                If intCnt >= Enemy_LifeCount Then
                    Exit For
                End If

                For i = 1 To 5
                    intCnt += 1
                    G.DrawImage(ImgPos.Enemy_ICO, 10 + (i - 1) * 20, 30 + (t - 1) * 20)

                    If intCnt >= Enemy_LifeCount Then
                        Exit For
                    End If
                Next
            Next

            G.DrawImage(ImgPos.IP_ICO, 5, 150)
            G.DrawString(" : " & Play_1P_LifeCount, New Font("SimSun", 14), Brushes.Black, New Point(30, 156))
            G.DrawString("分数:" & Play_IP_Score, New Font("SimSun", 12), Brushes.Black, New Point(1, 176))
            intCnt = 0
            For t = 1 To (Play_1P_LifeCount \ 4) + 1
                If intCnt >= Play_1P_LifeCount Then
                    Exit For
                End If

                For i = 1 To 5
                    intCnt += 1
                    G.DrawImage(ImgPos.Play_ICO, 10 + (i - 1) * 20, 195 + (t - 1) * 20)

                    If intCnt >= Play_1P_LifeCount Then
                        Exit For
                    End If
                Next
            Next

            G.DrawImage(ImgPos.IIP_ICO, 5, 280)
            G.DrawString(" : " & Play_2P_LifeCount, New Font("SimSun", 14), Brushes.Black, New Point(30, 286))
            G.DrawString("分数:" & Play_IIP_Score, New Font("SimSun", 12), Brushes.Black, New Point(1, 306))

            intCnt = 0
            For t = 1 To (Play_2P_LifeCount \ 4) + 1
                If intCnt >= Play_2P_LifeCount Then
                    Exit For
                End If

                For i = 1 To 5
                    intCnt += 1
                    G.DrawImage(ImgPos.Play_ICO, 10 + (i - 1) * 20, 325 + (t - 1) * 20)

                    If intCnt >= Play_2P_LifeCount Then
                        Exit For
                    End If
                Next
            Next

            G.DrawImage(ImgPos.StageFlag, 10, tmpBit.Height - 35) '390
            G.DrawString(" " & Play_Stage, New Font("SimSun", 30), Brushes.Black, New Point(40, tmpBit.Height - 38)) '387
            MainForm.picStatus.Image = tmpBit

        Catch ex As Exception

        End Try
    End Sub

    Public Sub Play_Control(ByVal intDirection As Base_Object.Direction, ByVal intPlay As Base_Object.Object_Type, ByVal blnKeyDown As Boolean)
        Select Case intPlay
            Case Base_Object.Object_Type.Tank_IP
                If Play_1P_LifeCount > 0 Then
                    If IsNothing(Tank_IP) OrElse Tank_IP.Broken Then
                        Tank_Factory(Base_Object.Object_Type.Tank_IP, 4, Play_1P_Appear_Postion)

                    End If
                Else
                    If Play_2P_LifeCount > 0 Then
                        If IsNothing(Tank_IP) OrElse Tank_IP.Broken Then
                            Tank_Factory(Base_Object.Object_Type.Tank_IP, 4, Play_1P_Appear_Postion)

                        End If
                    Else
                        blnGame_Over = True
                    End If
                End If

                If Not blnGame_Over Then
                    If blnKeyDown Then
                        If Not Tank_IP.isMoved Then
                            If CheckCollasion(Tank_IP, intDirection) = 0 Then
                                If Not Tank_IP.isMoved Then
                                    Tank_IP.Move(True, intDirection)
                                End If

                            Else
                                If Not Tank_IP.isMoved Then
                                    Tank_IP.Move(False, intDirection)
                                End If
                            End If
                        End If

                    Else
                        'If Not Tank_IP.isMoved Then
                        Tank_IP.MoveStop(intDirection)
                        'End If

                    End If
                End If


            Case Base_Object.Object_Type.Tank_IIP
                If Play_2P_LifeCount > 0 Then
                    If IsNothing(Tank_IIP) OrElse Tank_IIP.Broken Then
                        Tank_Factory(Base_Object.Object_Type.Tank_IIP, 4, Play_2P_Appear_Postion)
                        Play_2P_LifeCount -= 1
                    End If
                Else
                    If Play_1P_LifeCount > 0 Then
                        If IsNothing(Tank_IIP) OrElse Tank_IIP.Broken Then
                            Tank_Factory(Base_Object.Object_Type.Tank_IIP, 4, Play_2P_Appear_Postion)
                            Play_1P_LifeCount -= 1
                        End If
                    Else
                        blnGame_Over = True
                    End If
                End If

                If Not blnGame_Over Then
                    If blnKeyDown Then
                        If CheckCollasion(Tank_IIP, intDirection) = 0 Then
                            If Not Tank_IiP.isMoved Then
                                Tank_IIP.Move(True, intDirection)
                            End If
                        Else
                            If Not Tank_IIP.isMoved Then
                                Tank_IIP.Move(False, intDirection)
                            End If
                        End If
                    Else
                        'If Not Tank_IIP.isMoved Then
                        Tank_IIP.MoveStop(intDirection)
                        'End If

                    End If
                End If

        End Select

    End Sub

    Public Sub ReadMap()
        Dim strMap As String
        Dim MapStreamR As IO.StreamReader

        If IO.File.Exists(strMapFile) Then
            MapStreamR = New IO.StreamReader(strMapFile)
            Map_Stage_List.Clear()

            Do
                strMap = MapStreamR.ReadLine()
                If strMap <> "" Then
                    Map_Stage_List.Add(strMap)
                End If
            Loop Until MapStreamR.EndOfStream
            MapStreamR.Close()

        End If
    End Sub

    Public Sub DrawPassStage()
        Dim tmpBit As Bitmap
        Dim tmpG As Graphics
        Dim i As Integer
        Dim dicKill As New Dictionary(Of String, Integer)
        Dim tmpTank As Tank_Object
        Dim intTtlCnt As Integer
        Dim intTtlScore As Integer

        MainForm.Timer1.Stop()

        dicKill.Add("IP_1", 0)
        dicKill.Add("IP_2", 0)
        dicKill.Add("IP_3", 0)
        dicKill.Add("IP_4", 0)
        dicKill.Add("IIP_1", 0)
        dicKill.Add("IIP_2", 0)
        dicKill.Add("IIP_3", 0)
        dicKill.Add("IIP_4", 0)

        intTtlCnt = 0
        intTtlScore = 0

        If Tank_IP IsNot Nothing Then
            For i = 0 To Play_1P_KillList.Count - 1
                tmpTank = Play_1P_KillList.Item(i)
                intTtlCnt += 1

                Select Case tmpTank.Type
                    Case Base_Object.Object_Type.Tank_Army1
                        If dicKill.ContainsKey("IP_1") Then
                            dicKill.Item("IP_1") = dicKill.Item("IP_1") + 1
                        Else
                            dicKill.Add("IP_1", 1)
                        End If
                        intTtlScore += 100

                    Case Base_Object.Object_Type.Tank_Army2
                        If dicKill.ContainsKey("IP_2") Then
                            dicKill.Item("IP_2") = dicKill.Item("IP_2") + 1
                        Else
                            dicKill.Add("IP_2", 1)
                        End If
                        intTtlScore += 200

                    Case Base_Object.Object_Type.Tank_Army3
                        If dicKill.ContainsKey("IP_3") Then
                            dicKill.Item("IP_3") = dicKill.Item("IP_3") + 1
                        Else
                            dicKill.Add("IP_3", 1)
                        End If
                        intTtlScore += 300

                    Case Base_Object.Object_Type.Tank_Army4
                        If dicKill.ContainsKey("IP_4") Then
                            dicKill.Item("IP_4") = dicKill.Item("IP_4") + 1
                        Else
                            dicKill.Add("IP_4", 1)
                        End If
                        intTtlScore += 400

                End Select
            Next

            Play_IP_Score += intTtlScore
            Tank_IP.Kill_List.Clear()
        End If
        dicKill.Add("IP_KillCnt", intTtlCnt)
        dicKill.Add("IP_KillScore", intTtlScore)

        intTtlCnt = 0
        intTtlScore = 0

        If Tank_IIP IsNot Nothing Then
            For i = 0 To Play_2P_KillList.Count - 1
                tmpTank = Play_2P_KillList.Item(i)
                intTtlCnt += 1

                Select Case tmpTank.Type
                    Case Base_Object.Object_Type.Tank_Army1
                        If dicKill.ContainsKey("IIP_1") Then
                            dicKill.Item("IIP_1") = dicKill.Item("IIP_1") + 1
                        Else
                            dicKill.Add("IIP_1", 1)
                        End If
                        intTtlScore += 100

                    Case Base_Object.Object_Type.Tank_Army2
                        If dicKill.ContainsKey("IIP_2") Then
                            dicKill.Item("IIP_2") = dicKill.Item("IIP_2") + 1
                        Else
                            dicKill.Add("IIP_2", 1)
                        End If
                        intTtlScore += 200

                    Case Base_Object.Object_Type.Tank_Army3
                        If dicKill.ContainsKey("IIP_3") Then
                            dicKill.Item("IIP_3") = dicKill.Item("IIP_3") + 1
                        Else
                            dicKill.Add("IIP_3", 1)
                        End If
                        intTtlScore += 300

                    Case Base_Object.Object_Type.Tank_Army4
                        If dicKill.ContainsKey("IIP_4") Then
                            dicKill.Item("IIP_4") = dicKill.Item("IIP_4") + 1
                        Else
                            dicKill.Add("IIP_4", 1)
                        End If
                        intTtlScore += 400
                End Select
            Next
            Play_IIP_Score += intTtlScore

            Tank_IIP.Kill_List.Clear()
        End If

        dicKill.Add("IIP_KillCnt", intTtlCnt)
        dicKill.Add("IIP_KillScore", intTtlScore)

        tmpBit = New Bitmap(26 * 16, 26 * 16)
        tmpG = Graphics.FromImage(tmpBit)

        tmpG.Clear(Color.Black)

        tmpG.DrawLine(New Pen(Brushes.White, 4), New Point(12.5 * 16, 2 * 16), New Point(12.5 * 16, 25 * 16))
        tmpG.DrawString("Player 1P", New Font("SimSun", 24), Brushes.Yellow, New Point(2 * 16, 2 * 16))
        tmpG.DrawString("Player 2P", New Font("SimSun", 24), Brushes.Yellow, New Point(15 * 16, 2 * 16))

        For i = 3 To 6
            tmpG.DrawImage(ImgPos.dicImageList.Item(i & "_1_2_0"), New Point(0 * 16, 5 * 16 + (i - 3) * 4 * 16))
            tmpG.DrawImage(ImgPos.dicImageList.Item(i & "_1_2_0"), New Point(13 * 16, 5 * 16 + (i - 3) * 4 * 16))

            tmpG.DrawString("×" & dicKill.Item("IP_" & i - 2), New Font("SimSun", 24), Brushes.Yellow, New Point(2 * 16, 5 * 16 + (i - 3) * 4 * 16))
            tmpG.DrawString("＝" & dicKill.Item("IP_" & i - 2) * 100 * (i - 2), New Font("SimSun", 24), Brushes.Yellow, New Point(5 * 16, 5 * 16 + (i - 3) * 4 * 16))

            tmpG.DrawString("×" & dicKill.Item("IIP_" & i - 2), New Font("SimSun", 24), Brushes.Yellow, New Point(15 * 16, 5 * 16 + (i - 3) * 4 * 16))
            tmpG.DrawString("＝" & dicKill.Item("IIP_" & i - 2) * 100 * (i - 2), New Font("SimSun", 24), Brushes.Yellow, New Point(18 * 16, 5 * 16 + (i - 3) * 4 * 16))

            MainForm.picMap.Image = tmpBit
            My.Computer.Audio.Play(Application.StartupPath & "\Resource\sounds\total.score.tick.wav", AudioPlayMode.Background)
            Wait(500)
        Next

        tmpG.DrawString("Total  " & dicKill.Item("IP_KillCnt"), New Font("SimSun", 24), Brushes.Yellow, New Point(0 * 16, 20 * 16))
        tmpG.DrawString(dicKill.Item("IP_KillScore"), New Font("SimSun", 24), Brushes.Yellow, New Point(0 * 16, 22 * 16))

        tmpG.DrawString("Total  " & dicKill.Item("IIP_KillCnt"), New Font("SimSun", 24), Brushes.Yellow, New Point(13 * 16, 20 * 16))
        tmpG.DrawString(dicKill.Item("IIP_KillScore"), New Font("SimSun", 24), Brushes.Yellow, New Point(13 * 16, 22 * 16))

        MainForm.picMap.Image = tmpBit


        blnStagePassed = True
    End Sub

    Private Sub Wait(ByVal intMM As Integer)
        Dim intT As Integer
        Dim intCnt As Integer

        intT = My.Computer.Clock.TickCount
        intCnt = 1
        While My.Computer.Clock.TickCount - intT < intMM
            'intCnt += 1

            'If intCnt Mod 20 = 0 Then
            Application.DoEvents()
            'intCnt = 0
            'End If

        End While
    End Sub
End Module
