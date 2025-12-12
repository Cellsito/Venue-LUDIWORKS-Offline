using UnityEngine;
using UnityEngine.UI;

public class PlayingText : MonoBehaviour
{
    public float altura = 50f;
    public float duracao = 1f;

    public Image img;
    private Color corInicial;
    private Vector3 posInicial;
    private float tempo;

    void Start()
    {
        img = GetComponent<Image>();
        corInicial = img.color;
        posInicial = transform.position;
    }

    void Update()
    {
        tempo += Time.deltaTime;
        float t = tempo / duracao;

        transform.position = posInicial + Vector3.up * altura * t;

        Color c = corInicial;
        c.a = 1f - t;
        img.color = c;

        if (tempo >= duracao)
            Destroy(gameObject);
    }
}
