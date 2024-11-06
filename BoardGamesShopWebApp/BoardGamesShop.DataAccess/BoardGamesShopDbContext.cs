using BoardGamesShop.DataAccess.Entities;
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
        //Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserEntity>().HasKey(x => x.Id);
        modelBuilder.Entity<PurchaseHistory>().HasKey(x => x.Id);
        modelBuilder.Entity<PurchaseHistory>()
            .HasOne(ph => ph.UserEntity)
            .WithMany(u => u.PurchaseHistory)
            .HasForeignKey(ph => ph.UserEntityId);
        
        modelBuilder.Entity<BoardGame>().HasKey(x => x.Id);
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