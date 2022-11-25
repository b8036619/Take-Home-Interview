using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;

public class pageswitcher : MonoBehaviour
{
    public GameObject page1;
    public GameObject page2;
    public GameObject page3;

    private VisualElement root;

    
    // Start is called before the first frame update
    void Start()
    {
        switchToPage1();
    }

    private void Awake()
    {
        root = GetComponent<UIDocument>().rootVisualElement;

        root.Q<Button>("Page1Button").clicked += () => switchToPage1();
        root.Q<Button>("Page2Button").clicked += () => switchToPage2();
        root.Q<Button>("Page3Button").clicked += () => switchToPage3();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void setInactive()
    {
        page1.SetActive(false);
        page2.SetActive(false);
        page3.SetActive(false);
    }

    public void switchToPage1()
    {
        setInactive();
        page1.SetActive(true);
        root.Q<Label>("Header").text = ("Page 1");
    }

    public void switchToPage2()
    {
        setInactive();
        page2.SetActive(true);
        root.Q<Label>("Header").text = ("Page 2");
    }

    public void switchToPage3()
    {
        setInactive();
        page3.SetActive(true);
        root.Q<Label>("Header").text = ("Page 3");
    }


}
