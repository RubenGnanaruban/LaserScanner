
'Author: Vimalatharmaiyah Gnanaruban
'October 2022


Imports System.ComponentModel
Imports AForge.Imaging
Imports AForge.Video.DirectShow
Imports System.IO
Imports MathNet.Numerics.Interpolation
Imports MathNet.Numerics
#Const PRINTFILE = 0

Public Class LaserScannerClass
    Dim XrangeLaser As Single
    Dim YrangeLaser As Single
    Dim Xmax As Single
    Dim Zsteps As Integer
    Dim Zrange As Single
    Dim nCol As Integer
    Dim nRow As Integer
    Dim y1Crop As Integer
    Dim x1Crop As Integer
    Dim binSize As Integer
    Dim laserPosMat(,) As Double
    Dim laserFit(,) As Double
    Public sampleZMat(,) As Single
    Public brightnessMat(,) As Single
    Public Heatmap, Brightnessmap As FastBitmap
    Public ZZ_laser As Single
    'Public ZZ_laser, ZZ_estimate_laser, ZZ_estimate_laser_min(), ZZ_estimate_laser_max() As Single
    Dim BrightnessAvg_laser As Single
    Dim Blure, BlureBright As FFTW_VB_Real
    'Dim vidDev As Integer = 1
    Dim videoSourceplayer1 As AForge.Controls.VideoSourcePlayer
    Dim laserMiddle As Single
    Public rangeFac As Single
    Public ZZ_estimate_laser_min, ZZ_estimate_laser_max, ZZ_estimate_laser_avg As Single
    Public IsLaserScanned, IsMaskMade As Boolean
    Public Mask As Bitmap
    Public pixX_FOV_laser, pixY_FOV_laser As Integer

    Public Sub New(x1 As Integer, y1 As Integer, nX As Integer, nY As Integer, ByRef videoSourceplayer As AForge.Controls.VideoSourcePlayer)
        'Form1.CheckBox_previewLaserMap.Checked = False
        Form1.RadioButton2.Enabled = False
        Form1.RadioButton3.Enabled = False
        videoSourceplayer1 = videoSourceplayer
        x1Crop = x1
        y1Crop = y1
        nCol = nX
        nRow = nY
        rangeFac = 0.2
        Zsteps = Setting.Gett("LASER_NUMCALIBRATION")
        Zrange = Setting.Gett("LASER_ZRANGE")
        'laserMiddle = Setting.Gett("LASER_MID")
        laserMiddle = (Val(Setting.Gett("XMAX")) + Val(Setting.Gett("XMIN"))) * 0.5
        XrangeLaser = Setting.Gett("XRANGE")
        YrangeLaser = Setting.Gett("YRANGE")
        Xmax = Setting.Gett("XMAX")
        IsLaserScanned = False
        IsMaskMade = False
        ' This call is required by the designer.

        ' Add any initialization after the InitializeComponent() call.
        Dim laserPosMat(Zsteps - 1, nCol - 1) As Double
        Dim laserFit(nCol - 1, 1) As Double
        'Dim sampleZMat(nRow - 1, nCol - 1) As Double
        'Dim reflectivityMat(nRow - 1, nCol - 1) As Double
        webcam.SetProperties(Form1.TextBox24.Text, Form1.TextBox23.Text)
        Try
            LoadCalibration("laserCalibration.txt")
        Catch ex As Exception

        End Try
        pixX_FOV_laser = Stage.FOVX * Val(Setting.Gett("LASER_SCAN_LINES")) / XrangeLaser
        pixY_FOV_laser = Stage.FOVY * nCol / YrangeLaser
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

    Public Sub Calibrate(cutoff As Integer)
        binSize = Setting.Gett("LASER_BINSIZE")
        Dim gCh3_area, g3, gCh3_centroid As Double
        ReDim laserPosMat(Zsteps - 1, nCol - 1)
        ReDim laserFit(nCol - 1, 1)
        Dim r, g, b As Byte
        Dim xCalib(Zsteps - 1), yCalib(Zsteps - 1) As Double
        Dim xLaserCent As Integer
        'Stage.GoToFocus()
        Stage.MoveAbsolute(Stage.Xaxe, laserMiddle + Val(Setting.Gett("LASER_XOFFSET")), True)
        Stage.MoveAbsolute(Stage.Yaxe, 0, True)
        Form1.CheckBox3.Checked = True
        'Stage.MoveAbsolute(Stage.Zaxe, 0, True) 'go to the bottom most layer'
        If Form1.CheckBox_PlasticHolder.Checked Then
            Stage.MoveAbsolute(Stage.Zaxe, Setting.Gett("LASER_DELPLATES"), True) 'go to the bottom most layer'
        Else
            Stage.MoveAbsolute(Stage.Zaxe, 0, True) 'go to the bottom most layer'
        End If
        'Stage.MoveAbsolute(Stage.Zaxe, Setting.Gett("LASER_DELPLATES") - Zrange * 0.5, True) 'go to the bottom most layer'
        'Stage.MoveRelative(Stage.Zaxe, Setting.Gett("LASER_DELPLATES") - Zrange * 0.5) 'go to the bottom most layer'
        Form1.Pbar.Maximum = Zsteps
        Form1.Pbar.Value = 0

        Dim Smooth As AForge.Imaging.Filters.BilateralSmoothing
        Smooth = New Filters.BilateralSmoothing
        Smooth.KernelSize = 7
        Smooth.SpatialFactor = 10 '10
        Smooth.ColorFactor = 60
        Smooth.ColorPower = 0.5

        For indSeris As Integer = 1 To Zsteps
            Dim bmp_full As New Bitmap(videoSourceplayer1.GetCurrentVideoFrame())
            If indSeris <> Zsteps Then 'Move the bed up for next scan, except for the final layer
                Stage.MoveRelative(Stage.Zaxe, Zrange / (Zsteps - 1))
                Form1.Pbar.Increment(1)
                'MsgBox(Math.Round(indSeris / Zsteps * 100) & " % of calibration done")
                Application.DoEvents()
            Else
                'Stage.MoveAbsolute(Stage.Zaxe, Zrange * 0.5) 'go to the middle layer'
                If Form1.CheckBox_PlasticHolder.Checked Then
                    Stage.MoveAbsolute(Stage.Zaxe, Setting.Gett("LASER_DELPLATES") + Zrange * 0.5) 'go to the middle layer'
                Else
                    Stage.MoveAbsolute(Stage.Zaxe, Zrange * 0.5) 'go to the middle layer'
                End If
                'Stage.MoveRelative(Stage.Zaxe, -Setting.Gett("LASER_DELPLATES") + Zrange * -0.5) 'go to the middle layer'
                Form1.Pbar.Increment(1)
                'MsgBox("Calibration completed, Saving ...")
            End If
            Dim CropRect As New Rectangle(x1Crop, y1Crop, nCol, nRow)
            Dim bmp_cal = New FastBitmap(CropRect.Width, CropRect.Height, Imaging.PixelFormat.Format24bppRgb)
            bmp_cal.GR.DrawImage(bmp_full, New Rectangle(0, 0, CropRect.Width, CropRect.Height), CropRect, GraphicsUnit.Pixel)

            Smooth.ApplyInPlace(bmp_cal.Realbmp)
            BitmapToBytes(bmp_cal.Realbmp, bmp_cal.bytes)
            'bmp_cal.Realbmp.Save("c:\temp\" & Form1.Laser_seriesName.Text & indSeris.ToString("D3") & "Calib" & ".png")
            bmp_full.Dispose()
            For iC As Integer = 0 To nCol - 1
                gCh3_area = 0
                gCh3_centroid = 0
                For iG As Integer = 0 To nRow - 1 'This loop finds the centroidal green pixel in each column
                    'If iC >= binSize \ 2 AndAlso iC <= nCol - 1 - binSize \ 2 Then 'binning each columns
                    '    g3 = 0
                    '    For iWin As Integer = -binSize \ 2 To binSize \ 2
                    '        bmp_cal.GetPixel(iC + iWin, iG, r, g, b)
                    '        g3 += g
                    '    Next
                    '    g3 /= (binSize + 1)
                    'Else
                    '    bmp_cal.GetPixel(iC, iG, r, g, b)
                    '    g3 = g
                    'End If
                    bmp_cal.GetPixel(iC, iG, r, g, b)
                    g3 = g
                    g3 = g3 ^ 8
                    ' To remove the wings
                    'If g3 > cutoff Then
                    gCh3_centroid += g3 * iG 'first moment of area
                    gCh3_area += g3
                    'End If
                Next
                gCh3_centroid /= gCh3_area
                laserPosMat(indSeris - 1, iC) = gCh3_centroid
#If PRINTFILE Then
                If Double.IsNaN(gCh3_centroid) Then
                Else
                    xLaserCent = gCh3_centroid
                End If
                bmp_cal.FillOriginalBluePixel(iC, xLaserCent, 0)
#End If
            Next
            xCalib(indSeris - 1) = Stage.Z

#If PRINTFILE Then
            byteToBitmap(bmp_cal.bytes, bmp_cal.Realbmp)
            bmp_cal.Realbmp.Save("c:\temp\" & Form1.Laser_seriesName.Text & indSeris.ToString("D3") & "Calib" & ".png")
#End If

        Next
        Form1.CheckBox3.Checked = False

        'xCalib(0) = Zrange * -0.5
        'For iZ As Integer = 1 To Zsteps - 1
        '    xCalib(iZ) = xCalib(iZ - 1) + Zrange / (Zsteps - 1)
        'Next
        For iC As Integer = 0 To nCol - 1
            For iSeris As Integer = 0 To Zsteps - 1
                yCalib(iSeris) = laserPosMat(iSeris, iC)
                If Double.IsNaN(laserPosMat(iSeris, iC)) Then
                    If iSeris = 0 Then
                        yCalib(iSeris) = laserPosMat(iSeris + 1, iC)
                    Else
                        yCalib(iSeris) = laserPosMat(iSeris - 1, iC)
                    End If
                End If
            Next
            Dim line As Tuple(Of Double, Double) = Fit.Line(xCalib, yCalib)
            laserFit(iC, 0) = line.Item1
            laserFit(iC, 1) = line.Item2
        Next


        Dim fn_calibFit As Integer = FreeFile()
        'FileOpen(fn_calibFit, "c:\temp\CalibrationFit_" & Form1.Laser_seriesName.Text & ".txt", OpenMode.Output)
        ' to save and  load autamatically when loading the program
        FileOpen(fn_calibFit, "laserCalibration" & ".txt", OpenMode.Output)
        For iY = 0 To laserPosMat.GetUpperBound(1)
            'Dim s As String = ""
            'For iZ As Integer = 0 To laserPosMat.GetUpperBound(0)
            '    s += laserPosMat(iZ, iY).ToString & ","
            'Next
            'PrintLine(fn_calib, s)
            Dim s_fit As String = ""
            s_fit += laserFit(iY, 0).ToString & ","
            s_fit += laserFit(iY, 1).ToString & ","
            PrintLine(fn_calibFit, s_fit)
        Next
        'FileClose(fn_calib)
        FileClose(fn_calibFit)
        Form1.Pbar.Value = 0
        Form1.Laser_scan.Enabled = True
    End Sub

    Public Sub LoadCalibration(pathOpenFile As String)


        Dim lineCount = File.ReadLines(pathOpenFile).Count()
        'ReDim laserPosMat(Zsteps - 1, lineCount - 1)
        ReDim laserFit(lineCount - 1, 1)
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
                        'laserPosMat(iC, indSeris) = currentField
                        laserFit(indSeris, iC) = currentField
                        iC += 1
                        If iC = 2 Then 'iC = Zsteps Then
                            indSeris += 1
                            Exit For
                        End If
                    Next
                Catch ex As Microsoft.VisualBasic.FileIO.MalformedLineException
                    MsgBox("Line " & ex.Message & "is not valid and will be skipped.")
                End Try
            End While
        End Using

        'MsgBox("Calibration loaded")
        Form1.Laser_scan.Enabled = True
    End Sub

    Public Sub Scan(NScanLines As Single, cutoff As Integer)
        IsLaserScanned = False
        'Form1.CheckBox_previewLaserMap.Checked = False
        Form1.RadioButton2.Enabled = False
        Form1.RadioButton3.Enabled = False
        ReDim sampleZMat(NScanLines - 1, nCol - 1)
        ReDim brightnessMat(NScanLines - 1, nCol - 1)
        Dim rgCh8_area, rg8, rgCh8_centroid As Double
        Dim rg As Single
        Dim r, g, b As Byte
        Dim xLaserCent As Integer
        Blure = New FFTW_VB_Real(NScanLines, nCol)
        Blure.MakeGaussianReal(0.2, Blure.MTF, 2)

        BlureBright = New FFTW_VB_Real(NScanLines, nCol)
        BlureBright.MakeGaussianReal(0.4, BlureBright.MTF, 2)

        Stage.MoveAbsolute(Stage.Yaxe, 0, True)
        'Stage.MoveAbsolute(Stage.Zaxe, Zrange * 0.5, True)
        If Form1.CheckBox_PlasticHolder.Checked Then
            Stage.MoveAbsolute(Stage.Zaxe, Setting.Gett("LASER_DELPLATES") + Zrange * 0.5) 'go to the middle layer'
        Else
            Stage.MoveAbsolute(Stage.Zaxe, Zrange * 0.5) 'go to the middle layer'
        End If
        Form1.CheckBox3.Checked = True
        If Zsteps = 1 Then
            Stage.MoveAbsolute(Stage.Xaxe, laserMiddle, True)
        Else
            Stage.MoveAbsolute(Stage.Xaxe, (Xmax + Val(Setting.Gett("LASER_XOFFSET"))), True) 'bring the first line to laser for scanning
            'Stage.MoveAbsolute(Stage.Xaxe, laserMiddle - Xrange * 0.5, True) 'bring the first line to laser for scanning
        End If
        'Dim xCalib(Zsteps - 1), yCalib(Zsteps - 1) As Double
        'xCalib(0) = Zrange * -0.5
        'For iZ As Integer = 1 To Zsteps - 1
        '    xCalib(iZ) = xCalib(iZ - 1) + Zrange / (Zsteps - 1)
        'Next
        Form1.Pbar.Maximum = NScanLines
        Form1.Pbar.Value = 0
        'Dim Smooth As AForge.Imaging.Filters.Mean
        'Smooth = New Filters.Mean

        Dim Smooth As AForge.Imaging.Filters.BilateralSmoothing
        Smooth = New Filters.BilateralSmoothing
        Smooth.KernelSize = 7
        Smooth.SpatialFactor = 10 '10
        Smooth.ColorFactor = 60
        Smooth.ColorPower = 0.5
        For iLine As Integer = 1 To NScanLines
            Dim bmp_full As New Bitmap(videoSourceplayer1.GetCurrentVideoFrame())
            If iLine <> NScanLines Then 'Move the bed along for the next line, except for the final line
                Stage.MoveRelative(Stage.Xaxe, -XrangeLaser / (NScanLines - 1), False)
                Application.DoEvents()
                'Threading.Thread.CurrentThread.Sleep(200)
            End If
            Dim CropRect As New Rectangle(x1Crop, y1Crop, nCol, nRow)
            Dim bmp = New FastBitmap(CropRect.Width, CropRect.Height, Imaging.PixelFormat.Format24bppRgb)

            bmp.GR.DrawImage(bmp_full, New Rectangle(0, 0, CropRect.Width, CropRect.Height), CropRect, GraphicsUnit.Pixel)
            'Smooth.ApplyInPlace(bmp.Realbmp)
            'Smooth.ApplyInPlace(bmp.Realbmp)
            'Smooth.ApplyInPlace(bmp.Realbmp)
            Smooth.ApplyInPlace(bmp.Realbmp)
            BitmapToBytes(bmp.Realbmp, bmp.bytes)
            'Dim bytessingle(bmp.bytes.Length - 1) As Single
            'For j = 0 To bmp.bytes.Length - 1

            'Next

            For iC As Integer = 0 To nCol - 1
                'For iSeris As Integer = 0 To Zsteps - 1
                '    yCalib(iSeris) = laserPosMat(iSeris, iC)
                'Next
                rgCh8_area = 0
                rgCh8_centroid = 0
                For iR As Integer = 0 To nRow - 1 '- binSize 'This loop finds the centroidal green pixel in each column
                    bmp.GetPixel(iC, iR, r, g, b)
                    rg = Convert.ToSingle(r) + Convert.ToSingle(g)
                    If rg < cutoff Then
                        rg = 0
                    End If
                    rg8 = rg ^ 8

                    rgCh8_centroid += rg8 * iR 'first moment of area
                    rgCh8_area += rg8
                Next
                rgCh8_centroid /= rgCh8_area
                If Double.IsNaN(rgCh8_centroid) Then
                Else
                    xLaserCent = rgCh8_centroid
                End If
                bmp.GetPixel(iC, xLaserCent, r, g, b)
                brightnessMat(iLine - 1, iC) = Convert.ToSingle(r) + Convert.ToSingle(g) '+ Convert.ToSingle(b)
                'For iWin As Integer = -binSize \ 2 To binSize \ 2
                '    bmp.GetPixel(iC, xLaserCent + iWin, r, g, b)
                '    brightnessMat(iLine - 1, iC) += g
                '    'brightnessMat(iLine - 1, iC) += r
                'Next
                'Dim line As Tuple(Of Double, Double) = Fit.Line(xCalib, yCalib)
                'sampleZMat(iLine - 1, iC) = (gCh3_centroid - line.Item1) / line.Item2
                sampleZMat(iLine - 1, iC) = (rgCh8_centroid - laserFit(iC, 0)) / laserFit(iC, 1)
                If Single.IsNaN(sampleZMat(iLine - 1, iC)) Then sampleZMat(iLine - 1, iC) = 0
                'bmp.SetPixel24(iC, xLaserCent, Color.GreenYellow)
#If PRINTFILE Then
                bmp.FillOriginalBluePixel(iC, xLaserCent, 0)
#End If
            Next
#If PRINTFILE Then
            byteToBitmap(bmp.bytes, bmp.Realbmp)
            bmp.Realbmp.Save("c:\temp\" & Form1.Laser_seriesName.Text & iLine.ToString.PadLeft(5, "0") & "Scan" & ".png")
            'bmp.Realbmp.Save("c:\temp\" & Form1.Laser_seriesName.Text & iLine.ToString("D3") & "Scan" & ".png")
#End If

            Form1.Pbar.Increment(1)
        Next
        Form1.CheckBox3.Checked = False
        'MsgBox("Scanning completed")

        'Blure.UpLoad(sampleZMat)
        'Blure.Process_FT_MTF()
        'Blure.DownLoad(sampleZMat)

        BlureBright.UpLoad(brightnessMat)
        BlureBright.Process_FT_MTF()
        BlureBright.DownLoad(brightnessMat)

        Dim fnZ As Integer = FreeFile()
        FileOpen(fnZ, "c:\temp\Zmap_" & Form1.Laser_seriesName.Text & ".txt", OpenMode.Output)
        Dim fnB As Integer = FreeFile()
        FileOpen(fnB, "c:\temp\BrightnessMap_" & Form1.Laser_seriesName.Text & ".txt", OpenMode.Output)
        For iX = 0 To sampleZMat.GetUpperBound(0)
            Dim sZ As String = ""
            Dim sB As String = ""
            For iY As Integer = 0 To sampleZMat.GetUpperBound(1)
                sZ += sampleZMat(iX, iY).ToString & ","
                sB += brightnessMat(iX, iY).ToString & ","
            Next
            PrintLine(fnZ, sZ)
            PrintLine(fnB, sB)
        Next
        FileClose(fnZ)
        FileClose(fnB)

        load_colormap()
        'HeatMapped(brightnessMat, "BrightnessMap_" & Form1.Laser_seriesName.Text)
        Brightnessmap = New FastBitmap(NScanLines, nCol, Imaging.PixelFormat.Format24bppRgb)
        HeatMapped(brightnessMat, "BrightnessMap_" & Form1.Laser_seriesName.Text, Brightnessmap)

        'Dim MaskedFilter As AForge.Imaging.Filters.MaskedFilter
        ''Dim basefiler As Filters.IFilter
        'Dim maskimage As Bitmap
        'MaskedFilter = New Filters.MaskedFilter(New Filters.Sepia(), maskimage)
        'MaskedFilter.ApplyInPlace(Brightnessmap.Realbmp)

        'HeatMapped(sampleZMat, "zMap")
        Heatmap = New FastBitmap(NScanLines, nCol, Imaging.PixelFormat.Format24bppRgb)
        'Dim Heatmap As New FastBitmap(DimX, DimY, Imaging.PixelFormat.Format24bppRgb)
        HeatMapped(sampleZMat, "Zmap_" & Form1.Laser_seriesName.Text, Heatmap)


        'Dim Heatmap As New FastBitmap(DimX, DimY, Imaging.PixelFormat.Format24bppRgb)

        'Heatmap.Reset()
        'Form1.PictureBox0.Image = Heatmap.Realbmp
        'Form1.PictureBox0.Image.RotateFlip(RotateFlipType.Rotate90FlipX)
        'Heatmap.Realbmp.Save("c:\temp\heatmap.jpg")

        Stage.MoveAbsolute(Stage.Xaxe, (laserMiddle + Val(Setting.Gett("LASER_XOFFSET"))), True)
        Form1.Pbar.Value = 0
        MsgBox("Scanning completed")
        'Form1.CheckBox_previewLaserMap.Enabled = True
        'Form1.CheckBox_previewLaserMap.Checked = True
        'Form1.PictureBox_Preview.Image = Heatmap.Realbmp
        Form1.RadioButton2.Enabled = True
        Form1.RadioButton3.Enabled = True
        Form1.RadioButton3.Checked = True
        Form1.PictureBox_Preview.Refresh()
        Form1.CalibrateZoffset_focus.Enabled = True
        IsLaserScanned = True
    End Sub

    Overloads Sub HeatMapped(ByRef zMap(,) As Double, fileName As String)
        Dim DimX As Integer
        DimX = zMap.GetLength(0)
        Heatmap = New FastBitmap(DimX, nCol, Imaging.PixelFormat.Format24bppRgb)
        Dim min, max As Double
        max = Double.MinValue '0.001
        min = Double.MaxValue '0
        For y = 0 To nCol - 1
            For x = 0 To DimX - 1
                If zMap(x, y) <> Double.NaN Then
                    If zMap(x, y) > max Then max = zMap(x, y)
                    If zMap(x, y) < min Then min = zMap(x, y)
                End If
            Next
        Next

        Dim index As Integer
        For y = 0 To nCol - 1
            For x = 0 To DimX - 1
                If Double.IsNaN(zMap(x, y)) Then
                    index = 0
                Else index = (zMap(x, y) - min) / (max - min) * 1000
                End If
                Heatmap.FillOriginalPixel(x, y, rr(index), gg(index), bb(index))
            Next
        Next
        Heatmap.Reset()
        Heatmap.Realbmp.Save("c:\temp\" & fileName & ".jpg")
    End Sub

    Overloads Sub HeatMapped(ByRef zMap(,) As Single, fileName As String, Heatmap As FastBitmap)
        Dim DimX As Integer
        DimX = zMap.GetLength(0)

        Dim min, max As Double
        max = Double.MinValue '0.001
        min = Double.MaxValue '0
        For y = 0 To nCol - 1
            For x = 0 To DimX - 1
                If zMap(x, y) <> Double.NaN Then
                    If zMap(x, y) > max Then max = zMap(x, y)
                    If zMap(x, y) < min Then min = zMap(x, y)
                End If
            Next
        Next

        Dim index As Integer
        For y = 0 To nCol - 1
            For x = 0 To DimX - 1
                If Double.IsNaN(zMap(x, y)) Then
                    index = 0
                Else index = (zMap(x, y) - min) / (max - min) * 1000
                End If
                Heatmap.FillOriginalPixel(x, y, rr(index), gg(index), bb(index))
            Next
        Next
        Heatmap.Reset()
        Heatmap.Realbmp.Save("c:\temp\" & fileName & ".jpg")
    End Sub

    Public Function ConvertCoordinatetoZmapX(XX As Single) As Integer
        Dim P As Integer
        XX = XX - (laserMiddle - XrangeLaser * 0.5)
        P = sampleZMat.GetUpperBound(0) - XX * sampleZMat.GetUpperBound(0) / XrangeLaser
        Return P
    End Function

    Public Function ConvertCoordinatetoZmapY(YY As Single) As Integer
        Dim P As Integer
        YY = Setting.Gett("YMAX") - YY
        P = YY * sampleZMat.GetUpperBound(1) / YrangeLaser
        Return P
    End Function

    Public Function ConvertZmaptoCoordinateZ(ZZ As Single) As Single
        Dim P As Single
        P = Setting.Gett("ZOFFSET") - ZZ + Setting.Gett("LASER_Z_BASE")
        Return P
    End Function

    Public Sub ExtractLaserFOVdata(XX As Single, YY As Single)
        Dim pixX_laser, pixY_laser As Integer
        pixX_laser = ConvertCoordinatetoZmapX(XX)
        pixY_laser = ConvertCoordinatetoZmapY(YY)
        'pixY_laser = LaserScanner.ConvertCoordinatetoZmapY(YY, 36)
        'pixY_laser = LaserScanner.ConvertCoordinatetoZmapY(YY, 36)
        If pixX_laser < 0 Or pixX_laser > sampleZMat.GetUpperBound(0) Or pixY_laser < 0 Or pixY_laser > sampleZMat.GetUpperBound(1) Then
            Form1.ToolStripStatusLabel4.Text = "LaserEstimate: Not available outside the window"
        Else
            ZZ_laser = sampleZMat(pixX_laser, pixY_laser)

            Dim ZZ_min_laserFOV As Single = ZZ_laser
            Dim ZZ_max_laserFOV As Single = ZZ_laser
            Dim Zz_avg_laserFOV As Single = 0
            Dim Brightness_laser As Single = 0
            Dim n_fov As Integer = 0
            Form1.Chart2.Series(0).Points.Clear()
            For ix As Integer = Math.Max(0, pixX_laser - pixX_FOV_laser \ 2) To Math.Min(sampleZMat.GetUpperBound(0), pixX_laser + pixX_FOV_laser \ 2)
                For iy As Integer = Math.Max(0, pixY_laser - pixY_FOV_laser \ 2) To Math.Min(sampleZMat.GetUpperBound(1), pixY_laser + pixY_FOV_laser \ 2)
                    If sampleZMat(ix, iy) < ZZ_min_laserFOV Then
                        ZZ_min_laserFOV = sampleZMat(ix, iy)
                    End If
                    If sampleZMat(ix, iy) > ZZ_max_laserFOV Then
                        ZZ_max_laserFOV = sampleZMat(ix, iy)
                    End If
                    n_fov += 1
                    Zz_avg_laserFOV += sampleZMat(ix, iy)
                    Brightness_laser += brightnessMat(ix, iy)
                    Form1.Chart2.Series(0).Points.AddXY(n_fov, sampleZMat(ix, iy))
                    'Application.DoEvents()
                Next
            Next
            Zz_avg_laserFOV = Zz_avg_laserFOV / n_fov
            BrightnessAvg_laser = Brightness_laser / n_fov
            ZZ_estimate_laser_min = ConvertZmaptoCoordinateZ(ZZ_max_laserFOV)
            ZZ_estimate_laser_max = ConvertZmaptoCoordinateZ(ZZ_min_laserFOV)
            ZZ_estimate_laser_avg = ConvertZmaptoCoordinateZ(Zz_avg_laserFOV)
            Form1.ToolStripStatusLabel4.Text = "Zmin: " + ZZ_estimate_laser_min.ToString + "Zmax: " + ZZ_estimate_laser_max.ToString + " | Brightness: " + BrightnessAvg_laser.ToString + "#Z:" + ZEDOF.Z.ToString
            'Stage.MoveAbsolute(Stage.Zaxe, ZZ_estimate_laser_min - 0.02, False)
            'Form1.ToolStripStatusLabel4.Text = "X:" + pixX_laser.ToString + ",Y:" + pixY_laser.ToString + ",Z Laser: " + ZZ_laser.ToString
        End If
    End Sub

    Public Sub MoveToFOVLaserMin()
        'Dim expandedlaserRangeFOV As Single = (ZZ_estimate_laser_max - ZZ_estimate_laser_min) * rangeFac
        Stage.MoveAbsoluteAsync(Stage.Zaxe, LaserScanner.ZZ_estimate_laser_min + 0.02, False)
    End Sub

    Public Sub MoveToFOVLaserOutOfTissue()
        Stage.MoveAbsoluteAsync(Stage.Zaxe, LaserScanner.ZZ_estimate_laser_avg, False)
    End Sub

    Public Sub MakeMask(Tol_out As Integer, Tol_in As Integer)
        'If IsLaserScanned = True Then

        'Dim brightnessEdge As New Bitmap("c:\temp\BrightnessMap_Calib1Gray.bmp")
        ''PictureBox_Preview.Image = brightnessEdge

        'Dim SobelFilter As AForge.Imaging.Filters.SobelEdgeDetector
        'SobelFilter = New Filters.SobelEdgeDetector()
        'SobelFilter.ApplyInPlace(brightnessEdge)

        'Dim CannyFilter As AForge.Imaging.Filters.CannyEdgeDetector
        'CannyFilter = New Filters.CannyEdgeDetector(20, 50)
        'CannyFilter.GaussianSize = 16
        'CannyFilter.ApplyInPlace(brightnessEdge)

        'Dim PointedColorFloodFill As AForge.Imaging.Filters.PointedColorFloodFill
        'PointedColorFloodFill = New PointedColorFloodFill()
        'PointedColorFloodFill.Tolerance = Color.FromArgb(20, 20, 20)
        'PointedColorFloodFill.FillColor = Color.FromArgb(0, 0, 0)
        'PointedColorFloodFill.StartingPoint = New AForge.IntPoint(50, 50)
        'PointedColorFloodFill.ApplyInPlace(brightnessEdge)

        'PointedColorFloodFill.StartingPoint = New AForge.IntPoint(300, 300)
        'PointedColorFloodFill.FillColor = Color.FromArgb(255, 255, 255)
        'PointedColorFloodFill.Tolerance = Color.FromArgb(17, 17, 17)
        ''PointedColorFloodFill.Tolerance = Color.FromArgb(150, 92, 92)
        'PointedColorFloodFill.ApplyInPlace(brightnessEdge)

        ''Dim FillHoles As AForge.Imaging.Filters.FillHoles
        ''FillHoles = New FillHoles()
        ''FillHoles.MaxHoleHeight = 20
        ''FillHoles.MaxHoleWidth = FillHoles.MaxHoleHeight
        ''FillHoles.ApplyInPlace(brightnessEdge)

        'PictureBox_Preview.Image = brightnessEdge

        Dim brightnessEdgeColor As New Bitmap("c:\temp\BrightnessMap_Calib1.jpg")

        Dim PointedColorFloodFill As AForge.Imaging.Filters.PointedColorFloodFill
        PointedColorFloodFill = New Filters.PointedColorFloodFill()
        'Dim colortol As Integer = 120 '190
        PointedColorFloodFill.Tolerance = Color.FromArgb(Tol_out, Tol_out, Tol_out)
        PointedColorFloodFill.FillColor = Color.FromArgb(0, 0, 0)
        PointedColorFloodFill.StartingPoint = New AForge.IntPoint(25, 150)
        PointedColorFloodFill.ApplyInPlace(brightnessEdgeColor)

        Dim blobFilter As New AForge.Imaging.Filters.BlobsFiltering
        blobFilter = New Filters.BlobsFiltering()
        blobFilter.MinHeight = 60
        blobFilter.MinWidth = blobFilter.MinHeight
        blobFilter.ApplyInPlace(brightnessEdgeColor)

        PointedColorFloodFill.StartingPoint = New AForge.IntPoint(100, 400)
        PointedColorFloodFill.FillColor = Color.White
        'PointedColorFloodFill.FillColor = Color.FromArgb(255, 255, 255)
        'colortol = 200
        PointedColorFloodFill.Tolerance = Color.FromArgb(Tol_in, Tol_in, Tol_in)
        'PointedColorFloodFill.Tolerance = Color.FromArgb(150, 92, 92)
        PointedColorFloodFill.ApplyInPlace(brightnessEdgeColor)

        Dim Grayscale As AForge.Imaging.Filters.Grayscale
        Grayscale = New Filters.Grayscale(0.2125, 0.7154, 0.0721)

        Mask = Grayscale.Apply(brightnessEdgeColor)

        Dim Threshold As AForge.Imaging.Filters.Threshold
        Threshold = New Filters.Threshold(20)
        Threshold.ApplyInPlace(Mask)
        'Dim FillHoles As AForge.Imaging.Filters.FillHoles
        'FillHoles = New FillHoles()
        'FillHoles.MaxHoleHeight = 40
        'FillHoles.MaxHoleWidth = FillHoles.MaxHoleHeight
        'FillHoles.ApplyInPlace(Mask)

        Form1.PictureBox_Preview.Image = Mask
        Mask.Save("c:\temp\Mask.jpg")
        'End If
        IsMaskMade = True
    End Sub

    Public Sub ExtractMaskFOV(XX As Single, YY As Single)
        Dim InTissue As Boolean
        If IsMaskMade Then
            Dim pixX_laser, pixY_laser As Integer
            pixX_laser = ConvertCoordinatetoZmapX(XX)
            pixY_laser = ConvertCoordinatetoZmapY(YY)

            InTissue = False
            If pixX_laser < 0 Or pixX_laser > sampleZMat.GetUpperBound(0) Or pixY_laser < 0 Or pixY_laser > sampleZMat.GetUpperBound(1) Then
                Form1.ToolStripStatusLabel4.Text = "LaserEstimate: Not available outside the window"
            Else

                For ix As Integer = Math.Max(0, pixX_laser - pixX_FOV_laser \ 2) To Math.Min(sampleZMat.GetUpperBound(0), pixX_laser + pixX_FOV_laser \ 2)
                    For iy As Integer = Math.Max(0, pixY_laser - pixY_FOV_laser \ 2) To Math.Min(sampleZMat.GetUpperBound(1), pixY_laser + pixY_FOV_laser \ 2)
                        Dim maskcol As Color = Mask.GetPixel(ix, iy)
                        If maskcol.R = 255 Then
                            InTissue = True
                            GoTo 5
                        End If

                    Next
                Next
            End If
        Else
            InTissue = True
        End If
5:

        If InTissue Then
            Dim laserRangeFOV As Single = (ZZ_estimate_laser_max - ZZ_estimate_laser_min) * (1 + 2 * rangeFac)
            'ZEDOF.Z = 10
            ZEDOF.ChangeRangeANDz(laserRangeFOV)
            LaserScanner.MoveToFOVLaserMin()
        Else
            'ZEDOF.Z = 1
            ZEDOF.ChangeRangeANDzOutOfTissue()
            LaserScanner.MoveToFOVLaserOutOfTissue()
        End If


    End Sub
End Class
