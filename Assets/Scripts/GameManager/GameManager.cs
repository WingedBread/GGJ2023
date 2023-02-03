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

    public enum States {START, GAMEPLAY, END_TURN, GAME_OVER};
    public States gameState;
    private bool pause;
    
    private List<EndTurnObserver> endTurnSubsritors;

    public List<Card> deck = new List<Card>();
    public List<Card> allCards = new List<Card>();
    public Transform[] cardSlots;
    public bool[] avaiableSlots;
    public GameObject bird;

    public CanvasGroup canvasGameplay;
    public CanvasGroup canvasGameOver;
    public CanvasGroup canvasStart;
    public CanvasGroup canvasPause;

    int turn = 1;
    public int birdAparition = 5;
    private Tile lastClickedTile;


    private void Awake()
    {
        _instance = this;
        endTurnSubsritors = new List<EndTurnObserver>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (gameState == States.START)
        {
            canvasGameplay.interactable = true;
            canvasStart.alpha = 1;
        }
        for (int i = 0; i < cardSlots.Length; i++)
        {
            DrawCard();
        }

        ChangeTileColliderState(false);
    }

    void Update()
    {
        if(!pause){
            if (gameState == States.START)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    StartBehaviour();
                }
            }

            if (gameState == States.GAME_OVER)
            {
                if (Input.GetKeyDown(KeyCode.R))
                {
                    Restart();
                }
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

    public void PlayCard(Card card)
    {
        if(lastClickedTile != null){
            bool played = card.play(lastClickedTile);
            EndTurn();
        }
    }

    private void EndTurn(){
        gameState = States.END_TURN;
                
        lastClickedTile.UnClicked();
        if(endTurnSubsritors.Count > 0){
            foreach(EndTurnObserver suscritor in endTurnSubsritors){ 
                bool notified = suscritor.notify();
            }
        }

        turn++;

        if(turn % birdAparition == 0){
            Instantiate(bird, new Vector3(-10,-10), Quaternion.identity);
        }

        if(GameOverCondition()){
            GameOver();
        } else {
            gameState = States.GAMEPLAY;
        }
    }

    public void ChangeTileColliderState(bool activate)
    {
        for (int i = 0; i < GridManager.Instance.tileColliders.Count; i++)
        {
            if(GridManager.Instance.tileColliders[i] != null) GridManager.Instance.tileColliders[i].enabled = activate;
        }
    }

    public void ChangeCardColliderState(bool activate)
    {
        foreach (Card card in allCards)
        {
            card.GetComponent<BoxCollider>().enabled = activate;
        }
    }

    private bool GameOverCondition(){
        //TODO
        return false;
    }

    public void GameOver()
    {
        canvasGameplay.interactable = false;
        canvasGameOver.alpha = 1;
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
        if(lastClickedTile != null){
            lastClickedTile.UnClicked();
        }
        
        lastClickedTile = tile;
    }

    public void EndTurnSubscribe(EndTurnObserver suscritor){
        endTurnSubsritors.Add(suscritor);
    }

    public void EndTurnUnsuscribe(EndTurnObserver suscritor){
        endTurnSubsritors.Remove(suscritor);
    }
}