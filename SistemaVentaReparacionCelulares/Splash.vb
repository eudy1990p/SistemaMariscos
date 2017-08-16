Imports Microsoft.VisualBasic.DateAndTime


Public Class Splash
    Private fecha As String = DateAndTime.Now.ToString()

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Me.ProgressBar1.Step += 1
        Me.ProgressBar1.Value = Me.ProgressBar1.Step
        If Me.ProgressBar1.Step >= 100 Then
            Me.Timer1.Stop()
            Dim main As New Login
            main.Show()
            Me.Hide()
        End If
    End Sub

    Private Sub Splash_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Timer1.Start()
        'MsgBox(fecha)
        'Dim c As New Conexion
        'c.InsertarUsuario("prueba1", "prueba1", "cajero", "1")
        'c.Cerrar()
    End Sub
End Class