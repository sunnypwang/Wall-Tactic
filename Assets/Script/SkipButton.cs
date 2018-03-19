using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SkipButton : MonoBehaviour {
    public Button button;
    public Text buttonText;
    private GameController gameController;

    public void Initialize(GameController controller)
    {
        gameController = controller;
        Disable();
        buttonText.text = "SKIP";
    }

    public void Skip()
    {
        if (gameController.currentPlayer == 0)
        {
            
            if (gameController.currentPhase == 0)
            {
                print("AAA start turn");
                gameController.StartTurn();
            }
            else
            {
                print("BBB next phase");
                Disable();
                gameController.NextPhase();
            }
            
        } else
        {
            print("CCC start bot turn");
            gameController.StartBotTurn();
            
        }
        
    }

    public void Disable()
    {
        button.interactable = false;
    }

    public void Enable()
    {
        button.interactable = true;
    }
}
