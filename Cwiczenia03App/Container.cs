namespace Cwiczenia03App;

public class Container
{
    public double CargoWeight { get; set; }
    public double ContainerHeight { get; set; }
    public double ContainerWeight { get; set; }
    public double ContainerDepth { get; set; }
    public string ContainerSerialNumber { get; set; }
    public double ContainerMaxLoad { get; set; }
    private static int IdCounter = 0;
    private static HashSet<string> UsedSerialNumbers = new HashSet<string>();

    public Container(double cargoWeight, double cheight, double cWeight, double cDepth, string cSerialNumber, double cMaxLoad)
    {
        CargoWeight = cargoWeight;
        ContainerHeight = cheight;
        ContainerWeight = cWeight;
        ContainerDepth = cDepth;
        ContainerSerialNumber = GenerateSerialNumber(cSerialNumber);
        ContainerMaxLoad = cMaxLoad;
    }

    public virtual void Unload()
    {
        CargoWeight = 0;
        Console.WriteLine($"Container {ContainerSerialNumber} unloaded.");
    }

    public virtual void Load(double weight)
    {
        if (weight < 0)
        {
            throw new ArgumentException("Load weight cannot be negative.");
        }
        if (CargoWeight + weight > ContainerMaxLoad)
        {
            throw new OverFillException("Too much weight.");
        }
        CargoWeight += weight;
        Console.WriteLine($"Loaded {weight} kg into {ContainerSerialNumber}. Actual weight is: {CargoWeight} kg.");
    }

    private string GenerateSerialNumber(string containerType)
    {
        string serialNumber;
        do
        {
            IdCounter++;
            serialNumber = $"KON-{containerType}-{IdCounter}";
        } while (UsedSerialNumbers.Contains(serialNumber));

        UsedSerialNumbers.Add(serialNumber);
        return serialNumber;
    }
}