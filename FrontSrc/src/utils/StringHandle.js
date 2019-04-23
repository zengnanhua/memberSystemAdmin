
// 参数序列化
export function getParam(param) {
  var retParams = new Array()
  for (var key in param) {
    retParams.push(key + '=' + encodeURIComponent((param[key] == null || param[key] == undefined) ? '' : param[key]))
  }
  return retParams.join('&')
}
export function getParamNotSignature(param) {
  var retParams = new Array()
  for (var key in param) {
    retParams.push(key + '=' + param[key])
  }
  return retParams.join('&')
}
// 转换为json
export function objToJson(obj) {
  if (obj && obj != '') {
    return JSON.stringify(obj)
  }
  return ''
}
// md5加密
export function md5Encry(str) {
  if (str && str != '') {
    return new MD5().update(obj).digest('hex')
  }
  return ''
}
