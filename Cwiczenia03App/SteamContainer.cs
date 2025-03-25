namespace Cwiczenia03App;

public class SteamContainer(double cargoWeight, double cheight, double cWeight, double cDepth, string cSerialNumber, double cMaxLoad, double pressure)
    : Container(cargoWeight, cheight, cWeight, cDepth, "G", cMaxLoad), IHazardNotifier
{

    private double Pressure { get; set; }
    
    
    public void HazardNotifier(string containerNumber, string message)
    {
        Console.WriteLine($"[DANGER!] Container {containerNumber}: {message}");
    }



    public override void Load(double weight)
    {
        double maxAllowed = ContainerMaxLoad * 0.9;

        if (weight < 0)
        {
            throw new ArgumentException("Load weight cannot be negative.");
        }
        if (CargoWeight + weight > maxAllowed)
        {
            HazardNotifier(ContainerSerialNumber, "Too much weight for gas container.");
            throw new OverFillException("Too much weight for gas container.");
        }

        CargoWeight += weight;
        HazardNotifier(ContainerSerialNumber, $"Loaded {weight} kg of gas. Actual weight is: {CargoWeight} kg (Max Allowed: {maxAllowed}). Pressure: {Pressure} bar.");
    }

    public override void Unload()
    {
        HazardNotifier(ContainerSerialNumber, "Releasing pressure before unloading");
    }
    
}