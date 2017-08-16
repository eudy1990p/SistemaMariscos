Imports MySql.Data.MySqlClient
Imports System.Data.SqlClient
Public Class Conexion

    Private _Conector As Object

    'Private _Conector As MySqlConnection
    'Private _ConectorSqlServer As SqlConnection

    Private _Opcion As Integer = 1
    Private _Motor As Integer = 2

    Private _Server As String = "localhost"
    Private _DB As String = "db_sistema_venta_reparacion_celulares"
    Private _Usuario As String = "root"
    Private _Clave As String = ""
    'Private _lector As MySqlDataReader

    Private _lector As Object
    Private _comando As Object
    Private _Fecha As String



    'Private _lectorSqlServer As SqlDataReader

    Sub New()
        Try

            If Me._Motor = Me._Opcion Then
                'Dim StringConexion As String = "Data Source =DESKTOP-L35G0RU\VOSTRO;Initial Catalog=" + Me._DB + ";Integrated Security=True"
                Dim StringConexion As String = "Integrated Security=SSPI;Initial Catalog=" + Me._DB + ";Data Source=DESKTOP-L35G0RU\SQLEXPRESS"

                Me._Conector = New SqlConnection(StringConexion)
                Me._Fecha = "getDate()"

                'Me._ConectorSqlServer = New SqlConnection(StringConexion)
                'Me._ConectorSqlServer.Open()
            Else
                Dim StringConexion As String = "server=" + Me._Server + ";database=" + Me._DB + ";Uid=" + Me._Usuario + ";pwd=" + Me._Clave + ";"
                _Conector = New MySqlConnection(StringConexion)
                Me._Fecha = "now()"
            End If
            Me._Conector.Open()
            Console.WriteLine("conectado")

        Catch ex As Exception
            MessageBox.Show("Error Conexion " + ex.ToString())
        End Try
    End Sub

    Public Sub Cerrar()
        Try
            Me._Conector.Close()
        Catch ex As Exception
            MessageBox.Show("Error Conexion " + ex.ToString())
        End Try
    End Sub
    Public Sub validarCreaateCommand()
        If Me._Motor = Me._Opcion Then
            Me._comando = Me._Conector.CreateCommand()
        Else
            Me._comando = Me._Conector.CreateCommand()
        End If
    End Sub
    Public Function Insert(Tabla As String, Campos As String, Valores As String) As Boolean
        Try
            Me.validarCreaateCommand()

            Me._comando.CommandText = "INSERT INTO " + Tabla + " (" + Campos + ") VALUES (" + Valores + ") "
            Me._comando.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            MessageBox.Show("Error insertando " + ex.ToString())
            Return False
        End Try
    End Function
    Public Function InsertGetID(Tabla As String, Campos As String, Valores As String) As Integer
        Try
            Me.validarCreaateCommand()
            _comando.CommandText = "INSERT INTO " + Tabla + " (" + Campos + ") VALUES (" + Valores + ") "
            _comando.ExecuteNonQuery()
            Dim sql As String = "Select max(id) As id from " + Tabla + " "
            Console.WriteLine(sql)
            _comando.CommandText = sql
            Dim id As Integer = _comando.ExecuteScalar()
            Return id
        Catch ex As Exception
            MessageBox.Show("Error insertando " + ex.ToString())
            Return 0
        End Try
    End Function
    Public Function QueryFree(sql As String) As Boolean
        Try
            Me.validarCreaateCommand()

            _comando.CommandText = sql
            _comando.ExecuteNonQuery()
            Return True

        Catch ex As Exception
            MessageBox.Show("Error con el comando " + ex.ToString())
            Return False
        End Try
    End Function

    Public Function Mostrar(Tabla As String, Campos As String, Where As String) As Boolean
        Try
            Me.validarCreaateCommand()
            'MessageBox.Show("Select " + Campos + "  FROM " + Tabla + " WHERE " + Where + " ")
            Dim sql As String = "Select " + Campos + "  FROM " + Tabla + " WHERE " + Where + " "
            Console.WriteLine(sql)
            _comando.CommandText = sql
            Me._lector = _comando.ExecuteReader()
            Return True

        Catch ex As Exception
            MessageBox.Show("Error mostrando " + ex.ToString())
            Return False
        End Try
    End Function

    Public Sub MostrarUsuarios(dataGridView As DataGridView, where As String)
        Dim respuesta As Boolean = Me.Mostrar("usuarios", " * ", where)
        If respuesta Then
            If Me._lector.HasRows Then
                dataGridView.Rows.Clear()
                Try
                    While Me._lector.Read()
                        dataGridView.Rows.Add(Me._lector("id"), Me._lector("usuario"), Me._lector("tipo"))
                    End While
                Catch ex As Exception
                    MessageBox.Show("Error consiguiendo los datos " + ex.ToString())
                End Try
            Else
                MessageBox.Show("No hay registros ")
            End If
        Else
            MessageBox.Show("No se pudo mostrar ")
        End If

    End Sub
    Public Function ObtenerDataUsuario(id As String) As String()
        Dim respuesta As Boolean = Me.Mostrar("usuarios", " * ", " id = '" + id + "'  and visible = 1 ")
        If respuesta Then
            If Me._lector.HasRows Then
                Try
                    If Me._lector.Read() Then
                        'Me._lector("id"), Me._lector("usuario"), Me._lector("tipo")
                        Dim Data(4) As String
                        Data(0) = Me._lector("id")
                        Data(1) = Me._lector("usuario")
                        Data(2) = Me._lector("tipo")
                        Data(3) = Me._lector("clave")
                        Return Data
                    End If
                Catch ex As Exception
                    MessageBox.Show("Error consiguiendo los datos " + ex.ToString())
                End Try
            Else
                MessageBox.Show("No hay registros ")
                Return {"", "", ""}

            End If
        Else
            MessageBox.Show("No se pudo mostrar ")
            Return {"", "", ""}

        End If
        Return {"", "", ""}

    End Function

    Public Function whereUsuario(where As String) As String()
        Dim respuesta As Boolean = Me.Mostrar("usuarios", " * ", where)
        If respuesta Then
            If Me._lector.HasRows Then
                Try
                    If Me._lector.Read() Then
                        'Me._lector("id"), Me._lector("usuario"), Me._lector("tipo")
                        Dim Data(4) As String
                        Data(0) = Me._lector("id")
                        Data(1) = Me._lector("usuario")
                        Data(2) = Me._lector("tipo")
                        Data(3) = Me._lector("clave")
                        Return Data
                    End If
                Catch ex As Exception
                    '      MessageBox.Show("Error consiguiendo los datos " + ex.ToString())
                End Try
            Else
                '   MessageBox.Show("No hay registros ")
                Return {"", "", ""}

            End If
        Else
            'MessageBox.Show("No se pudo mostrar ")
            Return {"", "", ""}

        End If
        Return {"", "", ""}

    End Function

    Public Sub InsertarUsuario(Nombre As String, Clave As String, Tipo As String, UsuarioID As String)
        Dim respuesta As Boolean = Me.Insert("usuarios", "usuario,clave,tipo,fecha,usuario_id", "'" + Nombre + "','" + Clave + "','" + Tipo + "'," + Me._Fecha + ",'" + UsuarioID + "' ")
        If respuesta Then
            MessageBox.Show("Se inserto el registro de forma correcta")
        Else
            MessageBox.Show("No se inserto el registro de forma correcta")
        End If
    End Sub
    Public Sub ActualizarUsuario(Nombre As String, Clave As String, Tipo As String, UsuarioID As String)
        Dim respuesta As Boolean = Me.QueryFree("Update usuarios set usuario='" + Nombre + "',clave='" + Clave + "',tipo = '" + Tipo + "' where id ='" + UsuarioID + "' ")
        If respuesta Then
            MessageBox.Show("Se Actualizo el registro de forma correcta")
        Else
            MessageBox.Show("No se Actualizo el registro de forma correcta")
        End If
    End Sub
    Public Sub ActualizarCliente(Nombre As String, Apellido As String, Cedula As String, rnc As String, ClienteID As String)
        Dim respuesta As Boolean = Me.QueryFree("Update clientes set nombre='" + Nombre + "',apellido='" + Apellido + "',cedula = '" + Cedula + "',rnc = '" + rnc + "' where id ='" + ClienteID + "' ")
        If respuesta Then
            MessageBox.Show("Se Actualizo el registro de forma correcta")
        Else
            MessageBox.Show("No se Actualizo el registro de forma correcta")
        End If
    End Sub
    Public Function ActualizarClaveUsuario(Clave As String, UsuarioID As String) As Boolean
        Dim respuesta As Boolean = Me.QueryFree("Update usuarios set clave='" + Clave + "' where id ='" + UsuarioID + "' ")
        If respuesta Then
            MessageBox.Show("Se Actualizo el registro de forma correcta")
        Else
            MessageBox.Show("No se Actualizo el registro de forma correcta")
        End If
        Return respuesta
    End Function
    Public Sub EliminarUsuario(UsuarioID As String)
        ' Dim respuesta As Boolean = Me.QueryFree("Delete from usuarios where id ='" + UsuarioID + "' ")
        Dim respuesta As Boolean = Me.QueryFree("update usuarios set visible = false where id ='" + UsuarioID + "' ")

        If respuesta Then
            MessageBox.Show("Se Elimino el registro de forma correcta")
        Else
            MessageBox.Show("No se Elimino el registro de forma correcta")
        End If
    End Sub
    Public Sub EliminarCliente(ClienteID As String)
        'Dim respuesta As Boolean = Me.QueryFree("Delete from clientes where id ='" + ClienteID + "' ")
        Dim respuesta As Boolean = Me.QueryFree("update clientes set visible = false where id ='" + ClienteID + "' ")
        If respuesta Then
            MessageBox.Show("Se Elimino el registro de forma correcta")
        Else
            MessageBox.Show("No se Elimino el registro de forma correcta")
        End If
    End Sub

    Private ResultArrayList As New ArrayList
    Public Function getResultArrayList()
        Return Me.ResultArrayList
    End Function
    Public Sub MostrarEquiposCombo(combo As ComboBox)
        Dim respuesta As Boolean = Me.Mostrar("equipos", " * ", "   visible = 1 ")
        If respuesta Then
            If Me._lector.HasRows Then
                combo.Items.Clear()
                Try
                    While Me._lector.Read()
                        'MessageBox.Show(" Codigo: " + Me._lector("id").ToString())
                        Dim idEquipo As String = Me._lector("id").ToString()
                        combo.Items.Add(" Codigo: " + idEquipo + " Marca " + Me._lector("marca").ToString() + " Modelo " + Me._lector("modelo").ToString())
                        Me.ResultArrayList.Add(idEquipo)
                    End While
                    combo.SelectedIndex = 0
                Catch ex As Exception
                    MessageBox.Show("Error consiguiendo los datos para combo " + ex.ToString())
                End Try
            Else
                MessageBox.Show("No hay equipos")
            End If
        Else
            MessageBox.Show("No se pudo mostrar ")
        End If

    End Sub

    Public Sub InsertarEquipos(marca As String, modelo As String, precio As String, UsuarioID As String)

        Dim respuesta As Boolean = Me.Insert("equipos", "marca,modelo ,precio_venta ,cantidad_almacen ,fecha  ,usuario_id", "'" + marca + "','" + modelo + "','" + precio + "','0'," + Me._Fecha + ",'" + UsuarioID + "' ")
        If respuesta Then
            MessageBox.Show("Se inserto el registro de forma correcta")
        Else
            MessageBox.Show("No se inserto el registro de forma correcta")
        End If
    End Sub
    Public Function InsertarCompra(equipo_id As String, cantidad As String, compra As String, venta As String, monto As String, NSuplidor As String, ESuplidor As String, TSuplidor As String, UsuarioID As String) As Integer
        Dim idCompra As Integer = Me.InsertGetID("compras", "equipo_id,cantidad  ,precio_venta   ,precio_compra  ,monto_compra   ,fecha  ,usuario_id,nombre_suplidor  ,telefono_suplidor  ,email_suplidor  ", "'" + equipo_id + "','" + cantidad + "','" + venta + "','" + compra + "','" + monto + "'," + Me._Fecha + ",'" + UsuarioID + "','" + NSuplidor + "','" + TSuplidor + "','" + ESuplidor + "' ")
        If idCompra > 0 Then
            Dim nuevoCampo As String = ""
            If venta <> "" Then
                nuevoCampo = ",precio_venta = '" + venta + "' "
            End If
            Me.QueryFree("update equipos set cantidad_almacen = cantidad_almacen + " + cantidad + " " + nuevoCampo + " where id = '" + equipo_id + "'  ")
            MessageBox.Show("Se inserto el registro de forma correcta")
            Return idCompra
        Else
            MessageBox.Show("No se inserto el registro de forma correcta")
            Return 0
        End If
        Return idCompra
    End Function
    Public Sub MostrarEquiposDataGridView(dataGridView As DataGridView, where As String)
        Dim respuesta As Boolean = Me.Mostrar("equipos", " * ", where)
        If respuesta Then
            If Me._lector.HasRows Then
                dataGridView.Rows.Clear()
                Try
                    While Me._lector.Read()
                        dataGridView.Rows.Add(Me._lector("id"), Me._lector("marca"), Me._lector("modelo"), Me._lector("cantidad_almacen"), Me._lector("precio_venta"))
                    End While
                Catch ex As Exception
                    MessageBox.Show("Error consiguiendo los datos " + ex.ToString())
                End Try
            Else
                MessageBox.Show("No hay registros ")
            End If
        Else
            MessageBox.Show("No se pudo mostrar ")
        End If

    End Sub
    Public Sub MostrarComprasDataGridView(dataGridView As DataGridView, where As String)
        Dim respuesta As Boolean = Me.Mostrar("compras as c inner join equipos as e on c.equipo_id = e.id ", " c.*,concat(e.marca,' ',e.modelo) as producto ", where)
        If respuesta Then
            If Me._lector.HasRows Then
                dataGridView.Rows.Clear()
                Try
                    While Me._lector.Read()
                        dataGridView.Rows.Add(Me._lector("id"), Me._lector("producto"), Me._lector("cantidad"), Me._lector("precio_compra"), Me._lector("nombre_suplidor"), Me._lector("telefono_suplidor"), Me._lector("email_suplidor"))
                    End While
                Catch ex As Exception
                    MessageBox.Show("Error consiguiendo los datos " + ex.ToString())
                End Try
            Else
                MessageBox.Show("No hay registros ")
            End If
        Else
            MessageBox.Show("No se pudo mostrar ")
        End If

    End Sub

    Public Function InsertarCliente(nombre As String, apellido As String, cedula As String, rnc As String, UsuarioID As String) As Integer
        Dim idCliente As Integer = 0
        Dim respuesta As Boolean = Me.Mostrar("clientes", " * ", " cedula = '" + cedula + "' or rnc = '" + rnc + "' ")
        If respuesta Then
            If (Me._lector.HasRows) = False Then
                Me._Conector.Close()
                Me._Conector.Open()
                idCliente = Me.InsertGetID("clientes", "nombre ,apellido   ,cedula    ,rnc   ,fecha ,usuario_id", "'" + nombre + "','" + apellido + "','" + cedula + "','" + rnc + "'," + Me._Fecha + ",'" + UsuarioID + "' ")
                If idCliente <= 0 Then
                    MessageBox.Show("No se inserto el registro de forma correcta")
                End If
            Else
                MessageBox.Show("Ya existe esta cedula: " + cedula)
            End If
        End If
        Return idCliente
    End Function
    Public Sub InsertarClienteEmail(correo As String, UsuarioID As String)
        Dim respuesta As Boolean = Me.Insert("cliente_email", "correo ,fecha  ,usuario_id", "'" + correo + "'," + Me._Fecha + ",'" + UsuarioID + "' ")
        If respuesta Then
            ' MessageBox.Show("Se inserto el registro de forma correcta")
        Else
            MessageBox.Show("No se inserto el registro de forma correcta")
        End If
    End Sub
    Public Sub InsertarClienteTelefono(telefono As String, tipo As String, UsuarioID As String)
        Dim respuesta As Boolean = Me.Insert("cliente_telefono", "telefono ,tipo  ,fecha  ,usuario_id", "'" + telefono + "','" + tipo + "'," + Me._Fecha + ",'" + UsuarioID + "' ")
        If respuesta Then
            ' MessageBox.Show("Se inserto el registro de forma correcta")
        Else
            MessageBox.Show("No se inserto el registro de forma correcta")
        End If
    End Sub
    Public Sub MostrarClientes(dataGridView As DataGridView, where As String)
        Dim respuesta As Boolean = Me.Mostrar("clientes", " * ", where)
        If respuesta Then
            If Me._lector.HasRows Then
                dataGridView.Rows.Clear()
                Try
                    While Me._lector.Read()
                        Me.ResultArrayList.Add(Me._lector("id"))
                        dataGridView.Rows.Add(Me._lector("id"), Me._lector("nombre"), Me._lector("apellido"), Me._lector("cedula"), Me._lector("rnc"))
                    End While
                Catch ex As Exception
                    MessageBox.Show("Error consiguiendo los datos cliente " + ex.ToString())
                End Try
            Else
                MessageBox.Show("No hay registros ")
            End If
        Else
            MessageBox.Show("No se pudo mostrar ")
        End If

    End Sub

    Public Function InsertarFacturaVenta(subTotal As String, itbis As String, montoAPagar As String, clienteID As String, UsuarioID As String) As Integer
        Dim idFacturaVenta As Integer = 0
        idFacturaVenta = Me.InsertGetID("factura_ventas", "sub_total   ,monto_apagar     ,monto_itbis  ,estado     ,cliente_id     ,fecha ,usuario_id", "'" + subTotal + "','" + montoAPagar + "','" + itbis + "','pendiente','" + clienteID + "'," + Me._Fecha + ",'" + UsuarioID + "' ")
        If idFacturaVenta <= 0 Then
            MessageBox.Show("No se inserto el registro de forma correcta")
        End If
        Return idFacturaVenta
    End Function
    Public Function InsertarFacturaReparacion(subTotal As String, itbis As String, montoAPagar As String, clienteID As String, UsuarioID As String) As Integer
        Dim idFacturaVenta As Integer = 0
        idFacturaVenta = Me.InsertGetID("factura_reparacion", "sub_total   ,monto_apagar     ,monto_itbis  ,estado     ,cliente_id     ,fecha ,usuario_id", "'" + subTotal + "','" + montoAPagar + "','" + itbis + "','pendiente','" + clienteID + "'," + Me._Fecha + ",'" + UsuarioID + "' ")
        If idFacturaVenta <= 0 Then
            MessageBox.Show("No se inserto el registro de forma correcta")
        End If
        Return idFacturaVenta
    End Function
    Public Function InsertarFacturaVentaDetalle(facturaID As String, equipoID As String, cantidad As String, monto As String, precio As String, UsuarioID As String) As Integer
        Dim resp As Boolean = True
        resp = Me.Insert("factura_venta_detalle", "equipo_id     ,cantidad      ,monto   ,precio     ,fecha ,usuario_id , estado,factura_venta_id   ", "'" + equipoID + "','" + cantidad + "','" + monto + "','" + precio + "'," + Me._Fecha + ",'" + UsuarioID + "','pendiente','" + facturaID + "' ")
        If resp = False Then
            MessageBox.Show("No se inserto el registro de forma correcta")
        End If
        Me.QueryFree("update equipos set cantidad_almacen = cantidad_almacen - " + cantidad + "   where id = '" + equipoID + "'  ")
        Return resp
    End Function
    Public Function InsertarFacturaReparacionDetalle(facturaID As String, equipoID As String, cantidad As String, monto As String, precio As String, UsuarioID As String, problema As String) As Integer
        Dim resp As Boolean = True
        resp = Me.Insert("factura_reparacion_detalle", "equipo_id     ,cantidad      ,monto   ,precio     ,fecha ,usuario_id , estado,factura_reparacion_id   ,problema   ", "'" + equipoID + "','" + cantidad + "','" + monto + "','" + precio + "'," + Me._Fecha + ",'" + UsuarioID + "','pendiente','" + facturaID + "','" + problema + "' ")
        If resp = False Then
            MessageBox.Show("No se inserto el registro de forma correcta")
        End If
        ' Me.QueryFree("update equipos set cantidad_almacen = cantidad_almacen - " + cantidad + "   where id = '" + equipoID + "'  ")
        Return resp
    End Function
    Public Function InsertarPago(facturaVentaID As String, facturaReparacionID As String, tipoFactura As String, montoPagado As String, devuelta As String, UsuarioID As String, tipoPago As String) As Integer
        Dim resp As Boolean = True
        resp = Me.Insert("pagos", "tipo_factura ,factura_reparacion_id ,factura_venta_id ,monto_pagado  ,devuelta ,fecha ,usuario_id , tipo_pago ", "'" + tipoFactura + "','" + facturaReparacionID + "','" + facturaVentaID + "','" + montoPagado + "','" + devuelta + "'," + Me._Fecha + ",'" + UsuarioID + "','" + tipoPago + "' ")
        If resp = False Then
            MessageBox.Show("No se inserto el registro de forma correcta")
        End If
        Return resp
    End Function

    Public Sub MostrarClientesListBox(combo As ListBox)
        Dim respuesta As Boolean = Me.Mostrar("clientes", " * ", "  visible = 1 ")
        If respuesta Then
            If Me._lector.HasRows Then
                combo.Items.Clear()
                Try
                    combo.Items.Add(" No seleccionado ")
                    Me.ResultArrayList.Add("0")
                    While Me._lector.Read()
                        'MessageBox.Show(" Codigo: " + Me._lector("id").ToString())
                        Dim idCliente As String = Me._lector("id").ToString()
                        combo.Items.Add(" Codigo: " + idCliente + " Cliente " + Me._lector("nombre").ToString() + " " + Me._lector("apellido").ToString() + " " + Me._lector("cedula").ToString() + " " + Me._lector("rnc").ToString())
                        Me.ResultArrayList.Add(idCliente)
                    End While
                    combo.SelectedIndex = 0
                Catch ex As Exception
                    MessageBox.Show("Error consiguiendo los datos para combo " + ex.ToString())
                End Try
            Else
                MessageBox.Show("No hay equipos")
            End If
        Else
            MessageBox.Show("No se pudo mostrar ")
        End If

    End Sub
    Public Sub MostrarUsuariosListBox(combo As ListBox)
        Dim respuesta As Boolean = Me.Mostrar("usuarios", " * ", " tipo <> 'administrador' and visible = 1 ")
        If respuesta Then
            If Me._lector.HasRows Then
                combo.Items.Clear()
                Try
                    combo.Items.Add(" No seleccionado ")
                    Me.ResultArrayList.Add("0")
                    While Me._lector.Read()
                        'MessageBox.Show(" Codigo: " + Me._lector("id").ToString())
                        Dim idCliente As String = Me._lector("id").ToString()
                        combo.Items.Add(" Codigo: " + idCliente + " Usuario: " + Me._lector("usuario").ToString())
                        Me.ResultArrayList.Add(idCliente)
                    End While
                    combo.SelectedIndex = 0
                Catch ex As Exception
                    MessageBox.Show("Error consiguiendo los datos para combo " + ex.ToString())
                End Try
            Else
                MessageBox.Show("No hay equipos")
            End If
        Else
            MessageBox.Show("No se pudo mostrar ")
        End If

    End Sub

    Public Sub MostrarEquiposListBox(combo As ListBox)
        Dim respuesta As Boolean = Me.Mostrar("equipos", " * ", "   visible = 1 ")
        If respuesta Then
            If Me._lector.HasRows Then
                combo.Items.Clear()
                Try
                    combo.Items.Add(" No seleccionado ")
                    Me.ResultArrayList.Add("0")
                    While Me._lector.Read()
                        'MessageBox.Show(" Codigo: " + Me._lector("id").ToString())
                        Dim idEquipo As String = Me._lector("id").ToString()
                        combo.Items.Add(" Codigo: " + idEquipo + " Producto: " + Me._lector("marca").ToString() + " " + Me._lector("modelo").ToString())
                        Me.ResultArrayList.Add(idEquipo)
                    End While
                    combo.SelectedIndex = 0
                Catch ex As Exception
                    MessageBox.Show("Error consiguiendo los datos para combo " + ex.ToString())
                End Try
            Else
                MessageBox.Show("No hay equipos")
            End If
        Else
            MessageBox.Show("No se pudo mostrar ")
        End If

    End Sub

    Public Sub MostrarReporteDatos(tabla As String, where As String)
        Dim respuesta As Boolean = Me.Mostrar(tabla, " f.*,concat(c.nombre,' ',c.apellido) as nombrecompleto, u.usuario ", where)
        If respuesta Then
            If Me._lector.HasRows Then
                Try
                    Me.ResultArrayList.Clear()
                    While Me._lector.Read()
                        ' MessageBox.Show(" Fid " + Me._lector("id").ToString())
                        Me.ResultArrayList.Add(New ModeloReporteFactura(Me._lector("usuario").ToString(), Me._lector("nombrecompleto").ToString(), Me._lector("id").ToString(), Me._lector("sub_total").ToString(), Me._lector("monto_itbis").ToString(), Me._lector("monto_apagar").ToString(), Me._lector("fecha").ToString()))
                    End While
                Catch ex As Exception
                    MessageBox.Show("Error consiguiendo los datos cliente " + ex.ToString())
                End Try
            Else
                MessageBox.Show("No hay registros ")
            End If
        Else
            MessageBox.Show("No se pudo mostrar ")
        End If

    End Sub

    Public Function ObtenerDataFacturaVentas(id As String) As String()
        Dim respuesta As Boolean = Me.Mostrar("factura_ventas", " * ", " id = '" + id + "'  and visible = 1 ")
        If respuesta Then
            If Me._lector.HasRows Then
                Try
                    If Me._lector.Read() Then
                        'Me._lector("id"), Me._lector("usuario"), Me._lector("tipo")
                        Dim Data(4) As String
                        Data(0) = Me._lector("id")
                        Data(1) = Me._lector("fecha")
                        'Data(2) = Me._lector("tipo")
                        'Data(3) = Me._lector("clave")
                        Return Data
                    End If
                Catch ex As Exception
                    MessageBox.Show("Error consiguiendo los datos " + ex.ToString())
                End Try
            Else
                MessageBox.Show("No hay registros ")
                Return {"", "", ""}

            End If
        Else
            MessageBox.Show("No se pudo mostrar ")
            Return {"", "", ""}

        End If
        Return {"", "", ""}

    End Function

    Public Function ObtenerDataFacturaReparacion(id As String) As String()
        Dim respuesta As Boolean = Me.Mostrar("factura_reparacion", " * ", " id = '" + id + "'  and visible = 1 ")
        If respuesta Then
            If Me._lector.HasRows Then
                Try
                    If Me._lector.Read() Then
                        'Me._lector("id"), Me._lector("usuario"), Me._lector("tipo")
                        Dim Data(4) As String
                        Data(0) = Me._lector("id")
                        Data(1) = Me._lector("fecha")
                        'Data(2) = Me._lector("tipo")
                        'Data(3) = Me._lector("clave")
                        Return Data
                    End If
                Catch ex As Exception
                    MessageBox.Show("Error consiguiendo los datos " + ex.ToString())
                End Try
            Else
                MessageBox.Show("No hay registros ")
                Return {"", "", ""}

            End If
        Else
            MessageBox.Show("No se pudo mostrar ")
            Return {"", "", ""}

        End If
        Return {"", "", ""}

    End Function

    Public Function ObtenerDataEquipo(id As String) As String()
        Dim respuesta As Boolean = Me.Mostrar("equipos", " * ", " id = '" + id + "'  and visible = 1 ")
        If respuesta Then
            If Me._lector.HasRows Then
                Try
                    If Me._lector.Read() Then
                        'Me._lector("id"), Me._lector("usuario"), Me._lector("tipo")
                        Dim Data(6) As String
                        Data(0) = Me._lector("id")
                        Data(1) = Me._lector("marca")
                        Data(2) = Me._lector("modelo")
                        Data(3) = Me._lector("cantidad_almacen")
                        Data(4) = Me._lector("precio_venta")

                        Return Data
                    End If
                Catch ex As Exception
                    MessageBox.Show("Error consiguiendo los datos " + ex.ToString())
                End Try
            Else
                MessageBox.Show("No hay registros ")
                Return {"", "", "", "", "", ""}

            End If
        Else
            MessageBox.Show("No se pudo mostrar ")
            Return {"", "", "", "", "", ""}

        End If
        Return {"", "", "", "", "", ""}

    End Function
End Class
