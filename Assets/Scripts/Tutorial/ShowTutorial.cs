using System;
using UnityEngine;

public class ShowTutorial : MonoBehaviour
{
    private GameObject _showTutorial;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
      VerificarTag(other.tag);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _showTutorial.SetActive(false);
    }

    private void VerificarTag(string tag)
    {
        if (tag == "Droid")
        {
            _showTutorial.SetActive(true);
        }else if (tag == "Human")
        {
            _showTutorial.SetActive(true);
        }
    }
}
