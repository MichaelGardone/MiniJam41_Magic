using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : Magic
{
    Bauble bauble;

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
        if(other.gameObject.layer != LayerMask.NameToLayer("Ignore Raycast"))
        {
            if(other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                other.GetComponent<Entity>().ModifyHealth(-damage, owner.gameObject);
            }

            Destroy(gameObject);
        }
    }
}
