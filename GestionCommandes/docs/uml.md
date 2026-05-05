# Diagrammes UML - Gestion des commandes

## 1. Diagramme de Use Case

```mermaid
flowchart LR
Admin["Utilisateur (Admin)"]

Admin --> AjouterClient([Ajouter client])
Admin --> AjouterProduit([Ajouter produit])
Admin --> CreerCommande([Creer commande])
Admin --> AjouterProduitCommande([Ajouter produit a commande])
Admin --> ConsulterCommandes([Consulter commandes])

CreerCommande -. "<<include>>" .-> AjouterProduitCommande
```

## 2. Diagramme de classes conceptuel

```mermaid
classDiagram
class Client {
  +int Id
  +string Nom
  +string Email
}

class Produit {
  +int Id
  +string Nom
  +decimal Prix
}

class Commande {
  +int Id
  +DateTime Date
  +int ClientId
}

class LigneCommande {
  +int Id
  +int Quantite
  +int ProduitId
  +int CommandeId
}

Client "1" --> "*" Commande
Commande "1" --> "*" LigneCommande
Produit "1" --> "*" LigneCommande
```

## 3. Diagramme de classes technique - 3 couches

```mermaid
classDiagram
namespace DAL {
  class AppDbContext
  class ClientRepository
  class ProductRepository
  class CommandeRepository
}

namespace BLL {
  class ClientService
  class ProductService
  class CommandeService
}

namespace PL {
  class MainForm
  class FormClient
  class FormProduit
  class FormCommande
}

MainForm --> FormClient
MainForm --> FormProduit
MainForm --> FormCommande
FormClient --> ClientService
FormProduit --> ProductService
FormCommande --> ClientService
FormCommande --> ProductService
FormCommande --> CommandeService
ClientService --> ClientRepository
ProductService --> ProductRepository
CommandeService --> ClientRepository
CommandeService --> ProductRepository
CommandeService --> CommandeRepository
ClientRepository --> AppDbContext
ProductRepository --> AppDbContext
CommandeRepository --> AppDbContext
```

Relation globale : `UI -> Service -> Repository -> DB`.

## 4. Diagramme de sequence - Ajouter un produit

```mermaid
sequenceDiagram
actor User as Utilisateur
participant Form as FormProduit
participant Service as ProductService
participant Repo as ProductRepository
participant Context as AppDbContext
participant DB as SQLite DB

User->>Form: Clique "Ajouter"
Form->>Service: AddProduct(nom, prix)
Service->>Service: Valider nom et prix
Service->>Repo: Add(produit)
Repo->>Context: Produits.Add(produit)
Repo->>Context: SaveChanges()
Context->>DB: INSERT Produit
DB-->>Context: OK
Context-->>Repo: OK
Repo-->>Service: OK
Service-->>Form: OK
Form-->>User: Liste actualisee
```

## 5. Diagramme de sequence boite blanche - Ajouter un produit

```mermaid
sequenceDiagram
participant FormProduit
participant ProductService
participant ProductRepository
participant AppDbContext

FormProduit->>ProductService: AddProduct(nom, prix)
ProductService->>ProductService: validation nom non vide
ProductService->>ProductService: validation prix > 0
ProductService->>ProductRepository: Add(new Produit)
ProductRepository->>AppDbContext: Set<Produit>().Add(produit)
ProductRepository->>AppDbContext: SaveChanges()
AppDbContext-->>ProductRepository: produit insere
ProductRepository-->>ProductService: retour
ProductService-->>FormProduit: ajout termine
```
