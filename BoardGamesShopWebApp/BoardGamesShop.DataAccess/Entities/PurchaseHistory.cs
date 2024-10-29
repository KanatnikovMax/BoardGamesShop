using System.ComponentModel.DataAnnotations.Schema;

namespace BoardGamesShop.DataAccess.Entities;

[Table("purchase_history")]
public class PurchaseHistory : BaseEntity
{
    public DateTime PurchaseDate { get; set; }
    public int OrderNumber { get; set; }
    public int Payment { get; set; }
    
    public int UserEntityId { get; set; }
    public UserEntity UserEntity { get; set; }
    
    public int BoardGameId { get; set; }
    public BoardGame BoardGame { get; set; }
}