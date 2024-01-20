using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnDiscard : MonoBehaviour
{
    // OnDiscard
    public int discardedCardCount = 0;
    public  List<RawImage>  discardCardImages;

    public static OnDiscard instance;

    private void Awake()
    {
        instance = this;
    }

    public void SetCardColor()
    {
        discardCardImages[discardedCardCount-1].color = Color.red;
    }
}
