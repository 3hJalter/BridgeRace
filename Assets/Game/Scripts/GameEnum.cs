using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    OnStage,
    OnStair,
    OnHit,
    OnDie,
}

public enum PlayerAnim
{
    Idle,
    Move,
    Hit,
    Win, 
    Lose
}

public enum GameState
{
    MainMenu,
    InGame,
}

public enum CharacterBelong
{
    None,
    Player,
    Bot1,
    Bot2,
    Bot3,
}

public enum GameTag
{
    Player,
    Enemy,
}
