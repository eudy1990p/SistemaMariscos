Public Class Login
    Private clave As String
    Private usuario As String
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.loguearUsuario()
    End Sub
    Public Sub loguearUsuario()
        If Me.ValidarDatos() Then
            Dim DatosUsuario() As String
            Dim conector As New Conexion()
            DatosUsuario = conector.whereUsuario(" usuario = '" + Me.usuario + "' and clave = '" + Me.clave + "'  and visible = 1 ")
            conector.Cerrar()
            If DatosUsuario(0) <> "" Then
                Dim main As New Form1()
                main.setDataUsuario(DatosUsuario(1), DatosUsuario(0), DatosUsuario(2))
                main.Show()
                Me.Hide()
            Else
                MessageBox.Show("Usuario o clave incorrecta ")
            End If
        End If
    End Sub
    Public Function ValidarDatos() As Boolean
        Dim todoBien As Boolean = False
        Me.cargarData()
        If usuario = "" Then
            MessageBox.Show("El campo usuario esta vacío ")
            Me.TextBox1.Focus()
        ElseIf clave = "" Then
            MessageBox.Show("El campo clave esta vacío ")
            Me.TextBox2.Focus()
        Else
            todoBien = True
        End If
        Return todoBien
    End Function
    Public Sub cargarData()
        Me.clave = Me.TextBox2.Text
        Me.usuario = Me.TextBox1.Text
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Application.Exit()
    End Sub

    Private Sub TextBox2_MouseUp(sender As Object, e As MouseEventArgs) Handles TextBox2.MouseUp

        'Me.loguearUsuario()

    End Sub

    Private Sub TextBox1_KeyUp(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyUp
        Me.PresioneEnter(e)
    End Sub

    Private Sub TextBox2_KeyUp(sender As Object, e As KeyEventArgs) Handles TextBox2.KeyUp
        Me.PresioneEnter(e)
    End Sub
    Public Sub PresioneEnter(e As KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
            Me.loguearUsuario()
        End If
    End Sub

    Private Sub Login_Load(sender As Object, e As EventArgs) Handles MyBase.Load



    End Sub
End Class