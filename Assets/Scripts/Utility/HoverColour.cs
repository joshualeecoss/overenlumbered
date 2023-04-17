using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HoverColour : MonoBehaviour
{
    public Button button;
    public Color wantedColor;
    private Color originalColor;
    private ColorBlock cb;

    void Start()
    {
        cb = button.colors;
        originalColor = cb.selectedColor;
    }

    void Update()
    {

    }

    public void ChangeWhenHover()
    {
        cb.selectedColor = wantedColor;
        button.colors = cb;
    }

    public void ChangeWhenLeaves()
    {
        cb.selectedColor = originalColor;
        button.colors = cb;
    }

}
