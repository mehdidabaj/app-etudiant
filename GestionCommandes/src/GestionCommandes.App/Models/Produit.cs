namespace GestionCommandes.App.Models;

public class Produit
{
    public int Id { get; set; }

    public string Nom { get; set; } = string.Empty;

    public decimal Prix { get; set; }

    public ICollection<LigneCommande> LignesCommande { get; set; } = new List<LigneCommande>();

    public override string ToString()
    {
        return $"{Nom} - {Prix:C}";
    }
}
