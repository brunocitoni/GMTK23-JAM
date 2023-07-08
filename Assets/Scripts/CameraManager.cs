using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;

    public CinemachineVirtualCamera virtualCamera;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    public static void Shake(float duration, float magnitude)
    {
        if(instance != null)
            instance.StartCoroutine(instance.ShakeIEnumerator(duration, magnitude));
    }

    IEnumerator ShakeIEnumerator(float duration, float magnitude)
    {
        /*
        var transposer = virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
        transposer.m_TrackedObjectOffset = Vector3.zero;

        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transposer.m_TrackedObjectOffset = new Vector3(x, y, -10f);
            elapsed += Time.deltaTime;
            yield return 0;
        }
        transposer.m_TrackedObjectOffset = Vector3.zero;
        */

        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.position = new Vector3(x, y, -10f);
            elapsed += Time.deltaTime;
            yield return 0;
        }
        transform.position = new Vector3(0, 0, -10f);
    }
}
