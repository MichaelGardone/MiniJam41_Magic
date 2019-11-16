using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOE : MonoBehaviour
{
    public float timerMax = 2.0f;

    float timer = 0.0f;

    // Update is called once per frame
    void Update()
    {
        if (timer >= timerMax)
            Destroy(gameObject);

        timer += Time.deltaTime;

        transform.localScale = new Vector3(transform.localScale.x * 1.025f, transform.localScale.y * 1.005f, transform.localScale.z * 1.025f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            // Do things
        }
    }
}
