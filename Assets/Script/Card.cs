using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Card : MonoBehaviour {

    public int atkType; //0 is Melee, 1 is target, 2 is self
    public string atkName;
    public int atk;
    public int knock;
    public int cost;
    public int range;
    private GameController gameController;
    //private CardPreview cardPreview;
    public bool usable;
    public int ID;
    public void Initialize(GameController controller)
    {
        gameController = controller;
        //cardPreview = gameController.cardPreview;
    }
    public void SetCard(int id)
    {
        ID = id;
        if (id > 0)
        {
            
            gameObject.GetComponent<Image>().sprite = gameController.cardinfo.sprites[id];
            print("Set image " + id);
            usable = true;
            atkType = gameController.cardinfo.atkType[id];
            atkName = gameController.cardinfo.atkName[id];
            atk = gameController.cardinfo.atk[id];
            knock = gameController.cardinfo.knock[id];
            cost = gameController.cardinfo.cost[id];
            range = gameController.cardinfo.range[id];
        }
        else
        {
            gameObject.GetComponent<Image>().sprite = gameController.cardinfo.sprites[0];
            print("Set image " + 0);
            usable = false;
        }
       
    }

    public void DisableCard()
    {
        gameObject.GetComponent<Button>().interactable = false;
        
    }
    public void EnableCard()
    {
        //print("card enabled");
        gameObject.GetComponent<Button>().interactable = true;
    }

    public void UseCard()
    {
        int i = ID;
        print("card id" + i + " used!");
        SetCard(-1);
        DisableCard();
        gameController.player[gameController.currentPlayer].DecreaseNumberInHand();
        gameController.PerformAction(i);
    }

    public bool isUsable()
    {
        return usable;
    }

    public void Show()
    {
        gameController.ShowPreview(gameObject.GetComponent<Image>().sprite);
        
    }
    public void Hide()
    {
        gameController.HidePreview();
        
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
