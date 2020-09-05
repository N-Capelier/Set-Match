using System.Collections.Generic;
using UnityEngine;

namespace TennisMatch
{
    public class _MatchExchangeManager : Singleton<_MatchExchangeManager>
    {
        [Header("GameEvent")]
        [SerializeField] private _MatchTurnManager turnManager;
        [SerializeField] private _MatchRally rally;
        [SerializeField] private _MatchScore scorer;

        [Header("Variable")]
        public bool canUndo = false;
        public bool canRedo = false;
        [Space(10)]
        public Stack<MatchExchange> moveHistory = new Stack<MatchExchange>();
        public Stack<MatchExchange> movesRewind = new Stack<MatchExchange>();

        private void Awake()
        {
            moveHistory = new Stack<MatchExchange>();
            movesRewind = new Stack<MatchExchange>();
        }

        private void Update()
        {
            if (moveHistory == null)
            {
                moveHistory = new Stack<MatchExchange>();
            }
            if (movesRewind == null)
            {
                movesRewind = new Stack<MatchExchange>();
            }

            //s'il y a des actions de dispo alors on peut undo ou redo
            canUndo = moveHistory.Count != 0;
            canRedo = movesRewind.Count != 0;
        }

        public void ExchangeCommand(int increment)
        {
            MatchExchange currentMove = _MatchExchangeCommand.GenerateExchange
                (rally.Pos, increment, turnManager.turnOfPlayer);

            ExecuteExchange(currentMove);
        }

        public void ExecuteExchange(MatchExchange exchange)
        {
            //Do Exchange
            turnManager.NextTurn();
            int rallyPos = exchange.rallyPosBeforeShoot + exchange.increment;
            rally.MovedTo(rallyPos);
            if (exchange.haveMarkedPoint)
            {
                scorer.MarkedPoint(exchange.aTeamAction);
            }

            //Save Exchange
            movesRewind.Clear();
            moveHistory.Push(exchange);

            //Event du move
            MatchEvents.Exchange();
            MatchEvents.VisualUpdate();
        }

        public void UndoMove()
        {
            //Get the exchange
            MatchExchange undoMove = moveHistory.Pop();
            movesRewind.Push(undoMove);
            
            //undo
            turnManager.TurnOf(undoMove.playerShooting);
            rally.MovedTo(undoMove.rallyPosBeforeShoot);
            if (undoMove.haveMarkedPoint)
            {
                scorer.RemovePoint(undoMove.aTeamAction);
            }

            //Event du move
            MatchEvents.Undo();
            MatchEvents.VisualUpdate();
        }

        public void RedoMove()
        {
            //Get the exchange
            MatchExchange redoMove = movesRewind.Pop();

            //Do the exchange
            ExecuteExchange(redoMove);

            //Save the exchange
            moveHistory.Push(redoMove);
        }
    }
}
