using GestionCommandes.App.Data;
using GestionCommandes.App.Models;

namespace GestionCommandes.App.Repositories;

public class ClientRepository : Repository<Client>
{
    public ClientRepository(AppDbContext dbContext)
        : base(dbContext)
    {
    }
}
