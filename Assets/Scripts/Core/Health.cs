using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.Core
{
    public class Health : MonoBehaviour 
    {
        public float healthPoints = 100f;

        bool isDead = false;
        bool getHit = false;

        public bool IsDead()
        {
            return isDead;
        }

        public void SetGetHit(bool value)
        {
            getHit = value;
        }

        public bool IsGetHit()
        {
            return getHit;
        }

        public void TakeDamage(float damage)
        {
            getHit = true;
            healthPoints = Mathf.Max(healthPoints - damage, 0);
            if (healthPoints == 0)
            {
                Die();
                if (this.gameObject.CompareTag("Player"))
                {
                    StartCoroutine(respawn());
                }
            }
        }

        private void Die()
        {
            if (isDead) return;
            //! comment to attack corps
            isDead = true;
            GetComponent<Animator>().SetTrigger("die");
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        IEnumerator respawn()
        {
            yield return new WaitForSeconds(3);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    
}