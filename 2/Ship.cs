namespace APBD2;

public class Ship
{
    public static List<Ship> DB_Ship = new List<Ship>();
    
    private List<Container> containers;
    private string _name;
    private int _speed;
    private int _maxContainerNum;
    private int _maxWeight;


    public Ship(string name, int speed, int maxContainerNum, int maxWeight)
    {
        this.containers = new List<Container>();
        this._name = name;
        this._speed = speed;
        this._maxContainerNum = maxContainerNum;
        this._maxWeight = maxWeight;
        
        DB_Ship.Add(this);
    }

    public static void CreateExmple_Ship()
    {
        Ship s1 = new Ship("SHIP_01", 10, 150, 40000);
        Ship s2 = new Ship("SHIP_02", 15, 140, 35000);
        Ship s3 = new Ship("SHIP_03", 20, 130, 30000);
        Ship s4 = new Ship("SHIP_04", 25, 120, 25000);
        Ship s5 = new Ship("SHIP_05", 30, 110, 20000);
    }

    public static void Create_Ship()
    {
        Console.WriteLine("Enter ship information:");

        Console.Write("Name: ");
        string name = Console.ReadLine();

        Console.Write("Speed (knots): ");
        int speed = int.Parse(Console.ReadLine());

        Console.Write("Maximum container capacity: ");
        int maxContainerNum = int.Parse(Console.ReadLine());

        Console.Write("Maximum weight capacity (kg): ");
        int maxWeight = int.Parse(Console.ReadLine());

        Ship newShip = new Ship(name, speed, maxContainerNum, maxWeight);

        Console.WriteLine($"Ship '{name}' created successfully!");
    }

    
    public static string DrawShip()
    {
        int currentContainerNumber = 0;
        int currentWeight = 0;
        string a = "";

        if (DB_Ship.Count == 0)
        {
            a += $"╚═════════╩═══════╩══════════════╩════════════╝\n\n";
        }
        else
        {
            foreach (var ship in DB_Ship)
            {
                a += $"║ {ship._name} ║   {ship._speed}  ║   {currentContainerNumber} / {ship._maxContainerNum}  ║ {currentWeight} / {ship._maxWeight} ║\n";
            
                if (DB_Ship.IndexOf(ship) == DB_Ship.Count - 1)
                {
                    a += $"╚═════════╩═══════╩══════════════╩════════════╝\n\n";
                }
                else
                {
                    a += $"╠═════════╬═══════╬══════════════╬════════════╣\n";
                }
            }
        }

        return a;
    }
    
    public void AddContainer(Container container)
    {
        if (containers.Count < _maxContainerNum)// TODO add mass check 
        {
            containers.Add(container);
        }
        else
        {
            Console.WriteLine("Cannot add more containers. Max capacity reached.");
        }
    }
}