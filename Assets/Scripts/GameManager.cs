using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] NetworkManager networkManager;

    public void GotNetworkMessage (string message)
    {
        print("message recieved: " + message);

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

        Game.Instance.myTurn = true;
    }

}