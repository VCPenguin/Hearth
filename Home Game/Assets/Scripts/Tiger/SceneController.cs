using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace UnityStandardAssets.ImageEffects
{
    public class SceneController : MonoBehaviour
    {
        public static SceneController instance;

        [Header("Scene Controller")]
        public string SceneName;

        [Header("Main Menu Items")]
        public GameObject SplashScreen;
        public GameObject MenuLookPoint;
        public GameObject Cam;
        public GameObject Menu;
        public GameObject CreditsGroup;
        public GameObject ControlsGroup;

        [Header("Game Scene Items")]
        public GameObject PauseMenu;
        public GameObject DeathMenu;

        public InputController inputController;

        public bool Paused;
        public bool Dead;

        float tempPlayerMovespeed;

        // Use this for initialization
        void Start()
        {
            Scene CurrentScene = SceneManager.GetActiveScene();
            SceneName = CurrentScene.name;

            //  CreditsGroup.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            //If you are in the game
            if (PlayerController.instance == null)
            {
                MainMenu();

                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    Pause();
                }

                if (inputController.aButtonDown == 1)
                {
                    NewGame();
                }

                if (inputController.bButtonDown == 1)
                {
                    //If credits is open
                    if (CreditsGroup.activeSelf == true)
                    {
                        CloseCredits();
                    }
                    //If controls are open
                    else if (ControlsGroup.activeSelf == true)
                    {
                        CloseControls();
                    }
                    //If nothing is open
                    else
                    {
                        QuitGame();
                    }
                }

                if (inputController.xButtonDown == 1)
                {
                    //If credits is open
                    if (CreditsGroup.activeSelf == false && ControlsGroup.activeSelf == false)
                    {
                        Credits();
                    }
                }

                if (inputController.yButtonDown == 1)
                {
                    //If controls is open
                    if (CreditsGroup.activeSelf == false && ControlsGroup.activeSelf == false)
                    {
                        Controls();
                    }
                }

                if (inputController.aButtonDown == 1)
                {
                    //If controls is open
                    if (CreditsGroup.activeSelf == false && ControlsGroup.activeSelf == false)
                    {
                        NewGame();
                    }
                }

                
            }
            else
            {
                if (inputController.startButtonDown == 1)
                {

                    if (Dead)
                    {
                        QuitMenu();
                    }
                    else if (Paused == false)
                    {
                        Pause();
                    }
                    else
                    {
                        ContinueGame();
                    }

                }

                if (inputController.bButtonDown == 1)
                {

                    if (Paused == false)
                    {
                        //Pause();
                    }
                    else
                    {
                        QuitMenu();
                    }

                }

                if (inputController.yButtonDown == 1)
                {

                    if (Paused == false)
                    {
                        //Pause();
                    }
                    else
                    {
                        PlayerController.instance.inputController.invertY = !PlayerController.instance.inputController.invertY;

                        PlayerPrefs.SetInt("invertY", PlayerController.instance.inputController.invertY == false ? 0 : 1);
                    }
                }
            }
        }

        public void MainMenu()
        {
            if (SceneName == "Menu")
            {
                if (Input.anyKey)
                {
                    DepthOfField.instance.focalTransform = MenuLookPoint.transform;
                }
            }
        }

        public void QuitGame()
        {
            Application.Quit();
            print("Quit");
        }

        public void Options()
        {
            print("Options");
        }

        public void Credits()
        {
            CreditsGroup.SetActive(true);
            Menu.SetActive(false);
        }

        public void CloseCredits()
        {
            CreditsGroup.SetActive(false);
            Menu.SetActive(true);
        }

        public void Controls()
        {
            ControlsGroup.SetActive(true);
            Menu.SetActive(false);
        }

        public void CloseControls()
        {
            ControlsGroup.SetActive(false);
            Menu.SetActive(true);
        }

        public void NewGame()
        {
            Application.LoadLevel("NewWorld");
        }

        public void Pause()
        {
            Paused = true;
            PauseMenu.SetActive(true);
            Time.timeScale = 0.5f;
        }

        public void ContinueGame()
        {
            Paused = false;
            Time.timeScale = 1;
            PauseMenu.SetActive(false);
        }

        public void ActivateDeadMenu()
        {
            Dead = true;
            Time.timeScale = 0.1f;
            DeathMenu.SetActive(true);
        }

        public void QuitMenu()
        {
            Time.timeScale = 1;

            if(DeathMenu.activeSelf == true)
            {
                //DeathMenu.SetActive(false);
            }
            SceneManager.LoadScene("Menu");
        }
    }
}
