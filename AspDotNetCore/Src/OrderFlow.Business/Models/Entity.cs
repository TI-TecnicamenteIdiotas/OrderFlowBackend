using System;
using System.Collections.Generic;
using System.Linq;

namespace OrderFlow.Business.Models
{
    public abstract class Entity
    {
        public int Id { get; set; }

        /// <summary>
        /// Checks if entity has any property with a null value
        /// </summary>
        /// <param name="exceptPropNames">Properties to exclude from list (allow null)</param>
        /// <returns>The values that are null</returns>
        public string[] GetNullValues(params string[] exceptPropNames)
        {
            if (exceptPropNames == null) exceptPropNames = Array.Empty<string>();
            var props = GetType().GetProperties().Where(p => !exceptPropNames.Contains(p.Name));
            List<string> nullProps = new List<string>();
            foreach (var prop in props)
            {
                if (prop.GetValue(this) == null)
                    nullProps.Add(prop.Name);
            }
            return nullProps.ToArray();
        }
    }
}