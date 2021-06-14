
namespace Controllers
{
    public abstract class Controller : IController
    {
        public delegate void ChangedHandler(Controller controller);
        public event ChangedHandler Changed;

        private readonly Controller _next;

        public Controller()
        {

        }

        public Controller(Controller next)
        {
            _next = next;
        }

        public abstract void Update();

        public void DoNothing()
        {

        }

        protected void OnChange()
        {
            Changed?.Invoke(_next);
        }

        protected void OnChange(Controller next)
        {
            Changed?.Invoke(next);
        }
    }
}
