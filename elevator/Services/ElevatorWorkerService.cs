
using elevator.Models;

namespace elevator.Services;

public class ElevatorWorkerService: BackgroundService
{
    // 10 seconds
    private const int OperationDelay = 10 * 1000; 
 
    private readonly Elevator _elevator;

    public ElevatorWorkerService(
        Elevator elevator) =>
        (_elevator) = (elevator);
        
    /// <summary>
    /// Background Service that waits 10 seconds and then tells the elevator it is time to do it's action.
    /// </summary>
    /// <param name="stoppingToken"></param>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(OperationDelay, stoppingToken);
            await DoElevatorOperation();
        }
    }

    /// <summary>
    /// Tells the Elevator to complete another action.
    /// </summary>
    /// <returns>Success Task if everything worked as expected</returns>
    private Task DoElevatorOperation()
    {
        if (_elevator.CallList.Count > 0)
        {
            _elevator.DoSingleElevatorOperation();
        }

        return Task.FromResult("Operation succeeded");
    }
}