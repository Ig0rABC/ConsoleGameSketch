using System;
using Controllers;

namespace Menus
{
    public static class MenuFactory
    {
        public static Menu Create(Controller controller)
        {
            switch (controller)
            {
                case BattleController:
                    return new BattleMenu(controller as BattleController);
                case null:
                    throw new ArgumentNullException();
                default:
                    throw new ArgumentException($"Unknow controller: {controller}");
            }
        }
    }
}
