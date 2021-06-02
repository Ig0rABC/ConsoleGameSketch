
namespace Controllers
{
    public abstract class Controller : IController
    {
        public delegate void ChangedHandler(Controller controller);
        public event ChangedHandler Changed;

        private Controller _next;

        public Controller()
        {

        }

        public Controller(Controller next)
        {
            _next = next;
        }

        public abstract void Update();

        protected void OnChange()
        {
            Changed?.Invoke(_next);
        }
    }
}
