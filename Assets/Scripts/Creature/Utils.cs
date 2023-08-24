
using TeamOdd.Ratocalypse.CreatureLib.Cat;
using TeamOdd.Ratocalypse.CreatureLib.Rat;
using TeamOdd.Ratocalypse.Obstacle;
using static TeamOdd.Ratocalypse.MapLib.MapData;

namespace TeamOdd.Ratocalypse.CreatureLib
{
    public class Utils 
    {
        public static bool IsEnemy(Placement a, Placement b, bool containObstacle = false)
        {
            if(containObstacle && (a is ObstacleData || b is ObstacleData))
            {
                return true;
            }
            return (a is CatData && b is RatData) || (a is RatData && b is CatData);
        }
    }
}