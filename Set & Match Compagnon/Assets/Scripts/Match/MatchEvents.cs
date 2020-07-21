using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchEvents : Singleton<MatchEvents>
{
    public event Action onExchange;
    public event Action onPointMarked;
    public event Action onSetWin;

    public event Action onUndo;
    public event Action onRedo;

    public event Action onFilet;
    public event Action onFaute;

    public void Exchange() => onExchange?.Invoke();
    public void PointMarked() => onPointMarked?.Invoke();
    public void SetWin() => onSetWin?.Invoke();

    public void Undo() => onUndo?.Invoke();
    public void Redo() => onRedo?.Invoke();

    public void Filet() => onFilet?.Invoke();
    public void Faute() => onFaute?.Invoke();

}
