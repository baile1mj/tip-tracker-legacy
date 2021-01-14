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
        ''' Gets a value indicating whether the user can set the date for this type.
        ''' </summary>
        ''' <returns>True if dates for tips of this type are use-specified; otherwise, false.</returns>
        Public ReadOnly Property CanSpecifyDate As Boolean

        ''' <summary>
        ''' Gets a value indicating whether the tip originates from an event.
        ''' </summary>
        ''' <returns>True if tips of this type must be linked to an event; otherwise, false.</returns>
        Public ReadOnly Property IsEventOriginated As Boolean

        ''' <summary>
        ''' Creates a new tip type.
        ''' </summary>
        ''' <param name="name">The name of the type.</param>
        ''' <param name="canSpecifyDate">True if the tip type uses a system-calculated date; otherwise, false.</param>
        ''' <param name="isEventOriginated">True if the tip type originates from an event.</param>
        Private Sub New(name As String, canSpecifyDate As Boolean, isEventOriginated As Boolean)
            Me.Name = name
            Me.CanSpecifyDate = canSpecifyDate
            Me.IsEventOriginated = isEventOriginated
        End Sub

        ''' <inheritdoc/>
        Public Overrides Function ToString() As String
            Return Name
        End Function

        ''' <summary>
        ''' Defines tips received from credit card sales.
        ''' </summary>
        Public Shared CreditCard As New TipTypes("Credit Card", True, False)

        ''' <summary>
        ''' Defines tips received from room charge sales.
        ''' </summary>
        Public Shared RoomCharge As New TipTypes("Room Charge", True, False)

        ''' <summary>
        ''' Defines tips received from special events.
        ''' </summary>
        Public Shared SpecialFunction As New TipTypes("Special Function", False, True)

        ''' <summary>
        ''' Defines tips received from cash sales.
        ''' </summary>
        Public Shared Cash As New TipTypes("Cash", False, False)

        ''' <summary>
        ''' Gets a collection containing all defined tip types.
        ''' </summary>
        ''' <returns>An enumerable of defined tip types.</returns>
        Public Shared ReadOnly Iterator Property Values As IEnumerable(Of TipTypes)
            Get
                Yield CreditCard
                Yield RoomCharge
                Yield SpecialFunction
                Yield Cash
            End Get
        End Property
    End Class
End Namespace