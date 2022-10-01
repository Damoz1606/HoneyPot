using System.Collections.Generic;
using UnityEngine;

public abstract class ATileModel
{
    [SerializeField] public TileTypes tileType;
    [SerializeField] public int score = 100;

    [SerializeField] public ChallengeCollectChannel _challengeCollectChannel;
    [SerializeField] public ScoreChannel _scoreChannel;
    [SerializeField] public ChannelTile _channelTileEvents;

}