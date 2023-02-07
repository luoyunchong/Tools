import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'
import * as path from 'path';
import AutoImport from 'unplugin-auto-import/vite'
import Components from 'unplugin-vue-components/vite'
import { NaiveUiResolver } from 'unplugin-vue-components/resolvers'

// https://vitejs.dev/config/
export default defineConfig({
    plugins: [
        vue(),
        AutoImport({
          include: [
            /\.[tj]sx?$/, // .ts, .tsx, .js, .jsx
            /\.vue$/,
            /\.vue\?vue/ // .vue
          ],
          dts: 'typings/auto-imports.d.ts',
          imports: [
            'vue',
            'vue-router', 
            'pinia',
            {
              '@vueuse/core': [
                // named imports
                'useMouse', // import { useMouse } from '@vueuse/core',
                // alias
                ['useFetch', 'useMyFetch'], // import { useFetch as useMyFetch } from '@vueuse/core',
              ],
              'axios': [
                // default imports
                ['default', 'axios'], // import { default as axios } from 'axios',
              ],
            },
            {
              'naive-ui': [
                'useDialog',
                'useMessage',
                'useNotification',
                'useLoadingBar'
              ]
            }
          ]
        }),
        Components({
          resolvers: [NaiveUiResolver()]
        })
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
