# Diagrammes UML - App_Etudiants

Ce dossier contient les diagrammes UML derives du projet Java `App_Etudiants`.

## Fichiers source PlantUML

- `use-case.puml` : diagramme de cas d'utilisation detaille.
- `classe-conceptuel.puml` : diagramme de classe conceptuel du domaine.
- `classe-analyse.puml` : diagramme de classe d'analyse avec presentation, controleurs, entites, repositories et JPA.
- `sequence-conceptuel.puml` : sequence conceptuelle du cas "creer un etudiant".
- `sequence-analyse.puml` : sequence d'analyse du cas "ajouter un etudiant" dans l'architecture Servlet/JPA.

## Images generees

Les images PNG correspondantes sont disponibles dans `docs/uml/images/` :

- `images/use-case.png`
- `images/classe-conceptuel.png`
- `images/classe-analyse.png`
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
