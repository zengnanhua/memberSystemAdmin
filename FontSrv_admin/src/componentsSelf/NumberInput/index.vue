<template>
    <div>
        <el-input  v-model="tempValue"  ref='input_num' multiple   :placeholder="placeholder" :disabled="disabled" @blur="blur" style="width:100%">
            <slot></slot>
        </el-input>
    </div>
</template>
<script>
    export default {
        data() {
            return {
                tempValue: "",
            };
        },
        props: {
            value: {
                type: [String, Number],
                default: ""
            },
            precision:{
                type: [String, Number],
                default: 0
            },
            disabled:{
                type: Boolean,
                default: false
            },
            placeholder: ""
        },
        watch: {
            value: function() {
                this.GetValue();
            },
            tempValue: function() {
                this.SetValue();
            }
        },
        methods: {
            GetValue: function() {
                if (this.value == null || this.value == "") {
                    this.tempValue = "";
                    return;
                }
                var str="";
                if(this.precision==0||this.precision=="0"){
                    str=(this.value+"").replace(/[^0-9]/ig,"");
                }
                else{
                    str=(this.value+"").replace(/[^\d.]/g, "").replace(/^\./g, "").replace(/\.{2,}/g, ".")
                        .replace(".", "$#$").replace(/\./g, "").replace("$#$", ".").replace(/^(\-)*(\d+)\.(\d\d).*$/, '$1$2.$3');
                }
         
                this.$nextTick(() => { 
                    this.tempValue = str;
                });
                
            },
            SetValue: function() {
                var str = this.tempValue;
                this.$emit('input', str);
                  
            },
            blur:function(ee){
                this.$emit('blur',ee);
            },
            focus:function(){
                this.$refs.input_num.focus();
            }
           
        },
        mounted() {
            this.GetValue();
        },
    }
</script>
<style lang="scss" scoped>

</style>