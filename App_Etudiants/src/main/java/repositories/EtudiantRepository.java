package repositories;

import entities.Etudiant;
import entities.Filiere;
import javax.persistence.*;
import java.util.List;

public class EtudiantRepository extends Repository<Etudiant, Integer> {
    
    public EtudiantRepository() {
        super(Etudiant.class);
    }
    
    @SuppressWarnings("unchecked")
	public List<Etudiant> findByName(String nom) {
        EntityManager em = getEntityManager();
        try {
            Query query = em.createNamedQuery("Etudiant.findByNom");
            query.setParameter("nom", "%" + nom + "%");
            return query.getResultList();
        } finally {
            em.close();
        }
    }
    
    @SuppressWarnings("unchecked")
	public List<Etudiant> findByFiliere(Filiere filiere) {
        EntityManager em = getEntityManager();
        try {
            Query query = em.createNamedQuery("Etudiant.findByFiliere");
            query.setParameter("filiere", filiere);
            return query.getResultList();
        } finally {
            em.close();
        }
    }
    
    @SuppressWarnings("unchecked")
	public List<Etudiant> findByNameAndFiliere(String nom, Filiere filiere) {
        EntityManager em = getEntityManager();
        try {
            Query query = em.createNamedQuery("Etudiant.findByNomAndFiliere");
            query.setParameter("nom", "%" + nom + "%");
            query.setParameter("filiere", filiere);
            return query.getResultList();
        } finally {
            em.close();
        }
    }
    
    @SuppressWarnings("unchecked")
	public List<Etudiant> search(String nom, Filiere filiere, String niveau) {
        EntityManager em = getEntityManager();
        try {
            StringBuilder jpql = new StringBuilder("SELECT e FROM Etudiant e WHERE 1=1");
            
            if (nom != null && !nom.trim().isEmpty()) {
                jpql.append(" AND e.nomEtudiant LIKE :nom");
            }
            if (filiere != null) {
                jpql.append(" AND e.filiere = :filiere");
            }
            if (niveau != null && !niveau.trim().isEmpty()) {
                jpql.append(" AND e.niveau = :niveau");
            }
            
            jpql.append(" ORDER BY e.nomEtudiant, e.prenomEtudiant");
            
            Query query = em.createQuery(jpql.toString());
            
            if (nom != null && !nom.trim().isEmpty()) {
                query.setParameter("nom", "%" + nom + "%");
            }
            if (filiere != null) {
                query.setParameter("filiere", filiere);
            }
            if (niveau != null && !niveau.trim().isEmpty()) {
                query.setParameter("niveau", niveau);
            }
            
            return query.getResultList();
        } finally {
            em.close();
        }
    }
}