using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Card : MonoBehaviour {

    private int atkType; //0 is Melee, 1 is target
    private string atkName;
    private int atk;
    private int knock;
    private int cost;
    private int range;
    private GameController gameController;
    public Sprite[] sprites;
    public Sprite blankCard;
    private bool usable;
    public void Initialize(GameController controller)
    {
        gameController = controller;
    }
    public void SetImage(int id)
    {
        if(id > 0)
        {
            gameObject.GetComponent<Image>().sprite = sprites[id-1];
            print("Set image " + id);
            usable = true;
        }
        else
        {
            gameObject.GetComponent<Image>().sprite = blankCard;
            print("Set image " + id);
            usable = false;
        }
    }

    public void DisableCard()
    {
        gameObject.GetComponent<Button>().interactable = false;
        
    }
    public void EnableCard()
    {
        print("card enabled");
        gameObject.GetComponent<Button>().interactable = true;
    }

    public void UseCard()
    {
        print("card used!");
        SetImage(-1);
        gameController.player[gameController.currentPlayer].DecreaseNumberInHand();
        gameController.EndTurn();
        
    }

    public bool isUsable()
    {
        return usable;
    }
    //public Card(int id)
    //{
    //    atkType = 0;
    //    atkName = "BACK SLASH";
    //    atk = 1;
    //    knock = 1;
    //    cost = 0;
    //    range = 1;
    //}
}
