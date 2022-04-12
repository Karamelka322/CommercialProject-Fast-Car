using CodeBase.Services.Replay;

namespace CodeBase.Logic.Item
{
    public class Capsule : Item, IReplayHandler
    {
        public int Power;

        public void OnReplay()
        {
            if(gameObject)
                Destroy(gameObject);
        }
    }
}