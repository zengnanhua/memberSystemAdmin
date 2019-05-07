using AdminSystem.Domain.CommonClass;
using AdminSystem.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdminSystem.Domain.AggregatesModel.MenuAggregate
{
    public class Zmn_Ac_Menu : Entity, IAggregateRoot, IAudited
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
        /// 不管菜单在哪一级，都显示到主菜单上
        /// </summary>
        public bool AlwaysShow { get; private set; }
        /// <summary>
        /// 是否不缓存
        /// </summary>
        public bool NoCache { get; private set; }
        /// <summary>
        /// 不显示关闭按钮 ，一直存这标签
        /// </summary>
        public bool Affix { get; set; }

        /// <summary>
        /// 平台类型
        /// </summary>
        public PlatformType PlatformType { get; private set; }
        public MenuFuntionType MenuFuntionType { get; private set; }

        public int UpdateUserId { get; private set; }

        public DateTime UpdateDateTime { get; private set; }



        /// <summary>
        /// 默认是false
        /// </summary>
        public bool IsDelete { get; private set; }

        protected Zmn_Ac_Menu() { }
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
        public Zmn_Ac_Menu(string menuNo,string  menuName, string menuUrl,string order,string menuIcon,PlatformType platformType)
        {

            this.MenuNo = menuNo;
            this.PMenuNo = "base";
            this.MenuName = menuName;
            this.Order = order;
            this.MenuIcon = menuIcon;
            this.MenuFuntionType = MenuFuntionType.Menu;
            this.PlatformType = platformType;
            this.MenuUrl = MenuUrl;
            this.UpdateDateTime = DateTime.Now;
            this.IsDelete = false;
            this.IsVisible = true;
            this.DeepPath = $"{this.MenuNo}";
        }
        public Zmn_Ac_Menu CreateSonMenu(string menuNo, string menuName, string order, string menuIcon, MenuFuntionType menuFuntionType, PlatformType platformType, string menuUrl = null)
        {
            Zmn_Ac_Menu menu = new Zmn_Ac_Menu();
            menu.MenuNo = menuNo;
            menu.PMenuNo =this.MenuNo;
            menu.MenuName = menuName;
            menu.Order = order;
            menu.MenuIcon = menuIcon;
            menu.MenuFuntionType = menuFuntionType;
            menu.PlatformType = platformType;
            menu.MenuUrl = menuUrl;
            menu.UpdateDateTime = DateTime.Now;
            menu.IsDelete = false;
            menu.IsVisible = true;
            menu.DeepPath = $"{this.DeepPath},{menu.MenuNo}";

            return menu;
        }
        /// <summary>
        /// 设置菜单特性
        /// </summary>
        public void SetMenuAttributeFeature(bool alwaysShow=false,bool noCache=false,bool affix=false)
        {
            this.AlwaysShow = alwaysShow;
            this.NoCache = noCache;
            this.Affix = affix;
        }
    }
    [EnumRemark("菜单类型")]
    public enum MenuFuntionType
    {
        [EnumRemark("菜单")]
        Menu=1,
        [EnumRemark("功能")]
        Funtion=2,
    }
}
