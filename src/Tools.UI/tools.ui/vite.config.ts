import { defineConfig } from "vite";
import vue from "@vitejs/plugin-vue";
import AutoImport from "unplugin-auto-import/vite";
import Components from "unplugin-vue-components/vite";
import { NaiveUiResolver } from "unplugin-vue-components/resolvers";
import { fileURLToPath, URL } from "node:url";
import UnoCSS from '@unocss/vite'

// https://vitejs.dev/config/
export default defineConfig({
    base: "/tools/",
    plugins: [
        vue(),
        AutoImport({
            include: [
                /\.[tj]sx?$/, // .ts, .tsx, .js, .jsx
                /\.vue$/,
                /\.vue\?vue/,
            ],
            dts: "typings/auto-imports.d.ts",
            imports: [
                "vue",
                "vue-router",
                "pinia",
                {
                    "naive-ui": [
                        "useDialog",
                        "useMessage",
                        "useNotification",
                        "useLoadingBar",
                    ],
                },
            ],
        }),
        Components({
            resolvers: [NaiveUiResolver()],
        }),
        UnoCSS({
            presets: [], // disable default preset
            rules: [
                // your custom rules
            ],
        }),
    ],
    resolve: {
        alias: {
            "@": fileURLToPath(new URL("./src", import.meta.url)),
        },
    },
    server: {
        port: 8084, //启动端口
        hmr: {
            clientPort: 8084,
        },
        // 设置 https 代理
        // proxy: {
        //     '/api': {
        //         target: 'your https address',
        //         changeOrigin: true,
        //         rewrite: (path: string) => path.replace(/^\/api/, '')
        //     }
        // }
    },
});
