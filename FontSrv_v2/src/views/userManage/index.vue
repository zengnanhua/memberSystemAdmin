<template>
    <div>asdfasdf
        <el-input v-model="mm"></el-input>
        <ExpandButton></ExpandButton>
         <treeselect v-model="value" :multiple="true" :options="options" />
    </div>
</template>
<script>
export default {
    data() {
        return {
            value: null,
            options: [ {
            id: 'a',
            label: 'a',
            children: [ {
                id: 'aa',
                label: '阿斯顿发送到',
            }, {
                id: 'ab',
                label: '阿斯顿发',
            } ],
            }, {
                id: 'b',
                label: '发的方法方法',
            }, {
                id: 'c',
                label: '风帆股份',
            } ],

            mm:"",
        }
    },
    methods: {

    }
}
</script>

