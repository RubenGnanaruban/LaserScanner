Imports Zaber.Motion
Imports Zaber.Motion.Binary

Public Class ZaberNew

    Dim Com As Object
    Public Elapsedtime As Long
    Dim Watch As New Stopwatch
    Public Xpos, Ypos, Zpos As Single
    Public Xacc, Yacc, Zacc As Single
    Public Xspeed, Yspeed, Zspeed As Single
    Public xp, yp As Single

    Public SweptZ As Single
    Public Xaxe, Yaxe, Zaxe As Device

    Public Sub New()
        Dim com As Connection = Connection.OpenSerialPort("COM9")
        Dim Devicelist = com.DetectDevices()
        Xaxe = Devicelist(2)
        Yaxe = Devicelist(1)
        Zaxe = Devicelist(0)

        Home()

        MoveAbsolute(Xaxe, 12.5, True)
        MoveAbsolute(Yaxe, 3.5, True)
        MoveAbsolute(Zaxe, 17.0, True)
        'MoveAbsolute(Xaxe, 25.4 / 2, False)
        'MoveAbsolute(Xaxe, 37.6, True)
    End Sub



    Public Sub Home()
        Zaxe.Home()
        Xaxe.Home()
        Yaxe.Home()


        Xspeed = 65
        Yspeed = 25
        Zspeed = 10


        SetSpeed(Xaxe, Xspeed)
        SetSpeed(Yaxe, Yspeed)
        SetSpeed(Zaxe, Zspeed)

        Xacc = 300
        Yacc = 300
        Zacc = 50

        SetAcceleration(Xaxe, Xacc)
        SetAcceleration(Yaxe, Yacc)
        SetAcceleration(Zaxe, Zacc)


    End Sub
    Public Sub MoveRelative(ByRef Axis As Device, R As Single, Optional update As Boolean = True)

        Try

            Axis.MoveRelative(R, Units.Length_Millimetres)
            If update Then
                UpdatePositions()

            End If
        Catch ex As Exception

        End Try

    End Sub

    Public Sub MoveRelativeAsync(ByRef Axis As Device, R As Single, Optional update As Boolean = True)
        Axis.MoveRelativeAsync(R, Units.Length_Millimetres)
        If update Then
            UpdatePositions()

        Else

        End If
    End Sub
    Public Sub MoveAbsolute(ByRef Axis As Device, R As Single, Optional update As Boolean = True)
        Try
            Axis.MoveAbsolute(R, Units.Length_Millimetres)
            If update Then
                UpdatePositions()

            End If
        Catch ex As Exception

        End Try

    End Sub

    Public Sub MoveAbsoluteAsync(ByRef Axis As Device, R As Single)
        Try
            Axis.MoveAbsoluteAsync(R, Units.Length_Millimetres)
            UpdatePositions()

        Catch ex As Exception

        End Try

    End Sub
    Public Sub SetSpeed(ByRef Axis As Device, S As Single)
        Try
            Axis.GenericCommandWithUnits(42, S, Units.Velocity_MillimetresPerSecond, Units.Velocity_MillimetresPerSecond, 100)
        Catch ex As Exception

        End Try

    End Sub

    Public Sub SetAcceleration(ByRef Axis As Device, A As Integer)
        Axis.GenericCommand(43, A, 100, True)
    End Sub

    Public Function GetPosition(ByRef Axis As Device) As Single
        Return Axis.GetPosition(Units.Length_Millimetres)
    End Function

    Public Sub StorePosition(ByRef Axis As Device, position As Integer)
        Axis.GenericCommand(16, position, 0, True)
    End Sub






    Public Sub UpdatePositions()
        Xpos = Xaxe.GetPosition(Units.Length_Millimetres)
        Ypos = Yaxe.GetPosition(Units.Length_Millimetres)
        Zpos = Zaxe.GetPosition(Units.Length_Millimetres)
    End Sub

    Public Sub UpdateZPositions()
        Zpos = Zaxe.GetPosition(Units.Length_Millimetres)
    End Sub

    Public Sub Go_Middle()
        MoveAbsolute(Xaxe, 12.7)
        MoveAbsolute(Yaxe, 38)
    End Sub

    Public Sub SetSweptZ(SweptZ As Single)
        Me.SweptZ = SweptZ
    End Sub

    Public Sub MoveSweptZ()
        Zaxe.MoveRelative(SweptZ, Units.Length_Millimetres)

    End Sub
End Class
