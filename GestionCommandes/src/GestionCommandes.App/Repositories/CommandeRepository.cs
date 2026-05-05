using GestionCommandes.App.Data;
using GestionCommandes.App.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionCommandes.App.Repositories;

public class CommandeRepository : Repository<Commande>
{
    public CommandeRepository(AppDbContext context)
        : base(context)
    {
    }

    public override IReadOnlyList<Commande> GetAll()
    {
        return Context.Commandes
            .Include(commande => commande.Client)
            .Include(commande => commande.Lignes)
            .ThenInclude(ligne => ligne.Produit)
            .OrderByDescending(commande => commande.Date)
            .ToList();
    }

    public override Commande? GetById(int id)
    {
        return Context.Commandes
            .Include(commande => commande.Client)
            .Include(commande => commande.Lignes)
            .ThenInclude(ligne => ligne.Produit)
            .FirstOrDefault(commande => commande.Id == id);
    }
}
