using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public static Game Instance;

    [SerializeField] Image ButtonImage;
    public Transform[] spawnPoints;
    public GameObject Green;
    public GameObject Red;

    public bool myTurn = false;
    public bool IamX = false;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        ButtonImage.color = Color.red;
    }

    //For UI button
    public void ToggleTurn()
    {
        myTurn = !myTurn;

        if (myTurn)
        {
            ButtonImage.color = Color.green;
            IamX = true;
        }
        else
        {
            ButtonImage.color = Color.red;
            IamX = false;
        }
    }

}