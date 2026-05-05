package servlets;

import javax.servlet.*;
import javax.servlet.http.*;
import javax.servlet.annotation.*;
import entities.Etudiant;
import entities.Filiere;
import repositories.EtudiantRepository;
import repositories.FiliereRepository;
import java.io.IOException;
import java.util.List;

@WebServlet(name = "EtudiantServlet", urlPatterns = {
    "/etudiants",
    "/etudiants/add",
    "/etudiants/edit",
    "/etudiants/delete",
    "/etudiants/search"
})
public class EtudiantServlet extends HttpServlet {
    
    private static final long serialVersionUID = 1L;
    private EtudiantRepository etudiantRepository;
    private FiliereRepository filiereRepository;
    
    
    @Override
    public void init() throws ServletException {
        super.init();
    }
    
    private EtudiantRepository getEtudiantRepository() {
        if (etudiantRepository == null) {
            etudiantRepository = new EtudiantRepository();
        }
        return etudiantRepository;
    }
    
    private FiliereRepository getFiliereRepository() {
        if (filiereRepository == null) {
            filiereRepository = new FiliereRepository();
        }
        return filiereRepository;
    }
    
    @Override
    protected void doGet(HttpServletRequest request, HttpServletResponse response) 
            throws ServletException, IOException {
        
        String action = request.getServletPath();
        
        try {
            switch (action) {
                case "/etudiants":
                    listEtudiants(request, response);
                    break;
                case "/etudiants/add":
                    showAddForm(request, response);
                    break;
                case "/etudiants/edit":
                    showEditForm(request, response);
                    break;
                case "/etudiants/delete":
                    deleteEtudiant(request, response);
                    break;
                case "/etudiants/search":
                    searchEtudiants(request, response);
                    break;
                default:
                    listEtudiants(request, response);
                    break;
            }
        } catch (Exception e) {
            e.printStackTrace();
            throw new ServletException(e);
        }
    }
    
    @Override
    protected void doPost(HttpServletRequest request, HttpServletResponse response) 
            throws ServletException, IOException {
        
        String action = request.getServletPath();
        
        try {
            switch (action) {
                case "/etudiants/add":
                    addEtudiant(request, response);
                    break;
                case "/etudiants/edit":
                    updateEtudiant(request, response);
                    break;
                case "/etudiants/search":
                    searchEtudiants(request, response);
                    break;
                default:
                    listEtudiants(request, response);
                    break;
            }
        } catch (Exception e) {
            e.printStackTrace();
            throw new ServletException(e);
        }
    }
    
    private void listEtudiants(HttpServletRequest request, HttpServletResponse response) 
            throws ServletException, IOException {
        
        List<Etudiant> etudiants = getEtudiantRepository().findAll();
        List<Filiere> filieres = getFiliereRepository().findAll();
        
        request.setAttribute("etudiants", etudiants);
        request.setAttribute("filieres", filieres);
        
        RequestDispatcher dispatcher = request.getRequestDispatcher("/etudiants/list.jsp");
        dispatcher.forward(request, response);
    }
    
    private void showAddForm(HttpServletRequest request, HttpServletResponse response) 
            throws ServletException, IOException {
        
        List<Filiere> filieres = getFiliereRepository().findAll();
        request.setAttribute("filieres", filieres);
        
        RequestDispatcher dispatcher = request.getRequestDispatcher("/etudiants/form.jsp");
        dispatcher.forward(request, response);
    }
    
    private void showEditForm(HttpServletRequest request, HttpServletResponse response) 
            throws ServletException, IOException {
        
        int id = Integer.parseInt(request.getParameter("id"));
        Etudiant etudiant = getEtudiantRepository().findById(id);
        List<Filiere> filieres = getFiliereRepository().findAll();
        
        request.setAttribute("etudiant", etudiant);
        request.setAttribute("filieres", filieres);
        
        RequestDispatcher dispatcher = request.getRequestDispatcher("/etudiants/form.jsp");
        dispatcher.forward(request, response);
    }
    
    private void addEtudiant(HttpServletRequest request, HttpServletResponse response) 
            throws IOException {
        
        String nom = request.getParameter("nom");
        String prenom = request.getParameter("prenom");
        String niveau = request.getParameter("niveau");
        int filiereId = Integer.parseInt(request.getParameter("filiere"));
        
        Filiere filiere = getFiliereRepository().findById(filiereId);
        Etudiant etudiant = new Etudiant(nom, prenom, niveau, filiere);
        
        getEtudiantRepository().save(etudiant);
        
        response.sendRedirect(request.getContextPath() + "/etudiants");
    }
    
    private void updateEtudiant(HttpServletRequest request, HttpServletResponse response) 
            throws IOException {
        
        int id = Integer.parseInt(request.getParameter("id"));
        String nom = request.getParameter("nom");
        String prenom = request.getParameter("prenom");
        String niveau = request.getParameter("niveau");
        int filiereId = Integer.parseInt(request.getParameter("filiere"));
        
        Etudiant etudiant = getEtudiantRepository().findById(id);
        Filiere filiere = getFiliereRepository().findById(filiereId);
        
        etudiant.setNomEtudiant(nom);
        etudiant.setPrenomEtudiant(prenom);
        etudiant.setNiveau(niveau);
        etudiant.setFiliere(filiere);
        
        getEtudiantRepository().update(etudiant);
        
        response.sendRedirect(request.getContextPath() + "/etudiants");
    }
    
    private void deleteEtudiant(HttpServletRequest request, HttpServletResponse response) 
            throws IOException {
        
        int id = Integer.parseInt(request.getParameter("id"));
        getEtudiantRepository().delete(id);
        
        response.sendRedirect(request.getContextPath() + "/etudiants");
    }
    
    private void searchEtudiants(HttpServletRequest request, HttpServletResponse response) 
            throws ServletException, IOException {
        
        String nom = request.getParameter("nom");
        String filiereId = request.getParameter("filiere");
        String niveau = request.getParameter("niveau");
        
        List<Etudiant> etudiants;
        List<Filiere> filieres = getFiliereRepository().findAll();
        
        if ((nom == null || nom.trim().isEmpty()) && 
            (filiereId == null || filiereId.trim().isEmpty()) &&
            (niveau == null || niveau.trim().isEmpty())) {
            
            etudiants = getEtudiantRepository().findAll();
        } else {
            Filiere filiere = null;
            if (filiereId != null && !filiereId.trim().isEmpty()) {
                filiere = getFiliereRepository().findById(Integer.parseInt(filiereId));
            }
            
            etudiants = getEtudiantRepository().search(nom, filiere, niveau);
        }
        
        request.setAttribute("etudiants", etudiants);
        request.setAttribute("filieres", filieres);
        request.setAttribute("searchNom", nom);
        request.setAttribute("searchNiveau", niveau);
        request.setAttribute("searchFiliereId", filiereId);
        
        RequestDispatcher dispatcher = request.getRequestDispatcher("/etudiants/list.jsp");
        dispatcher.forward(request, response);
    }
    
    @Override
    public void destroy() {
        if (etudiantRepository != null) {
            etudiantRepository.close();
        }
        if (filiereRepository != null) {
            filiereRepository.close();
        }
        super.destroy();
    }
}