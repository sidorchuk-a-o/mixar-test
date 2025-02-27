namespace Game.Battle
{
    public interface IBattleVMFactory
    {
        BattleResultVM GetBattleResult();
        SpaceshipVM[] GetSpacehips();
    }
}