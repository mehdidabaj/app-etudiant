using GestionCommandes.App.Models;
using GestionCommandes.App.Repositories;

namespace GestionCommandes.App.Services;

public class ProductService
{
    private readonly ProductRepository _repository;

    public ProductService(ProductRepository repository)
    {
        _repository = repository;
    }

    public IReadOnlyList<Produit> GetAll()
    {
        return _repository.GetAll();
    }

    public void AddProduct(string nom, decimal prix)
    {
        if (string.IsNullOrWhiteSpace(nom))
        {
            throw new ArgumentException("Le nom du produit est obligatoire.", nameof(nom));
        }

        if (prix <= 0)
        {
            throw new ArgumentException("Le prix doit etre superieur a zero.", nameof(prix));
        }

        _repository.Add(new Produit
        {
            Nom = nom.Trim(),
            Prix = prix
        });
    }
}
