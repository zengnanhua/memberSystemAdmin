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
        closeSelectedTag() {
            var view=this.$route;

            this.$store.dispatch('tagsView/delView', view).then(({ visitedViews }) => {
                const latestView = visitedViews.slice(-1)[0]
                if (latestView) {
                    this.$router.push(latestView);
                    this.refreshSelectedTag(latestView);
                }
                
                //this.toLastView(visitedViews, view)
            })
        },
        toLastView(visitedViews, view) {
            const latestView = visitedViews.slice(-1)[0]
            if (latestView) {
                this.$router.push(latestView)
            } else {
                // now the default is to redirect to the home page if there is no tags-view,
                // you can adjust it according to your needs.
                if (view.name === 'Dashboard') {
                // to reload home page
                    this.$router.replace({ path: '/redirect' + view.fullPath })
                } else {
                    this.$router.push('/')
                }
            }
        },
        refreshSelectedTag(view) {
            this.$store.dispatch('tagsView/delCachedView', view).then(() => {
                const { fullPath } = view
                this.$nextTick(() => {
                    this.$router.replace({
                        path: '/redirect' + fullPath
                    })
                })
            })
        },
    }
}
</script>

