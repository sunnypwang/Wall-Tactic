using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHand : MonoBehaviour {
    public Card[] hand;
    public Player player;
    public int numberInHand;
    private GameController gameController;
    private int playerNo;
    public void Initialize(int no, GameController controller)
    {
        gameController = controller;
        for (int i = 0; i < hand.Length; i++)
        {
            //player.GetComponentInChildren<Card[]>()[i].Initialize(this);
            //player.GetComponentInChildren<Card[]>()[i].SetImage();
            hand[i].Initialize(gameController);
        }
        playerNo = no;
        numberInHand = 5;
        for (int i = 0; i < numberInHand; i++)
        {
            hand[i].SetCard(gameController.deck.Draw());
        }
        for (int i = numberInHand; i < hand.Length; i++)
        {  
            hand[i].SetCard(-1);
        }
        DisableHand();
    }
    public void DisableHand()
    {
        for (int i = 0; i < hand.Length; i++)
        {
            hand[i].DisableCard();
        }
    }
    public void EnableHand()
    {
        for (int i = 0; i < hand.Length; i++)
        {
            if (hand[i].isUsable())
            {
                hand[i].EnableCard();
            }
        }
    } 
    public void IncreaseNumberInHand()
    {
        numberInHand++;
    }
    public void DecreaseNumberInHand()
    {
        numberInHand--;
    }
}
