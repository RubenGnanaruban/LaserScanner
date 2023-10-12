Imports System.ComponentModel
Imports AForge.Imaging
Imports AForge.Video.DirectShow
Imports System.IO

Public Class FormLaser
    Dim vidDev As Integer = 1

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'load_colormap()
        'stage = New ZaberNew()
        'Dim videoDevices As New FilterInfoCollection(FilterCategory.VideoInputDevice)
        'Dim videoSource As New VideoCaptureDevice(videoDevices(vidDev).MonikerString)
        'videoSource.VideoResolution = videoSource.VideoCapabilities(10) '10: 1920*1080
        'videoSource.SnapshotResolution = videoSource.VideoCapabilities(10)
        'VideoSourcePlayer1.VideoSource = videoSource
        ''Dim minFocus, maxFocus, stepFocus, defFocus As Integer
        ''videoSource.GetCameraPropertyRange(CameraControlProperty.Focus, minFocus, maxFocus, stepFocus, defFocus, CameraControlFlags.Manual)
        ''Focus ranges from 0 to 40 in integer steps
        ''videoSource.GetCameraPropertyRange(CameraControlProperty.Exposure, minFocus, maxFocus, stepFocus, defFocus, CameraControlFlags.Manual)
        ''Zoom ranges from 0 to 317 in integer steps
        ''Exposure ranges from -13 to 0 in integer steps
        'videoSource.SetCameraProperty(CameraControlProperty.Focus, NumericUpDownFocus.Value, CameraControlFlags.Manual)
        'videoSource.SetCameraProperty(CameraControlProperty.Exposure, NumericUpDownExposure.Value, CameraControlFlags.Manual)
        'videoSource.Start()
        'VideoSourcePlayer1.Start()
    End Sub

    Private Sub Form1_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        Application.Exit()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim laserCal As New LaserZScan
        laserCal.DisplayValue.Text = "Loading ..."
        laserCal.Show()
        laserCal.DisplayValue.Text = "Ready"
    End Sub
End Class