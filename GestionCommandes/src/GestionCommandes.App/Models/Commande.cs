namespace GestionCommandes.App.Models;

public class Commande
{
    public int Id { get; set; }

    public DateTime Date { get; set; } = DateTime.Now;

    public int ClientId { get; set; }

    public Client? Client { get; set; }

    public ICollection<LigneCommande> Lignes { get; set; } = new List<LigneCommande>();

    public decimal Total => Lignes.Sum(ligne => ligne.SousTotal);

    public override string ToString()
    {
        return $"Commande #{Id} - {Date:dd/MM/yyyy}";
    }
}
