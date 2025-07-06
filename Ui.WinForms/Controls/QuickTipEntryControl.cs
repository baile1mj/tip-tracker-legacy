using TipTracker.Ui.DataObjects;

namespace TipTracker.Ui.Controls;

/// <summary>
/// Allows quick entry of tip data.
/// </summary>
public partial class QuickEntryForm : UserControl
{
    /// <summary>
    /// Creates a new instance of the entry form control.
    /// </summary>
    public QuickEntryForm()
    {
        InitializeComponent();

        // Set up keyboard quick actions.
        txtServer.KeyPress += HandleKeyPress;
        txtAmount.KeyPress += HandleKeyPress;

        // Add validation for controls.
        txtServer.LostFocus += TxtServer_LostFocus;
        txtAmount.LostFocus += TxtAmount_LostFocus;
        txtServerName.GotFocus += PreventNameSelection;
    }

    /// <summary>
    /// Gets the server that was entered.
    /// </summary>
    public Server? Server => txtServerName.Tag as Server;

    /// <summary>
    /// Gets the amount that was entered.
    /// </summary>
    public decimal? Amount => txtAmount.Tag as decimal?;

    /// <summary>
    /// Sets the selected <see cref="Server"/> object instance.
    /// </summary>
    /// <param name="server">The server to set.</param>
    public void SetServer(Server server)
    {
        ArgumentNullException.ThrowIfNull(server);
        txtServerName.Text = server.FullName();
        txtServerName.Tag = server;
    }

    /// <summary>
    /// Sets the tip amount.
    /// </summary>
    /// <param name="value">The numeric value to set.</param>
    /// <param name="format">A string used to format the numeric value for display.</param>
    public void SetTipAmount(decimal value, string format)
    {
        txtAmount.Tag = value;
        txtAmount.Text = value.ToString(format);
    }

    /// <summary>
    /// Clears the form.
    /// </summary>
    public void ClearForm()
    {
        txtServer.Clear();
        txtAmount.Clear();
        txtServerName.Clear();
        txtServerName.Tag = null;
        txtServer.Focus();
    }

    /// <summary>
    /// The event that is fired when a valid tip is entered.
    /// </summary>
    public event EventHandler<PropertyValidationEventArgs> NewTipEntered;

    /// <summary>
    /// The event that is fired when the user enters a server number.
    /// </summary>
    public event EventHandler<InputValidationEventArgs<string>> ServerNumberEntered;

    /// <summary>
    /// The event that is fired when the user enters a tip amount.
    /// </summary>
    public event EventHandler<InputValidationEventArgs<string>> TipAmountEntered;

    /// <summary>
    /// Event handler to clear the selected server or fire the <see cref="ServerNumberEntered"/>
    /// event, depending on what was entered into <see cref="txtServer"/> before it lost focus.
    /// </summary>
    /// <param name="sender">The object firing the event.</param>
    /// <param name="e">Arguments associated with the event.</param>
    private void TxtServer_LostFocus(object? sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtServer.Text) || btnClear.Focused)
        {
            txtServerName.Tag = null;
            return;
        }

        var validationArgs = InputValidationEventArgs.ForValue(txtServer.Text);
        ServerNumberEntered.Invoke(this, validationArgs);

        if (validationArgs.IsInputValid) { return; }

        // Server is not valid, so clear the input.
        txtServer.Clear();
        txtServerName.Tag = null;
        txtServer.Focus();
    }

    /// <summary>
    /// Event handler that fires the <see cref="TipAmountEntered"/> event when
    /// <see cref="txtAmount"/> loses focus.
    /// </summary>
    /// <param name="sender">The object firing the event.</param>
    /// <param name="e">Arguments associated with the event.</param>
    private void TxtAmount_LostFocus(object? sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtAmount.Text) || btnClear.Focused) { return; }
        ValidateTipAmount();
    }

    /// <summary>
    /// Event handler to redirect focus to the server number input when the server name input is selected.
    /// </summary>
    /// <param name="sender">The object instance firing this event.</param>
    /// <param name="e">Arguments associated with the event.</param>
    private void PreventNameSelection(object? sender, EventArgs e)
    {
        txtServer.Focus();
    }

    /// <summary>
    /// Handles the KeyPress event for controls that support keyboard quick entry.
    /// </summary>
    /// <param name="sender">The object instance firing this event.</param>
    /// <param name="e">Arguments associated with the event.</param>
    /// <exception cref="InvalidOperationException"></exception>
    private void HandleKeyPress(object? sender, KeyPressEventArgs e)
    {
        const char ENTER_KEY = (char)Keys.Enter;

        if (e.KeyChar != ENTER_KEY) { return; }

        if (sender == txtServer)
        {
            txtAmount.Focus();
        }
        else if (sender == txtAmount)
        {
            // Take the focus away from txtAmount, which will fire amount validation.  If btnAdd still
            // has focus after validation, then we can proceed to perform the click.
            btnAdd.Focus();

            if (btnAdd.Focused)
            {
                btnAdd.PerformClick();
            }
        }
        else
        {
            throw new InvalidOperationException("Keyboard quick actions not supported.");
        }
    }

    /// <summary>
    /// Validates the tip amount and clears the input if it is invalid.
    /// </summary>
    private void ValidateTipAmount()
    {
        var validationArgs = InputValidationEventArgs.ForValue(txtAmount.Text);
        TipAmountEntered.Invoke(this, validationArgs);

        if (validationArgs.IsInputValid) { return; }

        txtAmount.Clear();
        txtAmount.Focus();
    }

    /// <summary>
    /// Event handler that validates the input and raises the <see cref="NewTipEntered"/> event
    /// when the Add button is clicked.
    /// </summary>
    /// <param name="sender">The object instance firing this event.</param>
    /// <param name="e">Arguments associated with the event.</param>
    private void btnAdd_Click(object sender, EventArgs e)
    {
        var eventArgs = new PropertyValidationEventArgs();
        NewTipEntered.Invoke(this, eventArgs);

        if (eventArgs.IsValid) {
            ClearForm();
            return; 
        }

        switch (eventArgs.PropertyName)
        {
            case nameof(Server):
                txtServer.Focus();
                break;
            case nameof(Amount):
                txtAmount.Focus();
                break;
            default:
                throw new InvalidOperationException("Provided property name does not exist on the control.");
        }
    }

    /// <summary>
    /// Event handler that clears the entry control when the Clear button is clicked.
    /// </summary>
    /// <param name="sender">The object instance firing this event.</param>
    /// <param name="e">Arguments associated with the event.</param>
    private void btnClear_Click(object sender, EventArgs e)
    {
        ClearForm();
    }
}