<template>
    <admin-layout :fixed-footer="false" :footer-visible="true" :tab-visible="false">
        <template #header>
            <div class="h-full dark:bg-[#18181c] dark:text-white dark:text-opacity-82 transition-all duration-300 ease-in-out"
                :class="inverted ? 'bg-[#001428] text-white' : 'bg-white text-[#333639]'
                    ">
                <h1 class="pl-8px text-26px font-bold text-primary transition duration-300 ease-in-out">
                    在线工具
                </h1>
            </div>
        </template>
        <!-- <template #tab></template> -->
        <template #sider>
            <n-menu v-model:value="activeKey" mode="vertical" :options="menuOptions" />
        </template>
        <div :class="{ 'p-16px': showPadding }"
            class="h-full bg-[#f6f9f8] dark:bg-[#101014] transition duration-300 ease-in-out">
            <router-view v-slot="{ Component, route }">
                <component :is="Component" :key="route.fullPath" />
            </router-view>
        </div>
        <template #footer>
            <div class="flex-center h-full">
                <span> Copyright ©2023 igeekfan All Rights Reserved</span>
            </div>
        </template>
    </admin-layout>
</template>

<script setup lang="ts">
    import { NIcon } from "naive-ui";
    import AdminLayout from "@soybeanjs/vue-admin-layout";
    import {
        BookOutline,
        BaseballSharp,
        LogoFirebase,
        Boat,
        Bookmarks,
        BrowsersOutline,
        BookSharp,
        CarSport,
        CheckmarkCircleSharp,
    } from "@vicons/ionicons5";
    import { RouterLink } from "vue-router";
    function renderIcon(icon: Component) {
        return () => h(NIcon, null, { default: () => h(icon) });
    }
    const route = useRoute();
    const activeKey = computed(
        () => (route.meta?.activeMenu ? route.meta.activeMenu : route.name) as string
    );
    const showPadding = ref(true);
    const inverted = ref(false);
    const menuOptions = [
        {
            label: () =>
                h(
                    RouterLink,
                    {
                        to: {
                            name: "index",
                        },
                    },
                    { default: () => "所有工具" }
                ),
            key: "index",
            icon: renderIcon(BookOutline),
        },
        {
            label: () =>
                h(
                    RouterLink,
                    {
                        to: {
                            name: "md5",
                        },
                    },
                    { default: () => "MD5" }
                ),
            key: "md5",
            icon: renderIcon(CarSport),
        },
        {
            label: () =>
                h(
                    RouterLink,
                    {
                        to: {
                            name: "sm4",
                        },
                    },
                    { default: () => "SM4" }
                ),
            key: "sm4",
            icon: renderIcon(CheckmarkCircleSharp),
        },
        {
            label: () =>
                h(
                    RouterLink,
                    {
                        to: {
                            name: "des",
                        },
                    },
                    { default: () => "DES" }
                ),
            key: "des",
            icon: renderIcon(LogoFirebase),
        },
        {
            label: () =>
                h(
                    RouterLink,
                    {
                        to: {
                            name: "base64",
                        },
                    },
                    { default: () => "BASE64" }
                ),
            key: "base64",
            icon: renderIcon(BaseballSharp),
        },
        {
            label: () =>
                h(
                    RouterLink,
                    {
                        to: {
                            name: "uri",
                        },
                    },
                    { default: () => "URI" }
                ),
            key: "uri",
            icon: renderIcon(BaseballSharp),
        },
        {
            label: () =>
                h(
                    RouterLink,
                    {
                        to: {
                            name: "json",
                        },
                    },
                    { default: () => "JSON" }
                ),
            key: "json",
            icon: renderIcon(BrowsersOutline),
        },
        {
            label: () =>
                h(
                    RouterLink,
                    {
                        to: {
                            name: "jsontocs",
                        },
                    },
                    { default: () => "JSON转C#" }
                ),
            key: "jsontocs",
            icon: renderIcon(BrowsersOutline),
        },
        {
            label: () =>
                h(
                    RouterLink,
                    {
                        to: {
                            name: "captcha",
                        },
                    },
                    { default: () => "图片验证码" }
                ),
            key: "captcha",
            icon: renderIcon(BookSharp),
        },
        {
            label: () =>
                h(
                    RouterLink,
                    {
                        to: {
                            name: "timestamp",
                        },
                    },
                    { default: () => "时间戳转换" }
                ),
            key: "timestamp",
            icon: renderIcon(Boat),
        },
        {
            label: () =>
                h(
                    RouterLink,
                    {
                        to: {
                            name: "markdown",
                        },
                    },
                    { default: () => "MarkDown预览" }
                ),
            key: "markdown",
            icon: renderIcon(Bookmarks),
        },
    ];
</script>
