using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public List<int> hand;
    private GameController gameController;
    private int playerNo;
    public void Initialize(int no, GameController controller)
    {
        gameController = controller;
        hand = new List<int>();
        playerNo = no;
    }

    public void PlayDrawPhase()
    {
        int drawnCard = gameController.deck.Draw();
        hand.Add(drawnCard);
        print("draws " + drawnCard);
        //gameObject.AddComponent(typeof(Card));
        gameController.EndTurn();
    }
}
