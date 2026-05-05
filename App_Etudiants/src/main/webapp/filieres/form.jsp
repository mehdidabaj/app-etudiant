<%@ page contentType="text/html; charset=UTF-8" language="java" %>
<%@ page import="entities.Filiere" %>

<%
    Filiere filiere = (Filiere) request.getAttribute("filiere");
    boolean isEdit = filiere != null;
%>

<!DOCTYPE html>
<html>
<head>
    <meta charset="UTF-8">
    <title><%= isEdit ? "Modifier la filière" : "Ajouter une filière" %></title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet">
</head>
<body>
<div class="container mt-4">
    <h2><%= isEdit ? "Modifier la filière" : "Ajouter une filière" %></h2>

    <form action="<%=request.getContextPath()%>/filieres/<%= isEdit ? "edit" : "add" %>" method="post">
        <% if(isEdit){ %>
            <input type="hidden" name="id" value="<%=filiere.getIdFiliere()%>">
        <% } %>

        <div class="mb-3">
            <label for="nom" class="form-label">Nom de la filière</label>
            <input type="text" class="form-control" id="nom" name="nom" required
                   value="<%= isEdit ? filiere.getNomFiliere() : "" %>">
        </div>

        <button type="submit" class="btn btn-primary"><%= isEdit ? "Modifier" : "Ajouter" %></button>
        <a href="<%=request.getContextPath()%>/filieres" class="btn btn-secondary">Annuler</a>
    </form>
</div>
<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>
