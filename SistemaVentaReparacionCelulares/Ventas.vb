Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Public Class Ventas

    Public Sub ReIniciar()
        Me.TextBox1.Text = ""
        Me.TextBox2.Text = ""
        Me.TextBox3.Text = ""
        Me.TextBox4.Text = ""
        Me.TextBox5.Text = ""
        Me.TextBox6.Text = ""
        Me.TextBox7.Text = ""
        Me.TextBox8.Text = ""
        Me.ArrayListProductos.Clear()

        _Sub_Total = 0.00
        _CodigoProducto = 0.00
        _TotalSubTotal = 0.00
        _TotalITBISTotal = 0.00
        _TotalMontoTotal = 0.00
        _MontoPago = 0.00
        _DevueltaPago = 0.00
        _TipoPago = "efectivo"
        _cantidadDisponible = 0
        _ClienteID = ""
        _ClienteNombreComple = ""
        _ClienteCedula = ""
        _ClienteRNC = ""
        _facturaID = ""
        _facturaFecha = ""
        Me.LbMontoItbis.Text = Me._TotalITBISTotal.ToString()
        Me.LbSubTotal.Text = Me._TotalSubTotal.ToString()
        Me.LbMontoAPagar.Text = Me._TotalMontoTotal.ToString()
        Me.LbMontoPagado.Text = Me._MontoPago.ToString()
        Me.DataGridView1.Rows.Clear()
    End Sub

    Private ArrayListProductos As New ArrayList()
    Private ArrayListProductosCalcular As New ArrayList()

    Private _Sub_Total As Double
    Private _CodigoProducto As String
    Private _TotalSubTotal As Double = 0.00
    Private _TotalITBISTotal As Double = 0.00
    Private _TotalMontoTotal As Double = 0.00
    Private _MontoPago As Double = 0.00
    Private _DevueltaPago As Double = 0.00
    Private _TipoPago As String = "efectivo"
    Private _cantidadDisponible As Integer
    Private _ClienteID As String
    Private _ClienteNombreComple As String
    Private _ClienteCedula As String
    Private _ClienteRNC As String
    Private _usuarioID As String = "1"
    Private _facturaID As String = ""
    Private _facturaFecha As String = ""


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Me._MontoPago > 0 Then
            Me.VerReporte(True)
        Else
            MessageBox.Show("No se puede generar la factura sin haber hecho el pago")
        End If

    End Sub
    Public Sub setUsuarioID(usuarioID As String)
        Me._usuarioID = usuarioID
    End Sub

    Public Sub insertarFacturaVenta()
        Dim conector As New Conexion()
        Dim facturaID As Integer = conector.InsertarFacturaVenta(Me._TotalSubTotal, Me._TotalITBISTotal, Me._TotalMontoTotal, Me._ClienteID, Me._usuarioID)
        conector.Cerrar()
        If facturaID > 0 Then
            Me.insertarFacturaVentaDetalle(facturaID.ToString())
            If Me._MontoPago > 0 Then
                Me.insertarPago(facturaID.ToString())
            End If
            Me.obtenerFacturaVenta(facturaID)
        End If
    End Sub
    Public Sub insertarFacturaVentaDetalle(facturaID As String)
        Dim conector As New Conexion()
        For i As Integer = 0 To Me.ArrayListProductos.Count - 1 Step 1
            Dim productos As ModeloVentaReporte = Me.ArrayListProductos(i)
            conector.InsertarFacturaVentaDetalle(facturaID, productos.Codigo, productos.Cantidad, productos.Total, productos.Precio, Me._usuarioID)
        Next
        conector.Cerrar()
    End Sub
    Public Sub insertarPago(facturaID As String)
        Dim conector As New Conexion()
        conector.InsertarPago(facturaID, "0", "venta", Me._MontoPago.ToString(), Me._DevueltaPago.ToString(), Me._usuarioID, Me._TipoPago)
        conector.Cerrar()
    End Sub
    Public Sub obtenerFacturaVenta(facturaID As String)
        Dim conector As New Conexion()
        Dim array() As String = conector.ObtenerDataFacturaVentas(facturaID)
        Me._facturaID = array(0)
        Me._facturaFecha = array(1).Split(" ")(0)
        conector.Cerrar()
    End Sub
    Public Sub VerReporte(guardar As Boolean)

        If Me.validarClienteProducto Then
            If guardar Then
                Me.insertarFacturaVenta()
                Me.ReFormatearDecimales()
            End If

            Dim reporte As New ReportDocument()

            reporte.Load("C:\Diplomado\SistemaVentaReparacionCelulares\SistemaVentaReparacionCelulares\FacturaVenta.rpt")
            reporte.SetDataSource(Me.ArrayListProductos)
            reporte.SetParameterValue("Fecha", Me._facturaFecha.ToString())
            reporte.SetParameterValue("NoFactura", Me._facturaID.ToString())

            reporte.SetParameterValue("CodigoCliente", Me._ClienteID.ToString())
            reporte.SetParameterValue("NombreCliente", Me._ClienteNombreComple.ToString())

            reporte.SetParameterValue("SubTotal", "$ " + Me.formatearDecimal(Me._Sub_Total))
            reporte.SetParameterValue("Itbis", "$ " + Me.formatearDecimal(Me._TotalITBISTotal))
            reporte.SetParameterValue("MontoAPagar", "$ " + Me.formatearDecimal(Me._TotalMontoTotal))
            reporte.SetParameterValue("TipoPago", Me._TipoPago.ToString())
            reporte.SetParameterValue("Pago", "$ " + Me.formatearDecimal(Me._MontoPago))
            reporte.SetParameterValue("Devuelta", "$ " + Me.formatearDecimal(Me._DevueltaPago))

            Dim vista As New Views()
            vista.CrystalReportViewer1.ReportSource = reporte
            vista.CrystalReportViewer1.Refresh()
            vista.Show()
            If guardar Then
                Me.ReIniciar()
            End If

        End If
    End Sub

    Public Sub ReFormatearDecimales()
        For i As Integer = 0 To Me.ArrayListProductos.Count - 1 Step 1
            Dim Producto As ModeloVentaReporte = Me.ArrayListProductos(i)
            Producto.Total = Me.formatearDecimal(Double.Parse(Producto.Total))
            Producto.Precio = Me.formatearDecimal(Double.Parse(Producto.Precio))
        Next
    End Sub

    Public Function formatearDecimal(value As Double)
        Return String.Format("{0:0,0.00}", value)
    End Function
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Agregar()
    End Sub
    Public Sub Agregar()
        Try
            Dim cantidadComprada As Integer
            If Me.TextBox3.Text <> "" Then
                cantidadComprada = Integer.Parse(Me.TextBox3.Text)
            End If
            If Me.TextBox1.Text = "" Then
                MessageBox.Show("Esta vacío el nombre del producto")
            ElseIf Me.TextBox2.Text = "" Then
                MessageBox.Show("Esta vacío el precio del producto")
            ElseIf Me.TextBox3.Text = "" Then
                MessageBox.Show("Esta vacía la cantidad del producto")
            ElseIf cantidadComprada > Me._cantidadDisponible Then
                Dim sobrePaso As Integer = cantidadComprada - Me._cantidadDisponible
                MessageBox.Show("Se esta comprando mas cantidad de la disponible en el almacen, el excedente es de " + sobrePaso.ToString())
            Else
                Dim total As Double = Double.Parse(Me.TextBox3.Text) * Double.Parse(Me.TextBox2.Text)
                Me._Sub_Total += total
                Me.ArrayListProductosCalcular.Add(New ModeloVentaReporte(Me._CodigoProducto, Me.TextBox1.Text, Me.TextBox3.Text, Me.TextBox2.Text, total))
                Me.ArrayListProductos.Add(New ModeloVentaReporte(Me._CodigoProducto, Me.TextBox1.Text, Me.TextBox3.Text, Me.TextBox2.Text, total))
                Me.limpiar()
                Me.cargarGridViewProductosVender()
            End If

        Catch ex As Exception
            MessageBox.Show("Error convirtiendo los datos " + ex.ToString())
        End Try
    End Sub
    Public Sub Eliminar()
        If MessageBox.Show("Seguro que desea eliminar el producto?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            If Me.DataGridView1.SelectedRows.Count > 0 Or Me.DataGridView1.SelectedCells.Count > 0 Then
                Dim index As Integer = Me.DataGridView1.CurrentRow.Index
                Me.ArrayListProductos.RemoveAt(index)
                Me.ArrayListProductosCalcular.RemoveAt(index)
                Me.cargarGridViewProductosVender()
            Else
                MessageBox.Show("No hay  selección")
            End If
        End If


    End Sub

    Public Sub limpiar()
        Me.TextBox1.Text = ""
        Me.TextBox2.Text = ""
        Me.TextBox3.Text = ""
        Me.TextBox4.Text = ""
    End Sub

    Public Sub setProducto(codigo As String, producto As String, precio As String, cantidadDisponible As Integer)
        Me._CodigoProducto = codigo
        Me.TextBox1.Text = producto
        Me.TextBox2.Text = precio
        Me._cantidadDisponible = cantidadDisponible
    End Sub
    Public Sub setCliente(codigo As String, nombre As String, cedula As String, rnc As String)
        Me._ClienteID = codigo
        Me._ClienteNombreComple = nombre
        Me._ClienteCedula = cedula
        Me._ClienteRNC = rnc
        Me.TextBox5.Text = codigo
        Me.TextBox6.Text = nombre
        Me.TextBox7.Text = cedula
        Me.TextBox8.Text = rnc
    End Sub

    Private Sub TextBox1_Click(sender As Object, e As EventArgs) Handles TextBox1.Click
        Dim buscador As New BuscadorEquipos()
        buscador.setVentas(Me)
        buscador.setOrigen("ventas")
        buscador.Show()
    End Sub

    Private Sub TextBox3_KeyUp(sender As Object, e As KeyEventArgs) Handles TextBox3.KeyUp
        Me.calcularTotal()
        Me.PresioneEnter(e)
    End Sub
    Public Sub PresioneEnter(e As KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
            Me.Agregar()
        End If
    End Sub
    Public Sub calcularTotal()
        If Me.TextBox3.Text = "" Then
            ' MessageBox.Show("No puede estar vacío el cantidad del producto")
        Else
            Dim total As Double = Double.Parse(Me.TextBox3.Text) * Double.Parse(Me.TextBox2.Text)
            Me.TextBox4.Text = total.ToString()
        End If
    End Sub
    Public Sub cargarGridViewProductosVender()
        Me.DataGridView1.Rows.Clear()

        Me._Sub_Total = 0.00
        Me._TotalSubTotal = 0.00
        Me._TotalMontoTotal = 0.00
        Me._TotalITBISTotal = 0.00
        Dim subTotal As Double = 0.00

        For i As Integer = 0 To Me.ArrayListProductosCalcular.Count - 1 Step 1
            Dim Producto As ModeloVentaReporte = Me.ArrayListProductosCalcular(i)
            'MessageBox.Show("c " + Producto.Codigo + " N " + Producto.Producto)
            subTotal += Double.Parse(Producto.Total)
            Me.DataGridView1.Rows.Add(Producto.Codigo, Producto.Producto, Producto.Precio, Producto.Cantidad, Producto.Total)
        Next
        Me.calcularTotales(subTotal)
    End Sub

    Public Sub calcularTotales(subTotal As Double)
        Me._TotalSubTotal = subTotal
        Me._Sub_Total = subTotal
        If Me.CheckBox1.Checked Then
            Me._TotalITBISTotal = Me._TotalSubTotal * (18 / 100)
        End If
        Me._TotalMontoTotal = Me._TotalITBISTotal + Me._TotalSubTotal

        Me.LbMontoItbis.Text = Me._TotalITBISTotal.ToString()
        Me.LbSubTotal.Text = Me._TotalSubTotal.ToString()
        Me.LbMontoAPagar.Text = Me._TotalMontoTotal.ToString()

        If Me._MontoPago > 0 Then
            Me._DevueltaPago = Me._MontoPago - Me._TotalMontoTotal
            Me.setPago(Me._MontoPago, Me._TipoPago, Me._DevueltaPago)
        End If
    End Sub
    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click

    End Sub

    Private Sub CheckBox1_CheckStateChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckStateChanged
        Me.cargarGridViewProductosVender()
    End Sub

    Public Sub setPago(monto As Double, tipo As String, devuelta As Double)
        Me._MontoPago = monto
        Me._TipoPago = tipo
        Me._DevueltaPago = devuelta
        Me.LbMontoPagado.Text = monto.ToString()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If Me.validarClienteProducto Then
            Dim pagos As New Pagos()
            pagos.setDeuda(Me._TotalMontoTotal)
            pagos.setVentas(Me)
            pagos.Show()
        End If
    End Sub
    Public Function validarClienteProducto() As Boolean
        Dim resp As Boolean = False
        If Me.ArrayListProductos.Count <= 0 Then
            MessageBox.Show("Aun no ha agregado un producto")
        ElseIf Me._ClienteID <= 0 Then
            MessageBox.Show("Aun no ha agregado un cliente")
        Else
            resp = True
        End If
        Return resp
    End Function

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.VerReporte(False)
    End Sub

    Private Sub TextBox5_Click(sender As Object, e As EventArgs) Handles TextBox5.Click
        Me.BuscarCliente()
    End Sub
    Public Sub BuscarCliente()
        Dim buscador As New BuscadorCliente()
        buscador.setVentas(Me)
        buscador.setUsuarioID(Me._usuarioID)
        buscador.Show()
    End Sub

    Private Sub TextBox6_Click(sender As Object, e As EventArgs) Handles TextBox6.Click
        Me.BuscarCliente()
    End Sub

    Private Sub TextBox7_Click(sender As Object, e As EventArgs) Handles TextBox7.Click
        Me.BuscarCliente()
    End Sub

    Private Sub TextBox8_Click(sender As Object, e As EventArgs) Handles TextBox8.Click
        Me.BuscarCliente()
    End Sub

    Private Sub ToolStripMenuItem2_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub EliminarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EliminarToolStripMenuItem.Click
        Me.Eliminar()

    End Sub

    Private Sub Ventas_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class