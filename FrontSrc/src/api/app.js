import request from '@/utils/request'
import { getParam } from '@/utils/StringHandle'
export function GetMenu(data) {
    // var dlist = [{
    //         path: '',
    //         children: [{
    //             path: 'dashboard',
    //             name: '首页',
    //             meta: { title: '首页', icon: 'dashboard', noCache: false, affix: false }
    //         }]
    //     },

    //     {
    //         path: "",
    //         "name": "systemManage",
    //         "meta": {
    //             "title": "系统管理",
    //             "icon": "lock",
    //         },
    //         "children": [{
    //             "path": "/permission/page",
    //             "name": "menuManage",
    //             "meta": {
    //                 "title": "菜单管理",
    //                 "icon": "lock",
    //             },

    //         }, {
    //             "path": "/permission/directive",
    //             "name": "userManage",
    //             "meta": {
    //                 "title": "用户管理",
    //                 "icon": "lock",

    //             },

    //         }, {
    //             "path": "/permission/role",
    //             "name": "roleManage",
    //             "meta": {
    //                 "title": "角色管理",
    //                 "icon": "lock",

    //             },

    //         }]

    //     }
    // ];
    // var list = dlist;
    // return new Promise(resolve => {
    //     resolve(list);
    // });

    return request({
        url: '/Account/GetPageMenu',
        method: 'post',
        data: getParam(data)
    })
}