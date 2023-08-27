using UnityEngine.SceneManagement;
using static TeamOdd.Ratocalypse.MapLib.GameLib.ExecuteResult;

namespace TeamOdd.Ratocalypse.MapLib.GameLib.Commands
{
    public class LoadNewGame : Command
    {
        public override ExecuteResult Execute()
        {
            SceneManager.LoadScene("Loading");
            return new End();
        }
        
    }
}