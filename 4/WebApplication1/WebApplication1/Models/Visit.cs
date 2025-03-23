namespace WebApplication1;

public class Visit
{
    public int Id { get; set; }
    public DateTime DateVisit { get; set; }
    public Animal Animal { get; set; }
    public string Description { get; set; }
    public int Price { get; set; }
}