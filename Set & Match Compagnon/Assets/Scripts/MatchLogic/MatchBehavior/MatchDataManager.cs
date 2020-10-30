using Player;
using UnityEngine;

namespace TennisMatch
{
    public class MatchDataManager : Singleton<MatchDataManager>
    {
        [Header("GameMeta")]
        [SerializeField] private MatchData match;
        [SerializeField] private PlayerWaitingMatchs matchWaiting;

        private void Awake()
        {
            MatchEvents.VisualUpdate();
        }
        
        private void OnEnable() 
        {
            MatchEvents.onExchange += TurnCount;
            MatchEvents.onUndo += Undo;
            //MatchEvents.onFault += Initialisation;
            //MatchEvents.onNet += Initialisation;

            //MatchEvents.onPointMarked += Initialisation;
            //MatchEvents.onGameMarked += Initialisation;
            //MatchEvents.onSetMarked += Initialisation;

            //MatchEvents.onMatchStart += Initialisation;
            MatchEvents.onMatchClose += Reboot;
            MatchEvents.onMatchStoped += SaveUnfinishedGame;
            MatchEvents.onMatchEnd += SaveCompleteGame;
        }
        private void OnDisable()
        {
            MatchEvents.onExchange -= TurnCount;
            MatchEvents.onUndo -= Undo;
            //MatchEvents.onFault -= Initialisation;
            //MatchEvents.onNet -= Initialisation;

            //MatchEvents.onPointMarked -= Initialisation;
            //MatchEvents.onGameMarked -= Initialisation;
            //MatchEvents.onSetMarked -= Initialisation;

            //MatchEvents.onMatchStart -= Initialisation;
            MatchEvents.onMatchClose -= Reboot;
            MatchEvents.onMatchStoped -= SaveUnfinishedGame;
            MatchEvents.onMatchEnd -= SaveCompleteGame;        
        }

        private void Start()
        {
            MatchEvents.VisualUpdate();
        }
    
        private void TurnCount()
        {
            match.turnCount++;
        }
        private void PointCount()
        {
            match.pointCount++;
        }
        private void GameCount()
        {
            match.gameCount++;
        }
        
        private void Undo()
        {
            match.turnCount --;
        }

        private void Reboot()
        {
            match.Reboot();
        }


        private void SaveCompleteGame()
        {
            CompleteMatch matchComplete = MatchDataConvertor.ConvertToCompleteMatch(match);
            PlayerData.CompleteMatchs.Add(matchComplete);
            Reboot();
        }

        private void SaveUnfinishedGame()
        {
            UnfinnishedMatch matchUnfinished = MatchDataConvertor.ConvertToUnfinnishedMatch(match);
            matchWaiting.AddUnfinnishedMatch(matchUnfinished);
            Reboot();
        }
    }
}