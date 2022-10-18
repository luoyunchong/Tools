 
<template>
  <div class="root">
    <h1>index</h1>
    <ul>
      <li>
        <router-link to="login">login</router-link>
      </li>
      <li>
        <router-link to="vueUse">vueUse</router-link>
      </li>
      <li>
        <router-link to="request">request</router-link>
      </li>
    </ul>
  </div>
  <el-row class="mb-4">
    <el-button>Default</el-button>
    <el-button type="primary">Primary</el-button>
    <el-button type="success">Success</el-button>
    <el-button type="info">Info</el-button>
    <el-button type="warning">Warning</el-button>
    <el-button type="danger">Danger</el-button>
  </el-row>
</template>

 
<script lang="ts">
import { defineComponent } from "vue";

export default defineComponent({
  name: "Index",
   setup() {
  },
});
</script>

<style lang="scss">
.root {
  ul li {
    list-style: none;
  }
}
</style>
