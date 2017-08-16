Public Class Inventario
    Private _usuarioID As String = "1"
    Private _equipo As String
    Private _cantidad As String
    Private _precioCompra As String
    Private _precioVenta As String
    Private _NombreSuplidor As String
    Private _EmailSuplidor As String
    Private _TelefonoSuplidor As String
    Private _ArrayListIDEquipos As ArrayList
    Private _BuscadorEquipos As BuscadorEquipos
    Private _venta As Ventas
    Private _origen As String = "inventario"

    Public Sub setVentas(ventas As Ventas)
        Me._venta = ventas
    End Sub
    Public Sub setBuscadorEquipos(buscadorEquipos As BuscadorEquipos)
        Me._BuscadorEquipos = buscadorEquipos
    End Sub
    Public Sub setOrigen(origen As String)
        Me._origen = origen
    End Sub

    Public Sub setUsuarioID(usuarioID As String)
        Me._usuarioID = usuarioID
    End Sub

    Public Sub cargarComboBox()
        Dim conector As New Conexion()
        conector.MostrarEquiposCombo(Me.ComboBox1)
        conector.Cerrar()
        Me._ArrayListIDEquipos = conector.getResultArrayList()
        '''<summary>
        '''For i As Integer = 0 To Me._ArrayListIDEquipos.Count - 1 Step 1
        '''    MessageBox.Show(i.ToString() + " " + Me._ArrayListIDEquipos(i))
        '''Next
        '''</summary>



    End Sub

    'Public Sub insertarCompra()
    'If Me.ValidarCampos() Then
    'Dim conector As New Conexion()
    'Dim clienteID As Integer = conector.InsertarCliente(Me._Nombre, Me._Apellido, Me._Cedula, Me._RNC, Me._usuarioID)
    'conector.Cerrar()
    'If clienteID > 0 Then
    'If Me._TelefonoArrayList.Count > 0 Then
    'Me.insertarTelefono()
    'End If
    'If Me._EmailArrayList.Count > 0 Then
    'Me.insertarEmail()
    'End If
    'End If
    'End If
    'End Sub

    Private Sub Inventario_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.cargarComboBox()
        Me.CargarGridViewEquipos()
        Me.CargarGridViewCompras()
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked

    End Sub

    Private Sub LinkLabel1_Click(sender As Object, e As EventArgs) Handles LinkLabel1.Click
        Dim equipo As New Equipos()
        equipo.setObjIventario(Me)
        equipo.setUsuarioID(Me._usuarioID)
        equipo.Show()
    End Sub

    Public Sub CargarData()
        Me._equipo = Me.ComboBox1.SelectedIndex()
        Me._cantidad = Me.TextBox1.Text
        Me._precioCompra = Me.TextBox2.Text
        Me._precioVenta = Me.TextBox3.Text
        Me._NombreSuplidor = Me.TextBox4.Text
        Me._EmailSuplidor = Me.TextBox5.Text
        Me._TelefonoSuplidor = Me.TextBox6.Text
    End Sub
    Public Sub Limpiar()
        Me.ComboBox1.SelectedIndex = 0
        Me.TextBox1.Text = ""
        Me.TextBox2.Text = ""
        Me.TextBox3.Text = ""
        Me.TextBox4.Text = ""
        Me.TextBox5.Text = ""
        Me.TextBox6.Text = ""
    End Sub

    Public Sub insertarCompra()
        Me.CargarData()

        If Me._equipo = "" Then
            MessageBox.Show("El campo equipo esta vacío ")
        ElseIf Me._cantidad = "" Then
            MessageBox.Show("El campo cantidad esta vacío ")
            Me.TextBox1.Focus()
        ElseIf Me._precioCompra = "" Then
            MessageBox.Show("El campo precio compra esta vacío ")
            Me.TextBox2.Focus()
        Else
            Dim conector As New Conexion()
            Dim MontoAP As Double = Integer.Parse(Me._cantidad) * Integer.Parse(Me._precioCompra)
            Dim idProducto As Integer = Me._ArrayListIDEquipos(Integer.Parse(Me._equipo))
            If Me._precioVenta = "" Then
                Me._precioVenta = "0"
            End If

            Dim idCompra As Integer = conector.InsertarCompra(Me._ArrayListIDEquipos(Integer.Parse(Me._equipo)).ToString(), Me._cantidad, Me._precioCompra, Me._precioVenta, MontoAP.ToString(), Me._NombreSuplidor, Me._EmailSuplidor, Me._TelefonoSuplidor, Me._usuarioID)
            conector.Cerrar()

            If Me._origen.Equals("ventas") Or Me._origen.Equals("reparaciones") Then
                Dim conectorG As New Conexion()
                Dim DatoEquipo() As String = conectorG.ObtenerDataEquipo(idProducto)
                conectorG.Cerrar()
                If Not (DatoEquipo(0) = "") Then
                    If Me._origen.Equals("ventas") Then
                        Me._BuscadorEquipos.getVentas().setProducto(DatoEquipo(0), DatoEquipo(1) + " " + DatoEquipo(2), DatoEquipo(4), Integer.Parse(DatoEquipo(3)))
                    ElseIf Me._origen.Equals("reparaciones") Then
                        Me._BuscadorEquipos.getReparaciones().setProducto(DatoEquipo(0), DatoEquipo(1) + " " + DatoEquipo(2), DatoEquipo(4), Integer.Parse(DatoEquipo(3)))
                    End If
                    Me._BuscadorEquipos.Hide()
                    Me.Hide()
                Else
                    MessageBox.Show("Hay un error obteniendo los datos del equipo")
                End If
            Else
                    Me.Limpiar()
                Me.CargarGridViewEquipos()
                Me.CargarGridViewCompras()
                MessageBox.Show("Se agrego la compra")
            End If

            'MessageBox.Show(Me._ArrayListIDEquipos(Integer.Parse(Me._equipo)).ToString())

            'Me.MostrarUsuarios(" 1 ")
            'Me.Limpiar()
            'Me.CargarGridViewEquipos()
            'MessageBox.Show("Se agrego la compra")
        End If
    End Sub
    Public Sub CargarGridViewEquipos()
        Dim conector As New Conexion()
        conector.MostrarEquiposDataGridView(Me.DataGridView1, " visible = 1 ")
        conector.Cerrar()

    End Sub
    Public Sub CargarGridViewCompras()
        Dim conector As New Conexion()
        conector.MostrarComprasDataGridView(Me.DataGridView2, " c.visible = 1 ")
        conector.Cerrar()

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.insertarCompra()

    End Sub
End Class