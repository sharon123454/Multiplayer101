using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    [SerializeField] NetworkManager NetworkManager;

    [SerializeField] int myLoc;
    bool empty = true;

    public void TurnEnd()   //connected to all the map buttons
    {
        if (Game.Instance.myTurn && empty)
        {
            string messageToSend;

            if (Game.Instance.IamX)
                messageToSend = "g";
            else
                messageToSend = "r";

            messageToSend += myLoc.ToString();

            Game.Instance.myTurn = false;

            UpdateBoard(messageToSend);
            NetworkManager.ForwardMessage(messageToSend);
        }
    }

    void UpdateBoard(string message)
    {
        switch (message[0])
        {
            case 'r':
                Instantiate(Game.Instance.Red, Game.Instance.spawnPoints[int.Parse(message[1].ToString())], false);
                break;
            case 'g':
                Instantiate(Game.Instance.Green, Game.Instance.spawnPoints[int.Parse(message[1].ToString())], false);
                break;
            default:
                break;
        }
    }

}