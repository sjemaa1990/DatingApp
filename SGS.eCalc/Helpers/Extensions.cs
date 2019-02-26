using Microsoft.AspNetCore.Http;

namespace SGS.eCalc.Helpers
{
    public static class Extensions
    {
        public static void AddApplicationError(this HttpResponse response, string errorMessage){

            response.Headers.Add("Application-error", errorMessage);
            response.Headers.Add("Access-Control-Expose-Headers","Application-error");
            response.Headers.Add("Access-Control-Allow-Origin","*");
        }
    }
}