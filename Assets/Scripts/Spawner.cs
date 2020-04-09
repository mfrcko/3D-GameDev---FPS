using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] targets;
    private int numberOfTargets;
    private int maxNumberOfTargets;
    [SerializeField]
    private float spawnTime;
    [SerializeField]
    private int spawnRadius;
    
    
    // Start is called before the first frame update
    void Start()
    {
        numberOfTargets = 0;
        maxNumberOfTargets = 5;
        spawnTime = 2f;
        spawnRadius = 10;
    }

    // Update is called once per frame
    void Update()
    {
        if (numberOfTargets < maxNumberOfTargets) {
            StartCoroutine(Spawn());
        }
    }

    IEnumerator Spawn() {
        int randIndex = Random.Range(0, 2);
        Vector2 circle = Random.insideUnitCircle * spawnRadius;
        Vector3 pos = new Vector3(transform.position.x + circle.x, 2, transform.position.z + circle.y);
        GameObject target = Instantiate(targets[randIndex], pos, Quaternion.identity);
        numberOfTargets++;
        if (target.tag == Tags.PERSON) {
            StartCoroutine(DestroyPerson(target));
        }
        yield return new WaitForSeconds(spawnTime);
    }

    public void TargetKilled() {
        numberOfTargets--;
    }

    IEnumerator DestroyPerson(GameObject person) {
        Destroy(person, 3f);
        yield return new WaitForSeconds(3f);
        numberOfTargets--;
    }
}
