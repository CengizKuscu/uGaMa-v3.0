using System;
using System.Collections.Generic;
using uGaMa.Core;
using uGaMa.Views;
using UnityEngine;

namespace uGaMa.Extensions.MenuSystem
{
    [ScriptOrder(-10001)]
    public class MenuManager : View<MenuManager>
    {
        [SerializeField] private GameObject menuContainer;
        [SerializeField] private List<MenuArg> menus;
        
        protected override void OnRegister()
        {
            base.OnRegister();
            /*
 
            foreach (var menuArg in menus)
            {
                menuArg.MenuItem
            }*/
        }

        protected override void OnRemove()
        {
            base.OnRemove();
        }

        [Serializable]
        public class MenuArg
        {
            [SerializeField] private Menu menu;

            public Menu MenuItem
            {
                get { return menu; }
                set { menu = value; }
            }
        }
    }
}