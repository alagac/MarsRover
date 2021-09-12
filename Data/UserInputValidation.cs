using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MarsRover.Inputs;
using MarsRover.Interfaces;

namespace MarsRover.Data
{
    public class UserInputValidation : IUserInputValidation
    {
        public ValidationOperationResult ValidateUserInput(MarsRoverUserInput userInputs)
        {
            ValidationOperationResult result = ValidateGridCoordinates(userInputs.GridUpperRightCoords);
            if (result.IsValid)
            {
                result = ValidateRoverPositionDirection(userInputs.RoverPropMovement.RoverPositionAndDirection, userInputs.GridUpperRightCoords);
                if (result.IsValid)
                {
                    result = ValidateRoverMovement(userInputs.RoverPropMovement.RoverMovement);
                }
            }
            return result;
        }

        private ValidationOperationResult ValidateGridCoordinates(string upperRightCoords)
        {
            ValidationOperationResult result = new ValidationOperationResult() { IsValid = false, ResultMessage = ValidationMessages.InvalidGridCoords };
            if(!string.IsNullOrEmpty(upperRightCoords) && upperRightCoords.Split(' ').Count(i => !string.IsNullOrEmpty(i)) == 2)
            {
                List<string> coords = upperRightCoords.Split(' ').Where(i => !string.IsNullOrEmpty(i)).ToList();
                int x, y;
                result.IsValid = (int.TryParse(coords[0], out x) && int.TryParse(coords[1], out y));
            }
            return result;
        }

        private ValidationOperationResult ValidateRoverPositionDirection(string positionAndDirection, string upperRightCoords)
        {
            ValidationOperationResult result = new ValidationOperationResult() 
            {
                IsValid = true
            };
            if (!string.IsNullOrEmpty(positionAndDirection) && positionAndDirection.Split(' ').Count(i => !string.IsNullOrEmpty(i)) == 3)
            {
                List<string> coords = positionAndDirection.Split(' ').Where(i => !string.IsNullOrEmpty(i)).ToList();
                int x, y;
                if (!int.TryParse(coords[0], out x) || !int.TryParse(coords[1], out y))
                {
                    result.IsValid = false;
                    result.ResultMessage = ValidationMessages.InvalidRoverPosition;
                    return result;
                }
                else
                {
                    List<string> upperCoords = upperRightCoords.Split(' ').Where(i => !string.IsNullOrEmpty(i)).ToList();
                    if (x < 0 || x > int.Parse(upperCoords[0]) || y < 0 || y > int.Parse(upperCoords[1]))
                    {
                        result.IsValid = false;
                        result.ResultMessage = $"{ValidationMessages.InvalidRoverPosition}: Rover coordinates should not be negative or greater than upper x and y coordinates of the plateau";
                        return result;
                    }
                    string controlPattern = "^[NWES]+$";
                    result.IsValid = Regex.IsMatch(coords[2], controlPattern);
                    result.ResultMessage = ValidationMessages.InvalidRoverDirection;
                }
            }
            else
            {
                result.IsValid = false;
                result.ResultMessage = $"{ValidationMessages.InvalidRoverPosition} and/or {ValidationMessages.InvalidRoverDirection}";
            }
            return result;
        }

        private ValidationOperationResult ValidateRoverMovement(string movement)
        {
            ValidationOperationResult result = new ValidationOperationResult() { IsValid = false, ResultMessage = ValidationMessages.InvalidRoverMovement };
            if (!string.IsNullOrEmpty(movement) && !movement.Contains(" "))
            {
                string controlPattern = "^[LRM]+$";
                result.IsValid = Regex.IsMatch(movement, controlPattern);
            }
            return result;
        }

    }

    public class ValidationMessages
    {
        public const string InvalidGridCoords = "Invalid plateau coordinates";
        public const string InvalidRoverPosition = "Invalid rover position";
        public const string InvalidRoverDirection = "Invalid rover direction";
        public const string InvalidRoverMovement = "Invalid rover movement";
    }

    public class ValidationOperationResult
    {
        public bool IsValid { get; set; }
        public string ResultMessage { get; set; }
    }
}
