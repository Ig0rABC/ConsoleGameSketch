
namespace Controllers
{
    public interface IController
    {
        void Update();
        delegate void ChangedHandler();
    }
}
