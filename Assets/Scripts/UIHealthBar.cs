using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{
    public static UIHealthBar instance { get; private set; }
    public Image mask;
    float orginalSize;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        orginalSize = mask.rectTransform.rect.width;   
    }


    public void SetValue(float value) {
        mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, orginalSize *value);
    }
}
