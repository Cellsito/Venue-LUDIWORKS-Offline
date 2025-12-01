using UnityEngine;
using UnityEngine.EventSystems;

public class UISound : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler
{
    public AudioClip hoverSound;
    public AudioClip clickSound;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = FindObjectOfType<AudioSource>(); // pega o AudioSource da UI
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (hoverSound != null)
            audioSource.PlayOneShot(hoverSound);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (clickSound != null)
            audioSource.PlayOneShot(clickSound);
    }
}
