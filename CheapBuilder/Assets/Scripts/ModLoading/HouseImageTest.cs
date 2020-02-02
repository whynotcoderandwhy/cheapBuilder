using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HouseImageTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        RawImage img = gameObject.GetComponent<RawImage>();
        Texture2D loadedImg = FileLoader.LoadImageFromDisk("C:\\Users\\krus0021\\Desktop\\CheapBuilder\\cheapBuilder\\CheapBuilder\\Resources\\Images\\House_Small.png");
        img.texture = loadedImg;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
