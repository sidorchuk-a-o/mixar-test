namespace Game
{
    public class GameVMFactory : IGameVMFactory
    {
        private readonly IGameState _gameState;

        public GameVMFactory(IGameState gameState)
        {
            _gameState = gameState;
        }

        public CubesVM GetCubes()
        {
            return new CubesVM(_gameState);
        }
    }
}