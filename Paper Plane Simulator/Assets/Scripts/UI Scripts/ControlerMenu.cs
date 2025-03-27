using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

//should be optimized 
//should support controller
//need to make scenes to fit all statements
public class ControlerMenu : MonoBehaviour
{
    [Header("Menu 1")]
    public GameObject[] menuPhotos;
    public GameObject[] arrows1;
    public GameObject[] buttons;
    public byte x = 0; // top is 4

    [Header("Menu 2")]
    public GameObject[] inclusionPhotos;
    public GameObject[] arrows2;
    public GameObject[] buttonsInc;
    public bool Inc = false;
    public byte y = 0; //top is 6

    [Header("Fade Out")]

    public Animator canvasanimator;


    void Start()
    {
        DebugMenuSelection();

    }

    void Update()
    {
        if (Inc == false)
        {
            DebugMenuSelection(); 

            if (Input.GetKeyDown(KeyCode.S))
            {
                x+=1;
                if (x > 3)
                {
                    x = 0;
                } 
                DebugMenuSelection();
            }
            else if (Input.GetKeyDown(KeyCode.W))
            {
                x-=1;
                if (x < 0 || x > 254)
                {
                    x = 3; 
                }
                DebugMenuSelection();
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                switch (x)
                {
                    case 0:
                    StartCoroutine(FadeOutAndPlay("Intro"));
                    break;

                    case 1:
                    StartCoroutine(FadeOutAndPlay("Prototype")); //change scene for later
                    break;

                    case 2:
                    foreach (GameObject arrow in arrows1)
                    {
                        arrow.SetActive(false);
                    }
                    foreach (GameObject photos in menuPhotos)
                    {
                        photos.SetActive(false);
                    }
                    foreach (GameObject button in buttons)
                    {
                        button.SetActive(false);
                    }
                    foreach (GameObject button in buttonsInc)
                    {
                        button.SetActive(true);
                    }
                    foreach (GameObject word in buttonsInc)
                    {
                        word.SetActive(true);
                    }
                    Inc = true;
                    break;

                    case 3:
                    #if UNITY_EDITOR
                        EditorApplication.ExitPlaymode(); // Stops play mode in Unity Editor
                    #else
                        Application.Quit(); // Quits the game in a built application
                    #endif
                    break;
                }
            }

        }
        else if (Inc == true)
        {
            DebugIncSelection();

            if (Input.GetKeyDown(KeyCode.S))
            {
                y+=1;
                if (y > 5)
                {
                    y = 0;
                } 
                DebugIncSelection(); 
            }
            else if (Input.GetKeyDown(KeyCode.W))
            {
                y-=1;
                if (y < 0 || y > 254)
                {
                    y = 5; 
                }
                DebugIncSelection(); 
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                switch (y)
                {
                    case 0:
                    StartCoroutine(FadeOutAndPlay("Prototype"));
                    break;

                    case 1:
                    //StartCoroutine(FadeOutAndPlay("Physics"));
                    break;

                    case 2:
                    //StartCoroutine(FadeOutAndPlay("Animation"));
                    break;

                    case 3:
                    //StartCoroutine(FadeOutAndPlay("AI"));
                    break;

                    case 4:
                    //StartCoroutine(FadeOutAndPlay("Game Data"));
                    break;

                    case 5:
                    foreach (GameObject arrow in arrows2)
                    {
                        arrow.SetActive(false);
                    }
                    foreach (GameObject button in buttonsInc)
                    {
                        button.SetActive(false);
                    }
                    foreach (GameObject button in buttons)
                    {
                        button.SetActive(true);
                    }
                    Inc = false;
                    break;
                }
            }

        }
        
    }

    void DebugIncSelection()
    {
        switch (y)
        {
            case 0:
            arrows2[0].SetActive(true);
            arrows2[1].SetActive(false);
            arrows2[2].SetActive(false);
            arrows2[3].SetActive(false);
            arrows2[4].SetActive(false);
            arrows2[5].SetActive(false);

            inclusionPhotos[0].SetActive(true);
            inclusionPhotos[1].SetActive(false);
            inclusionPhotos[2].SetActive(false);
            inclusionPhotos[3].SetActive(false);
            inclusionPhotos[4].SetActive(false);

            break;

            case 1:
            arrows2[0].SetActive(false);
            arrows2[1].SetActive(true);
            arrows2[2].SetActive(false);
            arrows2[3].SetActive(false);
            arrows2[4].SetActive(false);
            arrows2[5].SetActive(false);

            inclusionPhotos[0].SetActive(false);
            inclusionPhotos[1].SetActive(true);
            inclusionPhotos[2].SetActive(false);
            inclusionPhotos[3].SetActive(false);
            inclusionPhotos[4].SetActive(false);
            break;

            case 2:
            arrows2[0].SetActive(false);
            arrows2[1].SetActive(false);
            arrows2[2].SetActive(true);
            arrows2[3].SetActive(false);
            arrows2[4].SetActive(false);
            arrows2[5].SetActive(false);

            inclusionPhotos[0].SetActive(false);
            inclusionPhotos[1].SetActive(false);
            inclusionPhotos[2].SetActive(true);
            inclusionPhotos[3].SetActive(false);
            inclusionPhotos[4].SetActive(false);
            break;

            case 3:
            arrows2[0].SetActive(false);
            arrows2[1].SetActive(false);
            arrows2[2].SetActive(false);
            arrows2[3].SetActive(true);
            arrows2[4].SetActive(false);
            arrows2[5].SetActive(false);

            inclusionPhotos[0].SetActive(false);
            inclusionPhotos[1].SetActive(false);
            inclusionPhotos[2].SetActive(false);
            inclusionPhotos[3].SetActive(true);
            inclusionPhotos[4].SetActive(false);
            break;

            case 4:
            arrows2[0].SetActive(false);
            arrows2[1].SetActive(false);
            arrows2[2].SetActive(false);
            arrows2[3].SetActive(false);
            arrows2[4].SetActive(true);
            arrows2[5].SetActive(false);

            inclusionPhotos[0].SetActive(false);
            inclusionPhotos[1].SetActive(false);
            inclusionPhotos[2].SetActive(false);
            inclusionPhotos[3].SetActive(false);
            inclusionPhotos[4].SetActive(true);
            break;

            case 5:
            arrows2[0].SetActive(false);
            arrows2[1].SetActive(false);
            arrows2[2].SetActive(false);
            arrows2[3].SetActive(false);
            arrows2[4].SetActive(false);
            arrows2[5].SetActive(true);

            inclusionPhotos[0].SetActive(false);
            inclusionPhotos[1].SetActive(false);
            inclusionPhotos[2].SetActive(false);
            inclusionPhotos[3].SetActive(false);
            inclusionPhotos[4].SetActive(false);
            break;

        }
    }

    void DebugMenuSelection()
    {
        switch (x)
        {
            case 0:
            arrows1[0].SetActive(true);
            arrows1[1].SetActive(false);
            arrows1[2].SetActive(false);
            arrows1[3].SetActive(false);

            menuPhotos[0].SetActive(true);
            menuPhotos[1].SetActive(false);
            menuPhotos[2].SetActive(false);
            break;

            case 1:
            arrows1[0].SetActive(false);
            arrows1[1].SetActive(true);
            arrows1[2].SetActive(false);
            arrows1[3].SetActive(false);

            menuPhotos[0].SetActive(false);
            menuPhotos[1].SetActive(true);
            menuPhotos[2].SetActive(false);
            break;

            case 2:
            arrows1[0].SetActive(false);
            arrows1[1].SetActive(false);
            arrows1[2].SetActive(true);
            arrows1[3].SetActive(false);

            menuPhotos[0].SetActive(false);
            menuPhotos[1].SetActive(false);
            menuPhotos[2].SetActive(true);
            break;

            case 3:
            arrows1[0].SetActive(false);
            arrows1[1].SetActive(false);
            arrows1[2].SetActive(false);
            arrows1[3].SetActive(true);

            menuPhotos[0].SetActive(false);
            menuPhotos[1].SetActive(false);
            menuPhotos[2].SetActive(false);
            break;
        }

    }

    //after animation (parameter trigger "Into") has played load scene by taking in scene name from switch statements 
    private IEnumerator FadeOutAndPlay(string scenename)
    {
        if (canvasanimator != null)
        {
            canvasanimator.SetTrigger("Into"); 
            yield return new WaitForSeconds(canvasanimator.GetCurrentAnimatorStateInfo(0).length + 2f);
        }

        SceneManager.LoadScene(scenename);
    }
}
