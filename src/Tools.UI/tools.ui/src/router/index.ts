import { createRouter, createWebHashHistory, RouteRecordRaw } from "vue-router";

const routes: Array<RouteRecordRaw> = [
  {
    path: "/",
    name: "",
    component: () => import("@/layout/index.vue"),
    children: [
      {
        path: "/",
        name: "index",
        meta: {
          title: "首页",
          keepAlive: true,
          requireAuth: true,
        },
        component: () => import("@/pages/index.vue"),
      },
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
        path: "/des",
        name: "des",
        meta: {
          title: "DES分组密码（块密码）加密技术",
          keepAlive: true,
          requireAuth: false,
        },
        component: () => import("@/pages/des.vue"),
      },
      {
        path: "/sm4",
        name: "sm4",
        meta: {
          title: "国密SM4",
          keepAlive: true,
          requireAuth: false,
        },
        component: () => import("@/pages/sm4.vue"),
      },
      {
        path: "/uri",
        name: "uri",
        meta: {
          title: "uri编码",
          keepAlive: true,
          requireAuth: false,
        },
        component: () => import("@/pages/uri.vue"),
      },
      {
        path: "/json",
        name: "json",
        meta: {
          title: "json格式化",
          keepAlive: true,
          requireAuth: false,
        },
        component: () => import("@/pages/json.vue"),
      },
      {
        path: "/captcha",
        name: "captcha",
        meta: {
          title: "图片验证码",
          keepAlive: true,
          requireAuth: false,
        },
        component: () => import("@/pages/captcha.vue"),
      },
      {
        path: "/timestamp",
        name: "timestamp",
        meta: {
          title: "时间戳转换",
          keepAlive: true,
          requireAuth: false,
        },
        component: () => import("@/pages/timestamp.vue"),
      },
    ],
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
  history: createWebHashHistory(),
  routes,
});
export default router;
