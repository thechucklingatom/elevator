namespace elevator;

public class Elevator
{
    public int CurrentFloor { get; set; } = 0;

    public ButtonPanel ButtonPanel { get; } = new();
}