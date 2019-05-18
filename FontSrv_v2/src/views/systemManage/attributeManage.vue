<template>
    <div>
        <el-container>
            <el-main>
                <el-row :gutter="20">
                    <el-col :span="10">
                        <el-card class="box-card" shadow="hover" >
                            <div slot="header">

                            </div>
                            <div>
                                <el-table :data="paginationEntity.tableData"  v-loading="loadObj.table_load"
                                    @row-click="row_click_event"  highlight-current-row style="width: 100%; margin-top: 5px">
                                    <el-table-column type="index" label="序号"  width="50"></el-table-column>
                                    <el-table-column prop="AttrCode" label="属性编码" width="150" align="center"></el-table-column>
                                    <el-table-column prop="AttrDescr" label="属性名称" align="center"></el-table-column>
                                </el-table>
                                <pagination v-show="paginationEntity.total>0" :total="paginationEntity.total" :page.sync="paginationEntity.currentPage" 
                                    :limit.sync="paginationEntity.currentSize" @pagination="getList" />
                            </div>
                        </el-card>
                    </el-col>
                    <el-col :span="10" :offset="2">
                        <el-card class="box-card" shadow="hover" >
                            <div slot="header">

                            </div>
                            <div>
                                <el-table :data="twoTableData"   highlight-current-row style="width: 100%; margin-top: 5px">
                                    <el-table-column type="index" label="序号"  width="50"></el-table-column>
                                    <el-table-column prop="AttrValue" label="属性值" width="150" align="center"></el-table-column>
                                    <el-table-column prop="AttrText" label="属性名" align="center"></el-table-column>
                                </el-table>
                                
                            </div>
                        </el-card>
                    </el-col>
                </el-row>
            </el-main>
        </el-container>
    </div>
</template>
<script>
import {GetAttributeList} from "@/api/systemManageApi"
import Pagination from '@/components/Pagination'
import waves from '@/directive/waves'
export default {
    name:"attributeManage1",
    components: { Pagination },
    directives: { waves },
    data() {
        return{
            loadObj:{
                table_load:false,
            },
            paginationEntity:{
                currentPage:1,
                currentSize:20,
                tableData:[],
                total:0,
            },
            twoTableData:[]
        }
    },
    methods:{
        getList:function(){
            var param={
                page:this.paginationEntity.currentPage,
                pageSize:this.paginationEntity.currentSize
            };
            this.loadObj.table_load=true;
            GetAttributeList(param).then(res=>{
                this.loadObj.table_load=false;
                this.paginationEntity.tableData=res.data;
                this.paginationEntity.total=res.extend;
            }).catch(err=>{
                this.loadObj.table_load=false;
                this.$message.error(err);
            });
        },
        row_click_event:function(row, event, column){
            this.twoTableData=row.DetailList;
        }
    },
    mounted(){
        this.getList();
    }

}
</script>



