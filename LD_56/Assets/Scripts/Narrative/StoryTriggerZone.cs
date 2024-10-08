using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryTriggerZone : MonoBehaviour
{
    [SerializeField]
    private StoryText_SO storyText;
    [SerializeField]
    private NarrativeTextController narrativeBox;
    [SerializeField]
    [Tooltip("How long the text stays up before it disappears")]
    private float lifespan;

    [Tooltip("Can this text pop up again?")]
    public bool Reactivateable;

    private bool isActivated = false;

    private GameObject[] enemies;
    private GameObject[] players;

    private float[] originalSpeeds;

    // Start is called before the first frame update
   private void OnEnable() {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        players = GameObject.FindGameObjectsWithTag("Player");
        originalSpeeds = new float[players.Length];
        isActivated = false;
   }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player") && !isActivated) {
            TriggerStory();
        }
    }

    public void TriggerStory() {
        isActivated = true;
        StartCoroutine(ProcessText());
        SendMessageUpwards("ToggleZones", this, SendMessageOptions.DontRequireReceiver);
    }

    private IEnumerator ProcessText() {
        setDisableEnemiesAI(true);
        setFreezePlayer(true);
        string currentText = "";
        foreach (string text in storyText.texts)
        {
            currentText = text;
            narrativeBox.DisplayText(text);
            yield return new WaitForSeconds(lifespan);
        }
        yield return null;
        StartCoroutine(ProcessLifespan(currentText));
    }

    private IEnumerator ProcessLifespan(string text) {
        yield return null;
        setDisableEnemiesAI(false);
        setFreezePlayer(false);
        narrativeBox.TryDisableTextBox(text);
        if (Reactivateable) {
            isActivated = false;
        } else  {
            gameObject.SetActive(false);
        }
    }

    private void setDisableEnemiesAI(bool disable)
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject obj in enemies)
        {
            EnemyAI ai = obj.GetComponent<EnemyAI>();
            if (ai != null)
                ai.disableAI = disable;
        }
    }

    private void setFreezePlayer(bool freeze)
    {
        int count = 0;
        foreach(GameObject obj in players)
        {
            FirstPersonMovement move = obj.GetComponent<FirstPersonMovement>();
            if (move != null)
            {
                float ogSpeed = move.moveSpeed;
                move.moveSpeed = freeze ? 0 : originalSpeeds[count];

                originalSpeeds[count] = ogSpeed;
                count++;
            }
            PlayerAttack atk = obj.GetComponent<PlayerAttack>();
            if (atk != null)
            {
                atk.enabled = !freeze;
            }
        }
    }
}