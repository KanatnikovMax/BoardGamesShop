using BoardGamesShop.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BoardGamesShop.DataAccess;

public class BoardGamesShopDbContext : DbContext
{
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<BoardGame> BoardGames { get; set; }
    public DbSet<GameDay> GameDays { get; set; }
    public DbSet<PurchaseHistory> PurchaseHistories { get; set; }
    
    public BoardGamesShopDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<IdentityUserClaim<int>>().ToTable("user_claims");
        modelBuilder.Entity<IdentityUserLogin<int>>().ToTable("user_logins").HasNoKey();
        modelBuilder.Entity<IdentityUserToken<int>>().ToTable("user_tokens").HasNoKey();;
        modelBuilder.Entity<UserRoleEntity>().ToTable("user_roles");
        modelBuilder.Entity<IdentityRoleClaim<int>>().ToTable("user_role_claims");
        modelBuilder.Entity<IdentityUserRole<int>>().ToTable("user_role_owners").HasNoKey();
        
        modelBuilder.Entity<UserEntity>().HasKey(x => x.Id);
        modelBuilder.Entity<UserEntity>().HasIndex(x => x.ExternalId).IsUnique();
        modelBuilder.Entity<UserEntity>().HasIndex(x => x.Login).IsUnique();
        modelBuilder.Entity<UserEntity>().HasIndex(x => x.PhoneNumber).IsUnique();
        modelBuilder.Entity<UserEntity>().HasIndex(x => x.Email).IsUnique();
        modelBuilder.Entity<UserEntity>().HasIndex(x => x.PasswordHash).IsUnique();
        modelBuilder.Entity<PurchaseHistory>().HasKey(x => x.Id);
        modelBuilder.Entity<PurchaseHistory>()
            .HasOne(ph => ph.UserEntity)
            .WithMany(u => u.PurchaseHistory)
            .HasForeignKey(ph => ph.UserEntityId);
        
        modelBuilder.Entity<BoardGame>().HasKey(x => x.Id);
        modelBuilder.Entity<BoardGame>().HasIndex(x => x.ExternalId).IsUnique();
        modelBuilder.Entity<BoardGame>().HasIndex(x => x.Name).IsUnique();
        modelBuilder.Entity<PurchaseHistory>()
            .HasOne(ph => ph.BoardGame)
            .WithMany(u => u.PurchaseHistory)
            .HasForeignKey(ph => ph.BoardGameId);
        
        modelBuilder.Entity<GameDay>().HasKey(x => x.Id);
        modelBuilder.Entity<GameDay>()
            .HasOne(gd => gd.GameClub)
            .WithMany(g => g.GameDays)
            .HasForeignKey(gd => gd.GameClubId);
        
        modelBuilder.Entity<BoardGame>()
            .HasMany(gb => gb.GameDays)
            .WithMany(gd => gd.BoardGames)
            .UsingEntity(j => j.ToTable("game_day_board_game"));
        
        modelBuilder.Entity<UserEntity>()
            .HasMany(u => u.GameDays)
            .WithMany(gd => gd.Users)
            .UsingEntity(j => j.ToTable("user_game_day"));
    }
}