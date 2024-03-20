using System.Text.Json;
using Microsoft.Extensions.Logging;
using Serialization;

namespace JsonSerializer.Serialization
{
    /// <summary>
    /// Presents the serialization functionality of the sequence<see cref="IEnumerable{Uri}"/>
    /// with using JsonSerialization class.
    /// </summary>
    public class JsonSerializerTechnology : IDataSerializer<Uri>
    {
        private readonly string? path;
        /// <summary>
        /// Initializes a new instance of the <see cref="JsonSerializerTechnology"/> class.
        /// </summary>
        /// <param name="path">The path to json file.</param>
        /// <param name="logger">The logger.</param>
        /// <exception cref="ArgumentException">Throw if text reader is null or empty.</exception>
        public JsonSerializerTechnology(string? path, ILogger<JsonSerializerTechnology>? logger = default)
        {
            this.path = path;
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
                throw new ArgumentNullException();
            }

            var uriInfos = new List<object>();
            foreach (var uri in source)
            {
                if (uri != null) 
                {
                    var pathSegments = new List<string>(uri.AbsolutePath.Split('/'));
                    pathSegments.RemoveAll(string.IsNullOrWhiteSpace);
                    var query = new List<object>();
                    if (!string.IsNullOrEmpty(uri.Query))
                    {
                        string queryKeyValues = uri.Query.TrimStart('?');
                        string[] pairs = queryKeyValues.Split('&');
                        foreach (string pair in pairs)
                        {
                            var keyValue = pair.Split('=');
                            if (pair.Split('=').Length == 2)
                            {
                                var obj = new { key = keyValue[0], value = keyValue[1] };
                                query.Add(obj);
                            }
                        }
                    }

                    var uriInfo = new
                    {
                        scheme = uri.Scheme,
                        host = uri.Host,
                        path = pathSegments,
                        query = query.Any() ? query : null,
                    };
                    uriInfos.Add(uriInfo);
                }
            }

            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                IgnoreNullValues = true,
            };
            using (FileStream stream = new FileStream(this.path, FileMode.OpenOrCreate, FileAccess.Write)) 
            {
                string jsonString = System.Text.Json.JsonSerializer.Serialize(uriInfos, options);
                using (StreamWriter writer = new StreamWriter(stream)) 
                {
                    writer.Write(jsonString);
                }
            }
        }
    }
}
