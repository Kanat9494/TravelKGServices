﻿namespace TravelKGServices.Models;

public class MediaFile
{
    public int FileId { get; set; }
    public string FileName { get; set; }
    public byte[] FileBytes { get; set; }
    public string FilePath { get; set; }
}
