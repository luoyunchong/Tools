 
<template>
  <div class="root">
    <n-card title="字符串转换">
      <n-input v-model:value="value" type="textarea" placeholder="请输入要转换的字符串" />

      <n-button type="primary" @click="md5">
        MD5
      </n-button>
      <n-input v-model:value="value2" type="text" placeholder="转换结果" />
      <n-input v-model:value="value3" type="text" placeholder="转换结果" />
      <n-input v-model:value="value4" type="text" placeholder="转换结果" />
      <n-input v-model:value="value5" type="text" placeholder="转换结果" />
    </n-card>
  </div>
</template>

 
<script lang="ts" setup>
import API from '@/api';

const value = ref('12');
const value2 = ref('');
const value3 = ref('');
const value4 = ref('');
const value5 = ref('');
  
const md5 = async () => {
  let result = await API.allmd5(value.value);
  console.log(result);

  value2.value = result.Md5Digit32
  value3.value = result.Md5Digit16
  value4.value = result.Md5Digit32Lower
  value5.value = result.Md5Digit16Lower
};



</script>

<style lang="scss">
.root {
  ul li {
    list-style: none;
  }
}
</style>
