import request from '@/utils/request'
// import { getParam } from '@/utils/StringHandle'
export function GetUserList(data) {
    return request({
        url: '/Account/GetUserList',
        method: 'post',
        data: data
    })
}
export function CreateUser(data) {
    return request({
        url: '/Account/CreateUser',
        method: 'post',
        data: data
    })
}
export function UpdateUser(data) {
    return request({
        url: '/Account/UpdateUser',
        method: 'post',
        data: data
    })
}
export function DeleteUser(data) {
    return request({
        url: '/Account/DeleteUser',
        method: 'post',
        data: data
    })
}
export function GetMenuTree(data) {
    return request({
        url: '/Account/GetMenuTree',
        method: 'post',
        data: data
    })
}
export function GetAttributeList(data){
    return request({
        url: '/SysManage/GetAttributeList',
        method: 'post',
        data: data
    }) 
}