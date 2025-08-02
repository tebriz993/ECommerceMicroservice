using System.Collections.Generic;

namespace Product.Application.Wrappers.Base
{
    /// <summary>
    /// Represents a standardized API response.
    /// </summary>
    /// <typeparam name="T">The type of the data being returned.</typeparam>
    public class Response<T>
    {
        /// <summary>
        /// Indicates whether the request was successful.
        /// </summary>
        public bool Succeeded { get; set; }

        /// <summary>
        /// A message providing details about the response.
        /// </summary>
        public string? Message { get; set; }

        /// <summary>
        /// A list of errors that occurred during the request. Null if the request was successful.
        /// </summary>
        public List<string>? Errors { get; set; }

        /// <summary>
        /// The data payload of the response.
        /// </summary>
        public T? Data { get; set; }

        public Response() { }

        /// <summary>
        /// Creates a successful response.
        /// </summary>
        /// <param name="data">The data payload.</param>
        /// <param name="message">An optional success message.</param>
        public Response(T data, string? message = null)
        {
            Succeeded = true;
            Message = message;
            Data = data;
        }

        /// <summary>
        /// Creates a failed response.
        /// </summary>
        /// <param name="message">The failure message.</param>
        public Response(string message)
        {
            Succeeded = false;
            Message = message;
        }
    }
}