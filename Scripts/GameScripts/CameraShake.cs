using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{

    private void LateUpdate()
    {
        if (Input.GetButtonUp("Jump"))//Input.GetMouseButtonDown(0)
        {
            StartCoroutine(Shake(0.3f, 0.4f));
        }
    }
    public IEnumerator Shake(float duration, float magnitude)
    {
       
        Vector3 originalPos = transform.localPosition;
        float elapsed = 0.0f; //keeps track how much time has elapsed since we started shaking the camera

        //we kep shaking until whike elapsed...
        while (elapsed < duration)
        {
            float x = Random.Range(-1f,1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;
            transform.localPosition = new Vector3(originalPos.x+x, originalPos.y+y, originalPos.z);
            elapsed += Time.deltaTime;
            //before we continue to the next iteration, we wait until next frame
            yield return null;
        }

        //we reset the posiiton
        transform.localPosition = originalPos;
    }
}
