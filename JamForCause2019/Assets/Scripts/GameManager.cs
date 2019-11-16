using System.Collections;
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
        GameObject gj = GameObject.Find("level_light");
        int indexcount = gj.transform.childCount;
        for(int i = 0; i < indexcount; i++)
        {
            objectSwapList.Add(gj.transform.GetChild(i).gameObject);
        }
        gj = GameObject.Find("level_dark");
        indexcount = gj.transform.childCount;
        for (int i = 0; i < indexcount; i++)
        {
            objectSwapList.Add(gj.transform.GetChild(i).gameObject);
        }
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
