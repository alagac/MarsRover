using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover.Data
{
    public class RoverPositionDirection
    {
        public int XCoordinate { get; set; }
        public int YCoordinate { get; set; }
        public Direction Direction { get; set; }
    }

    public enum Direction
    {
        N = 1,
        W = 2,
        E = 3,
        S = 4
    }
}
