Imports TipTracker.Core

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
        ''' Gets the <see cref="TipType"/> instance represented by a specified string.
        ''' </summary>
        ''' <param name="value">The value to parse.</param>
        ''' <returns>The tip type represented by the specified string.</returns>
        ''' <exception cref="ArgumentException">If the specified value does not represent a valid tip type.</exception>
        Public Shared Function Parse(value As String) As TipType
            Select Case value
                Case CreditCard.Name
                    Return CreditCard
                Case RoomCharge.Name
                    Return RoomCharge
                Case Cash.Name
                    Return Cash
                Case SpecialFunction.Name
                    Return SpecialFunction
                Case Else
                    Throw New ArgumentException($"The specified value {value} does not correspond to a valid tip type.")
            End Select
        End Function

        ''' <summary>
        ''' Defines tips received from credit card sales.
        ''' </summary>
        Public Shared CreditCard As New TipType("Credit Card", TipClassification.ChargeTips, True, False)

        ''' <summary>
        ''' Defines tips received from room charge sales.
        ''' </summary>
        Public Shared RoomCharge As New TipType("Room Charge", TipClassification.ChargeTips, True, False)

        ''' <summary>
        ''' Defines tips received from special events.
        ''' </summary>
        Public Shared SpecialFunction As New TipType("Special Function", TipClassification.ChargeTips, False, True)

        ''' <summary>
        ''' Defines tips received from cash sales.
        ''' </summary>
        Public Shared Cash As New TipType("Cash", TipClassification.CashTips, False, False)

        ''' <summary>
        ''' Gets a collection containing all defined tip types.
        ''' </summary>
        ''' <returns>An enumerable of defined tip types.</returns>
        Public Shared ReadOnly Iterator Property Values As IEnumerable(Of TipType)
            Get
                Yield CreditCard
                Yield RoomCharge
                Yield SpecialFunction
                Yield Cash
            End Get
        End Property
    End Class
End Namespace