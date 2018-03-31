using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ScrollbarIncrementer : MonoBehaviour
{
    public Scrollbar Target;
    public Button TheOtherButton;
    public float Step = 0.1f;

    public void Increment()
    {
        if (Target == null || TheOtherButton == null) throw new Exception("Setup ScrollbarIncrementer first!");
        Target.value = Mathf.Clamp(Target.value + Step, 0, 1);
        GetComponent<Button>().interactable = Target.value != 1;
        TheOtherButton.interactable = true;
    }

    public void Decrement()
    {
        if (Target == null || TheOtherButton == null) throw new Exception("Setup ScrollbarIncrementer first!");
        Target.value = Mathf.Clamp(Target.value - Step, 0, 1);
        GetComponent<Button>().interactable = Target.value != 0; ;
        TheOtherButton.interactable = true;
    }
}
