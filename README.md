# elevator

To run this code either run `dotnet run` from the command line, or use the run option in your favorite editor.

## Call the elevator to your floor

Once the API is running it will be available to call an elevator using either the swagger endpoint
or Postman. To call an elevator to your floor you will send a post request to `/Elevator/Call` with floor
number as part of the request.

## See the elevators status

Call `/Elevator/` with a get request to get an Elevator object that tells it's current floor and the list of floors it
is working on servicing. 

sample return

```json
{
  "currentFloor": 2,
  "callList": [
    1, 6, 7, 10
  ]
}
```

## Take elevator to a floor

Call `/Elevator/ButtonPanel/AvailableFloors` as a get request to get a list of the floors the current user is authorized
to call. If you would like to get a token you can use the username `test` and the password `password` to see the private
floors. Once the users have decided which floors they would like to go to they can "push" the button panel by posting
the list of ints that they want to go to the `/Elevator/ButtonPanel/GoToFloors` endpoint. 

## Elevator logic

The elevator will process either letting people off, or moving to a floor every 10 seconds. Once floors have been added
the elevator will start moving to those floors. When people add floors it will continue in it's current direction until
there are no more requests closer to the extreme it is heading towards. At that point it will switch directions and
service those floors, or stop completely if there are no more floors that require servicing.