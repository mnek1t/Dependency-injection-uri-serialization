﻿using System;
using Conversion;
using Validation;
using Microsoft.Extensions.Logging;

namespace UriConversion
{
    /// <summary>
    /// The convertor class from string to Uri.
    /// </summary>
    public class UriConverter : IConverter<Uri?>
    {
        private readonly IValidator<string>? validator;

        /// <summary>
        /// Initializes a new instance of the <see cref="UriConverter"/> class.
        /// </summary>
        /// <param name="validator">The string validator.</param>
        /// <param name="logger">The logger.</param>
        /// <exception cref="ArgumentNullException">Throw if validator is null.</exception>
        public UriConverter(IValidator<string>? validator, ILogger<UriConverter>? logger = default)
        {
            this.validator = validator;
        }

        /// <summary>
        /// Converts the source string to Uri object.
        /// </summary>
        /// <param name="obj">The source string.</param>
        /// <returns> The Uri object if source string is valid and null otherwise.</returns>
        /// <exception cref="ArgumentNullException">Throw if source string is null.</exception>
        public Uri? Convert(string? obj)
        {
            if (obj == null || this.validator == null)
            {
                throw new ArgumentNullException(paramName: "Attempt to use a method with null");
            }

            if (this.validator.IsValid(obj)) 
            {
                return new Uri(obj);
            }

            return null;
        }
    }
}
