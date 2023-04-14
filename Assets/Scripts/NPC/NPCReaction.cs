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

    private void FirstTalk() {
        msgQ.Enqueue("Hi, I am Mike. Guess you are the knight here to help us. Thanks for that!");
        msgQ.Enqueue("Before fighting with rogues, you have to prove yourself by finising a small task first.");
        msgQ.Enqueue("A thief is wanted, please go get him.");
    }

    private void SecondTalk() {
        msgQ.Enqueue("Thank you for catching the thief!");
        msgQ.Enqueue("You have proved that you are strong enough to fight against those rogues.");
        msgQ.Enqueue("Go get them.");
    }

    private void FinalTalk() {
        msgQ.Enqueue("Thank you so much for your help!");
        msgQ.Enqueue("You are the real hero of the village.");
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
