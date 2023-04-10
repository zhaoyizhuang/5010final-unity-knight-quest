using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractUI : MonoBehaviour
{
    [SerializeField] private GameObject container;
    [SerializeField] private MeetNPC meetNpc;

    private void Update() {
        if (meetNpc.Interactable()) {
            Show();
        } else {
            Hide();
        }
    }
    
    private void Show() {
        container.SetActive(true);
    }

    private void Hide() {
        container.SetActive(false);
    }
}
