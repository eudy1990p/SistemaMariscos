Public Class ModeloVentaReporte
    Private _Codigo As String
    Private _Producto As String
    Private _Cantidad As String
    Private _Precio As String
    Private _Total As String


    Sub New(ByVal codigo As String, ByVal producto As String, ByVal cantidad As String, ByVal precio As String, ByVal total As String)
        Me._Cantidad = cantidad
        Me._Precio = precio
        Me._Total = total
        Me._Producto = producto
        Me._Codigo = codigo
    End Sub

    Public Property Codigo() As String
        Set(value As String)
            Me._Codigo = value
        End Set
        Get
            Return Me._Codigo
        End Get
    End Property

    Public Property Producto() As String
        Set(value As String)
            Me._Producto = value
        End Set
        Get
            Return Me._Producto
        End Get
    End Property

    Public Property Cantidad() As String
        Set(value As String)
            Me._Cantidad = value
        End Set
        Get
            Return Me._Cantidad
        End Get
    End Property

    Public Property Precio() As String
        Set(value As String)
            Me._Precio = value
        End Set
        Get
            Return Me._Precio
        End Get
    End Property

    Public Property Total() As String
        Set(value As String)
            Me._Total = value
        End Set
        Get
            Return Me._Total
        End Get
    End Property

End Class
