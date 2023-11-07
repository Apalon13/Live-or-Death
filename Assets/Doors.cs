using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Doors : MonoBehaviour
{
    public GameObject spriteDoor;

    public GameObject openDoor;

    public GameObject buttonDoor;

    public bool playerDetect;

    private void Start()
    {   
        playerDetect = false;
    }
    void Update()
    {
        if (playerDetect)
        {
            buttonDoor.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E)) 
            {
                spriteDoor.SetActive(false);
                openDoor.SetActive(true);
            }
        }
        else
        {
            spriteDoor.SetActive(true);
            buttonDoor.SetActive(false);
            openDoor.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerDetect = true;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerDetect = false;
        }

    }

}
