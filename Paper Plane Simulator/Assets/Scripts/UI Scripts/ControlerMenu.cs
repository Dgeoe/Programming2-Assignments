using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

#if UNITY_EDITOR
using UnityEditor;
#endif

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

    private Menu_Input_Actions inputActions;
    private InputAction Move;
    private InputAction MoveUp;
    private InputAction Select;

    private void Awake()
    {
        inputActions = new Menu_Input_Actions();
    }

    private void OnEnable()
    {
        Move = inputActions.Menu.Move;
        Move.Enable();
        MoveUp = inputActions.Menu.MoveUp;
        MoveUp.Enable();
        Select = inputActions.Menu.Select;
        Select.Enable();
    }

    private void OnDisable()
    {
        Move.Disable();   
        MoveUp.Disable();
        Select.Disable();   
    }

    void Start()
    {
        DebugMenuSelection();

    }

    void Update()
    {
        if (Inc == false)
        {
            DebugMenuSelection(); 

            if (Move.triggered)
            {
                x+=1;
                if (x > 3)
                {
                    x = 0;
                } 
                DebugMenuSelection();
            }
            else if (MoveUp.triggered)
            {
                x-=1;
                if (x < 0 || x > 254)
                {
                    x = 3; 
                }
                DebugMenuSelection();
            }
            else if (Select.triggered)
            {
                switch (x)
                {
                    case 0:
                    StartCoroutine(FadeOutAndPlay("Intro"));
                    break;

                    case 1:
                    //needs scene made
                    //StartCoroutine(FadeOutAndPlay("Prototype")); //change scene for later
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

            if (Move.triggered)
            {
                y+=1;
                if (y > 5)
                {
                    y = 0;
                } 
                DebugIncSelection(); 
            }
            else if (MoveUp.triggered)
            {
                y-=1;
                if (y < 0 || y > 254)
                {
                    y = 5; 
                }
                DebugIncSelection(); 
            }
            else if (Select.triggered)
            {
                switch (y)
                {
                    case 0:
                    StartCoroutine(FadeOutAndPlay("Proto 1"));
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
            EnableOnly(arrows2, 0);
            EnableOnly(inclusionPhotos, 0);
            break;

            case 1:
            EnableOnly(arrows2, 1);
            EnableOnly(inclusionPhotos, 1);
            break;

            case 2:
            EnableOnly(arrows2, 2);
            EnableOnly(inclusionPhotos, 2);
            break;

            case 3:
            EnableOnly(arrows2, 3);
            EnableOnly(inclusionPhotos, 3);
            break;

            case 4:
            EnableOnly(arrows2, 4);
            EnableOnly(inclusionPhotos, 4);
            break;

            case 5:
            EnableOnly(arrows2, 5);

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
            EnableOnly(arrows1, 0);
            EnableOnly(menuPhotos, 0);
            break;

            case 1:
            EnableOnly(arrows1, 1);
            EnableOnly(menuPhotos, 1);
            break;

            case 2:
            EnableOnly(arrows1, 2);
            EnableOnly(menuPhotos, 2);
            break;

            case 3:
            EnableOnly(arrows1, 3);

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

    void EnableOnly(GameObject[] objects, int indexToEnable)
    {
        for (int i = 0; i < objects.Length; i++)
            objects[i].SetActive(i == indexToEnable); 
    }


}
