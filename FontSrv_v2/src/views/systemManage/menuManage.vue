<template>
    <div >
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
                        <el-button type="primary"  icon="el-icon-plus" @click="view_event(row,'add')" >添 加</el-button>
                        <el-table :data="paginationEntity.tableData"   highlight-current-row style="width: 100%; margin-top: 5px">
                            <el-table-column type="index" width="50"></el-table-column>
                            <el-table-column prop="MenuNo" label="菜单编号" width="150" ></el-table-column>
                            <el-table-column prop="MenuName" label="菜单名" width="150" ></el-table-column>
                            <el-table-column prop="Order" label="排序" width="100" ></el-table-column>    
                            <el-table-column prop="MenuIcon" label="菜单图片" width="100" ></el-table-column>    
                            <el-table-column prop="MenuUrl" label="MenuUrl" ></el-table-column>    
                            <el-table-column label="操作" width="150">
                                <template slot-scope="{row}">
                                    <el-button  @click="view_event(row,'edit')" type="text">编辑</el-button>
                                    <el-button  @click="view_event(row,'delete')" type="text">删除</el-button>
                                </template>
                            </el-table-column>    
                            
                        </el-table>
                        <pagination v-show="paginationEntity.total>0" :total="paginationEntity.total" :page.sync="paginationEntity.currentPage" 
                            :limit.sync="paginationEntity.currentSize" @pagination="getList" />
                    </el-col>
                </el-row>
                
                <el-dialog
                    title="提示"
                    :visible.sync="dialogVisible"
                    width="650px"
                    :close-on-click-modal="false"
                    >
                    <el-form ref="saveEntity" :model="saveEntity" :rules="saveEntityRules"  label-width="80px">
                        <el-form-item label="所属菜单" prop="PMenuNo">
                            <treeselect  :multiple="false" :options="menuTree" :normalizer="normalizer" placeholder="默认全部" 
                            v-model="saveEntity.PMenuNo" value-label="默认全部" clearable></treeselect>
                        </el-form-item>
                        <el-form-item label="菜单编号" prop="MenuNo">
                            <el-input v-model="saveEntity.MenuNo" placeholder="请输入菜单编号"></el-input>
                        </el-form-item>
                        <el-form-item label="菜单名称" prop="MenuName">
                            <el-input v-model="saveEntity.MenuName" placeholder="请输入菜单名称"></el-input>
                        </el-form-item>
                        
                        <el-form-item label="菜单图片" prop="MenuIcon">
                            <el-input v-model="saveEntity.MenuIcon" disabled placeholder="选择菜单图片">
                                <template slot="prepend"><i :class="saveEntity.MenuIcon"></i></template>
                                <el-button slot="append" style="color:red"  icon="el-icon-search" @click="select_icon_click_event"></el-button>
                            </el-input>
                        </el-form-item>
                         <el-form-item label="菜单Url" prop="MenuUrl">
                            <el-input v-model="saveEntity.MenuUrl" placeholder="请输入菜单Url"></el-input>
                        </el-form-item>
                        <el-form-item label="排序" prop="Order">
                            <NumberInput v-model="saveEntity.Order" placeholder="请输入菜单排序"></NumberInput>
                        </el-form-item>
                    </el-form>
                    <span slot="footer" class="dialog-footer">
                        <el-button type="primary" :loading="loadObj.save_load" @click="save">确 定</el-button>
                    </span>
                </el-dialog>
                
                <QueryIcon :visible.sync="queryIconVisible" @select_IconVal="select_IconVal"></QueryIcon>
            </el-main>
        </el-container>
    </div>
</template>

<script>
import {GetMenuTree} from "@/api/systemManageApi"
import Pagination from '@/components/Pagination'
import NumberInput from '@/componentsSelf/NumberInput'
import QueryIcon from '@/componentsBll/QueryIcon'

import waves from '@/directive/waves'
export default {
    name:"menuManage1",
    components: { Pagination,NumberInput,QueryIcon },
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
            },

            queryIconVisible:false,
            funFlag:"",
            dialogVisible:false,
            saveEntity:{
                PMenuNo:null,
                MenuNo:"",
                MenuName:"",
                MenuIcon:"",
                MenuUrl:"",
                Order:"",
            },
            saveEntityRules:{
                PMenuNo: [
                    { required: true, message: '请选择所属菜单', trigger: 'blur' },
                ],
                MenuNo: [
                    { required: true, message: '请输入菜单编号', trigger: 'blur' },
                ],
                MenuName: [
                    { required: true, message: '请输入菜单名称', trigger: 'blur' },
                ],
                MenuIcon: [
                    { required: true, message: '请选择菜单图片', trigger: 'blur' },
                ],
                MenuUrl: [
                    { required: true, message: '请输入菜单Url', trigger: 'blur' },
                ],
                Order: [
                    { required: true, message: '请输入菜单排序', trigger: 'blur' },
                ],
            },
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
            var i=0;
        },
        GetMenuTree:function(){
            GetMenuTree({}).then(res=>{
                this.menuTree=new Array(res.data);
            }).then(err=>{

            })
        },
        normalizer(node) {
            if(!node.Children||node.Children.length<=0){
                return {
                    id: node.MenuNo,
                    label: node.MenuName,
                }
            }
            return {
                id: node.MenuNo,
                label: node.MenuName,
                children: node.Children,
            }
        },
        select_icon_click_event:function(){
            this.queryIconVisible=true;
        },
        select_IconVal:function(obj){
            console.info(obj);
            this.saveEntity.MenuIcon=obj;
        },
        view_event:function(row,flag){
            this.funFlag=flag
            if(flag=="add"){
                this.dialogVisible=true;
            }
        },
        save:function(){

        }
    },
    mounted:function(){
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