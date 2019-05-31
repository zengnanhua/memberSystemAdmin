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
            signalRHelp.Invoke("SendMessage","signalrddd").then(res=>{
                console.info(res);
            }).catch(err=>{
                console.info(err);
            });
            signalRHelp.Invoke("SendMessage1","ccc").then(res=>{
                console.info(res);
            }).catch(err=>{
                console.info(err);
            });
            
        }
    },
    mounted(){
        signalRHelp.SetListenMethod("ReceiveMessage",function(res){
            console.info(res);
        });
        signalRHelp.SetListenMethod("ReceiveMessage1",function(res){
            console.info(res);
        });
    }
}
</script>
