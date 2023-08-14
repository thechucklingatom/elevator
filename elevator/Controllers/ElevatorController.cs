using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace elevator.Controllers;

[ApiController]
[Route("[controller]")]
public class ElevatorController: ControllerBase
{
    [HttpGet("")]
    public int GetCurrentFloor([FromServices] Elevator elevator)
    {
        return elevator.CurrentFloor;
    }
    
    
    [HttpGet("AvailableFloors")]
    public new List<int> GetAvailableFloors([FromServices] Elevator elevator)
    {
        //special floors
        if (!(User.Identity?.IsAuthenticated ?? false))
        {
            return elevator.ButtonPanel.PublicFloors;
        }

        return elevator.ButtonPanel.PublicFloors.Concat(elevator.ButtonPanel.PrivateFloors).ToList();
    }

}