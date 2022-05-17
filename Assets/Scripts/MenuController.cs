using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuController : MonoBehaviour
{
    public enum Button {Play, Instruccions, Credits};
    public Button button;

    public TMP_Text play;
    public TMP_Text instructions;
    public TMP_Text credits;

    private TextMeshProUGUI textPlay;
    private TextMeshProUGUI textInst;
    private TextMeshProUGUI textCredits;
    
    // Start is called before the first frame update
    void Start()
    {
        textPlay = play.GetComponent<TextMeshProUGUI>();
        textInst = instructions.GetComponent<TextMeshProUGUI>();
        textCredits = credits.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        changeButtons();

    }

    private void changeButtons() {
        switch (button) {
            case Button.Play:
                textPlay.fontSize = 60;
                textPlay.outlineColor = new Color32(0,255,0,255);
                textPlay.outlineWidth = 0.5f;

                textInst.fontSize = 40;
                textInst.outlineColor = Color.black;
                textInst.outlineWidth = 0.25f;

                textCredits.fontSize = 40;
                textCredits.outlineColor = Color.black;
                textCredits.outlineWidth = 0.25f;
            break;
            case Button.Instruccions:
                textInst.fontSize = 60;
                textInst.outlineColor = new Color32(0,255,0,255);
                textInst.outlineWidth = 0.5f;

                textPlay.fontSize = 40;
                textPlay.outlineColor = Color.black;
                textPlay.outlineWidth = 0.25f;

                textCredits.fontSize = 40;
                textCredits.outlineColor = Color.black;
                textCredits.outlineWidth = 0.25f;
            break;
            case Button.Credits:
                textCredits.fontSize = 60;
                textCredits.outlineColor = new Color32(0,255,0,255);
                textCredits.outlineWidth = 0.5f;

                textPlay.fontSize = 40;
                textPlay.outlineColor = Color.black;
                textPlay.outlineWidth = 0.25f;

                textInst.fontSize = 40;
                textInst.outlineColor = Color.black;
                textInst.outlineWidth = 0.25f;
            break;
        }
    }
    public void changeModeUp() {
        if(button == Button.Play) button = Button.Credits;
        else if(button == Button.Instruccions) button = Button.Play;
        else if(button == Button.Credits) button = Button.Instruccions;
    }
    public void changeModeDown() {
        if(button == Button.Play) button = Button.Instruccions;
        else if(button == Button.Instruccions) button = Button.Credits;
        else if(button == Button.Credits) button = Button.Play;
    }
}
