Imports CrystalDecisions.CrystalReports.Engine

Public Class Reportes

    Private _ArrayListClientes As New ArrayList()
    Private _ArrayListEquipos As New ArrayList()
    Private _ArrayListUsuarios As New ArrayList()
    Private _ArrayListClientesID As New ArrayList()
    Private _ArrayListEquiposID As New ArrayList()
    Private _ArrayListUsuariosID As New ArrayList()
    Private _ArrayListFacturas As ArrayList

    Private _TipoReporte As String
    Private _FechaDesde As String
    Private _FechaHasta As String
    Private _ListClientes As String = ""
    Private _ListUsuarios As String = ""
    Private _ListEquipos As String = ""
    Private _TotalItbis As Double = 0.0
    Private _TotalSubTotal As Double = 0.0
    Private _TotalMonto As Double = 0.0
    Private _TotalCantidadFactura As Integer = 0


    Public Sub CargarDatos()
        Me._TotalItbis = 0.0
        Me._TotalSubTotal = 0.0
        Me._TotalMonto = 0.0
        Me._TotalCantidadFactura = 0
        Me._ListClientes = ""
        Me._ListUsuarios = ""
        Me._ListEquipos = ""
        Me._FechaDesde = Me.DateTimePicker1.Text
        Me._FechaHasta = Me.DateTimePicker2.Text
        Me._TipoReporte = Me.ComboBox2.Text.ToLower()
        'MessageBox.Show(Me._FechaDesde + " " + Me._FechaHasta + " " + Me._TipoReporte)
        Me.GetSeleccionUsuarios()
        Me.GetSeleccionClientes()
        Me.GetSeleccionEquipos()
    End Sub

    Public Sub GetSeleccionUsuarios()
        For i As Integer = 0 To Me.ListBox2.SelectedIndices.Count - 1 Step 1
            Dim id As Integer = Integer.Parse(Me.ListBox2.SelectedIndices(i).ToString())
            If Not Me._ArrayListUsuariosID(id).ToString().Equals("0") Then
                If i = 0 Then
                    Me._ListUsuarios += ""
                Else
                    Me._ListUsuarios += ","
                End If
                Me._ListUsuarios += Me._ArrayListUsuariosID(id).ToString()
                'MessageBox.Show(Me._ArrayListUsuariosID(id).ToString())
            End If
        Next
    End Sub
    Public Sub GetSeleccionClientes()
        For i As Integer = 0 To Me.ListBox3.SelectedIndices.Count - 1 Step 1
            Dim id As Integer = Integer.Parse(Me.ListBox3.SelectedIndices(i).ToString())
            If Not Me._ArrayListClientesID(id).ToString().Equals("0") Then
                If i = 0 Then
                    Me._ListClientes += ""
                Else
                    Me._ListClientes += ","
                End If
                Me._ListClientes += Me._ArrayListClientesID(id).ToString()
                'MessageBox.Show(Me._ArrayListClientesID(id).ToString())
            End If
        Next
    End Sub
    Public Sub GetSeleccionEquipos()
        For i As Integer = 0 To Me.ListBox1.SelectedIndices.Count - 1 Step 1
            Dim id As Integer = Integer.Parse(Me.ListBox1.SelectedIndices(i).ToString())
            If Not Me._ArrayListEquiposID(id).ToString().Equals("0") Then

                If i = 0 Then
                    Me._ListEquipos += ""
                Else
                    Me._ListEquipos += ","
                End If
                Me._ListEquipos += Me._ArrayListEquiposID(id).ToString()
                'MessageBox.Show(Me._ArrayListEquiposID(id).ToString())

            End If
        Next
    End Sub

    Public Sub CargarClienes()
        Dim conector As New Conexion()
        conector.MostrarClientesListBox(Me.ListBox3)
        Me._ArrayListClientesID = conector.getResultArrayList()
        conector.Cerrar()
    End Sub
    Public Sub CargarUsuarios()
        Dim conector As New Conexion()
        conector.MostrarUsuariosListBox(Me.ListBox2)
        Me._ArrayListUsuariosID = conector.getResultArrayList()
        conector.Cerrar()
    End Sub
    Public Sub CargarEquipos()
        Dim conector As New Conexion()
        conector.MostrarEquiposListBox(Me.ListBox1)
        Me._ArrayListEquiposID = conector.getResultArrayList()
        conector.Cerrar()
    End Sub

    Private Sub Reportes_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.CargarUsuarios()
        Me.CargarClienes()
        Me.CargarEquipos()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.GenerarReporte()
    End Sub

    Public Sub CrearConsulta()
        Dim where As String = "  f.visible = 1 "
        Dim tabla As String = ""
        Dim tablaDetalle As String = ""
        Dim id As String = ""
        Dim firt As Boolean = False

        If Me._TipoReporte.ToLower().Equals("ventas") Then
            tabla = " factura_ventas as f "
            id = " factura_venta_id  "
            tablaDetalle = " factura_venta_detalle "
        ElseIf Me._TipoReporte.ToLower().Equals("reparaciones") Then
            tabla = " factura_reparacion as f "
            id = " factura_reparacion_id  "
            tablaDetalle = " factura_reparacion_detalle "
        End If

        If Not Me._FechaDesde.Equals(Me._FechaHasta) Then
            If firt Then
                where = " "
                firt = False
            Else
                where += " And "
                'firt = False
            End If
            where += " ( Date(f.fecha) between '" + Me._FechaDesde + "' and '" + Me._FechaHasta + "' ) "
        End If
        'MessageBox.Show("fecha - " + firt.ToString())

        If Me._ListClientes <> "" Then
            If firt Then
                where = " "
                firt = False
            Else
                where += " and "
                'firt = False
            End If
            where += " ( f.cliente_id  in (" + Me._ListClientes + ") ) "
        End If
        'MessageBox.Show("cliente - " + firt.ToString())

        If Me._ListUsuarios <> "" Then
            If firt Then
                where = " "
                firt = False
            Else
                where += " and "
                'firt = False
            End If
            where += " ( f.usuario_id  in (" + Me._ListUsuarios + ") ) "
        End If
        'MessageBox.Show("usuario - " + firt.ToString())

        If Me._ListEquipos <> "" Then
            If firt Then
                where = " "
                firt = False
            Else
                where += " and "

            End If
            where += " ( f.id  in ( select  " + id + "  from " + tablaDetalle + " where equipo_id  in (" + Me._ListEquipos + ") ) ) "
        End If
        'MessageBox.Show("equipo - " + firt.ToString())

        where += " group by f.id  "
        tabla += " inner join clientes as c on c.id = f.cliente_id  inner join usuarios as u on u.id = f.usuario_id "
        'MessageBox.Show(tabla + " " + where)


        Dim conector As New Conexion()
        conector.MostrarReporteDatos(tabla, where)
        Me._ArrayListFacturas = conector.getResultArrayList()
        conector.Cerrar()

    End Sub

    Public Sub GenerarReporte()
        Me.CargarDatos()
        Me.CrearConsulta()
        Me.CalcularTotales()
        Me.Reporte()
    End Sub

    Public Sub Reporte()
        Dim reporte As New ReportDocument()
        reporte.Load("C:\Diplomado\SistemaVentaReparacionCelulares\SistemaVentaReparacionCelulares\ReporteFacturas.rpt")

        reporte.SetDataSource(Me._ArrayListFacturas)

        reporte.SetParameterValue("CantidadTotalFactura", "  " + Me._TotalCantidadFactura.ToString())
        reporte.SetParameterValue("ItbisTotalFactura", "$ " + Me.formatearDecimal(Me._TotalItbis))
        reporte.SetParameterValue("MontoTotalFactura", "$ " + Me.formatearDecimal(Me._TotalMonto))
        reporte.SetParameterValue("SubTotalTotalFactura", "$ " + Me.formatearDecimal(Me._TotalSubTotal))

        Dim vista As New Views()
        vista.CrystalReportViewer1.ReportSource = reporte
        vista.CrystalReportViewer1.Refresh()
        vista.Show()
    End Sub
    Public Sub CalcularTotales()
        For i As Integer = 0 To Me._ArrayListFacturas.Count - 1 Step 1
            Dim facturas As ModeloReporteFactura = Me._ArrayListFacturas(i)
            Me._TotalCantidadFactura += 1
            Me._TotalItbis += Double.Parse(facturas.Itbis)
            Me._TotalSubTotal += Double.Parse(facturas.SubTotal)
            Me._TotalMonto += Double.Parse(facturas.Monto)



            facturas.Itbis = Me.formatearDecimal(Double.Parse(facturas.Itbis))
            facturas.SubTotal = Me.formatearDecimal(Double.Parse(facturas.SubTotal))
            facturas.Monto = Me.formatearDecimal(Double.Parse(facturas.Monto))
            facturas.Fecha = facturas.Fecha.Split(" ")(0)

            facturas.Itbis = "$ " + facturas.Itbis
            facturas.SubTotal = "$ " + facturas.SubTotal
            facturas.Monto = "$ " + facturas.Monto
        Next
    End Sub

    Public Function formatearDecimal(value As Double)
        Return String.Format("{0:0,0.00}", value)
    End Function

    Private Sub Button1_Move(sender As Object, e As EventArgs) Handles Button1.Move

    End Sub
End Class