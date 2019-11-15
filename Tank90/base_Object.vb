
Public Class Base_Object
    Public Enum Direction
        Up
        Right
        Down
        Left
    End Enum

    Public Enum Object_Type
        Null
        Tank_IP     'Play1
        Tank_IIP    'Play2
        Tank_Army1  '敌人1
        Tank_Army2
        Tank_Army3
        Tank_Army4
        'Tank_Army5  '红色带宝物
        'Tank_Army6
        'Tank_Army7
        'Tank_Army8
        Tank_Bullet '子弹
        Bulid_Brick '砖块
        Bulid_Steel '钢块
        Bulid_Grass '草地
        Bulid_River '河流
        Bulid_Snow  '雪地
        Bulid_Base  '基地
        Bulid_BaseBroken  '基地损坏

        Bonus_Ship    '船
        Bonus_Time    '时钟
        Bonus_Gun     '枪
        Bonus_Star    '星星
        Bonus_Helmet  '头盔
        Bonus_Boom    '炸弹
        Bonus_Shovel  '铲子
        Bonus_Life    '生命
    End Enum

    Public Enum Object_Bonus
        Null
        Ship = 1   '船
        Time = 2   '时钟
        Gun = 4    '枪
        Star = 8   '星星
        Helmet = 16  '头盔
        Boom = 32   '炸弹
        Shovel = 64 '铲子
        Life = 128   '生命
    End Enum

    'Public Enum ShootOwner
    '    Play_1P
    '    Play_2P
    '    Enemy
    'End Enum

    Dim m_Rect As RectangleF
    Dim m_Position As Point
    Dim m_Image As Bitmap
    Dim m_Extras_Bonus As Object_Bonus
    Dim m_Type As Object_Type

    Public Image_Position As ImagePosition
    Public Parent As Object

    Public Property Attach_Function As Object_Bonus
        Set(value As Object_Bonus)
            m_Extras_Bonus = value
        End Set
        Get
            Return m_Extras_Bonus
        End Get
    End Property

    Public Property Type As Object_Type
        Set(value As Object_Type)
            m_Type = value
        End Set
        Get
            Return m_Type
        End Get
    End Property

    Public Property Rect As RectangleF
        Set(value As RectangleF)
            m_Rect = value

            m_Position.X = Convert.ToInt32((m_Rect.X \ 16))
            m_Position.Y = Convert.ToInt32((m_Rect.Y \ 16))
        End Set
        Get
            m_Position.X = Convert.ToInt32((m_Rect.X \ 16))
            m_Position.Y = Convert.ToInt32((m_Rect.Y \ 16))
            Return m_Rect

        End Get
    End Property

    Public Property Position As Point
        Set(value As Point)
            m_Position = value

            m_Rect.X = value.X * 16
            m_Rect.Y = value.Y * 16
        End Set
        Get
            Return m_Position
        End Get
    End Property

    Public Property Image As Bitmap
        Set(value As Bitmap)
            m_Image = value

            m_Rect.Width = value.Width
            m_Rect.Height = value.Height
        End Set
        Get
            Return m_Image
        End Get
    End Property


End Class
