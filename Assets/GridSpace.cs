using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GridSpace : MonoBehaviour {
    private GameController gameController;
    public Button button;
    public int row;
    public int col;
    public void Initialize(GameController controller, int i, int j)
    {
        gameController = controller;
        row = i;
        col = j;
        button.interactable = false;
    }
    
    public void SetSpace()
    {
        button.interactable = false;
        if (gameController.currentPhase == 1)
        {
            
            gameController.setMarker(gameController.playerMarker[gameController.currentPlayer], row, col);
            gameController.EndMovePhase();
        }
        else if (gameController.currentPhase == 2){
            gameController.CalculateEffect(gameController.currentCardID, row, col);
            //gameController.EndAttackPhase();
        }
        
        
    }

}
