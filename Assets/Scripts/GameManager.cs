using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    //Hacemos que nuestro Game Manager sea un Singleton para que sea f�cilmente accesible desde otras clases
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance is null)
            {
                Debug.LogError("FALTA GAME MANAGER");
            }
            return _instance;
        }
    }
    public enum States {Start, Gameplay, Pause, GameOver};
    public States gameState;

    public List<Card> deck = new List<Card>();
    public List<Card> allCards = new List<Card>();
    public Transform[] cardSlots;
    public bool[] avaiableSlots;
    public GridManager gridManager;
    public GameObject bird;
    List<GameObject> birds;

    public CanvasGroup canvasGameplay;
    public CanvasGroup canvasGameOver;
    public CanvasGroup canvasStart;
    public CanvasGroup canvasPause;

    bool gameOver = false;
    bool pause = false;
    public bool initWithStart = false;

    [SerializeField]
    private Tile lastClickedTile;
    [SerializeField]
    private Card playedCard;

    private void Awake()
    {
        _instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        if (initWithStart)
        {
            canvasGameplay.interactable = false;
            canvasStart.alpha = 1;
        }
        for (int i = 0; i < cardSlots.Length; i++)
        {
            DrawCard();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (initWithStart)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartBehaviour();
            }
        }
        if (gameOver)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Restart();
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            Instantiate(bird, new Vector3(-10,-10), Quaternion.identity);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            pause = !pause;
            PauseBehaviour(pause);
        }
    }

    public void DrawCard()
    {
        //Si hay alguna carta en la baraja
        if(deck.Count >= 1)
        {
            //Recogemos una carta random de la lista del mazo
            Card randomCard = deck[Random.Range(0, deck.Count)];

            //Miramos si tenemos alg�n slot para poner la carta, si lo hay, la activamos, 
            //le ponemos su posici�n (y le decimos que el slot esta ocupado) y la eliminamos del deck.
            for (int i = 0; i < avaiableSlots.Length; i++)
            {
                if(avaiableSlots[i] == true)
                {
                    randomCard.gameObject.SetActive(true);
                    randomCard.transform.position = cardSlots[i].position;
                    avaiableSlots[i] = false;
                    randomCard.hasBeenPlayed = false;
                    randomCard.handIndex = i;
                    deck.Remove(randomCard);
                    return;
                }
            }
        }
    }

    public void SetPlayedCard(Card card)
    {
        playedCard = card;
    }

    public void PlayCard()
    {
        playedCard.CardBehaviour();
        playedCard.HideCard();
    }

    public void ChangeTileColliderState(bool activate)
    {
        for (int i = 0; i < gridManager.tileColliders.Count; i++)
        {
            gridManager.tileColliders[i].enabled = activate;
        }
    }

    public void ChangeCardColliderState(bool activate)
    {
        foreach (Card card in allCards)
        {
            card.GetComponent<BoxCollider>().enabled = activate;
        }
    }

    public void GameOver()
    {
        canvasGameplay.interactable = false;
        canvasGameOver.alpha = 1;
        gameOver = true;
    }

    void Restart()
    {
        deck.Clear();
        foreach (Card card in allCards)
        {
            card.Restart();
            deck.Add(card);
        }
        canvasGameplay.interactable = true;
        canvasGameOver.alpha = 0;
        gameOver = false;
    }

    void StartBehaviour()
    {
        canvasGameplay.interactable = true;
        canvasStart.alpha = 0;
    }

    void PauseBehaviour(bool pause)
    {
        if (pause)
        {
            canvasGameplay.interactable = false;
            canvasPause.alpha = 1;
        }
        else
        {
            canvasGameplay.interactable = true;
            canvasPause.alpha = 0;
        }
    }

    public void SetClickedTile(Tile tile)
    {
        lastClickedTile = tile;
        Debug.Log(tile.name);
        Debug.Log(lastClickedTile.name);
    }

    public Tile GetClickedTile()
    {
        return lastClickedTile;
    }
}
