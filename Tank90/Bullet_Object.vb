
Public Class Bullet_Object
    Inherits Base_Object

    Public Speedfactor As Integer
    'Public ImgPos As ImagePosition
    Public lethality As Integer     '杀伤力
    Public m_Direction As Direction
    Public Shooter As Tank_Object

    Dim m_Broken As Boolean

    Public Sub New()
        MyBase.New()
        Parent = Me
        m_Broken = False
    End Sub

    Public Property Broken As Boolean
        Set(value As Boolean)
            m_Broken = value

            If m_Broken Then
                Boom()
            End If
        End Set
        Get
            Return m_Broken
        End Get
    End Property


    Public Property Shoot_Direction As Direction
        Get
            Return m_Direction
        End Get
        Set(value As Direction)
            m_Direction = value
            Image = Image_Position.dicImageList.Item("Bullet_" & value)
        End Set
    End Property

    Public Sub Move()
        Select Case Shoot_Direction
            Case Direction.Down
                Rect = New RectangleF(Rect.X, Rect.Y + Speedfactor, Rect.Width, Rect.Height)

            Case Direction.Left
                Rect = New RectangleF(Rect.X - Speedfactor, Rect.Y, Rect.Width, Rect.Height)

            Case Direction.Right
                Rect = New RectangleF(Rect.X + Speedfactor, Rect.Y, Rect.Width, Rect.Height)

            Case Direction.Up
                Rect = New RectangleF(Rect.X, Rect.Y - Speedfactor, Rect.Width, Rect.Height)
        End Select
    End Sub

    Public Sub Boom()
        'Dim intTcnt As Integer
        Dim sngX, sngY As Single

        Select Case m_Direction
            Case Direction.Up
                sngX = (Position.X * 16)
                sngY = Position.Y * 16 - 8
            Case Direction.Down
                sngX = (Position.X * 16)
                sngY = Position.Y * 16 + 8

            Case Direction.Left
                sngY = (Position.Y * 16)
                sngX = Position.X * 16 - 8

            Case Direction.Right
                sngY = (Position.Y * 16)
                sngX = Position.X * 16 + 8

        End Select

        'For intTcnt = 2 To 2

        Image = ImgPos.dicImageList.Item("Boom_" & 2)
        Rect = New RectangleF(sngX, sngY, Rect.Width, Rect.Height)

        'Next


    End Sub
End Class
