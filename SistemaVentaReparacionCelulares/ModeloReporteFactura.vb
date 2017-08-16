Public Class ModeloReporteFactura

    Private _Usuario As String
    Private _Cliente As String
    Private _NoFactura As String
    Private _SubTotal As String
    Private _Itbis As String
    Private _Monto As String
    Private _Fecha As String


    Sub New(ByVal usuario As String, ByVal cliente As String, ByVal noFactura As String, ByVal subTotal As String, ByVal itbis As String, ByVal monto As String, ByVal fecha As String)
        Me._Usuario = usuario
        Me._Cliente = cliente
        Me._NoFactura = noFactura
        Me._SubTotal = subTotal
        Me._Itbis = itbis
        Me._Monto = monto
        Me._Fecha = fecha
    End Sub

    Public Property Usuario() As String
        Set(value As String)
            Me._Usuario = value
        End Set
        Get
            Return Me._Usuario
        End Get
    End Property

    Public Property Cliente() As String
        Set(value As String)
            Me._Cliente = value
        End Set
        Get
            Return Me._Cliente
        End Get
    End Property

    Public Property NoFactura() As String
        Set(value As String)
            Me._NoFactura = value
        End Set
        Get
            Return Me._NoFactura
        End Get
    End Property

    Public Property SubTotal() As String
        Set(value As String)
            Me._SubTotal = value
        End Set
        Get
            Return Me._SubTotal
        End Get
    End Property

    Public Property Itbis() As String
        Set(value As String)
            Me._Itbis = value
        End Set
        Get
            Return Me._Itbis
        End Get
    End Property

    Public Property Monto() As String
        Set(value As String)
            Me._Monto = value
        End Set
        Get
            Return Me._Monto
        End Get
    End Property

    Public Property Fecha() As String
        Set(value As String)
            Me._Fecha = value
        End Set
        Get
            Return Me._Fecha
        End Get
    End Property


End Class
