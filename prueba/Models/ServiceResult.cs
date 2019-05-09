namespace prueba.Models
{
    /// <summary>
    /// Service result.
    /// </summary>
    public class ServiceResult
    {
        public bool Success { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public object Response { get; set; }
        public string TokenResponse { get; set; }
    }
}
