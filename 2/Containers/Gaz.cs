namespace APBD2;

public class Gaz : Container, IHazardNotifier
{
    private double presure;
    
    public Gaz(int weightLoad, int height, int weightShell, int depth, string serialNumber, int weightMax, double _pressure) :
        base(weightLoad, height, weightShell, depth, serialNumber, weightMax)
    {
        this.presure = _pressure;
    }
    
    public void NotifyDangerousSituation(string containerNumber)
    {
        Console.WriteLine($"Notyfikacja: Kontener na gaz o numerze {containerNumber} znajduje się w niebezpiecznej sytuacji!");
    }

    public override string CreateSerialNumber()
    {
        string serialNumber = $"KON-G-{ReturnID()}";
        return serialNumber;
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
                else if (loadMass < con._weightMax * 0.05)
                {
                    throw new OverfillException("Pozostawienie 5% ładunku wewnątrz kontenera.");
                }
            }
        }
    }
}