using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_Manager : MonoBehaviour
{

    public void isFoliage(Animator anim)
    {
        if (anim.GetBool("isFoliage") == false)
        {
            anim.SetBool("isFoliage", true);
        }
        else if (anim.GetBool("isFoliage") == true)
        {
            anim.SetBool("isFoliage", false);
        }
    }

    public void isEnrichment(Animator anim)
    {
        if (anim.GetBool("isEnrichment") == false)
        {
            anim.SetBool("isEnrichment", true);
        }
        else if (anim.GetBool("isEnrichment") == true)
        {
            anim.SetBool("isEnrichment", false);
        }
    }

    public void isEnclosure(Animator anim)
    {
        if (anim.GetBool("isEnclosure") == false)
        {
            anim.SetBool("isEnclosure", true);
        }
        else if (anim.GetBool("isEnclosure") == true)
        {
            anim.SetBool("isEnclosure", false);
        }
    }

    public void isAnimal(Animator anim)
    {
        if (anim.GetBool("isAnimal") == false)
        {
            anim.SetBool("isAnimal", true);
        }
        else if (anim.GetBool("isAnimal") == true)
        {
            anim.SetBool("isAnimal", false);
        }
    }
    // For pauing the game
    public void PausetheGame()
    {
        // Pause/unpause the game
        // If time is not 0
        if (Time.timeScale != 0)
        {
            // Set time to equal zero
            Time.timeScale = 0;
        }
        else
        {
            // Set time tp equal one
            Time.timeScale = 1;
        }

        // Make the game greyscale

    }

    public void EnablePopBoolInAnimator(Animator anim)
    {
        if (anim.GetBool("isHidden") == false)
        {
            anim.SetBool("isHidden", true);
        }
        else if (anim.GetBool("isHidden") == true)
        {
            anim.SetBool("isHidden", false);
        }
    }

    public void EnableClickedInAnimator(Animator anim)
    {
        if (anim.GetBool("isClicked") == false)
        {
            anim.SetBool("isClicked", true);
        }
        else if (anim.GetBool("isClicked") == true)
        {
            anim.SetBool("isClicked", false);
        }
    }

}
