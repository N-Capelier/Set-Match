using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TennisMatch
{
    public class ChallengePanel : Singleton<ChallengePanel>
    {
        private void Awake()
        {
            CreateSingleton(false);
        }
    }
}