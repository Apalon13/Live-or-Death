using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    public bool detect;
    public GameObject button;
    public GameObject fade;
    public GameObject openDoor;
    public GameObject closeDoor;
    public int level;
    void Update()
    {
        if (detect)
        {
            button.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                fade.GetComponent<Animator>().SetTrigger("Fade");
                fade.GetComponent<ChangesScene>().levelToload = level;
                closeDoor.SetActive(false);
                openDoor.SetActive(true);
            }
        }
        else
        {
            closeDoor.SetActive(true);
            openDoor.SetActive(false);
            button.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            detect = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            detect = false;
        }
    }
}
