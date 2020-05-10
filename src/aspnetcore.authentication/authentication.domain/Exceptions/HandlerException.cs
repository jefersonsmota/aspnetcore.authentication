using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace authentication.domain.Exceptions
{
    /// <summary>
    /// Custom Handlers Exceptions para a aplicação
    /// </summary>
    [Serializable]
    public abstract class HandlerException : Exception
    {
        public int HttpStatusCode { get; private set; }
        private readonly string _resourceName;
        private readonly IList<string> _validationErrors;

        protected HandlerException(int httpStatusCode) { this.HttpStatusCode = httpStatusCode; }

        protected HandlerException(string message, int httpStatusCode) : base(message) 
        {
            this.HttpStatusCode = httpStatusCode;
        }

        protected HandlerException(string message, Exception innerException, int httpStatusCode) : base(message, innerException) 
        {
            this.HttpStatusCode = httpStatusCode;
        }

        protected HandlerException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            _resourceName = info.GetString("ResourceName");
            _validationErrors = (IList<string>)info.GetValue("ValidationErrors", typeof(IList<string>));
        }
    }

#pragma warning disable S3925 // "ISerializable" should be implemented correctly
    public class ValidationException : HandlerException
#pragma warning restore S3925 // "ISerializable" should be implemented correctly
    {
        public ValidationException(string message = "", Exception innerException = null) : base(message, innerException, 400)
        {

        }
    }

#pragma warning disable S3925 // "ISerializable" should be implemented correctly
    public class NotFoundException : HandlerException
#pragma warning restore S3925 // "ISerializable" should be implemented correctly
    {
        public NotFoundException(string message = "", Exception innerException = null) : base(message, innerException, 404)
        {

        }
    }
}
