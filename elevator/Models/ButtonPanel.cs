using System.Text.Json.Serialization;

namespace elevator;

public class ButtonPanel
{
    public int MaxFloor => Math.Max(PublicFloors.Max(), PrivateFloors.Max());
    public int MinFloor => Math.Min(PublicFloors.Min(), PrivateFloors.Min());

    public List<int> PublicFloors { get; } = new(){0, 2, 3, 4, 5, 6, 7};
    
    public List<int> PrivateFloors { get; } = new(){8, 9, 10};
}