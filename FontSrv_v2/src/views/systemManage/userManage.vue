<template>
    <div>
        <el-container>
            <el-main >
                <el-row tyle="display:flex">
                    <el-col :span="20">
                        <el-form :inline="true" :model="queryWhereEntity" class="demo-form-inline">
                            <el-form-item label="用户姓名">
                                <el-input v-model="queryWhereEntity.name" placeholder="请输入用户姓名"></el-input>
                            </el-form-item>
                        </el-form>
                    </el-col>
                    <el-col :span="4" style="display:flex;align-items: flex-end;justify-content: flex-end;flex-direction: column;margin-bottom: 22px;">
                        <div>
                            <el-button v-waves type="primary" icon="el-icon-search" @click="query_event">查询</el-button>
                            <template v-for="(item,index) in menuButtonEntity"  >
                                <el-button v-if="item.BtnNo=='add'" type="primary"  @click="view_event('','add')" :key="index">新增</el-button>
                            </template>
                            <ExpandButton @click="expandClick_event"></ExpandButton>
                        </div>
                    </el-col>
                </el-row>
                <el-button v-waves type="primary" @click="query_event">新增</el-button>
                 <el-button v-waves type="primary" @click="query_event">新增</el-button>
                <el-table style="width: 100%;margin-top:20px" :data="tableData"  key="tableData"
                    v-loading='loadObj.table_load'
                    highlight-current-row  >
                    <el-table-column type="index" width="50" label="序号"></el-table-column>
                    <el-table-column prop="UserName" label="用户名" width="100"></el-table-column>
                    <el-table-column prop="Name" label="姓名"  width="100"></el-table-column>
                    <el-table-column prop="Phone" label="手机号码" width="100"></el-table-column>
                    <el-table-column prop="Sex" label="性别" width="110"> </el-table-column>
                    <el-table-column prop="UpdateDateTime" label="最后更新日期" > </el-table-column>
                    
                </el-table>
                <pagination v-show="paginationEntity.total>0" :total="paginationEntity.total" :page.sync="paginationEntity.currentPage" 
                    :limit.sync="paginationEntity.currentSize" @pagination="getList" />
            </el-main>
        </el-container>
    </div>
</template>
<script>
import {GetUserList} from "@/api/systemManageApi"
import Pagination from '@/components/Pagination'
import waves from '@/directive/waves'
export default {
    name:"userManage1",
    components: { Pagination },
    directives: { waves },
    data() {
        return {
            loadObj:{
                table_load:false,
            },
            menuButtonEntity:[],
            queryWhereEntity:{
                name:"",
                phone:"",
            },
            isShowQueryWhere:false,
            tableData:[],
            paginationEntity:{
                currentPage:1,
                currentSize:20,
                total:800,
            },
        }
    },
    methods: {
        expandClick_event:function(flag){
            this.isShowQueryWhere=flag;
        },
        query_event:function(){

        },
        view_event:function(){

        },
        getList:function(){
            var param={
                page:this.paginationEntity.currentPage,
                pageSize:this.paginationEntity.currentSize,
                name:this.queryWhereEntity.name,
                phone:this.queryWhereEntity.phone
            }
            this.loadObj.table_load=true;
            GetUserList(param).then(res=>{
                this.loadObj.table_load=false;
                this.tableData=res.data;
            }).catch(err=>{
                this.loadObj.table_load=false;
                this.$message.error("你没有此门店的审核权限");
            })

        }
    },
    mounted(){
        this.getList();
    }
}
</script>

