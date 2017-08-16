Public Class Usuarios

    Private _usuarioID As String = "1"
    Dim _tipo As String
    Dim _usuario As String
    Dim _clave As String
    Dim _registroID As String

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.GuardarDatos()
    End Sub
    Public Sub setUsuarioID(usuarioID As String)
        Me._usuarioID = usuarioID
    End Sub

    Public Sub GuardarDatos()
        Dim tipo As String = Me.ComboBox2.SelectedItem.ToString()
        Dim usuario As String = Me.TextBox2.Text
        Dim clave As String = Me.TextBox3.Text

        If clave = "" Then
            MessageBox.Show("El campo clave esta vacío ")
            Me.TextBox3.Focus()
        ElseIf usuario = "" Then
            MessageBox.Show("El campo usuario esta vacío ")
            Me.TextBox3.Focus()
        ElseIf tipo = "" Then
            MessageBox.Show("El campo tipo esta vacío ")

        Else
            Dim conector As New Conexion()
            conector.InsertarUsuario(usuario, clave, tipo.ToLower(), Me._usuarioID)
            conector.Cerrar()
            Me.MostrarUsuarios("  visible = 1 ")
            Me.limpiar()
        End If

    End Sub

    Private Sub Usuarios_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MostrarUsuarios("  visible = 1 ")
        Me.ocultarBotones()
    End Sub

    Private Sub DataGridView1_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
        Me.CargarDatosUsuario()
        Me.mostrarBotones()
    End Sub

    Public Sub MostrarUsuarios(where As String)
        Dim conector As New Conexion()
        conector.MostrarUsuarios(Me.DataGridView1, where)
        conector.Cerrar()
    End Sub
    Public Sub CargarDatosUsuario()
        Dim fila As Integer = DataGridView1.CurrentRow.Index
        Dim id As String = DataGridView1.Rows(fila).Cells(0).Value
        'MessageBox.Show(id)
        Dim respuesta() As String
        Dim conector As New Conexion()
        respuesta = conector.ObtenerDataUsuario(id)
        Me.TextBox2.Text = respuesta(1)
        Me.ComboBox2.SelectedItem = respuesta(2).ToUpper
        Me.TextBox3.Text = respuesta(3)
        Me._registroID = respuesta(0)
        'MessageBox.Show(respuesta(0) + " " + respuesta(1) + " " + respuesta(2))
        conector.Cerrar()
    End Sub
    Public Sub ActualizarDatosUsuario()
        If Me.ValidarDatos Then
            Dim conector As New Conexion()
            conector.ActualizarUsuario(Me._usuario, Me._clave, Me._tipo, Me._registroID)
            conector.Cerrar()
            Me.limpiar()
            Me.MostrarUsuarios("  visible = 1 ")
        End If
    End Sub
    Public Sub EliminarUsuario()

        Dim conector As New Conexion()
        conector.EliminarUsuario(Me._registroID)
        conector.Cerrar()
            Me.limpiar()
        Me.MostrarUsuarios("  visible = 1 ")
    End Sub
    Public Function ValidarDatos() As Boolean
        Dim todoBien As Boolean = False
        Me.CargarAtributos()
        If Me._clave = "" Then
            MessageBox.Show("El campo clave esta vacío ")
            Me.TextBox3.Focus()
        ElseIf Me._usuario = "" Then
            MessageBox.Show("El campo usuario esta vacío ")
            Me.TextBox3.Focus()
        ElseIf Me._tipo = "" Then
            MessageBox.Show("El campo tipo esta vacío ")
        Else
            todoBien = True
        End If
        Return todoBien
    End Function

    Public Sub CargarAtributos()
        Me._tipo = Me.ComboBox2.SelectedItem.ToString()
        Me._usuario = Me.TextBox2.Text
        Me._clave = Me.TextBox3.Text
    End Sub
    Public Sub limpiar()
        Me.ComboBox2.SelectedIndex = 0
        Me.TextBox2.Text = ""
        Me.TextBox3.Text = ""
    End Sub

    Private Sub TextBox4_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.ActualizarDatosUsuario()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Me.EliminarUsuario()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Me.MostrarUsuarios(" usuario like '%" + Me.TextBox4.Text + "%' and  visible = 1 ")
    End Sub

    Public Sub ocultarBotones()
        Me.Button2.Visible = False
        Me.Button3.Visible = False
        Me.Button4.Visible = False
        Me.Button1.Visible = True
        Me.limpiar()
    End Sub
    Public Sub mostrarBotones()
        Me.Button2.Visible = True
        Me.Button3.Visible = True
        Me.Button4.Visible = True
        Me.Button1.Visible = False
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.ocultarBotones()
    End Sub


    Private Sub TextBox4_KeyUp(sender As Object, e As KeyEventArgs) Handles TextBox4.KeyUp

        Me.MostrarUsuarios(" usuario like '%" + Me.TextBox4.Text + "%' and  visible = 1 ")
    End Sub
End Class