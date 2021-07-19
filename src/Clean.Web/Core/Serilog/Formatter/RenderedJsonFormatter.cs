using Serilog.Formatting.Json;

namespace Clean.Web.Core.Serilog.Formatter
{
    /// <summary>
    /// RenderedJsonFormatter uses Serilog's JsonFormatter, with renderMessage set to true
    /// </summary>
    public class RenderedJsonFormatter : JsonFormatter
    {
        public RenderedJsonFormatter() : base(closingDelimiter: null, renderMessage: true, formatProvider: null)
        {
        }
    }
}
