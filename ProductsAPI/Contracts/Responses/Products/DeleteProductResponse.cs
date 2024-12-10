namespace ProductsAPI.Contracts.Responses.Products
{
    public class DeleteProductResponse
    {
        public bool Success {  get; set; }
        public string? Error { get; set; }

        public DeleteProductResponse(bool success, string? error)
        {
            Success = success;
            Error = error;
        }
    }
}
