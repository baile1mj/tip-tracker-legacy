using TipTracker.Ui.ViewData;

namespace TipTracker.Ui.Controls;

/// <summary>
/// Allows quick entry of tips of a single type.
/// </summary>
public partial class QuickTipEditor : UserControl
{
    private readonly bool _autoInsertDecimal = true;

    /// <summary>
    /// Creates a new instance of the form.
    /// </summary>
    public QuickTipEditor()
    {
        InitializeComponent();
        tipEntryForm.ServerNumberEntered += TipEntryForm_ServerNumberEntered;
        tipEntryForm.TipAmountEntered += TipEntryForm_TipAmountEntered;
        tipEntryForm.NewTipEntered += TipEntryForm_NewTipEntered;
        tipBindingSource.DataSource = new SortableBindingList<TipView>(new List<TipView>(), 
            TipViewSortComparer.Create);
    }

    /// <summary>
    /// The event that is fired when an error occurs that requires standard error handling.
    /// </summary>
    public event EventHandler<Error> ErrorOccurred;

    /// <summary>
    /// The event that is fired when a server number needs to be resolved to a 
    /// server instance.
    /// </summary>
    public event EventHandler<ServerQueryEventArgs> ServerQuery;

    /// <summary>
    /// Updates the total of all tips that have been entered.
    /// </summary>
    private void UpdateTotal()
    {
        var total = tipBindingSource
            .Cast<TipView>()
            .Sum(t => t.Amount);
        lblTotal.Text = $"Total: {total:c}";
    }

    /// <summary>
    /// Handles the <see cref="QuickEntryForm.ServerNumberEntered"/> event.
    /// </summary>
    /// <param name="sender">The event sender.</param>
    /// <param name="e">The server number that was entered.</param>
    private void TipEntryForm_ServerNumberEntered(object? sender, InputValidationEventArgs<string> e)
    {
        var query = new ServerQueryEventArgs(e.Input);
        ServerQuery.Invoke(this, query);
        var server = query.LookupResult;

        if (server == null)
        {
            e.Invalidate();
            ErrorOccurred.Invoke(this, new Error("The server number you entered does not belong to an active server.", "Server Not Found"));
        }
        else
        {
            tipEntryForm.SetServer(server);
        }
    }

    /// <summary>
    /// Handles the <see cref="QuickEntryForm.TipAmountEntered"/> event.
    /// </summary>
    /// <param name="sender">The event sender.</param>
    /// <param name="e">Arguments associated with the event.</param>
    private void TipEntryForm_TipAmountEntered(object? sender, InputValidationEventArgs<string> e)
    {
        var isNumber = decimal.TryParse(e.Input, out var result);

        if (!isNumber)
        {
            e.Invalidate();
            ErrorOccurred.Invoke(this, new Error("The tip amount must be a number.", "Invalid Amount"));
            return;
        }

        if (result == 0)
        {
            e.Invalidate();
            ErrorOccurred.Invoke(this, new Error("You may not enter a zero dollar tip.", "Invalid Amount"));
            return;
        }

        if (_autoInsertDecimal) { result /= 100; }
        tipEntryForm.SetTipAmount(result, "0.00");
    }

    /// <summary>
    /// Handles the <see cref="QuickEntryForm.NewTipEntered"/> event.
    /// </summary>
    /// <param name="sender">The event sender.</param>
    /// <param name="e">Arguments associated with the event.</param>
    private void TipEntryForm_NewTipEntered(object? sender, PropertyValidationEventArgs e)
    {
        var selectedServer = tipEntryForm.Server;
        var amount = tipEntryForm.Amount;

        if (selectedServer == null)
        {
            e.ValidationFailedFor(nameof(tipEntryForm.Server));
            ErrorOccurred.Invoke(this, new Error("You must enter a server number.", "Invalid Server"));
        }
        else if (amount == null)
        {
            e.ValidationFailedFor(nameof(tipEntryForm.Amount));
            ErrorOccurred.Invoke(this, new Error("You must enter a tip amount.", "Invalid Amount"));
        }
        else
        {
            var newTip = new TipView(tipBindingSource.Count + 1, selectedServer, amount.Value);
            tipBindingSource.Add(newTip);
            tipBindingSource.Position = tipBindingSource.IndexOf(newTip);
            
            UpdateTotal();
        }
    }
}