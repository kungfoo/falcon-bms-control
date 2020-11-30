namespace F4Utils.Terrain
{
    public interface ITerrainDBFactory
    {
        TerrainDB Create(string bmsBaseDir, bool loadAllLods);
    }
    public class TerrainDBFactory : ITerrainDBFactory
    {
        public TerrainDB Create(string bmsBaseDir, bool loadAllLods)
        {
            return new TerrainDB(bmsBaseDir, loadAllLods);
        }

    }
}
