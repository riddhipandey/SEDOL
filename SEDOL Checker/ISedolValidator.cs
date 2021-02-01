using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDOL_Checker
{
    public interface ISedolValidator
    {
        ISedolValidationResult ValidateSedol(string input);
    }
}
