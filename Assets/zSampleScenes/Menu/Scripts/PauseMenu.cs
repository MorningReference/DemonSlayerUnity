using System;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    private Toggle m_MenuToggle;
    private float m_TimeScaleRef = 1f;
    private float m_VolumeRef = 1f;
    private bool m_Paused;

    [SerializeField] GameObject pauseMenu;


    void Awake()
    {
        m_MenuToggle = GetComponent<Toggle>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Pressing Escape");
            m_MenuToggle.isOn = !m_MenuToggle.isOn;
            // Cursor.visible = m_MenuToggle.isOn;//force the cursor visible if anythign had hidden it
            OnMenuStatusChange();
        }
    }


    private void MenuOn()
    {
        m_TimeScaleRef = Time.timeScale;
        Time.timeScale = 0f;

        m_VolumeRef = AudioListener.volume;
        AudioListener.volume = 0f;

        m_Paused = true;
    }


    public void MenuOff()
    {
        Time.timeScale = m_TimeScaleRef;
        AudioListener.volume = m_VolumeRef;
        m_Paused = false;
        pauseMenu.GetComponentInChildren<GameObject>().SetActive(false);
    }


    public void OnMenuStatusChange()
    {
        if (m_MenuToggle.isOn && !m_Paused)
        {
            MenuOn();
        }
        else if (!m_MenuToggle.isOn && m_Paused)
        {
            MenuOff();
        }
    }


    // #if !MOBILE_INPUT
    // void Update()
    // {
    //     if (Input.GetKeyUp(KeyCode.Escape))
    //     {
    //         Debug.Log("Pressing Escape");
    //         m_MenuToggle.isOn = !m_MenuToggle.isOn;
    //         Cursor.visible = m_MenuToggle.isOn;//force the cursor visible if anythign had hidden it
    //     }
    // }
    // #endif

}
