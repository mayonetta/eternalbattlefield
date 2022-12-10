using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int BulletMovementSpeed;

    Verorong VerorongComponent;

    float Chronometre;

    void Awake()
    {
        VerorongComponent = GameObject.FindGameObjectWithTag("VERORONG").GetComponent<Verorong>();
    }

    void Start()
    {
        this.transform.up = VerorongComponent.TargetLocation;
    }

    void Update()
    {
        Chronometre += Time.deltaTime;

        this.transform.position = Vector3.MoveTowards(this.transform.position, VerorongComponent.TargetLocation, BulletMovementSpeed * Time.deltaTime);

        if (Chronometre >= 1.0f)
        {
            Destroy(this.gameObject);
        }
    }
}
