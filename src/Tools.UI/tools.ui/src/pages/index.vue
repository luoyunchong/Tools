 
<template>
  <n-space vertical size="large">
      <n-layout embedded content-style="padding: 24px;">
        <n-layout-content content-style="padding: 24px;">
          <n-grid x-gap="12" :cols="4">
            <n-gi>
              <n-card>
                <router-link :to="'/md5'">MD5</router-link>
              </n-card>
            </n-gi>
            <n-gi>
              <n-card>
                <router-link :to="'/base64'">Base64</router-link>
              </n-card>
            </n-gi>
            <n-gi>
              <n-card>
                <router-link :to="'/sm4'">国密SM4</router-link>
              </n-card>
            </n-gi>
            <n-gi>
              <n-card>
                <router-link :to="'/sm3'">SM3</router-link>
              </n-card>
            </n-gi>
          </n-grid>
        </n-layout-content>
      </n-layout>
  </n-space>

</template>

 
<script lang="ts" setup>

</script>

<style lang="scss">
.root {
  ul li {
    list-style: none;
  }
}
</style>
