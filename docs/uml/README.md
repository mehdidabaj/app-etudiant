# Diagrammes UML - App_Etudiants

Ce dossier contient les diagrammes UML derives du projet Java `App_Etudiants`.
Le perimetre presente une seule fonctionnalite principale : le CRUD etudiant.
La filiere est conservee uniquement comme information rattachee a un etudiant.

## Fichiers source PlantUML

- `use-case.puml` : cas d'utilisation du CRUD etudiant.
- `classe-conceptuel.puml` : concepts metier utiles au CRUD etudiant.
- `classe-analyse.puml` : boundary, control, entity et persistence du CRUD etudiant.
- `classe-technique.puml` : diagramme technique simplifie du CRUD etudiant.
- `sequence-conceptuel.puml` : vue conceptuelle du cycle CRUD etudiant.
- `sequence-analyse.puml` : sequence d'analyse simplifiee du CRUD etudiant.

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
- `repositories.Repository`
- `repositories.EtudiantRepository`
- `repositories.FiliereRepository`
