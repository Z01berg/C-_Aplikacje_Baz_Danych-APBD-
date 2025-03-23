using System.Drawing;

namespace WebApplication1;

public enum CategoryE
{
    Dog,
    Cat,
    Bird,
    Wolf,
    Fox
}

public class Animal
{
    public int Id { get; set; }
    public string Name { get; set; }
    public CategoryE Category { get; set; }
    public double Mass { get; set; }
    public string ColorWool { get; set; }
}