using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{

    public float speed = 5f;

    public float duration = 6.0f;

    public Vector3 direction;

    float timer = 0.0f;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= duration)
            Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        transform.position += direction * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {

        }

        Destroy(gameObject);
    }
}
