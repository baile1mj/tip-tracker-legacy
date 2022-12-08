Imports System.Linq
Imports TipTracker.Core
Imports TipTracker.Utilities

Public Class frmManageSpecialFunctions
    Private ReadOnly _functions As IList(Of SpecialEvent)
    Private ReadOnly _dataStore As IDataStore(Of SpecialEvent)

    Public Function GetChanges() As List(Of SpecialEvent)
        Return New List(Of SpecialEvent)(_functions)
    End Function

    Public Sub New(dataStore As IDataStore(Of SpecialEvent))
        _dataStore = dataStore
        InitializeComponent()
        _functions = New SortableBindingList(Of SpecialEvent)(dataStore.GetAll().ToList())
    End Sub

    Private Sub frmManageSpecialFunctions_Load(sender As System.Object, e As EventArgs) Handles MyBase.Load
        EventBindingSource.DataSource = _functions
        EventBindingSource.Sort = NameOf(SpecialEvent.Name)
    End Sub

    Private Sub btnClose_Click(sender As System.Object, e As EventArgs) Handles btnClose.Click
        Close()
    End Sub

    Private Sub btnAdd_Click(sender As System.Object, e As EventArgs) Handles btnAdd.Click
        Dim inputDialog As New frmAddSpecialFunction()

        While inputDialog.ShowDialog() = DialogResult.OK
            If _functions.Any(Function(evt) evt.Name = inputDialog.EventName) Then
                Dim message = $"The pay period already contains a function called ""{inputDialog.EventName}"".  Please enter a different name."
                MessageBox.Show(message, "Invalid Function Name", MessageBoxButtons.OK)
            Else
                Dim newEvent = New SpecialEvent With {
                    .Name = inputDialog.EventName,
                    .[Date] = inputDialog.EventDate}

                _functions.Add(newEvent)
                _dataStore.Add(newEvent)
                EventBindingSource.Position = _functions.IndexOf(newEvent)

                Exit While
            End If
        End While

        inputDialog.Dispose()
    End Sub

    Private Sub btnDelete_Click(sender As System.Object, e As EventArgs) Handles btnDelete.Click
        Dim selectedFunction = DirectCast(EventBindingSource.Current, SpecialEvent)
        Dim confirmation  = $"You are about to delete the function ""{selectedFunction.Name}"" that occurred on {selectedFunction.Date:M/d/yyyy}.  "

        If selectedFunction.Tips.Count > 0 Then confirmation &= $"This will delete {selectedFunction.Tips.Count} asociated tips as well.  "
        
        confirmation &= "Do you wish to continue?"
        
        If MessageBox.Show(confirmation, "Confirm Delete", MessageBoxButtons.YesNo) = DialogResult.Yes Then
            EventBindingSource.RemoveCurrent()
            _dataStore.Delete(selectedFunction)
        End If
    End Sub

    Private Sub btnEdit_Click(sender As System.Object, e As EventArgs) Handles btnEdit.Click
        Dim original  = _functions(SpecialFunctionsDataGridView.CurrentRow.Index)
        Dim updated = original.Clone()
        Dim editDialog As New frmAddSpecialFunction(updated.Name, updated.Date)

        While editDialog.ShowDialog() = DialogResult.OK
            If _functions.Any(Function(evt) evt.Name = editDialog.EventName) Then
                Dim message = $"The pay period already contains a function called ""{editDialog.EventName}"".  Please enter a different name."
                MessageBox.Show(message, "Invalid Function Name", MessageBoxButtons.OK)
            Else
                updated.Name = editDialog.EventName
                updated.Date = editDialog.EventDate
                
                _functions.Remove(original)
                _functions.Add(updated)
                _dataStore.Update(original, updated)
                EventBindingSource.Position = _functions.IndexOf(updated)
                
                Exit While
            End If
        End While

        editDialog.Dispose()
    End Sub
End Class