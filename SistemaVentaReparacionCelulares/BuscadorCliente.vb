Public Class BuscadorCliente
    Private _Origen As String = "ventas"
    Private _ventas As Ventas
    Private _Reparaciones As Reparaciones
    Private _usuarioID As String = "1"
    Public Sub setReparaciones(reparaciones As Reparaciones)
        Me._Reparaciones = reparaciones
    End Sub
    Public Sub setVentas(ventas As Ventas)
        Me._ventas = ventas
    End Sub
    Public Function getVentas() As Ventas
        Return Me._ventas
    End Function
    Public Function getReparaciones() As Reparaciones
        Return Me._Reparaciones
    End Function
    Public Sub setUsuarioID(usuarioID As String)
        Me._usuarioID = usuarioID
    End Sub
    Public Sub setOrigen(origen As String)
        Me._Origen = origen
    End Sub

    Private Sub TextBox1_KeyUp(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyUp
        Me.MostrarClientes()
    End Sub

    Private Sub BuscadorEquipos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MostrarClientes()
    End Sub

    Public Sub MostrarClientes()
        Dim palabra As String = Me.TextBox1.Text
        Dim conector As New Conexion()
        conector.MostrarClientes(Me.DataGridView1, " concat(id,' ',nombre,' ',apellido,' ',cedula,' ',rnc ) like '%" + palabra + "%'  and visible = 1 ")
        conector.Cerrar()
    End Sub

    Private Sub DataGridView1_CellContentDoubleClick(sender As Object, e As DataGridViewCellEventArgs)
        Me.asignarDatos()
    End Sub
    Public Sub asignarDatos()
        Dim fila As Integer = Me.DataGridView1.CurrentRow.Index
        Dim id As String = Me.DataGridView1.Rows(fila).Cells(0).Value.ToString()
        Dim nombre As String = Me.DataGridView1.Rows(fila).Cells(1).Value.ToString()
        Dim apellido As String = Me.DataGridView1.Rows(fila).Cells(2).Value.ToString()
        Dim cedula As String = Me.DataGridView1.Rows(fila).Cells(3).Value.ToString()
        Dim rnc As String = Me.DataGridView1.Rows(fila).Cells(4).Value.ToString()
        If Me._Origen.Equals("ventas") Then
            Me._ventas.setCliente(id, nombre + " " + apellido, cedula, rnc)
        ElseIf Me._Origen.Equals("reparaciones") Then
            Me._Reparaciones.setCliente(id, nombre + " " + apellido, cedula, rnc)
        End If
        Me.Hide()

    End Sub
    Public Sub asignarDatosVentas(id As String, nombre As String, apellido As String, cedula As String, rnc As String)
        ' Me._ventas.setCliente(id, nombre + " " + apellido, cedula, rnc)
        If Me._Origen.Equals("ventas") Then
            Me._ventas.setCliente(id, nombre + " " + apellido, cedula, rnc)
        ElseIf Me._Origen.Equals("reparaciones") Then
            Me._Reparaciones.setCliente(id, nombre + " " + apellido, cedula, rnc)
        End If
        Me.Hide()

    End Sub

    Private Sub DataGridView1_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
        Me.asignarDatos()
    End Sub

    Private Sub LinkLabel2_Click(sender As Object, e As EventArgs) Handles LinkLabel2.Click
        Dim cliente As New Cliente()
        If Me._Origen.Equals("ventas") Then
            cliente.setOrigen("ventas")
        ElseIf Me._Origen.Equals("reparaciones") Then
            cliente.setOrigen("reparaciones")
        End If
        cliente.setBuscadorCliente(Me)
        cliente.setUsuarioID(Me._usuarioID)
        cliente.Show()
    End Sub

    Private Sub LinkLabel2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked

    End Sub
End Class