using System;
namespace MOTDetails.Exceptions
{
    public class ValidationException:Exception
    {
        enum ValidationRule
        {
            MustHavePassedTest
        }

        public ValidationException(ValidationRule failedRule)
        {
            switch (failedRule) {
                case ValidationRule.MustHavePassedTest
            }
        }
    }
}
