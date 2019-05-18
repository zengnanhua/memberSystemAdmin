(window["webpackJsonp"]=window["webpackJsonp"]||[]).push([["chunk-92cd9100"],{"01e6":function(n,t,e){},2017:function(n,t,e){"use strict";var o=e("3b76"),s=e.n(o);s.a},"3b76":function(n,t,e){},"9c06":function(n,t,e){},"9ed6":function(n,t,e){"use strict";e.r(t);var o=function(){var n=this,t=n.$createElement,e=n._self._c||t;return e("div",{staticClass:"login-container"},[e("el-form",{ref:"loginForm",staticClass:"login-form",attrs:{model:n.loginForm,rules:n.loginRules,"auto-complete":"on","label-position":"left"}},[e("div",{staticClass:"title-container"},[e("h3",{staticClass:"title"},[n._v("\n        "+n._s(n.$t("login.title"))+"\n      ")])]),n._v(" "),e("el-form-item",{attrs:{prop:"username"}},[e("span",{staticClass:"svg-container"},[e("svg-icon",{attrs:{"icon-class":"user"}})],1),n._v(" "),e("el-input",{ref:"username",attrs:{placeholder:n.$t("login.username"),name:"username",type:"text",tabindex:"1","auto-complete":"on"},model:{value:n.loginForm.username,callback:function(t){n.$set(n.loginForm,"username",t)},expression:"loginForm.username"}})],1),n._v(" "),e("el-tooltip",{attrs:{content:"Caps lock is On",placement:"right",manual:""},model:{value:n.capsTooltip,callback:function(t){n.capsTooltip=t},expression:"capsTooltip"}},[e("el-form-item",{attrs:{prop:"password"}},[e("span",{staticClass:"svg-container"},[e("svg-icon",{attrs:{"icon-class":"password"}})],1),n._v(" "),e("el-input",{key:n.passwordType,ref:"password",attrs:{type:n.passwordType,placeholder:n.$t("login.password"),name:"password",tabindex:"2","auto-complete":"on"},on:{blur:function(t){n.capsTooltip=!1}},nativeOn:{keyup:[function(t){return n.checkCapslock(t)},function(t){return!t.type.indexOf("key")&&n._k(t.keyCode,"enter",13,t.key,"Enter")?null:n.handleLogin(t)}]},model:{value:n.loginForm.password,callback:function(t){n.$set(n.loginForm,"password",t)},expression:"loginForm.password"}}),n._v(" "),e("span",{staticClass:"show-pwd",on:{click:n.showPwd}},[e("svg-icon",{attrs:{"icon-class":"password"===n.passwordType?"eye":"eye-open"}})],1)],1)],1),n._v(" "),e("el-button",{staticStyle:{width:"100%","margin-bottom":"30px"},attrs:{loading:n.loading,type:"primary"},nativeOn:{click:function(t){return t.preventDefault(),n.handleLogin(t)}}},[n._v("\n      "+n._s(n.$t("login.logIn"))+"\n    ")])],1),n._v(" "),e("el-dialog",{attrs:{title:n.$t("login.thirdparty"),visible:n.showDialog},on:{"update:visible":function(t){n.showDialog=t}}},[n._v("\n    "+n._s(n.$t("login.thirdpartyTips"))+"\n    "),e("br"),n._v(" "),e("br"),n._v(" "),e("br"),n._v(" "),e("social-sign")],1)],1)},s=[],i=e("61f7"),a=e("1131"),r=function(){var n=this,t=n.$createElement,e=n._self._c||t;return e("div",{staticClass:"social-signup-container"},[e("div",{staticClass:"sign-btn",on:{click:function(t){return n.wechatHandleClick("wechat")}}},[e("span",{staticClass:"wx-svg-container"},[e("svg-icon",{staticClass:"icon",attrs:{"icon-class":"wechat"}})],1),n._v("\n    WeChat\n  ")]),n._v(" "),e("div",{staticClass:"sign-btn",on:{click:function(t){return n.tencentHandleClick("tencent")}}},[e("span",{staticClass:"qq-svg-container"},[e("svg-icon",{staticClass:"icon",attrs:{"icon-class":"qq"}})],1),n._v("\n    QQ\n  ")])])},c=[],l={name:"SocialSignin",methods:{wechatHandleClick:function(n){alert("ok")},tencentHandleClick:function(n){alert("ok")}}},u=l,p=(e("edc1"),e("2877")),d=Object(p["a"])(u,r,c,!1,null,"c817cede",null),g=d.exports,f={name:"Login",components:{LangSelect:a["a"],SocialSign:g},data:function(){var n=function(n,t,e){Object(i["d"])(t)?e():e(new Error("Please enter the correct user name"))},t=function(n,t,e){t.length<5?e(new Error("The password can not be less than 6 digits")):e()};return{loginForm:{username:"admin",password:"123456"},loginRules:{username:[{required:!0,trigger:"blur",validator:n}],password:[{required:!0,trigger:"blur",validator:t}]},passwordType:"password",capsTooltip:!1,loading:!1,showDialog:!1,redirect:void 0}},watch:{$route:{handler:function(n){this.redirect=n.query&&n.query.redirect},immediate:!0}},created:function(){},mounted:function(){""===this.loginForm.username?this.$refs.username.focus():""===this.loginForm.password&&this.$refs.password.focus()},destroyed:function(){},methods:{checkCapslock:function(){var n=arguments.length>0&&void 0!==arguments[0]?arguments[0]:{},t=n.shiftKey,e=n.key;e&&1===e.length&&(this.capsTooltip=!!(t&&e>="a"&&e<="z"||!t&&e>="A"&&e<="Z")),"CapsLock"===e&&!0===this.capsTooltip&&(this.capsTooltip=!1)},showPwd:function(){var n=this;"password"===this.passwordType?this.passwordType="":this.passwordType="password",this.$nextTick(function(){n.$refs.password.focus()})},handleLogin:function(){var n=this;this.$refs.loginForm.validate(function(t){if(!t)return console.log("error submit!!"),!1;n.loading=!0,n.$store.dispatch("user/login",n.loginForm).then(function(){n.OutOthenLogin()}).catch(function(){n.loading=!1})})},OutOthenLogin:function(){signalRHelp.Init();var n=this;setTimeout(function(){n.$router.push({path:n.redirect||"/"}),n.loading=!1,signalRHelp.Invoke("singleLogin").then(function(n){console.info(n)}).catch(function(n){console.info(n)})},1e3)}}},h=f,m=(e("2017"),e("e1b6"),Object(p["a"])(h,o,s,!1,null,"7f79538a",null));t["default"]=m.exports},e1b6:function(n,t,e){"use strict";var o=e("9c06"),s=e.n(o);s.a},edc1:function(n,t,e){"use strict";var o=e("01e6"),s=e.n(o);s.a}}]);