using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarsRover.Inputs;
using MarsRover.Data;

namespace MarsRover.Interfaces
{
    public interface IUserInputValidation
    {
        ValidationOperationResult ValidateUserInput(MarsRoverUserInput userinputs);
    }
}
