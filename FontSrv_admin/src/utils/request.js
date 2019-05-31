import axios from 'axios'
import { MessageBox, Message } from 'element-ui'
import store from '@/store'
import { getToken } from '@/utils/auth'

// create an axios instance
const service = axios.create({
    baseURL: process.env.VUE_APP_BASE_API, // url = base url + request url
    withCredentials: true, // send cookies when cross-domain requests
    timeout: 60 * 1000 // request timeout
})

// request interceptor
service.interceptors.request.use(
    config => {
        // do something before request is sent

        if (store.getters.token) {
            // let each request carry token --['X-Token'] as a custom key.
            // please modify it according to the actual situation.
            //config.headers['X-Token'] = getToken()
            config.headers['Authorization'] = getToken()

        }
        return config
    },
    error => {
        // do something with request error
        console.log(error) // for debug
        return Promise.reject(error)
    }
)

// response interceptor
service.interceptors.response.use(
    /**
     * If you want to get information such as headers or status
     * Please return  response => response
     */

    /**
     * Determine the request status by custom code
     * Here is just an example
     * You can also judge the status by HTTP Status Code.
     */
    response => {
        const res = response.data
        if (res.resultCode == "10000") {
            return res;
        } else {
            Message({
                message: res.resultMessage || 'error',
                type: 'error',
                duration: 3 * 1000
            })
            return Promise.reject(res)
        }

    },
    error => {
        console.info(error);
        var errStr = 'err' + error;
        console.log(errStr) // for debug
        if (errStr.indexOf('code') > 0 && errStr.indexOf('401') > 0) {
            // to re-login
            MessageBox.confirm('你已被登出，可以取消继续留在该页面，或者重新登录', '确定登出', {
                confirmButtonText: '重新登录',
                cancelButtonText: '取消',
                type: 'warning'
            }).then(() => {
                store.dispatch('user/resetToken').then(() => {
                    location.reload()
                })
            })
        } else {
            Message({
                message: error.message,
                type: 'error',
                duration: 5 * 1000
            })
        }

        //return Promise.reject(error)
    }
)

export default service