using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float initialBulletSpeed = 80f;
    public float delayBetweenShots = 5f;
    public float speedIncreaseRate = 0.1f;
    public AudioClip shootSound;

    private float nextShootTime;
    private float currentBulletSpeed;
    private AudioSource audioSource;


    void Start()
    {
        nextShootTime = Time.time;
        currentBulletSpeed = initialBulletSpeed;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Time.time >= nextShootTime)
        {
            List<GameObject> shooters = GetRandomShooters();

            foreach (GameObject shooter in shooters)
            {
                GameObject bullet = Instantiate(bulletPrefab, shooter.transform.position, shooter.transform.rotation);
                Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
                bulletRigidbody.velocity = shooter.transform.forward * currentBulletSpeed;

                PlayShootSound();

                delayBetweenShots = Mathf.Max(delayBetweenShots, 1f);
            }

            nextShootTime = Time.time + delayBetweenShots;
            currentBulletSpeed += speedIncreaseRate;
        }
    }

    private List<GameObject> GetRandomShooters()
    {
        // Find all objects with the "Shooter" tag
        GameObject[] allShooters = GameObject.FindGameObjectsWithTag("Shooter");
        List<GameObject> selectedShooters = new List<GameObject>();

        int numberOfShooters = Random.Range(2, Mathf.Min(7, allShooters.Length));

        for (int i = 0; i < numberOfShooters; i++)
        {
            GameObject selectedShooter = allShooters[Random.Range(0, allShooters.Length)];
            if (!selectedShooters.Contains(selectedShooter)) // Ensure we don't select the same shooter more than once
            {
                selectedShooters.Add(selectedShooter);
            }
            else
            {
                i--; // Repeat this iteration to ensure we always select the required number of shooters
            }
        }

        return selectedShooters;
    }

    private void PlayShootSound()
    {
        if (shootSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(shootSound);
        }
    }
}