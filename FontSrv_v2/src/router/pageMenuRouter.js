import Layout from '@/layout'
const pageMenuRouter = {
    path: '',
    component: Layout,
    children: [

        {
            path: '/systemManage/userManage',
            name: 'userManage',
            component: () =>
                import ('@/views/systemManage/userManage')
        },
        {
            path: '/systemManage/userManageAdd',
            name: "userManageAdd",
            meta: { title: "添加用户" },
            component: () =>
                import ('@/views/systemManage/userManageAdd')
        },
        {
            path: '/systemManage/menuManage',
            name: 'menuManage',
            component: () =>
                import ('@/views/systemManage/menuManage')
        },
        {
            path: '/systemManage/roleManage',
            name: 'roleManage',
            component: () =>
                import ('@/views/systemManage/roleManage')
        },
        {
            path: '/systemManage/attributeManage',
            name: 'attributeManage',
            component: () =>
                import ('@/views/systemManage/attributeManage')
        },
        

    ]
}
export default pageMenuRouter