using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ChangesScene : MonoBehaviour
{
    private Animator animator;
    public int levelToload;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void FadeToLevel()
    {
        animator.SetTrigger("Fade");
    }
    public void OnFadeComplete()
    {
        SceneManager.LoadScene(levelToload);
    }
}
