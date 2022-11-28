using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public class page1Controller : MonoBehaviour
{

    private GameObject imageTextBlock;

    private Image[] imgList;
    private TextMeshProUGUI[] textList;

    private int imgNum;
    
    void Start() //Sets the image and text blocks into position
    {
        imgNum = 1;

        Image[] ilist = GameObject.FindObjectsOfType<Image>();
        imgList = ilist;

        TextMeshProUGUI[] tlist = GameObject.FindObjectsOfType<TextMeshProUGUI>();
        textList = tlist;

        int positionY = -2000;
        for(int i = 1; i < 11; i++)
        {
            imageTextBlock = GameObject.Find("ImageAndTextBlock" + i);
            imageTextBlock.transform.position = new Vector3(imageTextBlock.transform.position.x, positionY, imageTextBlock.transform.position.z);
            positionY = positionY + 750;

            StartCoroutine(getSetImageAndTextData("https://jsonplaceholder.typicode.com/photos/" + imgNum, i));
            imgNum++;
        }

        positionY = -2375;
        for (int i = 1; i < 11; i++)
        {
            imageTextBlock = GameObject.Find("ImageAndTextBlockInv" + i);
            imageTextBlock.transform.position = new Vector3(imageTextBlock.transform.position.x, positionY, imageTextBlock.transform.position.z);
            positionY = positionY + 750;

            StartCoroutine(getSetImageAndTextData("https://jsonplaceholder.typicode.com/photos/" + imgNum, i + 10));
            imgNum++;
        }
        

    }

    
    void Update()
    {

        for (int i = 1; i < 11; i++)
        {
            boundaryCheck(GameObject.Find("ImageAndTextBlock" + i), i);
            boundaryCheck(GameObject.Find("ImageAndTextBlockInv" + i), i + 10);

            
            if (GameObject.Find("ImageAndTextBlock" + i).transform.position.y > 0 && GameObject.Find("ImageAndTextBlock" + i).transform.position.y < 3000)
            {
                posCheck(GameObject.Find("ImageAndTextBlock" + i), GameObject.Find("ImageAndTextBlockInv" + i));

                int below = i - 1;
                if ((i - 1) == 0) { below = 10; }
                posCheck(GameObject.Find("ImageAndTextBlockInv" + i), GameObject.Find("ImageAndTextBlock" + below));
            }

        }

    }

    private void FixedUpdate()
    {
        
    }

    private void posCheck(GameObject obj, GameObject belowBlock) //Checks that the image and text blocks are correctly spaced and if not corrects it
    {
        if ((obj.transform.position.y - belowBlock.transform.position.y) != 375)
        {
            obj.transform.position = new Vector3(obj.transform.position.x, belowBlock.transform.position.y + 375, obj.transform.position.z);
        }
    }

    private void boundaryCheck(GameObject obj, int i) //Checks if an image and text block have gone too far from the screen and sends them to the bottom or top, also gives moved blocks new image and text
    {

        if (obj.transform.position.y > 5125)
        {
            obj.transform.position = new Vector3(obj.transform.position.x, -2375f, obj.transform.position.z);
            StartCoroutine(getSetImageAndTextData("https://jsonplaceholder.typicode.com/photos/" + imgNum, i));
            imgNum++;
        }
        else if (obj.transform.position.y < -2750)
        {
            obj.transform.position = new Vector3(obj.transform.position.x, 4750f, obj.transform.position.z);
            StartCoroutine(getSetImageAndTextData("https://jsonplaceholder.typicode.com/photos/" + imgNum, i));
            imgNum++;
        }

    }

    IEnumerator getSetImageAndTextData(string url, int i) // Sets ImgTxt class variables using json url
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            yield return webRequest.SendWebRequest();
            
            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(string.Format("Something went wrong!", webRequest.error));
                    break;
                case UnityWebRequest.Result.Success:
                    ImgTxt imgTxt = JsonConvert.DeserializeObject<ImgTxt>(webRequest.downloadHandler.text);

                    StartCoroutine(setImgAndText(imgTxt, i));

                    break;
            }


        }
    }

    public class ImgTxt
    {
        public int albumId { get; set; }
        public int id { get; set; }
        public string title { get; set; }
        public string url { get; set; }
        public string thumbnailUrl { get; set; }
    }

    IEnumerator setImgAndText(ImgTxt imgTxt, int i) // Gives image and text to block using json
    {

        using (UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture(imgTxt.url + ".png"))
        {
            yield return webRequest.SendWebRequest();
            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(string.Format("Something went wrong!", webRequest.error));
                    break;
                case UnityWebRequest.Result.Success:
                    
                    string name = "Image" + i;
                    Image img = imgList[0];

                    for (int y = 0; y < imgList.Length; y++)
                    {
                        if(imgList[y].gameObject.name == name)
                        {
                            img = imgList[y];
                        }
                    }

                    Texture2D tex = ((DownloadHandlerTexture)webRequest.downloadHandler).texture;
                    img.sprite = Sprite.Create(tex, new Rect(0,0, tex.width, tex.height), new Vector2(0,0));


                    name = "Text (TMP)" + i;
                    TextMeshProUGUI text = textList[0];

                    for (int y = 0; y < textList.Length; y++)
                    {
                        if (textList[y].gameObject.name == name)
                        {
                            text = textList[y];
                        }
                    }

                    text.text = imgTxt.title;

                    break;
            }
            
        }
    }
}
