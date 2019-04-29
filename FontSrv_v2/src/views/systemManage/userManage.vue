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
                            <el-form-item label="手机号码">
                                <el-input v-model="queryWhereEntity.phone" placeholder="请输入手机号码"></el-input>
                            </el-form-item>
                        </el-form>
                    </el-col>
                    <el-col :span="4" style="display:flex;align-items: flex-end;justify-content: flex-end;flex-direction: column;margin-bottom: 22px;">
                        <div>
                            <el-button v-waves type="primary" :loading="loadObj.table_load" icon="el-icon-search" @click="getList">查询</el-button>
                            <template v-for="(item,index) in menuButtonEntity"  >
                                <el-button v-if="item.BtnNo=='add'" type="primary"  @click="view_event('','add')" :key="index">新增</el-button>
                            </template>
                            <ExpandButton @click="expandClick_event"></ExpandButton>
                        </div>
                    </el-col>
                </el-row>
                <!-- <router-link :to="{ path: '/systemManage/userManageAdd', query: { plan: 'private' }}" class="link-type">
                   
                </router-link> -->
                <el-button v-waves type="primary" @click="view_event('','add')" >新增</el-button>
                
                <el-table style="width: 100%;margin-top:20px" :data="paginationEntity.tableData"  key="tableData"
                    v-loading='loadObj.table_load'
                    highlight-current-row  >
                    <el-table-column type="index" label="序号"  width="50"></el-table-column>
                    <el-table-column prop="UserName" label="用户名" width="100"></el-table-column>
                    <el-table-column prop="Name" label="姓名"  width="100"></el-table-column>
                    <el-table-column prop="Phone" label="手机号码" width="150"></el-table-column>
                    <el-table-column prop="Sex" label="性别" width="110"> </el-table-column>
                    <el-table-column prop="UpdateDateTime" label="最后更新日期" > </el-table-column>
                    <el-table-column  label="操作"  width="200" >
                        <template slot-scope="{row}">
                            <el-button  @click="view_event(row,'edit')" type="text">编辑</el-button>
                        </template>
                    </el-table-column>
                </el-table>
                <pagination v-show="paginationEntity.total>0" :total="paginationEntity.total" :page.sync="paginationEntity.currentPage" 
                    :limit.sync="paginationEntity.currentSize" @pagination="getList" />

                <el-dialog
                    title="提示"
                    :visible.sync="dialogVisible"
                    width="800px"
                    :close-on-click-modal="false"
                    >
                    <el-form ref="saveEntity" :model="saveEntity" :rules="saveEntityRules"  label-width="80px">
                        <el-form-item label="用户名" prop="userName">
                            <el-input v-model="saveEntity.userName" disabled></el-input>
                        </el-form-item>
                        <el-form-item label="密码"  prop="pwd">
                            <el-input type="password" v-model="saveEntity.pwd"></el-input>
                        </el-form-item>
                        <el-form-item label="姓名" prop="name">
                            <el-input v-model="saveEntity.name"></el-input>
                        </el-form-item>
                        <el-form-item label="手机号码" prop="phone">
                            <el-input v-model="saveEntity.phone"></el-input>
                        </el-form-item>
                        <el-form-item label="性别" prop="sex">
                            <el-select v-model="saveEntity.sex" placeholder="请选择性别" style="width:100%">
                                <el-option label="男" value="男"></el-option>
                                <el-option label="女" value="女"></el-option>
                            </el-select>
                        </el-form-item>
                    </el-form>
                    <span slot="footer" class="dialog-footer">
                        <el-button @click="dialogVisible = false">取 消</el-button>
                        <el-button type="primary" :loading="loadObj.save_load" @click="save">确 定</el-button>
                    </span>
                </el-dialog>

            </el-main>
        </el-container>
    </div>
</template>
<script>
import {GetUserList,CreateUser,UpdateUser} from "@/api/systemManageApi"
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
                save_load:false,
            },
            menuButtonEntity:[],
            queryWhereEntity:{
                name:"",
                phone:"",
            },
            isShowQueryWhere:false,
            paginationEntity:{
                currentPage:1,
                currentSize:20,
                tableData:[],
                total:0,
            },
            dialogVisible:false,
            funFlag:"",
            saveEntity:{
                name:"",
                userName:"",
                pwd:"",
                sex:"",
                phone:"",
            },
            saveEntityRules:{
                userName: [
                    { required: true, message: '请输入用户名', trigger: 'blur' },
                ],
                pwd: [
                    { required: true, message: '请输入密码', trigger: 'blur' },
                ],
                name: [
                    { required: true, message: '请输入姓名', trigger: 'blur' },
                ],
                phone: [
                    { required: true, message: '请输入手机号码', trigger: 'blur' },
                ],
                sex: [
                    { required: true, message: '请输入性别', trigger: 'blur' },
                ],
            }
        }
    },
    methods: {
        expandClick_event:function(flag){
            this.isShowQueryWhere=flag;
        },
        view_event:function(row,flag){
            this.funFlag=flag
            if(flag=="add"){
                this.dialogVisible=true;
                this.saveEntity.userName=row.UserName;
                this.saveEntity.name=row.Name;
                this.saveEntity.pwd=row.Pwd;
                this.saveEntity.sex=row.Sex;
                this.saveEntity.phone=row.Phone;
            }
            else if(flag=="edit"){
                this.dialogVisible=true;
                this.saveEntity.Id=row.Id;
                this.saveEntity.userName=row.UserName;
                this.saveEntity.name=row.Name;
                this.saveEntity.pwd=row.Pwd;
                this.saveEntity.sex=row.Sex;
                this.saveEntity.phone=row.Phone;
            }
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
                this.paginationEntity.tableData=res.data;
                this.paginationEntity.total=res.extend;
                
            }).catch(err=>{
                this.loadObj.table_load=false;
                this.$message.error(err);
            })

        },
        save:function(){
            this.$refs.saveEntity.validate((valid) => {
                if(valid){
                    this.loadObj.save_load=true;
                    if(this.funFlag=="add"){
                        CreateUser(this.saveEntity).then(res=>{
                            this.$notify({
                                title: '成功',
                                message: "成功",
                                type: 'success',
                                duration:500,
                                onClose:()=>{
                                    this.dialogVisible=false;
                                    this.loadObj.save_load=false;
                                    this.getList();
                                },
                            }); 
                        }).catch(err=>{
                            this.loadObj.save_load=false;
                        })
                    }
                    else if(this.funFlag=="edit"){

                        UpdateUser(this.saveEntity).then(res=>{
                            this.$notify({
                                title: '成功',
                                message: "成功",
                                type: 'success',
                                duration:500,
                                onClose:()=>{
                                    this.dialogVisible=false;
                                    this.loadObj.save_load=false;
                                    this.getList();
                                },
                            }); 
                        }).catch(err=>{
                            this.loadObj.save_load=false;
                        })
                    }
                    
                }
            });
        }
    },
    mounted(){
        this.getList();
    }
}
</script>

