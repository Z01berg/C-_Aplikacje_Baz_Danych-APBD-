using APBD2;

class Program
{
    public static void Main(string[] args)
    {
        // Stworzenie kontenera danego typu
        Container liquid = new Liquid(1250, 15, 150, 10, "5", 1500, true);
        Container gaz = new Gaz(5, 5, 5, 5, "5", 1500, 100);
        Container cooler = new Cooler(5, 5, 5, 5, "5", 1200, "", 5);
        
        Console.Write(_showListContainer());
        
        //Załadowanie ładunku do danego kontenera
        Console.WriteLine(liquid._serialNumber);
        liquid.LoadLoad(liquid._serialNumber);
        gaz.LoadLoad(gaz._serialNumber);
        cooler.LoadLoad(cooler._serialNumber);
        
        //Załadowanie kontenera na statek
        
        
        //Załadowanie listy kontenerów na statek
        
        
        //Usunięcie kontenera ze statku
        
        
        //Rozładowanie kontenera
        
        
        //Zastąpienie kontenera na statku o danym numerze innym kontenerem
        
        
        //Możliwość przeniesienie kontenera między dwoma statkami
        
        
        //Wypisanie informacji o danym kontenerze
        
        
        //Wypisanie informacji o danym statku i jego ładunku
        
        
        //liquid.LoadLoad();
        // bool on = true;
        // while (on)
        // {
        //     Terminal();
        // }
        //
        // Ship.CreateExmple_Ship();
        // Console.WriteLine(_showListShip());
        // Console.WriteLine(_showListContainer());
    }

    /*Default start of Terminal*/
    private static string _showListContainer()
    {
        string a = $"░░░▒▒▒▓▓▓▓ Lista kontenerów: ▓▓▓▓▒▒▒░░░\n" +
                   $"█ Wszystkich kontenerów: {_containerCount}\n" +
                   $"╔═════════════╦════════════╦══════════════╦═══════╦═══════════════╦════════╗\n" +
                   $"║ Weight_Load ║ Weight_Max ║ Weight_Shell ║ Depth ║ Serial_Number ║ Height ║\n" +
                   $"╠═════════════╬════════════╬══════════════╬═══════╬═══════════════╬════════╣\n" +
                   $"{Container.DrawContainer()}";
        return a;
    }
    
    private static int _containerCount { get { return Container.DB_Container.Count; } }
    
    private static string _showListShip()
    {
        string a = $"░░░▒▒▒▓▓▓▓ Lista kontenerowców: ▓▓▓▓▒▒▒░░░\n" +
                   $"█ Wszystkich kontenerowców: {_shipCount}\n" +
                   $"╔═════════╦═══════╦══════════════╦════════════╗\n" +
                   $"║   Name  ║ Speed ║   Cont_Num   ║   Weight   ║\n" +
                   $"╠═════════╬═══════╬══════════════╬════════════╣\n" +
                   $"{Ship.DrawShip()}";
        return a;
    }
    
    private static int _shipCount { get { return Ship.DB_Ship.Count; } }

    private static void Terminal()
    {
        Console.WriteLine(_showListShip());
        Console.WriteLine(_showListContainer());
        
    }
}

