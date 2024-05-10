﻿namespace infoManagerAPI.Exceptions
{
    public class HttpException : Exception
    {
        public int StatusCode { get; }

        public HttpException(int statusCode, string message)
            : base(message)
        {
            StatusCode = statusCode;
        }

        public HttpException(string? message) : base(message)
        {
        }
    }
}
