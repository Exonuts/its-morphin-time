using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public GameObject mainMenu;
    public GameObject settingsMenu;

    public TMP_Text playText;
    public TMP_Text settingsText;
    public TMP_Text thirdText;

    int selected = 0; // 0 = play 1 = settings

    // Start is called before the first frame update
    void Start() {
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.DownArrow)){
            if (selected == 2){
                selected = 2;
            } else {
                selected++;
            }
        } else if (Input.GetKeyDown(KeyCode.UpArrow)){
            if (selected == 0){
                selected = 0;
            } else {
                selected--;
            }
        } else if (Input.GetKeyDown(KeyCode.Space)){
            switch (selected){
                case 0:
                    GoPlay();
                    break;
                case 1:
                    break;
            }
        } else if (Input.GetKeyDown(KeyCode.L)){
            SceneManager.LoadScene("TESTTESTTEST");
        }

        switch (selected){
            case 0:
                SetPlaySelected();
                break;
            case 1:
                SetSettingsSelected();
                break;
            case 2:
                SetThirdSelected();
                break;
        }
    }

    public void GoPlay() {
        SceneManager.LoadScene("Scenes/HugoScene"); // this is temporary, i can send it somewhere else if need be
        Debug.Log("play");
    }
    

    public void GoSettings() {
        Debug.Log("This button really doesnt do anything");
    }


    public void GoQuit() {
        Application.Quit(0);
    }


    public void SetPlaySelected(){
        selected = 0;
        playText.color = Color.red;
        playText.fontStyle = FontStyles.Underline;
        settingsText.color = Color.black;
        settingsText.fontStyle = FontStyles.Normal;
        thirdText.color = Color.black;
        thirdText.fontStyle = FontStyles.Normal;
    }

    public void SetSettingsSelected(){
        selected = 1;
        playText.color = Color.black;
        playText.fontStyle = FontStyles.Normal;
        settingsText.color = Color.red;
        settingsText.fontStyle = FontStyles.Underline;
        thirdText.color = Color.black;
        thirdText.fontStyle = FontStyles.Normal;
    }

    public void SetThirdSelected(){
        selected = 2;
        playText.color = Color.black;
        playText.fontStyle = FontStyles.Normal;
        settingsText.color = Color.black;
        settingsText.fontStyle = FontStyles.Normal;
        thirdText.color = Color.red;
        thirdText.fontStyle = FontStyles.Underline;
    }
}
