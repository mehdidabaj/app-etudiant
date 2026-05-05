using System.Drawing;
using System.Windows.Forms;
using GestionCommandes.App.Services;

namespace GestionCommandes.App.Forms;

public class FormClient : Form
{
    private readonly ClientService _clientService;
    private readonly DataGridView _gridClients = new();
    private readonly TextBox _txtNom = new();
    private readonly TextBox _txtEmail = new();

    public FormClient(ClientService clientService)
    {
        _clientService = clientService;
        InitializeComponent();
        LoadClients();
    }

    private void InitializeComponent()
    {
        Text = "Gestion des clients";
        Width = 700;
        Height = 450;
        StartPosition = FormStartPosition.CenterParent;

        var layout = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 2,
            RowCount = 4,
            Padding = new Padding(16)
        };

        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120));
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 36));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 36));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 46));
        layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

        layout.Controls.Add(new Label { Text = "Nom", Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleLeft }, 0, 0);
        layout.Controls.Add(_txtNom, 1, 0);
        layout.Controls.Add(new Label { Text = "Email", Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleLeft }, 0, 1);
        layout.Controls.Add(_txtEmail, 1, 1);

        var btnAjouter = new Button { Text = "Ajouter client", Width = 140 };
        btnAjouter.Click += BtnAjouter_Click;
        layout.Controls.Add(btnAjouter, 1, 2);

        _gridClients.Dock = DockStyle.Fill;
        _gridClients.ReadOnly = true;
        _gridClients.AutoGenerateColumns = true;
        _gridClients.AllowUserToAddRows = false;
        layout.Controls.Add(_gridClients, 0, 3);
        layout.SetColumnSpan(_gridClients, 2);

        Controls.Add(layout);
    }

    private void BtnAjouter_Click(object? sender, EventArgs e)
    {
        try
        {
            _clientService.AddClient(_txtNom.Text, _txtEmail.Text);
            _txtNom.Clear();
            _txtEmail.Clear();
            LoadClients();
        }
        catch (ArgumentException exception)
        {
            MessageBox.Show(exception.Message, "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    private void LoadClients()
    {
        _gridClients.DataSource = _clientService.GetClients()
            .Select(client => new
            {
                client.Id,
                client.Nom,
                client.Email
            })
            .ToList();
    }
}
