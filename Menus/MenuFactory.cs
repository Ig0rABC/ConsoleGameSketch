using System;
using Controllers;

namespace Menus
{
    public static class MenuFactory
    {
        public static Menu Create(Controller controller) => controller switch
        {
            null => throw new ArgumentNullException(),
            BattleController => new BattleMenu((BattleController)controller),
            InventoryController => new InventoryMenu((InventoryController)controller),
            _ => throw new ArgumentException($"Unknow controller: {controller}"),
        };
    }
}
