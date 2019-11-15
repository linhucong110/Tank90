<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CreateMap
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
        Me.picStatus = New System.Windows.Forms.PictureBox()
        Me.picMap = New System.Windows.Forms.PictureBox()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.地图ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.创建新地图ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.加载地图ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripSeparator()
        Me.保存地图ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ListView1 = New System.Windows.Forms.ListView()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.lstMap = New System.Windows.Forms.ListBox()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.查看地图ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripSeparator()
        Me.修改地图ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.删除地图ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        CType(Me.picStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picMap, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip1.SuspendLayout()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'picStatus
        '
        Me.picStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.picStatus.Location = New System.Drawing.Point(422, 30)
        Me.picStatus.Name = "picStatus"
        Me.picStatus.Size = New System.Drawing.Size(150, 416)
        Me.picStatus.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picStatus.TabIndex = 3
        Me.picStatus.TabStop = False
        '
        'picMap
        '
        Me.picMap.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.picMap.Location = New System.Drawing.Point(0, 30)
        Me.picMap.Name = "picMap"
        Me.picMap.Size = New System.Drawing.Size(416, 416)
        Me.picMap.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picMap.TabIndex = 2
        Me.picMap.TabStop = False
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.地图ToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(580, 27)
        Me.MenuStrip1.TabIndex = 4
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        '地图ToolStripMenuItem
        '
        Me.地图ToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.创建新地图ToolStripMenuItem, Me.加载地图ToolStripMenuItem, Me.ToolStripMenuItem1, Me.保存地图ToolStripMenuItem})
        Me.地图ToolStripMenuItem.Name = "地图ToolStripMenuItem"
        Me.地图ToolStripMenuItem.Size = New System.Drawing.Size(51, 23)
        Me.地图ToolStripMenuItem.Text = "地图"
        '
        '创建新地图ToolStripMenuItem
        '
        Me.创建新地图ToolStripMenuItem.Name = "创建新地图ToolStripMenuItem"
        Me.创建新地图ToolStripMenuItem.Size = New System.Drawing.Size(153, 24)
        Me.创建新地图ToolStripMenuItem.Text = "创建新地图"
        '
        '加载地图ToolStripMenuItem
        '
        Me.加载地图ToolStripMenuItem.Name = "加载地图ToolStripMenuItem"
        Me.加载地图ToolStripMenuItem.Size = New System.Drawing.Size(153, 24)
        Me.加载地图ToolStripMenuItem.Text = "加载地图"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(150, 6)
        '
        '保存地图ToolStripMenuItem
        '
        Me.保存地图ToolStripMenuItem.Name = "保存地图ToolStripMenuItem"
        Me.保存地图ToolStripMenuItem.Size = New System.Drawing.Size(153, 24)
        Me.保存地图ToolStripMenuItem.Text = "保存地图"
        '
        'ListView1
        '
        Me.ListView1.LargeImageList = Me.ImageList1
        Me.ListView1.Location = New System.Drawing.Point(422, 30)
        Me.ListView1.MultiSelect = False
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(150, 226)
        Me.ListView1.TabIndex = 5
        Me.ListView1.UseCompatibleStateImageBehavior = False
        Me.ListView1.View = System.Windows.Forms.View.Tile
        '
        'ImageList1
        '
        Me.ImageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit
        Me.ImageList1.ImageSize = New System.Drawing.Size(32, 32)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        '
        'lstMap
        '
        Me.lstMap.ContextMenuStrip = Me.ContextMenuStrip1
        Me.lstMap.FormattingEnabled = True
        Me.lstMap.ItemHeight = 15
        Me.lstMap.Location = New System.Drawing.Point(422, 262)
        Me.lstMap.Name = "lstMap"
        Me.lstMap.Size = New System.Drawing.Size(150, 184)
        Me.lstMap.TabIndex = 6
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.查看地图ToolStripMenuItem, Me.ToolStripMenuItem2, Me.修改地图ToolStripMenuItem, Me.删除地图ToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(153, 104)
        '
        '查看地图ToolStripMenuItem
        '
        Me.查看地图ToolStripMenuItem.Name = "查看地图ToolStripMenuItem"
        Me.查看地图ToolStripMenuItem.Size = New System.Drawing.Size(152, 24)
        Me.查看地图ToolStripMenuItem.Text = "查看地图"
        '
        'ToolStripMenuItem2
        '
        Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        Me.ToolStripMenuItem2.Size = New System.Drawing.Size(149, 6)
        '
        '修改地图ToolStripMenuItem
        '
        Me.修改地图ToolStripMenuItem.Name = "修改地图ToolStripMenuItem"
        Me.修改地图ToolStripMenuItem.Size = New System.Drawing.Size(152, 24)
        Me.修改地图ToolStripMenuItem.Text = "修改地图"
        '
        '删除地图ToolStripMenuItem
        '
        Me.删除地图ToolStripMenuItem.Name = "删除地图ToolStripMenuItem"
        Me.删除地图ToolStripMenuItem.Size = New System.Drawing.Size(152, 24)
        Me.删除地图ToolStripMenuItem.Text = "删除地图"
        '
        'CreateMap
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(580, 451)
        Me.Controls.Add(Me.lstMap)
        Me.Controls.Add(Me.ListView1)
        Me.Controls.Add(Me.picStatus)
        Me.Controls.Add(Me.picMap)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "CreateMap"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "创建地图"
        CType(Me.picStatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picMap, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents picStatus As System.Windows.Forms.PictureBox
    Friend WithEvents picMap As System.Windows.Forms.PictureBox
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents 地图ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 创建新地图ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 保存地图ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ListView1 As System.Windows.Forms.ListView
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents 加载地图ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents lstMap As System.Windows.Forms.ListBox
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents 查看地图ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents 修改地图ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 删除地图ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
End Class
