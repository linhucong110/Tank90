
Imports System.Drawing

Public Class Tank_Object
    Inherits Base_Object

    'Dim m_ItsBrain As New clsNeuralNet      '坦克的神经网络

    Dim m_LifeValue As Integer     '生命力
    Public Lethality As Integer     '杀伤力
    Public Speedfactor As Integer   '一次移动距离
    Dim m_Move_Direction As Direction
    'Public Bullet As Bullet_Object
    Public Bullet_Count As Integer
    Public Bullet_List As List(Of Bullet_Object)
    Public Extras_Bonus As Object_Bonus
    Public isMoved As Boolean
    Public CanPlayAudio As Boolean
    Public Kill_List As New List(Of Tank_Object)
    Public Score As Integer
    Public blnAppear As Boolean

    Dim Bullet_Speedfactor As Integer

    Dim m_Broken As Boolean
    Dim m_Level As Integer
    'Dim dicTank As New Dictionary(Of String, Bitmap)
    Dim blnMove As Boolean

    Dim WithEvents Timer1 As New Timer
    Dim thd As Threading.Thread
    Private Delegate Sub dlgPlayAudio(ByVal strFile As String)

    Dim Task_List As New List(Of Task)
    Dim intImgID As Integer

    Private Enum TaskList
        Appear
        Helmet
    End Enum

    Private Structure Task
        Dim TaskName As TaskList
        Dim TaskTime As Integer
        Dim TaskTimeDiff As Integer
    End Structure

    Public Property Move_Direction As Direction
        Set(value As Direction)
            m_Move_Direction = value
            Image = Image_Position.dicImageList.Item(Type & "_" & m_Level & "_" & value & "_" & Convert.ToInt32(blnMove))
        End Set
        Get
            Return m_Move_Direction
        End Get
    End Property

    Public Property LifeValue As Integer
        Set(value As Integer)
            m_LifeValue = value

            If m_LifeValue <= 0 Then
                Broken = True

            End If
        End Set
        Get
            Return m_LifeValue
        End Get
    End Property

    Public Sub Hit(ByVal intValue As Integer)
        m_LifeValue -= intValue

        If m_LifeValue <= 0 Then
            Broken = True
        Else
            If CanPlayAudio Then
                'My.Computer.Audio.Play(Image_Position.Audio_Enemy_Armor_hit, AudioPlayMode.Background)
                thd = New Threading.Thread(New Threading.ParameterizedThreadStart(AddressOf PlayAudio))
                thd.Start(Image_Position.Audio_Enemy_Armor_hit)

            End If
        End If
    End Sub

    Public Property Level As Integer
        Get
            Return m_Level
        End Get

        Set(value As Integer)
            m_Level = value
            Image = Image_Position.dicImageList.Item(Type & "_" & m_Level & "_" & m_Move_Direction & "_" & 0)
            If (Extras_Bonus And Object_Bonus.Ship) = Object_Bonus.Ship Then
                Image = Image_Merge(Image_Position.ShipICO)
            End If
            If (Extras_Bonus And Object_Bonus.Helmet) = Object_Bonus.Helmet Then
                Image = Image_Merge(Image_Position.HelmetICO)
            End If
        End Set
    End Property

    Public Property Broken As Boolean
        Set(value As Boolean)
            m_Broken = value

            If m_Broken Then
                '坦克爆炸
                Image = Image_Position.dicImageList("Boom_2")
                If CanPlayAudio Then
                    'My.Computer.Audio.Play(Image_Position.Audio_Tank_Boom, AudioPlayMode.Background)
                    thd = New Threading.Thread(New Threading.ParameterizedThreadStart(AddressOf PlayAudio))
                    thd.Start(Image_Position.Audio_Tank_Boom)
                End If

            End If
        End Set
        Get
            Return m_Broken
        End Get
    End Property

    Public Sub New()
        MyBase.New()
        Speedfactor = 1
        blnMove = False
        Parent = Me
        m_Broken = False
        CanPlayAudio = True
        Extras_Bonus = Object_Bonus.Null
        Bullet_Speedfactor = 5
        intImgID = 0
        Timer1.Interval = 50
        Timer1.Start()
        blnAppear = False
    End Sub

    Public Sub LoadImage(Optional ByVal intDirection As Direction = Direction.Down, Optional ByVal intSide As Integer = 0)
        Image = Image_Position.dicImageList.Item(Type & "_" & m_Level & "_" & intDirection & "_" & intSide)
    End Sub

    Public Sub Boom()
        Image = Image_Position.dicImageList.Item("Boom_2")
    End Sub

    Public Sub Move(ByVal blnCanMove As Boolean, ByVal Direction As Direction)
        isMoved = True

        m_Move_Direction = Direction

        If blnCanMove Then

            Select Case Direction
                Case Base_Object.Direction.Down
                    Rect = New RectangleF(Rect.X, Rect.Y + Speedfactor, Rect.Width, Rect.Height)

                Case Base_Object.Direction.Left
                    'Rect.Offset(-Speedfactor, 0)
                    Rect = New RectangleF(Rect.X - Speedfactor, Rect.Y, Rect.Width, Rect.Height)

                Case Base_Object.Direction.Right
                    'Rect.Offset(Speedfactor, 0)
                    Rect = New RectangleF(Rect.X + Speedfactor, Rect.Y, Rect.Width, Rect.Height)

                Case Base_Object.Direction.Up
                    'Rect.Offset(0, -Speedfactor)
                    Rect = New RectangleF(Rect.X, Rect.Y - Speedfactor, Rect.Width, Rect.Height)
            End Select

        End If

        blnMove = Not blnMove
        'Image = dicTank.Item(Type & "_" & Direction & "_" & Convert.ToInt32(blnMove))
        Image = Image_Position.dicImageList.Item(Type & "_" & m_Level & "_" & Direction & "_" & Convert.ToInt32(blnMove))

        If (Extras_Bonus And Object_Bonus.Ship) = Object_Bonus.Ship Then
            Image = Image_Merge(Image_Position.ShipICO)
        End If
        If (Extras_Bonus And Object_Bonus.Helmet) = Object_Bonus.Helmet Then
            Image = Image_Merge(Image_Position.HelmetICO)
        End If

        isMoved = False

        If CanPlayAudio Then
            'thd = New Threading.Thread(New Threading.ParameterizedThreadStart(AddressOf PlayAudio))
            'thd.Start(Image_Position.Audio_Tank_Play_Move)
        End If

    End Sub

    Public Sub PlayAudio(ByVal strFile As String)

        My.Computer.Audio.Play(strFile, AudioPlayMode.Background)

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

    Public Sub MoveStop(ByVal intDirection As Direction)
        Dim Pos As Point

        isMoved = True

        Pos = Position
        Select Case intDirection
            Case Direction.Down
                If Rect.Y > Pos.Y * 16 Then
                    Do While Rect.Y + Speedfactor < (Pos.Y + 1) * 16
                        Move(True, Direction.Down)
                        Wait(20)
                    Loop
                    Rect = New RectangleF(Pos.X * 16, (Pos.Y + 1) * 16, Rect.Width, Rect.Height)
                End If

            Case Direction.Left
                Do While Rect.X - Speedfactor > (Pos.X) * 16
                    Move(True, Direction.Left)
                    Wait(20)
                Loop
                Rect = New RectangleF((Pos.X) * 16, (Pos.Y) * 16, Rect.Width, Rect.Height)

            Case Direction.Right
                If Rect.X > Pos.X * 16 Then
                    Do While Rect.X + Speedfactor < (Pos.X + 1) * 16
                        Move(True, Direction.Right)
                        Wait(20)
                    Loop
                    Rect = New RectangleF((Pos.X + 1) * 16, (Pos.Y) * 16, Rect.Width, Rect.Height)
                End If

            Case Direction.Up
                Do While (Rect.Y - Speedfactor) > (Pos.Y) * 16
                    Move(True, Direction.Up)
                    Wait(20)
                Loop
                Rect = New RectangleF((Pos.X) * 16, (Pos.Y) * 16, Rect.Width, Rect.Height)

        End Select


        blnMove = Not blnMove
        Image = Image_Position.dicImageList.Item(Type & "_" & m_Level & "_" & intDirection & "_" & Convert.ToInt32(blnMove))
        If (Extras_Bonus And Object_Bonus.Ship) = Object_Bonus.Ship Then
            Image = Image_Merge(Image_Position.ShipICO)
        End If
        If (Extras_Bonus And Object_Bonus.Helmet) = Object_Bonus.Helmet Then
            Image = Image_Merge(Image_Position.HelmetICO)
        End If
        isMoved = False
    End Sub

    Public Sub Appear()
        Dim tmpTask As Task
        tmpTask.TaskName = TaskList.Appear
        tmpTask.TaskTime = My.Computer.Clock.TickCount
        tmpTask.TaskTimeDiff = 1000

        Task_List.Add(tmpTask)
    End Sub

    Public Sub Got_Extras_Bonus(ByVal Bonus As Object_Bonus)
        Select Case Bonus
            Case Object_Bonus.Gun    '枪
                Lethality = 200
                If (Extras_Bonus And Object_Bonus.Gun) <> Object_Bonus.Gun Then
                    Extras_Bonus = Extras_Bonus Or Object_Bonus.Gun
                End If

            Case Object_Bonus.Helmet '头盔
                Image = Image_Merge(Image_Position.HelmetICO)
                If (Extras_Bonus And Object_Bonus.Helmet) <> Object_Bonus.Helmet Then
                    Extras_Bonus = Extras_Bonus Or Object_Bonus.Helmet
                End If

                Dim tmpTask As Task
                tmpTask.TaskName = TaskList.Helmet
                tmpTask.TaskTime = My.Computer.Clock.TickCount
                tmpTask.TaskTimeDiff = 30000

                Task_List.Add(tmpTask)

            Case Object_Bonus.Ship
                Image = Image_Merge(Image_Position.ShipICO)
                If (Extras_Bonus And Object_Bonus.Ship) <> Object_Bonus.Ship Then
                    Extras_Bonus = Extras_Bonus Or Object_Bonus.Ship
                End If

            Case Object_Bonus.Star
                Select Case Type
                    Case Object_Type.Tank_IP, Object_Type.Tank_IIP
                        If Level < 4 Then
                            Level += 1
                            Speedfactor = 3
                            LifeValue += 100
                        End If
                    Case Object_Type.Tank_Army1, Object_Type.Tank_Army2, Object_Type.Tank_Army3, Object_Type.Tank_Army4
                        Speedfactor = 3

                End Select
                Bullet_Speedfactor = 10

                If (Extras_Bonus And Object_Bonus.Star) <> Object_Bonus.Star Then
                    Extras_Bonus = Extras_Bonus Or Object_Bonus.Star
                End If

        End Select
    End Sub

    Private Function Image_Merge(ByVal Merge_bitmap As Bitmap) As Bitmap
        Dim g As Graphics
        Dim tmpBit As Bitmap

        tmpBit = Image.Clone

        g = Graphics.FromImage(tmpBit)
        g.DrawImage(Merge_bitmap, 0, 0)

        Return tmpBit
    End Function

    Public Sub Shoot()
        If Bullet_Count < 2 Then
            Dim tmpBullet As New Bullet_Object

            tmpBullet.Image_Position = Image_Position
            tmpBullet.Shooter = Me
            tmpBullet.Shoot_Direction = m_Move_Direction
            tmpBullet.Type = Object_Type.Tank_Bullet
            tmpBullet.lethality = Lethality
            tmpBullet.Speedfactor = Bullet_Speedfactor

            Select Case m_Move_Direction
                Case Direction.Down
                    tmpBullet.Rect = New RectangleF(Rect.X + 16 - tmpBullet.Rect.Width / 2, Rect.Y + Rect.Height, tmpBullet.Rect.Width, tmpBullet.Rect.Height)

                Case Direction.Left
                    tmpBullet.Rect = New RectangleF(Rect.X, Rect.Y + 16 - tmpBullet.Rect.Height / 2, tmpBullet.Rect.Width, tmpBullet.Rect.Height)

                Case Direction.Right
                    tmpBullet.Rect = New RectangleF(Rect.X + Rect.Width, Rect.Y + 16 - tmpBullet.Rect.Height / 2, tmpBullet.Rect.Width, tmpBullet.Rect.Height)

                Case Direction.Up
                    tmpBullet.Rect = New RectangleF(Rect.X + 16 - tmpBullet.Rect.Width / 2, Rect.Y, tmpBullet.Rect.Width, tmpBullet.Rect.Height)
            End Select

            Bullet_List.Add(tmpBullet)

            If CanPlayAudio Then
                If Type = 1 Or Type = 2 Then
                    thd = New Threading.Thread(New Threading.ParameterizedThreadStart(AddressOf PlayAudio))
                    thd.Start(Image_Position.Audio_Tank_Play_Shoot)
                End If

            End If

            Bullet_Count += 1

            'Threading.Thread.Sleep(20)
            'Wait(20)
        End If

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Dim tmpTask As Task
        Dim i As Integer

        Try

            For i = Task_List.Count - 1 To 0 Step -1
                tmpTask = Task_List.Item(i)

                Select Case tmpTask.TaskName
                    Case TaskList.Helmet
                        If My.Computer.Clock.TickCount - tmpTask.TaskTime >= tmpTask.TaskTimeDiff Then
                            Extras_Bonus = Extras_Bonus Xor Object_Bonus.Helmet
                            Image = Image_Position.dicImageList.Item(Type & "_" & m_Level & "_" & m_Move_Direction & "_" & Convert.ToInt32(blnMove))

                            If (Extras_Bonus And Object_Bonus.Ship) = Object_Bonus.Ship Then
                                Image = Image_Merge(Image_Position.ShipICO)
                            End If

                            Task_List.RemoveAt(i)
                        Else
                            Image = Image_Position.dicImageList.Item(Type & "_" & m_Level & "_" & m_Move_Direction & "_" & Convert.ToInt32(blnMove))
                            If intImgID Mod 2 = 0 Then
                                Image = Image_Merge(Image_Position.HelmetICO)
                                intImgID = 0
                            End If
                        End If

                    Case TaskList.Appear
                        If My.Computer.Clock.TickCount - tmpTask.TaskTime >= tmpTask.TaskTimeDiff Then
                            Image = Image_Position.dicImageList.Item(Type & "_" & m_Level & "_" & m_Move_Direction & "_" & Convert.ToInt32(blnMove))
                            Task_List.RemoveAt(i)
                            blnAppear = True
                        Else
                            If Me.Type < 3 Then
                                If intImgID >= 1 Then
                                    intImgID = 0
                                End If
                                Image = Image_Position.dicImageList.Item(Type & "_" & m_Level & "_" & m_Move_Direction & "_" & Convert.ToInt32(blnMove))
                                Image = Image_Merge(Image_Position.dicImageList.Item("Player_Disp_" & intImgID))

                                If intImgID >= 1 Then
                                    intImgID = 0
                                End If
                            Else
                                If intImgID >= 3 Then
                                    intImgID = 0
                                End If
                                'Image = Image_Position.dicImageList.Item(Type & "_" & m_Level & "_" & Move_Direction & "_" & Convert.ToInt32(blnMove))
                                Image = Image_Position.dicImageList.Item("Army_Disp_" & intImgID) ' Image_Merge(Image_Position.dicImageList.Item("Army_Disp_" & intImgID))

                                If intImgID >= 3 Then
                                    intImgID = 0
                                End If
                            End If
                        End If

                End Select

            Next
            intImgID += 1

            If intImgID > 500 Then
                intImgID = 0
            End If
            'Application.DoEvents()

        Catch ex As Exception

        End Try
    End Sub
End Class
