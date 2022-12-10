using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageNotice : MonoBehaviour
{
    public Text DamageText;

    BulletDeletion bulletdeletion;

    GameObject ReferenceGameObject;

    float Chronometre;

    void Awake()
    {
        DamageText.text = "";
    }

    void Start()
    {
        ParentDetection();

        DamageText.text = bulletdeletion.Damage.ToString();
    }

    void Update()
    {
        Destroy(this.gameObject, 0.25f);
    }

    void ParentDetection()
    {
        int LayerIndex = LayerMask.GetMask("TARGET");
        Collider[] ParentObject = Physics.OverlapSphere(this.transform.position, 3, LayerIndex);

        if (ParentObject[0].gameObject.CompareTag("TARGET"))
        {
            ReferenceGameObject = ParentObject[0].gameObject;

            bulletdeletion = ReferenceGameObject.GetComponent<BulletDeletion>();
        }
    }
}
