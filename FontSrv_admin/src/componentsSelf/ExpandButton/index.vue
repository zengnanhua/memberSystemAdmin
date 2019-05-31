<template>
    <el-button type="text" @click="click_event">{{isExpand?"收起":"展开"}}<i :class="isExpand?'el-icon-arrow-up':'el-icon-arrow-down'"></i></el-button>
</template>
<script>
export default {
    data(){
        return{
            isExpand:false,
        };
    },
    methods:{
        click_event:function(){
            this.isExpand=!this.isExpand;
            this.$emit('click',this.isExpand)
        }
    }
}
</script>
<style lang="scss" scoped>

</style>

