<template>  
    <div>   
        <el-row>
            <el-col :span="10">
                <el-input placeholder="请输入地名" v-model="searchTxt" class="input-with-select" style="margin-bottom:10px">
                    <el-button slot="append" icon="el-icon-search" @click="search_event">搜索</el-button>
                </el-input>
            </el-col>
        </el-row>
        <div id="allmap" v-bind:style="showStyle"></div>  
    </div>  
</template>  
<script>
//   import BMap from 'BMap'  
  export default {
    
    data(){
        return {
            showStyle:{'width':'100%','height':'300px','border':'1px solid gray','overflow':'hidden' },
            clongitude:0,
            clatitude:0,
            map : null,
            marker:null,
            searchTxt:"",
        }
    },
    props:{
     
      // 地图在该视图上的高度
      mapHeight:{
        type:Number,
        default:500
      },
      // 经度
      longitude:{
        type:String,
        default:116.404
      },
      // 纬度
      latitude:{
        type:String,
        default:39.915
      },
      description:{
        type:String,
        default:'天安门'
      },
      isEdit:{
          type:Boolean,
          default:true
      }
    },
    watch:{
        longitude:function(){
            this.Init();
        },
        latitude:function(){
            this.Init();
        }
    },
    methods: {
        Init:function(){
            if((typeof BMap)+""=="undefined"){
              console.info("地图没有加载");
              return "";
            }
            this.map =new BMap.Map("allmap");
          
            this.IsSetDefault();
            
            var point =new BMap.Point(this.clongitude,this.clatitude);
            this.marker =new BMap.Marker(point);
            this.map.addOverlay(this.marker);


            this.map.centerAndZoom(point,15);
            // 信息窗的配置信息
            var opts ={
                width :250,
                height:75,
                title :"地址：",
            }
            this.map.enableScrollWheelZoom(this.isEdit) ;
            //var infoWindow =BMap.InfoWindow(this.description, opts);// 创建信息窗口对象
            
            this.map.enableScrollWheelZoom(this.isEdit);
            this.map.openInfoWindow(null,point);//开启信息窗口

            var that = this;
            if(this.isEdit){
                this.map.addEventListener("click",function(e){
                    that.SetPoint(e.point.lng,e.point.lat);
                    
                        //that.$emit("reset-map",that.clongitude,that.clatitude,that.longitude,that.latitude);  
                });
            }
        },
        search_event:function(){
            var local = new BMap.LocalSearch(this.map, {
                renderOptions:{map: this.map}
            });
            local.search(this.searchTxt);
        },
        IsSetDefault:function(){
            if(this.longitude==null||this.longitude==""){
                this.clongitude=116.404
            }
            else{
                this.clongitude=this.longitude;
            }
            this.$emit('update:longitude', this.clongitude+"");
            if(this.latitude==null||this.latitude==""){
                this.clatitude=39.915
            }
            else{
                this.clatitude=this.latitude;
            }
            this.$emit('update:latitude', this.clatitude+"");
        },
        SetPoint:function(longitude,latitude){
            if(longitude==null||longitude==""||latitude==null||latitude==""){
                return;
            }
          
            //移除旧对象
            this.map.removeOverlay(this.marker);

            this.clongitude = longitude;
            this.clatitude = latitude;

            var point =new BMap.Point(this.clongitude,this.clatitude);
            this.marker =new BMap.Marker(point);
            this.map.addOverlay(this.marker);
            this.$emit('update:longitude', this.clongitude+"");
            this.$emit('update:latitude', this.clatitude+"");
        }
    },
    mounted(){
        this.Init();

    }
  }
</script>

<style scoped>

</style>