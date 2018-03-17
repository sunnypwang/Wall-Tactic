using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameController : MonoBehaviour {
    public Deck deck;
    public PlayerHand[] player;
    public int currentPlayer;
    //public Card[] card;
    void Awake()
    {
        deck.Initialize(this);
        //for(int i=0;i<players.Length;i++)
        //{
        //    players[i].Initialize(i, this);
        //}
        player[0].Initialize(0,this);
        
        currentPlayer = 0;
        StartTurn();
    }

    public void StartTurn()
    {
        print("Player " + currentPlayer + "'s turn");
        player[currentPlayer].PlayDrawPhase();
        player[currentPlayer].PlayAttackPhase();
        
    }
    public void EndTurn()
    {
        player[currentPlayer].DisableHand();
        currentPlayer++;
        currentPlayer = (currentPlayer == 4) ? 0 : currentPlayer;
    }
}
