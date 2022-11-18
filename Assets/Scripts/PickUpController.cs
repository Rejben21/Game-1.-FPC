using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpController : MonoBehaviour
{
    private new Rigidbody rigidbody;
    private bool pickedUp;

    public GameObject[] models;

    public enum PickUpType
    {
        Health,
        Bomb
    }

    public PickUpType type;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();

        float randomValue = Random.Range(1, 5);
        rigidbody.velocity = transform.up * randomValue;
        rigidbody.AddTorque(transform.right * randomValue);
        rigidbody.AddTorque(transform.forward * randomValue);

        switch(type)
        {
            case PickUpType.Health:
                models[0].SetActive(true);
                break;

            case PickUpType.Bomb:
                models[1].SetActive(true);
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (type == PickUpType.Health && !pickedUp)
            {
                other.GetComponent<PlayerHealthController>().AddHealth();
                pickedUp = true;
            }

            if (type == PickUpType.Bomb && other.GetComponent<PlayerCombatController>().bombs < 3 && !pickedUp)
            {
                other.GetComponent<PlayerCombatController>().bombs++;
                pickedUp = true;
            }

            Destroy(gameObject);
        }
    }
}
