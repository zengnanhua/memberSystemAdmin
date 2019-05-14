import * as signalr from "@aspnet/signalr"
import { getToken } from '@/utils/auth'
import { stat } from "fs";
const state = {
    connection: null,
    methodNameList: [],
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
    }
}
const actions = {
    //, { accessTokenFactory: () => getToken().replace("Bearer", "") }
    InitSignalrConnection: ({ commit, state }, funcBackCall) => {
        var connection = null;
        if (!state.connection) {
            //| signalr.HttpTransportType.WebSockets
            connection = new signalr.HubConnectionBuilder()
                .withUrl(process.env.VUE_APP_BASE_API + "/chatHub", {
                    transport: signalr.HttpTransportType.LongPolling,
                    accessTokenFactory: () => getToken().replace("Bearer", "")
                })
                .configureLogging(signalr.LogLevel.Warning)
                .build();
            connection.onclose(function() {
                connection.start();
            })
            commit('m_Set_Connection', connection);
        }

        if (state.connection.connectionState == 0) {
            try {
                state.connection.start().then(res => {

                    if (funcBackCall) {
                        funcBackCall();
                    }
                }).catch(err => {

                });
            } catch (e) {}

        } else {
            if (funcBackCall) {
                funcBackCall();
            }
        }



    },
    SetListenMethod: ({ dispatch, commit, state }, listenMethod) => {
        if (!state.connection || state.connection.connectionState == 0) {
            dispatch("InitSignalrConnection", function() {
                commit('m_set_ListenMethod', listenMethod);
            });
        } else {
            commit('m_set_ListenMethod', listenMethod);
        }
    },
    Invoke: ({ dispatch, state }, obj) => {
        var invokeFunc = function() {
            if (obj.methodName && !obj.data) {
                state.connection.invoke(obj.methodName).then(res => {
                    if (obj.func) {
                        obj.func(res);
                    }
                }).catch(err => {});
            } else if (obj.methodName && obj.data) {
                state.connection.invoke(obj.methodName, obj.data).then(res => {
                    if (obj.func) {
                        obj.func(res);
                    }
                }).catch(err => {});
            }
        }

        if (!state.connection || state.connection.connectionState == 0) {
            dispatch("InitSignalrConnection", function() {
                invokeFunc();
            });
        } else {
            invokeFunc();
        }
    }
}
export default {
    namespaced: true,
    state,
    mutations,
    actions
}