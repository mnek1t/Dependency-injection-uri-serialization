using System;
using System.Collections.Generic;
using Serialization;
using System.IO;
using Microsoft.Extensions.Logging;

namespace XmlWriter.Serialization
{
    /// <summary>
    /// Presents the serialization functionality of the sequence<see cref="IEnumerable{Uri}"/>
    /// with using XmlWriter class.
    /// </summary>
    public class XmlWriterTechnology : IDataSerializer<Uri>
    {
        private readonly string PATH;
        private ILogger<XmlWriterTechnology> Logger;
        /// <summary>
        /// Initializes a new instance of the <see cref="XmlWriterTechnology"/> class.
        /// </summary>
        /// <param name="path">The path to json file.</param>
        /// <param name="logger">The logger.</param>
        /// <exception cref="ArgumentException">Throw if text reader is null or empty.</exception>
        public XmlWriterTechnology(string? path, ILogger<XmlWriterTechnology>? logger = default)
        {
            this.PATH = File.ReadAllText(path);
            this.Logger = logger;
        }

        /// <summary>
        /// Serializes the source sequence of Uri elements in json format. 
        /// </summary>
        /// <param name="source">The source sequence of Uri elements.</param>
        /// <exception cref="ArgumentNullException">Throw if the source sequence is null.</exception>
        public void Serialize(IEnumerable<Uri>? source)
        {
            System.Xml.XmlWriterSettings settings = new System.Xml.XmlWriterSettings();
            //using (System.Xml.XmlWriter writer = System.Xml.XmlWriter.Create()) 
            //{

            //}
        }
    }
}
