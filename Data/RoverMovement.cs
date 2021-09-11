using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarsRover.Inputs;
using MarsRover.Interfaces;

namespace MarsRover.Data
{
    public class RoverMovement : IRoverMovement
    {
        RoverPositionDirection positionDirection;
        public RoverMovement()
        {
            positionDirection = new RoverPositionDirection();
        }

        public string MoveRover(MarsRoverUserInput userInput)
        {
            string movementMessage = "";







            return movementMessage;
        }
    }
}
