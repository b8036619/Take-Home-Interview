using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class screenResize : MonoBehaviour
{
    public RectTransform Page1Scroll;

    private float screenX;
    private float screenY;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        screenX = Screen.width;
        screenY = Screen.height;

        Debug.Log(screenY);

        float onePercentY = screenY / 100;
        float ySize = onePercentY * 70;

        Page1Scroll.sizeDelta = new Vector2(screenX, ySize);
        Page1Scroll.localPosition = new Vector3(0, onePercentY * 2.5f, 0);
    }

    private void FixedUpdate()
    {
        
    }
}
