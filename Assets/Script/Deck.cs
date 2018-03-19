using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Deck : MonoBehaviour {
    private List<int> deck;
    private System.Random _random;
    private GameController gameController;

    public void Initialize(GameController controller)
    {
        gameController = controller;
        _random = new System.Random();
        //deck = new List<int> {1,1,1,1,1,1,6,6,6,6,6,6,11,11,11,11,11,11, 2, 4, 7, 9, 12, 14 , 2, 4, 7, 9, 12, 14 , 2, 4, 7, 9, 12, 14 , 2, 4, 7, 9, 12, 14, 3, 5, 8, 10, 13, 15 , 3, 5, 8, 10, 13, 15 , 3, 5, 8, 10, 13, 15, 19 , 19 , 19 , 19 , 19 ,20,20,20};
        deck = new List<int> { 1,1,1,1,1,
                                2,2,2,2,2,
                                8,8,8,8,8,
                                10,10,10,10,10,
                                12,12,12,12,12,
                                13,13,13,13,13};
        Shuffle(deck);
        //foreach (int i in deck)
        //{
        //    print(i);
        //}
        
    }

    void Shuffle(List<int> array)
    {
        int p = array.Count;
        for (int n = p - 1; n > 0; n--)
        {
            int r = _random.Next(0, n);
            int t = array[r];
            array[r] = array[n];
            array[n] = t;
        }
    }
    public int Draw()
    {
        if(deck.Count > 0)
        {
            int top = deck[deck.Count - 1];
            deck.RemoveAt(deck.Count - 1);
            print(deck.Count);
            return top;
        }
        else
        {
            print("Deck empty!");
            print(deck.Count);
            return -1;
        }

    }
    public int getTop()
    {
        return deck[deck.Count - 1];
    }
}
