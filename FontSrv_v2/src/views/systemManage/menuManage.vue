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
                            <div style="height:700px;overflow-y: scroll;">
                                <div style="height:700px">asdfad</div>
                                <!-- <el-tree
                                class="filter-tree"
                                :data="Treedata"
                                @node-click="handleNodeClick"
                                :props="defaultProps"
                                default-expand-all
                                :filter-node-method="filterNode"
                                ref="tree1">
                                </el-tree> -->
                            </div>
                        </el-card>                
                    </el-col>
                    <el-col :span="18">
                        <el-button type="primary"  icon="el-icon-plus" >添 加</el-button>
                        <el-table   highlight-current-row style="width: 100%; margin-top: 5px">
                            <el-table-column type="index" width="50"></el-table-column>
                            <el-table-column prop="Menuno" label="菜单编号" width="150" align="center"></el-table-column>
                            <el-table-column prop="MenuName" label="菜单名" width="150" align="center"></el-table-column>
                            <el-table-column prop="MenuOrder" label="排序" align="center"></el-table-column>    
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
import Pagination from '@/components/Pagination'
import waves from '@/directive/waves'
export default {
    name:"menuManage1",
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
                total:10,
            },
        }
    },
    methods: {
        getList:function(){

        }
    },
    mounted:function(){

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