Public Class Cliente
    Private _origen As String = "cliente"
    Private _TelefonoArrayList As New ArrayList()
    Private _TipoTelefonoArrayList As New ArrayList()
    Private _EmailArrayList As New ArrayList()

    Private _ArrayClienteID As New ArrayList()

    Private _ID As String
    Private _Nombre As String
    Private _Apellido As String
    Private _Cedula As String
    Private _RNC As String
    Private _Codigo As String
    Private _venta As Ventas
    Private _BuscadorCliente As BuscadorCliente
    Private _usuarioID As String = "1"
    Private _Accion As Integer = 1


    Public Sub setVentas(ventas As Ventas)
        Me._venta = ventas
    End Sub
    Public Sub setBuscadorCliente(buscadorCliente As BuscadorCliente)
        Me._BuscadorCliente = buscadorCliente
    End Sub
    Public Sub setUsuarioID(usuarioID As String)
        Me._usuarioID = usuarioID
    End Sub

    Public Sub setOrigen(origen As String)
        Me._origen = origen
    End Sub

    Private Sub Cliente_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.CargarClientes()
        Me.ComboBox1.SelectedIndex = 0
    End Sub

    Private Sub LinkLabel2_Click(sender As Object, e As EventArgs) Handles LinkLabel2.Click
        Me.asignarEmail()
    End Sub

    Public Sub asignarEmail()
        Dim email As String = Me.TextBox2.Text
        If email = "" Then
            MessageBox.Show("El campo email esta vacío")
            Me.ListBox2.Focus()
        Else
            Me.ListBox2.Items.Add(email)
            Me._EmailArrayList.Add(email)
            Me.TextBox2.Text = ""
        End If
    End Sub
    Public Sub asignarTelefono()
        Dim telefono As String = Me.TextBox1.Text
        If telefono = "" Then
            MessageBox.Show("El campo teléfono esta vacío")
            Me.TextBox1.Focus()
        Else
            Me.ListBox1.Items.Add(telefono)
            Me._TelefonoArrayList.Add(telefono)
            Me._TipoTelefonoArrayList.Add(Me.ComboBox1.SelectedItem.ToString())
            Me.TextBox1.Text = ""
            Me.ComboBox1.SelectedIndex = 0
        End If
    End Sub

    Public Sub cargarData()
        Me._Nombre = Me.TextBox3.Text
        Me._Apellido = Me.TextBox4.Text
        Me._Cedula = Me.TextBox5.Text
        Me._RNC = Me.TextBox6.Text
    End Sub
    Public Sub limpiar()
        Me.TextBox3.Text = ""
        Me.TextBox4.Text = ""
        Me.TextBox5.Text = ""
        Me.TextBox6.Text = ""
        Me.ListBox1.Items.Clear()
        Me.ListBox2.Items.Clear()
        Me._EmailArrayList.Clear()
        Me._TelefonoArrayList.Clear()
        Me._TipoTelefonoArrayList.Clear()
        Me.ComboBox1.SelectedIndex = 0
    End Sub

    Public Function ValidarCampos() As Boolean
        Me.cargarData()
        Dim respuesta As Boolean = False
        If Me._Nombre = "" Then
            MessageBox.Show("El campo nombre esta vacío")
            Me.TextBox3.Focus()
        ElseIf Me._Apellido = "" Then
            MessageBox.Show("El campo apellido esta vacío")
            Me.TextBox4.Focus()
        Else
            respuesta = True
        End If
        Return respuesta
    End Function

    Public Sub insertarCliente()
        If Me.ValidarCampos() Then
            Dim conector As New Conexion()
            Dim clienteID As Integer = conector.InsertarCliente(Me._Nombre, Me._Apellido, Me._Cedula, Me._RNC, Me._usuarioID)
            conector.Cerrar()
            If clienteID > 0 Then
                If Me._TelefonoArrayList.Count > 0 Then
                    Me.insertarTelefono()
                End If
                If Me._EmailArrayList.Count > 0 Then
                    Me.insertarEmail()
                End If

                If Me._origen.Equals("ventas") Or Me._origen.Equals("reparaciones") Then
                    If Me._origen.Equals("ventas") Then
                        Me._BuscadorCliente.getVentas().setCliente(clienteID.ToString(), Me._Nombre + " " + Me._Apellido, Me._Cedula, Me._RNC)
                    ElseIf Me._origen.Equals("reparaciones") Then
                        Me._BuscadorCliente.getReparaciones().setCliente(clienteID.ToString(), Me._Nombre + " " + Me._Apellido, Me._Cedula, Me._RNC)
                    End If
                    Me._BuscadorCliente.Hide()
                    Me.Hide()
                Else
                    MessageBox.Show("Se agrego el cliente")
                    Me.CargarClientes()
                    Me.limpiar()
                End If
            End If
        End If
    End Sub

    Public Sub EditarCliente()
        If Me.ValidarCampos() Then
            Dim conector As New Conexion()
            conector.ActualizarCliente(Me._Nombre, Me._Apellido, Me._Cedula, Me._RNC, Me._ID)
            conector.Cerrar()
            'MessageBox.Show("Se edito el cliente")
            Me.CargarClientes()
            Me.limpiar()
            Me._Accion = 1
            Me.validadAccion()
        End If
    End Sub

    Public Sub insertarEmail()
        Dim conector As New Conexion()
        For i As Integer = 0 To Me._EmailArrayList.Count - 1 Step 1
            conector.InsertarClienteEmail(Me._EmailArrayList(i).ToString(), Me._usuarioID)
        Next
        conector.Cerrar()
    End Sub
    Public Sub insertarTelefono()
        Dim conector As New Conexion()
        For i As Integer = 0 To Me._TelefonoArrayList.Count - 1 Step 1
            conector.InsertarClienteTelefono(Me._TelefonoArrayList(i).ToString(), Me._TipoTelefonoArrayList(i).ToString(), Me._usuarioID)
        Next
        conector.Cerrar()
    End Sub
    Public Sub CargarClientes()
        Dim conector As New Conexion()
        conector.MostrarClientes(Me.DataGridView1, "  visible = 1 ")
        Me._ArrayClienteID = conector.getResultArrayList()
        conector.Cerrar()
    End Sub

    Private Sub LinkLabel1_Click(sender As Object, e As EventArgs) Handles LinkLabel1.Click
        Me.asignarTelefono()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Me._Accion = 1 Then
            Me.insertarCliente()
        ElseIf Me._Accion = 2 Then
            Me.EditarCliente()
        End If
    End Sub

    Public Sub Eliminar()
        If MessageBox.Show("Seguro que desea eliminar el cliente?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            If Me.DataGridView1.SelectedRows.Count > 0 Or Me.DataGridView1.SelectedCells.Count > 0 Then
                Dim index As Integer = Me.DataGridView1.CurrentRow.Index

                Dim id As String = Me._ArrayClienteID(index).ToString()
                Dim conector As New Conexion()
                conector.EliminarCliente(id)
                conector.Cerrar()
                Me._Accion = 1
                Me.validadAccion()
                Me.CargarClientes()
            Else
                MessageBox.Show("No hay selección ")
            End If
        End If


    End Sub

    Public Sub ObtenerDatosClienteEditar()
        If Me.DataGridView1.SelectedRows.Count > 0 Or Me.DataGridView1.SelectedCells.Count > 0 Then
            Dim index As Integer = Me.DataGridView1.CurrentRow.Index
            Me._ID = Me._ArrayClienteID(index).ToString()

            Me.TextBox3.Text = Me.DataGridView1.Rows(index).Cells(1).Value.ToString()
            Me.TextBox4.Text = Me.DataGridView1.Rows(index).Cells(2).Value.ToString()
            Me.TextBox5.Text = Me.DataGridView1.Rows(index).Cells(3).Value.ToString()
            Me.TextBox6.Text = Me.DataGridView1.Rows(index).Cells(4).Value.ToString()
            Me._Accion = 2
            Me.validadAccion()
        Else
            MessageBox.Show("No hay selección ")
        End If
    End Sub
    Public Sub validadAccion()
        Select Case Me._Accion
            Case 1
                Me.Button1.Text = "Guardar"
            Case 2
                Me.Button1.Text = "Editar"
        End Select
    End Sub

    Private Sub EditarClienteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EditarClienteToolStripMenuItem.Click
        Me.ObtenerDatosClienteEditar()
    End Sub

    Private Sub EliminarClienteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EliminarClienteToolStripMenuItem.Click
        Me.Eliminar()
    End Sub
End Class