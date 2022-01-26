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

    public CFileInfo(string filePath)
    {
        fileInfo = new FileInfo(filePath);
        this.filePath = filePath;

        fileExtensionName = fileInfo.Extension.Split('.')[1];
        fileName = fileInfo.Name.TrimEnd(("." + fileExtensionName).ToCharArray());
    }
}
