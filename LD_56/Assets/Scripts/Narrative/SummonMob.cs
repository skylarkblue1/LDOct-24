using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SummonMob : MonoBehaviour
{
    [SerializeField]
    List<GameObject> mobs;
    [SerializeField]
    private bool spawnMobs;
    [SerializeField]
    private bool aggroMobs;
    private bool isActivated = false;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player") && !isActivated) {
            ActivateAllMobs();
        }
    }

    public void ActivateAllMobs() {
        foreach(GameObject mob in mobs) {
            mob.SetActive(spawnMobs);
        }
        if (aggroMobs) AgroEnemy();
        gameObject.SetActive(false);
    }

    public void AgroEnemy() {
        foreach (GameObject mob in mobs) {
            EnemyAI mobAI = mob.GetComponent<EnemyAI>();
            if (!mobAI) { continue; }
            mobAI.aggroPlayer = true;
        }
    }
}
