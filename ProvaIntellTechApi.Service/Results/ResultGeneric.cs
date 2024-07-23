namespace ProvaIntellTechApi.Service.Results
{
    public class Result<T> : Result
    {
        public T? Data { get; set; }
    }
}
