import request from '@/utils/request'
// import { getParam } from '@/utils/StringHandle'
export function GetMenu(data) {

    return request({
        url: '/Account/GetPageMenu',
        method: 'post',
        data: data
    })
}