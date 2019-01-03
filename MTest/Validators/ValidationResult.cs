using System.Collections.Generic;

namespace MTest.Validators
{
    //Assumption: We want some flexibilities around returning validation result, 
    //so I decided not to use the MVC out-of-box validation attributes 
    public class ValidationResult
    {
        public bool IsValid { get; set; }
        public string ErrorMessage { get; set; }   
    }
  
}
