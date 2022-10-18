import { createRouter, createWebHistory, RouteRecordRaw } from 'vue-router';

const routes: Array<RouteRecordRaw> = [

    {
        path: '/vueUse',
        name: 'vueUse',
        meta: {
            title: 'vueUse',
            keepAlive: true,
            requireAuth: false
        },
        component: () => import('@/pages/vueUse.vue')
    }, {
        path: '/login',
        name: 'Login',
        meta: {
            title: '登录',
            keepAlive: true,
            requireAuth: false
        },
        component: () => import('@/pages/login.vue')
    }, 
    {
        path: '/',
        name: 'Index',
        meta: {
            title: '首页',
            keepAlive: true,
            requireAuth: true
        },
        component: () => import('@/pages/index.vue')
    },
    {
        path: '/request',
        name: 'RequestPage',
        meta: {
            title: 'request demo',
            keepAlive: true,
            requireAuth: true
        },
        component: () => import('@/pages/request.vue')
    }
]

const router = createRouter({
    history: createWebHistory(),
    routes
});
export default router;