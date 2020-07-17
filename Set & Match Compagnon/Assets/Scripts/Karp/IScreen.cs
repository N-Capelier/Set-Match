using UnityEngine;

public interface IScreen 
{
    void EnterViewportH(float startPos);
    void ExitViewportH(float endPos); 
    void EnterViewportV(float startPos);
    void ExitViewportV(float endPos);
}
