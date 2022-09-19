using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace SampleProject.API.Infrastructure.Extensions
{
    public static class ModelStateExtensions
    {
        /// <summary>
        /// Gets the model state errors if any
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public static List<string> GetErrorMessages(this ModelStateDictionary dictionary)
        {
            return dictionary.SelectMany(m => m.Value.Errors)
                             .Select(m => m.ErrorMessage)
                             .ToList();
        }
    }
}