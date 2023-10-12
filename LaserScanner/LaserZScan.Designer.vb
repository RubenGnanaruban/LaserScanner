<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class LaserZScan
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(LaserZScan))
        Me.VideoSourcePlayer1 = New AForge.Controls.VideoSourcePlayer()
        Me.TextBoxX = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TextBoxY = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TextBoxZ = New System.Windows.Forms.TextBox()
        Me.NumericUpDown_n_image = New System.Windows.Forms.NumericUpDown()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.NumericUpDown_Z_range = New System.Windows.Forms.NumericUpDown()
        Me.ButtonCalibrate = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.ButtonLoadCalib = New System.Windows.Forms.Button()
        Me.TextBox4 = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.ButtonSaveCalib = New System.Windows.Forms.Button()
        Me.ButtonPlot = New System.Windows.Forms.Button()
        Me.NumericUpDown_X_range = New System.Windows.Forms.NumericUpDown()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.NumericUpDownNLines = New System.Windows.Forms.NumericUpDown()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.NumericUpDownFocus = New System.Windows.Forms.NumericUpDown()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Chart1 = New AForge.Controls.Chart()
        Me.NumericUpDownExposure = New System.Windows.Forms.NumericUpDown()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.TrackBarX = New System.Windows.Forms.TrackBar()
        Me.TrackBarTopCrop = New System.Windows.Forms.TrackBar()
        Me.TrackBarBottomCrop = New System.Windows.Forms.TrackBar()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.DisplayValue = New System.Windows.Forms.Label()
        Me.ButtonScan = New System.Windows.Forms.Button()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Scan = New System.Windows.Forms.GroupBox()
        CType(Me.NumericUpDown_n_image, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDown_Z_range, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.NumericUpDown_X_range, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDownNLines, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDownFocus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDownExposure, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TrackBarX, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TrackBarTopCrop, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TrackBarBottomCrop, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Scan.SuspendLayout()
        Me.SuspendLayout()
        '
        'VideoSourcePlayer1
        '
        Me.VideoSourcePlayer1.Location = New System.Drawing.Point(703, 154)
        Me.VideoSourcePlayer1.Margin = New System.Windows.Forms.Padding(6, 3, 6, 3)
        Me.VideoSourcePlayer1.Name = "VideoSourcePlayer1"
        Me.VideoSourcePlayer1.Size = New System.Drawing.Size(1646, 1120)
        Me.VideoSourcePlayer1.TabIndex = 0
        Me.VideoSourcePlayer1.Text = "VideoSourcePlayer1"
        Me.VideoSourcePlayer1.VideoSource = Nothing
        '
        'TextBoxX
        '
        Me.TextBoxX.Location = New System.Drawing.Point(243, 70)
        Me.TextBoxX.Margin = New System.Windows.Forms.Padding(9, 10, 9, 10)
        Me.TextBoxX.Name = "TextBoxX"
        Me.TextBoxX.Size = New System.Drawing.Size(167, 55)
        Me.TextBoxX.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(186, 80)
        Me.Label1.Margin = New System.Windows.Forms.Padding(9, 0, 9, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(41, 48)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "X"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(186, 173)
        Me.Label2.Margin = New System.Windows.Forms.Padding(9, 0, 9, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(40, 48)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Y"
        '
        'TextBoxY
        '
        Me.TextBoxY.Location = New System.Drawing.Point(243, 163)
        Me.TextBoxY.Margin = New System.Windows.Forms.Padding(9, 10, 9, 10)
        Me.TextBoxY.Name = "TextBoxY"
        Me.TextBoxY.Size = New System.Drawing.Size(167, 55)
        Me.TextBoxY.TabIndex = 4
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(186, 266)
        Me.Label3.Margin = New System.Windows.Forms.Padding(9, 0, 9, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(41, 48)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "Z"
        '
        'TextBoxZ
        '
        Me.TextBoxZ.Location = New System.Drawing.Point(243, 256)
        Me.TextBoxZ.Margin = New System.Windows.Forms.Padding(9, 10, 9, 10)
        Me.TextBoxZ.Name = "TextBoxZ"
        Me.TextBoxZ.Size = New System.Drawing.Size(167, 55)
        Me.TextBoxZ.TabIndex = 6
        '
        'NumericUpDown_n_image
        '
        Me.NumericUpDown_n_image.Location = New System.Drawing.Point(443, 166)
        Me.NumericUpDown_n_image.Margin = New System.Windows.Forms.Padding(9, 10, 9, 10)
        Me.NumericUpDown_n_image.Maximum = New Decimal(New Integer() {200, 0, 0, 0})
        Me.NumericUpDown_n_image.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NumericUpDown_n_image.Name = "NumericUpDown_n_image"
        Me.NumericUpDown_n_image.Size = New System.Drawing.Size(134, 55)
        Me.NumericUpDown_n_image.TabIndex = 9
        Me.NumericUpDown_n_image.Value = New Decimal(New Integer() {100, 0, 0, 0})
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(34, 173)
        Me.Label4.Margin = New System.Windows.Forms.Padding(9, 0, 9, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(411, 48)
        Me.Label4.TabIndex = 10
        Me.Label4.Text = "Images per line (Z stops)"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(34, 275)
        Me.Label5.Margin = New System.Windows.Forms.Padding(9, 0, 9, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(226, 48)
        Me.Label5.TabIndex = 12
        Me.Label5.Text = "Z range (µm)"
        '
        'NumericUpDown_Z_range
        '
        Me.NumericUpDown_Z_range.Increment = New Decimal(New Integer() {10, 0, 0, 0})
        Me.NumericUpDown_Z_range.Location = New System.Drawing.Point(443, 269)
        Me.NumericUpDown_Z_range.Margin = New System.Windows.Forms.Padding(9, 10, 9, 10)
        Me.NumericUpDown_Z_range.Maximum = New Decimal(New Integer() {5000, 0, 0, 0})
        Me.NumericUpDown_Z_range.Minimum = New Decimal(New Integer() {10, 0, 0, 0})
        Me.NumericUpDown_Z_range.Name = "NumericUpDown_Z_range"
        Me.NumericUpDown_Z_range.Size = New System.Drawing.Size(134, 55)
        Me.NumericUpDown_Z_range.TabIndex = 11
        Me.NumericUpDown_Z_range.Value = New Decimal(New Integer() {500, 0, 0, 0})
        '
        'ButtonCalibrate
        '
        Me.ButtonCalibrate.Location = New System.Drawing.Point(34, 371)
        Me.ButtonCalibrate.Margin = New System.Windows.Forms.Padding(9, 10, 9, 10)
        Me.ButtonCalibrate.Name = "ButtonCalibrate"
        Me.ButtonCalibrate.Size = New System.Drawing.Size(214, 74)
        Me.ButtonCalibrate.TabIndex = 13
        Me.ButtonCalibrate.Text = "Calibrate"
        Me.ButtonCalibrate.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.TextBoxX)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.TextBoxY)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.TextBoxZ)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Location = New System.Drawing.Point(51, 38)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(9, 10, 9, 10)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(9, 10, 9, 10)
        Me.GroupBox1.Size = New System.Drawing.Size(620, 368)
        Me.GroupBox1.TabIndex = 14
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Relative Bed Movement (mm)"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.ButtonLoadCalib)
        Me.GroupBox2.Controls.Add(Me.TextBox4)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.ButtonCalibrate)
        Me.GroupBox2.Controls.Add(Me.NumericUpDown_n_image)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.NumericUpDown_Z_range)
        Me.GroupBox2.Controls.Add(Me.ButtonSaveCalib)
        Me.GroupBox2.Controls.Add(Me.ButtonPlot)
        Me.GroupBox2.Location = New System.Drawing.Point(54, 778)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(9, 10, 9, 10)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(9, 10, 9, 10)
        Me.GroupBox2.Size = New System.Drawing.Size(617, 586)
        Me.GroupBox2.TabIndex = 15
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Calibration"
        '
        'ButtonLoadCalib
        '
        Me.ButtonLoadCalib.Location = New System.Drawing.Point(363, 470)
        Me.ButtonLoadCalib.Margin = New System.Windows.Forms.Padding(6, 3, 6, 3)
        Me.ButtonLoadCalib.Name = "ButtonLoadCalib"
        Me.ButtonLoadCalib.Size = New System.Drawing.Size(214, 70)
        Me.ButtonLoadCalib.TabIndex = 21
        Me.ButtonLoadCalib.Text = "Load Calibration"
        Me.ButtonLoadCalib.UseVisualStyleBackColor = True
        '
        'TextBox4
        '
        Me.TextBox4.Location = New System.Drawing.Point(309, 64)
        Me.TextBox4.Margin = New System.Windows.Forms.Padding(9, 10, 9, 10)
        Me.TextBox4.Name = "TextBox4"
        Me.TextBox4.Size = New System.Drawing.Size(261, 55)
        Me.TextBox4.TabIndex = 16
        Me.TextBox4.Text = "image"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(34, 74)
        Me.Label6.Margin = New System.Windows.Forms.Padding(9, 0, 9, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(273, 48)
        Me.Label6.TabIndex = 14
        Me.Label6.Text = "File series name"
        '
        'ButtonSaveCalib
        '
        Me.ButtonSaveCalib.Location = New System.Drawing.Point(34, 470)
        Me.ButtonSaveCalib.Margin = New System.Windows.Forms.Padding(6, 3, 6, 3)
        Me.ButtonSaveCalib.Name = "ButtonSaveCalib"
        Me.ButtonSaveCalib.Size = New System.Drawing.Size(214, 70)
        Me.ButtonSaveCalib.TabIndex = 17
        Me.ButtonSaveCalib.Text = "Save Calibration"
        Me.ButtonSaveCalib.UseVisualStyleBackColor = True
        '
        'ButtonPlot
        '
        Me.ButtonPlot.Location = New System.Drawing.Point(363, 371)
        Me.ButtonPlot.Margin = New System.Windows.Forms.Padding(9, 10, 9, 10)
        Me.ButtonPlot.Name = "ButtonPlot"
        Me.ButtonPlot.Size = New System.Drawing.Size(214, 74)
        Me.ButtonPlot.TabIndex = 20
        Me.ButtonPlot.Text = "Plot"
        Me.ButtonPlot.UseVisualStyleBackColor = True
        '
        'NumericUpDown_X_range
        '
        Me.NumericUpDown_X_range.Location = New System.Drawing.Point(443, 282)
        Me.NumericUpDown_X_range.Margin = New System.Windows.Forms.Padding(9, 10, 9, 10)
        Me.NumericUpDown_X_range.Maximum = New Decimal(New Integer() {25, 0, 0, 0})
        Me.NumericUpDown_X_range.Name = "NumericUpDown_X_range"
        Me.NumericUpDown_X_range.Size = New System.Drawing.Size(134, 55)
        Me.NumericUpDown_X_range.TabIndex = 28
        Me.NumericUpDown_X_range.Value = New Decimal(New Integer() {20, 0, 0, 0})
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(34, 288)
        Me.Label10.Margin = New System.Windows.Forms.Padding(9, 0, 9, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(236, 48)
        Me.Label10.TabIndex = 19
        Me.Label10.Text = "X range (mm)"
        '
        'NumericUpDownNLines
        '
        Me.NumericUpDownNLines.Location = New System.Drawing.Point(443, 176)
        Me.NumericUpDownNLines.Margin = New System.Windows.Forms.Padding(9, 10, 9, 10)
        Me.NumericUpDownNLines.Maximum = New Decimal(New Integer() {500, 0, 0, 0})
        Me.NumericUpDownNLines.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NumericUpDownNLines.Name = "NumericUpDownNLines"
        Me.NumericUpDownNLines.Size = New System.Drawing.Size(134, 55)
        Me.NumericUpDownNLines.TabIndex = 18
        Me.NumericUpDownNLines.Value = New Decimal(New Integer() {100, 0, 0, 0})
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(34, 182)
        Me.Label9.Margin = New System.Windows.Forms.Padding(9, 0, 9, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(312, 48)
        Me.Label9.TabIndex = 17
        Me.Label9.Text = "# of lines (X stops)"
        '
        'NumericUpDownFocus
        '
        Me.NumericUpDownFocus.Location = New System.Drawing.Point(311, 67)
        Me.NumericUpDownFocus.Margin = New System.Windows.Forms.Padding(9, 10, 9, 10)
        Me.NumericUpDownFocus.Maximum = New Decimal(New Integer() {40, 0, 0, 0})
        Me.NumericUpDownFocus.Name = "NumericUpDownFocus"
        Me.NumericUpDownFocus.Size = New System.Drawing.Size(134, 55)
        Me.NumericUpDownFocus.TabIndex = 17
        Me.NumericUpDownFocus.Value = New Decimal(New Integer() {40, 0, 0, 0})
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(97, 74)
        Me.Label7.Margin = New System.Windows.Forms.Padding(9, 0, 9, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(111, 48)
        Me.Label7.TabIndex = 18
        Me.Label7.Text = "Focus"
        '
        'Chart1
        '
        Me.Chart1.Location = New System.Drawing.Point(2505, 154)
        Me.Chart1.Margin = New System.Windows.Forms.Padding(9, 10, 9, 10)
        Me.Chart1.Name = "Chart1"
        Me.Chart1.RangeX = CType(resources.GetObject("Chart1.RangeX"), AForge.Range)
        Me.Chart1.RangeY = CType(resources.GetObject("Chart1.RangeY"), AForge.Range)
        Me.Chart1.Size = New System.Drawing.Size(994, 1120)
        Me.Chart1.TabIndex = 19
        Me.Chart1.Text = "Chart1"
        '
        'NumericUpDownExposure
        '
        Me.NumericUpDownExposure.Location = New System.Drawing.Point(311, 173)
        Me.NumericUpDownExposure.Margin = New System.Windows.Forms.Padding(9, 10, 9, 10)
        Me.NumericUpDownExposure.Maximum = New Decimal(New Integer() {0, 0, 0, 0})
        Me.NumericUpDownExposure.Minimum = New Decimal(New Integer() {13, 0, 0, -2147483648})
        Me.NumericUpDownExposure.Name = "NumericUpDownExposure"
        Me.NumericUpDownExposure.Size = New System.Drawing.Size(134, 55)
        Me.NumericUpDownExposure.TabIndex = 21
        Me.NumericUpDownExposure.Value = New Decimal(New Integer() {11, 0, 0, -2147483648})
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(97, 179)
        Me.Label8.Margin = New System.Windows.Forms.Padding(9, 0, 9, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(164, 48)
        Me.Label8.TabIndex = 22
        Me.Label8.Text = "Exposure"
        '
        'TrackBarX
        '
        Me.TrackBarX.Location = New System.Drawing.Point(666, 38)
        Me.TrackBarX.Margin = New System.Windows.Forms.Padding(9, 10, 9, 10)
        Me.TrackBarX.Maximum = 1920
        Me.TrackBarX.Minimum = 1
        Me.TrackBarX.Name = "TrackBarX"
        Me.TrackBarX.Size = New System.Drawing.Size(1720, 136)
        Me.TrackBarX.TabIndex = 23
        Me.TrackBarX.Value = 960
        '
        'TrackBarTopCrop
        '
        Me.TrackBarTopCrop.Location = New System.Drawing.Point(2369, 112)
        Me.TrackBarTopCrop.Margin = New System.Windows.Forms.Padding(9, 10, 9, 10)
        Me.TrackBarTopCrop.Maximum = 510
        Me.TrackBarTopCrop.Minimum = 1
        Me.TrackBarTopCrop.Name = "TrackBarTopCrop"
        Me.TrackBarTopCrop.Orientation = System.Windows.Forms.Orientation.Vertical
        Me.TrackBarTopCrop.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TrackBarTopCrop.Size = New System.Drawing.Size(136, 608)
        Me.TrackBarTopCrop.SmallChange = 5
        Me.TrackBarTopCrop.TabIndex = 24
        Me.TrackBarTopCrop.TickStyle = System.Windows.Forms.TickStyle.TopLeft
        Me.TrackBarTopCrop.Value = 160
        '
        'TrackBarBottomCrop
        '
        Me.TrackBarBottomCrop.Location = New System.Drawing.Point(2369, 707)
        Me.TrackBarBottomCrop.Margin = New System.Windows.Forms.Padding(9, 10, 9, 10)
        Me.TrackBarBottomCrop.Maximum = 510
        Me.TrackBarBottomCrop.Minimum = 1
        Me.TrackBarBottomCrop.Name = "TrackBarBottomCrop"
        Me.TrackBarBottomCrop.Orientation = System.Windows.Forms.Orientation.Vertical
        Me.TrackBarBottomCrop.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TrackBarBottomCrop.Size = New System.Drawing.Size(136, 608)
        Me.TrackBarBottomCrop.SmallChange = 5
        Me.TrackBarBottomCrop.TabIndex = 25
        Me.TrackBarBottomCrop.TickStyle = System.Windows.Forms.TickStyle.TopLeft
        Me.TrackBarBottomCrop.Value = 350
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.NumericUpDownFocus)
        Me.GroupBox3.Controls.Add(Me.Label7)
        Me.GroupBox3.Controls.Add(Me.NumericUpDownExposure)
        Me.GroupBox3.Controls.Add(Me.Label8)
        Me.GroupBox3.Location = New System.Drawing.Point(51, 442)
        Me.GroupBox3.Margin = New System.Windows.Forms.Padding(9, 10, 9, 10)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Padding = New System.Windows.Forms.Padding(9, 10, 9, 10)
        Me.GroupBox3.Size = New System.Drawing.Size(620, 294)
        Me.GroupBox3.TabIndex = 27
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Camera Properties"
        '
        'DisplayValue
        '
        Me.DisplayValue.AutoSize = True
        Me.DisplayValue.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.DisplayValue.Location = New System.Drawing.Point(2505, 54)
        Me.DisplayValue.Margin = New System.Windows.Forms.Padding(9, 0, 9, 0)
        Me.DisplayValue.Name = "DisplayValue"
        Me.DisplayValue.Size = New System.Drawing.Size(111, 48)
        Me.DisplayValue.TabIndex = 28
        Me.DisplayValue.Text = "Value"
        '
        'ButtonScan
        '
        Me.ButtonScan.Location = New System.Drawing.Point(157, 387)
        Me.ButtonScan.Margin = New System.Windows.Forms.Padding(6, 3, 6, 3)
        Me.ButtonScan.Name = "ButtonScan"
        Me.ButtonScan.Size = New System.Drawing.Size(317, 70)
        Me.ButtonScan.TabIndex = 30
        Me.ButtonScan.Text = "Scan"
        Me.ButtonScan.UseVisualStyleBackColor = True
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'ComboBox1
        '
        Me.ComboBox1.DisplayMember = "0"
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Items.AddRange(New Object() {"Linear", "Akima Spline"})
        Me.ComboBox1.Location = New System.Drawing.Point(309, 70)
        Me.ComboBox1.Margin = New System.Windows.Forms.Padding(9, 10, 9, 10)
        Me.ComboBox1.MaxDropDownItems = 2
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(261, 56)
        Me.ComboBox1.TabIndex = 31
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(34, 80)
        Me.Label11.Margin = New System.Windows.Forms.Padding(9, 0, 9, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(225, 48)
        Me.Label11.TabIndex = 32
        Me.Label11.Text = "Interpolation"
        '
        'PictureBox1
        '
        Me.PictureBox1.Location = New System.Drawing.Point(703, 1305)
        Me.PictureBox1.Margin = New System.Windows.Forms.Padding(9, 10, 9, 10)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(2796, 605)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox1.TabIndex = 33
        Me.PictureBox1.TabStop = False
        '
        'Scan
        '
        Me.Scan.Controls.Add(Me.Label11)
        Me.Scan.Controls.Add(Me.Label9)
        Me.Scan.Controls.Add(Me.ButtonScan)
        Me.Scan.Controls.Add(Me.NumericUpDownNLines)
        Me.Scan.Controls.Add(Me.ComboBox1)
        Me.Scan.Controls.Add(Me.Label10)
        Me.Scan.Controls.Add(Me.NumericUpDown_X_range)
        Me.Scan.Location = New System.Drawing.Point(54, 1408)
        Me.Scan.Margin = New System.Windows.Forms.Padding(9, 10, 9, 10)
        Me.Scan.Name = "Scan"
        Me.Scan.Padding = New System.Windows.Forms.Padding(9, 10, 9, 10)
        Me.Scan.Size = New System.Drawing.Size(617, 502)
        Me.Scan.TabIndex = 34
        Me.Scan.TabStop = False
        Me.Scan.Text = "Scan"
        '
        'LaserZScan
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(20.0!, 48.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(3542, 1949)
        Me.Controls.Add(Me.TrackBarTopCrop)
        Me.Controls.Add(Me.Scan)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.DisplayValue)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.Chart1)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.VideoSourcePlayer1)
        Me.Controls.Add(Me.TrackBarBottomCrop)
        Me.Controls.Add(Me.TrackBarX)
        Me.Margin = New System.Windows.Forms.Padding(6, 3, 6, 3)
        Me.Name = "LaserZScan"
        Me.Text = "X"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.NumericUpDown_n_image, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDown_Z_range, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.NumericUpDown_X_range, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDownNLines, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDownFocus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDownExposure, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TrackBarX, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TrackBarTopCrop, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TrackBarBottomCrop, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Scan.ResumeLayout(False)
        Me.Scan.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents VideoSourcePlayer1 As AForge.Controls.VideoSourcePlayer
    Friend WithEvents TextBoxX As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents TextBoxY As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents TextBoxZ As TextBox
    Friend WithEvents NumericUpDown_n_image As NumericUpDown
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents NumericUpDown_Z_range As NumericUpDown
    Friend WithEvents ButtonCalibrate As Button
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents TextBox4 As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents ButtonSaveCalib As Button
    Friend WithEvents NumericUpDownFocus As NumericUpDown
    Friend WithEvents Label7 As Label
    Friend WithEvents Chart1 As AForge.Controls.Chart
    Friend WithEvents ButtonPlot As Button
    Friend WithEvents NumericUpDownExposure As NumericUpDown
    Friend WithEvents Label8 As Label
    Friend WithEvents TrackBarX As TrackBar
    Friend WithEvents TrackBarTopCrop As TrackBar
    Friend WithEvents TrackBarBottomCrop As TrackBar
    Friend WithEvents NumericUpDown_X_range As NumericUpDown
    Friend WithEvents Label10 As Label
    Friend WithEvents NumericUpDownNLines As NumericUpDown
    Friend WithEvents Label9 As Label
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents DisplayValue As Label
    Friend WithEvents ButtonScan As Button
    Friend WithEvents ButtonLoadCalib As Button
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents ComboBox1 As ComboBox
    Friend WithEvents Label11 As Label
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Scan As GroupBox
End Class
