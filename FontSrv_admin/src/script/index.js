var signalRHelp = {
        Init: function() {
            thisVue.$store.dispatch('signalR/RegisterSignalrConnection');
            thisVue.$store.dispatch('signalR/StartSignalr');
        },
        Stop: function() {
            thisVue.$store.dispatch('signalR/Stop');
        },
        SetListenMethod: function(methodName, func) {
            thisVue.$store.dispatch('signalR/SetListenMethod', {
                methodName: methodName,
                callBack: function(data) {
                    func(data);
                }
            });
        },
        Invoke: function(methodName, data) {
            return thisVue.$store.dispatch('signalR/Invoke', { methodName: methodName, data: data })
        }
    }
    /***************全局注册***************** */
window.signalRHelp = signalRHelp;