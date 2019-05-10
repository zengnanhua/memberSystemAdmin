import * as signalr from "@aspnet/signalr"
import { getToken } from '@/utils/auth'
const state = {
    connection: null,
    methodNameList: [],
}
const mutations = {
    m_Set_Connection: (state, connection) => {
        state.connection = connection;
        console.info(3);
    },
    m_set_ListenMethod: (state, listenMethod) => {
        if (!listenMethod) return;
        if (!listenMethod.methodName) return;
        if (listenMethod.methodName == "") return;
        if (!listenMethod.callBack) return;
        if (state.methodNameList.includes(listenMethod.methodName)) {
            state.connection.off(listenMethod.methodName);
        }
        state.methodNameList.push(listenMethod.methodName);
        state.connection.on(listenMethod.methodName, listenMethod.callBack);
    }
}
const actions = {
    //, { accessTokenFactory: () => getToken().replace("Bearer", "") }
    InitSignalrConnection: ({ commit }) => {
        var connection = new signalr.HubConnectionBuilder().withUrl(process.env.VUE_APP_BASE_API + "/chatHub", { accessTokenFactory: () => getToken().replace("Bearer", "") })
            .configureLogging(signalr.LogLevel.Warning)
            .build();
        connection.start();
        connection.onclose(function() {
            console.info("链接关闭了");
            connection.start();
        })
        console.info(1);
        commit('m_Set_Connection', connection);

    },
    SetListenMethod: ({ commit }, listenMethod) => {
        console.info(2);
        commit('m_set_ListenMethod', listenMethod);
    }
}
export default {
    namespaced: true,
    state,
    mutations,
    actions
}