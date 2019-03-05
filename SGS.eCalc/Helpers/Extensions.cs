using System;
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

        public static int CalculateAge(this DateTime theDateTime){

            var age = DateTime.Now.Year - theDateTime.Year;
            if(theDateTime.AddYears(age) > DateTime.Today){
                return age -- ;
            }
            return age;
        }
    }
}