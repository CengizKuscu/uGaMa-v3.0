using System.Collections.Generic;
using System.Linq;
using uGaMa.Core;
using uGaMa.Observer;
using UnityEngine;

namespace uGaMa.Extensions.MenuSystem
{
    public enum MenuMode
    {
        Single,
        Additive
    }

    public enum MenuManagerEvent
    {
        ChangeMenu,
        OpenMenu,
        CloseMenu
    }

    [ScriptOrder(-15000)]
    public partial class MenuManager : MonoBehaviour
    {
        [SerializeField] private Transform menuRoot;
        [SerializeField] private Transform popupRoot;
        [SerializeField] private List<MenuBase> activeList;

        [SerializeField] private MenuBase starterMenu;

        private string currentMenuName;
        private string prevMenuName;

        #region Properties

        public static string CurrentMenuName
        {
            get { return instance.currentMenuName; }
        }

        public string PrevMenuName
        {
            get { return prevMenuName; }
        }

        #endregion


        private void Awake()
        {
            instance = this;
            Init();
        }

        private static MenuManager instance;

        public static MenuManager Instance
        {
            get { return instance; }
        }

        public static void Init()
        {
            foreach (var menuBase in instance.activeList)
            {
                menuBase.gameObject.SetActive(false);
            }

            foreach (Transform go in instance.menuRoot.transform)
            {
                go.gameObject.SetActive(false);
            }
        }

        public static void OpenMenu(string menuName, MenuMode menuMode = MenuMode.Single, object param = null)
        {
            MenuBase menu = instance.activeList.Find(s => s.menuName == menuName);

            if (menuMode == MenuMode.Single)
                CloseAllMenus();

            if (menu != null)
            {
                DispatchManager.Instance.Dispatch(MenuManagerEvent.OpenMenu, menuName);
                if (menu.gameObject.activeSelf)
                    return;
            }
            else
            {
                GameObject load = Resources.Load<GameObject>("Menus/" + menuName);

                if (load == null)
                    return;

                MenuBase tmp = load.GetComponent<MenuBase>();

                Transform root = tmp.IsPopup ? instance.popupRoot : instance.menuRoot;

                GameObject go = Instantiate(load, root);
                menu = go.GetComponent<MenuBase>();
                instance.activeList.Add(menu);
                DispatchManager.Instance.Dispatch(MenuManagerEvent.OpenMenu, menuName);
            }

            instance.prevMenuName = instance.currentMenuName;
            instance.currentMenuName = menuName;
            menu.Open(param);
            RectTransform rectTransform = menu.GetComponent<RectTransform>();
            if (rectTransform != null)
                rectTransform.SetAsLastSibling();
        }

        public static void CloseMenu(string menuName = null, object param = null)
        {
            MenuBase menu;
            if (!string.IsNullOrEmpty(menuName))
            {
                menu = instance.activeList.Find(s => s.menuName == menuName);
            }
            else
            {
                menu = instance.activeList.Find(s => s.menuName == CurrentMenuName);
            }

            if (menu != null && menu.gameObject.activeSelf)
            {
                menu.Close(param);
                DestroyMenu(menu);
            }
        }

        private static void CloseAllMenus()
        {
            foreach (var item in instance.activeList.ToList())
            {
                if (item.gameObject.activeSelf)
                {
                    item.Close();
                    DestroyMenu(item);
                }
            }
        }

        private static void DestroyMenu(MenuBase menu)
        {
            if (menu.DestroyOnClose)
            {
                instance.activeList.Remove(menu);
                Destroy(menu.gameObject);
            }
        }
    }
}