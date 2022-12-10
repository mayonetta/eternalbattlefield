using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDeletion : MonoBehaviour
{
    public GameObject DamagePanelInstantiationLocation;
    public GameObject DamagePanel;

    public int Damage;

    BoxCollider boxcollider;

    Vector3 DamagePanelInstantiation()
    {
        Vector3 OriginalInstantiationLocation = DamagePanelInstantiationLocation.transform.position;
        Vector3 InstantiationLocation = boxcollider.size;

        float x = OriginalInstantiationLocation.x + Random.Range(-InstantiationLocation.x / 2.0f, InstantiationLocation.x / 2.0f);
        float y = OriginalInstantiationLocation.y + Random.Range(-InstantiationLocation.y / 2.0f, InstantiationLocation.y / 2.0f);
        float z = OriginalInstantiationLocation.z + Random.Range(-InstantiationLocation.z / 2.0f, InstantiationLocation.z / 2.0f);

        Vector3 DamageInstantiation = new Vector3(x, y, z);

        return DamageInstantiation;
    }

    void Awake()
    {
        boxcollider = DamagePanelInstantiationLocation.GetComponent<BoxCollider>();
    }

    void OnTriggerEnter(Collider CollidedObject)
    {
        if (CollidedObject.gameObject.CompareTag("BULLET"))
        {
            Destroy(CollidedObject.gameObject);

            Damage = Random.Range(0, 11);

            Vector3 DamagePanelLocation = DamagePanelInstantiation();

            Instantiate(DamagePanel, DamagePanelLocation, Quaternion.identity);
        }
    }
}
