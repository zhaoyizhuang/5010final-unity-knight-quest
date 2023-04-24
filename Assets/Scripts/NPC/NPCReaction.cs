using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NPCReaction : MonoBehaviour
{
    [SerializeField] private MeetNPC meetNpc;
    [SerializeField] private Reputation reputation;
    public Text npcName;
    public Text currentMsg;
    public int nextScene;
    public bool finalScene = false;

    private Animator animator;
    private Queue<string> msgQ;
    private string name;

    void Start() {
        name = "Mike";
        msgQ = new Queue<string>();
        PopulateMsg();
    }

    private void FirstTalk()
    {
        msgQ.Enqueue("Hi, I'm Mike. I guess you're the knight here to help us. Thank you for that!");
        msgQ.Enqueue("Before we fight the rogues, I need to make sure you're strong enough.");
        msgQ.Enqueue("Please go catch a thief.");
    }

    private void SecondTalk()
    {
        msgQ.Enqueue("Thank you for catching the thief!");
        msgQ.Enqueue("You've proven that you're strong enough to take on the rogues.");
        msgQ.Enqueue("Now go get them.");
    }

    private void FinalTalk()
    {
        msgQ.Enqueue("Thank you so much for your help! ");
        msgQ.Enqueue("You're the true hero of the village.");
    }

    private void PopulateMsg() {
        Debug.Log(reputation.reputationLevel);
        msgQ.Clear();
        if (reputation.reputationLevel == 0) {
            FirstTalk();
        } else if (reputation.reputationLevel == 1) {
            SecondTalk();
        } else {
            FinalTalk();
        }        
    }

    public void Interact(Transform transform) {
        PopulateMsg();
        NextMsg();
    }

    IEnumerator TypeMsg (string msg) {
        currentMsg.text = "";
        foreach (char c in msg.ToCharArray())
        {
            currentMsg.text += c;
            yield return null;
        }
    }

    IEnumerator SwitchScene(int scene) {
        yield return SceneManager.LoadSceneAsync(scene);
    }

    public void NextMsg() {
        if (msgQ.Count == 0) {
            meetNpc.EndConversation();
            // if (reputation.reputationLevel == 0) {
            //     StartCoroutine(SwitchScene(1));
            // } else if (reputation.reputationLevel == 1) {
            //     StartCoroutine(SwitchScene(0));
            // }
            if (finalScene) {
                return;
            }
            StartCoroutine(SwitchScene(nextScene));
        } else {
            npcName.text = name;
            StopAllCoroutines();
            StartCoroutine(TypeMsg(msgQ.Dequeue()));
        }
    }
}
