using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHP : MonoBehaviour {
    public Text hpText;
    public int hp;

    public void Initialize(int initHP)
    {
        
        setHP(initHP);
    }
    public void setHP(int newHP)
    {
        hp = newHP;
        print("Text Updated "+hp);
        hpText.text = "" + hp;
    }

    public bool isDead()
    {
        return hp <= 0;
    }
}
