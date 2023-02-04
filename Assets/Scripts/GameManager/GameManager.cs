using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    private bool firstSprout;
    
    private List<EndTurnObserver> endTurnSubsritors;

    public GameObject bird;

    //public CanvasGroup canvasGameplay;
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

    private bool gridCollidersState = false;
    private bool cardsCollidersState = false;

    private bool prePauseGridColliderState = false;
    private bool prePauseCardsColliderState = false;

    private void Awake()
    {
        _instance = this;
        endTurnSubsritors = new List<EndTurnObserver>();
    }

    // Start is called before the first frame update
    void Start()
    {
        gameState = States.START;
        AudioController.Instance.PlayMenuBGMusic();
        canvasStart.alpha = 1;   
        turnText.text = turn.ToString();
        pointsText.text = points.ToString();

        player.EnableCardCollider(false);
        GridManager.Instance.EnableGridColliders(false);
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

            if (gameState == States.END_TURN)
            {
                EndTurn();
            }

            if (gameState == States.GAME_OVER)
            {
                if (Input.GetKeyDown(KeyCode.R))
                {
                    Restart();
                    //SceneManager.LoadScene(0);
                }
            }
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
            lastClickedCard.play(lastClickedTile);
            player.cardUsed(lastClickedCard);
            gameState = States.END_TURN;
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
        if(firstSprout && GridManager.Instance.GetTileSproutAndCarrotCount() <= 0){
            return true;
        }
        return false;
    }

    public void GameOver()
    {
        gameState = States.GAME_OVER;
        canvasGameOver.alpha = 1;
        player.EnableCardCollider(false);
        GridManager.Instance.EnableGridColliders(false);
        AudioController.Instance.PlayGameOverSound();
        AudioController.Instance.PlayGameOverBGMusic();

        //Recargar Scene
    }

    void Restart()
    {
        canvasGameOver.alpha = 0;
        StartBehaviour();
    }

    void StartBehaviour()
    {
        player.EnableCardCollider(true);
        GridManager.Instance.EnableGridColliders(false);
        canvasStart.alpha = 0;
        gameState = States.GAMEPLAY;
        AudioController.Instance.PlayGameplayBGMusic();
    }

    public void PauseBehaviour()
    {
        pause = !pause;

        AudioController.Instance.SetPauseMusic(pause);

        if (pause)
        {
            canvasPause.alpha = 1;
            prePauseCardsColliderState = cardsCollidersState;
            prePauseGridColliderState = gridCollidersState;

            player.EnableCardCollider(false);
            GridManager.Instance.EnableGridColliders(false);
        }
        else
        {
            canvasPause.alpha = 0;
            player.EnableCardCollider(prePauseCardsColliderState);
            GridManager.Instance.EnableGridColliders(prePauseGridColliderState);
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

    public void SetGridCollidersState(bool state)
    {
        gridCollidersState = state;
    }
    public void SetCardsCollidersState(bool state)
    {
        cardsCollidersState = state;
    }

    public void FirstSproutPlaced(){
        firstSprout = true;
    }
}