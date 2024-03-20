using System;
using System.Collections.Generic;
using Serialization;
using Microsoft.Extensions.Logging;
using System.Xml;

namespace XDomWriter.Serialization
{
    /// <summary>
    /// Presents the serialization functionality of the sequence<see cref="IEnumerable{Uri}"/>
    /// with using X-DOM model.
    /// </summary>
    public class XDomTechnology : IDataSerializer<Uri>
    {
        private readonly string PATH;
        //private const string OUTPUT_PATH = "D:\\EPAM\\Practical Assigments\\Dependecy Injection\\ConsoleClient\\bin\\Debug\\net6.0\\url-address.xml";
        /// <summary>
        /// Initializes a new instance of the <see cref="XDomTechnology"/> class.
        /// </summary>
        /// <param name="path">The path to json file.</param>
        /// <param name="logger">The logger.</param>
        /// <exception cref="ArgumentException">Throw if text reader is null or empty.</exception>
        public XDomTechnology(string? path, ILogger<XDomTechnology>? logger = default)
        {
            this.PATH = path;
        }

        /// <summary>
        /// Serializes the source sequence of Uri elements in json format. 
        /// </summary>
        /// <param name="source">The source sequence of Uri elements.</param>
        /// <exception cref="ArgumentNullException">Throw if the source sequence is null.</exception>
        public void Serialize(IEnumerable<Uri>? source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            if (string.IsNullOrEmpty(PATH))
            {
                throw new ArgumentException(message: "Path cannot be null or empty", nameof(Path));
            }
            var xmlSettings = new XmlWriterSettings() { Indent = true , IndentChars = "    "};
            using (XmlWriter writer = XmlWriter.Create(PATH, xmlSettings))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("uriAdresses");

                foreach (var uri in source)
                {
                    if (uri != null)
                    {
                        writer.WriteStartElement("uriAdress");
                        writer.WriteStartElement("scheme");
                        writer.WriteAttributeString("name", uri.Scheme);
                        writer.WriteEndElement();
                        writer.WriteStartElement("host");
                        writer.WriteAttributeString("name", uri.Host);
                        writer.WriteEndElement();

                        string[] segments = uri.AbsolutePath.Split('/');
                        writer.WriteStartElement("path");
                        foreach (string segment in segments)
                        {
                            if (!string.IsNullOrEmpty(segment))
                            {
                                writer.WriteElementString("segment", segment);
                            }
                        }
                        writer.WriteEndElement();
                        if (!string.IsNullOrEmpty(uri.Query))
                        {
                            writer.WriteStartElement("query");
                            string query = uri.Query.TrimStart('?');
                            string[] pairs = query.Split('&');
                            foreach (string pair in pairs)
                            {
                                string[] keyValue = pair.Split('=');
                                if (keyValue.Length == 2)
                                {
                                    writer.WriteStartElement("parameter");
                                    writer.WriteAttributeString("key", keyValue[0]);
                                    writer.WriteAttributeString("value", keyValue[1]);
                                    writer.WriteEndElement();
                                }
                            }
                            writer.WriteEndElement();
                        }
                        writer.WriteEndElement();
                    }
                }

                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
        }
    }
}
