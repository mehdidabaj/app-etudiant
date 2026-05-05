using System.Drawing;
using System.Windows.Forms;
using GestionCommandes.App.Models;
using GestionCommandes.App.Services;

namespace GestionCommandes.App.Forms;

public class FormCommande : Form
{
    private readonly ClientService _clientService;
    private readonly ProductService _productService;
    private readonly CommandeService _commandeService;
    private readonly ComboBox _clientsComboBox = new();
    private readonly ComboBox _produitsComboBox = new();
    private readonly NumericUpDown _quantiteNumeric = new();
    private readonly DataGridView _lignesGrid = new();
    private readonly DataGridView _commandesGrid = new();
    private readonly BindingSource _lignesSource = new();
    private readonly BindingSource _commandesSource = new();
    private readonly List<LigneCommandeDraft> _lignes = new();

    public FormCommande(
        ClientService clientService,
        ProductService productService,
        CommandeService commandeService)
    {
        _clientService = clientService;
        _productService = productService;
        _commandeService = commandeService;

        Text = "Gestion des commandes";
        Width = 980;
        Height = 620;
        StartPosition = FormStartPosition.CenterParent;

        BuildLayout();
        LoadData();
    }

    private void BuildLayout()
    {
        var root = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 2,
            RowCount = 1,
            Padding = new Padding(12)
        };
        root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 42));
        root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 58));

        root.Controls.Add(BuildCreationPanel(), 0, 0);
        root.Controls.Add(BuildConsultationPanel(), 1, 0);
        Controls.Add(root);
    }

    private Control BuildCreationPanel()
    {
        var group = new GroupBox
        {
            Text = "Creer une commande",
            Dock = DockStyle.Fill
        };

        var layout = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 2,
            RowCount = 7,
            Padding = new Padding(12)
        };
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35));
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 34));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 34));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 34));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 42));
        layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 44));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 10));

        _clientsComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        _produitsComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        _quantiteNumeric.Minimum = 1;
        _quantiteNumeric.Maximum = 1000;
        _quantiteNumeric.Value = 1;

        var ajouterLigneButton = new Button
        {
            Text = "Ajouter produit a commande",
            Dock = DockStyle.Fill
        };
        ajouterLigneButton.Click += (_, _) => AddLine();

        _lignesGrid.AutoGenerateColumns = false;
        _lignesGrid.AllowUserToAddRows = false;
        _lignesGrid.ReadOnly = true;
        _lignesGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        _lignesGrid.Dock = DockStyle.Fill;
        _lignesGrid.Columns.Add(new DataGridViewTextBoxColumn
        {
            HeaderText = "Produit",
            DataPropertyName = nameof(LigneCommandeDraft.ProduitNom),
            AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        });
        _lignesGrid.Columns.Add(new DataGridViewTextBoxColumn
        {
            HeaderText = "Quantite",
            DataPropertyName = nameof(LigneCommandeDraft.Quantite),
            Width = 80
        });
        _lignesGrid.Columns.Add(new DataGridViewTextBoxColumn
        {
            HeaderText = "Prix",
            DataPropertyName = nameof(LigneCommandeDraft.PrixUnitaire),
            Width = 90
        });

        var creerCommandeButton = new Button
        {
            Text = "Creer commande",
            Dock = DockStyle.Fill
        };
        creerCommandeButton.Click += (_, _) => CreateCommande();

        layout.Controls.Add(new Label { Text = "Client", TextAlign = ContentAlignment.MiddleLeft }, 0, 0);
        layout.Controls.Add(_clientsComboBox, 1, 0);
        layout.Controls.Add(new Label { Text = "Produit", TextAlign = ContentAlignment.MiddleLeft }, 0, 1);
        layout.Controls.Add(_produitsComboBox, 1, 1);
        layout.Controls.Add(new Label { Text = "Quantite", TextAlign = ContentAlignment.MiddleLeft }, 0, 2);
        layout.Controls.Add(_quantiteNumeric, 1, 2);
        layout.Controls.Add(ajouterLigneButton, 1, 3);
        layout.Controls.Add(_lignesGrid, 0, 4);
        layout.SetColumnSpan(_lignesGrid, 2);
        layout.Controls.Add(creerCommandeButton, 1, 5);

        group.Controls.Add(layout);
        return group;
    }

    private Control BuildConsultationPanel()
    {
        var group = new GroupBox
        {
            Text = "Consulter commandes",
            Dock = DockStyle.Fill
        };

        _commandesGrid.AutoGenerateColumns = false;
        _commandesGrid.AllowUserToAddRows = false;
        _commandesGrid.ReadOnly = true;
        _commandesGrid.Dock = DockStyle.Fill;
        _commandesGrid.Columns.Add(new DataGridViewTextBoxColumn
        {
            HeaderText = "Id",
            DataPropertyName = nameof(Commande.Id),
            Width = 60
        });
        _commandesGrid.Columns.Add(new DataGridViewTextBoxColumn
        {
            HeaderText = "Date",
            DataPropertyName = nameof(Commande.Date),
            Width = 130
        });
        _commandesGrid.Columns.Add(new DataGridViewTextBoxColumn
        {
            HeaderText = "Client",
            DataPropertyName = nameof(CommandeListItem.ClientNom),
            AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        });
        _commandesGrid.Columns.Add(new DataGridViewTextBoxColumn
        {
            HeaderText = "Total",
            DataPropertyName = nameof(CommandeListItem.Total),
            Width = 100
        });

        group.Controls.Add(_commandesGrid);
        return group;
    }

    private void LoadData()
    {
        _clientsComboBox.DataSource = _clientService.GetClients().ToList();
        _clientsComboBox.DisplayMember = nameof(Client.Nom);
        _clientsComboBox.ValueMember = nameof(Client.Id);

        _produitsComboBox.DataSource = _productService.GetAll().ToList();
        _produitsComboBox.DisplayMember = nameof(Produit.Nom);
        _produitsComboBox.ValueMember = nameof(Produit.Id);

        _lignesSource.DataSource = _lignes;
        _lignesGrid.DataSource = _lignesSource;

        RefreshCommandes();
    }

    private void AddLine()
    {
        if (_produitsComboBox.SelectedItem is not Produit produit)
        {
            MessageBox.Show("Veuillez selectionner un produit.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        _lignes.Add(new LigneCommandeDraft(produit.Id, produit.Nom, (int)_quantiteNumeric.Value, produit.Prix));
        _lignesSource.ResetBindings(false);
    }

    private void CreateCommande()
    {
        try
        {
            if (_clientsComboBox.SelectedItem is not Client client)
            {
                throw new InvalidOperationException("Veuillez selectionner un client.");
            }

            var commande = _commandeService.CreateCommande(
                client.Id,
                _lignes.Select(ligne => (ligne.ProduitId, ligne.Quantite)).ToList());

            _lignes.Clear();
            _lignesSource.ResetBindings(false);
            RefreshCommandes();
            MessageBox.Show($"Commande #{commande.Id} creee.", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex) when (ex is ArgumentException or InvalidOperationException)
        {
            MessageBox.Show(ex.Message, "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    private void RefreshCommandes()
    {
        _commandesSource.DataSource = _commandeService.GetCommandes()
            .Select(commande => new CommandeListItem(
                commande.Id,
                commande.Date,
                commande.Client?.Nom ?? string.Empty,
                commande.Total.ToString("0.00")))
            .ToList();
        _commandesGrid.DataSource = _commandesSource;
    }

    private sealed record LigneCommandeDraft(int ProduitId, string ProduitNom, int Quantite, decimal PrixUnitaire);

    private sealed record CommandeListItem(int Id, DateTime Date, string ClientNom, string Total);
}
