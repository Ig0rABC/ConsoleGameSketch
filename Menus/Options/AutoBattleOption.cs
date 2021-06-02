﻿using Controllers;
using Commands;

namespace Menus
{
    public sealed class AutoBattleOption : MenuOption
    {
        public AutoBattleOption(BattleController controller) : base("Switch to auto battle mode", new AutoBattleCommand(controller))
        {

        }
    }
}