using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineQueuing.Helpers
{
    public class CustomException
    {
        public class MissingClaimsException : Exception
        {
        }

        public class MissingUsernameException : Exception
        {
        }

        public class MissingInformationException : Exception
        {
        }

        public class UserNotExistException : Exception
        {
        }

        public class TokenGenerationException : Exception
        {
        }

        public class MissingUserEmailException : Exception
        {
        }

        public class MissingFieldsException : Exception
        {
        }

        public class UserNotFoundException : Exception
        {
        }
    }
}
