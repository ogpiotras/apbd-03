namespace Cwiczenia03App;

public class ContainerShip(double maxWeight, int maxContainerNumber, int maxSpeed)
{
    private List<Container> Containers { get; set; } = new List<Container>();
    private double MaxWeight { get; set; } = maxWeight;
    private int MaxContainerNumber { get; set; } = maxContainerNumber;
    private int MaxSpeed { get; set; } = maxSpeed;
    private double CurrentSpeed { get; set; }

    
    public void LoadContainers(Container container)
    {
        double currentWeight = Containers.Sum(cont => (cont.CargoWeight + cont.ContainerWeight)) / 1000;
        double containerWeight = (container.CargoWeight + container.ContainerWeight) / 1000;

        if (Containers.Count >= MaxContainerNumber) // Poprawka: Sprawdzamy >= zamiast >
        {
            throw new InvalidOperationException("Can't load more containers. Max container limit reached.");
        }

        if (currentWeight + containerWeight > MaxWeight)
        {
            throw new InvalidOperationException("Can't load more containers. Max weight will be exceeded.");
        }

        Containers.Add(container);
        Console.WriteLine($"{container.ContainerSerialNumber} loaded on the ship");
    }

    
    public void LoadContainers(List<Container> containers)
    {
        foreach (var container in containers)
        {
            LoadContainers(container);
        }
    }

    
    public void UnloadContainers(string serialNumber)
    {
        var container = Containers.FirstOrDefault(c => c.ContainerSerialNumber == serialNumber);
        if (container == null)
        {
            throw new InvalidOperationException("Container not found.");
        }
        Containers.Remove(container);
        Console.WriteLine($"{container.ContainerSerialNumber} unloaded from the ship");
        
        UpdateCurrentSpeed();
    }

    
    public void ReplaceContainer(string serialNumber, Container newContainer)
    {
        UnloadContainers(serialNumber);
        LoadContainers(newContainer);
        Console.WriteLine($"{serialNumber} was unloaded and replaced with {newContainer.ContainerSerialNumber}");
    }

    
    public void PrintShipInfo()
    {
        Console.WriteLine("Ship info:");
        Console.WriteLine($"Max speed: {MaxSpeed} knots");
        Console.WriteLine($"Current speed: {CurrentSpeed:F2} knots");
        Console.WriteLine($"Max weight: {MaxWeight} tons");
        Console.WriteLine($"Max container number: {MaxContainerNumber}");
        Console.WriteLine($"Current number of containers: {Containers.Count}");
        Console.WriteLine($"Current weight on board: {Containers.Sum(cont => (cont.CargoWeight + cont.ContainerWeight)) / 1000} tons");
        Console.WriteLine("Loaded containers: ");
        foreach (var container in Containers)
        {
            PrintContainerInfo(container); 
        }
    }

   
    public void PrintContainerInfo(Container container)
    {
        Console.WriteLine($"Container {container.ContainerSerialNumber}:");
        Console.WriteLine($"Type: {(container is FluidContainer ? "Fluid" : container is SteamContainer ? "Steam" : "Cooling")}");
        Console.WriteLine($"Cargo Weight: {container.CargoWeight} kg");
        Console.WriteLine($"Container Weight: {container.ContainerWeight} kg");
        Console.WriteLine($"Max Load: {container.ContainerMaxLoad} kg");
        Console.WriteLine($"Height: {container.ContainerHeight} cm");
        Console.WriteLine($"Depth: {container.ContainerDepth} cm");
    }

   
    public Container FindContainer(string serialNumber)
    {
        return Containers.FirstOrDefault(c => c.ContainerSerialNumber == serialNumber);
    }

    
    private void UpdateCurrentSpeed()
    {
        double currentWeight = Containers.Sum(cont => (cont.CargoWeight + cont.ContainerWeight)) / 1000;
        double weightRatio = currentWeight / MaxWeight;
        
        CurrentSpeed = MaxSpeed * (1 - 0.5 * weightRatio);

        
        if (CurrentSpeed < MaxSpeed * 0.5)
        {
            CurrentSpeed = MaxSpeed * 0.5;
        }
    }
}