using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarsRover.Inputs;
using MarsRover.Data;
using MarsRover.Interfaces;

namespace MarsRover.Data
{
    public class RoverMovement : IRoverMovement
    {
        public const string InvalidOperationString = "Invalid movement";
        RoverPositionDirection positionDirection;
        IUserInputValidation _userInputValidation;
        public RoverMovement(IUserInputValidation userInputValidation)
        {
            positionDirection = new RoverPositionDirection();
            _userInputValidation = userInputValidation;
        }

        public string MoveRover(MarsRoverUserInput userInput)
        {
            string movementMessage = "";
            movementMessage = ValidateUserInputs(userInput);
            if (string.IsNullOrEmpty(movementMessage))
            {
                List<string> roverpositionDirection = userInput.RoverPropMovement.RoverPositionAndDirection.Split(' ').Where(i => !string.IsNullOrEmpty(i)).ToList();
                positionDirection.XCoordinate = Convert.ToInt32(roverpositionDirection[0]);
                positionDirection.YCoordinate = Convert.ToInt32(roverpositionDirection[1]);
                positionDirection.Direction = (Direction)Enum.Parse(typeof(Direction), roverpositionDirection[2]);

                List<string> gridCoords = userInput.GridUpperRightCoords.Split(' ').Where(i => !string.IsNullOrEmpty(i)).ToList();
                int gridX = Convert.ToInt32(gridCoords[0]);
                int gridY = Convert.ToInt32(gridCoords[1]);

                foreach (char c in userInput.RoverPropMovement.RoverMovement)
                {
                    switch (c)
                    {
                        case 'L':
                            RotateLeft();
                            break;
                        case 'R':
                            RotateRight();
                            break;
                        case 'M':
                            Move();
                            break;
                        default:
                            break;
                    }

                    if (positionDirection.XCoordinate < 0 || positionDirection.XCoordinate > gridX || positionDirection.YCoordinate < 0 || positionDirection.YCoordinate > gridY)
                    {
                        movementMessage = $"{InvalidOperationString}: Rover can't go beyond plateau boundaries.";
                        return movementMessage;
                    }
                }

                movementMessage = $"{positionDirection.XCoordinate} {positionDirection.YCoordinate} {positionDirection.Direction}";
            }

            return movementMessage;
        }

        private string ValidateUserInputs(MarsRoverUserInput input)
        {
            ValidationOperationResult result = _userInputValidation.ValidateUserInput(input);
            return result.IsValid ? "" : result.ResultMessage;
        }

        private void RotateLeft()
        {
            switch (positionDirection.Direction)
            {
                case Direction.N:
                    positionDirection.Direction = Direction.W;
                    break;
                case Direction.W:
                    positionDirection.Direction = Direction.S;
                    break;
                case Direction.E:
                    positionDirection.Direction = Direction.N;
                    break;
                case Direction.S:
                    positionDirection.Direction = Direction.E;
                    break;
                default:
                    break;
            }
        }

        private void RotateRight()
        {
            switch (positionDirection.Direction)
            {
                case Direction.N:
                    positionDirection.Direction = Direction.E;
                    break;
                case Direction.W:
                    positionDirection.Direction = Direction.N;
                    break;
                case Direction.E:
                    positionDirection.Direction = Direction.S;
                    break;
                case Direction.S:
                    positionDirection.Direction = Direction.W;
                    break;
                default:
                    break;
            }
        }

        private void Move()
        {
            switch (positionDirection.Direction)
            {
                case Direction.N:
                    positionDirection.YCoordinate += 1;
                    break;
                case Direction.W:
                    positionDirection.XCoordinate -= 1;
                    break;
                case Direction.E:
                    positionDirection.XCoordinate += 1;
                    break;
                case Direction.S:
                    positionDirection.YCoordinate -= 1;
                    break;
                default:
                    break;
            }
        }

    }
}
