<template>
    <div>
        <el-select  v-model="tempValue"  multiple  :collapse-tags="collapseTags"  :disabled="disabled" :placeholder="placeholder" style="width:100%">
            <slot></slot>
        </el-select>
    </div>
</template>
<script>
export default {
    data(){
        return{
            tempValue:[],
        };
    },
    props:{
        value:{
            type:String,
            default:""
        },
        collapseTags:{
            type:Boolean,
            default:false
        },
        placeholder:"",
        disabled:{
            type:Boolean,
            default:false
        },
    },
    watch:{
        value:function(){
            this.GetValue();
        },
        tempValue:function(){
            this.SetValue();
        }
    },
    methods:{
        GetValue:function(){
            if(this.value==null||this.value==""){
                this.tempValue=new Array();
                return;
            }
            this.tempValue=this.value.split(';');
        },
        SetValue:function(){
            var str= this.tempValue.join(";");
            this.$emit('input', str);
        }
    },
    mounted() {
        this.GetValue();
    },
}
</script>
<style lang="scss" scoped>

</style>

