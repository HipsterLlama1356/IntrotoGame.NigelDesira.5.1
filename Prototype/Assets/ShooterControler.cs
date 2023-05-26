using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterControler : MonoBehaviour
{
    public GameObject circlePrefab;
    private List<GameObject> gameObjects;

    void Start()
    {
        // get all gameobjects with the specific tag
        gameObjects = new List<GameObject>(GameObject.FindGameObjectsWithTag("Shooter"));
    }

    public void ChooseAndExecute()
    {
        if(gameObjects.Count == 0) return;

        // choose a random gameobject
        int index = Random.Range(0, gameObjects.Count);
        GameObject selectedObject = gameObjects[index];

        // start the flash and shoot sequence
        StartCoroutine(FlashAndShoot(selectedObject));
    }

    IEnumerator FlashAndShoot(GameObject gameObject)
    {
        // flash color
        Renderer renderer = gameObject.GetComponent<Renderer>();
        if (renderer != null)
        {
            for (int i = 0; i < 3; i++)
            {
                renderer.material.color = Color.red;
                yield return new WaitForSeconds(1f);
                renderer.material.color = Color.white;
                yield return new WaitForSeconds(1f);
            }
        }

        // shoot circle
        GameObject circle = Instantiate(circlePrefab, gameObject.transform.position, Quaternion.identity);
        Rigidbody2D circleRigidbody = circle.GetComponent<Rigidbody2D>();

        if(circleRigidbody != null)
        {
            circleRigidbody.velocity = new Vector2(10f, 0); // adjust the speed to your needs
        }

        // start a coroutine to handle the circle despawn logic
        StartCoroutine(DespawnOnHit(circle));
    }

    IEnumerator DespawnOnHit(GameObject circle)
    {
        while(true)
        {
            yield return new WaitForEndOfFrame();

            Collider2D hit = Physics2D.OverlapCircle(circle.transform.position, 0.5f, LayerMask.GetMask("ScoreWall"));
            if(hit)
            {
                Destroy(circle);
                break;
            }
        }
    }
}
