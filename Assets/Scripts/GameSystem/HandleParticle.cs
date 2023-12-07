using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HandleParticle : MonoBehaviour
{
    public static HandleParticle instance;

    [SerializeField] private ParticleSystem[] _particleSystem;

    private void Awake()
    {
        instance = this;
    }

    public void SummonParticle(int index, Vector2 position)
    {
        Instantiate(_particleSystem[index], position, Quaternion.identity);
    }
}
