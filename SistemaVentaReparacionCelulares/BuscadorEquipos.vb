Public Class BuscadorEquipos
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
        Me.MostrarEquipos()
    End Sub

    Private Sub BuscadorEquipos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MostrarEquipos()
    End Sub

    Public Sub MostrarEquipos()
        Dim palabra As String = Me.TextBox1.Text
        Dim conector As New Conexion()
        conector.MostrarEquiposDataGridView(Me.DataGridView1, " concat(id,' ',marca,' ',modelo,' ',precio_venta   ) like '%" + palabra + "%'  and visible = 1 ")
        conector.Cerrar()
    End Sub

    Private Sub DataGridView1_CellContentDoubleClick(sender As Object, e As DataGridViewCellEventArgs)
        Me.asignarDatos()
    End Sub
    Public Sub asignarDatos()
        Dim fila As Integer = Me.DataGridView1.CurrentRow.Index
        Dim id As String = Me.DataGridView1.Rows(fila).Cells(0).Value.ToString()
        Dim marca As String = Me.DataGridView1.Rows(fila).Cells(1).Value.ToString()
        Dim modelo As String = Me.DataGridView1.Rows(fila).Cells(2).Value.ToString()
        Dim precio As String = Me.DataGridView1.Rows(fila).Cells(4).Value.ToString()
        Dim cantidad As Integer = Integer.Parse(Me.DataGridView1.Rows(fila).Cells(3).Value.ToString())
        If Me._Origen.Equals("ventas") Then
            Me._ventas.setProducto(id, marca + " " + modelo, precio, cantidad)
        ElseIf Me._Origen.Equals("reparaciones") Then
            Me._Reparaciones.setProducto(id, marca + " " + modelo, precio, cantidad)
        End If
        Me.Hide()

    End Sub

    Private Sub DataGridView1_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
        Me.asignarDatos()
    End Sub

    Private Sub LinkLabel2_Click(sender As Object, e As EventArgs) Handles LinkLabel2.Click
        Me.AgregarProducto()

    End Sub
    Public Sub AgregarProducto()
        Dim inventario As New Inventario()
        If Me._Origen.Equals("ventas") Then
            inventario.setOrigen("ventas")
        ElseIf Me._Origen.Equals("reparaciones") Then
            inventario.setOrigen("reparaciones")
        End If
        inventario.setBuscadorEquipos(Me)
        inventario.setUsuarioID(Me._usuarioID)
        inventario.Show()
    End Sub
End Class