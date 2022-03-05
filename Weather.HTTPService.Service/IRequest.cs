namespace Weather.HTTPService.Service
{
    public interface IRequest
    {
        Task<DTO.HttpResponseValues> ExecuteRequestAsync(string request, string method);
    }
}