using System;

namespace TennisMatch
{
    /// <summary>
    /// Gère tout les événements d'un match 
    /// <para> ARD </para>
    /// </summary>
    public class MatchEvents : Singleton<MatchEvents>
    {
        #region Events

        public event Action onHeadOrTailLauch;
        public event Action onMatchStart;

        public event Action onFaute;
        public event Action onFilet;

        public event Action onUndo;
        public event Action onRedo;

        public event Action onExchange;
        public event Action onPointMarked;
        public event Action onGameMarked;
        public event Action onSetMarked;

        public event Action onMatchStoped;
        public event Action onMatchEnd;
        public event Action onMatchClose;

        #endregion

        #region Events Call
        public void HeadOrTailLauch() => onHeadOrTailLauch?.Invoke();
        public void MatchStart() => onMatchStart?.Invoke();


        public void Faute() => onFaute?.Invoke();
        public void Filet() => onFilet?.Invoke();


        public void Undo() => onUndo?.Invoke();
        public void Redo() => onRedo?.Invoke();

        public void Exchange() => onExchange?.Invoke();
        public void PointMarked() => onPointMarked?.Invoke();
        public void GameMarked() => onGameMarked?.Invoke();
        public void SetMarked() => onSetMarked?.Invoke();


        public void MatchStoped() => onMatchStoped?.Invoke();
        public void MatchEnd() => onMatchEnd?.Invoke();
        public void MatchClose() => onMatchClose?.Invoke();


        #endregion
    }
}
