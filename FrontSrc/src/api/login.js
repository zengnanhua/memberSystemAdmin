import request from '@/utils/request'
import { getParam } from '@/utils/StringHandle'

export function loginByUsername(username, password) {
  // const data = {
  //     username,
  //     password
  // }
  // return request({
  //     url: '/login/login',
  //     method: 'post',
  //     data
  // })
  const data = {
    identityName: username,
    password: password
  }
  return request({
    url: '/Account/GetToken',
    method: 'post',
    data: data
  })
}

export function logout() {
  return request({
    url: '/login/logout',
    method: 'post'
  })
}

export function getUserInfo(token) {
  return request({
    url: '/user/info',
    method: 'get',
    params: { token }
  })
}
