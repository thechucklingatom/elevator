using elevator.Models;
using Microsoft.AspNetCore.Mvc;

namespace elevator.Controllers;

[ApiController]
[Route("[controller]")]
public class ElevatorController: ControllerBase
{
    /// <summary>
    /// Return the elevator object to the caller
    /// </summary>
    /// <param name="elevator">The singleton Elevator registered to the service</param>
    /// <returns>The current Elevator we are watching</returns>
    [HttpGet]
    [ProducesResponseType(typeof(Elevator), 200)]
    public Elevator Get([FromServices] Elevator elevator)
    {
        return elevator;
    }
    
    
    /// <summary>
    /// Gets the floors the user is allowed to go to.
    /// </summary>
    /// <param name="elevator">The singleton Elevator registered to the service</param>
    /// <returns>The List of ints that contains the floors the user is allowed to access</returns>
    [HttpGet("ButtonPanel/AvailableFloors")]
    [ProducesResponseType(typeof(List<int>), 200)]
    public List<int> GetAvailableFloors([FromServices] Elevator elevator)
    {
        // exclude special floors if the user is not authenticated
        if (!(User.Identity?.IsAuthenticated ?? false))
        {
            return elevator.ButtonPanel.PublicFloors;
        }

        return elevator.ButtonPanel.PublicFloors.Concat(elevator.ButtonPanel.PrivateFloors).ToList();
    }

    /// <summary>
    /// Post method for pressing multiple buttons on the panel.
    /// </summary>
    /// <param name="elevator">The singleton Elevator registered to the service</param>
    /// <param name="floorsToGoTo">The floors the passengers want to visit</param>
    /// <returns>Status code based on how much valid data was passed. Bad Request if all floors are invalid.
    /// Ok if all floors are valid. Ok with a list of bad floors if some floors are invalid</returns>
    [HttpPost("ButtonPanel/GoToFloors")]
    public IActionResult PostGoToFloors([FromServices] Elevator elevator, List<int> floorsToGoTo)
    {
        List<int> badFloors = new();
        Boolean atLeastOneGoodFloor = false;
        floorsToGoTo.ForEach(floor => 
        {
            if (floor <= elevator.ButtonPanel.MinFloor || floor > elevator.ButtonPanel.MaxFloor)
            { 
                badFloors.Add(floor);
            }
            else if (!(User.Identity?.IsAuthenticated ?? false) && elevator.ButtonPanel.PrivateFloors.Contains(floor))
            {
                badFloors.Add(floor);
            }
            else
            {
                atLeastOneGoodFloor = true;
                elevator.CallList.Add(floor);
            }
        });

        if (badFloors.Count > 0 && !atLeastOneGoodFloor)
        {
            return BadRequest("All floors are invalid.");
        }

        if (badFloors.Count > 0)
        {
            return Ok($"Some floors not added: {string.Join(',', badFloors)}");
        }


        return Ok();
    }

    /// <summary>
    /// Calls the elevator to the users floor.
    /// </summary>
    /// <param name="elevator">The singleton Elevator registered to the service</param>
    /// <param name="floorToCallFrom">The floor the user is on</param>
    /// <returns>Status ok if the floor is valid, bad request otherwise</returns>
    [HttpPost("Call")]
    public IActionResult PostCallElevator([FromServices] Elevator elevator, int floorToCallFrom)
    {
        if (floorToCallFrom <= elevator.ButtonPanel.MinFloor || floorToCallFrom > elevator.ButtonPanel.MaxFloor)
        {
            return BadRequest("Invalid floor");
        }
        elevator.CallList.Add(floorToCallFrom);
        
        return Ok();
    }

}