using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardInfo : MonoBehaviour {
    private GameController gameController;
    public Sprite[] sprites;
    
    public int[] atkType;
    public string[] atkName;
    public int[] atk;
    public int[] knock;
    public int[] cost;
    public int[] range;
    public int[] radius;
    public const int MELEE = 0;
    public const int TARGET = 1;
    public const int SELF = 2;
    
    public void Initialize(GameController controller)
    {
        gameController = controller;
        atkType = new int[] { -1,0,0,0,0,0
                                ,0,0,0,0,1
                                ,4,3,2,2,4
                                ,0,0,0,0,0};
        atkName = new string[] { "","1_Step Card"," 2_Blitz_Sweep"," 3_Diffusion Shot"," 4_Hawk Slash"," 5_Full Power",
            " 6_Rush Smash"," 7_Quick Steal"," 8_Back Strike"," 9_Flash Sliver"," 10 Warp Assault",
            " 11 Wave Force"," 12 Ruin Sphere"," 13 Null Blast"," 14 Flame Blast"," 15 Comet Fall",
            " 16 Attack +"," 17 Mobility +"," 18 HP +"," 19 Draw Charge"," 20_Ultimus", };
        atk = new int[] { 0,6,4,4,6,8,
                            5,1,6,7,8,
                            4,4,2,5,6,
                            0,0,0,0,13};
        knock = new int[] { 0,3,2,2,2,4,
                              2,0,2,2,2,
                              1,0,1,2,2,
                              0,0,0,0,4};
        cost = new int[] { 0,0,0,0,1,2,
                             0,0,0,1,2,
                             0,0,0,1,2,
                             0,0,0,0,3};
        range = new int[] { 0,1,0,1,1,1,
                              1,2,1,1,2,
                              4,3,2,2,4,
                              0,0,0,0,0};
        radius = new int[] { 0,0,1,0,0,0,
                               0,0,0,0,1,
                               0,0,1,0,0,
                               1,1,1,0,2};

    }

    
    public void specialEffect(int id,int playerType)
    {
        switch (id)
        {
            case (1):
                if (playerType == 0)
                {

                }
                break;
            case (2):
                if (playerType == 0)
                {

                }
                break;
            case (6):
                if (playerType == 1)
                {

                }
                break;
            case (11):
                if (playerType == 1)
                {

                }
                break;
            case (12):
                if (playerType == 2)
                {

                }
                break;
            case (13):
                if (playerType == 2)
                {

                }
                break;
            case (16):
                if (playerType == 0)
                {

                }
                break;
            case (17):
                if (playerType == 1)
                {

                }
                break;
            case (18):
                if (playerType == 2)
                {

                }
                break;
            case (20):
                if (playerType == 0)
                {

                }
                else if (playerType == 1)
                {

                }
                else if (playerType == 2)
                {

                }
                break;
            default:
                break;
        }
    }

}
