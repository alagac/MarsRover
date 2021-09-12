using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarsRover.Inputs;
using MarsRover.Interfaces;
using MarsRover.Data;

namespace MarsRover
{
    class Program
    {
        static void Main(string[] args)
        {
            MarsRoverUserInput inputs = new MarsRoverUserInput();
            inputs.GridUpperRightCoords = Console.ReadLine().Trim();
            List<RoverPropertiesAndMovement> roverPropsMove = new List<RoverPropertiesAndMovement>();
            string operationInfo = "Y";
            while (operationInfo.ToUpper() == "Y")
            {
                roverPropsMove.Add(new RoverPropertiesAndMovement()
                {
                    RoverPositionAndDirection = Console.ReadLine().Trim(),
                    RoverMovement = Console.ReadLine().Trim()
                });
                Console.WriteLine("Enter Y to add more rovers to the plateau. Enter anything else to see results.");
                operationInfo = Console.ReadLine().Trim();
            }

            IUserInputValidation userInputValidator = new UserInputValidation();
            IRoverMovement roverMovement = new RoverMovement(userInputValidator);
            foreach (var rpm in roverPropsMove)
            {
                inputs.RoverPropMovement = rpm;
                Console.WriteLine(roverMovement.MoveRover(inputs));
            }
            Console.ReadLine();
        }
    }
}
