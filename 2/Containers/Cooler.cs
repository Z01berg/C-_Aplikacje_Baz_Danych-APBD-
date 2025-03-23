namespace APBD2;

public class Cooler : Container
{
    private string productType;
    private double temperature;
    public Cooler(int weightLoad, int height, int weightShell, int depth, string serialNumber, int weightMax, string _productType, double _temperature) :
        base(weightLoad, height, weightShell, depth, serialNumber, weightMax)
    {
        this.productType = _productType;
        this.temperature = _temperature;
    }

    public override void LoadLoad(string id)
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
                if (this._temperature < GetRequiredTemperature())
                {
                    throw new TemperatureMismatchException($"Temperatura kontenera jest zbyt niska dla produktu {_productType}.");
                }
            }
        }
    }

    private double GetRequiredTemperature()
    {
        throw new NotImplementedException();
    }
    
    public override string CreateSerialNumber()
    {
        string serialNumber = $"KON-C-{ReturnID()}";
        return serialNumber;
    }
}

public class TemperatureMismatchException : Exception
{
    public TemperatureMismatchException(string s)
    {
        throw new NotImplementedException();
    }
}

