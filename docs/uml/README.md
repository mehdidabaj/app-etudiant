# Diagrammes UML - App_Etudiants

Ce dossier contient les diagrammes UML derives du projet Java `App_Etudiants`.
Le perimetre presente uniquement deux modules :

- gestion des etudiants en CRUD ;
- gestion des filieres en CRUD.

## Fichiers source PlantUML

- `use-case.puml` : cas d'utilisation CRUD des etudiants et des filieres.
- `classe-conceptuel.puml` : concepts metier Etudiant, Filiere et Niveau.
- `classe-analyse.puml` : presentation, controleurs, entites, repositories et base MySQL limites au CRUD.
- `classe-technique.puml` : diagramme de classe technique limite a une seule fonctionnalite, le CRUD etudiant.
- `sequence-conceptuel.puml` : sequence conceptuelle d'un cycle CRUD.
- `sequence-analyse.puml` : sequence d'analyse du CRUD etudiant dans l'architecture Servlet/JPA.

## Images generees

Les images PNG correspondantes sont disponibles dans `docs/uml/images/` :

- `images/use-case.png`
- `images/classe-conceptuel.png`
- `images/classe-analyse.png`
- `images/classe-technique.png`
- `images/sequence-conceptuel.png`
- `images/sequence-analyse.png`

## Generation

Les fichiers sont au format PlantUML. Pour generer les images :

```bash
plantuml -tpng -o images docs/uml/*.puml
```

Les diagrammes se basent sur les classes Java suivantes :

- `entities.Etudiant`
- `entities.Filiere`
- `servlets.EtudiantServlet`
- `servlets.FiliereServlet`
- `repositories.Repository`
- `repositories.EtudiantRepository`
- `repositories.FiliereRepository`
