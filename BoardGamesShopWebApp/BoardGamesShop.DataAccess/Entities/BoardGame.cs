using System.ComponentModel.DataAnnotations.Schema;

namespace BoardGamesShop.DataAccess.Entities;

[Table("board_games")]
public class BoardGame : BaseEntity
{
    public string Name { get; set; }
    public string Genre { get; set; }
    public int Price { get; set; }
    public int MinAge { get; set; }
    public string Publisher { get; set; }
    public string Description { get; set; }
    
    public List<GameDay> GameDays { get; set; }
    
    public List<PurchaseHistory> PurchaseHistory { get; set; }
}