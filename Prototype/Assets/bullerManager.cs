using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class bullerManager : MonoBehaviour
{

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Debug.LogWarning(Score.Instance.score);
            Score.Instance.AddScore();
            Destroy(gameObject);  // Destroy the bullet when it collides with a wall
        }
    }
}
