<%@ page contentType="text/html; charset=UTF-8" language="java" %>
<%@ page import="entities.Etudiant" %>
<%@ page import="entities.Filiere" %>
<%@ page import="java.util.List" %>

<%
    Etudiant etudiant = (Etudiant) request.getAttribute("etudiant");
    boolean isEdit = etudiant != null;
    List<Filiere> filieres = (List<Filiere>) request.getAttribute("filieres");
%>

<!DOCTYPE html>
<html>
<head>
    <meta charset="UTF-8">
    <title><%= isEdit ? "Modifier l'étudiant" : "Ajouter un étudiant" %></title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet">
</head>
<body>
<div class="container mt-4">
    <h2><%= isEdit ? "Modifier l'étudiant" : "Ajouter un étudiant" %></h2>

    <form action="<%=request.getContextPath()%>/etudiants/<%= isEdit ? "edit" : "add" %>" method="post">
        <% if(isEdit){ %>
            <input type="hidden" name="id" value="<%=etudiant.getIdEtudiant()%>">
        <% } %>

        <div class="mb-3">
            <label class="form-label">Nom</label>
            <input type="text" name="nom" class="form-control" required value="<%= isEdit ? etudiant.getNomEtudiant() : "" %>">
        </div>
        <div class="mb-3">
            <label class="form-label">Prénom</label>
            <input type="text" name="prenom" class="form-control" required value="<%= isEdit ? etudiant.getPrenomEtudiant() : "" %>">
        </div>
        <div class="mb-3">
            <label class="form-label">Niveau</label>
            <select name="niveau" class="form-select" required>
                <option value="">Sélectionner un niveau</option>
                <option value="Licence 1" <%= isEdit && "Licence 1".equals(etudiant.getNiveau()) ? "selected" : "" %>>Licence 1</option>
                <option value="Licence 2" <%= isEdit && "Licence 2".equals(etudiant.getNiveau()) ? "selected" : "" %>>Licence 2</option>
                <option value="Licence 3" <%= isEdit && "Licence 3".equals(etudiant.getNiveau()) ? "selected" : "" %>>Licence 3</option>
                <option value="Master 1" <%= isEdit && "Master 1".equals(etudiant.getNiveau()) ? "selected" : "" %>>Master 1</option>
                <option value="Master 2" <%= isEdit && "Master 2".equals(etudiant.getNiveau()) ? "selected" : "" %>>Master 2</option>
            </select>
        </div>
        <div class="mb-3">
            <label class="form-label">Filière</label>
            <select name="filiere" class="form-select" required>
                <option value="">Sélectionner une filière</option>
                <% for(Filiere f : filieres){ %>
                    <option value="<%=f.getIdFiliere()%>" 
                        <%= isEdit && etudiant.getFiliere() != null && etudiant.getFiliere().getIdFiliere().equals(f.getIdFiliere()) ? "selected" : "" %>>
                        <%=f.getNomFiliere()%>
                    </option>
                <% } %>
            </select>
        </div>

        <button type="submit" class="btn btn-primary"><%= isEdit ? "Modifier" : "Ajouter" %></button>
        <a href="<%=request.getContextPath()%>/etudiants" class="btn btn-secondary">Annuler</a>
    </form>
</div>

<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>
