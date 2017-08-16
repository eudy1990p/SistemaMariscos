Public Class Equipos

    Dim objInventario As Inventario
    Private _usuarioID As String = "1"

    Private Sub Equipos_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Public Sub setObjIventario(ObjInventario As Inventario)
        Me.objInventario = ObjInventario
    End Sub
    Public Sub setUsuarioID(usuarioID As String)
        Me._usuarioID = usuarioID
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.insertarEquipo()
    End Sub
    Public Sub insertarEquipo()

        Dim marca As String = Me.TextBox1.Text
        Dim modelo As String = Me.TextBox2.Text
        Dim precio As String = Me.TextBox3.Text


        If precio = "" Then
            precio = "0.00"
        End If

        If marca = "" Then
            MessageBox.Show("El campo marca esta vacío ")
            Me.TextBox3.Focus()
        ElseIf modelo = "" Then
            MessageBox.Show("El campo modelo esta vacío ")
            Me.TextBox3.Focus()
        Else
            Dim conector As New Conexion()
            conector.InsertarEquipos(marca, modelo, precio, Me._usuarioID)
            conector.Cerrar()
            Me.objInventario.cargarComboBox()
            Me.objInventario.CargarGridViewEquipos()
            Me.Hide()
        End If
    End Sub
End Class