using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuTransition : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject CharacterMenu;

    public Animator animator;
    public void TransitionToCharacterMenu()
    {
        if(MainMenu.active)
        {
            MainMenu.SetActive(false);
            animator.SetBool("isTransitionning", true);
        }
    }

    public void TransitionToMainMenu()
    {
        if(CharacterMenu.active)
        {
            CharacterMenu.SetActive(false);
            animator.SetBool("isTransitionning", false);
        }
    }

    public void UI_EnableMainMenu()
    {
        MainMenu.SetActive(true);
    }

    public void UI_EnableCharacterMenu()
    {
        CharacterMenu.SetActive(true);
    }
}
