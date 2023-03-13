import { createApp } from "vue";
import setupAssets from "./plugins";
import App from "./App.vue";
import store from "./store";
import router from "@/router";

function setupApp() {
  // import assets: js、css
  setupAssets();
  // 创建vue实例
  const app = createApp(App);
  // 挂载pinia
  app.use(store);
  app.use(router);
  // 挂载实例
  app.mount("#app");
}

setupApp();
