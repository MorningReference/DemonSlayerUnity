using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameSwitch : MonoBehaviour
{
    public GameObject activeFrame;
    public GameObject[] otherFrames;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            activeFrame.SetActive(true);
            for (int i = 0; i < otherFrames.Length; i++)
            {
                otherFrames[i].SetActive(false);
            }
        }
    }
}