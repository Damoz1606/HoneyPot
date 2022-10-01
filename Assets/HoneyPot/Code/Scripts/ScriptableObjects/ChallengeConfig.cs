using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ChallengeConfig", menuName = "Config/ChallengeConfig", order = 0)]
public class ChallengeConfig : ScriptableObject
{
    [SerializeField] public List<ChallengeCollect> collectChallenges;
    [SerializeField] public ChallengeScore scoreChallenge;
}