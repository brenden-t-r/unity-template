using System;
using System.Collections;
using UnityEngine;

public static class UtilsStatic
{
    public static IEnumerator WaitXSeconds(int x)
    {
        float elapsedTime = 0;
        while (elapsedTime < x)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        yield return null;
    }
    
    public static IEnumerator WaitXMillis(int x)
    {
        float elapsedTime = 0;
        while (elapsedTime < x)
        {
            elapsedTime += Time.deltaTime * 1000;
            yield return null;
        }

        yield return null;
    }

    public static IEnumerator InterpolateOverSeconds(float value, float end, float seconds, Action<float> func)
    {
        float elapsedTime = 0;
        float start = value;
        while (elapsedTime < seconds)
        {
            value = Mathf.Lerp(start, end, (elapsedTime / seconds));
            func(value);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
    
    public static IEnumerator InterpolateOverMillis(float value, float end, float millis, Action<float> func)
    {
        float elapsedTime = 0;
        float start = value;
        while (elapsedTime < millis)
        {
            value = Mathf.Lerp(start, end, (elapsedTime / millis));
            func(value);
            elapsedTime += Time.deltaTime * 1000;
            yield return new WaitForEndOfFrame();
        }
    }

    public static IEnumerator MoveOverMillis(Transform objectToMove, Vector3 end, float millis)
    {
        float elapsedTime = 0;
        Vector3 startingPos = objectToMove.transform.localPosition;
        while (elapsedTime < millis)
        {
            objectToMove.transform.localPosition = Vector3.Lerp(startingPos, end, (elapsedTime / millis));
            elapsedTime += (Time.deltaTime * 1000);
            yield return new WaitForEndOfFrame();
        }

        objectToMove.transform.localPosition = end;
    }
    
    public static IEnumerator MoveOverSeconds(Transform objectToMove, Vector3 end, float seconds)
    {
        float elapsedTime = 0;
        Vector3 startingPos = objectToMove.transform.localPosition;
        while (elapsedTime < seconds)
        {
            objectToMove.transform.localPosition = Vector3.Lerp(startingPos, end, (elapsedTime / seconds));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        objectToMove.transform.localPosition = end;
    }

    public static IEnumerator MoveOverSeconds(Transform objectToMove, Vector3 end, float seconds, GameObject play)
    {
        float elapsedTime = 0;
        Vector3 startingPos = objectToMove.transform.position;
        while (elapsedTime < seconds)
        {
            objectToMove.transform.position = Vector3.Lerp(startingPos, end, (elapsedTime / seconds));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        objectToMove.transform.position = end;
        if (play != null) play.SetActive(true);
    }

    public static IEnumerator MoveOverSpeed(Transform objectToMove, Vector3 end, float speed)
    {
        // speed should be 1 unit per second
        while (objectToMove.transform.position != end)
        {
            objectToMove.transform.position =
                Vector3.MoveTowards(objectToMove.transform.position, end, speed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
    }

    public static void VerticalInputMovement(GameObject obj, float speed)
    {
        float h = 0;
        float v = -1 * Input.GetAxisRaw("Vertical");
        var tempVect = new Vector3(h, v, 0);
        tempVect = tempVect.normalized * speed * Time.deltaTime;
        obj.transform.position += tempVect;
    }

    public static void VerticalMouseMovement(GameObject obj, float speed)
    {
        Vector3 position = obj.transform.position;
        var newVector = new Vector3(
            x: position.x,
            y: position.y * Input.GetAxis("Mouse ScrollWheel"),
            z: 0
        );
        obj.transform.Translate(newVector);
    }

    public static IEnumerator AudioFadeOut(AudioSource audioSource, float fadeTime)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / fadeTime;

            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }

    public static bool IsNullFast(UnityEngine.Object myObject)
    {
        return ((object)myObject) == null;
    }
}