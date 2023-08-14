using elevator.Models;

namespace ElevatorTests;

public class ElevatorTests
{

    private Elevator _elevator;
    
    [SetUp]
    public void Setup()
    {
        _elevator = new Elevator();
    }
    
    [Test]
    public void Test_Elevator_ClearsFloorFromCallListWhenArrivesAtFloor()
    {
        _elevator.CallList.Add(2);
        _elevator.DoSingleElevatorOperation();
        Assert.That(_elevator.CurrentFloor == 2);
        Assert.That(_elevator.CallList.Count > 0);
        _elevator.DoSingleElevatorOperation();
        Assert.That(_elevator.CurrentFloor == 2);
        Assert.That(_elevator.CallList.Count == 0);
    }

    [Test]
    public void Test_Elevator_GoesUpWhenCallListHasSomethingAboveCurrentFloor()
    {
        _elevator.CallList.Add(6);
        _elevator.DoSingleElevatorOperation();
        Assert.That(_elevator.CurrentFloor == 2);
    }
    
    [Test]
    public void Test_Elevator_ChangesDirectionWhenOnlyFloorsAreInOppositeDirection()
    {
        _elevator.CurrentFloor = 6;
        _elevator.CallList.Add(4);
        _elevator.DoSingleElevatorOperation();
        Assert.That(_elevator.CurrentFloor == 5);
    }
}