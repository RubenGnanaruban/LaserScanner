Imports System.ComponentModel
Imports AForge.Imaging
Imports AForge.Video.DirectShow
Imports System.IO
Imports MathNet.Numerics.Interpolation
Imports MathNet.Numerics

Public Class LaserZScan
    Dim stage As ZaberNew
    Public Const nSeris As Integer = 100
    Public Const nCol As Integer = 760 '900
    Public nRow As Integer = 100
    Public yCrop As Integer
    Public Const xCrop As Integer = 570 '490
    Public Const binSize As Integer = 10
    Dim laserPosMat(nSeris - 1, nCol - 1) As Double
    Dim sampleZMat(nRow - 1, nCol - 1) As Double
    Dim btnCalibPress As Boolean = False
    Dim btnLoadPress As Boolean = False
    Dim rr(1000), gg(1000), bb(1000)
    Dim vidDev As Integer = 1
#Const Hardware = 1
#Const LaserAligned = 1

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
#If Hardware Then
        load_colormap()
        stage = New ZaberNew()
        Dim videoDevices As New FilterInfoCollection(FilterCategory.VideoInputDevice)
        Dim videoSource As New VideoCaptureDevice(videoDevices(vidDev).MonikerString)
        videoSource.VideoResolution = videoSource.VideoCapabilities(10) '10: 1920*1080
        videoSource.SnapshotResolution = videoSource.VideoCapabilities(10)
        VideoSourcePlayer1.VideoSource = videoSource
        'Dim minFocus, maxFocus, stepFocus, defFocus As Integer
        'videoSource.GetCameraPropertyRange(CameraControlProperty.Focus, minFocus, maxFocus, stepFocus, defFocus, CameraControlFlags.Manual)
        'Focus ranges from 0 to 40 in integer steps
        'videoSource.GetCameraPropertyRange(CameraControlProperty.Exposure, minFocus, maxFocus, stepFocus, defFocus, CameraControlFlags.Manual)
        'Zoom ranges from 0 to 317 in integer steps
        'Exposure ranges from -13 to 0 in integer steps
        videoSource.SetCameraProperty(CameraControlProperty.Focus, NumericUpDownFocus.Value, CameraControlFlags.Manual)
        videoSource.SetCameraProperty(CameraControlProperty.Exposure, NumericUpDownExposure.Value, CameraControlFlags.Manual)
        videoSource.Start()
        VideoSourcePlayer1.Start()
#End If
        ComboBox1.SelectedIndex = 0
        ButtonSaveCalib.Enabled = False
        ButtonPlot.Enabled = False
        ButtonScan.Enabled = False
    End Sub

    Public Function MaxArray1(ByRef a(,) As Double, nR As Integer) As Double
        Dim max As Double = a(0, 1)
        For iR As Integer = 1 To nR - 1
            If max < a(iR, 1) Then
                max = a(iR, 1)
            End If
        Next
        Return max
    End Function

    Public Sub NumericUpDown_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDownFocus.ValueChanged,
        NumericUpDownExposure.ValueChanged, TrackBarTopCrop.ValueChanged, TrackBarBottomCrop.ValueChanged
        Dim videoDevices As New FilterInfoCollection(FilterCategory.VideoInputDevice)
        Dim videoSource As New VideoCaptureDevice(videoDevices(vidDev).MonikerString)
        Try
            videoSource.SetCameraProperty(CameraControlProperty.Focus, NumericUpDownFocus.Value, CameraControlFlags.Manual)
            videoSource.SetCameraProperty(CameraControlProperty.Exposure, NumericUpDownExposure.Value, CameraControlFlags.Manual)
#If LaserAligned Then
            'next we plot the green channel of the select line
            Dim c As Color
            Dim bmp As New Bitmap(VideoSourcePlayer1.GetCurrentVideoFrame())
            yCrop = TrackBarTopCrop.Maximum - TrackBarTopCrop.Value
            nRow = 1080 - yCrop - TrackBarBottomCrop.Value
            Dim gCh(nRow - 1, 1), centroid(1, 1), gCh3_area, gCh3_centroid As Double
            gCh3_centroid = 0
            gCh3_area = 0
            For iG As Integer = 0 To nRow - 1 'This loop stores the Green channel values in the middle column
                gCh(iG, 1) = 0
                For iMovAvg As Integer = 0 To binSize - 1 'Moving average to smooth out the noise
                    c = bmp.GetPixel(TrackBarX.Value - 1, iG + yCrop + iMovAvg)
                    gCh(iG, 1) += c.G
                Next
                gCh(iG, 0) = iG
                If gCh(iG, 1) < 40 * binSize Then
                    gCh(iG, 1) = 0
                End If
                'If CheckBoxCubed.Checked Then
                '    gCh(iG, 1) *= gCh(iG, 1) * gCh(iG, 1) * gCh(iG, 1)
                'End If
                gCh3_centroid += gCh(iG, 1) * iG 'first moment of area
                gCh3_area += gCh(iG, 1)
            Next
            centroid(0, 1) = 0
            centroid(0, 0) = gCh3_centroid / gCh3_area
            centroid(1, 1) = MaxArray1(gCh, nRow)
            centroid(1, 0) = gCh3_centroid / gCh3_area
            DisplayValue.Text = Math.Round(gCh3_centroid / gCh3_area, 4)
            Application.DoEvents()
            Chart1.RemoveAllDataSeries()
            ' add New data series to the chart
            Chart1.AddDataSeries("Test", Color.Green, Chart1.SeriesType.ConnectedDots, 3)
            ' set X range to display
            Chart1.RangeX = New AForge.Range(0, nRow - 1)
            ' update the chart
            Chart1.UpdateDataSeries("Test", gCh)
            Chart1.AddDataSeries("Centroid", Color.Red, Chart1.SeriesType.ConnectedDots, 3)
            Chart1.UpdateDataSeries("Centroid", centroid)
#End If
        Catch ex As Exception
        End Try
    End Sub

    Public Sub TrackBarX_ValueChanged(sender As Object, e As EventArgs) Handles TrackBarX.ValueChanged
        If btnCalibPress Then
            ButtonPlot_Click(sender, e)
        Else
            NumericUpDown_ValueChanged(sender, e)
        End If
        If btnLoadPress Then
            PlotSpline(sender, e)
        End If
    End Sub

    Private Sub TextBoxX_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBoxX.KeyDown
        If e.KeyCode = Keys.Enter Then
            stage.MoveRelative(stage.Xaxe, TextBoxX.Text)
            NumericUpDown_ValueChanged(sender, e)
        End If
    End Sub

    Private Sub TextBoxY_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBoxY.KeyDown
        If e.KeyCode = Keys.Enter Then
            stage.MoveRelative(stage.Yaxe, TextBoxY.Text)
            NumericUpDown_ValueChanged(sender, e)
        End If
    End Sub


    Private Sub TextBoxZ_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBoxZ.KeyDown
        If e.KeyCode = Keys.Enter Then
            stage.MoveRelative(stage.Zaxe, TextBoxZ.Text)
            NumericUpDown_ValueChanged(sender, e)
        End If

    End Sub

    'Function DiracDeltaFit(a As Double, b As Double, x As Double) As Double
    '    Dim pow As Double = (x - b) / a
    '    DiracDeltaFit = -Math.Log(a) - pow * pow
    'End Function

    Public Sub ButtonCalibrate_Click(sender As Object, e As EventArgs) Handles ButtonCalibrate.Click
        btnCalibPress = True
        Dim gCh3_area, g3, gCh3_centroid As Double
        yCrop = TrackBarTopCrop.Maximum - TrackBarTopCrop.Value
        nRow = 1080 - yCrop - TrackBarBottomCrop.Value
        ReDim laserPosMat(NumericUpDown_n_image.Value - 1, nCol - 1)
        Dim r, g, b As Byte
        If NumericUpDown_n_image.Value = 1 Then
            Dim bmp_cal As New Bitmap(VideoSourcePlayer1.GetCurrentVideoFrame())
            bmp_cal.Save("c:\temp\" & TextBox4.Text & ".png")
        Else
            stage.MoveRelative(stage.Zaxe, (NumericUpDown_Z_range.Value * -0.0005)) 'go to the bottom most layer'
            For indSeris As Integer = 1 To NumericUpDown_n_image.Value
                Dim bmp_full As New Bitmap(VideoSourcePlayer1.GetCurrentVideoFrame())
                Dim CropRect As New Rectangle(xCrop, yCrop, nCol, nRow)
                Dim bmp_cal = New FastBitmap(CropRect.Width, CropRect.Height, Imaging.PixelFormat.Format24bppRgb)
                bmp_cal.GR.DrawImage(bmp_full, New Rectangle(0, 0, CropRect.Width, CropRect.Height), CropRect, GraphicsUnit.Pixel)
                BitmapToBytes(bmp_cal.Realbmp, bmp_cal.bytes)
                'bmp_cal.Realbmp.Save("c:\temp\" & TextBox4.Text & "_" & indSeris.ToString("D2") & ".png")
                'Using grp = Graphics.FromImage(bmp_cal)
                '    grp.DrawImage(bmp_full, New Rectangle(0, 0, CropRect.Width, CropRect.Height), CropRect, GraphicsUnit.Pixel)
                bmp_full.Dispose()
                '    bmp_cal.Save("c:\temp\" & TextBox4.Text & "_" & indSeris.ToString("D2") & ".png")
                'End Using
                For iC As Integer = 0 To nCol - 1
                    gCh3_area = 0
                    gCh3_centroid = 0
                    For iR As Integer = 0 To nRow - 1 'This loop finds the centroidal green pixel in each column
                        If iC >= binSize \ 2 AndAlso iC <= nCol - 1 - binSize \ 2 Then
                            g3 = 0
                            For iWin As Integer = -binSize \ 2 To binSize \ 2
                                bmp_cal.GetPixel(iC + iWin, iR, r, g, b)
                                g3 += g
                            Next
                            g3 /= (binSize + 1)
                        Else
                            bmp_cal.GetPixel(iC, iR, r, g, b)
                            g3 = g
                        End If
                        'For iMovAvg As Integer = 0 To binSize - 1 'Moving average to smooth out the noise
                        '    bmp_cal.GetPixel(iC, iR + iMovAvg, r, g, b)
                        '    'c = bmp.GetPixel(iC, iR + iMovAvg)
                        '    'g3 += c.G
                        '    g3 += g
                        'Next
                        'g3 /= binSize
                        'g3 = g3 ^ 10
                        If g3 < 40 Then
                            g3 = 0
                        End If
                        gCh3_centroid += g3 * iR 'first moment of area
                        gCh3_area += g3
                    Next
                    laserPosMat(indSeris - 1, iC) = gCh3_centroid / gCh3_area
                Next
                If indSeris <> NumericUpDown_n_image.Value Then 'Move the bed up for next scan, except for the final layer
                    stage.MoveRelative(stage.Zaxe, NumericUpDown_Z_range.Value * 0.001 / (NumericUpDown_n_image.Value - 1))
                    DisplayValue.Text = Math.Round(indSeris / NumericUpDown_n_image.Value * 100) & " % of calibration done"
                    Application.DoEvents()
                Else
                    stage.MoveRelative(stage.Zaxe, NumericUpDown_Z_range.Value * -0.0005) 'go to the middle layer'
                    DisplayValue.Text = "Calibration completed"
                End If
            Next
        End If
        ButtonSaveCalib.Enabled = True
        ButtonPlot.Enabled = True
        ButtonScan.Enabled = True
    End Sub

    Private Sub Form1_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        Dim videoDevices As New FilterInfoCollection(FilterCategory.VideoInputDevice)
        Dim videoSource As New VideoCaptureDevice(videoDevices(vidDev).MonikerString)
        VideoSourcePlayer1.VideoSource = videoSource
        videoSource.Stop()
        VideoSourcePlayer1.Stop()
        Application.Exit()
    End Sub

    Private Sub ButtonSaveCalib_Click(sender As Object, e As EventArgs) Handles ButtonSaveCalib.Click
        'Try
        '    'Dim FileName As String = "c:\temp\myarray.txt"
        '    'IO.File.WriteAllLines(FileName, laserPosMat.ToString)

        '    Using sw As StreamWriter = New StreamWriter("c:\temp\dataBlue" & TextBox4.Text & ".txt")
        '        For indCol As Integer = 0 To nCol - 1
        '            Dim s As String = ""
        '            For indSeris As Integer = 0 To nSeris - 1
        '                s += laserPosMat(indSeris, indCol).ToString & ","
        '            Next
        '            sw.WriteLine(s)
        '        Next
        '    End Using
        'Catch ex As Exception
        'End Try

        Dim fn As Integer = FreeFile()
        FileOpen(fn, "c:\temp\Calibration_" & TextBox4.Text & ".txt", OpenMode.Output)
        For iY = 0 To laserPosMat.GetUpperBound(1)
            Dim s As String = ""
            For iZ As Integer = 0 To laserPosMat.GetUpperBound(0)
                s += laserPosMat(iZ, iY).ToString & ","
            Next
            PrintLine(fn, s)
        Next
        FileClose(fn)
    End Sub

    Private Sub ButtonPlot_Click(sender As Object, e As EventArgs) Handles ButtonPlot.Click
        Dim calibPlot(laserPosMat.GetUpperBound(0), 1) As Double
        For iSeris As Integer = 0 To laserPosMat.GetUpperBound(0)
            calibPlot(iSeris, 0) = Convert.ToDouble(iSeris) * NumericUpDown_Z_range.Value / NumericUpDown_n_image.Value
            calibPlot(iSeris, 1) = laserPosMat(iSeris, TrackBarX.Value - xCrop)
        Next
        Chart1.RemoveAllDataSeries()
        ' add New data series to the chart
        Chart1.AddDataSeries("laserAtCol", Color.Blue, Chart1.SeriesType.ConnectedDots, 3)
        ' set X range to display
        'Dim n_im As Integer = NumericUpDown_n_image.Value
        Chart1.RangeX = New AForge.Range(0, NumericUpDown_Z_range.Value - 1)
        Chart1.UpdateDataSeries("laserAtCol", calibPlot)
    End Sub

    Private Sub PlotSpline(sender As Object, e As EventArgs)
        Dim calibPlot(laserPosMat.GetUpperBound(0), 1), xCalib(laserPosMat.GetUpperBound(0)), yCalib(laserPosMat.GetUpperBound(0)) As Double
        For iSeris As Integer = 0 To laserPosMat.GetUpperBound(0)
            calibPlot(iSeris, 0) = Convert.ToDouble(iSeris) * NumericUpDown_Z_range.Value / NumericUpDown_n_image.Value
            calibPlot(iSeris, 1) = laserPosMat(iSeris, TrackBarX.Value - xCrop)
            xCalib(iSeris) = calibPlot(iSeris, 0)
            yCalib(iSeris) = calibPlot(iSeris, 1)
        Next
        Chart1.RemoveAllDataSeries()
        ' add New data series to the chart
        Chart1.AddDataSeries("laserAtCol", Color.Blue, Chart1.SeriesType.Dots, 7)
        ' set X range to display
        Chart1.RangeX = New AForge.Range(0, NumericUpDown_Z_range.Value - 1)
        Chart1.UpdateDataSeries("laserAtCol", calibPlot)
        'Plot the fitted curve
        Dim curveFit(10 * laserPosMat.GetUpperBound(0), 1) As Double
        Select Case ComboBox1.SelectedIndex
            Case 1
                Dim akimaSpline As CubicSpline = CubicSpline.InterpolateAkimaSorted(xCalib, yCalib)
                For iSpl As Integer = 0 To 10 * laserPosMat.GetUpperBound(0)
                    curveFit(iSpl, 0) = 0.1 * Convert.ToDouble(iSpl) * NumericUpDown_Z_range.Value / NumericUpDown_n_image.Value
                    curveFit(iSpl, 1) = akimaSpline.Interpolate(curveFit(iSpl, 0))
                Next
            Case 0
                Dim line As Tuple(Of Double, Double) = Fit.Line(xCalib, yCalib)
                For iSpl As Integer = 0 To 10 * laserPosMat.GetUpperBound(0)
                    curveFit(iSpl, 0) = 0.1 * Convert.ToDouble(iSpl) * NumericUpDown_Z_range.Value / NumericUpDown_n_image.Value
                    curveFit(iSpl, 1) = line.Item1 + line.Item2 * curveFit(iSpl, 0)
                Next
        End Select
        Chart1.AddDataSeries("splineAtCol", Color.Red, Chart1.SeriesType.ConnectedDots, 3)
        Chart1.UpdateDataSeries("splineAtCol", curveFit)
    End Sub

    Private Sub ButtonScan_Click(sender As Object, e As EventArgs) Handles ButtonScan.Click
        yCrop = TrackBarTopCrop.Maximum - TrackBarTopCrop.Value
        nRow = 1080 - yCrop - TrackBarBottomCrop.Value
        ReDim sampleZMat(NumericUpDownNLines.Value - 1, nCol - 1)
        Dim c As Color, gCh3_area, g3, gCh3_centroid As Double
        Dim r, g, b As Byte
        If NumericUpDownNLines.Value = 1 Then
            stage.MoveAbsolute(stage.Xaxe, 12.5, True)
        Else
            stage.MoveAbsolute(stage.Xaxe, 12.5 - NumericUpDown_X_range.Value * 0.5, True) 'bring the first line to laser for scanning
        End If
        Dim xCalib(NumericUpDown_n_image.Value - 1), yCalib(NumericUpDown_n_image.Value - 1) As Double
        xCalib(0) = NumericUpDown_Z_range.Value * -0.0005
        For iZ As Integer = 1 To NumericUpDown_n_image.Value - 1
            xCalib(iZ) = xCalib(iZ - 1) + NumericUpDown_Z_range.Value * 0.001 / (NumericUpDown_n_image.Value - 1)
        Next
        For iLine As Integer = 1 To NumericUpDownNLines.Value
            Dim bmp_full As New Bitmap(VideoSourcePlayer1.GetCurrentVideoFrame())

            Dim CropRect As New Rectangle(xCrop, yCrop, nCol, nRow)
            Dim bmp = New FastBitmap(CropRect.Width, CropRect.Height, Imaging.PixelFormat.Format24bppRgb)

            bmp.GR.DrawImage(bmp_full, New Rectangle(0, 0, CropRect.Width, CropRect.Height), CropRect, GraphicsUnit.Pixel)
            BitmapToBytes(bmp.Realbmp, bmp.bytes)
            bmp.Realbmp.Save("c:\temp\" & TextBox4.Text & iLine.ToString("D3") & "Scan" & ".png")
            'Using grp = Graphics.FromImage(bmp)
            '    grp.DrawImage(bmp_full, New Rectangle(0, 0, CropRect.Width, CropRect.Height), CropRect, GraphicsUnit.Pixel)
            '    bmp_full.Dispose()
            '    bmp.Save("c:\temp\" & TextBox4.Text & iLine.ToString("D3") & "Scan" & ".png")
            'End Using
            For iC As Integer = 0 To nCol - 1
                For iSeris As Integer = 0 To NumericUpDown_n_image.Value - 1
                    yCalib(iSeris) = laserPosMat(iSeris, iC)
                    'If iC < binSize \ 2 Then
                    '    yCalib(iSeris) = laserPosMat(iSeris, iC)
                    'ElseIf iC > nCol - 1 - binSize \ 2 Then
                    '    yCalib(iSeris) = laserPosMat(iSeris, iC)
                    'Else
                    '    yCalib(iSeris) = 0
                    '    For iWin As Integer = -binSize \ 2 To binSize \ 2
                    '        yCalib(iSeris) += laserPosMat(iSeris, iC + iWin)
                    '    Next
                    '    yCalib(iSeris) /= (binSize + 1)
                    'End If
                Next
                gCh3_area = 0
                gCh3_centroid = 0
                For iR As Integer = 0 To nRow - 1 '- binSize 'This loop finds the centroidal green pixel in each column
                    bmp.GetPixel(iC, iR, r, g, b)
                    g3 = g

                    'g3 = 0
                    'For iMovAvg As Integer = 0 To binSize - 1 'Moving average to smooth out the noise
                    '    bmp.GetPixel(iC, iR + iMovAvg, r, g, b)
                    '    'c = bmp.GetPixel(iC, iR + iMovAvg)
                    '    'g3 += c.G
                    '    g3 += g
                    'Next
                    'g3 /= binSize

                    'g3 = g3 ^ 10
                    If g3 < 40 Then
                        g3 = 0
                    End If
                    gCh3_centroid += g3 * iR 'first moment of area
                    gCh3_area += g3
                Next
                gCh3_centroid /= gCh3_area
                Select Case ComboBox1.SelectedIndex
                    Case 1
                        Dim akimaSpline As CubicSpline = CubicSpline.InterpolateAkimaSorted(yCalib, xCalib)
                        sampleZMat(iLine - 1, iC) = akimaSpline.Interpolate(gCh3_centroid)
                    Case 0
                        Dim line As Tuple(Of Double, Double) = Fit.Line(xCalib, yCalib)
                        sampleZMat(iLine - 1, iC) = (gCh3_centroid - line.Item1) / line.Item2
                End Select
            Next
            If iLine <> NumericUpDownNLines.Value Then 'Move the bed along for the next line, except for the final line
                stage.MoveRelative(stage.Xaxe, NumericUpDown_X_range.Value / (NumericUpDownNLines.Value - 1))
                DisplayValue.Text = Math.Round(iLine / NumericUpDownNLines.Value * 100, 1) & " % of scanning done"
                Application.DoEvents()
            End If
        Next
        DisplayValue.Text = "Scanning completed"
        Dim fn As Integer = FreeFile()
        FileOpen(fn, "c:\temp\SampleZmap_" & TextBox4.Text & ".txt", OpenMode.Output)
        For iX = 0 To sampleZMat.GetUpperBound(0)
            Dim s As String = ""
            For iY As Integer = 0 To sampleZMat.GetUpperBound(1)
                s += sampleZMat(iX, iY).ToString & ","
            Next
            PrintLine(fn, s)
        Next
        FileClose(fn)

        Dim DimX, DimY As Integer
        DimX = sampleZMat.GetLength(0)
        DimY = sampleZMat.GetLength(1)

        Dim Heatmap As New FastBitmap(DimX, DimY, Imaging.PixelFormat.Format24bppRgb)
        Dim min, max As Double
        max = Double.MinValue '0.001
        min = Double.MaxValue '0
        For y = 0 To DimY - 1
            For x = 0 To DimX - 1
                If sampleZMat(x, y) <> Double.NaN Then
                    If sampleZMat(x, y) > max Then max = sampleZMat(x, y)
                    If sampleZMat(x, y) < min Then min = sampleZMat(x, y)
                End If
            Next
        Next

        Dim index As Integer
        For y = 0 To DimY - 1
            For x = 0 To DimX - 1
                If Double.IsNaN(sampleZMat(x, y)) Then
                    index = 0
                Else index = (sampleZMat(x, y) - min) / (max - min) * 1000
                End If
                Heatmap.FillOriginalPixel(x, y, rr(index), gg(index), bb(index))
            Next
        Next

        Heatmap.Reset()
        PictureBox1.Image = Heatmap.Realbmp
        PictureBox1.Image.RotateFlip(RotateFlipType.Rotate90FlipX)
        Heatmap.Realbmp.Save("c:\temp\heatmap.jpg")

        stage.MoveAbsolute(stage.Xaxe, 12.5, True)
        DisplayValue.Text = "Scanning completed"
    End Sub

    Private Sub ButtonLoadCalib_Click(sender As Object, e As EventArgs) Handles ButtonLoadCalib.Click
        btnLoadPress = True
        DisplayValue.Text = "Loading calibration file ..."
        Dim resultDialog As DialogResult = OpenFileDialog1.ShowDialog()
        If resultDialog = Windows.Forms.DialogResult.OK Then
            Dim pathOpenFile As String = OpenFileDialog1.FileName
            Dim lineCount = File.ReadLines(pathOpenFile).Count()
            ReDim laserPosMat(NumericUpDown_n_image.Value - 1, lineCount - 1)
            'Dim textCalib As String = File.ReadAllText(pathOpenFile)
            'Me.Text = textCalib.Length.ToString ' For debugging.
            Using textCalib As New Microsoft.VisualBasic.FileIO.TextFieldParser(pathOpenFile)
                textCalib.TextFieldType = FileIO.FieldType.Delimited
                textCalib.SetDelimiters(",")
                Dim currentRow As String()
                Dim indSeris As Integer = 0
                While Not textCalib.EndOfData

                    Try
                        currentRow = textCalib.ReadFields()
                        Dim currentField As String
                        Dim iC As Integer = 0
                        For Each currentField In currentRow
                            laserPosMat(iC, indSeris) = currentField
                            iC += 1
                            If iC = NumericUpDown_n_image.Value Then
                                indSeris += 1
                                Exit For
                            End If
                        Next
                    Catch ex As Microsoft.VisualBasic.FileIO.MalformedLineException
                        MsgBox("Line " & ex.Message & "is not valid and will be skipped.")
                    End Try
                End While
            End Using
        End If
        DisplayValue.Text = "Calibration loaded"
        ButtonScan.Enabled = True
        ButtonPlot.Enabled = True
    End Sub

    Public Sub load_colormap()
        ReDim rr(1000), gg(1000), bb(1000)

        FileOpen(1, "rgb.txt", OpenMode.Input)

        Dim t As Integer
        Do Until EOF(1)
            t += 1
            Input(1, rr(t))
            Input(1, gg(t))
            Input(1, bb(t))
        Loop
        FileClose(1)

    End Sub
End Class