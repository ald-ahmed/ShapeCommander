using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIClickable : Clickable
{
    public delegate void ClickHandler();

    public ClickHandler clickHandler;

    private Image myImage;

    [SerializeField]
    private Color highlightedColor;

    [SerializeField]
    private Color defaultColor;

    private bool cursorLeft = true;

    protected override void OnClicked()
    {
        UnHighlighted();
        clickHandler?.Invoke();
    }

    public override void Highlighted()
    {
        base.Highlighted();
        if (cursorLeft)
        {
            cursorLeft = false;
            myImage.color = highlightedColor;
            AudioManager.instance.Play("HighlightNoise");
        }
        
        //could also add a brief size increase while highlighted
        
    }
    public override void UnHighlighted()
    {
        base.UnHighlighted();
        myImage.color = defaultColor;
        cursorLeft = true;
        
    }
    //TODO: Make the box collider match the Width of the Rect Transform so having different size buttons doesn't mean squashing and stretching the prefab
    void Start()
    {
        base.Start();
        
        //get visual components
        myImage = gameObject.GetComponent<Image>();
        myImage.color = defaultColor;
    }
}
