namespace APBD2;

class Liquid : Container, IHazardNotifier
{
    public Liquid(int weightLoad, int height, int weightShell, int depth, string serialNumber, int weightMax,
        bool? isHazardous) : base(weightLoad, height, weightShell, depth, serialNumber, weightMax)
    {
    }
    
    public void NotifyDangerousSituation(string containerNumber)
    {
        Console.WriteLine($"Notyfikacja: Kontener o numerze {containerNumber} znajduje się w niebezpiecznej sytuacji!");
    }

    public override string CreateSerialNumber()
    {
        string serialNumber = $"KON-L-{ReturnID()}";
        return serialNumber;
    }
    public override void LoadLoad(string id)
    {
        foreach (var con in DB_Container)
        {
            if (con._serialNumber.Equals(id))
            {
                int loadMass = con._weightMax - (con._weightLoad + con._weightShell);
                if (con._isHazardous)
                {
                    // For hazardous containers, load up to 50% of capacity
                    if (loadMass < con._weightMax * 0.5)
                    {
                        throw new OverfillException("Masa ładunku przekracza 50% pojemności kontenera.");
                    }
                }
                else
                {
                    // For non-hazardous containers, load up to 90% of capacity
                    if (loadMass < con._weightMax * 0.9)
                    {
                        throw new OverfillException("Masa ładunku przekracza 90% pojemności kontenera.");
                    }
                }
            }
        }
    }
}

internal interface IHazardNotifier
{
    void NotifyDangerousSituation(string containerNumber);
}

