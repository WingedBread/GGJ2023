using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    //Hacemos que nuestro Game Manager sea un Singleton para que sea fácilmente accesible desde otras clases
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if(_instance is null)
            {
                Debug.LogError("FALTA GAME MANAGER");
            }
            return _instance;
        }
    }

    public List<CardBehaviour> deck = new List<CardBehaviour>();
    public List<CardBehaviour> discardPile = new List<CardBehaviour>();
    public List<CardBehaviour> allCards = new List<CardBehaviour>();
    public Transform[] cardSlots;
    public bool[] avaiableSlots;
    public TextMeshProUGUI deckText;
    public TextMeshProUGUI discardPileText;

    private SliderBehaviour slider;

    public CanvasGroup canvasGameplay;
    public CanvasGroup canvasGameOver;
    public CanvasGroup canvasStart;
    public CanvasGroup canvasPause;

    bool gameOver = false;
    bool pause = false;
    public bool initWithStart = true;

    private void Awake()
    {
        _instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        UpdateTexts();
        slider = GetComponent<SliderBehaviour>();
        if (initWithStart)
        {
            canvasGameplay.interactable = false;
            canvasStart.alpha = 1;
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
            CardBehaviour randomCard = deck[Random.Range(0, deck.Count)];

            //Miramos si tenemos algún slot para poner la carta, si lo hay, la activamos, 
            //le ponemos su posición (y le decimos que el slot esta ocupado) y la eliminamos del deck.
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
                    UpdateTexts();
                    return;
                }
            }
        }
    }

    public void ShuffleCards()
    {
        //Miramos si la pila de descartes es mayor a 1, si lo es reañadimos la carta al mazo, y limpiamos la lista.
        if(discardPile.Count >= 1)
        {
            foreach (CardBehaviour card in discardPile)
            {
                deck.Add(card);
            }
            discardPile.Clear();
            UpdateTexts();
        }
    }

    public void UpdateTexts()
    {
        deckText.text = deck.Count.ToString();
        discardPileText.text = discardPile.Count.ToString();
    }

    public void UpdateSliders(CardBehaviour randomCard)
    {
        slider.UpdateSlidersValue(randomCard.GetCardPowers());
    }


    public void GameOver()
    {
        canvasGameplay.interactable = false;
        canvasGameOver.alpha = 1;
        gameOver = true;
    }

    void Restart()
    {
        discardPile.Clear();
        deck.Clear();
        foreach (CardBehaviour card in allCards)
        {
            card.Restart();
            deck.Add(card);
        }
        slider.Restart();
        UpdateTexts();
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
}
