<template>
    <div>
        <el-container>
            <el-main >
                {{message}}
                <el-button @click="test">点击我</el-button>
            </el-main>
        </el-container>
    </div>
</template>
<script>
import * as signalr from "@aspnet/signalr"
import {GetUserList,CreateUser,UpdateUser,DeleteUser} from "@/api/systemManageApi"
import Pagination from '@/components/Pagination'
import waves from '@/directive/waves'
import { mapGetters } from 'vuex'
export default {
    name:"roleManage",
    components: { Pagination },
    directives: { waves },
    data() {
        return {
            connection:null,
            message:"asdfdddddfgsdfdad",
        }
    },
    computed: {
        ...mapGetters([
            'signalRConnection',
        ])
    },
    methods:{
        test:function(){
            this.signalRConnection.invoke('SendMessage','signalR').catch(function(err){
            });
        }
    },
    created(){
        
    },
    mounted(){
        var _this=this;
        this.$store.dispatch('signalR/SetListenMethod',{methodName:"ReceiveMessage",callBack:function(data){
            _this.message=data;
            console.info(data);
        }});
       
        
    }
}
</script>
