using System.Drawing;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;

namespace GestionCommandes.App.Forms;

public class MainForm : Form
{
    private readonly IServiceProvider _serviceProvider;

    public MainForm(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;

        Text = "Gestion des commandes";
        Width = 520;
        Height = 320;
        StartPosition = FormStartPosition.CenterScreen;
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;

        var title = new Label
        {
            Text = "Mini e-commerce",
            Dock = DockStyle.Top,
            Height = 60,
            TextAlign = ContentAlignment.MiddleCenter,
            Font = new Font(FontFamily.GenericSansSerif, 18, FontStyle.Bold)
        };

        var panel = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            FlowDirection = FlowDirection.TopDown,
            Padding = new Padding(120, 10, 120, 10),
            WrapContents = false
        };

        panel.Controls.Add(CreateButton("Gerer les clients", OpenClients));
        panel.Controls.Add(CreateButton("Gerer les produits", OpenProduits));
        panel.Controls.Add(CreateButton("Creer / consulter commandes", OpenCommandes));

        Controls.Add(panel);
        Controls.Add(title);
    }

    private static Button CreateButton(string text, EventHandler clickHandler)
    {
        var button = new Button
        {
            Text = text,
            Width = 260,
            Height = 45,
            Margin = new Padding(0, 10, 0, 10)
        };

        button.Click += clickHandler;
        return button;
    }

    private void OpenClients(object? sender, EventArgs e)
    {
        using var form = _serviceProvider.GetRequiredService<FormClient>();
        form.ShowDialog(this);
    }

    private void OpenProduits(object? sender, EventArgs e)
    {
        using var form = _serviceProvider.GetRequiredService<FormProduit>();
        form.ShowDialog(this);
    }

    private void OpenCommandes(object? sender, EventArgs e)
    {
        using var form = _serviceProvider.GetRequiredService<FormCommande>();
        form.ShowDialog(this);
    }
}
