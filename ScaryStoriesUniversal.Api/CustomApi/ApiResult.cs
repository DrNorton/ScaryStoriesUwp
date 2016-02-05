namespace ScaryStoriesUniversal.Api.CustomApi
{
    public class ApiResult<T>
    {
        public string ErrorMessage { get; set; }
        public int ErrorCode { get; set; }
        public T Result { get; set; }
    }
}
