using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public static class FileLoader
{





    public static Texture2D LoadImageFromDisk(string filePath)
    {
        Texture2D img = null;
        byte[] imgData;


        if (File.Exists(filePath))
        {
            imgData = File.ReadAllBytes(filePath);
            img = new Texture2D(2, 2);
            if (!img.LoadImage(imgData))
                return null;
        }
        return img;
    }

}
