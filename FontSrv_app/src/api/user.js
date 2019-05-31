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
        avatar: 'https://ss1.bdstatic.com/70cFuXSh_Q1YnxGkpoWK1HF6hhy/it/u=1126645554,2657561633&fm=26&gp=0.jpg',
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