using System;
using MarsRover.Data;
using MarsRover.Inputs;
using MarsRover.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MarsRoverUnitTests
{
    [TestClass]
    public class RoverTests
    {
        IUserInputValidation userInputValidator;
        IRoverMovement roverMovement;
        public RoverTests()
        {
            userInputValidator = new UserInputValidation();
            roverMovement = new RoverMovement(userInputValidator);
        }

        [TestMethod]
        public void SuccessMethodOne()
        {
            MarsRoverUserInput input = new MarsRoverUserInput()
            {
                GridUpperRightCoords = "5 5",
                RoverPropMovement = new RoverPropertiesAndMovement()
                {
                    RoverPositionAndDirection = "1 2 N",
                    RoverMovement = "LMLMLMLMM"
                }
            };
            string resultMessage = roverMovement.MoveRover(input);
            Assert.IsTrue(resultMessage == "1 3 N");
        }

        [TestMethod]
        public void SuccessMethodTwo()
        {
            MarsRoverUserInput input = new MarsRoverUserInput()
            {
                GridUpperRightCoords = "5 5",
                RoverPropMovement = new RoverPropertiesAndMovement()
                {
                    RoverPositionAndDirection = "3 3 E",
                    RoverMovement = "MMRMMRMRRM"
                }
            };
            string resultMessage = roverMovement.MoveRover(input);
            Assert.IsTrue(resultMessage == "5 1 E");
        }

        [TestMethod]
        public void SuccessMethodThree()
        {
            MarsRoverUserInput input = new MarsRoverUserInput()
            {
                GridUpperRightCoords = "8 10",
                RoverPropMovement = new RoverPropertiesAndMovement()
                {
                    RoverPositionAndDirection = "2 5 W",
                    RoverMovement = "RMMRMMLM"
                }
            };
            string resultMessage = roverMovement.MoveRover(input);
            Assert.IsTrue(resultMessage == "4 8 N");
        }

        [TestMethod]
        public void TestGridValidation()
        {
            MarsRoverUserInput input = new MarsRoverUserInput()
            {
                GridUpperRightCoords = "8 ",
                RoverPropMovement = new RoverPropertiesAndMovement()
                {
                    RoverPositionAndDirection = "2 5 W",
                    RoverMovement = "RMMRMMLM"
                }
            };
            string resultMessage = roverMovement.MoveRover(input);
            Assert.IsTrue(resultMessage == ValidationMessages.InvalidGridCoords);
        }

        [TestMethod]
        public void TestRoverMovementValidation()
        {
            MarsRoverUserInput input = new MarsRoverUserInput()
            {
                GridUpperRightCoords = "8 10",
                RoverPropMovement = new RoverPropertiesAndMovement()
                {
                    RoverPositionAndDirection = "2 5 W",
                    RoverMovement = "testtest"
                }
            };
            string resultMessage = roverMovement.MoveRover(input);
            Assert.IsTrue(resultMessage == ValidationMessages.InvalidRoverMovement);
        }

        [TestMethod]
        public void TestPositionDirectionValidation()
        {
            MarsRoverUserInput input = new MarsRoverUserInput()
            {
                GridUpperRightCoords = "8 10",
                RoverPropMovement = new RoverPropertiesAndMovement()
                {
                    RoverPositionAndDirection = "2 5 ",
                    RoverMovement = "testtest"
                }
            };
            string resultMessage = roverMovement.MoveRover(input);
            string checkResultMessage = $"{ValidationMessages.InvalidRoverPosition} and/or {ValidationMessages.InvalidRoverDirection}";
            Assert.IsTrue(resultMessage == checkResultMessage);
        }

        [TestMethod]
        public void TestRoverDirectionValidation()
        {
            MarsRoverUserInput input = new MarsRoverUserInput()
            {
                GridUpperRightCoords = "8 10",
                RoverPropMovement = new RoverPropertiesAndMovement()
                {
                    RoverPositionAndDirection = "2 5 P",
                    RoverMovement = "testtest"
                }
            };
            string resultMessage = roverMovement.MoveRover(input);
            Assert.IsTrue(resultMessage == ValidationMessages.InvalidRoverDirection);
        }

        [TestMethod]
        public void TestRoverPositionValidation()
        {
            MarsRoverUserInput input = new MarsRoverUserInput()
            {
                GridUpperRightCoords = "8 10",
                RoverPropMovement = new RoverPropertiesAndMovement()
                {
                    RoverPositionAndDirection = "A B N",
                    RoverMovement = "testtest"
                }
            };
            string resultMessage = roverMovement.MoveRover(input);
            Assert.IsTrue(resultMessage == ValidationMessages.InvalidRoverPosition);
        }

        [TestMethod]
        public void TestPositionInsideGridValidation()
        {
            MarsRoverUserInput input = new MarsRoverUserInput()
            {
                GridUpperRightCoords = "8 10",
                RoverPropMovement = new RoverPropertiesAndMovement()
                {
                    RoverPositionAndDirection = "10 10 N",
                    RoverMovement = "RMMRMMLM"
                }
            };
            string resultMessage = roverMovement.MoveRover(input);
            string checkResultMessage = $"{ValidationMessages.InvalidRoverPosition}: Rover coordinates should not be negative or greater than upper x and y coordinates of the plateau";
            Assert.IsTrue(resultMessage == checkResultMessage);
        }

        [TestMethod]
        public void TestFinalPositionValidation()
        {
            MarsRoverUserInput input = new MarsRoverUserInput()
            {
                GridUpperRightCoords = "8 10",
                RoverPropMovement = new RoverPropertiesAndMovement()
                {
                    RoverPositionAndDirection = "5 5 N",
                    RoverMovement = "MMMMMMMMM"
                }
            };
            string resultMessage = roverMovement.MoveRover(input);
            string checkResultMessage = $"{RoverMovement.InvalidOperationString}: Rover can't go beyond plateau boundaries.";
            Assert.IsTrue(resultMessage == checkResultMessage);
        }
    }
}
