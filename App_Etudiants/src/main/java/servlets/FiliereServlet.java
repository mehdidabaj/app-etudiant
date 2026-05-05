package servlets;

import javax.servlet.*;
import javax.servlet.http.*;
import javax.servlet.annotation.*;
import entities.Filiere;
import repositories.FiliereRepository;
import java.io.IOException;
import java.util.List;

@WebServlet(name = "FiliereServlet", urlPatterns = {
    "/filieres",
    "/filieres/add",
    "/filieres/edit",
    "/filieres/delete"
})
public class FiliereServlet extends HttpServlet {
    
    private static final long serialVersionUID = 1L;
    private FiliereRepository filiereRepository;
    
    @Override
    public void init() throws ServletException {
        super.init();
        filiereRepository = new FiliereRepository();
    }
    
    @Override
    protected void doGet(HttpServletRequest request, HttpServletResponse response) 
            throws ServletException, IOException {
        
        String action = request.getServletPath();
        
        try {
            switch (action) {
                case "/filieres":
                    listFilieres(request, response);
                    break;
                case "/filieres/add":
                    showAddForm(request, response);
                    break;
                case "/filieres/edit":
                    showEditForm(request, response);
                    break;
                case "/filieres/delete":
                    deleteFiliere(request, response);
                    break;
                default:
                    listFilieres(request, response);
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
                case "/filieres/add":
                    addFiliere(request, response);
                    break;
                case "/filieres/edit":
                    updateFiliere(request, response);
                    break;
                default:
                    listFilieres(request, response);
                    break;
            }
        } catch (Exception e) {
            e.printStackTrace();
            throw new ServletException(e);
        }
    }
    
    private void listFilieres(HttpServletRequest request, HttpServletResponse response) 
            throws ServletException, IOException {
        
        List<Filiere> filieres = filiereRepository.findAll();
        request.setAttribute("filieres", filieres);
        
        RequestDispatcher dispatcher = request.getRequestDispatcher("/filieres/list.jsp");
        dispatcher.forward(request, response);
    }
    
    private void showAddForm(HttpServletRequest request, HttpServletResponse response) 
            throws ServletException, IOException {
        
        RequestDispatcher dispatcher = request.getRequestDispatcher("/filieres/form.jsp");
        dispatcher.forward(request, response);
    }
    
    private void showEditForm(HttpServletRequest request, HttpServletResponse response) 
            throws ServletException, IOException {
        
        int id = Integer.parseInt(request.getParameter("id"));
        Filiere filiere = filiereRepository.findById(id);
        
        request.setAttribute("filiere", filiere);
        
        RequestDispatcher dispatcher = request.getRequestDispatcher("/filieres/form.jsp");
        dispatcher.forward(request, response);
    }
    
    private void addFiliere(HttpServletRequest request, HttpServletResponse response) 
            throws IOException {
        
        String nom = request.getParameter("nom");
        Filiere filiere = new Filiere(nom);
        
        filiereRepository.save(filiere);
        
        response.sendRedirect(request.getContextPath() + "/filieres");
    }
    
    private void updateFiliere(HttpServletRequest request, HttpServletResponse response) 
            throws IOException {
        
        int id = Integer.parseInt(request.getParameter("id"));
        String nom = request.getParameter("nom");
        
        Filiere filiere = filiereRepository.findById(id);
        filiere.setNomFiliere(nom);
        
        filiereRepository.update(filiere);
        
        response.sendRedirect(request.getContextPath() + "/filieres");
    }
    
    private void deleteFiliere(HttpServletRequest request, HttpServletResponse response) 
            throws IOException {
        
        int id = Integer.parseInt(request.getParameter("id"));
        filiereRepository.delete(id);
        
        response.sendRedirect(request.getContextPath() + "/filieres");
    }
    
    @Override
    public void destroy() {
        if (filiereRepository != null) {
            filiereRepository.close();
        }
        super.destroy();
    }
}