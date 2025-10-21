using System;
using UnityEngine;

public class KeyController : MonoBehaviour
{
    [SerializeField] private GameObject key;
    [SerializeField] private OutletInteractable outlet;

    private void Awake()
    {
        outlet.onUnplug.AddListener(ActivateKey);
    }

    private void ActivateKey()
    {
        key.SetActive(true);
    }
}
