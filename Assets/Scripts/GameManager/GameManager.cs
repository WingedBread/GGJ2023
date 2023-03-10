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

    public enum States {START, TUTORIAL, GAMEPLAY, END_TURN, GAME_OVER};
    public States gameState;
    private bool pause;

    private bool firstSprout;
    
    private List<EndTurnObserver> endTurnSubsritors;

    public GameObject bird;

    //public CanvasGroup canvasGameplay;
    public CanvasGroup canvasGameOver;
    public CanvasGroup canvasStart;
    public CanvasGroup canvasPause;
    public CanvasGroup canvasTutorial;
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

    [SerializeField]
    private Button pauseButton;
    [SerializeField]
    private GameObject muteButton;

    private bool birdEat = false;

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
        pauseButton.interactable = false;
        muteButton.SetActive(false);

        player.EnableCardCollider(false);
        GridManager.Instance.EnableGridColliders(false);
    }

    void Update()
    {
        if(!pause){
            if (gameState == States.START)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    TutorialBehaviour();
                }
            }
            else if (gameState == States.TUTORIAL)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    StartBehaviour();
                }
            }
            else if (gameState == States.GAMEPLAY)
            {
                if(lastClickedCard != null && lastClickedTile != null){
                    PlayCard();
                }
            } 
            else if (gameState == States.END_TURN)
            {
                EndTurn();
            } 
            else if (gameState == States.GAME_OVER)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    SceneManager.LoadScene(0);
                }
            }
        }

        if (gameState == States.GAMEPLAY && Input.GetMouseButtonDown(1) && lastClickedCard != null && lastClickedCard.IsCardSelected())
        {
            lastClickedCard.UnClicked();
            player.EnableCardCollider(true);
            GridManager.Instance.EnableGridColliders(false);
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
    }

    public void PlayCard()
    {
        GameManager.Instance.SetBirdEat(false);
        //Debug.Log("FALSE");
        if (lastClickedTile != null){
            lastClickedCard.play(lastClickedTile);
            player.cardUsed(lastClickedCard);
            gameState = States.END_TURN;
        }
    }

    private void EndTurn(){

        if(lastClickedCard != null) lastClickedCard.UnClicked();
        if(lastClickedTile != null) lastClickedTile.UnClicked();

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
            player.EnableCardCollider(true);
            GridManager.Instance.EnableGridColliders(false);
        }


    }

    private bool GameOverCondition(){        
        if(firstSprout && GridManager.Instance.GetTileSproutAndCarrotCount() <= 0 && birdEat){
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
    }

    void StartBehaviour()
    {
        player.EnableCardCollider(true);
        GridManager.Instance.EnableGridColliders(false);
        canvasStart.alpha = 0;
        canvasTutorial.alpha = 0;
        gameState = States.GAMEPLAY;
        pauseButton.interactable = true;
        AudioController.Instance.PlayGameplayBGMusic();
    }

    public void PauseBehaviour()
    {
        if (gameState == States.GAMEPLAY)
        {
            pause = !pause;

            AudioController.Instance.SetPauseMusic(pause);

            if (pause)
            {
                canvasPause.alpha = 1;
                muteButton.SetActive(true);
                canvasPause.interactable = true;
                prePauseCardsColliderState = cardsCollidersState;
                prePauseGridColliderState = gridCollidersState;

                player.EnableCardCollider(false);
                GridManager.Instance.EnableGridColliders(false);
            }
            else
            {
                canvasPause.alpha = 0;
                muteButton.SetActive(false);
                canvasPause.interactable = false;
                player.EnableCardCollider(prePauseCardsColliderState);
                GridManager.Instance.EnableGridColliders(prePauseGridColliderState);
            }
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
                suscritor.notify();
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

    public void SetBirdEat(bool eat)
    {
        birdEat = eat;
    }

    public void TutorialBehaviour()
    {
        gameState = States.TUTORIAL;
        canvasStart.alpha = 0;
        canvasTutorial.alpha = 1;
        pauseButton.interactable = false;
    }
}