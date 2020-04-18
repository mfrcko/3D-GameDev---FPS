using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private UIManager uiManager;
    private int score;
    private int damage;

    void Start()
    {
        score = 0;
        damage = 20;
    }

    public void TargetHit(GameObject t) {
        Target target = t.GetComponent<Target>();
        target.health -= damage;
        if (target.health < 0) {
            TargetKilled(t);
        }
    }

    private void TargetKilled(GameObject t) {
        score += t.GetComponent<Target>().point;
        Destroy(t);
        uiManager.UpdateScore(score);
    }

    public int GetScore() {
        return this.score;
    }
}
