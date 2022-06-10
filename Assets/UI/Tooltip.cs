using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ResourceManagement;

public class Tooltip : MonoBehaviour, IToolTip
{
    public Image buildingImage;
    public ResourceManager resourceManager;
    public string tooltipDescription;

    public float getOffsetX()
    {
        throw new System.NotImplementedException();
    }

    public Sprite getToolTipImage()
    {
        return buildingImage.sprite;
    }

    public string getToolTipMessage()
    {
        return tooltipDescription;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
