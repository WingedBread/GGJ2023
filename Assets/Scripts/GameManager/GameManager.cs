using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public GameObject bird;

    public CanvasGroup canvasGameplay;
    public CanvasGroup canvasGameOver;
    public CanvasGroup canvasStart;
    public CanvasGroup canvasPause;
    [SerializeField]
    private Text turnText;
    [SerializeField]
    private Text pointsText;

    int turn = 1;
    public int birdAparition = 5;
    private Tile lastClickedTile;
    private Card lastClickedCard;
    int points = 0;

    [SerializeField]
    private Player player;

    private void Awake()
    {
        _instance = this;
        endTurnSubsritors = new List<EndTurnObserver>();
    }

    // Start is called before the first frame update
    void Start()
    {
        gameState = States.START;
        canvasGameplay.interactable = false;
        canvasStart.alpha = 1; 
        player.EnableCardCollider(false);
        GridManager.Instance.EnableGridColliders(false);
        turnText.text = turn.ToString();
        pointsText.text = points.ToString();
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

    public void SetClickedTile(Tile tile)
    {
        if(lastClickedTile != null){
            lastClickedTile.UnClicked();
        }
        player.EnableCardCollider(false);
        GridManager.Instance.EnableGridColliders(false);

        lastClickedTile = tile;

        PlayCard();
    }

    public void PlayCard()
    {
        if(lastClickedTile != null){
            bool played = lastClickedCard.play(lastClickedTile);
            player.cardUsed(lastClickedCard);
            EndTurn();
        }
    }

    private void EndTurn(){
        gameState = States.END_TURN;

        lastClickedCard.UnClicked();
        lastClickedTile.UnClicked();

        lastClickedCard = null;
        lastClickedTile = null;

        EndTurnNotify();

        turn++;
        turnText.text = turn.ToString();

        if(turn % birdAparition == 0){
            Instantiate(bird, new Vector3(-10,-10), Quaternion.identity);
        }

        if(GameOverCondition()){
            GameOver();
        } else {
            gameState = States.GAMEPLAY;
        }

        player.EnableCardCollider(true);
        GridManager.Instance.EnableGridColliders(false);
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
        canvasGameplay.interactable = true;
        canvasGameOver.alpha = 0;
    }

    void StartBehaviour()
    {
        player.EnableCardCollider(true);
        canvasGameplay.interactable = true;
        canvasStart.alpha = 0;
        gameState = States.GAMEPLAY;
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

    public void SetClickedCard(Card card)
    {
        if (lastClickedCard != null)
        {
            lastClickedCard.UnClicked();
        }

        player.EnableCardCollider(false);
        GridManager.Instance.EnableGridColliders(true);

        lastClickedCard = card;
    }

    public void EndTurnSubscribe(EndTurnObserver suscritor){
        endTurnSubsritors.Add(suscritor);
    }

    public void EndTurnUnsuscribe(EndTurnObserver suscritor){
        endTurnSubsritors.Remove(suscritor);
    }

    public void EndTurnNotify(){
        List<EndTurnObserver> observers = new List<EndTurnObserver>();
        endTurnSubsritors.ForEach((EndTurnObserver observer) => {observers.Add(observer);});
        if(observers.Count > 0){
            foreach(EndTurnObserver suscritor in observers){ 
                bool notified = suscritor.notify();
            }
        }
    }

    public void AddPoint()
    {
        points++;
        pointsText.text = points.ToString();
    }
}