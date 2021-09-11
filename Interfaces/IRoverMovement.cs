using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarsRover.Inputs;

namespace MarsRover.Interfaces
{
    public interface IRoverMovement
    {
        string MoveRover(MarsRoverUserInput userInput);
    }
}
