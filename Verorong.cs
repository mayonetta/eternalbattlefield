using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering;

public class Verorong : MonoBehaviour
{
    public GameObject VerorongReticle;
    public GameObject Bullet;

    public Transform CoveringVerorongPosition;
    public Transform FiringVerorongPosition;
    public Transform BulletInstantiation;

    public Animator VerorongReticleAnimation;

    public ParticleSystem ShellEjection;

    public Vector3 TargetLocation;

    public float VerorongFireRate = 0.04f;
    public float MachineGunFireRate;

    RecoilAndHitEffect recoilandhiteffect;

    Animation VerorongAnimation;

    Vector3 VerorongAnimationError = new Vector3(-3.0f, 0.0f, -1.5f);

    bool GettingInPosition;

    void Awake()
    {
        recoilandhiteffect = Camera.main.GetComponent<RecoilAndHitEffect>();

        VerorongAnimation = GetComponent<Animation>();
    }

    void Start()
    {
        VerorongReticle.SetActive(false);

        GettingInPosition = false;

        VerorongAnimation.Play("CoveringVerorong");
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (GettingInPosition != true)
            {
                VerorongAnimation.CrossFade("FiringVerorong", 0.1f);

                GettingInPosition = true;
            }

            VerorongReticle.SetActive(true);

            StartCoroutine(VerorongFiringPosition());
            StartCoroutine(VerorongFiringLookRotation());
            StartCoroutine(VerorongReticleLocation());

            VerorongMachineGunFire();

            if (MachineGunFireRate < VerorongFireRate)
            {
                MachineGunFireRate += Time.deltaTime;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            VerorongReticle.SetActive(false);

            VerorongAnimation.CrossFade("CoveringVerorong", 0.1f);

            StartCoroutine(VerorongCoveringPosition());
            StartCoroutine(VerorongCoveringLookRotation());

            GettingInPosition = false;
        }

        if (GettingInPosition != true && this.transform.position == VerorongAnimationError)
        {
            this.transform.position = CoveringVerorongPosition.position;
        }
    }

    IEnumerator VerorongCoveringPosition()
    {
        Vector3 CoveringVerorong = CoveringVerorongPosition.position;
        Vector3 FiringVerorong = this.transform.position;

        float present = 0.0f;
        float future = 0.1f;
        float elapsedrate = present / future;

        while(elapsedrate < 1.0f)
        {
            present += Time.deltaTime;
            elapsedrate = present / future;

            this.transform.position = Vector3.Lerp(FiringVerorong, CoveringVerorong, elapsedrate);

            yield return null;
        }

        this.transform.position = CoveringVerorong;
    }

    IEnumerator VerorongCoveringLookRotation()
    {
        Vector3 CoveringVerorong = new Vector3(0.0f, -210.0f, 0.0f);
        Vector3 FiringVerorong = this.transform.eulerAngles;
            

        float present = 0.0f;
        float future = 0.1f;
        float elapsedrate = present / future;

        while (elapsedrate < 1.0f)
        {
            present += Time.deltaTime;
            elapsedrate = present / future;

            this.transform.eulerAngles = Vector3.Lerp(FiringVerorong, -CoveringVerorong, elapsedrate);

            yield return null;
        }

        this.transform.eulerAngles = CoveringVerorong;
    }

    IEnumerator VerorongFiringPosition()
    {
        Vector3 CoveringVerorong = this.transform.position;
        Vector3 FiringVerorong = FiringVerorongPosition.position;

        float present = 0.0f;
        float future = 0.1f;
        float elapsedrate = present / future;

        while (elapsedrate < 1.0f)
        {
            present += Time.deltaTime;
            elapsedrate = present / future;

            this.transform.position = Vector3.Lerp(CoveringVerorong, FiringVerorong, elapsedrate);

            yield return null;
        }

        this.transform.position = FiringVerorong;
    }

    IEnumerator VerorongFiringLookRotation()
    {
        Vector3 CoveringVerorong = this.transform.eulerAngles;
        Vector3 FiringVerorong = new Vector3(0.0f, 0.0f, 0.0f);

        float present = 0.0f;
        float future = 0.1f;
        float elapsedrate = present / future;

        while (elapsedrate < 0.1f)
        {
            present += Time.deltaTime;
            elapsedrate = present / future;

            this.transform.eulerAngles = Vector3.Lerp(CoveringVerorong, FiringVerorong, elapsedrate);

            yield return null;
        }

        this.transform.eulerAngles = FiringVerorong;
    }

    IEnumerator VerorongReticleLocation()
    {
        float Horizontal = Input.GetAxis("Mouse X");
        float Vertical = Input.GetAxis("Mouse Y");

        ReticleAnimation(Horizontal, Vertical);

        yield return null;
    }

    void ReticleAnimation(float Horizontal, float Vertical)
    {
        if (Horizontal >= 0.1f || Horizontal <= -0.1f || Vertical >= 0.1f || Vertical <= -0.1f)
        {
            VerorongReticleAnimation.SetBool("SEARCH", true);
        }
        else
        {
            VerorongReticleAnimation.SetBool("SEARCH", false);
        }
    }

    void VerorongMachineGunFire()
    {
        if (MachineGunFireRate < VerorongFireRate)
        {
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit Detection;
        int mask = 1 << 6;
        Debug.DrawRay(ray.origin, ray.direction * 128, Color.red);

        if (Physics.Raycast(ray, out Detection, 128, mask))
        {
            if (Detection.collider.CompareTag("TARGET"))
            {
                TargetLocation = Detection.transform.position;

                Instantiate(Bullet, BulletInstantiation.transform.position, BulletInstantiation.transform.rotation);

                VerorongReticleAnimation.SetBool("FIRE", true);

                Debug.Log("º£·Î·Õ ±âÃÑ ¹ß»ç");

                StartCoroutine(recoilandhiteffect.Effect(0.02f, 0.1f));
                ShellEjection.Play();
            }

            if (Detection.collider.CompareTag("BACKGROUND"))
            {
                VerorongReticleAnimation.SetBool("FIRE", false);

                Debug.Log("º£·Î·Õ ±âÃÑ Á¤Áö");
            }
        }

        MachineGunFireRate = 0.0f;
    }
}
