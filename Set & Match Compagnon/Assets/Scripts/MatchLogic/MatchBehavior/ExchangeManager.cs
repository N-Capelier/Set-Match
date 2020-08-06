using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TennisMatch
{
    public class ExchangeManager : MonoBehaviour
    {
        [Header("GameEvent")]
        private MatchEvents matchEvents;

        [Header("Variable")]
        [SerializeField] private MatchData match;
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
            if (moveHistory.Count != 0)
            {
                canUndo = true;
            }
            else
            {
                canUndo = false;
            }

            if (movesRewind.Count != 0)
            {
                canRedo = true;
            }
            else
            {
                canRedo = false;
            }
        }

        public void ExchangeCommand(int increment)
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
                Debug.Log("save game & Set" + setMarked.ToString());
            }

            //Construction du move à save
            Exchange currentMove = new Exchange(aTeamShoot, false, pos, increment, pointMarked, gameMarked, setMarked);

            //Move Storage
            movesRewind.Clear();
            moveHistory.Push(currentMove);

            //UpdateRally
            rally.MovedTo(pos + increment, pointMarked);

            //Event du move
            matchEvents.Exchange(aTeamShoot);
            if (pointMarked)
            {
                matchEvents.PointMarked(aTeamShoot);
            }
            if (gameMarked)
            {
                matchEvents.GameMarked(aTeamShoot);
            }
            if (setMarked)
            {
                matchEvents.SetMarked(aTeamShoot);
            }

            matchEvents.VisualUpdate();
        }

        public void UndoExchange()
        {
            Exchange undoMove =  moveHistory.Pop();

            movesRewind.Push(undoMove);

            rally.value = undoMove.rallyPosBeforeShoot;

            //Event du move
            matchEvents.Undo();

            if (undoMove.haveMarkedSet)
            {
                if (match.currentSet >= 1)
                {
                    match.currentSet--;
                }
            }
            if (undoMove.haveMarkedGame)
            {
                if (undoMove.aTeamShoot)
                {
                    match.teamA_Score.gamePerSet[match.currentSet]--;
                }
                else
                {
                    match.teamB_Score.gamePerSet[match.currentSet]--;
                }
            }
            if (undoMove.haveMarkedPoint)
            {
                if (undoMove.aTeamShoot)
                {
                    match.teamA_Score.point--;
                } 
                else 
                {
                    match.teamB_Score.point--;
                }
            }

            matchEvents.VisualUpdate();
        }
        public void RedoExchange()
        {
            Exchange redoMove = movesRewind.Pop();

            moveHistory.Push(redoMove);

            //UpdateRally
            rally.MovedTo(redoMove.rallyPosBeforeShoot + redoMove.exchangePoints, redoMove.haveMarkedPoint);

            //Event du move
            matchEvents.Exchange(redoMove.aTeamShoot);
            if (redoMove.haveMarkedPoint)
            {
                matchEvents.PointMarked(redoMove.aTeamShoot);
            }
            if (redoMove.haveMarkedGame)
            {
                matchEvents.GameMarked(redoMove.aTeamShoot);
            }
            if (redoMove.haveMarkedSet)
            {
                matchEvents.SetMarked(redoMove.aTeamShoot);
            }

            matchEvents.VisualUpdate();
        }

    }
}
