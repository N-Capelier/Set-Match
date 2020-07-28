using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TennisMatch
{
    public class OpponentPanel : MonoBehaviour
    {
        RaycastHit hit;

        private void Update()
        {
            for(int i = 0; i < Input.touchCount; i++)
            {
                if(Input.GetTouch(i).phase == TouchPhase.Began)
                {
                    Ray touchRay = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);

                    if(Physics.Raycast(touchRay, out hit))
                    {
                        if(hit.collider.CompareTag("Opponent"))
                        {
                            ChallengePanel.Instance.gameObject.SetActive(true);
                        }
                    }
                }
            }
        }
    }
}