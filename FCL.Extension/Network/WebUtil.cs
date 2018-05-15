using FCL.Network.Exceptions;
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
        public static void SendRequest<IT>(string url, HttpMethod method, IT body, IDictionary<string, string> headers = null) where IT : class
        {
            InternalSendRequest<IT>(url, method, body, headers);
        }

        public static OT SendRequest<IT, OT>(string url, HttpMethod method, IT body, IDictionary<string, string> headers = null)
                where IT : class
                where OT : class
        {
            var response = InternalSendRequest<IT>(url, method, body, headers);
            var respStream = response.GetResponseStream();
            if (respStream == null)
            {
                throw new HttpResponseException(HttpStatusCode.NoContent);
            }

            var streamReader = new StreamReader(respStream);
            string respText = streamReader.ReadToEnd();

            return JsonConvert.DeserializeObject<OT>(respText);
        }

        private static HttpWebResponse InternalSendRequest<IT>(string url, HttpMethod method, IT body, IDictionary<string, string> headers) where IT : class
        {
            // Send the task to Task Scheduler.
            var taskRequest = WebRequest.CreateHttp(url);
            taskRequest.Method = method.ToString();
            taskRequest.ContentType = "application/json";
            taskRequest.Accept = "application/json";
            if (headers != null)
            {
                foreach (var kvp in headers)
                {
                    taskRequest.Headers.Add(kvp.Key, kvp.Value);
                }
            }

            if (body != null)
            {
                string json = JsonConvert.SerializeObject(body, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.Auto });
                Stream requestStream = taskRequest.GetRequestStream();
                using (var requestWriter = new StreamWriter(requestStream))
                {
                    requestWriter.Write(json);
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