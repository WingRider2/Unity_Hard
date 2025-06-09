public enum PlayerState
{
    None , //상태 유지
    Idle ,
    Chase,
    Attack ,
    Dead ,
    Fail,//실패
}

public enum EnemyType
{
    None ,
    BaseCube,

}

public enum PoolType
{
    CommonProjectile,
    UncommonProjectile,
    RareProjectile,
    EpicProjectile,
    UniqueProjectile,
    LegendaryProjectile,

    Obstacle,

    Enemy,
}

public enum Tag
{
    Player,
    Enemy,
}

