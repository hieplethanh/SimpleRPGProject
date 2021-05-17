using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Interactable
{
    public string[] dialogs;
    public string[] postDialogs;
    public GameObject player;
    public string name;
    bool hasTalked = false;
    public override void Interact()
    {
        playerAgent.isStopped = true; //stop before too near. Social distancing.
        playerAgent.isStopped = false;
        transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
        if (!hasTalked)
        {
            DialogSystem.Instance.AddNewDialog(dialogs, name);
            hasTalked = true;
        }
        else
        {
            DialogSystem.Instance.AddNewDialog(postDialogs, name);
        }
    }
}
