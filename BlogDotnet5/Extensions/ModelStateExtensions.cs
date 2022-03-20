using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BlogDotnet5.Extensions
{
    public static class ModelStateExtensions
    {
        public static List<string> GetErros(this ModelStateDictionary modelState)
        {
            var result = new List<string>();
            foreach (var item in modelState.Values) result.AddRange(item.Errors.Select(error => error.ErrorMessage));

            return result;
        }
    }
}