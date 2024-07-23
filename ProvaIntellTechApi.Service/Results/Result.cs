using ProvaIntellTechApi.Service.Results.Enums;

namespace ProvaIntellTechApi.Service.Results
{
    public class Result
    {
        public bool IsValid { get; set; } = true;
        public StatusCodeResultEnum StatusCode { get; set; }
        public string[]? Message { get; set; }
    }
}
