import { createRouter, createWebHistory, RouteRecordRaw } from 'vue-router';

const routes: Array<RouteRecordRaw> = [
  {
    path: "/base64",
    name: "base64",
    meta: {
      title: "base64",
      keepAlive: true,
      requireAuth: false,
    },
    component: () => import("@/pages/base64.vue"),
  },
  {
    path: "/md5",
    name: "md5",
    meta: {
      title: "md5",
      keepAlive: true,
      requireAuth: false,
    },
    component: () => import("@/pages/md5.vue"),
  },
  {
    path: "/sm4",
    name: "SM4",
    meta: {
      title: "国密SM4",
      keepAlive: true,
      requireAuth: false,
    },
    component: () => import("@/pages/sm4.vue"),
  },
  {
    path: "/",
    name: "Index",
    meta: {
      title: "首页",
      keepAlive: true,
      requireAuth: true,
    },
    component: () => import("@/pages/index.vue"),
  },
  {
    path: "/request",
    name: "RequestPage",
    meta: {
      title: "request demo",
      keepAlive: true,
      requireAuth: true,
    },
    component: () => import("@/pages/request.vue"),
  },
];

const router = createRouter({
    history: createWebHistory(),
    routes
});
export default router;