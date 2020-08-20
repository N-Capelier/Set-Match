using System.Collections.Generic;
using UnityEngine;

namespace TennisMatch
{
    public class ExchangeManager : MonoBehaviour
    {
        [Header("GameEvent")]
        private MatchEvents matchEvents;
        [SerializeField] private MatchData match;

        [Header("Variable")]
        [SerializeField] private RallyScorer rally;
        public bool canUndo = false;
        public bool canRedo = false;

        [SerializeField] private Stack<Exchange> moveHistory = new Stack<Exchange>();
        [SerializeField] private Stack<Exchange> movesRewind = new Stack<Exchange>();

        private void Awake()
        {
            matchEvents = MatchEvents.Instance;

            moveHistory = new Stack<Exchange>();
            movesRewind = new Stack<Exchange>();
        }
        private void Update()
        {
            //s'il y a des actions de dispo alors on peut undo ou redo
            canUndo = moveHistory.Count != 0;
            canRedo = movesRewind.Count != 0;
        }
        
        /*public void ExchangeCommand(int increment)
        {
            bool aTeamShoot = match.teamA_Turn;
            int pos = rally.value;

            bool isLastSet = match.currentSet >= match.MatchSetNumber;
            int set = match.currentSet;

            bool pointMarked = false;
            bool gameMarked = false;
            bool setMarked = false;

            pos = Mathf.Clamp(pos, -3, 3);
            increment = aTeamShoot? increment : -increment;

            pointMarked = PartyScore.PointWin( pos, increment);
            if (pointMarked)
            {
                if(aTeamShoot)
                {
                    gameMarked = PartyScore.GameWin(match.teamA_Score.point, match.teamB_Score.point);
                }
                else
                {
                    gameMarked = PartyScore.GameWin(match.teamB_Score.point, match.teamA_Score.point);
                }
            }
            if (gameMarked)
            {
                if (aTeamShoot)
                {
                    setMarked = PartyScore.SetWin(match.teamA_Score.gamePerSet[set - 1], match.teamB_Score.gamePerSet[set - 1], isLastSet);
                }
                else
                {
                    setMarked = PartyScore.SetWin(match.teamB_Score.gamePerSet[set - 1], match.teamA_Score.gamePerSet[set - 1], isLastSet);
                }
            }

            //Construction du move à save
            Exchange currentMove = new Exchange(aTeamShoot, false, pos, increment, 
                                                match.teamA_Score, match.teamB_Score, 
                                                pointMarked, gameMarked, setMarked);

            //Move Storage
            movesRewind.Clear();
            moveHistory.Push(currentMove);

            //UpdateRally
            rally.MovedTo(pos + increment, pointMarked);

            //Event du move
            matchEvents.Exchange();
            if (pointMarked)
            {
                matchEvents.PointMarked();
            }
            if (gameMarked)
            {
                matchEvents.GameMarked();
            }
            if (setMarked)
            {
                matchEvents.SetMarked();
            }

            matchEvents.VisualUpdate();
        }
        */
        
        public void UndoExchange()
        {
            Exchange undoMove = moveHistory.Pop();

            movesRewind.Push(undoMove);

            rally.value = undoMove.rallyPosBeforeShoot;

            //Event du move
            matchEvents.Undo();

            //
            //match.teamA_Score = undoMove.teamAScore;
            //match.teamB_Score = undoMove.teamBScore;

            matchEvents.VisualUpdate();
        }
        public void RedoExchange()
        {
            Exchange redoMove = movesRewind.Pop();

            moveHistory.Push(redoMove);

            //UpdateRally
            rally.MovedTo(redoMove.rallyPosBeforeShoot + redoMove.exchangePoints, redoMove.haveMarkedPoint);

            //Event du move
            matchEvents.Exchange();
            if (redoMove.haveMarkedPoint)
            {
                matchEvents.PointMarked();
            }
            if (redoMove.haveMarkedGame)
            {
                matchEvents.GameMarked();
            }
            if (redoMove.haveMarkedSet)
            {
                matchEvents.SetMarked();
            }

            matchEvents.VisualUpdate();
        }

    }
}

