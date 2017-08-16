Public Class Form1

    Private _usuarioNombre As String
    Private _usuarioID As String
    Private _usuarioTipo As String
    Private _Cajero As Boolean
    Private _Supervisor As Boolean
    Private _Administrador As Boolean

    Public Sub AsignarPermiso()
        Me._Cajero = False
        Me._Supervisor = False
        Me._Administrador = False

        Select Case Me._usuarioTipo
            Case "cajero"
                Me._Cajero = True

            Case "supervisor"
                Me._Supervisor = True

            Case "administrador"
                Me._Administrador = True
        End Select


    End Sub

    Public Sub setDataUsuario(nombre As String, id As String, tipo As String)
        Me._usuarioID = id
        Me._usuarioNombre = nombre
        Me._usuarioTipo = tipo
        Me.Label3.Text = nombre
        Me.AsignarPermiso()
    End Sub

    Private Sub ReportesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReportesToolStripMenuItem.Click

    End Sub

    Private Sub CreadoPorToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CreadoPorToolStripMenuItem.Click
        If Me._Administrador Or Me._Supervisor Or Me._Cajero Then
            CreadoPor.Show()
        Else
            Me.MensajeValidacion()
        End If
    End Sub

    Private Sub UsuariosToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UsuariosToolStripMenuItem.Click
        If Me._Administrador Then
            Dim usuario As New Usuarios()
            usuario.setUsuarioID(Me._usuarioID)
            usuario.Show()
        Else
            Me.MensajeValidacion()
        End If

    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click

    End Sub

    Private Sub MenuStrip1_ItemClicked(sender As Object, e As ToolStripItemClickedEventArgs) Handles MenuStrip1.ItemClicked

    End Sub

    Private Sub Form1_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        If Me._Administrador Or Me._Supervisor Or Me._Cajero Then
            Application.Exit()
        Else
            Me.MensajeValidacion()
        End If
    End Sub

    Private Sub InventarioToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles InventarioToolStripMenuItem.Click

        If Me._Administrador Or Me._Supervisor Then
            Dim inventario As New Inventario()
            inventario.setUsuarioID(Me._usuarioID)
            inventario.Show()

        Else
            Me.MensajeValidacion()
        End If
    End Sub

    Private Sub CrearToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CrearToolStripMenuItem.Click

        If Me._Administrador Or Me._Supervisor Or Me._Cajero Then
            Dim ventas As New Ventas
            ventas.setUsuarioID(Me._usuarioID)
            ventas.Show()
        Else
            Me.MensajeValidacion()
        End If
    End Sub

    Private Sub CrearToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles CrearToolStripMenuItem1.Click
        If Me._Administrador Or Me._Supervisor Or Me._Cajero Then
            Dim reparaciones As New Reparaciones
            reparaciones.setUsuarioID(Me._usuarioID)
            reparaciones.Show()
        Else
            Me.MensajeValidacion()
        End If
    End Sub

    Private Sub SalirToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SalirToolStripMenuItem.Click
        If Me._Administrador Or Me._Supervisor Or Me._Cajero Then
            Application.Exit()
        Else
            Me.MensajeValidacion()
        End If
    End Sub

    Private Sub CambiarClaveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CambiarClaveToolStripMenuItem.Click
        If Me._Administrador Or Me._Supervisor Or Me._Cajero Then

            Dim cambiarClave As New CambiarClave
            cambiarClave.setUsuarioID(Me._usuarioID)
            cambiarClave.Show()
        Else
            Me.MensajeValidacion()

        End If
    End Sub

    Private Sub CrearToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles CrearToolStripMenuItem2.Click
        If Me._Administrador Or Me._Supervisor Then
            Dim reporte As New Reportes
            reporte.Show()
        Else
            Me.MensajeValidacion()

        End If
    End Sub

    Private Sub CerrarSesionToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CerrarSesionToolStripMenuItem.Click

        If Me._Administrador Or Me._Supervisor Or Me._Cajero Then
            'MessageBox.Show(Application.OpenForms.Count.ToString())
            Dim total As Integer = Application.OpenForms.Count
            Dim f As FormCollection = Application.OpenForms
            For i As Integer = 0 To total - 1 Step 1
                'MessageBox.Show(f(i).Text)
                Application.OpenForms(i).Hide()
            Next
            Dim login As New Login
            login.Show()
        Else
            Me.MensajeValidacion()

        End If

    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub


    Public Sub MensajeValidacion()
        MessageBox.Show("Lo siento, pero usted no tiene permiso para poder usar esta opción")
    End Sub

    Private Sub CrearToolStripMenuItem3_Click(sender As Object, e As EventArgs) Handles CrearToolStripMenuItem3.Click
        If Me._Administrador Or Me._Supervisor Or Me._Cajero Then
            Dim cliente As New Cliente
            cliente.setUsuarioID(Me._usuarioID)
            cliente.Show()
        Else
            Me.MensajeValidacion()
        End If
    End Sub
End Class
