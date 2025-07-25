using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class UITest : MonoBehaviour
{
    public Image Overlay;
    
    public Sprite selectedOne;
    public Sprite selectedTwo;
    public Sprite selectedThree;
    public Sprite selectedFour;

    int selected = 1;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        switch(selected){
            case 1:
                break;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1)){
            selected = 1;
        } else if (Input.GetKeyDown(KeyCode.Alpha2)){
            selected = 2;
        } else if (Input.GetKeyDown(KeyCode.Alpha3)){
            selected = 3;
        } else if (Input.GetKeyDown(KeyCode.Alpha4)){
            selected = 4;
        }
    }
}
