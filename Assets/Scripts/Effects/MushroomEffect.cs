using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomEffect : MonoBehaviour
{
    [SerializeField] string _mushroomEffect;

    public string GetNameEffect()
    {
        return _mushroomEffect;
    }
}

interface IPlayerEffect
{
    PlayerMovement player { set; get; }
    float timeEffect { get; set; }

    IEnumerator Effect();
}