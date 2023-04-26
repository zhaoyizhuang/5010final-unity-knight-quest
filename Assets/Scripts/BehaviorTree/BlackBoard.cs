using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BehaviorTree
{
    public class BlackBoard : MonoBehaviour
    {
        static BlackBoard instance; //Singleton

        public Dictionary<string, float> tick = new Dictionary<string, float>();

        public static BlackBoard Instance
        {
            get
            {
                return instance;
            }
        }

        private void Awake()
        {
            /***
             * Awake is called either when an active GameObject that contains 
             * the script is initialized when a Scene loads, 
             * or when a previously inactive GameObject is set to active, 
             * or after a GameObject created with Object.Instantiate is initialized. 
             * Use Awake to initialize variables or states before the application starts.
             */
            if (instance != null && instance != this)
            {
                //remove duplicates
                Destroy(this.gameObject);
            }
            else
            {
                instance = this;
            }
        }

        private void Update()
        {
            foreach (string key in tick.Keys.ToList())
            {
                tick[key] += Time.deltaTime;
            }
        }
    }
}
