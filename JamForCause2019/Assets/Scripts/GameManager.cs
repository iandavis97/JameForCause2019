﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject playerObject;
    private PlayerMovement player;//reference to script
    public List<GameObject> objectSwapList = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        player = playerObject.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        SwapObjects();
    }

    private void SwapObjects()
    {
        //going through list of objects that can be swapped
        foreach (GameObject g in objectSwapList)
        {
            //if dark player is active, turn off light objects
            if (player.isPlayer1 && g.gameObject.tag == "LightObject")
            {
                g.SetActive(false);
            }
            //if dark player is active, turn on dark objects
            if (player.isPlayer1 && g.gameObject.tag == "DarkObject")
            {
                g.SetActive(true);
            }

            //if light player is active, turn off dark objects
            if (player.isPlayer2 && g.gameObject.tag == "DarkObject")
            {
                g.SetActive(false);
            }
            //if dark player is active, turn on light objects
            if (player.isPlayer2 && g.gameObject.tag == "LightObject")
            {
                g.SetActive(true);
            }
        }
    }
}