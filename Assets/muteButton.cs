using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class muteButton : MonoBehaviour
{
    public void MuteButton(bool mute)
    {
        if (mute)
        {
            AudioListener.volume = 0;
        }
        else
        {
            AudioListener.volume = 1;
        }
    }
}
