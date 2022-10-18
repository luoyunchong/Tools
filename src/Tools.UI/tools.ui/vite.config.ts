import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'
import * as path from 'path';
import {
    createStyleImportPlugin,
    ElementPlusResolve,
} from 'vite-plugin-style-import';

// https://vitejs.dev/config/
export default defineConfig({
    plugins: [
        vue(),
        createStyleImportPlugin({
            resolves: [ElementPlusResolve()],
            libs: [
                {
                    libraryName: 'element-plus',
                    esModule: true,
                    resolveStyle: (name: string) => {
                        name = name.substring(3, name.length);
                        return `element-plus/es/components/${name}/style/index`;
                    },
                },
            ],
        }),
    ],
    resolve: {
        alias: {
            '@': path.resolve(__dirname, 'src')
        }
    },
    server: {
        port: 8084, //启动端口
        hmr: {
            host: '127.0.0.1',
            port: 8084
        },
        // 设置 https 代理
        // proxy: {
        //     '/api': {
        //         target: 'your https address',
        //         changeOrigin: true,
        //         rewrite: (path: string) => path.replace(/^\/api/, '')
        //     }
        // }
    }
})
