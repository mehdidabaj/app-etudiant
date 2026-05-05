using GestionCommandes.App.Data;
using GestionCommandes.App.Models;

namespace GestionCommandes.App.Repositories;

public class ProductRepository : Repository<Produit>
{
    public ProductRepository(AppDbContext context) : base(context)
    {
    }
}
