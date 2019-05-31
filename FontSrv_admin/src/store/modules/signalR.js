import * as signalr from "@aspnet/signalr"
import { getToken } from '@/utils/auth'
import { stat } from "fs";
const state = {
    connection: null,
    methodNameList: [],
    isFirstConnection: true, //
}
const mutations = {
    m_Set_Connection: (state, connection) => {
        state.connection = connection;
    },
    m_set_ListenMethod: (state, listenMethod) => {
        if (!listenMethod) return;
        if (!listenMethod.methodName) return;
        if (listenMethod.methodName == "") return;
        if (!listenMethod.callBack) return;
        if (state.methodNameList.includes(listenMethod.methodName)) {
            state.connection.off(listenMethod.methodName);
        } else {
            state.methodNameList.push(listenMethod.methodName);
        }
        state.connection.on(listenMethod.methodName, listenMethod.callBack);
    },
    m_set_IsFirstConnection: (state, isFirstConnection) => {
        state.isFirstConnection = isFirstConnection;
    }
}
const actions = {
    //, { accessTokenFactory: () => getToken().replace("Bearer", "") }
    RegisterSignalrConnection: ({ commit, state }) => {
        var connection = null;
        if (!state.connection) {
            //| signalr.HttpTransportType.WebSockets
            connection = new signalr.HubConnectionBuilder()
                .withUrl(process.env.VUE_APP_BASE_API + "/notificationhub", {
                    transport: signalr.HttpTransportType.LongPolling,
                    accessTokenFactory: () => (getToken() ? getToken() : "").replace("Bearer", "")
                })
                .configureLogging(signalr.LogLevel.Warning)
                .build();
            connection.onclose(function() {
                connection.start();
            })
            commit('m_Set_Connection', connection);
        }

    },
    StartSignalr: ({ commit, state }) => {
        if (state.connection.connectionState == 0 && state.isFirstConnection) {
            commit('m_set_IsFirstConnection', false);
            state.connection.start().then(res => {
                console.log('Hub connection started')
            }).catch(err => {
                console.log('Error while establishing connection' + err)
            });
        }
    },
    SetListenMethod: ({ dispatch, commit, state }, listenMethod) => {
        commit('m_set_ListenMethod', listenMethod);
    },
    Invoke: ({ state }, obj) => {
        if (obj.data) {
            return state.connection.invoke(obj.methodName, obj.data);
        }
        return state.connection.invoke(obj.methodName);

    },
    Stop: ({ state }) => {
        if (state.connection.connectionState == 1) {
            state.connection.stop();
        }

    }
}
export default {
    namespaced: true,
    state,
    mutations,
    actions
}