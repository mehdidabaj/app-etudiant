<%@ page contentType="text/html; charset=UTF-8" language="java" %>
<%@ page import="repositories.EtudiantRepository, repositories.FiliereRepository, java.util.List, entities.Etudiant, entities.Filiere" %>

<!DOCTYPE html>
<html>
<head>

<meta charset="UTF-8">
<title>Dashboard Université</title>

<link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet">

<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.0/css/all.min.css">

<style>

body{
    background:#f4f6f9;
}

/* Sidebar */

.sidebar{
    height:100vh;
    background:#343a40;
    color:white;
    padding-top:20px;
    position:fixed;
    width:240px;
}

.sidebar h4{
    text-align:center;
    margin-bottom:30px;
}

.sidebar a{
    display:block;
    padding:12px 20px;
    color:white;
    text-decoration:none;
}

.sidebar a:hover{
    background:#495057;
}

/* Main */

.main{
    margin-left:250px;
    padding:30px;
}

/* Cards */

.card{
    border:none;
    border-radius:12px;
    box-shadow:0 5px 15px rgba(0,0,0,0.1);
}

.stat-icon{
    font-size:40px;
}

.stat-number{
    font-size:32px;
    font-weight:bold;
}

footer{
    text-align:center;
    margin-top:40px;
    color:gray;
}

.creator{
    color:#007bff;
    font-weight:bold;
}

</style>

</head>

<body>

<!-- Sidebar -->

<div class="sidebar">

<h4>🎓 Université</h4>

<a href="<%=request.getContextPath()%>/">
<i class="fa fa-home"></i> Dashboard
</a>

<a href="<%=request.getContextPath()%>/etudiants">
<i class="fa fa-user-graduate"></i> Étudiants
</a>

<a href="<%=request.getContextPath()%>/filieres">
<i class="fa fa-school"></i> Filières
</a>

</div>

<!-- Main -->

<div class="main">

<h2 class="mb-4">Dashboard</h2>

<%

EtudiantRepository etudiantRepo = new EtudiantRepository();
FiliereRepository filiereRepo = new FiliereRepository();

List<Etudiant> etudiants = etudiantRepo.findAll();
List<Filiere> filieres = filiereRepo.findAll();

int nbEtudiants = etudiants != null ? etudiants.size() : 0;
int nbFilieres = filieres != null ? filieres.size() : 0;

etudiantRepo.close();
filiereRepo.close();

%>

<div class="row g-4">

<div class="col-md-4">

<div class="card p-4 text-center">

<div class="stat-icon text-primary mb-3">
<i class="fa fa-user-graduate"></i>
</div>

<h5>Étudiants</h5>

<div class="stat-number text-primary">
<%= nbEtudiants %>
</div>

</div>

</div>


<div class="col-md-4">

<div class="card p-4 text-center">

<div class="stat-icon text-success mb-3">
<i class="fa fa-school"></i>
</div>

<h5>Filières</h5>

<div class="stat-number text-success">
<%= nbFilieres %>
</div>

</div>

</div>

</div>


<footer>

Application Gestion Université | Java EE  
<br>
Created by <span class="creator">Hermich Mehdi</span>

</footer>

</div>

</body>
</html>