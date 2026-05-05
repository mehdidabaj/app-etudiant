<%@ page contentType="text/html; charset=UTF-8" language="java" %>
<%@ page import="entities.Etudiant" %>
<%@ page import="entities.Filiere" %>
<%@ page import="java.util.List" %>

<%
    List<Etudiant> etudiants = (List<Etudiant>) request.getAttribute("etudiants");
    List<Filiere> filieres = (List<Filiere>) request.getAttribute("filieres");
    String searchNom = request.getAttribute("searchNom") != null ? (String) request.getAttribute("searchNom") : "";
    String searchNiveau = request.getAttribute("searchNiveau") != null ? (String) request.getAttribute("searchNiveau") : "";
    String searchFiliereId = request.getAttribute("searchFiliereId") != null ? (String) request.getAttribute("searchFiliereId") : "";
%>

<!DOCTYPE html>
<html>
<head>
    <meta charset="UTF-8">
    <title>Liste des Étudiants</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet">
</head>
<body>
<div class="container mt-4">
    <h2>Étudiants</h2>
    <a href="<%=request.getContextPath()%>/etudiants/add" class="btn btn-success mb-3">Ajouter un étudiant</a>

    <!-- Formulaire de recherche -->
    <form method="get" action="<%=request.getContextPath()%>/etudiants/search" class="row g-3 mb-4">
        <div class="col-md-3">
            <input type="text" class="form-control" name="nom" placeholder="Nom" value="<%=searchNom%>">
        </div>
        <div class="col-md-3">
            <select name="filiere" class="form-select">
                <option value="">Toutes les filières</option>
                <% for(Filiere f : filieres){ %>
                    <option value="<%=f.getIdFiliere()%>" <%= f.getIdFiliere().toString().equals(searchFiliereId) ? "selected" : "" %>>
                        <%= f.getNomFiliere() %>
                    </option>
                <% } %>
            </select>
        </div>
        <div class="col-md-3">
            <select name="niveau" class="form-select">
                <option value="">Tous les niveaux</option>
                <option value="Licence 1" <%= "Licence 1".equals(searchNiveau) ? "selected" : "" %>>Licence 1</option>
                <option value="Licence 2" <%= "Licence 2".equals(searchNiveau) ? "selected" : "" %>>Licence 2</option>
                <option value="Licence 3" <%= "Licence 3".equals(searchNiveau) ? "selected" : "" %>>Licence 3</option>
                <option value="Master 1" <%= "Master 1".equals(searchNiveau) ? "selected" : "" %>>Master 1</option>
                <option value="Master 2" <%= "Master 2".equals(searchNiveau) ? "selected" : "" %>>Master 2</option>
            </select>
        </div>
        <div class="col-md-3">
            <button type="submit" class="btn btn-primary">Rechercher</button>
            <a href="<%=request.getContextPath()%>/etudiants" class="btn btn-secondary">Réinitialiser</a>
        </div>
    </form>

    <!-- Tableau des étudiants -->
    <table class="table table-striped table-bordered">
        <thead class="table-primary">
            <tr>
                <th>ID</th>
                <th>Nom complet</th>
                <th>Niveau</th>
                <th>Filière</th>
                <th>Actions</th>
            </tr>
        </thead>
        <tbody>
        <%
            if(etudiants != null){
                for(Etudiant e : etudiants){
        %>
            <tr>
                <td><%=e.getIdEtudiant()%></td>
                <td><%=e.getNomComplet()%></td>
                <td><%=e.getNiveau()%></td>
                <td><%= e.getFiliere() != null ? e.getFiliere().getNomFiliere() : "" %></td>
                <td>
                    <a href="<%=request.getContextPath()%>/etudiants/edit?id=<%=e.getIdEtudiant()%>" class="btn btn-warning btn-sm">Modifier</a>
                    <a href="<%=request.getContextPath()%>/etudiants/delete?id=<%=e.getIdEtudiant()%>" class="btn btn-danger btn-sm"
                       onclick="return confirm('Voulez-vous vraiment supprimer cet étudiant ?');">Supprimer</a>
                </td>
            </tr>
        <%
                }
            }
        %>
        </tbody>
    </table>

    <a href="<%=request.getContextPath()%>/" class="btn btn-secondary">Retour</a>
</div>

<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>
