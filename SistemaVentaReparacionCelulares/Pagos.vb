Public Class Pagos

    Private _Venta As Ventas
    Private _Reparaciones As Reparaciones
    Private _MontoDeuda As Double
    Private _Origen As String = "ventas"

    Private Sub Pagos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.ComboBox1.SelectedIndex = 0
    End Sub

    Public Sub setVentas(venta As Ventas)
        Me._Venta = venta
    End Sub
    Public Sub setOrigen(origen As String)
        Me._Origen = origen
    End Sub
    Public Sub setReparaciones(reparaciones As Reparaciones)
        Me._Reparaciones = reparaciones
    End Sub
    Public Sub setDeuda(deuda As Double)
        Me._MontoDeuda = deuda
    End Sub

    Public Sub Pagar()
        Dim pago As String = Me.TextBox1.Text
        If pago = "" Then
            MessageBox.Show("Se necesita ingresar un monto a pagar")
        Else
            Dim tipo As String = Me.ComboBox1.SelectedItem.ToString()
            'MessageBox.Show("tipo " + tipo)
            Dim pagoD As Double = Double.Parse(pago)
            If pagoD < Me._MontoDeuda Then
                MessageBox.Show("El pago no puede ser menor a la deuda")
            Else
                Try
                    Dim devuelta As Double = pagoD - Me._MontoDeuda
                    If Me._Origen.Equals("ventas") Then
                        Me._Venta.setPago(pagoD, tipo, devuelta)
                    ElseIf Me._Origen.Equals("reparaciones") Then
                        Me._Reparaciones.setPago(pagoD, tipo, devuelta)
                    End If
                    Me.Hide()
                    MessageBox.Show("Monto a devolver " + devuelta.ToString())
                Catch ex As Exception
                    MessageBox.Show("Monto a devolver " + ex.ToString())
                End Try

            End If


        End If

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Pagar()
    End Sub
End Class