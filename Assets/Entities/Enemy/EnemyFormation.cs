using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFormation : MonoBehaviour {

    public float health = 150;
    public GameObject enemyProjectile;
    public float projectileSpeed = 5f;
    public float shotsPerSeconds = 0.5f;
    public int scoreValue = 150;

    public AudioClip fireSound;
    public AudioClip deathSound;

    private ScoreKeeper scoreKeeper;

    void Start()
    {
        scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Projectile missile = col.gameObject.GetComponent<Projectile>();
        if(missile)
        {
            health -= missile.getDamage();
            missile.Hit();
            if(health <= 0)
            {
                Die();
            }
        }
    }

    void Die()
    {
        AudioSource.PlayClipAtPoint(deathSound, transform.position);
        scoreKeeper.Score(scoreValue);
        Destroy(gameObject); 
    }

    void Update()
    {
        float probability = Time.deltaTime * shotsPerSeconds;
        if(Random.value < probability)
        {
            Fire();
        }
    }

    void Fire()
    {
        GameObject laser = Instantiate(enemyProjectile, transform.position, Quaternion.identity) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
        AudioSource.PlayClipAtPoint(fireSound, transform.position);
    }
}
