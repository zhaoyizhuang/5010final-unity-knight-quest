using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeetNPC : MonoBehaviour
{

    public Transform npc;
    private float iRange = 2.5f;
    private bool startConversation = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) {
            if (Vector3.Distance(transform.position, npc.transform.position) < iRange) {
                if (npc.TryGetComponent(out NPCReaction npcReaction)) {
                    startConversation = true;
                    npcReaction.Interact(transform);
                }
            }
        }
        
    }

    public bool Interactable() {
        return Vector3.Distance(transform.position, npc.transform.position) < iRange;
    }

    public bool ConversationStatus() {
        return startConversation;
    }

    public void EndConversation() {
        startConversation = false; 
    }
}
