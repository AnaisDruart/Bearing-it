using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene1 : MonoBehaviour
{
    [SerializeField] Fader fade;


    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown)
        {
            fade.FadeOut();
        }
    }
}
