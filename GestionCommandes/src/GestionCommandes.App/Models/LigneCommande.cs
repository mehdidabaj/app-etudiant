namespace GestionCommandes.App.Models;

public class LigneCommande
{
    public int Id { get; set; }

    public int Quantite { get; set; }

    public decimal PrixUnitaire { get; set; }

    public int ProduitId { get; set; }

    public Produit Produit { get; set; } = null!;

    public int CommandeId { get; set; }

    public Commande Commande { get; set; } = null!;

    public decimal SousTotal => Quantite * PrixUnitaire;
}
