<template>
    <div class="components-container">
        <div>
            <el-dialog title="选择图标" :before-close="dialogClose_event" :show-close="false" width="40%" :visible.sync="visibleTemp" :center="true">
                <el-row :gutter="20">
                    <el-col :span="2">
                        <el-button icon="el-icon-info" circle @click="IconCHange('el-icon-info')"></el-button>
                    </el-col>
                    <el-col :span="2">
                        <el-button icon="el-icon-error" circle @click="IconCHange('el-icon-error')"></el-button>
                    </el-col>
                    <el-col :span="2">
                        <el-button icon="el-icon-success" circle @click="IconCHange('el-icon-success')"></el-button>
                    </el-col>
                    <el-col :span="2">
                        <el-button icon="el-icon-warning" circle @click="IconCHange('el-icon-warning')"></el-button>
                    </el-col>
                    <el-col :span="2">
                        <el-button icon="el-icon-question" circle @click="IconCHange('el-icon-question')"></el-button>
                    </el-col>
                    <el-col :span="2">
                        <el-button icon="el-icon-back" circle @click="IconCHange('el-icon-back')"></el-button>
                    </el-col> 
                    <el-col :span="2">
                        <el-button icon="el-icon-arrow-left" circle @click="IconCHange('el-icon-arrow-left')"></el-button>
                    </el-col>
                    <el-col :span="2">
                        <el-button icon="el-icon-arrow-down" circle @click="IconCHange('el-icon-arrow-down')"></el-button>
                    </el-col>
                    <el-col :span="2">
                        <el-button icon="el-icon-arrow-right" circle @click="IconCHange('el-icon-arrow-right')"></el-button>
                    </el-col>
                    <el-col :span="2">
                        <el-button icon="el-icon-arrow-up" circle @click="IconCHange('el-icon-arrow-up')"></el-button>
                    </el-col>
                    <el-col :span="2">
                        <el-button icon="el-icon-caret-left" circle @click="IconCHange('el-icon-caret-left')"></el-button>
                    </el-col>
                    <el-col :span="2">
                        <el-button icon="el-icon-caret-bottom" circle @click="IconCHange('el-icon-caret-bottom')"></el-button>
                    </el-col>
                    <el-col :span="2" style="margin-top: 3px; ">
                        <el-button icon="el-icon-caret-top" circle @click="IconCHange('el-icon-caret-top')"></el-button>
                    </el-col>
                    <el-col :span="2" style="margin-top: 3px; ">
                        <el-button icon="el-icon-caret-right" circle @click="IconCHange('el-icon-caret-right')"></el-button>
                    </el-col>
                    <el-col :span="2" style="margin-top: 3px; ">
                        <el-button icon="el-icon-d-arrow-left" circle @click="IconCHange('el-icon-d-arrow-left')"></el-button>
                    </el-col>
                    <el-col :span="2" style="margin-top: 3px; ">
                        <el-button icon="el-icon-d-arrow-right" circle @click="IconCHange('el-icon-d-arrow-right')"></el-button>
                    </el-col>
                    <el-col :span="2" style="margin-top: 3px; ">
                        <el-button icon="el-icon-minus" circle @click="IconCHange('el-icon-minus')"></el-button>
                    </el-col>
                    <el-col :span="2" style="margin-top: 3px; ">
                        <el-button icon="el-icon-plus" circle @click="IconCHange('el-icon-plus')"></el-button>
                    </el-col>
                    <el-col :span="2" style="margin-top: 3px; ">
                        <el-button icon="el-icon-remove" circle @click="IconCHange('el-icon-remove')"></el-button>
                    </el-col>
                    <el-col :span="2" style="margin-top: 3px; ">
                        <el-button icon="el-icon-circle-plus" circle @click="IconCHange('el-icon-circle-plus')"></el-button>
                    </el-col>
                    <el-col :span="2" style="margin-top: 3px; ">
                        <el-button icon="el-icon-remove-outline" circle @click="IconCHange('el-icon-remove-outline')"></el-button>
                    </el-col>
                    <el-col :span="2" style="margin-top: 3px; ">
                        <el-button icon="el-icon-circle-plus-outline" circle @click="IconCHange('el-icon-circle-plus-outline')"></el-button>
                    </el-col>
                    <el-col :span="2" style="margin-top: 3px; ">
                        <el-button icon="el-icon-close" circle @click="IconCHange('el-icon-close')"></el-button>
                    </el-col>
                    <el-col :span="2" style="margin-top: 3px; ">
                        <el-button icon="el-icon-check" circle @click="IconCHange('el-icon-check')"></el-button>
                    </el-col>
                </el-row>
                <el-row :gutter="20" style="margin-top: 3px; ">
                    <el-col :span="2">
                        <el-button icon="el-icon-circle-close" circle @click="IconCHange('el-icon-circle-close')"></el-button>
                    </el-col>
                    <el-col :span="2">
                        <el-button icon="el-icon-circle-check" circle @click="IconCHange('el-icon-circle-check')"></el-button>
                    </el-col>       
                    <el-col :span="2">
                        <el-button icon="el-icon-circle-close-outline" circle @click="IconCHange('el-icon-circle-close-outline')"></el-button>
                    </el-col>
                    <el-col :span="2">
                        <el-button icon="el-icon-circle-check-outline" circle @click="IconCHange('el-icon-circle-check-outline')"></el-button>
                    </el-col>   
                    <el-col :span="2">
                        <el-button icon="el-icon-zoom-out" circle @click="IconCHange('el-icon-zoom-out')"></el-button>
                    </el-col>
                    <el-col :span="2">
                        <el-button icon="el-icon-zoom-in" circle @click="IconCHange('el-icon-zoom-in')"></el-button>
                    </el-col>   
                    <el-col :span="2">
                        <el-button icon="el-icon-d-caret" circle @click="IconCHange('el-icon-d-caret')"></el-button>
                    </el-col>
                    <el-col :span="2">
                        <el-button icon="el-icon-sort" circle @click="IconCHange('el-icon-sort')"></el-button>
                    </el-col>   
                    <el-col :span="2">
                        <el-button icon="el-icon-sort-down" circle @click="IconCHange('el-icon-sort-down')"></el-button>
                    </el-col>
                    <el-col :span="2">
                        <el-button icon="el-icon-sort-up" circle @click="IconCHange('el-icon-sort-up')"></el-button>
                    </el-col>   
                    <el-col :span="2">
                        <el-button icon="el-icon-tickets" circle @click="IconCHange('el-icon-tickets')"></el-button>
                    </el-col>
                    <el-col :span="2">
                        <el-button icon="el-icon-document" circle @click="IconCHange('el-icon-document')"></el-button>
                    </el-col>
                    <el-col :span="2" style="margin-top: 3px; ">
                        <el-button icon="el-icon-goods" circle @click="IconCHange('el-icon-goods')"></el-button>
                    </el-col>
                    <el-col :span="2" style="margin-top: 3px; ">
                        <el-button icon="el-icon-sold-out" circle @click="IconCHange('el-icon-sold-out')"></el-button>
                    </el-col>
                    <el-col :span="2" style="margin-top: 3px; ">
                        <el-button icon="el-icon-news" circle @click="IconCHange('el-icon-news')"></el-button>
                    </el-col>
                    <el-col :span="2" style="margin-top: 3px; ">
                        <el-button icon="el-icon-message" circle @click="IconCHange('el-icon-message')"></el-button>
                    </el-col>
                    <el-col :span="2" style="margin-top: 3px; ">
                        <el-button icon="el-icon-date" circle @click="IconCHange('el-icon-date')"></el-button>
                    </el-col>
                    <el-col :span="2" style="margin-top: 3px; ">
                        <el-button icon="el-icon-printer" circle @click="IconCHange('el-icon-printer')"></el-button>
                    </el-col>
                    <el-col :span="2" style="margin-top: 3px; ">
                        <el-button icon="el-icon-time" circle @click="IconCHange('el-icon-time')"></el-button>
                    </el-col>
                    <el-col :span="2" style="margin-top: 3px; ">
                        <el-button icon="el-icon-bell" circle @click="IconCHange('el-icon-bell')"></el-button>
                    </el-col>
                    <el-col :span="2" style="margin-top: 3px; ">
                        <el-button icon="el-icon-mobile-phone" circle @click="IconCHange('el-icon-mobile-phone')"></el-button>
                    </el-col>
                    <el-col :span="2" style="margin-top: 3px; ">
                        <el-button icon="el-icon-menu" circle @click="IconCHange('el-icon-menu')"></el-button>
                    </el-col>
                    <el-col :span="2" style="margin-top: 3px; ">
                        <el-button icon="el-icon-service" circle @click="IconCHange('el-icon-service')"></el-button>
                    </el-col>
                    <el-col :span="2" style="margin-top: 3px; ">
                        <el-button icon="el-icon-view" circle @click="IconCHange('el-icon-view')"></el-button>
                    </el-col>               
                </el-row>
                <el-row :gutter="20" style="margin-top: 3px; ">
                    <el-col :span="2">
                        <el-button icon="el-icon-more" circle @click="IconCHange('el-icon-more')"></el-button>
                    </el-col>
                    <el-col :span="2">
                        <el-button icon="el-icon-more-outline" circle @click="IconCHange('el-icon-more-outline')"></el-button>
                    </el-col>       
                    <el-col :span="2">
                        <el-button icon="el-icon-star-on" circle @click="IconCHange('el-icon-star-on')"></el-button>
                    </el-col>
                    <el-col :span="2">
                        <el-button icon="el-icon-star-off" circle @click="IconCHange('el-icon-star-off')"></el-button>
                    </el-col>   
                    <el-col :span="2">
                        <el-button icon="el-icon-location" circle @click="IconCHange('el-icon-location')"></el-button>
                    </el-col>
                    <el-col :span="2">
                        <el-button icon="el-icon-location-outline" circle @click="IconCHange('el-icon-location-outline')"></el-button>
                    </el-col>   
                    <el-col :span="2">
                        <el-button icon="el-icon-phone" circle @click="IconCHange('el-icon-phone')"></el-button>
                    </el-col>
                    <el-col :span="2">
                        <el-button icon="el-icon-phone-outline" circle @click="IconCHange('el-icon-phone-outline')"></el-button>
                    </el-col>   
                    <el-col :span="2">
                        <el-button icon="el-icon-picture" circle @click="IconCHange('el-icon-picture')"></el-button>
                    </el-col>
                    <el-col :span="2">
                        <el-button icon="el-icon-picture-outline" circle @click="IconCHange('el-icon-picture-outline')"></el-button>
                    </el-col>   
                    <el-col :span="2">
                        <el-button icon="el-icon-delete" circle @click="IconCHange('el-icon-delete')"></el-button>
                    </el-col>
                    <el-col :span="2">
                        <el-button icon="el-icon-search" circle @click="IconCHange('el-icon-search')"></el-button>
                    </el-col>
                    <el-col :span="2" style="margin-top: 3px; ">
                        <el-button icon="el-icon-edit" circle @click="IconCHange('el-icon-edit')"></el-button>
                    </el-col>
                    <el-col :span="2" style="margin-top: 3px; ">
                        <el-button icon="el-icon-edit-outline" circle @click="IconCHange('el-icon-edit-outline')"></el-button>
                    </el-col>
                    <el-col :span="2" style="margin-top: 3px; ">
                        <el-button icon="el-icon-rank" circle @click="IconCHange('el-icon-rank')"></el-button>
                    </el-col>
                    <el-col :span="2" style="margin-top: 3px; ">
                        <el-button icon="el-icon-refresh" circle @click="IconCHange('el-icon-refresh')"></el-button>
                    </el-col>
                    <el-col :span="2" style="margin-top: 3px; ">
                        <el-button icon="el-icon-share" circle @click="IconCHange('el-icon-share')"></el-button>
                    </el-col>
                    <el-col :span="2" style="margin-top: 3px; ">
                        <el-button icon="el-icon-setting" circle @click="IconCHange('el-icon-setting')"></el-button>
                    </el-col>
                    <el-col :span="2" style="margin-top: 3px; ">
                        <el-button icon="el-icon-upload" circle @click="IconCHange('el-icon-upload')"></el-button>
                    </el-col>
                    <el-col :span="2" style="margin-top: 3px; ">
                        <el-button icon="el-icon-upload2" circle @click="IconCHange('el-icon-upload2')"></el-button>
                    </el-col>
                    <el-col :span="2" style="margin-top: 3px; ">
                        <el-button icon="el-icon-download" circle @click="IconCHange('el-icon-download')"></el-button>
                    </el-col>
                    <el-col :span="2" style="margin-top: 3px; ">
                        <el-button icon="el-icon-loading" circle @click="IconCHange('el-icon-loading')"></el-button>
                    </el-col>            
                </el-row>
                 <div slot="footer" class="dialog-footer">
                    <el-button @click="dialogClose_event">关 闭</el-button>
                </div>
            </el-dialog>
        </div>
    </div>
</template>
<script>
import { mapGetters } from 'vuex'
export default {
    data(){
        return{
            visibleTemp:false,
        }
    },
    props:{
        visible:{
            type: Boolean,
            default: false,
        },
    },
    watch: {
        visible:function(){
            this.visibleTemp=this.visible;
        }
    },
    methods:{
        IconCHange(val){
            this.$emit('select_IconVal',val);
        },
        dialogClose_event:function(){
            this.$emit('update:visible', false);
        }
    }
}
</script>