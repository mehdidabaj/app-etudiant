using GestionCommandes.App.Models;
using GestionCommandes.App.Repositories;

namespace GestionCommandes.App.Services;

public class CommandeService
{
    private readonly CommandeRepository _commandeRepository;
    private readonly ClientRepository _clientRepository;
    private readonly ProductRepository _productRepository;

    public CommandeService(
        CommandeRepository commandeRepository,
        ClientRepository clientRepository,
        ProductRepository productRepository)
    {
        _commandeRepository = commandeRepository;
        _clientRepository = clientRepository;
        _productRepository = productRepository;
    }

    public IReadOnlyList<Commande> GetCommandes()
    {
        return _commandeRepository.GetAll();
    }

    public Commande? GetCommande(int id)
    {
        return _commandeRepository.GetById(id);
    }

    public Commande CreateCommande(int clientId, IReadOnlyList<(int ProduitId, int Quantite)> lignes)
    {
        if (_clientRepository.GetById(clientId) is null)
        {
            throw new ArgumentException("Le client selectionne est introuvable.", nameof(clientId));
        }

        if (lignes.Count == 0)
        {
            throw new ArgumentException("Une commande doit contenir au moins un produit.", nameof(lignes));
        }

        var commande = new Commande
        {
            ClientId = clientId,
            Date = DateTime.Now
        };

        foreach (var ligne in lignes)
        {
            if (ligne.Quantite <= 0)
            {
                throw new ArgumentException("La quantite doit etre superieure a zero.", nameof(lignes));
            }

            var produit = _productRepository.GetById(ligne.ProduitId);
            if (produit is null)
            {
                throw new ArgumentException("Un produit selectionne est introuvable.", nameof(lignes));
            }

            commande.Lignes.Add(new LigneCommande
            {
                ProduitId = produit.Id,
                Quantite = ligne.Quantite,
                PrixUnitaire = produit.Prix
            });
        }

        _commandeRepository.Add(commande);
        return commande;
    }
}
