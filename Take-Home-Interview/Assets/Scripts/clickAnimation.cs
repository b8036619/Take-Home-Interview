using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clickAnimation : MonoBehaviour
{
    private Animator wolfAni;
    private float normalSpeed;

    void Start()
    {
        wolfAni = GetComponent<Animator>();
        normalSpeed = wolfAni.speed;
    }

    private void OnMouseDown() // When clicked animation plays
    {
        wolfAni.speed = normalSpeed;
        wolfAni.Play("run");
    }

    private void OnMouseUp() // When mouse is unclicked the animation takes 2 seconds to stop
    {
        StartCoroutine(StopAnimation());
    }

    IEnumerator StopAnimation()
    {
        float time = 2f;
        yield return new WaitForSeconds(time);

        wolfAni.speed = 0;
    }
}
