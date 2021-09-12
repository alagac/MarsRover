using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover.Inputs
{
    public class MarsRoverUserInput
    {
        public string GridUpperRightCoords { get; set; }
        public RoverPropertiesAndMovement RoverPropMovement { get; set; }
    }

    public class RoverPropertiesAndMovement
    {
        public string RoverPositionAndDirection { get; set; }
        public string RoverMovement { get; set; }
    }
}
