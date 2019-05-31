import Cookies from 'js-cookie'
import { getLanguage } from '@/lang/index'

const state = {
    device: 'desktop',
    language: getLanguage(),
}

const mutations = {

}

const actions = {

}

export default {
    namespaced: true,
    state,
    mutations,
    actions
}