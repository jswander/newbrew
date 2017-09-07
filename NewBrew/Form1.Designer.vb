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
        Me.components = New System.ComponentModel.Container()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.Recipes = New System.Windows.Forms.TabPage()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtBatchSize = New System.Windows.Forms.TextBox()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.lblAsstBrewer = New System.Windows.Forms.Label()
        Me.txtAsstBrewer = New System.Windows.Forms.TextBox()
        Me.lblBrewer = New System.Windows.Forms.Label()
        Me.txtBrewer = New System.Windows.Forms.TextBox()
        Me.lblType = New System.Windows.Forms.Label()
        Me.txtType = New System.Windows.Forms.TextBox()
        Me.lblName = New System.Windows.Forms.Label()
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.lstRecipeNames = New System.Windows.Forms.ListBox()
        Me.dgv1 = New System.Windows.Forms.DataGridView()
        Me.Amount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Preferences = New System.Windows.Forms.TabPage()
        Me.btnTest = New System.Windows.Forms.Button()
        Me.btnDeleteTables = New System.Windows.Forms.Button()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.btnFileOpen = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.RadioButton2 = New System.Windows.Forms.RadioButton()
        Me.RadioButton1 = New System.Windows.Forms.RadioButton()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnListRecipes = New System.Windows.Forms.Button()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.text1 = New System.Windows.Forms.TextBox()
        Me.btnNewDatabaseTables = New System.Windows.Forms.Button()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.txtLastTouched = New System.Windows.Forms.TextBox()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.TabControl1.SuspendLayout()
        Me.Recipes.SuspendLayout()
        CType(Me.dgv1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Preferences.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.Recipes)
        Me.TabControl1.Controls.Add(Me.Preferences)
        Me.TabControl1.Location = New System.Drawing.Point(12, 12)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(949, 502)
        Me.TabControl1.TabIndex = 0
        '
        'Recipes
        '
        Me.Recipes.Controls.Add(Me.Label3)
        Me.Recipes.Controls.Add(Me.Label2)
        Me.Recipes.Controls.Add(Me.txtBatchSize)
        Me.Recipes.Controls.Add(Me.txtSearch)
        Me.Recipes.Controls.Add(Me.lblAsstBrewer)
        Me.Recipes.Controls.Add(Me.txtAsstBrewer)
        Me.Recipes.Controls.Add(Me.lblBrewer)
        Me.Recipes.Controls.Add(Me.txtBrewer)
        Me.Recipes.Controls.Add(Me.lblType)
        Me.Recipes.Controls.Add(Me.txtType)
        Me.Recipes.Controls.Add(Me.lblName)
        Me.Recipes.Controls.Add(Me.txtName)
        Me.Recipes.Controls.Add(Me.lstRecipeNames)
        Me.Recipes.Controls.Add(Me.dgv1)
        Me.Recipes.Location = New System.Drawing.Point(4, 22)
        Me.Recipes.Name = "Recipes"
        Me.Recipes.Padding = New System.Windows.Forms.Padding(3)
        Me.Recipes.Size = New System.Drawing.Size(941, 476)
        Me.Recipes.TabIndex = 1
        Me.Recipes.Text = "Recipes"
        Me.Recipes.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(698, 38)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label3.Size = New System.Drawing.Size(21, 13)
        Me.Label3.TabIndex = 13
        Me.Label3.Text = "gal"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(520, 38)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label2.Size = New System.Drawing.Size(58, 13)
        Me.Label2.TabIndex = 12
        Me.Label2.Text = "Batch Size"
        '
        'txtBatchSize
        '
        Me.txtBatchSize.Location = New System.Drawing.Point(583, 31)
        Me.txtBatchSize.Name = "txtBatchSize"
        Me.txtBatchSize.Size = New System.Drawing.Size(109, 20)
        Me.txtBatchSize.TabIndex = 11
        '
        'txtSearch
        '
        Me.txtSearch.Location = New System.Drawing.Point(6, 17)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(185, 20)
        Me.txtSearch.TabIndex = 10
        '
        'lblAsstBrewer
        '
        Me.lblAsstBrewer.AutoSize = True
        Me.lblAsstBrewer.Location = New System.Drawing.Point(202, 90)
        Me.lblAsstBrewer.Name = "lblAsstBrewer"
        Me.lblAsstBrewer.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.lblAsstBrewer.Size = New System.Drawing.Size(63, 13)
        Me.lblAsstBrewer.TabIndex = 9
        Me.lblAsstBrewer.Text = "Asst Brewer"
        '
        'txtAsstBrewer
        '
        Me.txtAsstBrewer.Location = New System.Drawing.Point(271, 83)
        Me.txtAsstBrewer.Name = "txtAsstBrewer"
        Me.txtAsstBrewer.Size = New System.Drawing.Size(229, 20)
        Me.txtAsstBrewer.TabIndex = 8
        '
        'lblBrewer
        '
        Me.lblBrewer.AutoSize = True
        Me.lblBrewer.Location = New System.Drawing.Point(230, 64)
        Me.lblBrewer.Name = "lblBrewer"
        Me.lblBrewer.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.lblBrewer.Size = New System.Drawing.Size(40, 13)
        Me.lblBrewer.TabIndex = 7
        Me.lblBrewer.Text = "Brewer"
        '
        'txtBrewer
        '
        Me.txtBrewer.Location = New System.Drawing.Point(271, 57)
        Me.txtBrewer.Name = "txtBrewer"
        Me.txtBrewer.Size = New System.Drawing.Size(229, 20)
        Me.txtBrewer.TabIndex = 6
        '
        'lblType
        '
        Me.lblType.AutoSize = True
        Me.lblType.Location = New System.Drawing.Point(546, 64)
        Me.lblType.Name = "lblType"
        Me.lblType.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.lblType.Size = New System.Drawing.Size(31, 13)
        Me.lblType.TabIndex = 5
        Me.lblType.Text = "Type"
        '
        'txtType
        '
        Me.txtType.Location = New System.Drawing.Point(583, 57)
        Me.txtType.Name = "txtType"
        Me.txtType.Size = New System.Drawing.Size(109, 20)
        Me.txtType.TabIndex = 4
        '
        'lblName
        '
        Me.lblName.AutoSize = True
        Me.lblName.Location = New System.Drawing.Point(230, 38)
        Me.lblName.Name = "lblName"
        Me.lblName.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.lblName.Size = New System.Drawing.Size(35, 13)
        Me.lblName.TabIndex = 3
        Me.lblName.Text = "Name"
        '
        'txtName
        '
        Me.txtName.Location = New System.Drawing.Point(271, 31)
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(229, 20)
        Me.txtName.TabIndex = 2
        '
        'lstRecipeNames
        '
        Me.lstRecipeNames.FormattingEnabled = True
        Me.lstRecipeNames.Location = New System.Drawing.Point(6, 38)
        Me.lstRecipeNames.Name = "lstRecipeNames"
        Me.lstRecipeNames.Size = New System.Drawing.Size(185, 82)
        Me.lstRecipeNames.TabIndex = 1
        '
        'dgv1
        '
        Me.dgv1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Amount, Me.Column2, Me.Column3, Me.Column4, Me.Column1, Me.Column5})
        Me.dgv1.Location = New System.Drawing.Point(6, 159)
        Me.dgv1.Name = "dgv1"
        Me.dgv1.RowHeadersVisible = False
        Me.dgv1.Size = New System.Drawing.Size(906, 301)
        Me.dgv1.TabIndex = 0
        '
        'Amount
        '
        Me.Amount.HeaderText = "Amount"
        Me.Amount.Name = "Amount"
        Me.Amount.Width = 70
        '
        'Column2
        '
        Me.Column2.HeaderText = "Item"
        Me.Column2.Name = "Column2"
        Me.Column2.Width = 250
        '
        'Column3
        '
        Me.Column3.HeaderText = "Type"
        Me.Column3.Name = "Column3"
        Me.Column3.Width = 50
        '
        'Column4
        '
        Me.Column4.HeaderText = "% / IBU"
        Me.Column4.Name = "Column4"
        Me.Column4.Width = 50
        '
        'Column1
        '
        Me.Column1.HeaderText = "Cost"
        Me.Column1.Name = "Column1"
        Me.Column1.Width = 50
        '
        'Column5
        '
        Me.Column5.HeaderText = "Inventory"
        Me.Column5.Name = "Column5"
        Me.Column5.Width = 50
        '
        'Preferences
        '
        Me.Preferences.Controls.Add(Me.btnTest)
        Me.Preferences.Controls.Add(Me.btnDeleteTables)
        Me.Preferences.Controls.Add(Me.TextBox1)
        Me.Preferences.Controls.Add(Me.btnFileOpen)
        Me.Preferences.Controls.Add(Me.GroupBox1)
        Me.Preferences.Controls.Add(Me.Label1)
        Me.Preferences.Controls.Add(Me.btnListRecipes)
        Me.Preferences.Controls.Add(Me.ProgressBar1)
        Me.Preferences.Controls.Add(Me.text1)
        Me.Preferences.Controls.Add(Me.btnNewDatabaseTables)
        Me.Preferences.Location = New System.Drawing.Point(4, 22)
        Me.Preferences.Name = "Preferences"
        Me.Preferences.Padding = New System.Windows.Forms.Padding(3)
        Me.Preferences.Size = New System.Drawing.Size(941, 476)
        Me.Preferences.TabIndex = 0
        Me.Preferences.Text = "Setup"
        Me.Preferences.UseVisualStyleBackColor = True
        '
        'btnTest
        '
        Me.btnTest.Location = New System.Drawing.Point(355, 315)
        Me.btnTest.Name = "btnTest"
        Me.btnTest.Size = New System.Drawing.Size(110, 23)
        Me.btnTest.TabIndex = 13
        Me.btnTest.Text = "Test Error"
        Me.btnTest.UseVisualStyleBackColor = True
        '
        'btnDeleteTables
        '
        Me.btnDeleteTables.Location = New System.Drawing.Point(119, 315)
        Me.btnDeleteTables.Name = "btnDeleteTables"
        Me.btnDeleteTables.Size = New System.Drawing.Size(110, 23)
        Me.btnDeleteTables.TabIndex = 12
        Me.btnDeleteTables.Text = "Delete Tables"
        Me.btnDeleteTables.UseVisualStyleBackColor = True
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(198, 403)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(107, 20)
        Me.TextBox1.TabIndex = 11
        '
        'btnFileOpen
        '
        Me.btnFileOpen.Location = New System.Drawing.Point(102, 400)
        Me.btnFileOpen.Name = "btnFileOpen"
        Me.btnFileOpen.Size = New System.Drawing.Size(75, 23)
        Me.btnFileOpen.TabIndex = 10
        Me.btnFileOpen.Text = "File Open"
        Me.btnFileOpen.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.RadioButton2)
        Me.GroupBox1.Controls.Add(Me.RadioButton1)
        Me.GroupBox1.Location = New System.Drawing.Point(146, 6)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(169, 44)
        Me.GroupBox1.TabIndex = 8
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Display Units"
        '
        'RadioButton2
        '
        Me.RadioButton2.AutoSize = True
        Me.RadioButton2.Location = New System.Drawing.Point(89, 19)
        Me.RadioButton2.Name = "RadioButton2"
        Me.RadioButton2.Size = New System.Drawing.Size(61, 17)
        Me.RadioButton2.TabIndex = 6
        Me.RadioButton2.TabStop = True
        Me.RadioButton2.Text = "Imperial"
        Me.RadioButton2.UseVisualStyleBackColor = True
        '
        'RadioButton1
        '
        Me.RadioButton1.AutoSize = True
        Me.RadioButton1.Location = New System.Drawing.Point(29, 19)
        Me.RadioButton1.Name = "RadioButton1"
        Me.RadioButton1.Size = New System.Drawing.Size(54, 17)
        Me.RadioButton1.TabIndex = 5
        Me.RadioButton1.TabStop = True
        Me.RadioButton1.Text = "Metric"
        Me.RadioButton1.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(846, 227)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(39, 13)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Label1"
        '
        'btnListRecipes
        '
        Me.btnListRecipes.Location = New System.Drawing.Point(814, 140)
        Me.btnListRecipes.Name = "btnListRecipes"
        Me.btnListRecipes.Size = New System.Drawing.Size(75, 23)
        Me.btnListRecipes.TabIndex = 3
        Me.btnListRecipes.Text = "List Recipes"
        Me.btnListRecipes.UseVisualStyleBackColor = True
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(198, 353)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(600, 23)
        Me.ProgressBar1.TabIndex = 2
        '
        'text1
        '
        Me.text1.Location = New System.Drawing.Point(102, 56)
        Me.text1.Multiline = True
        Me.text1.Name = "text1"
        Me.text1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.text1.Size = New System.Drawing.Size(623, 239)
        Me.text1.TabIndex = 1
        '
        'btnNewDatabaseTables
        '
        Me.btnNewDatabaseTables.Location = New System.Drawing.Point(588, 315)
        Me.btnNewDatabaseTables.Name = "btnNewDatabaseTables"
        Me.btnNewDatabaseTables.Size = New System.Drawing.Size(110, 23)
        Me.btnNewDatabaseTables.TabIndex = 0
        Me.btnNewDatabaseTables.Text = "New Database"
        Me.btnNewDatabaseTables.UseVisualStyleBackColor = True
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'txtLastTouched
        '
        Me.txtLastTouched.Location = New System.Drawing.Point(167, 554)
        Me.txtLastTouched.Name = "txtLastTouched"
        Me.txtLastTouched.Size = New System.Drawing.Size(180, 20)
        Me.txtLastTouched.TabIndex = 24
        '
        'ComboBox1
        '
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Location = New System.Drawing.Point(511, 552)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(121, 21)
        Me.ComboBox1.TabIndex = 25
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1090, 735)
        Me.Controls.Add(Me.ComboBox1)
        Me.Controls.Add(Me.txtLastTouched)
        Me.Controls.Add(Me.TabControl1)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.TabControl1.ResumeLayout(False)
        Me.Recipes.ResumeLayout(False)
        Me.Recipes.PerformLayout()
        CType(Me.dgv1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Preferences.ResumeLayout(False)
        Me.Preferences.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents Preferences As TabPage
    Friend WithEvents Recipes As TabPage
    Friend WithEvents btnNewDatabaseTables As Button
    Friend WithEvents text1 As TextBox
    Friend WithEvents ProgressBar1 As ProgressBar
    Friend WithEvents dgv1 As DataGridView
    Friend WithEvents lstRecipeNames As ListBox
    Friend WithEvents btnListRecipes As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents lblName As Label
    Friend WithEvents txtName As TextBox
    Friend WithEvents lblAsstBrewer As Label
    Friend WithEvents txtAsstBrewer As TextBox
    Friend WithEvents lblBrewer As Label
    Friend WithEvents txtBrewer As TextBox
    Friend WithEvents lblType As Label
    Friend WithEvents txtType As TextBox
    Friend WithEvents txtSearch As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents txtBatchSize As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents RadioButton2 As RadioButton
    Friend WithEvents RadioButton1 As RadioButton
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents btnFileOpen As Button
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents btnDeleteTables As Button
    Friend WithEvents btnTest As Button
    Friend WithEvents Amount As DataGridViewTextBoxColumn
    Friend WithEvents Column2 As DataGridViewTextBoxColumn
    Friend WithEvents Column3 As DataGridViewTextBoxColumn
    Friend WithEvents Column4 As DataGridViewTextBoxColumn
    Friend WithEvents Column1 As DataGridViewTextBoxColumn
    Friend WithEvents Column5 As DataGridViewTextBoxColumn
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents txtLastTouched As TextBox
    Friend WithEvents ComboBox1 As ComboBox
End Class
