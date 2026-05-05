# Diagrammes UML - App_Etudiants

Ce dossier contient les diagrammes UML derives du projet Java `App_Etudiants`.

## Fichiers

- `use-case.puml` : diagramme de cas d'utilisation.
- `classe-conceptuel.puml` : diagramme de classe conceptuel du domaine.
- `classe-analyse.puml` : diagramme de classe d'analyse avec boundary, control, entity et persistence.
- `sequence-conceptuel.puml` : sequence conceptuelle du cas "creer un etudiant".
- `sequence-analyse.puml` : sequence d'analyse du cas "ajouter un etudiant" dans l'architecture Servlet/JPA.

## Generation

Les fichiers sont au format PlantUML. Pour generer les images :

```bash
plantuml docs/uml/*.puml
```

Les diagrammes se basent sur les classes Java suivantes :

- `entities.Etudiant`
- `entities.Filiere`
- `servlets.EtudiantServlet`
- `servlets.FiliereServlet`
- `repositories.Repository`
- `repositories.EtudiantRepository`
- `repositories.FiliereRepository`
