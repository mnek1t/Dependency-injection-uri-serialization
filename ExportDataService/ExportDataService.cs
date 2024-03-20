using System;
using System.Collections.Generic;
using DataReceiving;
using Conversion;
using Serialization;
using System.Text.Json.Serialization;
using XDomWriter.Serialization;

namespace ExportDataService
{
    /// <summary>
    /// Presents the export data of string to type T service.
    /// </summary>
    /// <typeparam name="T">The type data for export.</typeparam>
    public class ExportDataService<T>
    {
        private IDataReceiver receiver;
        private IDataSerializer<T> serializer;
        private IConverter<T> converter;
        /// <summary>
        /// Initializes a new instance of the <see cref="ExportDataService{T}"/> class.
        /// </summary>
        /// <param name="receiver">The data receiver.</param>
        /// <param name="serializer">The data serializer.</param>
        /// <param name="converter">The data convertor.</param>
        /// <exception cref="ArgumentNullException">Trow if receiver, writer or converter is null.</exception>
        public ExportDataService(IDataReceiver receiver, IDataSerializer<T> serializer, IConverter<T> converter)
        {
            this.receiver = receiver;
            this.serializer = serializer;
            this.converter = converter;
        }

        /// <summary>
        /// Performs the process of receiving the sequence of string <see cref="IEnumerable{string}"/>
        /// obtained from <see cref="IDataReceiver"/>, than transforming it into the sequence <see cref="IEnumerable{T}"/>,
        /// and then serializing it with <see cref="IDataSerializer{T}"/>.
        /// </summary>
        public void Run()
        {
            IEnumerable<string> data =  receiver.Receive();
            var uris = new List<T>();
            foreach (string dataItem in data) 
            {
                uris.Add(this.converter.Convert(dataItem));
            }
            serializer.Serialize(uris);
        }
    }
}
