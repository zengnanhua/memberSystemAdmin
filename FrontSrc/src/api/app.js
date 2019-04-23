import request from '@/utils/request'
import { getParam } from '@/utils/StringHandle'
export function GetMenu(data) {
    var list = [{
            path: '',
            children: [{
                path: 'dashboard',
                name: '首页',
                meta: { title: '首页', icon: 'dashboard', noCache: true, affix: true }
            }]
        },
        {
            path: '/permission',
            redirect: '/permission/index',
            alwaysShow: true, // will always show the root menu
            meta: {
                title: 'permission',
                icon: 'lock',
                roles: ['admin', 'editor'] // you can set roles in root nav
            },
            children: [{
                    path: 'page',
                    name: 'PagePermission',
                    meta: {
                        title: 'pagePermission',
                        roles: ['admin'] // or you can only set roles in sub nav
                    }
                },
                {
                    path: 'directive',
                    name: 'DirectivePermission',
                    meta: {
                        title: 'directivePermission'
                            // if do not set roles, means: this page does not require permission
                    }
                },
                {
                    path: 'role',
                    name: 'RolePermission',
                    meta: {
                        title: 'rolePermission',
                        roles: ['admin']
                    }
                }
            ]
        },
    ];
    return new Promise(resolve => {
        resolve(list);
    });

    return request({
        url: '/UserManage/GetPcMenu',
        method: 'post',
        data: getParam(data)
    })
}