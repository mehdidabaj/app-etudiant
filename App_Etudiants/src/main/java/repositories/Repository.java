package repositories;

import javax.persistence.*;
import java.util.List;

public abstract class Repository<T, ID> 
{
    
    protected EntityManagerFactory emf;
    protected Class<T> entityClass;
    
    public Repository(Class<T> entityClass) 
    {
        this.entityClass = entityClass;
        // Initialisation différée du EntityManagerFactory
    }
    
    protected EntityManager getEntityManager() {
        if (emf == null) {
            emf = Persistence.createEntityManagerFactory("GestionPU");
        }
        return emf.createEntityManager();
    }
    
    public T save(T entity) {
        EntityManager em = getEntityManager();
        EntityTransaction tx = em.getTransaction();
        try {
            tx.begin();
            em.persist(entity);
            tx.commit();
            return entity;
        } catch (Exception e) {
            if (tx != null && tx.isActive()) {
                tx.rollback();
            }
            throw e;
        } finally {
            em.close();
        }
    }
    
    public T findById(ID id) {
        EntityManager em = getEntityManager();
        try {
            return em.find(entityClass, id);
        } finally {
            em.close();
        }
    }
    
    @SuppressWarnings("unchecked")
	public List<T> findAll() {
        EntityManager em = getEntityManager();
        try {
            String className = entityClass.getSimpleName();
            Query query = em.createQuery("SELECT e FROM " + className + " e");
            return query.getResultList();
        } finally {
            em.close();
        }
    }
    
    public T update(T entity) {
        EntityManager em = getEntityManager();
        EntityTransaction tx = em.getTransaction();
        try {
            tx.begin();
            T merged = em.merge(entity);
            tx.commit();
            return merged;
        } catch (Exception e) {
            if (tx != null && tx.isActive()) {
                tx.rollback();
            }
            throw e;
        } finally {
            em.close();
        }
    }
    
    public void delete(ID id) {
        EntityManager em = getEntityManager();
        EntityTransaction tx = em.getTransaction();
        try {
            tx.begin();
            T entity = em.find(entityClass, id);
            if (entity != null) {
                em.remove(entity);
            }
            tx.commit();
        } catch (Exception e) {
            if (tx != null && tx.isActive()) {
                tx.rollback();
            }
            throw e;
        } finally {
            em.close();
        }
    }
    
    public void close() {
        if (emf != null && emf.isOpen()) {
            emf.close();
        }
    }
}