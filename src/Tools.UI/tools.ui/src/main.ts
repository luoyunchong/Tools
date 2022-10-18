import { createApp } from 'vue'
import ElementPlus from 'element-plus'
import 'element-plus/dist/index.css'
import './style.css'
import App from './App.vue'
import store from './store'
import router from '@/router';
// 创建vue实例
const app = createApp(App)

// 挂载pinia
app.use(store)
app.use(router);
app.use(ElementPlus);
// 挂载实例
app.mount('#app');