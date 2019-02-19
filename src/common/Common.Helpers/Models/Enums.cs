namespace Helpers.Models
{
    public class CommonEnums
    {

        public enum ResponseStatusCode
        {
            Success = 200,
            AlreadyExist = 210,
            InvalidInputs = 400,
            Unauthorized = 401,
            NotAuthenticated = 402,
            NotFound = 404,
            FoundMultiples = 405,
            ServerError = 500,
            BusinessError = 510,
            RelatedToOtherData = 515
        }

    }
}
