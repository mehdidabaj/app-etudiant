package entities;

import javax.persistence.*;

@Entity
@Table(name = "etudiant")
@NamedQueries({
    @NamedQuery(name = "Etudiant.findAll", query = "SELECT e FROM Etudiant e ORDER BY e.nomEtudiant"),
    @NamedQuery(name = "Etudiant.findByNom", 
                query = "SELECT e FROM Etudiant e WHERE e.nomEtudiant LIKE :nom ORDER BY e.nomEtudiant"),
    @NamedQuery(name = "Etudiant.findByFiliere",
                query = "SELECT e FROM Etudiant e WHERE e.filiere = :filiere ORDER BY e.nomEtudiant"),
    @NamedQuery(name = "Etudiant.findByNomAndFiliere",
                query = "SELECT e FROM Etudiant e WHERE e.nomEtudiant LIKE :nom AND e.filiere = :filiere ORDER BY e.nomEtudiant")
})
public class Etudiant {
    
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "id_etudiant")
    private Integer idEtudiant;
    
    @Column(name = "nom_etudiant", nullable = false, length = 50)
    private String nomEtudiant;
    
    @Column(name = "prenom_etudiant", nullable = false, length = 50)
    private String prenomEtudiant;
    
    @Column(name = "niveau", length = 20)
    private String niveau;
    
    @ManyToOne(fetch = FetchType.EAGER)
    @JoinColumn(name = "id_filiere")
    private Filiere filiere;
    
    // Constructeurs, Getters, Setters...
    public Etudiant() {}
    
    public Etudiant(String nomEtudiant, String prenomEtudiant, String niveau, Filiere filiere) {
        this.nomEtudiant = nomEtudiant;
        this.prenomEtudiant = prenomEtudiant;
        this.niveau = niveau;
        this.filiere = filiere;
    }
    
    // Getters et Setters...
    public Integer getIdEtudiant() {
        return idEtudiant;
    }
    
    public void setIdEtudiant(Integer idEtudiant) {
        this.idEtudiant = idEtudiant;
    }
    
    public String getNomEtudiant() {
        return nomEtudiant;
    }
    
    public void setNomEtudiant(String nomEtudiant) {
        this.nomEtudiant = nomEtudiant;
    }
    
    public String getPrenomEtudiant() {
        return prenomEtudiant;
    }
    
    public void setPrenomEtudiant(String prenomEtudiant) {
        this.prenomEtudiant = prenomEtudiant;
    }
    
    public String getNiveau() {
        return niveau;
    }
    
    public void setNiveau(String niveau) {
        this.niveau = niveau;
    }
    
    public Filiere getFiliere() {
        return filiere;
    }
    
    public void setFiliere(Filiere filiere) {
        this.filiere = filiere;
    }
    
    public String getNomComplet() {
        return nomEtudiant + " " + prenomEtudiant;
    }
    
    @Override
    public String toString() {
        return "Etudiant{" +
                "idEtudiant=" + idEtudiant +
                ", nomEtudiant='" + nomEtudiant + '\'' +
                ", prenomEtudiant='" + prenomEtudiant + '\'' +
                ", niveau='" + niveau + '\'' +
                ", filiere=" + (filiere != null ? filiere.getNomFiliere() : "null") +
                '}';
    }
}