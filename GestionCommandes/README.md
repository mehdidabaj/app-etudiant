# Gestion des commandes - Mini e-commerce

Application WinForms en 3 couches pour gerer des clients, des produits et des commandes.

## Fonctionnalites

- Ajouter un client.
- Ajouter un produit.
- Creer une commande pour un client.
- Ajouter des produits a une commande avec une quantite.
- Consulter les commandes existantes.

## Architecture

```text
PL (WinForms) -> BLL (Services) -> DAL (Repositories + AppDbContext) -> SQLite
```

- **DAL** : `AppDbContext`, `ClientRepository`, `ProductRepository`, `CommandeRepository`.
- **BLL** : `ClientService`, `ProductService`, `CommandeService`.
- **PL** : `MainForm`, `FormClient`, `FormProduit`, `FormCommande`.

## Prerequis

- Windows.
- .NET SDK 10 ou version compatible avec `net10.0-windows`.

## Lancer l'application

```bash
cd GestionCommandes
dotnet restore
dotnet run --project src/GestionCommandes.App/GestionCommandes.App.csproj
```

La base SQLite `gestion_commandes.db` est creee automatiquement au premier demarrage.

## Documentation UML

Les diagrammes Mermaid sont disponibles dans [`docs/uml.md`](docs/uml.md).
