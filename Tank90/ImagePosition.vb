
Public Class ImagePosition
    Dim m_ImgPath As String
    Public TankIP_Rect As Rectangle
    Public TankIIP_Rect As Rectangle
    Public Army1_Rect As Rectangle
    Public Army2_Rect As Rectangle
    Public Army3_Rect As Rectangle
    Public Army4_Rect As Rectangle

    Dim m_ArmyICO As Rectangle
    Dim m_PlayICO As Rectangle
    Dim m_IPICO As Rectangle
    Dim m_IIPICO As Rectangle
    Dim m_GameOver As Rectangle

    Public ArmyDisp_Rect As Rectangle
    Public PlayDisp_Rect As Rectangle

    Public Boom_Rect As Rectangle
    Public BigBoom_Rect As Rectangle

    Dim m_Bullet As Rectangle
    Dim m_Brick As Rectangle    '砖块
    Dim m_Steel As Rectangle    '钢块
    Dim m_Grass As Rectangle    '草地
    Dim m_River As Rectangle    '河流
    Dim m_Snow As Rectangle     '雪地
    Dim g As Graphics

    Dim m_Img As Image

    Public Audio_Tank_Play_Move As String
    Public Audio_Tank_Play_Shoot As String
    Public Audio_Tank_Boom As String
    Public Audio_BrickErase As String
    Public Audio_Base_Broken As String
    Public Audio_GameOver As String
    Public Audio_Bullet_Destroy As String
    Public Audio_GameStar As String
    Public Audio_Enemy_Armor_hit As String
    Public Audio_Enemy_Move As String
    Public Audio_Bonus_grenade As String
    Public Audio_Bonus_default As String
    Public Audio_Bonus_life As String

    Public dicImageList As New Dictionary(Of String, Bitmap)

    Dim m_Ship As Rectangle    '船
    Dim m_Time As Rectangle    '时钟
    Dim m_Gun As Rectangle     '枪
    Dim m_Star As Rectangle   '星星
    Dim m_Helmet As Rectangle  '头盔
    Dim m_BoomShell As Rectangle    '炸弹
    Dim m_Shovel As Rectangle  '铲子
    Dim m_Life As Rectangle    '生命

    Dim m_Base As Rectangle
    Dim m_BaseBroken As Rectangle
    Dim m_StageFlag As Rectangle

    Dim m_HelmetICO As Rectangle
    Dim m_ShipICO As Rectangle


    Public Property Sprite_ImgPath() As String
        Get
            Return m_ImgPath
        End Get
        Set(value As String)
            m_ImgPath = value
            If IO.File.Exists(value) Then
                m_Img = Image.FromFile(m_ImgPath)
                g = Graphics.FromImage(m_Img)
            End If

            TankIP_Rect = New Rectangle(1, 1, 32, 32)
            TankIIP_Rect = New Rectangle(1, 35, 32, 32)
            Army1_Rect = New Rectangle(1, 69, 32, 32)
            Army2_Rect = New Rectangle(273, 69, 32, 32)
            Army3_Rect = New Rectangle(545, 69, 32, 32)
            Army4_Rect = New Rectangle(817, 69, 32, 32)
            m_ArmyICO = New Rectangle(44, 146, 14, 14)
            m_PlayICO = New Rectangle(350, 213, 14, 16)
            m_IPICO = New Rectangle(69, 137, 32, 32)
            m_IIPICO = New Rectangle(103, 137, 32, 32)
            m_GameOver = New Rectangle(136, 136, 68, 34)
            ArmyDisp_Rect = New Rectangle(545, 137, 32, 32)
            PlayDisp_Rect = New Rectangle(443, 239, 32, 32)
            Boom_Rect = New Rectangle(680, 136, 34, 34)
            BigBoom_Rect = New Rectangle(782, 136, 68, 68)
            m_Bullet = New Rectangle(0, 170, 8, 10)
            m_Brick = New Rectangle(613, 171, 16, 16)
            m_Steel = New Rectangle(1, 206, 16, 16)
            m_River = New Rectangle(1, 239, 32, 32)
            m_Grass = New Rectangle(137, 239, 32, 32)
            m_Snow = New Rectangle(307, 239, 32, 32)

            m_Ship = New Rectangle(443, 205, 32, 32)
            m_Time = New Rectangle(477, 205, 32, 32)
            m_Gun = New Rectangle(511, 205, 32, 32)
            m_Star = New Rectangle(545, 205, 32, 32)
            m_Helmet = New Rectangle(613, 205, 32, 32)
            m_BoomShell = New Rectangle(681, 205, 32, 32)
            m_Shovel = New Rectangle(749, 205, 32, 32)
            m_Life = New Rectangle(817, 205, 32, 32)

            m_Base = New Rectangle(647, 171, 32, 32)
            m_BaseBroken = New Rectangle(681, 171, 32, 32)
            m_StageFlag = New Rectangle(715, 171, 32, 32)

            m_HelmetICO = New Rectangle(443, 239, 32, 32)
            m_ShipICO = New Rectangle(409, 239, 32, 32)

            LoadImage()
        End Set
    End Property

    Public ReadOnly Property IIP_ICO As Bitmap
        Get
            Return GetImage(m_IIPICO)
        End Get
    End Property

    Public ReadOnly Property IP_ICO As Bitmap
        Get
            Return GetImage(m_IPICO)
        End Get
    End Property

    Public ReadOnly Property Enemy_ICO As Bitmap
        Get
            Return GetImage(m_ArmyICO)
        End Get
    End Property

    Public ReadOnly Property Play_ICO As Bitmap
        Get
            Return GetImage(m_PlayICO)
        End Get
    End Property



    Public Sub LoadImage()
        Dim strKey As String = ""
        'Dim intDirect As Integer
        Dim intX, intY, intW, intH As Integer
        Dim intNum As Integer
        Dim Rect As Rectangle

        Dim intType As Integer
        Dim m_Level As Integer
        Dim Direction As Integer

        dicImageList.Clear()

        For intType = 1 To 6
            Select Case intType
                Case Base_Object.Object_Type.Tank_IP
                    For m_Level = 1 To 4
                        For Direction = 0 To 3
                            For intNum = 0 To 1
                                intX = TankIP_Rect.Left + (m_Level - 1) * 8 * 34 + 1
                                intY = TankIP_Rect.Top
                                intW = TankIP_Rect.Width
                                intH = TankIP_Rect.Height

                                strKey = intType & "_" & m_Level & "_" & Direction & "_" & intNum
                                Rect = New Rectangle(intX + (34 * Direction * 2 + intNum * 34), intY, intW, intH)

                                dicImageList.Add(strKey, GetImage(Rect))
                            Next
                        Next
                    Next


                Case Base_Object.Object_Type.Tank_IIP
                    For m_Level = 1 To 4
                        For Direction = 0 To 3
                            For intNum = 0 To 1
                                intX = TankIIP_Rect.Left + (m_Level - 1) * 8 * 34 + 1
                                intY = TankIIP_Rect.Top
                                intW = TankIIP_Rect.Width
                                intH = TankIIP_Rect.Height

                                strKey = intType & "_" & m_Level & "_" & Direction & "_" & intNum
                                Rect = New Rectangle(intX + (34 * Direction * 2 + intNum * 34), intY, intW, intH)

                                dicImageList.Add(strKey, GetImage(Rect))
                            Next
                        Next
                    Next

                Case Base_Object.Object_Type.Tank_Army1
                    For m_Level = 1 To 2
                        For Direction = 0 To 3
                            For intNum = 0 To 1
                                intX = Army1_Rect.Left
                                intY = Army1_Rect.Top + (m_Level - 1) * 34 + 1
                                intW = Army1_Rect.Width
                                intH = Army1_Rect.Height

                                strKey = intType & "_" & m_Level & "_" & Direction & "_" & intNum
                                Rect = New Rectangle(intX + (34 * Direction * 2 + intNum * 34), intY, intW, intH)

                                dicImageList.Add(strKey, GetImage(Rect))
                            Next
                        Next
                    Next


                Case Base_Object.Object_Type.Tank_Army2
                    For m_Level = 1 To 2
                        For Direction = 0 To 3
                            For intNum = 0 To 1
                                intX = Army2_Rect.Left
                                intY = Army2_Rect.Top + (m_Level - 1) * Army2_Rect.Height
                                intW = Army2_Rect.Width
                                intH = Army2_Rect.Height

                                strKey = intType & "_" & m_Level & "_" & Direction & "_" & intNum
                                Rect = New Rectangle(intX + (34 * Direction * 2 + intNum * 34), intY, intW, intH)

                                dicImageList.Add(strKey, GetImage(Rect))
                            Next
                        Next
                    Next

                Case Base_Object.Object_Type.Tank_Army3
                    For m_Level = 1 To 2
                        For Direction = 0 To 3
                            For intNum = 0 To 1
                                intX = Army3_Rect.Left
                                intY = Army3_Rect.Top + (m_Level - 1) * Army3_Rect.Height
                                intW = Army3_Rect.Width
                                intH = Army3_Rect.Height

                                strKey = intType & "_" & m_Level & "_" & Direction & "_" & intNum
                                Rect = New Rectangle(intX + (34 * Direction * 2 + intNum * 34), intY, intW, intH)

                                dicImageList.Add(strKey, GetImage(Rect))
                            Next
                        Next
                    Next

                Case Base_Object.Object_Type.Tank_Army4
                    For m_Level = 1 To 2
                        For Direction = 0 To 3
                            For intNum = 0 To 1
                                intX = Army4_Rect.Left
                                intY = Army4_Rect.Top + (m_Level - 1) * Army4_Rect.Height
                                intW = Army4_Rect.Width
                                intH = Army4_Rect.Height

                                strKey = intType & "_" & m_Level & "_" & Direction & "_" & intNum
                                Rect = New Rectangle(intX + (34 * Direction * 2 + intNum * 34), intY, intW, intH)

                                dicImageList.Add(strKey, GetImage(Rect))
                            Next
                        Next
                    Next

            End Select
        Next

        strKey = "Player_Disp" & "_" & 0
        intX = PlayDisp_Rect.X
        intY = PlayDisp_Rect.Y
        intW = PlayDisp_Rect.Width
        intH = PlayDisp_Rect.Height

        Rect = New Rectangle(intX, intY, intW, intH)
        dicImageList.Add(strKey, GetImage(Rect))

        strKey = "Player_Disp" & "_" & 1
        Rect = New Rectangle(intX + 34 * 2, intY, intW, intH)
        dicImageList.Add(strKey, GetImage(Rect))

        intX = ArmyDisp_Rect.X
        intY = ArmyDisp_Rect.Y
        intW = ArmyDisp_Rect.Width
        intH = ArmyDisp_Rect.Height

        For intNum = 3 To 0 Step -1
            strKey = "Army_Disp_" & intNum
            Rect = New Rectangle(intX + (3 - intNum) * 34, intY, intW, intH)
            dicImageList.Add(strKey, GetImage(Rect))
        Next

        For intNum = 0 To 2
            strKey = "Boom_" & intNum
            Rect = New Rectangle(Boom_Rect.X + Boom_Rect.Width * intNum, intY, Boom_Rect.Width, Boom_Rect.Height)
            dicImageList.Add(strKey, GetImage(Rect))
        Next

        For intNum = 0 To 1
            strKey = "BigBoom_" & intNum
            Rect = New Rectangle(BigBoom_Rect.X + BigBoom_Rect.Width * intNum, intY, BigBoom_Rect.Width, BigBoom_Rect.Height)
            dicImageList.Add(strKey, GetImage(Rect))
        Next

        Dim tmpBit As Bitmap

        tmpBit = Me.Bullet

        For intDirect = 0 To 3
            strKey = "Bullet_" & intDirect
            dicImageList.Add(strKey, CType(tmpBit.Clone, Bitmap))
            tmpBit.RotateFlip(RotateFlipType.Rotate90FlipNone)
        Next

    End Sub

    Public ReadOnly Property ShipICO As Bitmap
        Get
            Return GetImage(m_ShipICO)
        End Get
    End Property

    Public ReadOnly Property HelmetICO As Bitmap
        Get
            Return GetImage(m_HelmetICO)
        End Get
    End Property

    ''' <summary>
    ''' 关卡旗帜
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property StageFlag As Bitmap
        Get
            Return GetImage(m_StageFlag)
        End Get
    End Property

    ''' <summary>
    ''' 基地损坏
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property BaseBroken As Bitmap
        Get
            Return GetImage(m_BaseBroken)
        End Get
    End Property

    ''' <summary>
    ''' 基地
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Base As Bitmap
        Get
            Return GetImage(m_Base)
        End Get
    End Property

    ''' <summary>
    ''' 生命图标
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Bonus_Life As Bitmap
        Get
            Return GetImage(m_Life)
        End Get
    End Property

    ''' <summary>
    ''' 铲子图标
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Bonus_Shovel As Bitmap
        Get
            Return GetImage(m_Shovel)
        End Get
    End Property

    ''' <summary>
    ''' 炸弹图标
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Bonus_BoomShell As Bitmap
        Get
            Return GetImage(m_BoomShell)
        End Get
    End Property

    ''' <summary>
    ''' 头盔图标
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Bonus_Helmet As Bitmap
        Get
            Return GetImage(m_Helmet)
        End Get
    End Property

    ''' <summary>
    ''' 星星图标
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Bonus_Star As Bitmap
        Get
            Return GetImage(m_Star)
        End Get
    End Property

    ''' <summary>
    ''' 枪图标
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Bonus_Gun As Bitmap
        Get
            Return GetImage(m_Gun)
        End Get
    End Property

    ''' <summary>
    ''' 时钟图标
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Bonus_TimeClock As Bitmap
        Get
            Return GetImage(m_Time)
        End Get
    End Property

    ''' <summary>
    ''' 轮船图标
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Bonus_Ship As Bitmap
        Get
            Return GetImage(m_Ship)
        End Get
    End Property

    ''' <summary>
    ''' 子弹
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Bullet As Bitmap
        Get
            Return GetImage(m_Bullet)
        End Get
    End Property

    Public Function GetImage(ByVal rect As Rectangle) As Bitmap
        Dim tmpBit As Bitmap
        Dim tmpG As Graphics

        tmpBit = New Bitmap(rect.Width, rect.Height)
        tmpG = Graphics.FromImage(tmpBit)
        tmpG.DrawImage(m_Img, 0, 0, rect, GraphicsUnit.Pixel)

        Return tmpBit
    End Function

    Public ReadOnly Property Army4 As Bitmap
        Get
            Dim tmpBit As Bitmap
            Dim tmpG As Graphics

            tmpBit = New Bitmap(Army4_Rect.Width, Army4_Rect.Height)
            tmpG = Graphics.FromImage(tmpBit)
            tmpG.DrawImage(m_Img, 0, 0, Army4_Rect, GraphicsUnit.Pixel)

            Return tmpBit
        End Get
    End Property

    Public ReadOnly Property Army3 As Bitmap
        Get
            Dim tmpBit As Bitmap
            Dim tmpG As Graphics

            tmpBit = New Bitmap(Army3_Rect.Width, Army3_Rect.Height)
            tmpG = Graphics.FromImage(tmpBit)
            tmpG.DrawImage(m_Img, 0, 0, Army3_Rect, GraphicsUnit.Pixel)

            Return tmpBit
        End Get
    End Property

    Public ReadOnly Property Army2 As Bitmap
        Get
            Dim tmpBit As Bitmap
            Dim tmpG As Graphics

            tmpBit = New Bitmap(Army2_Rect.Width, Army2_Rect.Height)
            tmpG = Graphics.FromImage(tmpBit)
            tmpG.DrawImage(m_Img, 0, 0, Army2_Rect, GraphicsUnit.Pixel)

            Return tmpBit
        End Get
    End Property

    Public ReadOnly Property Army1 As Bitmap
        Get
            Dim tmpBit As Bitmap
            Dim tmpG As Graphics

            tmpBit = New Bitmap(Army1_Rect.Width, Army1_Rect.Height)
            tmpG = Graphics.FromImage(tmpBit)
            tmpG.DrawImage(m_Img, 0, 0, Army1_Rect, GraphicsUnit.Pixel)

            Return tmpBit
        End Get
    End Property

    Public ReadOnly Property TankIIP As Bitmap
        Get
            Dim tmpBit As Bitmap
            Dim tmpG As Graphics

            tmpBit = New Bitmap(TankIIP_Rect.Width, TankIIP_Rect.Height)
            tmpG = Graphics.FromImage(tmpBit)
            tmpG.DrawImage(m_Img, 0, 0, TankIIP_Rect, GraphicsUnit.Pixel)

            Return tmpBit
        End Get
    End Property

    Public ReadOnly Property TankIP As Bitmap
        Get
            Dim tmpBit As Bitmap
            Dim tmpG As Graphics

            tmpBit = New Bitmap(TankIP_Rect.Width, TankIP_Rect.Height)
            tmpG = Graphics.FromImage(tmpBit)
            tmpG.DrawImage(m_Img, 0, 0, TankIP_Rect, GraphicsUnit.Pixel)

            Return tmpBit
        End Get
    End Property

    ''' <summary>
    ''' 砖块图片
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Brick As Bitmap
        Get
            Dim tmpBit As Bitmap
            Dim tmpG As Graphics

            tmpBit = New Bitmap(m_Brick.Width, m_Brick.Height)
            tmpG = Graphics.FromImage(tmpBit)
            tmpG.DrawImage(m_Img, 0, 0, m_Brick, GraphicsUnit.Pixel)

            Return tmpBit
        End Get
    End Property

    ''' <summary>
    ''' 钢板图片
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Steel As Bitmap
        Get
            Dim tmpBit As Bitmap
            Dim tmpG As Graphics

            tmpBit = New Bitmap(m_Steel.Width, m_Steel.Height)
            tmpG = Graphics.FromImage(tmpBit)
            tmpG.DrawImage(m_Img, 0, 0, m_Steel, GraphicsUnit.Pixel)
            Return tmpBit
        End Get
    End Property

    Public ReadOnly Property River As Bitmap
        Get
            Dim tmpBit As Bitmap
            Dim tmpG As Graphics

            tmpBit = New Bitmap(m_River.Width, m_River.Height)
            tmpG = Graphics.FromImage(tmpBit)
            tmpG.DrawImage(m_Img, 0, 0, m_River, GraphicsUnit.Pixel)
            Return tmpBit
        End Get
    End Property

    Public ReadOnly Property Grass As Bitmap
        Get
            Dim tmpBit As Bitmap
            Dim tmpG As Graphics

            tmpBit = New Bitmap(m_Grass.Width, m_Grass.Height)
            tmpG = Graphics.FromImage(tmpBit)
            tmpG.DrawImage(m_Img, 0, 0, m_Grass, GraphicsUnit.Pixel)
            Return tmpBit
        End Get
    End Property

    Public ReadOnly Property Snow As Bitmap
        Get
            Dim tmpBit As Bitmap
            Dim tmpG As Graphics

            tmpBit = New Bitmap(m_Snow.Width, m_Snow.Height)
            tmpG = Graphics.FromImage(tmpBit)
            tmpG.DrawImage(m_Img, 0, 0, m_Snow, GraphicsUnit.Pixel)
            Return tmpBit
        End Get
    End Property

    Public Sub New()
        Audio_Tank_Play_Move = Application.StartupPath & "\Resource\sounds\player.move.wav"
        Audio_Tank_Play_Shoot = Application.StartupPath & "\Resource\sounds\shoot.wav"
        Audio_Tank_Boom = Application.StartupPath & "\Resource\sounds\kill.wav"
        Audio_BrickErase = Application.StartupPath & "\Resource\sounds\brickErase.wav"
        Audio_Base_Broken = Application.StartupPath & "\Resource\sounds\buh.wav"
        Audio_GameOver = Application.StartupPath & "\Resource\sounds\gameOver.wav"
        Audio_Bullet_Destroy = Application.StartupPath & "\Resource\sounds\bullet.destroy.wav"
        Audio_GameStar = Application.StartupPath & "\Resource\sounds\intro.wav"
        Audio_Enemy_Armor_hit = Application.StartupPath & "\Resource\sounds\enemy.armor.hit.wav"
        Audio_Enemy_Move = Application.StartupPath & "\Resource\sounds\enemy.move.wav"
        Audio_Bonus_grenade = Application.StartupPath & "\Resource\sounds\bonus.grenade.wav"
        Audio_Bonus_default = Application.StartupPath & "\Resource\sounds\bonus.default.wav"
        Audio_Bonus_life = Application.StartupPath & "\Resource\sounds\bonus.life.wav"
    End Sub
End Class
