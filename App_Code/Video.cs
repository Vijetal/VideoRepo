using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Video
/// </summary>
public class Video
{
    int _PKVideoID;
    string _VideoName;
    byte[] _Image;
    DateTime _DateUploaded;
    string _UserName;
    string _FileSize;

    public int PKVideoID
    {
        get { return _PKVideoID; }
        set { _PKVideoID = value; }
    }

    public string VideoName
    {
        get { return _VideoName; }
        set { _VideoName = value; }
    }

    public byte[] Image
    {
        get { return _Image; }
        set { _Image = value; }
    }
    public DateTime DateUploaded
    {
        get { return _DateUploaded; }
        set { _DateUploaded = value; }
    }

    public string UserName
    {
        get { return _UserName; }
        set { _UserName = value; }
    }

    public string FileSize
    {
        get { return _FileSize; }
        set { _FileSize = value; }
    }
}