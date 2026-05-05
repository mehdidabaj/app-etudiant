<%@ page contentType="text/html; charset=UTF-8" language="java" %>
<%@ page import="entities.Filiere" %>
<%@ page import="java.util.List" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="UTF-8">
    <title>Liste des Filières</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet">
</head>
<body>
<div class="container mt-4">
    <h2>Filières</h2>
    <a href="<%=request.getContextPath()%>/filieres/add" class="btn btn-success mb-3">Ajouter une filière</a>

    <table class="table table-striped table-bordered">
        <thead class="table-primary">
            <tr>
                <th>ID</th>
                <th>Nom de la filière</th>
                <th>Actions</th>
            </tr>
        </thead>
        <tbody>
        <%
            List<Filiere> filieres = (List<Filiere>) request.getAttribute("filieres");
            if(filieres != null) {
                for(Filiere f : filieres) {
        %>
            <tr>
                <td><%=f.getIdFiliere()%></td>
                <td><%=f.getNomFiliere()%></td>
                <td>
                    <a href="<%=request.getContextPath()%>/filieres/edit?id=<%=f.getIdFiliere()%>" class="btn btn-warning btn-sm">Modifier</a>
                    <a href="<%=request.getContextPath()%>/filieres/delete?id=<%=f.getIdFiliere()%>" class="btn btn-danger btn-sm"
                       onclick="return confirm('Voulez-vous vraiment supprimer cette filière ?');">Supprimer</a>
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
