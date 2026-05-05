using GestionCommandes.App.Models;
using GestionCommandes.App.Repositories;

namespace GestionCommandes.App.Services;

public class ClientService
{
    private readonly ClientRepository _clientRepository;

    public ClientService(ClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }

    public IReadOnlyList<Client> GetClients()
    {
        return _clientRepository.GetAll();
    }

    public void AddClient(string nom, string email)
    {
        if (string.IsNullOrWhiteSpace(nom))
        {
            throw new ArgumentException("Le nom du client est obligatoire.", nameof(nom));
        }

        if (string.IsNullOrWhiteSpace(email) || !email.Contains('@'))
        {
            throw new ArgumentException("L'email du client est invalide.", nameof(email));
        }

        _clientRepository.Add(new Client
        {
            Nom = nom.Trim(),
            Email = email.Trim()
        });
    }
}
