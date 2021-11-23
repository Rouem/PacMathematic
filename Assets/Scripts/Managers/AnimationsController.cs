using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationsController : MonoBehaviour
{
    Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Play(string variable){
        if(!anim){
            Debug.Log("No animator set");
            return;
        }
        anim.SetTrigger(variable);
    }    

    public void Play(string variable, int value){
        if(!anim){
            Debug.Log("No animator set");
            return;
        }
        anim.SetInteger(variable, value);
    }

    public void Play(string variable, bool value){
        if(!anim){
            Debug.Log("No animator set");
            return;
        }
        anim.SetBool(variable,value);
    }
}
