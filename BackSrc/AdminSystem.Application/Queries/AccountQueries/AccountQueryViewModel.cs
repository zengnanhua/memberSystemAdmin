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
    }
    public class PageMenu
    {
        public string path { get; set; }
        public bool alwaysShow { get; set; }
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

    public class UserDto
    {
        public int Id { get; set; }
        /// <summary>
        /// 用户姓名
        /// </summary>
        public string Name { get;  set; }
        /// <summary>
        ///  用户账号
        /// </summary>
        public string UserName { get;  set; }
        /// <summary>
        /// 用户性别
        /// </summary>
        public string Sex { get;  set; }
        /// <summary>
        /// 用户手机号码
        /// </summary>
        public string Phone { get;  set; }
        public DateTime UpdateDateTime { get;  set; }
    }
    public class GetUserListParameter
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
    }
}
