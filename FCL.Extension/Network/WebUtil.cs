using FCL.Network.Exceptions;
using FCL.Network.Formatters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FCL.Network
{
    public enum HttpMethod
    {
        GET,
        POST,
        PUT,
        DELETE
    }

    public class WebUtil
    {
        public static void SendRequest<IT>(string url, HttpMethod method, IT body, IDictionary<string, string> headers = null, ContentType requestBodyType = ContentType.Json, ContentType responseBodyType = ContentType.Json) where IT : class
        {
            var requestFormatter = Formatter.GetFormatter(requestBodyType);
            InternalSendRequest(url, method, requestFormatter.Serialize(body), requestBodyType.ToMimeTypeString(), responseBodyType.ToMimeTypeString(), headers);
        }

        public static OT SendRequest<IT, OT>(string url, HttpMethod method, IT body, IDictionary<string, string> headers = null, ContentType requestBodyType = ContentType.Json, ContentType responseBodyType = ContentType.Json)
                where IT : class
                where OT : class
        {
            var requestFormatter = Formatter.GetFormatter(requestBodyType);
            var response = InternalSendRequest(url, method, requestFormatter.Serialize<IT>(body), requestBodyType.ToMimeTypeString(), responseBodyType.ToMimeTypeString(), headers);
            var respStream = response.GetResponseStream();
            if (respStream == null)
            {
                throw new HttpResponseException(HttpStatusCode.NoContent);
            }

            var streamReader = new StreamReader(respStream);
            string respText = streamReader.ReadToEnd();

            var responseFormatter = Formatter.GetFormatter(responseBodyType);
            return responseFormatter.Deserialize<OT>(respText);
        }

        private static HttpWebResponse InternalSendRequest(string url, HttpMethod method, string body, string requestBodyType, string responseBodyType, IDictionary<string, string> headers)
        {
            // Send the task to Task Scheduler.
            var taskRequest = WebRequest.CreateHttp(url);
            taskRequest.Method = method.ToString();
            taskRequest.ContentType = requestBodyType;
            taskRequest.Accept = responseBodyType;
            if (headers != null)
            {
                foreach (var kvp in headers)
                {
                    taskRequest.Headers.Add(kvp.Key, kvp.Value);
                }
            }

            if (body != null)
            {
                //string json = JsonConvert.SerializeObject(body, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.Auto });
                Stream requestStream = taskRequest.GetRequestStream();
                using (var requestWriter = new StreamWriter(requestStream))
                {
                    requestWriter.Write(body);
                    requestWriter.Flush();
                }
            }

            var response = taskRequest.GetResponse() as HttpWebResponse;

            if ((int)response.StatusCode >= 300)
            {
                throw new HttpResponseException(response.StatusCode);
            }

            return response;
        }
    }
}