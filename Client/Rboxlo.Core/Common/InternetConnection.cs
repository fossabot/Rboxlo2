using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json.Linq;

namespace Rboxlo.Core.Common
{
    /// <summary>
    /// Response from a InternetConnection method
    /// </summary>
    public struct InternetResponse
    {
        public int StatusCode;
        public string Data;

        public InternetResponse(int statusCode, string data)
        {
            StatusCode = statusCode;
            Data = data;
        }
    }

    /// <summary>
    /// All things internet
    /// </summary>
    public class InternetConnection
    {
        public InternetConnection()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        }

        /// <summary>
        /// Performs a HTTP request
        /// </summary>
        /// <param name="method">HTTP method (GET, POST, etc.)</param>
        /// <param name="url">URL to fetch</param>
        /// <param name="headers">Requiest headers</param>
        /// <param name="jar">Cookie jar</param>
        /// <param name="body">Request body, where Item1 is if body type is form, and Item2 is the body (Dictionary<string, string> if key=value, or string if not)</param>
        /// <param name="ua">optional user agent</param>
        /// <returns>Returned data from website where the Item1 is the response code, and Item2 is the data itself</returns>
        public InternetResponse Request(string method, string url, Dictionary<string, string> headers = null, Dictionary<string, string> jar = null, Tuple<bool, object> body = null, string ua = null)
        {
            WebRequest request = WebRequest.Create(url);
            request.Method = method;

            if (ua != null)
            {
                ((HttpWebRequest)request).UserAgent = ua;
            }

            if (jar != null)
            {
                StringBuilder cookies = new StringBuilder();
                string format = jar.Count > 1 ? "{0}={1}; " : "{0}={1}";

                foreach (KeyValuePair<string, string> cookie in jar)
                {
                    cookies.Append(String.Format(format, cookie.Value, cookie.Key));
                }

                if (jar.Count > 1)
                {
                    // since we have >1 cookies, our final jar probably looks like this:
                    // "x=1; y=2; "
                    // we want to remove the final "; " at the end, so:

                    cookies.Length--; // remove final space " "
                    cookies.Length--; // remove trailing semicolon ";"
                }

                request.Headers["Cookie"] = cookies.ToString();
            }

            if (headers != null)
            {
                foreach (KeyValuePair<string, string> header in headers)
                {
                    request.Headers[header.Key] = header.Value;
                }
            }

            if (body != null)
            {
                bool isForm = body.Item1;
                ASCIIEncoding encoding = new ASCIIEncoding();
                string postData;

                if (isForm)
                {
                    Dictionary<string, string> form = (Dictionary<string, string>)body.Item2;

                    // since key=value, set application type as form
                    request.ContentType = "application/x-www-form-urlencoded";

                    // start constructing the body
                    StringBuilder rawBody = new StringBuilder();
                    string format = form.Count > 1 ? "{0}={1}&" : "{0}={1}";

                    foreach (KeyValuePair<string, string> input in form)
                    {
                        rawBody.Append(String.Format(format, input.Key, input.Value));
                    }

                    if (form.Count > 1)
                    {
                        // since we have more than one input, body probably looks like this
                        // "x=1&y=2&"
                        // we want to get rid of the "&" at the end, so:

                        rawBody.Length--; // remove final ampersand
                    }

                    postData = rawBody.ToString();
                }
                else
                {
                    string form = (string)body.Item2;

                    // since unknown set as octet
                    request.ContentType = "application/octet-stream";

                    // not much to do
                    postData = form;
                }

                byte[] rawPostData = encoding.GetBytes(postData);
                request.ContentLength = rawPostData.Length;

                Stream bodyStream = request.GetRequestStream();
                bodyStream.Write(rawPostData, 0, rawPostData.Length);
            }

            HttpWebResponse response = (HttpWebResponse)request.GetResponse(); // not stand-alone WebResponse because that class has no methods

            int status = (int)response.StatusCode;
            string data = null;

            using (Stream stream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(stream);

                data = reader.ReadToEnd();

                reader.Close();
            } // disposed

            response.Close();

            return new InternetResponse(status, data);
        }

        /// <summary>
        /// Performs a HTTP GET request
        /// </summary>
        /// <param name="url">URL to fetch</param>
        /// <param name="headers">Additional headers</param>
        /// <param name="jar">Cookie jar</param>
        /// <param name="userAgent">User agent</param>
        /// <returns>Returned data from website where the Item1 is the response code, and Item2 is the data itself</returns>
        public InternetResponse Get(string url, Dictionary<string, string> headers = null, Dictionary<string, string> jar = null, string userAgent = null)
        {
            return Request("GET", url, headers, jar, ua: userAgent);
        }

        /// <summary>
        /// Performs a HTTP POST request
        /// </summary>
        /// <param name="url">URL to fetch</param>
        /// <param name="isForm">Is body a form</param>
        /// <param name="body">Request body</param>
        /// <param name="headers">Additional headers</param>
        /// <param name="jar">Cookie jar</param>
        /// <returns>Returned data from website where the Item1 is the response code, and Item2 is the data itself</returns>
        public InternetResponse Post(string url, bool isForm, object body, string userAgent = null, Dictionary<string, string> headers = null, Dictionary<string, string> jar = null)
        {
            return Request("POST", url, headers, jar, Tuple.Create(isForm, body), ua: userAgent);
        }

        /// <summary>
        /// Can we connect to the internet, and is Rboxlo up?
        /// </summary>
        /// <returns>Whether everything internet-related is okay</returns>
        public bool OK()
        {
            JObject body;
            int status;

            try
            {
                InternetResponse response = Get(String.Format("{0}/api/launcher/ok", Constants.BaseURL));
                status = response.StatusCode;
                body = JObject.Parse(response.Data);
            }
            catch
            {
                return false;
            }

            return (body["success"].ToObject<bool>() == true) && (status == 200);
        }
    }
}
