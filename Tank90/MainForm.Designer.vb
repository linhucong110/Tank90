<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainForm
    Inherits System.Windows.Forms.Form

    'Form 重写 Dispose，以清理组件列表。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows 窗体设计器所必需的
    Private components As System.ComponentModel.IContainer

    '注意: 以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
        Me.picMap = New System.Windows.Forms.PictureBox()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.picStatus = New System.Windows.Forms.PictureBox()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.游戏ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.开始游戏ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.自定地图ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Timer2 = New System.Windows.Forms.Timer(Me.components)
        Me.坦克类型ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.经典坦克ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.少女系列ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.虫族敌人ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.虫族系列ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.机器敌人ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.机器系列ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.选择关卡ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        CType(Me.picMap, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'picMap
        '
        Me.picMap.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.picMap.Location = New System.Drawing.Point(0, 30)
        Me.picMap.Name = "picMap"
        Me.picMap.Size = New System.Drawing.Size(416, 416)
        Me.picMap.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picMap.TabIndex = 0
        Me.picMap.TabStop = False
        '
        'Timer1
        '
        Me.Timer1.Interval = 20
        '
        'picStatus
        '
        Me.picStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.picStatus.Location = New System.Drawing.Point(422, 30)
        Me.picStatus.Name = "picStatus"
        Me.picStatus.Size = New System.Drawing.Size(150, 416)
        Me.picStatus.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picStatus.TabIndex = 1
        Me.picStatus.TabStop = False
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.游戏ToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(577, 27)
        Me.MenuStrip1.TabIndex = 2
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        '游戏ToolStripMenuItem
        '
        Me.游戏ToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.开始游戏ToolStripMenuItem, Me.自定地图ToolStripMenuItem, Me.坦克类型ToolStripMenuItem, Me.选择关卡ToolStripMenuItem})
        Me.游戏ToolStripMenuItem.Name = "游戏ToolStripMenuItem"
        Me.游戏ToolStripMenuItem.Size = New System.Drawing.Size(51, 23)
        Me.游戏ToolStripMenuItem.Text = "游戏"
        '
        '开始游戏ToolStripMenuItem
        '
        Me.开始游戏ToolStripMenuItem.Name = "开始游戏ToolStripMenuItem"
        Me.开始游戏ToolStripMenuItem.Size = New System.Drawing.Size(152, 24)
        Me.开始游戏ToolStripMenuItem.Text = "开始游戏"
        '
        '自定地图ToolStripMenuItem
        '
        Me.自定地图ToolStripMenuItem.Name = "自定地图ToolStripMenuItem"
        Me.自定地图ToolStripMenuItem.Size = New System.Drawing.Size(152, 24)
        Me.自定地图ToolStripMenuItem.Text = "自定地图"
        '
        'Timer2
        '
        Me.Timer2.Enabled = True
        Me.Timer2.Interval = 30
        '
        '坦克类型ToolStripMenuItem
        '
        Me.坦克类型ToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.经典坦克ToolStripMenuItem, Me.少女系列ToolStripMenuItem, Me.虫族敌人ToolStripMenuItem, Me.虫族系列ToolStripMenuItem, Me.机器敌人ToolStripMenuItem, Me.机器系列ToolStripMenuItem})
        Me.坦克类型ToolStripMenuItem.Name = "坦克类型ToolStripMenuItem"
        Me.坦克类型ToolStripMenuItem.Size = New System.Drawing.Size(152, 24)
        Me.坦克类型ToolStripMenuItem.Text = "坦克类型"
        '
        '经典坦克ToolStripMenuItem
        '
        Me.经典坦克ToolStripMenuItem.Checked = True
        Me.经典坦克ToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
        Me.经典坦克ToolStripMenuItem.Name = "经典坦克ToolStripMenuItem"
        Me.经典坦克ToolStripMenuItem.Size = New System.Drawing.Size(152, 24)
        Me.经典坦克ToolStripMenuItem.Text = "经典坦克"
        '
        '少女系列ToolStripMenuItem
        '
        Me.少女系列ToolStripMenuItem.Name = "少女系列ToolStripMenuItem"
        Me.少女系列ToolStripMenuItem.Size = New System.Drawing.Size(152, 24)
        Me.少女系列ToolStripMenuItem.Text = "少女系列"
        '
        '虫族敌人ToolStripMenuItem
        '
        Me.虫族敌人ToolStripMenuItem.Name = "虫族敌人ToolStripMenuItem"
        Me.虫族敌人ToolStripMenuItem.Size = New System.Drawing.Size(152, 24)
        Me.虫族敌人ToolStripMenuItem.Text = "虫族敌人"
        '
        '虫族系列ToolStripMenuItem
        '
        Me.虫族系列ToolStripMenuItem.Name = "虫族系列ToolStripMenuItem"
        Me.虫族系列ToolStripMenuItem.Size = New System.Drawing.Size(152, 24)
        Me.虫族系列ToolStripMenuItem.Text = "虫族系列"
        '
        '机器敌人ToolStripMenuItem
        '
        Me.机器敌人ToolStripMenuItem.Name = "机器敌人ToolStripMenuItem"
        Me.机器敌人ToolStripMenuItem.Size = New System.Drawing.Size(152, 24)
        Me.机器敌人ToolStripMenuItem.Text = "机器敌人"
        '
        '机器系列ToolStripMenuItem
        '
        Me.机器系列ToolStripMenuItem.Name = "机器系列ToolStripMenuItem"
        Me.机器系列ToolStripMenuItem.Size = New System.Drawing.Size(152, 24)
        Me.机器系列ToolStripMenuItem.Text = "机器系列"
        '
        '选择关卡ToolStripMenuItem
        '
        Me.选择关卡ToolStripMenuItem.Name = "选择关卡ToolStripMenuItem"
        Me.选择关卡ToolStripMenuItem.Size = New System.Drawing.Size(152, 24)
        Me.选择关卡ToolStripMenuItem.Text = "选择关卡"
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(577, 451)
        Me.Controls.Add(Me.picStatus)
        Me.Controls.Add(Me.picMap)
        Me.Controls.Add(Me.MenuStrip1)
        Me.DoubleBuffered = True
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "MainForm"
        Me.Text = "Main"
        CType(Me.picMap, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picStatus, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents picMap As System.Windows.Forms.PictureBox
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents picStatus As System.Windows.Forms.PictureBox
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents 游戏ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 开始游戏ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 自定地图ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Timer2 As System.Windows.Forms.Timer
    Friend WithEvents 坦克类型ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 经典坦克ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 少女系列ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 虫族敌人ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 虫族系列ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 机器敌人ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 机器系列ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 选择关卡ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem

End Class
