<template>
    <div style="height:100%">
        <el-container>
            <el-main>
                <el-row :gutter="20">
                    <el-col :span="6">
                        <el-card class="box-card" shadow="hover" >
                            <div slot="header">
                                <el-input
                                placeholder="输入关键字进行过滤"
                                v-model="filterText">
                                </el-input>
                            </div>
                            <div style="height:600px;overflow-y: scroll;">
                                <el-tree
                                    class="filter-tree"
                                    node-key="MenuNo"
                                    :data="menuTree"
                                    :props="defaultProps"
                                    accordion
                                     :expand-on-click-node="false"
                                    default-expand-all
                                    @node-click="handleNodeClick"
                                    :filter-node-method="filterNode"
                                    ref="tree1">
                                </el-tree>
                            </div>
                        </el-card>                
                    </el-col>
                    <el-col :span="18">
                        <el-button type="primary"  icon="el-icon-plus" >添 加</el-button>
                        <el-table :data="paginationEntity.tableData"   highlight-current-row style="width: 100%; margin-top: 5px">
                            <el-table-column type="index" width="50"></el-table-column>
                            <el-table-column prop="MenuNo" label="菜单编号" width="150" align="center"></el-table-column>
                            <el-table-column prop="MenuName" label="菜单名" width="150" align="center"></el-table-column>
                            <el-table-column prop="Order" label="排序" align="center"></el-table-column>    
                        </el-table>
                        <pagination v-show="paginationEntity.total>0" :total="paginationEntity.total" :page.sync="paginationEntity.currentPage" 
                            :limit.sync="paginationEntity.currentSize" @pagination="getList" />
                    </el-col>
                </el-row>
            </el-main>
        </el-container>
    </div>
</template>

<script>
import {GetMenuTree} from "@/api/systemManageApi"
import Pagination from '@/components/Pagination'
import waves from '@/directive/waves'
export default {
    name:"menuManage",
    components: { Pagination },
    directives: { waves },
    data() {
        return {
            loadObj:{
                table_load:false,
                save_load:false,
            },
            filterText:"",
            paginationEntity:{
                currentPage:1,
                currentSize:20,
                tableData:[],
                total:0,
            },
            menuTree:[],
            defaultProps:{
                children: 'Children',
                label: 'MenuName'
            }
        }
    },
    watch: {
      filterText(val) {
        this.$refs.tree1.filter(val);
      }
    },
    methods: {
        filterNode(value, data) {
            if (!value) return true;
            return data.MenuName.indexOf(value) !== -1;
        },
        handleNodeClick:function(data){
            if(data.Children==null||data.Children.length<=0){
                this.paginationEntity.tableData=new Array(data);
                this.paginationEntity.total=1;
            }
            else{
                this.paginationEntity.tableData=data.Children;
                this.paginationEntity.total=data.Children.length;
            }  
    
        },
        getList:function(){

        },
        GetMenuTree:function(){
            GetMenuTree({}).then(res=>{
                this.menuTree=new Array(res.data);
            }).then(err=>{

            })
        }
    },
    mounted:function(){
        console.info(1);
        this.GetMenuTree();
    }
}
</script>
<style scoped>
::-webkit-scrollbar {
  width: 14px;
  height: 14px;
}
 
::-webkit-scrollbar-track,
::-webkit-scrollbar-thumb {
  border-radius: 999px;
  border: 5px solid transparent;
}
/* 
    ::-webkit-scrollbar-track {
  box-shadow: 1px 1px 5px rgba(0, 0, 0, 0.2) inset;
} */
 
::-webkit-scrollbar-thumb {
  min-height: 20px;
  background-clip: content-box;
  box-shadow: 0 0 0 5px rgba(0, 0, 0, 0.2) inset;
}
::-webkit-scrollbar-corner {
  background: transparent;
}

</style>