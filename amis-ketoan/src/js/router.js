import { createRouter, createWebHistory } from 'vue-router'
import EmployeeList from '../components/page/EmployeeList.vue'
// import TableList from '../components/page/TableList.vue'
import HomeList from '../components/page/HomeList.vue'
// import axios from 'axios'
// import VueAxios from 'vue-axios' 
const router = createRouter({
    history: createWebHistory(),
    routes: [ // { path: '/', component: Home },
        { path: '/employee', component: EmployeeList },
        // { path: '/employee', component: TableList },
        { path: '/', component: HomeList },
    ],
})

export default router;