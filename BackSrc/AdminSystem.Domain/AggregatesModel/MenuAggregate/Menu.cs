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

        public bool IsDelete { get; private set; }
    }

    public enum MenuFuntionType
    {
        [Remark("菜单")]
        Menu=1,
        [Remark("功能")]
        Funtion=2,
    }
}
