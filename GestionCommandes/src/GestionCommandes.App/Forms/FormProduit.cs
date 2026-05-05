using System.Drawing;
using System.Windows.Forms;
using GestionCommandes.App.Services;

namespace GestionCommandes.App.Forms;

public class FormProduit : Form
{
    private readonly ProductService _productService;
    private readonly TextBox _nomTextBox = new();
    private readonly NumericUpDown _prixNumeric = new();
    private readonly DataGridView _produitsGrid = new();

    public FormProduit(ProductService productService)
    {
        _productService = productService;

        Text = "Gestion des produits";
        Width = 760;
        Height = 480;
        StartPosition = FormStartPosition.CenterParent;

        InitializeComponents();
        LoadProduits();
    }

    private void InitializeComponents()
    {
        var panel = new TableLayoutPanel
        {
            Dock = DockStyle.Top,
            Height = 130,
            ColumnCount = 2,
            RowCount = 3,
            Padding = new Padding(16)
        };
        panel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120));
        panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

        _nomTextBox.Dock = DockStyle.Fill;
        _prixNumeric.DecimalPlaces = 2;
        _prixNumeric.Maximum = 1_000_000;
        _prixNumeric.Minimum = 0;
        _prixNumeric.Dock = DockStyle.Left;
        _prixNumeric.Width = 180;

        var addButton = new Button
        {
            Text = "Ajouter",
            Width = 120,
            Height = 32
        };
        addButton.Click += AddButton_Click;

        panel.Controls.Add(new Label { Text = "Nom", AutoSize = true }, 0, 0);
        panel.Controls.Add(_nomTextBox, 1, 0);
        panel.Controls.Add(new Label { Text = "Prix", AutoSize = true }, 0, 1);
        panel.Controls.Add(_prixNumeric, 1, 1);
        panel.Controls.Add(addButton, 1, 2);

        _produitsGrid.Dock = DockStyle.Fill;
        _produitsGrid.ReadOnly = true;
        _produitsGrid.AllowUserToAddRows = false;
        _produitsGrid.AllowUserToDeleteRows = false;
        _produitsGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        Controls.Add(_produitsGrid);
        Controls.Add(panel);
    }

    private void AddButton_Click(object? sender, EventArgs e)
    {
        try
        {
            _productService.AddProduct(_nomTextBox.Text, _prixNumeric.Value);
            _nomTextBox.Clear();
            _prixNumeric.Value = 0;
            LoadProduits();
        }
        catch (ArgumentException exception)
        {
            MessageBox.Show(exception.Message, "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    private void LoadProduits()
    {
        _produitsGrid.DataSource = _productService.GetAll()
            .Select(produit => new
            {
                produit.Id,
                produit.Nom,
                Prix = produit.Prix.ToString("0.00")
            })
            .ToList();
    }
}
