<template>
    <div>
        <el-button @click="close">关闭自己</el-button>
    </div>
</template>
<script>
export default {
    data(){
        return {
            
        }
    },
    methods:{
        close:function(){
            this.$store.dispatch('tagsView/closeSelfAndRefresh', {_this:this,name:"用户管理"});
        },
    }
}
</script>

