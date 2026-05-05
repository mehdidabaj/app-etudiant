package entities;

import javax.persistence.*;
import java.util.List;

@Entity
@Table(name = "filiere")
@NamedQueries({
    @NamedQuery(name = "Filiere.findAll", query = "SELECT f FROM Filiere f"),
    @NamedQuery(name = "Filiere.findByNom", query = "SELECT f FROM Filiere f WHERE f.nomFiliere LIKE :nom")
})
public class Filiere {
    
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "id_filiere")
    private Integer idFiliere;
    
    @Column(name = "nom_filiere", nullable = false, length = 100)
    private String nomFiliere;
    
    @OneToMany(mappedBy = "filiere", cascade = CascadeType.ALL, fetch = FetchType.LAZY)
    private List<Etudiant> etudiants;
    
    // Constructeurs, Getters, Setters...
    public Filiere() {}
    
    public Filiere(String nomFiliere) {
        this.nomFiliere = nomFiliere;
    }
    
    // Getters et Setters...
    public Integer getIdFiliere() {
        return idFiliere;
    }
    
    public void setIdFiliere(Integer idFiliere) {
        this.idFiliere = idFiliere;
    }
    
    public String getNomFiliere() {
        return nomFiliere;
    }
    
    public void setNomFiliere(String nomFiliere) {
        this.nomFiliere = nomFiliere;
    }
    
    public List<Etudiant> getEtudiants() {
        return etudiants;
    }
    
    public void setEtudiants(List<Etudiant> etudiants) {
        this.etudiants = etudiants;
    }
    
    @Override
    public String toString() {
        return nomFiliere;
    }
}