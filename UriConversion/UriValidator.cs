﻿using System;
using Validation;
using Microsoft.Extensions.Logging;

namespace UriConversion
{
    /// <summary>
    /// Uri string validator.
    /// </summary>
    public class UriValidator : IValidator<string>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UriValidator"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public UriValidator(ILogger<UriValidator>? logger = default)
        {
        }

        /// <summary>
        /// Determines if a string is valid Uri.
        /// </summary>
        /// <param name="obj">The source string.</param>
        /// <returns>true if the uri string is valid; otherwise, false.</returns>
        /// <exception cref="ArgumentNullException">Throw if source string is null.</exception>
        public bool IsValid(string? obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            Uri uri;
            return Uri.TryCreate(obj, UriKind.Absolute, out uri);
        }
    }
}
