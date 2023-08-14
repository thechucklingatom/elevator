using System.Text.Json.Serialization;
using elevator.Enums;

namespace elevator.Models;


public class Elevator
{
    private ElevatorDirection Direction { get; set; } = ElevatorDirection.UP;

    public int CurrentFloor { get; set; } = 1;

    [JsonIgnore]
    public ButtonPanel ButtonPanel { get; } = new();

    public SortedSet<int> CallList { get; } = new();

    /// <summary>
    /// Helper method that moves the elevator one floor depending on it's current direction.
    /// </summary>
    private void MoveFloor()
    {
        if (Direction == ElevatorDirection.DOWN)
        {
            CurrentFloor -= 1;
        }
        else if (Direction == ElevatorDirection.UP)
        {
            CurrentFloor += 1;
        }
    }

    /// <summary>
    /// Causes the elevator to either move either stop on a floor to allow embarking and disembarking, to move to another floor,
    /// or to sit and wait for a call.
    /// </summary>
    public void DoSingleElevatorOperation()
    {
        if (CallList.Contains(CurrentFloor))
        {
            // we made it to the floor dump the passengers, or get the passengers
            CallList.Remove(CurrentFloor);
            return;
        }

        if (CallList.Count == 0)
        {
            // nothing to do. Should not hit since the server caller checks this.
            return;
        }

        if (!CallList.Any(floor => floor > CurrentFloor) && Direction == ElevatorDirection.UP)
        {
            Direction = ElevatorDirection.DOWN;
        }
        
        if (!CallList.Any(floor => floor < CurrentFloor) && Direction == ElevatorDirection.DOWN)
        {
            Direction = ElevatorDirection.UP;
        }
        
        MoveFloor();
    }
    
}