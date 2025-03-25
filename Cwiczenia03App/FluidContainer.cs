namespace Cwiczenia03App;

public class FluidContainer
    (double cargoWeight, double cheight, double cWeight, double cDepth, string cSerialNumber, double cMaxLoad, bool isHazardous)
    : Container(cargoWeight, cheight, cWeight, cDepth, "L", cMaxLoad), IHazardNotifier
{
    private bool IsHazardous { get; } = isHazardous;

    public void HazardNotifier(string containerNumber, string message)
    {
        Console.WriteLine($"[DANGER!] Container {containerNumber}: {message}");
    }

    public override void Load(double weight)
    {
        double maxAllowed = IsHazardous ? ContainerMaxLoad * 0.5 : ContainerMaxLoad * 0.9;

        if (weight < 0)
        {
            throw new ArgumentException("Load weight cannot be negative.");
        }
        if (CargoWeight + weight > maxAllowed)
        {
            HazardNotifier(ContainerSerialNumber, "Too much weight.");
            throw new OverFillException("Too much weight for fluid container.");
        }

        CargoWeight += weight;
        if (IsHazardous)
        {
            HazardNotifier(ContainerSerialNumber, $"Loaded {weight} kg. Actual weight is: {CargoWeight} kg (Max Allowed: {maxAllowed}).");
        }
        else
        {
            Console.WriteLine($"Loaded {weight} kg into {ContainerSerialNumber}. Actual weight is: {CargoWeight} kg (Max Allowed: {maxAllowed}).");
        }
    }
}