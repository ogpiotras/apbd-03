namespace Cwiczenia03App;

public class Program
{
   static void Main(string[] args)
   {
       
       bool running = true;
       
       Console.Write("Enter ships max weight: (in tons) ");
       double maxWeight = double.Parse(Console.ReadLine());
       
       Console.Write("Enter max container number: ");
       int maxContainer = int.Parse(Console.ReadLine());
       
       Console.Write("Enter max ship speed: ");
       int MaxSpeed = int.Parse(Console.ReadLine());
       
       var ship = new ContainerShip(maxWeight, maxContainer, MaxSpeed);
       var containers = new List<Container>();

       while (running)
       {
           Console.WriteLine("\n=== Container Management System ===");
           Console.WriteLine("1. Create a new container");
           Console.WriteLine("2. Load a container onto the ship");
           Console.WriteLine("3. Load a list of containers onto the ship");
           Console.WriteLine("4. Unload a container from the ship");
           Console.WriteLine("5. Unload a container's cargo");
           Console.WriteLine("6. Replace a container on the ship");
           Console.WriteLine("7. Print container info");
           Console.WriteLine("8. Print ship info");
           Console.WriteLine("9. Exit");
           Console.Write("Select an option: ");
           
           string input = Console.ReadLine();

           try
           {
               switch (input)
               {
                   case "1":
                       Console.Write("Select container type: (1.Fluid, 2:Steam, 3:Cooling");
                       string type = Console.ReadLine();
                       Container container = null;

                       if (type == "1")
                       {
                           Console.Write("Is it hazardous? (y/n)");
                           bool isHazardous = Console.ReadLine().ToLower() == "y";
                           container = new FluidContainer(0, 100, 1000, 100, "L", 5000, isHazardous);
                       }
                       else if (type == "2")
                       {
                           Console.Write("Enter pressure: ");
                           double pressure = double.Parse(Console.ReadLine());
                           container = new SteamContainer(0, 100, 1000, 100, "G", 3000, pressure);
                       }
                       else if (type == "3")
                       {
                           Console.Write("Enter product type: e.q Bananas ");
                           string productType = Console.ReadLine();
                           Console.Write("Enter container temp in (\u00b0C): ");
                           double temp = double.Parse(Console.ReadLine());
                           container = new CoolingContainer(0, 100, 1000, 100, "C", 4000, productType, temp);
                       }

                       if (container != null)
                       {
                           containers.Add(container);
                           Console.WriteLine($"Container {container.ContainerSerialNumber} created");
                       }
                       else
                       {
                           Console.WriteLine("Invalid container type");
                       }

                       break;

                   case "2":
                       Console.Write("Enter container serial number to load: ");
                       string serialToLoad = Console.ReadLine();
                       var containerToLoad = containers.FirstOrDefault(c => c.ContainerSerialNumber == serialToLoad);
                       if (containerToLoad != null)
                       {
                           ship.LoadContainers(containerToLoad);
                       }
                       else
                       {
                           Console.WriteLine("Container not found");
                       }

                       break;
                   case "3":
                       ship.LoadContainers(containers);
                       break;

                   case "4":
                       Console.Write("Enter container serial number to unload: ");
                       string serialToUnload = Console.ReadLine();
                       ship.UnloadContainers(serialToUnload);
                       break;


                   case "5":
                       Console.Write("Enter container serial number to unload cargo: ");
                       string serialToUnloadCargo = Console.ReadLine();
                       var containerToUnloadCargo =
                           containers.FirstOrDefault(c => c.ContainerSerialNumber == serialToUnloadCargo);
                       if (containerToUnloadCargo != null)
                       {
                           containerToUnloadCargo.Unload();
                       }
                       else
                       {
                           Console.WriteLine("Container not found");
                       }

                       break;

                   case "6":
                       Console.Write("Enter container serial number to replace: ");
                       string serialToReplace = Console.ReadLine();
                       Console.WriteLine("Create a nwe container to replace with: ");
                       Console.WriteLine("Select container type (1: Fluid, 2: Steam, 3: Cooling): ");
                       string newType = Console.ReadLine();
                       Container newContainer = null;

                       if (newType == "1")
                       {
                           Console.Write("Is it hazardous? (y/n): ");
                           bool isHazardous = Console.ReadLine().ToLower() == "y";
                           newContainer = new FluidContainer(0, 100, 1000, 100, "L", 5000, isHazardous);
                       }
                       else if (newType == "2")
                       {
                           Console.Write("Enter pressure (bar): ");
                           double pressure = double.Parse(Console.ReadLine());
                           newContainer = new SteamContainer(0, 100, 1000, 100, "G", 3000, pressure);
                       }
                       else if (newType == "3")
                       {
                           Console.Write("Enter product type (e.g., Bananas, Chocolate): ");
                           string productType = Console.ReadLine();
                           Console.Write("Enter container temperature (°C): ");
                           double temp = double.Parse(Console.ReadLine());
                           newContainer = new CoolingContainer(0, 100, 1000, 100, "C", 4000, productType, temp);
                       }

                       if (newContainer != null)
                       {
                           containers.Add(newContainer);
                           ship.ReplaceContainer(serialToReplace, newContainer);
                       }
                       else
                       {
                           Console.WriteLine("Invalid container type.");
                       }

                       break;

                   case "7":
                       Console.Write("Enter container serial number to print Info");
                       string serialToPrint = Console.ReadLine();
                       var containerToPrint = containers.FirstOrDefault(c => c.ContainerSerialNumber == serialToPrint);
                       if (containerToPrint != null)
                       {
                           ship.PrintContainerInfo(containerToPrint);
                       }
                       else
                       {
                           Console.WriteLine("Container not found.");
                       }

                       break;

                   case "8":
                       ship.PrintShipInfo();
                       break;

                   case "9":
                       Console.WriteLine("Exiting...");
                       running = false;
                       break;

                   default:
                       Console.WriteLine("Invalid option");
                       break;
               }
           }
           catch (Exception e)
           {
               Console.WriteLine(e.Message);
           }
       }
   }
}