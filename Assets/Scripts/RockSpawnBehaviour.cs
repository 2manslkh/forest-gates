using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockSpawnBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject rockPrefab;
    private Vector2 spawnPosition;
    void Start()
    {
        spawnPosition = new Vector2(transform.position.x, transform.position.y + 0.5f);
        StartCoroutine("CreateRock");
        Destroy(gameObject, 3.0f);
    }

    // Update is called once per frame
    IEnumerator CreateRock()
    {
        yield return new WaitForSeconds(1.0f);
        var rock = Instantiate(rockPrefab, spawnPosition, Quaternion.identity);

    }
}
