import request from '@/utils/request'

const tokens = {
    admin: {
        token: 'admin-token'
    },
    editor: {
        token: 'editor-token'
    }
}
const users = {
    'admin-token': {
        roles: ['admin'],
        introduction: 'I am a super administrator',
        avatar: 'https://wpimg.wallstcn.com/f778738c-e4f8-4870-b634-56703b4acafe.gif',
        name: 'Super Admin'
    },
}

export function login(info) {
    const data = {
        identityName: info.username,
        password: info.password
    }
    return request({
            url: '/Account/GetToken',
            method: 'post',
            data: data
        })
        // return new Promise((resolve, reject) => {
        //         resolve({
        //             code: 20000,
        //             data: tokens["admin"]
        //         });
        //     })
        // return request({
        //   url: '/user/login',
        //   method: 'post',
        //   data
        // })
}

export function getInfo(token) {
    return new Promise((resolve, reject) => {
        resolve({
            code: 20000,
            data: users["admin-token"]
        });
    });
    // return request({
    //     url: '/user/info',
    //     method: 'get',
    //     params: { token }
    // })
}

export function logout() {
    return new Promise((resolve, reject) => {

        resolve({
            code: 20000,
            data: 'success'
        });
    });

    // return request({
    //     url: '/user/logout',
    //     method: 'post'
    // })
}