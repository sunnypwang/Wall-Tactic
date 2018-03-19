using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    public Deck deck;
    public PlayerHand[] player;
    public List<int>[] botCard;
    public SkipButton skipButton;
    public Text phaseText;
    public CardInfo cardinfo;
    //public CardPreview cardPreview;
    public GameObject previewPanel;
    public PlayerMarker[] playerMarker;
    public GridSpace[] board;
    public PlayerHP[] playerHP;
    public GameObject gameOverText;
    public GameObject knockText;
    public int currentPlayer;
    public int currentPhase;
    //public Card[] card;
    public const int DRAW = 0;
    public const int MOVE = 1;
    public const int ATTACK = 2;
    public const int END = 3;
    public const int COUNTER = 4;
    public bool counterable;
    //public bool isMoving;
    public int attacker;    //which player is attacking in that moment
    public int currentCardID;
    public int[] hps = new int[] { 20, 20 };
    public bool[] knock = new bool[] { false, false };

    void Awake()
    {
        cardinfo.Initialize(this);
        //cardPreview.Initialize(this);
        deck.Initialize(this);
        player[0].Initialize(0, this);
        for (int j = 0; j < 8; j++)
        {
            for (int i = 0; i < 6; i++)
            {
                int idx = 6 * j + i;
                board[idx].Initialize(this, i, j);  //*****j,i not i,j*****
            }
        }
        //botCard = new List<int>[4] { new List<int> { }, new List<int> { 1, 1, 1, 1, 1 }, new List<int> { 1, 1, 1, 1, 1 } , new List<int> { 1, 1, 1, 1, 1 } };
        botCard = new List<int>[2] { new List<int> { }, new List<int> { 1, 1, 1, 1, 1 } };
        skipButton.Initialize(this);
        previewPanel.SetActive(false);
        for(int i = 0; i < 2; i++)
        {
            print("hps[i] " + hps[i]);
            playerHP[i].Initialize(hps[i]);
        }

        setMarker(playerMarker[0], 1, 1);
        setMarker(playerMarker[1], 4, 6);
        /*setMarker(playerMarker[1], 4, 1);
        setMarker(playerMarker[2], 1, 6);
        setMarker(playerMarker[3], 4, 6);*/
        phaseText.text = "INITIALIZED";
        currentPlayer = 0;
        currentPhase = 0;
        counterable = false;
        attacker = 0;
        gameOverText.SetActive(false);
        knockText.SetActive(false);
        //isMoving = false;
        StartTurn();
    }

    public void StartTurn()
    {
        print("Player " + (currentPlayer + 1) + "'s turn");
        currentPhase = 0;
        UpdatePhaseText();
        Draw();
        player[currentPlayer].EnableHand();
        NextPhase();
    }
    public void NextPhase()
    {
        if (currentPhase == COUNTER)
        {
            EndCounterPhase();
        }
        else
        {
            currentPhase++;
            skipButton.Enable();
            UpdatePhaseText();
            if (currentPhase == 3)
            {

                EndTurn();
                //currentPhase = 0;
            }

        }
        print("CURRENT PHASE : " + currentPhase);

    }
    public void StartCounterPhase()
    {
        currentPhase = COUNTER;
        UpdatePhaseText();
        player[0].EnableHand();
        skipButton.Enable();
    }
    public void EndCounterPhase()
    {
        currentPhase = END;
        UpdatePhaseText();
        player[0].DisableHand();
        skipButton.Disable();
    }
    public void EndTurn()
    {

        player[0].DisableHand();
        //skipButton.Disable();
        //if (counterable)
        //{
        //    for (int i = 0; i < player.Length; i++)
        //    {
        //        if (i != currentPlayer)
        //        {
        //            StartCounterPhase();
        //        }
        //    }

        //}
        currentPlayer++;
        currentPlayer = (currentPlayer == 2) ? 0 : currentPlayer;
        currentPhase = 0;
    }
    public void PerformAction(int cardID)
    {
        if (currentPhase == MOVE)
        {
            Move(cardID);
        }
        else if (currentPhase == ATTACK)
        {
            Attack(cardID);
            counterable = true;
        }
        else if (currentPhase == COUNTER)
        {
            Counter(cardID);
            NextPhase();
        }

    }
    public void Draw()
    {
        print("Draw!");
        if (player[currentPlayer].numberInHand < 7)
        {
            int top = deck.Draw();
            for (int i = 0; i < player[currentPlayer].hand.Length; i++)
            {
                if (!player[currentPlayer].hand[i].isUsable())
                {
                    player[currentPlayer].hand[i].SetCard(top);
                    break;
                }
            }
            player[currentPlayer].numberInHand++;
        }
    }
    public void Move(int cardID, int maxDist = 4)
    {
        print("P" + (currentPlayer + 1) + " move!");
        //isMoving = true;
        player[0].DisableHand();
        skipButton.Disable();
        int pRow = playerMarker[currentPlayer].row;
        int pCol = playerMarker[currentPlayer].col;
        foreach (GridSpace cell in board)
        {
            if (cell.col == playerMarker[1].col && cell.row == playerMarker[1].row)
            {
                cell.GetComponent<Button>().interactable = false;
            }
            else if (Mathf.Abs(cell.row - pRow) + Mathf.Abs(cell.col - pCol) <= maxDist)
            {
                cell.GetComponent<Button>().interactable = true;
            }
        }
        //wait until player select a cell to execute EndMovePhase()
    }
    public void EndMovePhase()
    {
        foreach (GridSpace cell in board)
        {
            cell.GetComponent<Button>().interactable = false;
        }
        player[0].EnableHand();
        skipButton.Enable();
        //isMoving = false;
        NextPhase();
    }
    public void Attack(int cardID)
    {
        currentCardID = cardID;
        hitbox(cardID);
        print("P" + (currentPlayer + 1) + " attack!");
        player[0].DisableHand();
        skipButton.Disable();
    }

    public void CalculateEffect(int id, int row, int col)
    {
        if (id == 10)
        {
            setMarker(playerMarker[currentPlayer], row, col);
        }
        print("P" + (currentPlayer + 1) + " attack at " + row + " " + col);
        for (int i = 0; i < 2; i++)
        {
            if (i != currentPlayer)
            {
                if (Mathf.Abs(playerMarker[i].row - row) <= cardinfo.radius[id] && Mathf.Abs(playerMarker[i].col - col) <= cardinfo.radius[id])
                {
                    print("hit player " + (i + 1));
                    //hp[i] -= cardinfo.atk[id];
                    DecreaseHP(i, cardinfo.atk[id]);
                    print("player " + (i + 1) + " hps = " + hps[i]);
                    CalculateKnock(id, i);
                }
            }

        }

        EndAttackPhase();
    }

    public void CalculateKnock(int id, int playerID)
    {
        int x = playerMarker[playerID].col - playerMarker[currentPlayer].col;
        int y = playerMarker[playerID].row - playerMarker[currentPlayer].row;
        if (y < 0)
        {
            int newy = playerMarker[playerID].row - cardinfo.knock[id];
            if (newy < 0)
            {
                newy = 0;
                knock[playerID] = true;
                knockText.SetActive(true);
            }
            setMarker(playerMarker[playerID], newy, playerMarker[playerID].col);
        }
        else if (y > 0)
        {
            int newy = playerMarker[playerID].row + cardinfo.knock[id];
            if (newy > 5)
            {
                newy = 5;
                knock[playerID] = true;
                knockText.SetActive(true);
            }
            setMarker(playerMarker[playerID], newy, playerMarker[playerID].col);
        }
        else if (x < 0)
        {
            int newx = playerMarker[playerID].col - cardinfo.knock[id];
            if (newx < 0)
            {
                newx = 0;
                knock[playerID] = true;
                knockText.SetActive(true);
            }
            setMarker(playerMarker[playerID], playerMarker[playerID].row, newx);
        }
        else
        {
            int newx = playerMarker[playerID].col + cardinfo.knock[id];
            if (newx > 7)
            {
                newx = 7;
                knock[playerID] = true;
                knockText.SetActive(true);
            }
            setMarker(playerMarker[playerID], playerMarker[playerID].row, newx);
        }

    }

    public void EndAttackPhase()
    {
        foreach (GridSpace cell in board)
        {
            cell.GetComponent<Button>().interactable = false;
        }
        player[0].EnableHand();
        skipButton.Enable();
        //isMoving = false;
        NextPhase();
    }

    public void Counter(int cardID)
    {
        print("Counter to P" + (attacker + 1) + "!");
    }
    public void UpdatePhaseText()
    {
        if (currentPlayer == 0)
        {
            if (currentPhase == DRAW)
            {
                phaseText.text = "DRAW PHASE";
            }
            else if (currentPhase == MOVE)
            {
                phaseText.text = "MOVE PHASE";
            }
            else if (currentPhase == ATTACK)
            {
                phaseText.text = "ATTACK PHASE";
            }
            else if (currentPhase == COUNTER)
            {
                phaseText.text = "COUNTER PHASE";
            }
            else if (currentPhase == END)
            {
                phaseText.text = "END PHASE";
            }
            else
            {
                phaseText.text = "????????";
            }
        }
        else
        {
            phaseText.text = "P" + (currentPlayer + 1) + "'s Turn";
        }

    }

    public void ShowPreview(Sprite spr)
    {
        previewPanel.SetActive(true);
        previewPanel.GetComponent<Image>().sprite = spr;
        //print("mouse entered");
    }
    public void HidePreview()
    {
        previewPanel.SetActive(false);
        //print("mouse exited");
    }

    public void StartBotTurn()
    {
        print("Player " + (currentPlayer + 1) + "'s turn");
        currentPhase = 0;
        UpdatePhaseText();
        //Wait();
        
        //player[currentPlayer].EnableHand();
        //NextPhase();
        //PerformAction(botCard[currentPlayer][0]);
        int offsetRow = Random.Range(-4, 4);
        int offsetCol = Random.Range(-4 + Mathf.Abs(offsetRow), 4 - Mathf.Abs(offsetRow));
        print("random " + offsetRow + " , " + offsetCol);
        if (!knock[currentPlayer])
        {
            setMarker(playerMarker[currentPlayer], playerMarker[currentPlayer].row + offsetRow, playerMarker[currentPlayer].col + offsetCol);

        }
        else
        {
            print("Wall rush!");
            knock[currentPlayer] = false;
            knockText.SetActive(false);
        }

        //NextPhase();
        //PerformAction();
        //NextPhase();
        EndTurn();

    }

    public void setMarker(PlayerMarker marker, int i, int j)
    {
        if (i < 0) i = 0;
        else if (i > 5) i = 5;
        if (j < 0) j = 0;
        else if (j > 7) j = 7;
        marker.SetPos(i, j);
        
    }

    //IEnumerator Wait()
    //{
    //    yield return new WaitForSeconds(3);
    //}
    //public void WalkPhase(int handIndex)
    //{
    //    if (handIndex > hand.Count) return;
    //    if (handIndex != -1)//discard 1 card for walk
    //    {
    //        hand.RemoveAt(handIndex);
    //    }
    //}
    public void hitbox(int id)
    {
        int pRow = playerMarker[currentPlayer].row;
        int pCol = playerMarker[currentPlayer].col;
        foreach (GridSpace cell in board)
        {
            if (Mathf.Abs(cell.row - pRow) + Mathf.Abs(cell.col - pCol) <= cardinfo.range[id])
            {
                cell.GetComponent<Button>().interactable = true;
            }
        }


    }
    public void DecreaseHP(int playerID, int amount)
    {
        print("amount = "+amount);
        int newHP = playerHP[playerID].hp - amount;
        if (newHP < 0)
        {
            newHP = 0;
        }
        print("newHP = "+newHP);
        hps[playerID] = newHP;
        playerHP[playerID].setHP(newHP);

        if (playerHP[playerID].isDead())
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        gameOverText.SetActive(true);
        player[0].DisableHand();
        skipButton.Disable();
        foreach (GridSpace cell in board)
        {
            cell.GetComponent<Button>().interactable = false;
        }
    }

}


/*
switch (id)
        {
            case (1):
                { 
                    foreach (GridSpace cell in board)
                    {
                        if ((Mathf.Abs(cell.row - pRow) <= 1 && cell.col == pCol) || (Mathf.Abs(cell.col - pCol) <= 1 && cell.row == pRow))
                        {
                            cell.GetComponent<Button>().interactable = true;
                        }
                    }
                }
                break;
            case (2):
                { 
                    foreach (GridSpace cell in board)
                    {
                        if (Mathf.Abs(cell.row - pRow) <= 1 && Mathf.Abs(cell.col - pCol) <= 1)
                        {
                            cell.GetComponent<Button>().interactable = true;
                        }
                    }
                }
                break;
                
            case (6):
                {
                    foreach (GridSpace cell in board)
                    {
                        if ((Mathf.Abs(cell.row - pRow) <= 2 && cell.col == pCol) || (Mathf.Abs(cell.col - pCol) <= 2 && cell.row == pRow))
                        {
                            cell.GetComponent<Button>().interactable = true;
                        }
                    }
                }
                break;
                
            case (8):
                {
                    foreach (GridSpace cell in board)
                    {
                        if ((Mathf.Abs(cell.row - pRow) <= 1 && cell.col == pCol) || (Mathf.Abs(cell.col - pCol) <= 1 && cell.row == pRow))
                        {
                            cell.GetComponent<Button>().interactable = true;
                        }
                    }
                }
                break;
            case (10):
                {
                    foreach (GridSpace cell in board)
                    {
                        if(Mathf.Abs(cell.row - pRow) + Mathf.Abs(cell.col - pCol) <= 2)
                        {
                            cell.GetComponent<Button>().interactable = true;
                        }
                    }
                }
                break;
            case (11):
                {
                    foreach (GridSpace cell in board)
                    {
                        if ((Mathf.Abs(cell.row - pRow) <= 4 && cell.col == pCol) || (Mathf.Abs(cell.col - pCol) <= 4 && cell.row == pRow))
                        {
                            cell.GetComponent<Button>().interactable = true;
                        }
                    }
                }
                break;
                
            case (12):
                {
                    foreach (GridSpace cell in board)
                    {
                        if (Mathf.Abs(cell.row - pRow) + Mathf.Abs(cell.col - pCol) <= 3)
                        {
                            cell.GetComponent<Button>().interactable = true;
                        }
                    }
                }
                break;
            case (13):lolhi
                {
                    foreach (GridSpace cell in board)
                    {
                        if (Mathf.Abs(cell.row - pRow) + Mathf.Abs(cell.col - pCol) <= 2)
                        {
                            cell.GetComponent<Button>().interactable = true;
                        }
                    }
                }
                break;
            default:
                break;
        }
*/