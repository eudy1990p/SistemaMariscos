Public Class CambiarClave
    Private _usuarioID As String = "1"
    Private _clave1 As String
    Private _clave2 As String
    Public Sub setUsuarioID(usuarioID As String)
        Me._usuarioID = usuarioID
    End Sub

    Public Sub cargarData()
        Me._clave1 = Me.TextBox1.Text
        Me._clave2 = Me.TextBox2.Text
    End Sub

    Public Function ValidarCampos() As Boolean
        Me.cargarData()
        Dim respuesta As Boolean = False
        If Me._clave1 = "" Then
            MessageBox.Show("El campo clave esta vacío")
            Me.TextBox1.Focus()
        ElseIf Me._clave2 = "" Then
            MessageBox.Show("El campo repetir clave esta vacío")
            Me.TextBox2.Focus()
        ElseIf Me._clave2 <> Me._clave1 Then
            MessageBox.Show("Las claves no son iguales")
            Me.TextBox1.Focus()
        Else
            respuesta = True
        End If
        Return respuesta
    End Function

    Public Sub cambiarClave()
        If Me.ValidarCampos() Then
            Dim conector As New Conexion()
            Dim resp As Boolean = conector.ActualizarClaveUsuario(Me._clave1, Me._usuarioID)
            conector.Cerrar()
            If resp Then
                Me.Hide()
            End If
        End If
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Hide()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

    End Sub

End Class