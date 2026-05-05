namespace GestionCommandes.App.Models;

public class Client
{
    public int Id { get; set; }

    public string Nom { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public ICollection<Commande> Commandes { get; set; } = new List<Commande>();

    public override string ToString()
    {
        return $"{Nom} ({Email})";
    }
}
