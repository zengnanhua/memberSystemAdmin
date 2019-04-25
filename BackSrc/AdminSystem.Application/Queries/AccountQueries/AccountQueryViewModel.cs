using System;
using System.Collections.Generic;
using System.Text;

namespace AdminSystem.Application.Queries
{
    public class MenuDto
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
    }
    public class PageMenu
    {
        public string path { get; set; }
        //public bool alwaysShow { get; set; }
        //public string redirect { get; set; }
        public string name { get; set; }
        public PageMenuMeta meta { get; set; }
        public List<PageMenu> children { get;set;}
    }
    public class PageMenuMeta
    {
        public string title { get; set; }
        public string icon { get; set; }
        public bool noCache { get; set; }
        public bool affix { get; set; }
        public PageMenuMeta()
        {
            this.noCache = false;
            this.affix = false;
        }
    }
}
