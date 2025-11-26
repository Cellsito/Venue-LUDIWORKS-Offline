using System.Collections;
using UnityEngine;

public class PlayerSpriteController : MonoBehaviour
{
    [System.Serializable]
    public class CharacterSet
    {
        [Header("Idle")]
        public Sprite[] idleDown;
        public Sprite[] idleUp;
        public Sprite[] idleRight;
        public Sprite[] idleLeft;

        [Header("Walk")]
        public Sprite[] walkDown;
        public Sprite[] walkUp;
        public Sprite[] walkRight;
        public Sprite[] walkLeft;

        [Header("Push")]
        public Sprite[] pushRight;
        public Sprite[] pushLeft;

        [Header("Interact")]
        public Sprite[] interactRight;
        public Sprite[] interactLeft;
    }

    [Header("PERSONAGENS (10)")]
    public CharacterSet[] personagens = new CharacterSet[10];

    [Header("CONFIG")]
    public float tempoEntreFrames = 0.12f;

    private SpriteRenderer sr;
    private Rigidbody2D rb;

    private Sprite[] animAtual;
    private int frameAtual;
    private float contadorTempo;

    private enum Estado { Idle, Walk, Push, Interact }
    private Estado estado = Estado.Idle;

    private enum Direcao { Down, Up, Right, Left }
    private Direcao ultimaDirecao = Direcao.Down;
    private Direcao direcaoAtual = Direcao.Down;

    private Vector2 movimentoInput;
    private int personagemAtual = 0;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        // Carrega personagem salvo
        personagemAtual = PlayerPrefs.GetInt("PersonagemSelecionado", 0);

        // Animação inicial
        SetAnimation(GetIdleArray(ultimaDirecao));
    }

    void Update()
    {
        // Movimento detectado
        float movX = Input.GetAxisRaw("Horizontal");
        float movY = Input.GetAxisRaw("Vertical");
        movimentoInput = new Vector2(movX, movY).normalized;

        // Direção
        if (movimentoInput.magnitude > 0)
        {
            if (Mathf.Abs(movX) > Mathf.Abs(movY))
                direcaoAtual = movX > 0 ? Direcao.Right : Direcao.Left;
            else
                direcaoAtual = movY > 0 ? Direcao.Up : Direcao.Down;
        }

        // Estado atual
        Estado novoEstado =
            Input.GetKey(KeyCode.E) ? Estado.Interact :
            (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) ? Estado.Push :
            (movimentoInput.magnitude > 0) ? Estado.Walk :
            Estado.Idle;

        // Se algo mudou → troca animação
        if (novoEstado != estado || direcaoAtual != ultimaDirecao)
        {
            estado = novoEstado;
            ultimaDirecao = direcaoAtual;

            AtualizarAnimacaoPorEstado();   // 🔥 IMPORTANTE: antes estava comentado
        }

        AtualizarAnimacaoTempo();
    }

    private void AtualizarAnimacaoPorEstado()
    {
        frameAtual = 0;
        contadorTempo = 0f;

        switch (estado)
        {
            case Estado.Idle:
                SetAnimation(GetIdleArray(ultimaDirecao));
                break;

            case Estado.Walk:
                SetAnimation(GetWalkArray(ultimaDirecao));
                break;

            case Estado.Push:
                SetAnimation(ultimaDirecao == Direcao.Left ? GetPushLeft() : GetPushRight());
                break;

            case Estado.Interact:
                SetAnimation(ultimaDirecao == Direcao.Left ? GetInteractLeft() : GetInteractRight());
                break;
        }
    }

    private void SetAnimation(Sprite[] novoArray)
    {
        animAtual = (novoArray != null && novoArray.Length > 0) ? novoArray : null;
        frameAtual = 0;
        contadorTempo = 0f;
        AtualizarSpriteImediato();
    }

    private void AtualizarAnimacaoTempo()
    {
        if (animAtual == null || animAtual.Length == 0) return;

        contadorTempo += Time.deltaTime;

        if (contadorTempo >= tempoEntreFrames)
        {
            contadorTempo = 0f;
            frameAtual++;

            if (frameAtual >= animAtual.Length)
                frameAtual = 0;

            sr.sprite = animAtual[frameAtual];
        }
    }

    private void AtualizarSpriteImediato()
    {
        if (animAtual == null || animAtual.Length == 0) return;
        if (frameAtual >= animAtual.Length) frameAtual = 0;
        sr.sprite = animAtual[frameAtual];
    }

    // -------- PERSONAGENS --------

    private CharacterSet GetCurrentCharacter()
    {
        int idx = Mathf.Clamp(personagemAtual, 0, personagens.Length - 1);
        return personagens[idx];
    }

    private Sprite[] GetIdleArray(Direcao d)
    {
        var c = GetCurrentCharacter();
        return d switch
        {
            Direcao.Down => c.idleDown,
            Direcao.Up => c.idleUp,
            Direcao.Right => c.idleRight,
            Direcao.Left => c.idleLeft,
            _ => c.idleDown
        };
    }

    private Sprite[] GetWalkArray(Direcao d)
    {
        var c = GetCurrentCharacter();
        return d switch
        {
            Direcao.Down => c.walkDown,
            Direcao.Up => c.walkUp,
            Direcao.Right => c.walkRight,
            Direcao.Left => c.walkLeft,
            _ => c.walkDown
        };
    }

    private Sprite[] GetPushRight() => GetCurrentCharacter().pushRight;
    private Sprite[] GetPushLeft() => GetCurrentCharacter().pushLeft;
    private Sprite[] GetInteractRight() => GetCurrentCharacter().interactRight;
    private Sprite[] GetInteractLeft() => GetCurrentCharacter().interactLeft;

    // Manual selection
    public void DefinirPersonagem(int index)
    {
        personagemAtual = Mathf.Clamp(index, 0, personagens.Length - 1);

        PlayerPrefs.SetInt("PersonagemSelecionado", personagemAtual);
        PlayerPrefs.Save();

        AtualizarAnimacaoPorEstado();
    }
}