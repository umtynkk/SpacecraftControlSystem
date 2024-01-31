using SpacecraftControlSystem.Models;
using SpacecraftControlSystem.Processors;

Console.WriteLine("Welcome to the Spacecraft Control System!");

string[]? surfaceCoordinates;
byte maxX;
byte maxY;

do
{
    try
    {
        Console.WriteLine($"Enter the coordinates of the top-right corner for Mars surface (e.g., 5 5):");
        surfaceCoordinates = Console.ReadLine().Split(' ');
        maxX = byte.Parse(surfaceCoordinates[0]);
        maxY = byte.Parse(surfaceCoordinates[1]);
        break;
    }
    catch (Exception) { continue; }
} while (true);

var planet = new PlanetModel()
{
    MaxX = maxX,
    MaxY = maxY,
};

Console.WriteLine("Enter the number of spacecrafts:");
int spacecraftCount = int.Parse(Console.ReadLine());

List<SpacecraftModel> spacecrafts = new List<SpacecraftModel>();

for (int i = 0; i < spacecraftCount; i++)
{
    string[] craftInfo;
    byte x;
    byte y;
    char direction;

    do
    {
        try
        {
            Console.WriteLine($"Enter the initial position and orientation of Spacecraft {i + 1} (e.g., 1 2 N):");
            craftInfo = Console.ReadLine().Split(' ');
            x = byte.Parse(craftInfo[0]);
            y = byte.Parse(craftInfo[1]);
            direction = char.Parse(craftInfo[2]);
            break;
        }
        catch (Exception) { continue; }
    } while (true);


    string movementCommands;
    do
    {
        Console.WriteLine($"Enter the movement commands for Spacecraft {i + 1} (e.g., LMLMLMLMM):");
        movementCommands = Console.ReadLine();
    } while (string.IsNullOrWhiteSpace(movementCommands));

    var spaceCraft = new SpacecraftModel()
    {
        Direction = direction,
        PosX = x,
        PosY = y
    };

    SpacecraftControlProcessor processor = new SpacecraftControlProcessor(spaceCraft, planet);
    var spaceCraftMoveResponse = processor.Move(new SpacecraftControlMoveModel
    {
        MovementCommands = movementCommands
    });

    if (spaceCraftMoveResponse.Meta.Success)
    {
        spacecrafts.Add(spaceCraftMoveResponse.Data);
    }
    else
    {
        Console.WriteLine($"Error: { (spaceCraftMoveResponse.Meta != null && !string.IsNullOrWhiteSpace(spaceCraftMoveResponse.Meta.Message) ? spaceCraftMoveResponse.Meta.Message : "An error has occurred!") }");
    }
}

foreach (var spacecraft in spacecrafts)
{
    Console.WriteLine($"Result for Spacecraft {spacecrafts.IndexOf(spacecraft) + 1}:");
    Console.WriteLine($"{spacecraft.PosX} {spacecraft.PosY} {spacecraft.Direction}\n");
}

Console.ReadLine(); // Wait for a key press to keep the console open.