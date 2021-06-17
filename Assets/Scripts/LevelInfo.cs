using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class LevelInfo : MonoBehaviour
{

    public GameObject Player;
    public GameObject Platform;
    public GameObject Finish;

    public float PlatformLength;
    private const float PlatformWidth = 5.0f;
    private const float PlatformHeight = 1.0f;
    private const float PaddingBeforePlayer = 5.0f;
    private const float PaddingAfterEnd = 10.0f;

    private void OnValidate()
    {
        float fullPlatformLength = PlatformLength + PaddingBeforePlayer + PaddingAfterEnd;
        Platform.transform.localScale = new Vector3(PlatformWidth, PlatformHeight, fullPlatformLength);
        Platform.transform.position = new Vector3(0, -PlatformHeight/2, fullPlatformLength/2 - PaddingBeforePlayer);
        Finish.transform.position = new Vector3(0, 0, PlatformLength);
    }
}
