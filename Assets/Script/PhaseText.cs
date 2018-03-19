using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PhaseText : MonoBehaviour {
    private GameController gameController;
    public Text text;
    public void Initialize(GameController controller)
    {
        gameController = controller;
  
    }
}
