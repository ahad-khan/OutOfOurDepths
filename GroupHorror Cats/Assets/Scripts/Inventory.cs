using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public RawImage KeyImage;
    public Texture[] KeyTextures = new Texture[4];
    //bool[] Keys = {false, false, false, false };
    public int KeyCount = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PickUpKey()
    {
        //Keys[WhichKey] = true;
        KeyCount = KeyCount + 1;
        switch (KeyCount)
        {
            case 1:
                KeyImage.texture = KeyTextures[0];
                break;
            case 2:
                KeyImage.texture = KeyTextures[1];
                break;
            case 3:
                KeyImage.texture = KeyTextures[2];
                break;
            case 4:
                KeyImage.texture = KeyTextures[3];
                break;
        }
    }
}
