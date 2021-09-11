using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarsRover.Inputs;

namespace MarsRover
{
    class Program
    {
        static void Main(string[] args)
        {
            MarsRoverUserInput inputs = new MarsRoverUserInput();
            inputs.GridUpperRightCoords = Console.ReadLine().Trim();
            inputs.RoverPositionAndDirection = Console.ReadLine().Trim();

        }
    }
}
