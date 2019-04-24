using AdminSystem.Domain.AggregatesModel.Common;
using AdminSystem.Domain.AggregatesModel.CommonClass;
using AdminSystem.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdminSystem.Domain.AggregatesModel.MenuAggregate
{
    public class Menu : Entity, IAggregateRoot, IAudited
    {
        /// <summary>
        /// 菜单编码
        /// </summary>
        public string MenuNo { get; private set; }
        /// <summary>
        /// 父级菜单编码
        /// </summary>
        public string PMenuNo { get; private set; }
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string MenuName { get; private set; }
        /// <summary>
        /// 排序
        /// </summary>
        public string Order { get; private set; }
        /// <summary>
        /// 菜单url
        /// </summary>
        public string MenuUrl { get; private set; }
        /// <summary>
        /// 菜单图片
        /// </summary>
        public string MenuIcon { get; private set; }
        /// <summary>
        /// 是否显示
        /// </summary>
        public bool IsVisible { get; private set; }
        /// <summary>
        /// 层路径
        /// </summary>
        public string DeepPath { get; private set; }
        /// <summary>
        /// 平台类型
        /// </summary>
        public PlatformType PlatformType { get; private set; }
        public MenuFuntionType MenuFuntionType { get; private set; }

        public int CreateUserId { get; private set; }

        public DateTime CreateDateTime { get; private set; }

        public int UpdateUserId { get; private set; }

        public DateTime UpdateDateTime { get; private set; }

        public int DeleteUserId { get; private set; }

        public DateTime DeleteDateTime { get; private set; }

        /// <summary>
        /// 默认是false
        /// </summary>
        public bool IsDelete { get; private set; }

        protected Menu() { }
        /// <summary>
        /// 构建顶级菜单
        /// </summary>
        /// <param name="menuNo"></param>
        /// <param name="pMenuNo"></param>
        /// <param name="menuName"></param>
        /// <param name="order"></param>
        /// <param name="menuIcon"></param>
        /// <param name="menuFuntionType"></param>
        /// <param name="platformType"></param>
        /// <param name="menuUrl"></param>
        public Menu(string menuNo,string  menuName, string menuUrl,string order,string menuIcon,PlatformType platformType)
        {

            this.MenuNo = menuNo;
            this.PMenuNo = "base";
            this.MenuName = menuName;
            this.Order = order;
            this.MenuIcon = menuIcon;
            this.MenuFuntionType = MenuFuntionType.Menu;
            this.PlatformType = platformType;
            this.MenuUrl = MenuUrl;
            this.CreateDateTime = DateTime.Now;
            this.IsDelete = false;
            this.IsVisible = true;
            this.DeepPath = $"{this.MenuNo}";
        }
        public Menu CreateSonMenu(string menuNo, string menuName, string order, string menuIcon, MenuFuntionType menuFuntionType, PlatformType platformType, string menuUrl = null)
        {
            Menu menu = new Menu();
            menu.MenuNo = menuNo;
            menu.PMenuNo =this.MenuNo;
            menu.MenuName = menuName;
            menu.Order = order;
            menu.MenuIcon = menuIcon;
            menu.MenuFuntionType = menuFuntionType;
            menu.PlatformType = platformType;
            menu.MenuUrl = menuUrl;
            menu.CreateDateTime = DateTime.Now;
            menu.IsDelete = false;
            menu.IsVisible = true;
            menu.DeepPath = $"{this.DeepPath},{menu.MenuNo}";

            return menu;
        }
    }

    public enum MenuFuntionType
    {
        [Remark("菜单")]
        Menu=1,
        [Remark("功能")]
        Funtion=2,
    }
}
