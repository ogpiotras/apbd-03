namespace Cwiczenia03App;

public class CoolingContainer(double cargoWeight, double cheight, double cWeight, double cDepth, string cSerialNumber, double cMaxLoad, string productType, double containerTemperature)
    : Container(cargoWeight, cheight, cWeight, cDepth, "C", cMaxLoad), IHazardNotifier
{
    
    private string ProductType { get; } = productType;
    private double ContainerTemperature { get; } = containerTemperature;

    private readonly Dictionary<string, double> RequiredTemperatures = new Dictionary<string, double>
    {
        { "Bananas", 13.3 },
        { "Chocolate", 18 },
        { "Fish", 2 },
        { "Meat", -15 },
        { "Ice cream", -18 },
        { "Frozen pizza", -30 },
        { "Cheese", 7.2 },
        { "Sausage", 5 },
        { "Butter", 20.5 },
        { "Eggs", 19 },
    };
    
    public void HazardNotifier(string containerNumber, string message)
    {
        Console.WriteLine($"[DANGER!] Container {containerNumber}: {message}");
    }

    private void checkTemp()
    {
        if (!RequiredTemperatures.ContainsKey(ProductType))
        {
            throw new ArgumentException($"Unknown product: {ProductType}");
        }
        
        double requiredTemp = RequiredTemperatures[ProductType];
        if (ContainerTemperature != requiredTemp)
        {
            HazardNotifier(ContainerSerialNumber, $" To low temperature: {ProductType}: {requiredTemp}");
            throw new InvalidOperationException("Incorrect temperature");
        }
    }

    public override void Load(double weight)
    {
        checkTemp();
        
        double maxLoad = ContainerMaxLoad * 0.9;
        
        if(weight >  0)
        {
            throw new ArgumentException("Weight must be greater than zero");
        }

        if (CargoWeight + weight > maxLoad)
        {
            HazardNotifier(ContainerSerialNumber, "To much weight for cooling container");
            throw new OverFillException("Too much weight for cooling container.");
        }
        CargoWeight += weight;
        Console.WriteLine($"Loaded {weight} kg od {ProductType} into {ContainerSerialNumber}");
    }

    
}