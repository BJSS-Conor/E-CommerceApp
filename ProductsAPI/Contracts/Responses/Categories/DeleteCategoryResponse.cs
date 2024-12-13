namespace ProductsAPI.Contracts.Responses.Categories
{
    public class DeleteCategoryResponse
    {
        public bool Success { get; set; }

        public string? Error { get; set; }

        public DeleteCategoryResponse(bool success, string? error)
        {
            Success = success;
            Error = error;
        }
    }
}
