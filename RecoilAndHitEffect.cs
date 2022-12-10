using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoilAndHitEffect : MonoBehaviour
{
    Camera Cam;

    Vector3 OriginalCamPosition;

    void Start()
    {
        Cam = Camera.main;

        OriginalCamPosition = Cam.transform.localPosition;
    }

    public IEnumerator Effect(float elapsedrate, float magnitude)
    {
        float present = 0.0f;

        while (present <= elapsedrate)
        {
            present += Time.deltaTime;

            Cam.transform.localPosition = Random.insideUnitSphere * magnitude + OriginalCamPosition;

            yield return new WaitForSeconds(0.05f);
        }

        Cam.transform.localPosition = OriginalCamPosition;
    }
}
