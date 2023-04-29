using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class chase : MonoBehaviour
{
    public float distance;
    public Transform Enemy;
    public Transform Player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(Player.position, Enemy.position);
        if (distance < 1.5)
        {
            Debug.Log("catch!");
            SceneManager.LoadScene(3);
        }
    }
}
