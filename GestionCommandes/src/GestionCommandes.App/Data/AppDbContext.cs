using GestionCommandes.App.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionCommandes.App.Data;

public class AppDbContext : DbContext
{
    public DbSet<Client> Clients => Set<Client>();

    public DbSet<Produit> Produits => Set<Produit>();

    public DbSet<Commande> Commandes => Set<Commande>();

    public DbSet<LigneCommande> LignesCommande => Set<LigneCommande>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite("Data Source=gestion_commandes.db");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>(entity =>
        {
            entity.Property(client => client.Nom)
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(client => client.Email)
                .HasMaxLength(150)
                .IsRequired();
        });

        modelBuilder.Entity<Produit>(entity =>
        {
            entity.Property(produit => produit.Nom)
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(produit => produit.Prix)
                .HasPrecision(10, 2);
        });

        modelBuilder.Entity<Commande>(entity =>
        {
            entity.HasOne(commande => commande.Client)
                .WithMany(client => client.Commandes)
                .HasForeignKey(commande => commande.ClientId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<LigneCommande>(entity =>
        {
            entity.Property(ligne => ligne.PrixUnitaire)
                .HasPrecision(10, 2);

            entity.HasOne(ligne => ligne.Commande)
                .WithMany(commande => commande.Lignes)
                .HasForeignKey(ligne => ligne.CommandeId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(ligne => ligne.Produit)
                .WithMany(produit => produit.LignesCommande)
                .HasForeignKey(ligne => ligne.ProduitId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
