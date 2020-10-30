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
            increment += (int)Mathf.Sign(increment) * rally.Bonus;
            rally.Bonus = 0;

            MatchExchange currentMove = _MatchExchangeCommand.GenerateExchange
                (rally.Pos, increment, _MatchTurnManager.turnOfPlayer, false);

            ExecuteExchange(currentMove);
        }

        public void FaultCommand()
        {
            MatchExchange currentMove = _MatchExchangeCommand.GenerateExchange
                (rally.Pos, 0, _MatchTurnManager.turnOfPlayer, true);

            ExecuteExchange(currentMove);
        }

        public void ExecuteExchange(MatchExchange exchange)
        {
            int rallyPos = exchange.rallyPosBeforeShoot + exchange.increment;
            rally.MovedTo(rallyPos);

            if (exchange.haveMarkedPoint)
            {
                turnManager.NextTurn();
                scorer.MarkedPoint(exchange.aTeamAction);

                _MatchTurnManager.isService = true;
                _MatchTurnManager.is2ndService = false;
            }
            else if (exchange.haveFault)
            {
                if(exchange.isService)
                {
                    _MatchTurnManager.isService = false;
                    _MatchTurnManager.is2ndService = true;
                }
                else
                {
                    scorer.MarkedPoint(!exchange.aTeamAction);
                    rally.ResetRally();
                    MatchEvents.VisualUpdate();

                    _MatchTurnManager.isService = true;
                    _MatchTurnManager.is2ndService = false;
                }
            }
            else
            {
                turnManager.NextTurn();

                _MatchTurnManager.isService = false;
                _MatchTurnManager.is2ndService = false;
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
            _MatchTurnManager.isService = undoMove.isService;
            _MatchTurnManager.is2ndService = undoMove.is2ndService;

            if (undoMove.haveMarkedPoint)
            {
                scorer.RemovePoint(undoMove.aTeamAction);
            }
            else if (undoMove.haveFault)
            {
                scorer.RemovePoint(!undoMove.aTeamAction);
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
