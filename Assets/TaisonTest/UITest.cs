using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.TerrainTools;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UITest : MonoBehaviour
{
    #region in game hud
    public Image overlay;
    
    public Sprite selectedOne;
    public Sprite selectedTwo;
    public Sprite selectedThree;
    public Sprite selectedFour;

    int selected = 1;
    #endregion

    #region pause menu variables
    private int menuSelected = 0;
    public TMP_Text resumeText;
    public TMP_Text menuText;

    public GameObject pauseMenu;

    public Boolean isPaused = false;
    #endregion
    
    #region inventory management and form swap variables
    public TMP_Text noticeText;
    public int[] inventory = new int[3]; // four slots, form 0 will always be player. at level 1 you can only use slot 1 as an extra
    public int level = 0; // at different levels you unlock the ability to hold more forms

    #endregion
    
    // Start is called before the first frame update
    void Start() {
        noticeText.text = "";
    }

    // Update is called once per frame
    void Update() {
        #region in game hud variables
        switch(selected){ // switches the highlighted selected form based on key pressed
            case 1:
                overlay.sprite = selectedOne;
                break;
            case 2:
                overlay.sprite = selectedTwo;
                break;
            case 3:
                overlay.sprite = selectedThree;
                break;
            case 4:
                overlay.sprite = selectedFour;
                break;
        }
        #endregion

        #region pause menu update things
        if (isPaused){ // sets pause menu visibility based on if the game is paused or not
            pauseMenu.SetActive(true);
        } else{
            pauseMenu.SetActive(false);
        }

        switch(menuSelected){
            case 0:
                SetResumeSelected();
                break;
            case 1:
                SetGoMenuSelected();
                break;
        }
        #endregion

        if (Input.GetKeyDown(KeyCode.Alpha1)){ // looks for key presses to detect when key pressed
            selected = 1;
        } else if (Input.GetKeyDown(KeyCode.Alpha2)){
            selected = 2;
        } else if (Input.GetKeyDown(KeyCode.Alpha3)){
            selected = 3;
        } else if (Input.GetKeyDown(KeyCode.Alpha4)){
            selected = 4;
        } else if (Input.GetKeyDown(KeyCode.Escape)){
            if (isPaused){
                Resume();
            } else {
                isPaused = true;
            }
        }
    }

    #region switching and inventory functions
    void OnTriggerEnter2D(Collider2D trigger) {
        //Debug.Log("collision" + trigger.gameObject.name);
        switch (trigger.gameObject.name){
            case "GoBox":
                //Debug.Log("box interaction");
                noticeText.text = "[f] to pick up Box";
                break;
            case "GoPillar":
                //Debug.Log("pillar interaction");
                noticeText.text = "[f] to pick up pillar";
                break;
            case "GoBalloon":
                //Debug.Log("balloon interaction");
                noticeText.text = "[f] to pick up Balloon";
                break;
        }
    }

    void OnTriggerExit2D(Collider2D trigger) {
        //Debug.Log("Exiting trigger");
        noticeText.text = "";
    }
    #endregion

    #region pause menu functions
    public void SetResumeSelected(){
        menuSelected = 0;
        resumeText.color = Color.red;
        resumeText.fontStyle = FontStyles.Underline;
        menuText.color = Color.black;
        menuText.fontStyle = FontStyles.Normal;
    }
    public void SetGoMenuSelected(){
        menuSelected = 1;
        resumeText.color = Color.black;
        resumeText.fontStyle = FontStyles.Normal;
        menuText.color = Color.red;
        menuText.fontStyle = FontStyles.Underline;
    }

    public void Resume(){
        isPaused = false;
    }

    public void GoMainMenu(){
        SceneManager.LoadScene("Scenes/MainMenu");
    }
    #endregion
}