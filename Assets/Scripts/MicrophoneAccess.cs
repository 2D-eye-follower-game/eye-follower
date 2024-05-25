using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Microphoneaccess : MonoBehaviour
{
   private void Start()
    {
        string[] microphones = Microphone.devices;
        if(microphones.Length == 0 )
        {
            Debug.Log("No Microphone found ");

        }
        else 
        {
            Debug.Log("No of microphone available " + " " + microphones.Length);
            foreach( string microphone in microphones ){
                Debug.Log(microphone);
            }
        }
        

    }

}
