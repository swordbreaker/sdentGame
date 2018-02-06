using UnityEngine;

[RequireComponent(typeof(AudioSource), typeof(Animator))]
public class Door2 : MonoBehaviour {
    
    private AudioSource _audioSource;
    private Animator _anim;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            print("open");
            Open();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Close();
        }
    }

    private void Open()
    {
        _audioSource.Play();
        _anim.SetBool("character_nearby", true);
    }

    private void Close()
    {
        _anim.SetBool("character_nearby", false);
    }

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _anim = GetComponent<Animator>();
    }

}

