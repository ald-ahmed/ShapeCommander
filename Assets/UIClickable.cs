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

    protected override void OnClicked()
    {
        UnHighlighted();
        clickHandler?.Invoke();
    }

    public override void Highlighted()
    {
        base.Highlighted();
        myImage.color = highlightedColor;

        //could also add a brief size increase while highlighted
        
    }
    public override void UnHighlighted()
    {
        base.UnHighlighted();
        myImage.color = defaultColor;
        
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
