Namespace Utilities
    ''' <summary>
    ''' The collection of tip types supported by the application.
    ''' </summary>
    Public NotInheritable Class TipTypes
        ''' <summary>
        ''' Gets the name of the tip type.
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property Name As String

        ''' <summary>
        ''' Creates a new tip type.
        ''' </summary>
        ''' <param name="name">The name of the type.</param>
        Private Sub New(name As String)
            Me.Name = name
        End Sub

        ''' <summary>
        ''' Defines tips received from credit card sales.
        ''' </summary>
        Public Shared CreditCard As New TipTypes("Credit Card")

        ''' <summary>
        ''' Defines tips received from room charge sales.
        ''' </summary>
        Public Shared RoomCharge As New TipTypes("Room Charge")

        ''' <summary>
        ''' Defines tips received from special events.
        ''' </summary>
        Public Shared SpecialFunction As New TipTypes("Special Function")

        ''' <summary>
        ''' Defines tips received from cash sales.
        ''' </summary>
        Public Shared Cash As New TipTypes("Cash")
    End Class
End Namespace