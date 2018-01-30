<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.flagLBL = New System.Windows.Forms.Label()
        Me.FULL_GRID = New System.Windows.Forms.PictureBox()
        Me.Credits = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        CType(Me.FULL_GRID, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'flagLBL
        '
        Me.flagLBL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.flagLBL.Font = New System.Drawing.Font("NI7SEG", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.flagLBL.Location = New System.Drawing.Point(367, 31)
        Me.flagLBL.Name = "flagLBL"
        Me.flagLBL.Size = New System.Drawing.Size(39, 38)
        Me.flagLBL.TabIndex = 0
        Me.flagLBL.Text = "10"
        Me.flagLBL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'FULL_GRID
        '
        Me.FULL_GRID.Location = New System.Drawing.Point(62, 106)
        Me.FULL_GRID.Name = "FULL_GRID"
        Me.FULL_GRID.Size = New System.Drawing.Size(549, 549)
        Me.FULL_GRID.TabIndex = 2
        Me.FULL_GRID.TabStop = False
        Me.FULL_GRID.Visible = False
        '
        'Credits
        '
        Me.Credits.Font = New System.Drawing.Font("NI7SEG", 14.0!)
        Me.Credits.Location = New System.Drawing.Point(54, 704)
        Me.Credits.Name = "Credits"
        Me.Credits.Size = New System.Drawing.Size(120, 29)
        Me.Credits.TabIndex = 3
        Me.Credits.Text = "CREDITS"
        Me.Credits.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label1.Font = New System.Drawing.Font("NI7SEG", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(266, 31)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(95, 38)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "MINES :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.BackgroundImage = Global.FinalProject.My.Resources.Resources.FormBG
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(673, 769)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Credits)
        Me.Controls.Add(Me.FULL_GRID)
        Me.Controls.Add(Me.flagLBL)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.HelpButton = True
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(689, 808)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(689, 808)
        Me.Name = "Form1"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.Text = "Operation MINE"
        CType(Me.FULL_GRID, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents flagLBL As Label
    Friend WithEvents FULL_GRID As PictureBox
    Friend WithEvents Credits As Button
    Friend WithEvents Label1 As Label
End Class
