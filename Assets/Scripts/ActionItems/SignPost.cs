using UnityEngine;
using System.Collections;

public class SignPost : NPC
{
    public string[] dialog;
    public override void Interact()
    {
        DialogueSystem.Instance.AddNewDialogue(dialog, "Signpost");
        Debug.Log("Interacting with sign post.");
        //AfterInteract();
    }
}
