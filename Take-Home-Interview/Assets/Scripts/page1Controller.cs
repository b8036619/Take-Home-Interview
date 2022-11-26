using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class page1Controller : MonoBehaviour
{

    private GameObject imageTextBlock;

    
    void Start() //Sets the image and text blocks into position
    {
        int positionY = -2000;
        for(int i = 1; i < 11; i++)
        {
            imageTextBlock = GameObject.Find("ImageAndTextBlock" + i);
            imageTextBlock.transform.position = new Vector3(imageTextBlock.transform.position.x, positionY, imageTextBlock.transform.position.z);
            positionY = positionY + 750;
        }

        positionY = -2375;
        for (int i = 1; i < 11; i++)
        {
            imageTextBlock = GameObject.Find("ImageAndTextBlockInv" + i);
            imageTextBlock.transform.position = new Vector3(imageTextBlock.transform.position.x, positionY, imageTextBlock.transform.position.z);
            positionY = positionY + 750;
        }


    }

    
    void Update()
    {

        for (int i = 1; i < 11; i++)
        {
            boundaryCheck(GameObject.Find("ImageAndTextBlock" + i));
            boundaryCheck(GameObject.Find("ImageAndTextBlockInv" + i));

            
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

    private void boundaryCheck(GameObject obj) //Checks if an image and text block have gone too far from the screen and sends them to the bottom or top
    {

        if (obj.transform.position.y > 5125)
        {
            obj.transform.position = new Vector3(obj.transform.position.x, -2375f, obj.transform.position.z);
        }
        else if (obj.transform.position.y < -2750)
        {
            obj.transform.position = new Vector3(obj.transform.position.x, 4750f, obj.transform.position.z);
        }

    }

}
