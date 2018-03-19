using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public int hp;
    public int playerType;
    public int team;
    private PlayerHand hand;

    private bool stun;//when wall rush

    private int atkBuff;
    private int atkBuffTurn;
    private int moveBuff;
    private int moveBuffTurn;
    public bool move;
    
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public bool isMove()
    {
        return move;
    }
    public bool isDead()
    {
        return this.hp <= 0;
    }
   
    public bool isUseCard()
    {
        if (hand.numberInHand <= 0) return false;
        if (stun)
        {
            stun = false;
            return true;
        }
        return false;
    }
    public bool isCounter()
    {

        // what type of character
        if (hand.numberInHand <= 0) return false;



        else return true;
    }
    
}
