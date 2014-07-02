using System;

namespace LordCommander.Client
{
    public class WebCommunicationException : Exception
    {
        public WebError WebError { get; private set; }

        public WebCommunicationException(WebError webError)
        {
            WebError = webError;
        }
    }
}