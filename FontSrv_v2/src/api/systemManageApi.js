import request from '@/utils/request'
// import { getParam } from '@/utils/StringHandle'
export function GetUserList(data) {
    return request({
        url: '/Account/GetUserList',
        method: 'post',
        data: data
    })
}