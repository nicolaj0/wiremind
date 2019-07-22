using System.Net;
using System.Net.Http;
using System.Text;
using ConsoleApplication2.Scraping;
using HtmlAgilityPack;

namespace ConsoleApplication2.Browser
{
    public class BrowserSession
    {
        private bool _isPost;
        private HtmlDocument _htmlDoc;
        private bool _capture;
        private HttpClient _client;
        private Headers _headers = new Headers();

        /// <summary>
        /// System.Net.CookieCollection. Provides a collection container for instances of Cookie class 
        /// </summary>
        public CookieCollection Cookies { get; set; }

        /// <summary>
        /// Provide a key-value-pair collection of form elements 
        /// </summary>
        public FormElementCollection FormElements { get; set; }

        /// <summary>
        /// Makes a HTTP GET request to the given URL
        /// </summary>
        public string Get(string url)
        {
            _isPost = false;
            _capture = true;

            CreateWebRequestObject().Load(url);
            return _htmlDoc.DocumentNode.InnerHtml;
        }

        /// <summary>
        /// Makes a HTTP POST request to the given URL
        /// </summary>
        public string Post(string url)
        {
            _capture = false;
            _isPost = true;
            CreateWebRequestObject().Load(url, "POST");
            return _htmlDoc.DocumentNode.InnerHtml;
        }

        
        /// <summary>
        /// Creates the HtmlWeb object and initializes all event handlers. 
        /// </summary>
        private HtmlWeb CreateWebRequestObject()
        {
            HtmlWeb web = new HtmlWeb();
            web.UseCookies = true;
            web.CaptureRedirect = _capture;
            web.PreRequest = new HtmlWeb.PreRequestHandler(OnPreRequest);
            web.PostResponse = new HtmlWeb.PostResponseHandler(OnAfterResponse);
            web.PreHandleDocument = new HtmlWeb.PreHandleDocumentHandler(OnPreHandleDocument);
            return web;
        }

        /// <summary>
        /// Event handler for HtmlWeb.PreRequestHandler. Occurs before an HTTP request is executed.
        /// </summary>
        protected bool OnPreRequest(HttpWebRequest request)
        {
            // request.Headers["Accept-Encoding"] = "gzip, deflate, br";
            request.Accept = _headers["Accept"];
            request.UserAgent =_headers["UserAgent"];
            request.Headers["Accept-Language"] =_headers["Accept-Language"];
            AddCookiesTo(request); // Add cookies that were saved from previous requests
             if (_isPost)
            {
                request.ContentType = "application/x-www-form-urlencoded";
            }

            if (_isPost) AddPostDataTo(request); // We only need to add post data on a POST request
            return true;
        }

        /// <summary>
        /// Event handler for HtmlWeb.PostResponseHandler. Occurs after a HTTP response is received
        /// </summary>
        protected void OnAfterResponse(HttpWebRequest request, HttpWebResponse response)
        {
            SaveCookiesFrom(response); // Save cookies for subsequent requests
        }

        /// <summary>
        /// Event handler for HtmlWeb.PreHandleDocumentHandler. Occurs before a HTML document is handled
        /// </summary>
        protected void OnPreHandleDocument(HtmlDocument document)
        {
            SaveHtmlDocument(document);
        }

        /// <summary>
        /// Assembles the Post data and attaches to the request object
        /// </summary>
        private void AddPostDataTo(HttpWebRequest request)
        {
            string payload = FormElements.AssemblePostPayload();
            byte[] buff = Encoding.UTF8.GetBytes(payload.ToCharArray());
            request.ContentLength = buff.Length;

            System.IO.Stream reqStream = request.GetRequestStream();
            reqStream.Write(buff, 0, buff.Length);
        }

        /// <summary>
        /// Add cookies to the request object
        /// </summary>
        private void AddCookiesTo(HttpWebRequest request)
        {
            if (Cookies != null && Cookies.Count > 0)
            {
                request.CookieContainer.Add(Cookies);
            }
        }

        /// <summary>
        /// Saves cookies from the response object to the local CookieCollection object
        /// </summary>
        private void SaveCookiesFrom(HttpWebResponse response)
        {
            if (response.Cookies.Count > 0)
            {
                if (Cookies == null) Cookies = new CookieCollection();
                Cookies.Add(response.Cookies);
            }
        }

        /// <summary>
        /// Saves the form elements collection by parsing the HTML document
        /// </summary>
        private void SaveHtmlDocument(HtmlDocument document)
        {
            _htmlDoc = document;
            FormElements = new FormElementCollection(_htmlDoc);
        }
        
        
        public HttpClient   InitHttpHandler()
        {
            var handler = new HttpClientHandler {CookieContainer = new CookieContainer()};
            handler.CookieContainer.Add(Cookies);
            _client = new HttpClient(handler);
            handler.UseCookies = true;
            // client.DefaultRequestHeaders.Accept = ""
            _client.DefaultRequestHeaders.Add("Accept", "*/*");
            _client.DefaultRequestHeaders.Add("X-Requested-With", "XMLHttpRequest");
            _client.DefaultRequestHeaders.UserAgent.ParseAdd(_headers["UserAgent"]);
            _client.DefaultRequestHeaders.Add("Accept-Language", _headers["Accept-Language"]);
            return _client;
        }
    }
}