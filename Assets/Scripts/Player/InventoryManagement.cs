using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InventoryManagement : MonoBehaviour {

    public bool isTouchingWall = false;
    public TMP_Text testTest;

    #region pause menu variables
    private int menuSelected = 0;
    public GameObject pauseMenu;
    public TMP_Text resumeText;
    public TMP_Text menuText;
    Boolean isPaused = false;
    #endregion
    
    #region images and sprites
    public Image overlay;
    public Sprite selectedOne;
    public Sprite selectedTwo;
    public Sprite selectedThree;
    public Sprite selectedFour;

    public int selected = 1;

    public Image formOne;
    public Image formTwo;
    public Image formThree;
    public Image formFour;

    public Sprite noSprite;
    public Sprite slime;
    public Sprite box;
    public Sprite pillar;
    public Sprite balloon;
    public Sprite ball;
    #endregion 

    #region inventory   
    public TMP_Text noticeText;
    int[] inventory = new int[4]; // four slots
    // index 0 will always be player and cannot change
    // for now level 1-2 will give you 1 index unlock slot, levels onwords tbd
    // int 1 = box, int 2 = pillar int 3 = balloon if index 1 is 1 the second form is box
    int level = 0; // at different levels you unlock the ability to hold more forms
    int hoveredForm = 0; // 0 if nothing is being hovered, 1 if box is being hovered, 2 for pillar, 3 for balloon etc
    Boolean swapping = false;
    CollideCheckerGroupScript ccgs;
    #endregion
    
    #region cooldowns
    int[] cds = {0,10,10,10};
    public Boolean[] onCD = {false,false,false,false};

    IEnumerator Cooldown(int which){
        yield return new WaitForSeconds(cds[which]);
        onCD[which] = false;
    }
    #endregion

    // Start is called before the first frame update
    void Start() { noticeText.text = ""; swapping = false; ccgs = GetComponentInParent<CollideCheckerGroupScript>();
    } // HAHAHA I CAN PUT IT ALL ON ONE LINE

    // Update is called once per frame
    void Update() {
        #region pause menu update things
        if (isPaused){ // sets pause menu visibility based on if the game is paused or not
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
        } else{
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
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

        if (Input.GetKeyDown(KeyCode.Alpha1) && !swapping){ // looks for key presses to detect when key pressed
            if (selected == 1) { // if currently active is trying to be swapped too
                noticeText.text = "Cannot swap to currently active form"; // complain
                return;
            } else if (onCD[0]){
                noticeText.text = "This form is on cooldown";
                return;
            } else if (!ccgs.CanSpawn(inventory[0])){ // if not enough space
                noticeText.text = "Cannot swap forms here. Not enough space"; // complain
                return;
            }  else { // if okay
                selected = 1; // switch
                noticeText.text = "";
                onCD[0] = true;
                StartCoroutine(Cooldown(0));
            }
        } else if (Input.GetKeyDown(KeyCode.Alpha2) && inventory[1] != 0 && !swapping){ // if not swapping, and inventory not empty
            if (selected == 2) { // if currently active is trying to be swapped too
                noticeText.text = "Cannot swap to currently active form"; // complain
                return;
            } else if (onCD[1]){
                noticeText.text = "This form is on cooldown";
                return;
            }  else if (!ccgs.CanSpawn(inventory[1])){ // if not enough space
                noticeText.text = "Cannot swap forms here. Not enough space"; // complain
                return;
            }  else { // if okay
                selected = 2; // switch
                noticeText.text = "";
                onCD[1] = true;
                StartCoroutine(Cooldown(1));
            }
        } else if (Input.GetKeyDown(KeyCode.Alpha3)&& inventory[2] != 0 && !swapping){
            if (selected == 3) { // if currently active is trying to be swapped too
                noticeText.text = "Cannot swap to currently active form"; // complain
                return;
            } else if (onCD[2]){
                noticeText.text = "This form is on cooldown"; // complain
                return;
            }   else if (!ccgs.CanSpawn(inventory[2])){ // if not enough space
                noticeText.text = "Cannot swap forms here. Not enough space"; // complain
                return;
            } else { // if okay
                selected = 3; // switch
                noticeText.text = "";
                onCD[2] = true;
                StartCoroutine(Cooldown(2));
            }
        } else if (Input.GetKeyDown(KeyCode.Alpha4) && inventory[3] != 0 && !swapping){ // key pressed, not taking a form, 
            if (selected == 4) { // if currently active is trying to be swapped too
                noticeText.text = "Cannot swap to currently active form"; // complain
                return;
            } else if (onCD[3]){
                noticeText.text = "This form is on cooldown";
                return;
            } else if (!ccgs.CanSpawn(inventory[3])){ // if not enough space
                noticeText.text = "Cannot swap forms here. Not enough space"; // complain
                return;
            } else { // if okay
                selected = 4; // switch
                noticeText.text = "";
                onCD[3] = true;
                StartCoroutine(Cooldown(3));
            }
        } 

        if (Input.GetKeyDown(KeyCode.Escape)){
            if (isPaused){
                Resume();
            } else {
                isPaused = true;
            }
        } else if (Input.GetKeyDown(KeyCode.F) && hoveredForm != 0 && selected == 1){ // interact key pressed and form is avaiable (can only switch on main character)
            SwitchForm(hoveredForm);
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha1) && swapping){
            noticeText.text = "You cannot replace the player";
            swapping = false;
        } else if (Input.GetKeyDown(KeyCode.Alpha2) && swapping && AvailSlot() >= 1){
            if (inventory[1] != hoveredForm){ // if what is already stored isnt what youre trying to switch
               inventory[1] = hoveredForm; // then switch it correctly
               noticeText.text = "form swapped succesfully";
            } else {
                noticeText.text = "You already have this form in that slot";
            }
            swapping = false;
        } else if (Input.GetKeyDown(KeyCode.Alpha3) && swapping && AvailSlot() >= 2){
            if (inventory[2] != hoveredForm){ // if what is already stored isnt what youre trying to switch
                inventory[2] = hoveredForm; // then switch it correctly
               noticeText.text = "form swapped succesfully";
            } else {
                noticeText.text = "You already have this form in that slot";
            }
            swapping = false;
        } else if (Input.GetKeyDown(KeyCode.Alpha4) && swapping && AvailSlot() >= 3){
            if (inventory[3] != hoveredForm){ // if what is already stored isnt what youre trying to switch
                inventory[3] = hoveredForm; // then switch it correctly
               noticeText.text = "form swapped succesfully";
            } else {
                noticeText.text = "You already have this form in that slot";
            }
            swapping = false;
        } else if (Input.GetKeyDown(KeyCode.Alpha2) && swapping || Input.GetKeyDown(KeyCode.Alpha3) && swapping || Input.GetKeyDown(KeyCode.Alpha4) && swapping) {
            noticeText.text = "you are not high enough level yet!";
        }
        
        if (Input.GetKeyDown(KeyCode.L)){
            level++;
        }

        UpdateEquippedForms();
        UpdateFormList();
        AvailSlot();
    }

    void UpdateEquippedForms(){ // super simple lot of switch statments that change the UI elements based on what forms are equipped
        switch (inventory[0]){
            case 0: // it should only be this really.
                formOne.sprite = slime; 
                break;
            case 1: //shouldn thit this
                break;
        }
        formTwo.sprite = inventory[1] switch {
            1 => box,
            2 => pillar,
            3 => balloon,
            4 => ball,
            _ => noSprite,
        };
        formThree.sprite = inventory[2] switch {
            1 => box,
            2 => pillar,
            3 => balloon,
            4 => ball,
            _ => noSprite,
        };
        formFour.sprite = inventory[3] switch {
            1 => box,
            2 => pillar,
            3 => balloon,
            4 => ball,
            _ => noSprite,
        };
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

    }

    void SwitchForm(int toSwitch){ // stops you if you already have the form, if you cannot learn enough forms, or if you need to swap it
        for (int i = 0; i < 4; i++){
            if (inventory[i] == toSwitch){
                noticeText.text = "You already have this form";
                return;
            }
        }
        if (AvailSlot() == 0){
            noticeText.text = "You cannot learn multiple forms at this time"; // the option for forms should only appear once unlocked, so you shouldnt really get this
        } else {
            String toSwitchtx = "";
            int slotAdded = 0;
            switch (toSwitch){
                case 0:
                    Debug.Log("How the fuck?"); // should never happen
                    break;
                case 1:
                    toSwitchtx = "Box"; // if swapping box form
                    break;
                case 2:
                    toSwitchtx = "Pillar"; // if swapping pillar form
                    break;
                case 3:
                    toSwitchtx = "Balloon"; // if swapping balloon form
                    break;
                case 4:
                    toSwitchtx = "Ball"; // if swapping ball form
                    break;
            }
            switch(AvailSlot()){
                case 1: // if you have one unlocked slot
                    if (inventory[1] == 0){ // and that one slot is avaiable
                        slotAdded = 1;
                    } else {
                        noticeText.text = "All available slots are full, please select a form to replace";
                        swapping = true;
                        return;
                    }
                    break;    
                case 2: // if you have two unlocked slots
                    if (inventory[1] == 0){ // and slot 1 is avaiable
                        slotAdded = 1; // add to slot 1
                    } else if (inventory[2] == 0) { // if slot 1 isnt avaialbe
                        slotAdded = 2; // add to slot 2
                    } else { // otherwise commence replace
                        noticeText.text = "All avaiable slots are full, please select a form to replace";
                        swapping = true;
                        return;
                    }
                    break;      
                case 3: // if you have two unlocked slots
                    if (inventory[1] == 0){ // and slot 1 is avaiable
                        slotAdded = 1; // add to slot 1
                    } else if (inventory[2] == 0) { // if slot 1 isnt avaialbe
                        slotAdded = 2; // add to slot 2
                    } else if (inventory[3] == 0) { // if slot 1 and 2 arent avaialbe
                        slotAdded = 3; // add to slot 3
                    } else { // otherwise commence replace
                        noticeText.text = "All avaiable slots are full, please select a form to replace";
                        swapping = true;
                        return;
                    }
                    break;      
            }
            inventory[slotAdded] = toSwitch;
            noticeText.text = toSwitchtx + " form has been added to slot " + slotAdded;
        }
    }

    int AvailSlot(){ // simple calculation for how many form slots you have based on what elvel the player is (subject to change)
        if (level == 0){ // 0 no slots
            return 0;
        } else if (level <= 2){ // 1 2 | 1 extra
            return 1;
        } else if (level <= 4){ // 3 4 | 2 extra
            return 2;
        } else { // all slots unlocked
            return 3;
        }
    }

    void UpdateFormList(){ // testing function that displays text
        testTest.text = "";
        switch (inventory[0]){
            case 0:
                testTest.text += "Form1 = player";
                break;
            case 1: 
                Debug.Log("this should never happen");
                break;
            case 2:
                Debug.Log("this should never happen");
                break;
            case 3:
                Debug.Log("this should never happen");
                break;
        }
        if (onCD[0]){
            testTest.text += " on CD;";
        }
        testTest.text += "\n";
        switch (inventory[1]){
            case 0:
                testTest.text += "Form2 = empty";
                break;
            case 1: 
                testTest.text += "Form2 = box";
                break;
            case 2:
                testTest.text += "Form2 = pillar";
                break;
            case 3:
                testTest.text += "Form2 = balloon";
                break;
            case 4:
                testTest.text += "Form2 = ball";
                break;
        }
        if (onCD[1]){
            testTest.text += " on CD;";
        }
        testTest.text += "\n";
        switch (inventory[2]){
            case 0:
                testTest.text += "Form3 = nothing";
                break;
            case 1: 
                testTest.text += "Form3 = box";
                break;
            case 2:
                testTest.text += "Form3 = pillar";
                break;
            case 3:
                testTest.text += "Form3 = balloon";
                break;
            case 4:
                testTest.text += "Form3 = ball";
                break;
        }
        if (onCD[2]){
            testTest.text += " on CD;";
        }
        testTest.text += "\n";
        switch (inventory[3]){
            case 0:
                testTest.text += "Form4 = nothing";
                break;
            case 1: 
                testTest.text += "Form4 = box";
                break;
            case 2:
                testTest.text += "Form4 = pillar";
                break;
            case 3:
                testTest.text += "Form4 = balloon";
                break;
            case 4:
                testTest.text += "Form4 = ball";
                break;
        }
        if (onCD[3]){
            testTest.text += " on CD;";
        }
        testTest.text += "\nLevel:" + level;
        testTest.text += "\nSlots:" + AvailSlot();
    }

    #region switching and inventory functions
    void OnTriggerEnter2D(Collider2D trigger) {
        //Debug.Log("collision" + trigger.gameObject.name);
        if (selected == 1){ // only swithcable on player
            switch (trigger.gameObject.name){
                case "GoBox":
                    //Debug.Log("box interaction");
                    hoveredForm = 1;
                    noticeText.text = "[f] to pick up Box";
                    break;
                case "GoPillar":
                    //Debug.Log("pillar interaction");
                    hoveredForm = 2;
                    noticeText.text = "[f] to pick up pillar";
                    break;
                case "GoBalloon":
                    //Debug.Log("balloon interaction");
                    hoveredForm = 3;
                    noticeText.text = "[f] to pick up Balloon";
                    break;
                case "GoBall":
                    hoveredForm = 4;
                    noticeText.text = "[f] to pick up Ball";
                    break;
                case "level up":
                    level++;
                    break;
                case "level down":
                    level--;
                    break;
            }
        }
        if (trigger.CompareTag("Wall")) {
            isTouchingWall = true;
            //Debug.Log("SmallCharacterCollider isTouchingWall = true");
        }
    }

    void OnTriggerExit2D(Collider2D trigger) {
        //Debug.Log("Exiting trigger");
        hoveredForm = 0;
        noticeText.text = "";
        swapping = false;
        if (trigger.CompareTag("Wall")) {
            isTouchingWall = false;
            //Debug.Log("SmallCharacterCollider isTouchingWall = false");
        }
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