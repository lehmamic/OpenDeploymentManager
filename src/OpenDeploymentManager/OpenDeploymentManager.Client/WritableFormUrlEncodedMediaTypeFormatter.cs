using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;

namespace OpenDeploymentManager.Client
{
    public class WritableFormUrlEncodedMediaTypeFormatter : FormUrlEncodedMediaTypeFormatter
    {
        public override bool CanWriteType(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            return true;
        }

        public override async Task WriteToStreamAsync(Type type, object value, Stream writeStream, HttpContent content, TransportContext transportContext)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            if (writeStream == null)
            {
                throw new ArgumentNullException("writeStream");
            }

            if (content == null)
            {
                throw new ArgumentNullException("content");
            }

            using (var writer = new StreamWriter(writeStream, Encoding.UTF8, this.MaxDepth, true))
            {
                string[] serializedPropertyValues = type.GetProperties().Select(p => new { Name = p.Name, Value = p.GetValue(value)})
                                                        .Where(p => p.Value != null)
                                                        .Select(p => string.Format(CultureInfo.InvariantCulture, "{0}={1}", p.Name, p.Value))
                                                        .ToArray();

                string serializedObject = string.Join("&", serializedPropertyValues);
                await writer.WriteAsync(serializedObject);
            }
        }
    }
}