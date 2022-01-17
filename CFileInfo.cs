using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class CFileInfo
{
    public FileInfo fileInfo { get; private set; }

    public string filePath { get; private set; }

    public string fileName { get; private set; }
    public string fileExtensionName { get; private set; }
    public FileExtentionType fileExtension { get; private set; }

    public CFileInfo(string filePath)
    {
        fileInfo = new FileInfo(filePath);
        this.filePath = filePath;

        fileExtensionName = fileInfo.Extension.Split('.')[1];
        fileName = fileInfo.Name.TrimEnd(("." + fileExtensionName).ToCharArray());
        fileExtension = ToFileExtentionEnum(fileExtensionName);
    }

    public static bool IsVaildExtention(string extentionName) => Enum.IsDefined(typeof(FileExtentionType), extentionName.ToLower());

    public static FileExtentionType ToFileExtentionEnum(string extentionName)
    {
        if (!IsVaildExtention(extentionName))
            return FileExtentionType.notSupported;

        return (FileExtentionType)Enum.Parse(typeof(FileExtentionType), extentionName);
    }

    public enum FileExtentionType
    {
        notSupported = 0,
        png = 1,
        jpg = 1,
        jpeg = 1,
        mp4 = 2,
        webm = 2,
        gif = 3
    }
}
