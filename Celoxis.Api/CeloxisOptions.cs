using Flurl.Http;

namespace Celoxis.Api
{
    public class CeloxisOptions
    {
        public string AccessToken { get; set; } = string.Empty;
        public string BaseUrl { get; set; } = "https://app.celoxis.com/psa";
        public IFlurlClient? FlurlClient { get; set; }
    }
}
