using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //All the states that the player can be in.
    //This way, the individual movement scripts can check on these states in case they are affected
    //For example, if the player can't jump and slide at the same time
    private bool[] playerStates = { false, false, false };

    private void Awake()
    {
        SetupPlayerStates();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Obstacles"))
        {
            this.transform.SetParent(null);
            this.GetComponent<MeshRenderer>().enabled = false;
            this.GetComponent<BoxCollider>().enabled = false;
            StartCoroutine(PybUtilityScene.ReloadScene(1f));

        }
    }

    private void SetupPlayerStates()
    {
        int playerStateCount = Enum.GetNames(typeof(PlayerState)).Length;
        playerStates = new bool[playerStateCount];
        for (int i = 0; i < playerStateCount; i++)
        {
            playerStates[i] = false;
        }
    }

    //GETTERS AND SETTERS
    public bool GetState(PlayerState _state)
    {
        return playerStates[(int)_state];
    }
    public void SetState(PlayerState _state, bool _value)
    {
        playerStates[(int)_state] = _value;
    }


}

public enum PlayerState
{
    Jumping,
    Sliding,
    LaneSwitching
}
