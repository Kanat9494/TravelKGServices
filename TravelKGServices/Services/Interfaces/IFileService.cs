namespace TravelKGServices.Services.Interfaces;

public interface IFileService<TRequest> where TRequest : class
{
    Task UploadFilAsync(TRequest request);
}
