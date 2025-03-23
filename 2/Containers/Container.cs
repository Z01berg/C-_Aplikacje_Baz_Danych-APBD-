namespace APBD2;

abstract public class Container
{
    public static List<Container> DB_Container = new List<Container>();

    public int _weightLoad { get; set; } //in kg
    public int _height { get; set; } //in cm
    public int _weightShell { get; set; } //in kg
    public int _depth { get; set; } //in cm
    public string _serialNumber { get; set; }
    public int _weightMax { get; set; } //in kg
    public bool _isHazardous { get; set; } //check if liquid hazard
    public double _pressure { get; set; } 
    public string _productType { get; set; }
    public double _temperature { get; set; }


    public Container(int weightLoad, int height, int weightShell, int depth, string serialNumber, int weightMax,
        bool? isHazardous = null, double? pressure = null, string? productType = null, double? temperature = null)
    {
        this._weightLoad = weightLoad;
        this._height = height;
        this._weightShell = weightShell;
        this._depth = depth;
        this._serialNumber = CreateSerialNumber();
        this._weightMax = weightMax;
        this._isHazardous = (bool)isHazardous;
        this._pressure = (double)pressure;
        this._productType = productType;
        this._temperature = (double)temperature;
        DB_Container.Add(this);
    }

    public virtual string CreateSerialNumber()
    {
        string serialNumber = $"KON--{ReturnID()}";
        return serialNumber;
    }

    
    /* Random Generation Number */
    public static string GenerateID()
    {
        Guid guid = Guid.NewGuid();

        string guidString = guid.ToString("N");
        string firstFiveChars = guidString.Substring(0, 5);

        string code = "";
        foreach (char c in firstFiveChars)
        {
            if (char.IsLetter(c))
            {
                int unicodeValue = (int)c;
                int digitalValue = unicodeValue % 10;
                code += digitalValue.ToString();
            }
            else
            {
                code += c;
            }
        }

        return code;
    }

    public static bool CheckFalseID(string id)
    {
        foreach (var con in DB_Container)
        {
            string check = con._serialNumber.Substring(5);
            if (check == id)
            {
                return true;
            }
        }

        return false;
    }

    public static string ReturnID()
    {
        string id;

        do
        {
            id = GenerateID();
        } while (CheckFalseID(id));

        return id;
    }

    
    /* Lodaout */
    public static void ClearLoad(string id)
    {
        foreach (var con in DB_Container)
        {
            if (con._serialNumber.Equals(id))
            {
                con._weightLoad = 0;
            }
        }
    }

    public virtual void LoadLoad(string id)
    {
        foreach (var con in DB_Container)
        {
            if (con._serialNumber.Equals(id))
            {
                int loadMass = con._weightMax - (con._weightLoad + con._weightShell);
                if (loadMass < 0)
                {
                    throw new OverfillException("Masa ładunku przekracza pojemność kontenera.");
                }
            }
        }
    }
    
    /*Show Table*/
    public static string DrawContainer()
    {
        string a = "";
        if (DB_Container.Count == 0)
        {
            a += $"╚═════════════╩════════════╩══════════════╩═══════╩═══════════════╩════════╝\n\n";
        }
        else
        {
            foreach (var container in DB_Container)
            {
                a += $"║    {container._weightLoad}     ║    {container._weightMax}    ║      {container._weightShell}     ║   {container._depth}  ║   {container._serialNumber} ║   {container._height}   ║\n";
            
                if (DB_Container.IndexOf(container) == DB_Container.Count - 1)
                {
                    a += $"╚═════════════╩════════════╩══════════════╩═══════╩═══════════════╩════════╝\n\n";
                }
                else
                {
                    a += $"╠═════════════╬════════════╬══════════════╬═══════╬═══════════════╬════════╣\n";
                }
            }
        }

        return a;
    }
}

internal class OverfillException : Exception
{
    public OverfillException(string masaŁadunkuPrzekraczaPojemnośćKontenera)
    {
        throw new NotImplementedException();
    }
}