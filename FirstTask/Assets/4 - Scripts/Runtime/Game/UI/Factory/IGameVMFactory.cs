﻿namespace Game
{
    public interface IGameVMFactory
    {
        CubesVM GetCubes();
        ScoreVM GetScore();
    }
}