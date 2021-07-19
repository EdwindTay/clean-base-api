namespace Clean.Web.Configuration
{
    public class ExceptionHandlingConfiguration
    {
        /// <summary>
        // True => returns stack trace in response (for development)
        // False => does not return stack trace in response (for production)
        /// </summary>
        public bool ShowStackTrace { get; set; }
    }
}
