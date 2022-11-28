using System;
using System.Collections.Generic;

[Serializable]
public class jsonImgTxtClass
{
    public List<imgTxtList> photos;

}

[Serializable]
public class imgTxtList
{
    public int albumId;
    public int id;
    public string title;
    public string url;
    public string thumbnailUrl;

}

