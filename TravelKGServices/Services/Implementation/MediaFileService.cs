namespace TravelKGServices.Services.Implementation;

public class MediaFileService : IFileService<MediaFile>
{
    public async Task UploadFilAsync(MediaFile request)
    {
        try
        {
            //string directoryPath = Path.Combine(MedLinkConstants.FILE_BASE_PATH, request.FilePath);
            string directoryPath = $"{TravelKGConstants.FILE_BASE_PATH}\\{request.FilePath}";

            var fileFullPath = directoryPath + "\\" + request.FileName;
            await File.WriteAllBytesAsync(fileFullPath, request.FileBytes);
        }
        catch (Exception ex)
        {

        }
    }
}
